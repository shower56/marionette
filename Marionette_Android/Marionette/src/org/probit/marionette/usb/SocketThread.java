package org.probit.marionette.usb;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.ServerSocket;
import java.net.Socket;
import java.nio.charset.Charset;

import org.apache.http.entity.ByteArrayEntity;
import org.apache.http.util.ByteArrayBuffer;
import org.probit.marionette.BluetoothChat;
import org.probit.marionette.MainActivity;

import android.app.Activity;
import android.content.Context;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.widget.Toast;

public class SocketThread extends Thread {
	public String TAG = getClass().getSimpleName();
	private static final boolean D = true;
	
	final int BUFFER_SIZE = 128;

	private Handler mHandler;
	private boolean whileFlag;

	Context context = null;

	ServerSocket serverSocket = null;
	Socket socket = null;

	OutputStream os = null;
	InputStream is = null;

	public SocketThread(Handler handler, Context context) {
		if(D) Log.d(TAG, "SocketThread 생성자");

		this.mHandler = handler;
		this.whileFlag = true;
		this.context = context;
	}

	@Override
	public void run() {
		MessageVO mVO = null;

		// TODO Auto-generated method stub
		try {

			serverSocket = new ServerSocket(6549);
			if(D) Log.d(TAG, "ServerSocket 생성");

			while (true) {

				if (serverSocket == null) {
					// mHandler.obtainMessage(BluetoothChat.MESSAGE_SERIAL_DISCON,
					// 0, 0).sendToTarget();
					if(D) Log.d(TAG, "serverSocket is DEAD");
					break;
				}

				mHandler.obtainMessage(BluetoothChat.MESSAGE_SERIAL_LISTEN, 0, 0).sendToTarget();
				if(D) Log.d(TAG, "SERIAL LISTEN");

				socket = serverSocket.accept();

				mHandler.obtainMessage(BluetoothChat.MESSAGE_SERIAL_ACCEPT, 0, 0).sendToTarget();
				
				if(D) Log.d(TAG, "SERIAL ACCEPT");

				is = socket.getInputStream();
				os = socket.getOutputStream();

				whileFlag = true;

				try {
					while (whileFlag) {
						byte[] lenBytes = new byte[BUFFER_SIZE];
						int tempData = is.read(lenBytes, 0, BUFFER_SIZE);
						String str = new String(lenBytes);

						if(D) Log.d(TAG, tempData+"SERIAL MESSAGE READ--> " + str);
						
						
						Message msg = null;

						if (lenBytes[0] == 0 && lenBytes[1] == 0 && lenBytes[2] == 0 && lenBytes[3] == 0
								&& lenBytes[4] == 0 && lenBytes[5] == 0) {
							if(D) Log.d(TAG + "1", "lenBytes[0] == 0 && lenBytes[1] == 0 && lenBytes[2] == 0");
							whileFlag = false;
							break;
						}

						switch (lenBytes[0]) {
						case 3: // CLIP_SAVE
							
							do{
								
								if(lenBytes[0] != 0) {
									msg = Message.obtain();
									msg.obj = str;
									msg.what = BluetoothChat.MESSAGE_SERIAL_CLIPBOARD;
									mHandler.sendMessage(msg);
								}
								
								tempData = is.read(lenBytes, 0, BUFFER_SIZE);
								str = new String(lenBytes);
								//Log.d(TAG, "tempData->"+tempData);
							} while( tempData == 128 );

							if(D) Log.d(TAG, "CLIP SAVE");
							break;

						case 2: // DEVICE NAME
							str = str.substring(2, ((int) lenBytes[1]) + 2);

							msg = Message.obtain();
							msg.obj = str;
							msg.what = BluetoothChat.MESSAGE_SERIAL_DEVICE;
							mHandler.sendMessage(msg);

							if(D) Log.d(TAG, "DEVICE NAME: " + (str.length() + 2) + " " + str);
							break;

						case 100: // FORCE KILL
							whileFlag = false;
							if(D) Log.d(TAG, "FORCE KILL : 100");
							break;
						default: // READ MESSAGE
							mVO = new MessageVO(lenBytes);
							msg = Message.obtain();
							msg.what = BluetoothChat.MESSAGE_SERIAL_READ;
							msg.obj = mVO;
							mHandler.sendMessage(msg);

							if(D) Log.d(TAG, "MESSAGE READ");

						}
					}

					if(D) Log.d(TAG, "SERIAL SOCKET is DISCONNECTED");

					quitSocket();

				} catch (IOException e1) {
					// TODO Auto-generated catch block
					e1.printStackTrace();

					quitSocket();
				}
			}
		} catch (IOException e) {
			// TODO Auto-generated catch block
			if(D) Log.d(TAG, "serverSocket is DISCONNECTED");
			e.printStackTrace();
			
			quitServerSocket();
			
		}
		mHandler.obtainMessage(BluetoothChat.MESSAGE_SERIAL_DISCON, 0, 0).sendToTarget();

		quitServerSocket();
		
		if(D) Log.d(TAG, "SocketThread is QUIT");
		((Activity) context).finish();
	}

	public void quitSocket() {
		try {
			if (socket != null) {
				socket.close();
				socket = null;
				if(D) Log.d(TAG, "socket.close()");
			}

			if (is != null) {
				is.close();
				is = null;
				if(D) Log.d(TAG, "is.close()");
			}

			if (os != null) {
				os.close();
				os = null;
				if(D) Log.d(TAG, "os.close()");
			}

		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	byte[] bytes = new byte[BUFFER_SIZE];
	public void quitServerSocket() {
		try {
			if (serverSocket != null) {
				serverSocket.close();
				serverSocket = null;
				if(D) Log.d(TAG, "serverSocket.close()");
			}
			if (is != null) {
				is.close();
				is = null;
				if(D) Log.d(TAG, "is.close()");
			}

			if (os != null) {
				os.close();
				os = null;
				if(D) Log.d(TAG, "os.close()");
			}
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	public void write(byte[] lenBytes) {
		try {
			if(os != null)
				os.write(lenBytes);
			else
				Log.d(TAG, "Serial os is gone");
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

}