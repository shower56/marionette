package org.probit.marionette;

import org.probit.marionette.bluetooth.BluetoothChatService;
import org.probit.marionette.bluetooth.DeviceListActivity;
import org.probit.marionette.usb.SocketThread;

import android.os.Bundle;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Intent;
import android.support.v4.app.NotificationCompat;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.CheckBox;
import android.widget.ImageButton;
import android.widget.TextView;

public class MainActivity extends BluetoothChat {
	public static final int NOTIFICATION_STATE_NONE = 0; 
	public static final int NOTIFICATION_STATE_BLUETOOTH = 1;
	public static final int NOTIFICATION_STATE_SERIAL = 2;
	public static final int NOTIFICATION_STATE_ALL = 3;
	
	// CheckBox checkBox_Bluetooth;
	// CheckBox checkBox_Serial;
	CheckBox checkBox_Connect_Text;
	CheckBox checkBox_Connect_Line;

	CheckBox checkBox_Serial_Line;
	CheckBox checkBox_Serial_Icon;

	CheckBox checkBox_Bluetooth_Line;
	CheckBox checkBox_Bluetooth_Icon;

	TextView txt_Serial_Name;
	TextView txt_Bluetooth_Name;

	NotificationManager manager;
	
	OnClickListener onClickListener = new OnClickListener() {
		
		@Override
		public void onClick(View v) {
			// TODO Auto-generated method stub
			
			if(v.getId() == R.id.checkBox_Bluetooth_Icon) {
				if (checkBox_Bluetooth_Icon.isChecked()) {

					Intent serverIntent = null;
					serverIntent = new Intent(context, DeviceListActivity.class);
					startActivityForResult(serverIntent, REQUEST_CONNECT_DEVICE_SECURE);

				} else {
					quitBluetooth();
				}
			}
			
			if(v.getId() == R.id.checkBox_Serial_Icon) {
				if (!checkBox_Serial_Icon.isChecked()) {
					checkBox_Serial_Icon.setClickable(false);
					thread.quitSocket();
				}
				Log.i(TAG, "checkBox_Serial_Icon.isChecked() "+checkBox_Serial_Icon.isChecked());
			}
			
			
		}
	};

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		// Set up the window layout
		setContentView(R.layout.activity_main);

		ImageButton button1 = (ImageButton) findViewById(R.id.informButton);
		button1.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				Intent intent = new Intent(getApplicationContext(), TutorialActivity.class);
				startActivity(intent);
			}
		});

		checkBox_Serial_Line = (CheckBox) findViewById(R.id.checkBox_Serial_Line);
		checkBox_Serial_Icon = (CheckBox) findViewById(R.id.checkBox_Serial_Icon);
		checkBox_Serial_Icon.setOnClickListener(onClickListener);

		checkBox_Bluetooth_Line = (CheckBox) findViewById(R.id.checkBox_Bluetooth_Line);
		checkBox_Bluetooth_Icon = (CheckBox) findViewById(R.id.checkBox_Bluetooth_Icon);
		checkBox_Bluetooth_Icon.setOnClickListener(onClickListener);

		checkBox_Connect_Text = (CheckBox) findViewById(R.id.checkBox_Connect_Text);
		checkBox_Connect_Line = (CheckBox) findViewById(R.id.checkBox_Connect_Line);

		txt_Serial_Name = (TextView) findViewById(R.id.txt_Serial_Name);
		txt_Bluetooth_Name = (TextView) findViewById(R.id.txt_Bluetooth_Name);

		thread = new SocketThread(mSerialHandler, context);
		thread.start();

		state_of_serial = 0;
		state_of_bluetooth = 0;
		
		manager = (NotificationManager) getSystemService(NOTIFICATION_SERVICE);
	}
	
	

	
	@Override
	public void onDestroy() {
		// TODO Auto-generated method stub
		super.onDestroy();

		Log.d(TAG, "onDestroy");

		manager.cancel(1234);
		
		if (mChatService != null) {
			mChatService.stop();
			mChatService = null;
		}

		if (thread != null) {
			thread.quitServerSocket();
			thread = null;
		}
	}

	@Override
	public void setStatusBluetooth(int status) {
		// TODO Auto-generated method stub
		super.setStatusBluetooth(status);

		Log.d(TAG, "setStatusBluetooth " + state_of_bluetooth + " --> " + status);

		state_of_bluetooth = status;

		switch (state_of_bluetooth) {
		case BluetoothChatService.STATE_CONNECTED:
			checkBox_Bluetooth_Icon.setChecked(true);
			checkBox_Bluetooth_Line.setChecked(true);

			break;
		// case BluetoothChatService.STATE_CONNECTING:
		// break;
		case BluetoothChatService.STATE_LISTEN:
			checkBox_Bluetooth_Icon.setChecked(false);
			checkBox_Bluetooth_Line.setChecked(true);
			txt_Bluetooth_Name.setText("");

			break;
		case BluetoothChatService.STATE_NONE:
			checkBox_Bluetooth_Icon.setChecked(false);
			checkBox_Bluetooth_Line.setChecked(false);
			txt_Bluetooth_Name.setText("");
			
			break;
		}
		
		if(state_of_serial == MESSAGE_SERIAL_ACCEPT) {
			sendStateBluetooth(state_of_bluetooth);
			sendStateSerial(state_of_serial);
		}

		checkAllConnect();
	}

	@Override
	public void setStatusSerial(int status) {
		// TODO Auto-generated method stub
		super.setStatusSerial(status);

		Log.d(TAG, "setStatusSerial " + state_of_serial + " --> " + status);

		state_of_serial = status;

		switch (state_of_serial) {
		case MESSAGE_SERIAL_LISTEN:
			
			checkBox_Serial_Line.setChecked(true);
			checkBox_Serial_Icon.setChecked(false);
			
			checkBox_Serial_Icon.setClickable(false);

			txt_Serial_Name.setText("");
			
			break;
		case MESSAGE_SERIAL_ACCEPT:

			checkBox_Serial_Line.setChecked(true);
			checkBox_Serial_Icon.setChecked(true);

			checkBox_Serial_Icon.setClickable(true);
			
			break;
		case MESSAGE_SERIAL_DISCON:

			checkBox_Serial_Line.setChecked(false);
			checkBox_Serial_Icon.setChecked(false);

			checkBox_Serial_Icon.setClickable(false);
			
			txt_Serial_Name.setText("");
			break;
		}

		if(state_of_bluetooth == BluetoothChatService.STATE_CONNECTED) {
			sendStateBluetooth(state_of_bluetooth);
			sendStateSerial(state_of_serial);
		}
		
		checkAllConnect();
	}

	void setNotification(int status) {

		int icon = 0;
		String contentText = "";
		
//		if(manager != null)
//			manager.cancel(1234);
		
		switch(status) {
		case NOTIFICATION_STATE_ALL:
			icon = R.drawable.noti_all;
			contentText = "Connected";
			break;
		case NOTIFICATION_STATE_BLUETOOTH:
			icon = R.drawable.noti_bluetooth_only;
			contentText = "Bluetooth is connected";
			break;
		case NOTIFICATION_STATE_SERIAL:
			icon = R.drawable.noti_serial_only;
			contentText = "Serial is Connected";
			break;
//		case NOTIFICATION_STATE_NONE:
//			icon = R.drawable.noti_non;
//			contentText = "Disconnected";
//			break;
		}
		
		NotificationCompat.Builder builder = new NotificationCompat.Builder(getApplicationContext());
		builder.setSmallIcon(icon).setContentTitle("MarionetteService").setContentText(contentText).setOngoing(true);

		Intent i = new Intent(getApplicationContext(), MainActivity.class);

		i.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_CLEAR_TOP | Intent.FLAG_ACTIVITY_SINGLE_TOP);

		PendingIntent pIntent = PendingIntent.getActivity(getApplicationContext(), 0, i,
				PendingIntent.FLAG_UPDATE_CURRENT);

		builder.setContentIntent(pIntent);
		manager.notify(1234, builder.build());
	}

	@Override
	public void setTextBluetooth(String str) {
		Log.d(TAG, "setTextBluetooth " + str);
		txt_Bluetooth_Name.setText(str);
	}

	@Override
	public void setTextSerial(String str) {
		// TODO Auto-generated method stub
		Log.d(TAG, "setTextSerial " + str);
		txt_Serial_Name.setText(str);
	}

	@Override
	public void onBackPressed() {
		// TODO Auto-generated method stub

		if ((state_of_bluetooth == BluetoothChatService.STATE_CONNECTED)
				|| (state_of_serial == BluetoothChat.MESSAGE_SERIAL_ACCEPT)) {
			Log.d(TAG, "BACKGROUND");
			Intent intent = new Intent();
			intent.setAction("android.intent.action.MAIN");
			intent.addCategory("android.intent.category.HOME");
			intent.addFlags(Intent.FLAG_ACTIVITY_EXCLUDE_FROM_RECENTS | Intent.FLAG_ACTIVITY_FORWARD_RESULT
					| Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_PREVIOUS_IS_TOP
					| Intent.FLAG_ACTIVITY_RESET_TASK_IF_NEEDED);
			startActivity(intent);
		} else
			super.onBackPressed();

	}

	void checkAllConnect() {
		Log.i(TAG, "checkAllConnect");
		
		// connected all
		if (state_of_bluetooth == BluetoothChatService.STATE_CONNECTED
				&& state_of_serial == BluetoothChat.MESSAGE_SERIAL_ACCEPT) {

			checkBox_Connect_Line.setChecked(true);
			checkBox_Connect_Text.setChecked(true);
			
			setNotification(NOTIFICATION_STATE_ALL);
			
		} else if (state_of_bluetooth == BluetoothChatService.STATE_CONNECTED) {

			checkBox_Connect_Line.setChecked(false);
			checkBox_Connect_Text.setChecked(false);
			
			setNotification(NOTIFICATION_STATE_BLUETOOTH);
			
		} else if (state_of_serial == BluetoothChat.MESSAGE_SERIAL_ACCEPT) {

			checkBox_Connect_Line.setChecked(false);
			checkBox_Connect_Text.setChecked(false);
			
			setNotification(NOTIFICATION_STATE_SERIAL);
			
		}else {
			
			if(state_of_bluetooth != BluetoothChatService.STATE_CONNECTED)
				checkBox_Bluetooth_Icon.setChecked(false);
			
			checkBox_Connect_Line.setChecked(false);
			checkBox_Connect_Text.setChecked(false);
			
			manager.cancel(1234);
		}

	}

}
