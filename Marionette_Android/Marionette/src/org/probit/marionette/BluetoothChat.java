package org.probit.marionette;

import java.io.UnsupportedEncodingException;

import org.probit.marionette.bluetooth.BluetoothChatService;
import org.probit.marionette.bluetooth.DeviceListActivity;
import org.probit.marionette.usb.MessageVO;
import org.probit.marionette.usb.SocketThread;

import android.app.Activity;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.widget.Toast;

/**
 * This is the main Activity that displays the current chat session.
 */
public class BluetoothChat extends Activity {
	public String TAG = getClass().getSimpleName();
	final int BUFFER_SIZE = 128;
	
	public static final int MESSAGE_STATE_CHANGE = 1;
	public static final int MESSAGE_READ = 2;
	public static final int MESSAGE_WRITE = 3;
	public static final int MESSAGE_DEVICE_NAME = 4;
	public static final int MESSAGE_TOAST = 5;

	public static final int MESSAGE_SERIAL_LISTEN = 1;
	public static final int MESSAGE_SERIAL_ACCEPT = 2;
	public static final int MESSAGE_SERIAL_DISCON = 3;
	public static final int MESSAGE_SERIAL_READ = 0;
	public static final int MESSAGE_SERIAL_WRITE = 5;
	public static final int MESSAGE_SERIAL_DEVICE = 6;
	public static final int MESSAGE_SERIAL_CLIPBOARD = 7;

	int state_of_serial;
	int state_of_bluetooth;

	public static final String DEVICE_NAME = "device_name";
	public static final String TOAST = "toast";

	public static final int REQUEST_CONNECT_DEVICE_SECURE = 1;
	public static final int REQUEST_CONNECT_DEVICE_INSECURE = 2;
	public static final int REQUEST_ENABLE_BT = 3;

	private String mConnectedDeviceName = null;
	public BluetoothAdapter mBluetoothAdapter = null;
	public BluetoothChatService mChatService = null;

	SocketThread thread;

	Context context;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		context = this;

		mBluetoothAdapter = BluetoothAdapter.getDefaultAdapter();

		if (mBluetoothAdapter == null) {
			Toast.makeText(this, "Bluetooth is not available", Toast.LENGTH_LONG).show();
			finish();
			return;
		}

		Log.d(TAG, "onCreate");
	}

	@Override
	public void onStart() {
		super.onStart();

		if (!mBluetoothAdapter.isEnabled()) {
			Intent enableIntent = new Intent(BluetoothAdapter.ACTION_REQUEST_ENABLE);
			startActivityForResult(enableIntent, REQUEST_ENABLE_BT);
		} else {
			if (mChatService == null)
				setupChat();
		}
		Log.d(TAG, "onStart");
	}

	@Override
	public synchronized void onResume() {
		super.onResume();

		if (mChatService != null) {
			if (mChatService.getState() == BluetoothChatService.STATE_NONE) {
				mChatService.start();
			}
		}
		Log.d(TAG, "onResume");
	}

	protected void setupChat() {
		Log.d(TAG, "setupChat()");
		mChatService = new BluetoothChatService(this, mHandler);
	}

	final Handler mHandler = new Handler() {
		@Override
		public void handleMessage(Message msg) {
			switch (msg.what) {
			case MESSAGE_STATE_CHANGE:
				
				int status = msg.arg1;
				setStatusBluetooth(status);
				break;
			case MESSAGE_READ:
				
				String str2 = (String) msg.obj;

				byte[] readBuf = null;
				try {
					readBuf = str2.getBytes("UTF-8");
				} catch (UnsupportedEncodingException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
				
				if(readBuf[0] == 100) {
					quitBluetooth();
					break;
				}
				
				String readMessage = new String(readBuf, 0, msg.arg1);
				thread.write(readBuf);
				Log.i("MESSAGE_READ", "readMessage " + readMessage);
				break;
				
			case MESSAGE_DEVICE_NAME:
				
				mConnectedDeviceName = msg.getData().getString(DEVICE_NAME);
				Toast.makeText(getApplicationContext(), "Connected to " + mConnectedDeviceName, Toast.LENGTH_SHORT)
						.show();
				setTextBluetooth(mConnectedDeviceName);
				break;
			case MESSAGE_TOAST:
				
				Toast.makeText(getApplicationContext(), msg.getData().getString(TOAST), Toast.LENGTH_SHORT).show();
				break;
			}
		}
	};

	
	public final Handler mSerialHandler = new Handler() {
		@Override
		public void handleMessage(Message msg) {

			int status = msg.what;
			switch (status) {

			// STATUS ----------------------------------------------
			case MESSAGE_SERIAL_LISTEN:
			case MESSAGE_SERIAL_ACCEPT:
			case MESSAGE_SERIAL_DISCON:
				setStatusSerial(status);
				break;
			// end of STATUS ----------------------------------------------

			case MESSAGE_SERIAL_READ:
				MessageVO mVO = (MessageVO) msg.obj;
				mChatService.write(mVO.buf);
				break;

			case MESSAGE_SERIAL_DEVICE:
				String str = (String) msg.obj;
				setTextSerial(str);
				break;

			case MESSAGE_SERIAL_CLIPBOARD:
				//byte[] byteClip = (byte[]) msg.obj;
				String str2 = (String) msg.obj;

				byte[] byteClip = null;
				try {
					byteClip = str2.getBytes("UTF-8");
				} catch (UnsupportedEncodingException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
				Log.i(TAG, (int)byteClip[0]+" "+(int)byteClip[1]+" "+(int)byteClip[2]);
				mChatService.write(byteClip);
				break;

			case MESSAGE_SERIAL_WRITE:
				break;
			}
		}
	};
	
	public void onActivityResult(int requestCode, int resultCode, Intent data) {
		switch (requestCode) {

		case REQUEST_CONNECT_DEVICE_SECURE:
			if (resultCode == Activity.RESULT_OK) {
				connectDevice(data, true);
			} else {
				checkAllConnect();
			}
			break;
		case REQUEST_CONNECT_DEVICE_INSECURE:
			if (resultCode == Activity.RESULT_OK) {
				connectDevice(data, false);
			} else {
				checkAllConnect();
			}
			break;
		case REQUEST_ENABLE_BT:
			if (resultCode == Activity.RESULT_OK) {
				setupChat();
			} else {
				Log.d(TAG, "BT not enabled");
				Toast.makeText(this, R.string.bt_not_enabled_leaving, Toast.LENGTH_SHORT).show();
				finish();
			}
			break;
		}
	}

	protected void connectDevice(Intent data, boolean secure) {
		String address = data.getExtras().getString(DeviceListActivity.EXTRA_DEVICE_ADDRESS);
		BluetoothDevice device = mBluetoothAdapter.getRemoteDevice(address);
		mChatService.connect(device, secure);
	}
	
	void quitBluetooth() {
		
		if (mChatService != null) {
			mChatService.stop();
			mChatService.start();
		}
	}

	// send bluetooth state to serial
	void sendStateBluetooth(int state) {
		byte[] message = new byte[BUFFER_SIZE];
		
		if(state == BluetoothChatService.STATE_CONNECTED)
			message[0] = 101;
		else if(state == BluetoothChatService.STATE_LISTEN)
			message[0] = 100;
		
		thread.write(message);
		
		Log.d("TAG", "to: Serial what: bluetooth is " + state);
	}
	
	// send serial state to bluetooth
	void sendStateSerial(int state) {
		byte[] message = new byte[BUFFER_SIZE];
		
		if(state == MESSAGE_SERIAL_ACCEPT)
			message[0] = 121;
		else
			message[0] = 120;
		
		mChatService.write(message);
		
		Log.d("TAG", "to: Bluetooth what: serial is " + state);
	}
	
	void checkAllConnect() {
	}
	void setTextBluetooth(String str) {
	}
	void setTextSerial(String str) {
	}
	void setStatusSerial(int status) {
	}
	void setStatusBluetooth(int status) {
	}


}