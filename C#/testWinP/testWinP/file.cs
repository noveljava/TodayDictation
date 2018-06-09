using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Net;

using fileObjectClass;
using System.Runtime.InteropServices;   // dll import를 위한 using.

namespace userFileClass
{
    public class userFile
    {
        fileObject fileOb = new fileObject();
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);
        string htmlStr;
        bool checkingFlag;

        private void createFile(string path, string title, string outline)
        {
            FileStream fs = new FileStream(path + title, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);

            sw.Write(outline);
            
            sw.Close();
            fs.Close();
        }

        private string deleteElement(string word)
        {
            /************************************************************************
             * <p> 를 제외한 < 는 나의 관심밖이고 출력할 필요도 없다고 간주.        *
             * <div>, <h 계열, <a>, <script> .                                      *
             * 다 커플이라, </xx> 가 존재함. 그 안에 부분도 다 삭제.                *
             ************************************************************************/
            int startIndex = 0;
            int endIndex = 0;
            int maxWord = 5;
            string[] arr = new string[maxWord];

            arr[0] = "<h";
            arr[1] = "<a";
            arr[2] = "<script";
            arr[3] = "<iframe";
            arr[4] = "<object";

            foreach (string deleteWord in arr)
            {
                while (word.IndexOf(deleteWord) >= 0)
                {
                    startIndex = word.IndexOf(deleteWord);
                    endIndex = word.IndexOf("</" + deleteWord.Substring(1, deleteWord.Length - 1)) + deleteWord.Length + 3;

                    word = word.Substring(0, startIndex) + word.Substring(endIndex, word.Length - endIndex);
                }
            }
            
            while (word.IndexOf("<span") >= 0)
            {
                int start = word.IndexOf("<span");
                int sumStart = 0;
                int end = word.IndexOf("</span>");

                string spanTemp = "";

                if (end > (start + 6))
                {
                    spanTemp = word.Substring(start + 6, end - (start + 6));

                    while (spanTemp.IndexOf("<span") >= 0)
                    {
                        sumStart += spanTemp.IndexOf("<span") + 6;
                        spanTemp = spanTemp.Substring(spanTemp.IndexOf("<span") + 6, spanTemp.Length - (spanTemp.IndexOf("<span") + 6));
                    }
                }

                if (word.Substring(end + 7, 1).Equals("\n"))
                    end++;

                word = word.Substring(0, start + sumStart) + word.Substring(end+7, word.Length - (end + 7));
            }

            while (word.IndexOf("<em>") >= 0)
            {
                int point = word.IndexOf("<em>");
                word = word.Substring(0, point) + word.Substring(point + 4, word.Length - (point + 4));

                point = word.IndexOf("</em>");
                word = word.Substring(0, point) + word.Substring(point + 5, word.Length - (point + 5));
            }

            if (word.IndexOf("<br") >= 0)                       // 임시 추가부분, 많은 테스트 안 거쳐봄. 특정 스크립트에서 파싱 도중 
            {
                word = word.Substring(0, word.IndexOf("<br"));  // <br  />br   /> 이 남는 것을 확인. 이 부분을 추가적으로 삭제.
            }

            string temp = word;
            int sumI = 0;
            while (temp.IndexOf("\n") >= 0)
            {
                int i = temp.IndexOf("\n");
                sumI += (i + 1);

                if (i + 1 < temp.Length && i + 2 < temp.Length)
                {
                    if (temp.Substring(i + 1, 1).Equals("\n") && temp.Substring(i + 2, 1).Equals("\n"))
                    {
                        word = word.Substring(0, sumI);
                        break;   
                    }
                    else
                    {
                        temp = temp.Substring(i + 1, temp.Length - (i + 1));
                    }
                }
                else
                {
                    break;
                }
            }

            return word;
        }

        private string parsingNewsScript(string webNews)
        {
            // webNews 는 <p> 부터 끝까지 삭제를 반복해서 자기 자신에게 저장
            // return 할 news 는 <p> ~ </p> 까지 넣고, </p> 를 엔터로 치환.
            string news = "";
            
            //createFile("D:\\testing\\", "originalDelete.txt", webNews);
            while (webNews.IndexOf("<p>") >= 0 && webNews.IndexOf("</p>") >= 0)      
            {
                int pointP = webNews.IndexOf("<p>");
                int pointSP = webNews.IndexOf("</p>");
                
                news += webNews.Substring(pointP + 3, pointSP - (pointP + 3)) + "\n";   // 에러 나옴. Medical Spies Keep Watch on Leaders (0보다 작을 수 없음)
                
                webNews = webNews.Substring(pointSP + 1, webNews.Length - (pointSP + 1));

            }

            //createFile("D:\\testing\\", "beforeDelete.txt", news);
            news = deleteElement(news);
            //createFile("D:\\testing\\", "afterDelete.txt", news);
            return news;
        }

        public fileObject getFileDate()
        {
            return fileOb;
        }

        private string checkingFileName(string fileName)
        {
            /* : \ / ? * " < > | 가 사용시 파일 저장 불가. */
            //Console.WriteLine("checkingFileName : " + fileName);
            //fileName = "Scientists Continue Their Search for Better Treatments for Multiple Sclerosis ? and a Cure";
            string localFileName;

            localFileName = System.Text.RegularExpressions.Regex.Replace(fileName, @"[*:/?*<>]{1}", String.Empty);
            
            return localFileName;
        }
        
        public void checkFolder()
        {
            string currentPath = Environment.CurrentDirectory + "\\save";
            string fullYear;
            DateTime dt = DateTime.Now;
            fullYear = DateTime.Now.ToString("yyyy-MM-dd");

            try
            {
                if (!System.IO.Directory.Exists(currentPath))
                    System.IO.Directory.CreateDirectory(currentPath);


                currentPath += "\\" + fullYear.Substring(0, 4); // 2011 년도를 짜른다.
                if (!System.IO.Directory.Exists(currentPath))
                    System.IO.Directory.CreateDirectory(currentPath);

                currentPath += "\\" + fullYear.Substring(5, 2); // 월을 자른다.
                if (!System.IO.Directory.Exists(currentPath))
                    System.IO.Directory.CreateDirectory(currentPath);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private bool checkList(string title)
        {
            string currentPath = Environment.CurrentDirectory + "\\save";
            string fileName = "\\readList.txt";
            FileStream fs = new FileStream(currentPath + fileName, FileMode.Open);
            StreamReader sw = new StreamReader(fs, System.Text.Encoding.Default);

            string readLine;
            bool run = true;
            bool exist = false;
            do
            {
                readLine = sw.ReadLine();

                if (readLine != null)
                {
                    Console.WriteLine(readLine.IndexOf(title));
                    if (readLine.IndexOf(title) > 0)
                    {
                        fileOb.setTilte(readLine.Substring(7, readLine.Length - 7));
                        
                        readLine = sw.ReadLine();

                        fileOb.setUrl(readLine.Substring(5, readLine.Length - 5));
                        readLine = sw.ReadLine();

                        fileOb.setYear(readLine.Substring(6, 4));
                        fileOb.setMonth(readLine.Substring(12, 2));

                        run = false;
                        exist = true;
                        break;
                    }
                }
            } while (run && readLine != null);

            sw.Close();
            fs.Close();
            return exist;
        }

        public void createReadList(string title, string url)
        {
            string currentPath = Environment.CurrentDirectory + "\\save";
            string fileName = "\\readList.txt";

            string fullYear;

            DateTime dt = DateTime.Now;
            fullYear = DateTime.Now.ToString("yyyy-MM-dd");

            try
            {
                if (System.IO.File.Exists(currentPath + fileName))
                {
                    checkingFlag = checkList(title);
                    if (!checkingFlag)
                    {   
                        FileStream fs = new FileStream(currentPath + fileName, FileMode.Append);
                        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);

                        sw.Write("title: " + title + "\r\n");
                        sw.Write("url: " + url + "\r\n");
                        sw.Write("year: " + fullYear.Substring(0, 4) + "년 " + fullYear.Substring(5, 2) + "\r\n");
                        
                        fileOb.setTilte(title);
                        fileOb.setUrl(url);
                        fileOb.setYear(fullYear.Substring(0, 4));
                        fileOb.setMonth(fullYear.Substring(5, 2));
                        
                        sw.Close();
                        fs.Close();
                    }
                }
                else
                {
                    System.IO.Directory.CreateDirectory(currentPath);
                    FileStream fs = new FileStream(currentPath + fileName, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);

                    sw.Write("title: " + title + "\r\n");
                    sw.Write("url: " + url + "\r\n");
                    sw.Write("year: " + fullYear.Substring(0, 4) + "년 " + fullYear.Substring(5, 2) + "\r\n");

                    fileOb.setTilte(title);
                    fileOb.setUrl(url);
                    fileOb.setYear(fullYear.Substring(0, 4));
                    fileOb.setMonth(fullYear.Substring(5, 2));

                    sw.Close();
                    fs.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void mp3DownLoad(string title, string mp3Url)
        {
            string currentPath = Environment.CurrentDirectory + "\\save";
            string fullYear;
            DateTime dt = DateTime.Now;
            fullYear = DateTime.Now.ToString("yyyy-MM-dd");
            currentPath += "\\" + fullYear.Substring(0, 4) + "\\" + fullYear.Substring(5, 2);

            checkFolder();
            try
            {
                if (!checkingFlag)
                {
                    /*
                    WebRequest request = WebRequest.Create(url);
                    request.Credentials = CredentialCache.DefaultCredentials;

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    htmlStr = reader.ReadToEnd();

                    string tempHtmlStr = htmlStr.Substring(htmlStr.IndexOf("return popMediaWindow"), 300);
                    string mp3Url = "";

                    if (tempHtmlStr.IndexOf("MP3") >= 0)
                        mp3Url = tempHtmlStr.Substring(tempHtmlStr.IndexOf("http://"), tempHtmlStr.IndexOf("\');") - tempHtmlStr.IndexOf("http://"));
                    else
                    {
                        
                        string substringHtml = htmlStr.Substring(htmlStr.IndexOf("return popMediaWindow")+300, htmlStr.Length - (htmlStr.IndexOf("return popMediaWindow") + 300));
                        
                        tempHtmlStr = substringHtml.Substring(substringHtml.IndexOf("return popMediaWindow"), 300);
                        

                        mp3Url = tempHtmlStr.Substring(tempHtmlStr.IndexOf("http://"), tempHtmlStr.IndexOf("\');") - tempHtmlStr.IndexOf("http://"));
                        Console.WriteLine(mp3Url);
                    }

                    // html Str 부분에서 내가 필요한 부분 parsing 필요.
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                     */
                    WebClient wc = new WebClient();
                    
                    //(다운로드할 경로, 저장할 경로 + 파일명.확장자);
                    title = checkingFileName(title);
                    Console.WriteLine(mp3Url);
                    wc.DownloadFile(mp3Url, currentPath + "\\" + title + ".mp3");
                    
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Mp3 다운 중 문제가 발생하였습니다.");
                MessageBox.Show(e.Message);
            }
        }

        public void mp3Start(string title)        
        {
            title = checkingFileName(fileOb.getTitle());
            
            string currentPath = Environment.CurrentDirectory + "\\save";
            string filePath = currentPath + "\\" + fileOb.getYear() + "\\" + fileOb.getMonth() + "\\" + title + ".mp3";
            /*
            mciSendString(@filePath, "", 0, 0); //경로에 MDI File이 존재하는 위치를 기술
            mciSendString("PLAY MDIFile", "", 0, 0); //열린 File을 재생
             */
            mciSendString("open \"" + filePath + "\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
            mciSendString("play MediaFile", null, 0, IntPtr.Zero);

        }

        public ulong getCalcuratorLength()
        {
            int i = 128;
            StringBuilder str = new StringBuilder(i);
            mciSendString("status MediaFile length", str, i, IntPtr.Zero);
            ulong lng = Convert.ToUInt64(str.ToString());
            double temp = (double)lng;
            temp /= 1000;
            temp = Math.Round(temp, 1);
            lng = (ulong)temp;
            return lng;
        }

        public string createScript(string title)
        {
            string news = "";
            title = checkingFileName(fileOb.getTitle());
            if (!checkingFlag)
            {
                string tempScript = htmlStr.Substring(htmlStr.IndexOf("articleBody"), htmlStr.Length - htmlStr.IndexOf("articleBody"));
                tempScript = tempScript.Substring(0, htmlStr.IndexOf("startclickprintexclude"));

                news = parsingNewsScript(tempScript);

                string currentPath = Environment.CurrentDirectory + "\\save";
                string fullYear;
                DateTime dt = DateTime.Now;
                fullYear = DateTime.Now.ToString("yyyy-MM-dd");
                currentPath += "\\" + fullYear.Substring(0, 4) + "\\" + fullYear.Substring(5, 2) + "\\";
                
                FileStream fs = new FileStream(currentPath + title + ".txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Unicode);

                sw.Write(news);

                sw.Close();
                fs.Close();

            }
            else
            {
                string currentPath = Environment.CurrentDirectory + "\\save";
                string fullYear;
                DateTime dt = DateTime.Now;
                fullYear = DateTime.Now.ToString("yyyy-MM-dd");
                currentPath += "\\" + fullYear.Substring(0, 4) + "\\" + fullYear.Substring(5, 2) + "\\" + title + ".txt";

                StreamReader SRead = new StreamReader(currentPath, System.Text.Encoding.Unicode);

                string line = string.Empty;
                while ((line = SRead.ReadLine()) != null)
                {
                    news += line + "\n";
                }

                SRead.Close();
            }

            return news;
        }

        public void createWordText(string word, string mean)
        {
            string path = Environment.CurrentDirectory + "\\wordText";
            string title = "\\wordText";

            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);

                FileStream fs = new FileStream(path + title + ".txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Unicode);

                sw.Write(word + ">" + mean + ">1\n");

                sw.Close();
                fs.Close();
            }
            else
            {
                FileStream fs = new FileStream(path + title + ".txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs, System.Text.Encoding.Unicode);
                string readLine = "";
                string tempStr = "";
                bool find = false;

                do
                {
                    tempStr = sr.ReadLine();
                    
                    if (tempStr != null && tempStr.IndexOf(word) >= 0)
                    {
                        string[] tempStrArr = tempStr.Split('>');
                        int count = Int32.Parse(tempStrArr[2]);
                        tempStrArr[2] = Convert.ToString(count + 1);
                        find = true;
                        tempStr = tempStrArr[0] + ">" + tempStrArr[1] + ">" + tempStrArr[2];
                    }

                    if(tempStr != null)
                        readLine += tempStr + "\n";
                    
                } while (tempStr != null);

                sr.Close();
                fs.Close();

                if (find)
                {
                    fs = new FileStream(path + title + ".txt", FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Unicode);

                    sw.Write(readLine);

                    sw.Close();
                    fs.Close();
                }
                else
                {
                    fs = new FileStream(path + title + ".txt", FileMode.Append);
                    StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Unicode);

                    sw.Write(word + ">" + mean + ">1\n");

                    sw.Close();
                    fs.Close();
                }
            }
        }

        public void createScore(string score, string title)
        {
            string pullPath = Environment.CurrentDirectory + "\\save\\score.txt";
            string fullYear;

            DateTime dt = DateTime.Now;
            fullYear = DateTime.Now.ToString("yyyy-MM-dd");

            if (!System.IO.Directory.Exists(pullPath))
            {
                FileStream fs = new FileStream(pullPath, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Unicode);

                sw.Write(dt + " : " + title + "의 score : " + score + "\n");
                sw.Close();
                fs.Close();
            }
            else
            {
                FileStream fs = new FileStream(pullPath, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Unicode);

                sw.Write(dt + " : " + title + "의 score : " + score + "\n");

                sw.Close();
                fs.Close();
            }
        }
    }
}
