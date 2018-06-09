package com.example.helloworld;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;

import android.content.res.Resources;

public class FindText {
	String findWord;
	MainContent main = null;
	
	FindText(String findWord, MainContent main){
		this.findWord = findWord.toLowerCase();
		this.main = main;
	}
	
	String Find(){
		String mean = "찾는 단어의 뜻이 없습니다.";
		
		BufferedReader in;
		Resources myResources = main.getResources();
		InputStream input = myResources.openRawResource(R.raw.atog);
		
		char fisrtWord = findWord.charAt(0);
		
		if(fisrtWord >= 's' && fisrtWord <= 'z'){
			input = myResources.openRawResource(R.raw.stoz);
			System.out.println("s to z");
		}else if(fisrtWord >= 'h' && fisrtWord <= 'r'){
			input = myResources.openRawResource(R.raw.htor);
			System.out.println("h to r");
		}
		
		try{
			in = new BufferedReader(new InputStreamReader(input, "UTF-8"));
			String strLine = null;
			String temp = null;
			String plusEWord = "";
			boolean verbCheck = false;
			
			if(findWord.charAt(findWord.length()-1) == 's'){	// 명사일 경우만 생각
				if(findWord.substring(findWord.length()-2, findWord.length()).equals("es")){
					
					String iThrfromEnd = findWord.substring(findWord.length()-3, findWord.length()-2); // 끝에서 세번쨰
					String iThrToFofromEnd = findWord.substring(findWord.length()-4, findWord.length()-2); // 끝에서 세번쨰
					// y -> ies, f -> ves
					if(findWord.substring(findWord.length()-3, findWord.length()).equals("ies")){
						findWord = findWord.substring(0, findWord.length()-3) + "y";
					}else if(findWord.substring(findWord.length()-3, findWord.length()).equals("ves")){
						findWord = findWord.substring(0, findWord.length()-3) + "f";
					}else if(iThrfromEnd.equals("s") || iThrfromEnd.equals("o") || iThrfromEnd.equals("x")){
						findWord = findWord.substring(0, findWord.length()-2);
					}else if(iThrToFofromEnd.equals("ch") || iThrToFofromEnd.equals("sh")){
						findWord = findWord.substring(0, findWord.length()-2);
					}
				}else{
					findWord = findWord.substring(0, findWord.length()-1);
				}
			}
			
			if(findWord.substring(findWord.length()-2, findWord.length()).equals("ed")){
				if(findWord.substring(findWord.length()-3, findWord.length()).equals("ied")){
					findWord = findWord.substring(0, findWord.length()-3) + "y";
				}else if(findWord.substring(findWord.length()-4, findWord.length()-3).equals(findWord.substring(findWord.length()-3, findWord.length()-2))){
					findWord = findWord.substring(0, findWord.length()-3);
				}else{
					verbCheck = true;
					findWord = findWord.substring(0, findWord.length()-2);
					plusEWord = findWord +"e";
				}
			}
			System.out.println(findWord);
			
			while( (strLine = in.readLine()) != null){
				if(strLine.length() == 0 || strLine.indexOf('>') == -1){
					continue;
				}
				temp = strLine.substring(0, strLine.indexOf('>'));
//				if(temp.substring(0, 1).equals(findWord.substring(0, 1)) && temp.indexOf(findWord) != -1){
//					System.out.println(strLine);
//					mean = strLine;
//					break;
//				}
				if(temp.equals(findWord)){
					mean = strLine;
					break;
				}else if(verbCheck){
					if(temp.equals(plusEWord)){
						mean = strLine;
						break;	
					}
				}
				
//				if(strLine.indexOf(findWord) != -1){
//					temp = strLine.substring(0, strLine.indexOf('>'));
//					
//					System.out.println(temp);
//					
//					if(temp.equals(findWord)){
//						mean = strLine;
//						break;
//					}
//				}
			
//				temp = strLine.substring(0, strLine.indexOf('>')-1);
//				System.out.println(temp);
//				if(temp.equals(findWord)){
//					mean = strLine;
//					break;
//				}
			}
		}catch(Exception e){
			System.out.println(e);
		}
			
		return mean;
	}

	void SaveText(String mean){
		String absolutePath = android.os.Environment.getExternalStorageDirectory().getAbsolutePath() + "/todayWord/";
        
        File path = new File(absolutePath, "wordFolder");
		if(!path.isDirectory()) {
			System.out.println("폴더가 생성됨.");
			path.mkdirs();
		}
	    
		try{
			FileOutputStream file = new FileOutputStream(path+"/wordText.txt", true);
			mean = mean.replace('\n', '>');
			mean += "\n";
			file.write(mean.getBytes());
			
			System.out.println("기록 되었습니다." + mean);
			file.close();
		}catch(Exception e){
			System.out.println(e);
		}
	}
}
