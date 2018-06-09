package com.example.helloworld;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.ArrayList;

import org.json.JSONArray;
import org.json.JSONObject;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.Intent;
import android.graphics.Color;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.SystemClock;
import android.text.TextUtils;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

public class CopyOfMainActivity extends Activity {
	AlertDialog.Builder builder;
	AlertDialog alertDialog;
	
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.newactivity);
        
        ArrayList<String> listView1 = new ArrayList<String>();
        
        new BackgroundTask(this, listView1).execute("http://203.255.3.248/test.php");
   
        ListView listViewExample = (ListView)findViewById(R.id.listView1);

        listViewExample.setOnItemClickListener(itemClickListenerOfLanguageList);
    }
    
    
    private OnItemClickListener itemClickListenerOfLanguageList = new OnItemClickListener()
    {
        public void onItemClick(AdapterView<?> adapterView, View clickedView, int pos, long id)
        {
        	TextView tv = (TextView)clickedView;
            final String title = tv.getText().toString();
            
        	final Context mContext = CopyOfMainActivity.this;
        	LayoutInflater inflater = (LayoutInflater)mContext.getSystemService(LAYOUT_INFLATER_SERVICE);
        	
        	View layout = inflater.inflate(R.layout.prompts, (ViewGroup)findViewById(R.id.layout_root));
        	
        	builder = new AlertDialog.Builder(mContext);
        	builder.setView(layout);
        	
        	final Button okBtn = (Button)layout.findViewById(R.id.inputOk);
        	final EditText input = (EditText)layout.findViewById(R.id.editTextDialogUserInput);
        	
        	okBtn.setOnClickListener(new OnClickListener() {
    			
    			public void onClick(View v) {
    				// TODO Auto-generated method stub
    				if(!TextUtils.isEmpty(input.getText())){
	    				Intent myIntent = new Intent(getApplicationContext(), MainContent.class);
	    	            myIntent.putExtra("title", title);
	    				myIntent.putExtra("count", input.getText().toString());

	    				alertDialog.dismiss();
	    				SystemClock.sleep(1);
	    				
	    	            startActivity(myIntent);
    				}else{
    					Toast.makeText(mContext, "숫자를 입력하지 않았습니다.", Toast.LENGTH_SHORT).show();
    				}
    			}
    		});
        	
        	alertDialog = builder.create();
        	alertDialog.show();	
        }
    };

}

class CustomAdapter extends ArrayAdapter<String>{
	ArrayList<String> info;
	Context mContext;
	int count;
	public CustomAdapter(Context context, int resourceId, ArrayList<String> items) {
		// TODO Auto-generated constructor stub
		super(context, resourceId);
		this.mContext = context;
		this.info = items;
		this.count = items.size();
		System.out.println("생성자 호출");
	}
	
	public int getCount(){
		return count;
	}
	public View getView(int position, View convertView, ViewGroup parent){
		System.out.println(".... No call?");
		TextView view = (TextView)convertView;
		
		if(view == null){
			view = new TextView(getContext());
		}
		
		view.setText(info.get(position));
		view.setTextSize(25);
		view.setHeight(50);
		view.setGravity(Gravity.CENTER_VERTICAL);
		view.setTextColor(Color.WHITE);
		return view;
	}
}

class BackgroundTask extends AsyncTask<String, String, String>{
	private CopyOfMainActivity longOperationContext = null;
	private ArrayList<String> listView1 = null;
	
	public BackgroundTask(CopyOfMainActivity context, ArrayList<String> list) {
        longOperationContext = context;
        listView1 = list;
    }
	
	protected void onPostExecute(String json){
		//ArrayList<String> listView1 = new ArrayList<String>();
        try{
        	JSONArray ja = new JSONArray(json);
        	
        	for(int i=0; i<ja.length(); i++){
        		JSONObject title = ja.getJSONObject(i);
        		listView1.add(title.getString("title").substring(1, title.getString("title").length()-1));
        		
        	}
        }catch(Exception e){
        	System.out.println("error : not Parsing");
        }
        
        CustomAdapter adapter = new CustomAdapter(longOperationContext, android.R.layout.simple_list_item_1, listView1);
        
        ListView listView = (ListView)longOperationContext.findViewById(R.id.listView1);
        listView.setAdapter(adapter);
	}
	
	protected String doInBackground(String ... addr){
		StringBuilder jsonHtml = new StringBuilder();
    	try{
    		URL url = new URL(addr[0]);
    		HttpURLConnection conn = (HttpURLConnection)url.openConnection();
    		
    		if(conn != null){
    			conn.setConnectTimeout(10000);
    			conn.setUseCaches(false);
    			
    			int resCode = conn.getResponseCode();
    			if(resCode == HttpURLConnection.HTTP_OK){
    				
    				BufferedReader br = new BufferedReader(new InputStreamReader(conn.getInputStream(), "EUC-KR"));
    				for(;;){
    					String line = br.readLine();
    					if(line == null) break;
    					
    					jsonHtml.append(line + "\n");
    				}
    				
    				br.close();
    			}
    			
    			conn.disconnect();
    		}
		}catch(Exception e){
			System.out.println("error : get Html");
		}
    	
    	return jsonHtml.toString();
    }
}
