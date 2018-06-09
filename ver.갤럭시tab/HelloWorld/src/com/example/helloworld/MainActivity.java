package com.example.helloworld;

import android.app.Activity;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.view.Menu;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.Toast;

public class MainActivity extends Activity {

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //setContentView(R.layout.activity_main);
        setContentView(R.layout.activity_main);
        
        ImageButton startBtn = (ImageButton)findViewById(R.id.testButton);
        startBtn.setOnClickListener(new OnClickListener() {
			
			public void onClick(View v) {
				// TODO Auto-generated method stub
				Intent myIntent = new Intent(getApplicationContext(), CopyOfMainActivity.class);
				startActivity(myIntent);
			}
		});
        
       /* Button startBtn2 = (Button)findViewById(R.id.testButton2);
        startBtn2.setOnClickListener(new OnClickListener() {
			
			public void onClick(View v) {
				// TODO Auto-generated method stub
				Intent myIntent = new Intent(Intent.ACTION_VIEW, Uri.parse("http://m.naver.com"));
				startActivity(myIntent);
				
			}
		});*/
        
        /*Button startBtn3 = (Button)findViewById(R.id.testButton3);
        startBtn3.setOnClickListener(new OnClickListener() {
			
			public void onClick(View v) {
				// TODO Auto-generated method stub
				Intent myIntent = new Intent(Intent.ACTION_VIEW, Uri.parse("tel:010-7788-1234"));
				startActivity(myIntent);
				
			}
		});*/
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.activity_main, menu);
        return true;
    }

    
}
