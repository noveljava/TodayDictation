package com.example.helloworld;

import java.io.BufferedOutputStream;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileDescriptor;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLConnection;
import java.util.Random;

import org.json.JSONArray;
import org.json.JSONObject;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.app.TabActivity;
import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import android.content.Intent;
import android.media.MediaPlayer;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.text.Html;
import android.text.TextUtils;
import android.text.method.ScrollingMovementMethod;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnLongClickListener;
import android.view.View.OnTouchListener;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.SeekBar;
import android.widget.SlidingDrawer;
import android.widget.TabHost;
import android.widget.TabHost.OnTabChangeListener;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.ToggleButton;

public class MainContent extends TabActivity implements OnTabChangeListener{
	private MediaPlayer mMediaPlayer = null;
	private GetContentsBackGround back = null;
	private SeekBar mp3Pb = null;
	private EditText result;
	TabHost tabs;
	private final Handler handler = new Handler();
	
	MainContent context;
	
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Intent intent = getIntent();
        String title = intent.getExtras().getString("title");
        int count = Integer.parseInt(intent.getExtras().getString("count"));
//        
//        String content = null;
//        String mp3url = null;
        
        mMediaPlayer = new MediaPlayer();
        setContentView(R.layout.main);
        
        tabs = getTabHost();
        tabs.addTab(tabs.newTabSpec("tabs1").setIndicator("본문").setContent(R.id.view1));
        tabs.addTab(tabs.newTabSpec("tabs2").setIndicator("단어장").setContent(R.id.wordView));
        
        back = new GetContentsBackGround(this,title,mMediaPlayer, count, tabs);
        back.execute("http://203.255.3.248/test2.php?title=");
        
        mp3Pb = (SeekBar)findViewById(R.id.SeekBar01);
        //Button imgBtn = (Button)findViewById(R.id.btnImgP_PlayAndPause);
        
        final ToggleButton imgBtn = (ToggleButton)findViewById(R.id.toggleButton01);
        
        imgBtn.setOnClickListener(new View.OnClickListener() {
			public void onClick(View v) {
				try{
					if(mMediaPlayer.isPlaying()){
			    		mMediaPlayer.pause();
			    		imgBtn.setBackgroundDrawable(getResources().getDrawable(R.drawable.play_button));
			    	}else{
			    		mMediaPlayer.start();
			            startPlayProgressUpdater();
			            imgBtn.setBackgroundDrawable(getResources().getDrawable(R.drawable.pause_button));
			    	}
				}catch(Exception e){
					System.out.println("Error !!! FUCK !!!");
				}
			}
        });
        
        ImageButton stopButton = (ImageButton)findViewById(R.id.btnImgP_Stop);
    	ImageButton preButton = (ImageButton)findViewById(R.id.btnImgP_Pre);
    	ImageButton postButton = (ImageButton)findViewById(R.id.btnImgP_Post);
    	
    	stopButton.setOnClickListener(new View.OnClickListener() {
			public void onClick(View v) {
				// TODO Auto-generated method stub
				//mMediaPlayer.stop();
				imgBtn.setBackgroundDrawable(getResources().getDrawable(R.drawable.play_button));
				mMediaPlayer.pause();
				mMediaPlayer.seekTo(0);
				mp3Pb.setProgress(0);
				System.out.println(mMediaPlayer.getDuration());
			}
		});
    	
    	preButton.setOnClickListener(new View.OnClickListener() {
			public void onClick(View v) {
				if(mMediaPlayer.getCurrentPosition() >=10000 )
					if(mMediaPlayer.isPlaying()){
						mMediaPlayer.pause();
						mMediaPlayer.seekTo(mMediaPlayer.getCurrentPosition()-10000);
						mp3Pb.setProgress(mp3Pb.getProgress()-10000);
						mMediaPlayer.start();
					}else{
						mMediaPlayer.seekTo(mMediaPlayer.getCurrentPosition()-10000);
						mp3Pb.setProgress(mp3Pb.getProgress()-10000);
					}
			}
		});
    	
    	postButton.setOnClickListener(new View.OnClickListener() {
			public void onClick(View v) {
				if(mMediaPlayer.getCurrentPosition()+10000 <= mMediaPlayer.getDuration() )
					if(mMediaPlayer.isPlaying()){
						mMediaPlayer.pause();
						mMediaPlayer.seekTo(mMediaPlayer.getCurrentPosition()+10000);
						mp3Pb.setProgress(mp3Pb.getProgress()+10000);
						mMediaPlayer.start();
					}else{
						mMediaPlayer.seekTo(mMediaPlayer.getCurrentPosition()+10000);
						mp3Pb.setProgress(mp3Pb.getProgress()+10000);
					}
			}
		});
    	
    	LayoutInflater inflater = (LayoutInflater)getSystemService(LAYOUT_INFLATER_SERVICE);
    	View layout = inflater.inflate(R.layout.findword, (ViewGroup)findViewById(R.id.findWord_layout));
    	
    	final AlertDialog.Builder builder = new AlertDialog.Builder(this);
    	builder.setView(layout);
    	
    	final AlertDialog alertDialog;
    	alertDialog = builder.create();
    	
		final Button okBtn = (Button)layout.findViewById(R.id.findInputOk);
		final Button cancleBtn = (Button)layout.findViewById(R.id.findInputCancle);
    	final EditText input = (EditText)layout.findViewById(R.id.editTextDialogUserFindWordInput);
    	
    	input.setPrivateImeOptions("defaultInputmode=english;");
    	Button findWordBtn = (Button)findViewById(R.id.findWordBtn);
        findWordBtn.setOnClickListener(new View.OnClickListener() {
			public void onClick(View v) {
				// TODO Auto-generated method stub
					mMediaPlayer.pause();
					alertDialog.show();
			}
		});
        
    	cancleBtn.setOnClickListener(new View.OnClickListener() {
			public void onClick(View v) {
				// TODO Auto-generated method stub
				input.setText("");
				alertDialog.dismiss();
				mMediaPlayer.start();
			}
		});
    	
    	context = this;
    	okBtn.setOnClickListener(new View.OnClickListener() {
			public void onClick(View v) {
				// TODO Auto-generated method stub
				if(!TextUtils.isEmpty(input.getText())){
					String word = input.getText().toString();
					final FindText findCs = new FindText(word.trim(), context);
					final String mean = findCs.Find().replace('>', '\n');
					//mean = mean.replace('>', '\n');

					final AlertDialog ad = new AlertDialog.Builder(context).create();
					ad.setTitle("단어");
					ad.setMessage(mean);

					ad.setButton("단어장 등록", new DialogInterface.OnClickListener() {
						public void onClick(DialogInterface dialog, int which) {
							// TODO Auto-generated method stub
							findCs.SaveText(mean);
							tabs.setCurrentTab(1);
							
						}
					});
					
					ad.setButton2("취소", new DialogInterface.OnClickListener() {
						public void onClick(DialogInterface dialog, int which) {
							// TODO Auto-generated method stub
							mMediaPlayer.start();
						}
					});
					input.setText("");
					alertDialog.dismiss();
					
					ad.show();
					
				}else{
					Toast.makeText(getApplicationContext(), "단어를 입력하지 않았습니다.", Toast.LENGTH_SHORT).show();
					mMediaPlayer.start();
					alertDialog.dismiss();
				}
			}
		});
        
    	tabs.setOnTabChangedListener(this);
    }
        
    @Override
	public void onPause(){	// When Home button click, this function Called.
    	mMediaPlayer.pause();
    	super.onPause();
    };
    
    @Override
	public boolean onKeyDown(int KeyCode, KeyEvent event)
    {
     
	    if( event.getAction() == KeyEvent.ACTION_DOWN ){
		    if( KeyCode == KeyEvent.KEYCODE_BACK){
		    	mMediaPlayer.stop();
		    	back.cancel(true);
		    }
	    }
	    return super.onKeyDown( KeyCode, event );
    }
    public void startPlayProgressUpdater(){
    	mp3Pb.setProgress(mMediaPlayer.getCurrentPosition());
    	
    	if(mMediaPlayer.isPlaying()){
    		Runnable notification = new Runnable() {
				
				public void run() {
					// TODO Auto-generated method stub
					startPlayProgressUpdater();
				}
			};
			
			handler.postDelayed(notification, 1000);
    	}else{
    		mMediaPlayer.pause();
    	}
    }
    
    public void setMp3Pb(){
    	mp3Pb.setMax(mMediaPlayer.getDuration());
    	mp3Pb.setOnTouchListener(new OnTouchListener() {
			
			public boolean onTouch(View v, MotionEvent event) {
				// TODO Auto-generated method stub
				seekChange(v);
				return false;
			}
		});
    }
    
    public void seekChange(View v){
    	if(mMediaPlayer.isPlaying()){
    		SeekBar pb = (SeekBar)v;
    		mMediaPlayer.seekTo(pb.getProgress());
    	}
    }
    
    public void onTabChanged(String tabId){
    	if(tabId.equals("tabs2")){
    		mMediaPlayer.pause();
    		TextView tx = (TextView)findViewById(R.id.wordTextView);
    		
    		String path = android.os.Environment.getExternalStorageDirectory().getAbsolutePath() + "/todayWord/wordFolder/wordText.txt";
    		String word = "";
    		try{
    			BufferedReader in = new BufferedReader(new FileReader(path));
    			String s;
    			
    			while((s = in.readLine()) != null){
    				word += s.replace('>', ':') + "\n";
    			}
    		}catch(Exception e){
    			System.out.println(e);
    		}
    		
    		tx.setText(word);
    	}else{
    		mMediaPlayer.start();
    	}
    	
    }
}


class GetContentsBackGround extends AsyncTask<String, Integer, String> {
	private MainContent longOperationContext = null;
	private String title = null;
	private ProgressDialog progress = null;
	private MediaPlayer mMediaPlayer = null;
	private int file_length = 0;
	private int clearWord = 0;
	private TabHost tabs = null;
	private String correctContent;
	
	public GetContentsBackGround(MainContent context,String title, MediaPlayer mPlayer, int count, TabHost tabs) {
        longOperationContext = context;
        this.title = title;
        this.mMediaPlayer = mPlayer;
        this.clearWord = count;
        this.tabs = tabs;
    }
	
	protected String questionContent(String content){
		String array[] = content.split(" ");
		String correct[] = content.split(" ");
		
		Random randomGenerator = new Random();
		
		int count=0;
		boolean changeOk = false;
		
		while(count < clearWord){
			int num = randomGenerator.nextInt(array.length);
			char[] chArray = array[num].toCharArray();
			
			if(chArray[0] == '_' || chArray[0] == ' ')
				break;
			else{
				for(int i = 0; i < chArray.length; i++){
					if(chArray[i] != '\'' || chArray[i] != '\"')
						chArray[i] = '_';
				}
				
				correct[num] = "<font color=red>" + correct[num] + "</font>";
				changeOk = true;
			}
			
			if(changeOk){
				String temp = String.copyValueOf(chArray, 0, chArray.length);
				temp = "<font color=red>" + temp + "</font>";
				array[num] = temp;
				count++;
			}
		}
		
		content = "";
		correctContent = "";
		
		for(int i=0; i<array.length; i++){
			array[i] = array[i].replace("\n", "<br>");
			content += array[i] + " ";
			
			correctContent += correct[i].replace("\n", "<br>") + " ";
		}
		return content;
	}

	@Override
	protected void onPostExecute(String content){
        final TextView contentView = (TextView)longOperationContext.findViewById(R.id.content);
        int height = 0;
        
        View temp = longOperationContext.findViewById(R.id.view1);
        height = (int)(temp.getHeight() * 0.7);
        
        content = questionContent(content);
        contentView.setMovementMethod(new ScrollingMovementMethod());
        contentView.setText(Html.fromHtml(content));
        contentView.setHeight(height);
        
//        contentView.setOnLongClickListener(new OnLongClickListener() {
//			public boolean onLongClick(View v) {
//				mMediaPlayer.pause();
//				AlertDialog.Builder dial = new AlertDialog.Builder(longOperationContext);
//				dial.setTitle("사전 검색");
//			         
//				final CharSequence[] items ={"사전 검색"};
//			    
//				dial.setItems(items, new OnClickListener() {
//					public void onClick(DialogInterface dialog, int which) {
//						// TODO Auto-generated method stub
//						
//						String m = contentView.getText().toString().substring(0, contentView.getSelectionEnd());
//						String[] temp = m.split(" ");
//						
//						final FindText findCs = new FindText(temp[temp.length-1], longOperationContext);
//						
//						final String mean = findCs.Find().replace('>', '\n');
//						//mean = mean.replace('>', '\n');
//						
//						final AlertDialog ad = new AlertDialog.Builder(longOperationContext).create();
//						
//						ad.setTitle("단어");
//						//ad.setMessage(mean.substring(0, mean.indexOf('>')) + "\n" + mean.substring(mean.indexOf('>')+1, mean.length()-1));
//						ad.setMessage(mean);
//
//						ad.setButton("단어장 등록", new DialogInterface.OnClickListener() {
//							
//							public void onClick(DialogInterface dialog, int which) {
//								// TODO Auto-generated method stub
//								findCs.SaveText(mean);
//								tabs.setCurrentTab(1);
//								
//							}
//						});
//						
//						ad.setButton2("취소", new DialogInterface.OnClickListener() {
//							
//							public void onClick(DialogInterface dialog, int which) {
//								// TODO Auto-generated method stub
//								mMediaPlayer.start();
//							}
//						});
//						ad.show();
//						
//						//tabs.setCurrentTab(1);
//					}
//				});
//			        
//		        dial.show();
//		    	return false;
//			}
//		});

        progress.dismiss();
        
        String absolutePath = android.os.Environment.getExternalStorageDirectory().getAbsolutePath();
        String path = absolutePath + "/todayWord/" + title;
        //String path = "/sdcard/todayWord/" + title;			// 수정1.
        try{
        	FileInputStream fis = new FileInputStream(path);
        	FileDescriptor fd = fis.getFD();
	        mMediaPlayer.setDataSource(fd);
	        mMediaPlayer.prepare();
	        mMediaPlayer.start();
        }catch(Exception e){
        	System.out.println(e);
        }
        
        longOperationContext.setMp3Pb();
        longOperationContext.startPlayProgressUpdater();
        
        Button correctBtn = (Button)longOperationContext.findViewById(R.id.showAnswer);
        correctBtn.setOnClickListener(new View.OnClickListener() {
			
			public void onClick(View v) {
				// TODO Auto-generated method stub
				contentView.setText(Html.fromHtml(correctContent));
			}
		});
    }
	
	@Override
	protected void onCancelled(){
		progress.dismiss();
	}
	
	@Override
	protected void onPreExecute(){
		progress = new ProgressDialog(longOperationContext);
		progress.setProgressStyle(ProgressDialog.STYLE_HORIZONTAL);
		progress.setTitle("Mp3 Download");
		progress.setMessage("Wait...");
		progress.setCancelable(false);
		progress.setProgress(0);
		progress.setButton("Cancle", new DialogInterface.OnClickListener() {
			public void onClick(DialogInterface dialog, int whichButton) {
				cancel(true);
			}
		});
		
		progress.show();
	}
	
	@Override
	protected String doInBackground(String ... addr){
		String content = null;
		String mp3Url = null;
		
		StringBuilder jsonHtml = new StringBuilder();
    	try{
    		URL url = new URL(addr[0]+title);
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
    	
        try{
        	JSONArray ja = new JSONArray(jsonHtml.toString());
        	
        	for(int i=0; i<ja.length(); i++){
        		JSONObject Jobject = ja.getJSONObject(i);
        		
        		content = Jobject.getString("content").substring(1, Jobject.getString("content").length()-1);
        		mp3Url = Jobject.getString("mp3url").substring(1, Jobject.getString("mp3url").length()-1);
        	}
        }catch(Exception e){
        	System.out.println("error : not Parsing");
        }

        String absolutePath = android.os.Environment.getExternalStorageDirectory().getAbsolutePath();
        //String absolutePath = "/sdcard";		// 수정1.
        File path = new File(absolutePath, "todayWord");
	     if(!path.isDirectory()) {
	    	 System.out.println("폴더가 생성됨.");
	    	 System.out.println(absolutePath);
	             path.mkdirs();
	      }
	    
	     //int file_size = 0;
	     try{
		     URL mp3urlOb = new URL(mp3Url);
		     URLConnection urlConnection = mp3urlOb.openConnection();
		     urlConnection.connect();
		     file_length = urlConnection.getContentLength();
	     }catch(Exception e){
	    	 System.out.println("File Size error");	// UI 부분 에러도 출력 될 수 있도록 구현.
	     }
	     
	     progress.setMax(file_length);
	     
	     try {
	    	 title = title.replace("[*:/?*<>]{1}", "") + ".mp3";
	    	 
	    	 
	    	 InputStream inputStream = new URL(mp3Url).openStream();
            
	    	 File file = new File(path, title);
			
	    	 if(!file.isFile()){
		    	 OutputStream out = new FileOutputStream(file);
		    	 BufferedOutputStream bos = new BufferedOutputStream(out);
		    	 int bufferLength = 0;
		    	 int currentLength = 0;
		    	 int readByte = 0;
		    	 
		    	 byte[] buffer = new byte[1024];
		    	 
		    	 while((currentLength = inputStream.read(buffer)) > 0){
		    		 readByte += currentLength;
		    		 bos.write(buffer, 0, currentLength);
		    		 
		    		 publishProgress(readByte);
		    		 Thread.sleep(1);
		    	 }
		    	 out.close();
	    	 }
	     } catch(Exception e){
	    	 System.out.println("Fail !!!" +  e);
	     }
        
		return content;
	}
	
	@Override
	protected void onProgressUpdate(Integer...value){
		progress.setProgress(value[0]);
	}
}
