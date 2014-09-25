package org.probit.marionette;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.Window;

public class LoadingActivity extends Activity {

	@Override
	public void onCreate(Bundle savedInstanceState) {
		requestWindowFeature(Window.FEATURE_NO_TITLE);
	    super.onCreate(savedInstanceState);
		setContentView(R.layout.loading);
		
		Log.i("LoadingActivity", "LoadingActivity onCreate");
		
		Handler handler = new Handler(){
			public void handleMessage(Message msg){

				Intent intent = new Intent(getApplicationContext(), MainActivity.class);
				startActivity(intent);
				
				finish();
			}
		};
		handler.sendEmptyMessageDelayed(0,2000);
	
	    // TODO Auto-generated method stub
	}

}
