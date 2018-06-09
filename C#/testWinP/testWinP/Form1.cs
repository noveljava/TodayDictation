using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

using System.IO;
using System.Net;

using System.Text.RegularExpressions;
using System.Runtime.InteropServices;   // dll import를 위한 using.

using userFileClass;
using fileObjectClass;
using toolClass;
using InputBoxNamespace;

namespace testWinP
{
    public partial class Form1 : Form
    {
        Hashtable result = new Hashtable();
        tool toolClass = new tool();

        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);

        fileObject fileOb = new fileObject();
        ulong fileLength = 0;
        bool playStatus = false;    // false: 재생상태 X, true: 재생.
        int locationX = 0;
        int locationY = 0;
        string title = "";
        bool orderSet = false;

        private string toAscii(string title)
        {
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] buffer = enc.GetBytes(title);

            string ascTitle = "";
            foreach (byte byteBuffer in buffer)
                ascTitle += Convert.ToChar(byteBuffer);

            //Console.Write(title2.Equals(temp));
            return ascTitle;
        }

        private string parsingWord(String word, String findWord, String endWord)
        {
            int start = word.IndexOf(findWord);
            int end = 0;

            if (endWord.Equals(""))
                end = word.Length - start;
            else
                end = word.IndexOf(endWord) - start;

            if (start < 0)
                return word;
            else
                return word.Substring(start, end);
        }

        private string deleteString(String word)
        {
            //필요하지 않은 부분들을 전부 삭제 시킨다.
            string url = "";
            string text = "";
            result = new Hashtable();

            //string pattern = "<a href=\"(?<url>.*?)\".*?><?<text>.*?)</a>";
            string pattern = "<a href=\"(?<url>.*?)\">(?<text>.*?)</a>";
            Regex rg = new Regex(pattern , RegexOptions.IgnoreCase | RegexOptions.Singleline);
            
            for(Match m = rg.Match(word); m.Success; m=m.NextMatch())
            {
                // text가 태그로 시작되는건 저장하지 않는다.
                if(m.Groups["text"].Value[0] != '<')
                {
                    //url += m.Groups["url"].Value + "|";
                    text += m.Groups["text"].Value + "|";

                    result.Add(m.Groups["text"].Value, m.Groups["url"].Value);
                }
            }

            return text;
        }

        public void writeTitle(string word)
        {
            string[] title = word.Split('|');
            foreach (string tmp in title)
                todayNewsList.Items.Add(tmp);
        }

        public Form1()
        {
            InitializeComponent();
            locationX = 100;
            locationY = 100;
            this.Location = new Point(locationX, locationY);
        }

        private void newsUpdateBt_Click(object sender, EventArgs e)
        {
            /*
            WebRequest request = WebRequest.Create("http://www.voanews.com/learningenglish/home/world/");
            request.Credentials = CredentialCache.DefaultCredentials;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string htmlStr = reader.ReadToEnd();

            todayNewsList.Items.Clear();
            //제일 처음 내가 필요한 html 부분만 가져오기 위해서 파싱.
            htmlStr = parsingWord(htmlStr, "moreNewsBoxList", "right column of Main Content");

            //기사 title을 따낸다.
            htmlStr = deleteString(htmlStr);
            writeTitle(htmlStr);

            reader.Close();
            dataStream.Close();
            response.Close();
            */
            string temp;
            DataSet ds = new DataSet();
            string strConn = "Server=203.255.3.244;Database=testDB;Uid=root;Pwd=thsdydtjr2;";
            MySqlConnection conn = new MySqlConnection(strConn);

            //MySqlDataAdapter 클래스를 이용하여
            //비연결 모드로 데이타 가져오기
            string sql = "SELECT title FROM testTable";
            MySqlDataAdapter adpt = new MySqlDataAdapter(sql, conn);
            adpt.Fill(ds, "title");
        

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                temp = (string)r["title"];
                todayNewsList.Items.Add(temp.Substring(1, temp.Length-2));
            }

            conn.Close();

        }

        private void todayNewsList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            title = todayNewsList.Text;
            string url = Convert.ToString(result[title]);
            string dictationCount = "";
            string script = "";
            int x = this.Left;
            int y = this.Top;

            resultInput resultIn = new resultInput();
            InputBox inputbox = new InputBox();
            bool patternBool = false;

            userFile uf = new userFile();
            title = toAscii(title);

            string mp3Url, content;
            DataSet ds = new DataSet();
            string strConn = "Server=203.255.3.244;Database=testDB;Uid=root;Pwd=thsdydtjr2;";
            MySqlConnection conn = new MySqlConnection(strConn);

            string sql = "SELECT content,mp3url FROM testTable where title=\"\'" + title + "\'\"";
            MySqlDataAdapter adpt = new MySqlDataAdapter(sql, conn);
            adpt.Fill(ds, "row");

            mp3Url = "";
            content = "";
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                mp3Url = (string)r["mp3url"];
                mp3Url = mp3Url.Substring(1, mp3Url.Length - 2);
                Console.WriteLine(mp3Url);
                content = (string)r["content"];
            }

            conn.Close();
            
            uf.mp3DownLoad(title, mp3Url);

            uf.createReadList(title, mp3Url);
            //script = uf.createScript(title);                // parsing 부분을 쓰레드를 추가하여, 백그라운드로 처리. 그 이후에 스피너를 보여준다.
            script = content.Substring(1, content.Length-4);                                   // 스피너는 gif 혹은 다이렉트X 를 이용하여 만들어준다.

            string[] splitScript = script.Split(' ');
            
            int maxWordScript = splitScript.Length;

            bool checkSelectRange = false;

            tabControl1.SelectedIndex = 1;
            do
            {
                resultIn = inputbox.Show("몇개의 단어를 비우시겠습니까?\n 1-" + maxWordScript + " 안에서 선택해 주세요.", "딕테이션 1.0", "", x + 100, y + 100);
                
                if (!resultIn.getClickCheck())
                    break;
                
                string Pattern = @"^[0-9]*$";

                if (!Regex.IsMatch(resultIn.getTextResult(), Pattern))
                {
                    MessageBox.Show("숫자만 입력 가능합니다.");
                    patternBool = true;
                }
                else
                {
                    patternBool = false;
                }


                if (!patternBool)
                {
                    if (resultIn.getTextResult().Equals("") || resultIn.getTextResult().Equals("0"))
                    {
                        MessageBox.Show("0개를 비울 수는 없습니다.");
                        checkSelectRange = false;
                    }
                    else if (Int32.Parse(resultIn.getTextResult()) > maxWordScript)
                    {
                        MessageBox.Show("최대치를 벗어났습니다.");
                        checkSelectRange = false;
                    }
                    else
                    {
                        checkSelectRange = true;
                    }
                }

            } while (!checkSelectRange || patternBool);

            if (!resultIn.getClickCheck())
            {
                this.tabControl1.SelectedIndex = 0;
            }
            else
            {
                /*
                do
                {
                    dictationCount = Microsoft.VisualBasic.Interaction.InputBox("몇개의 단어를 비우시겠습니까?", "딕테이션 1.0", "", x, y);

                    //Console.Write(dictationCount.Equals(""));
                    if (dictationCount.Equals("0") || dictationCount.Equals(""))
                        MessageBox.Show("0개를 비울 수는 없습니다.");

                } while (dictationCount.Equals("0") || dictationCount.Equals(""));
                 */

                dictationCount = resultIn.getTextResult();
                script = toolClass.createDictation(script, dictationCount);
                
                article.Text = script;
                article.Font = new Font(article.Font, FontStyle.Regular);

                uf.mp3Start(title);
                playStatus = true;
                fileOb = uf.getFileDate();
                fileLength = uf.getCalcuratorLength();

                macTrackBar1.Maximum = (int)fileLength;
                timer1.Start();
            }
        }
        
        private void playbtn_Click(object sender, EventArgs e)
        {
            if(playStatus == false)
            {
                string currentPath = Environment.CurrentDirectory + "\\save";
                string filePath = currentPath + "\\" + fileOb.getYear() + "\\" + fileOb.getMonth() + "\\" + fileOb.getTitle() + ".mp3";
                
                mciSendString("open \"" + filePath + "\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
                mciSendString("play MediaFile", null, 0, IntPtr.Zero);
                timer1.Start();
                playStatus = true;

                macTrackBar1.Minimum = 0;
                macTrackBar1.Maximum = (int)fileLength;
            }
        }

        private void stopbtn_Click(object sender, EventArgs e)
        {
            if (playStatus == true)
            {
                mciSendString("close MediaFile", null, 0, IntPtr.Zero);
                timer1.Stop();
                macTrackBar1.Value = 0;
                playStatus = false;
                macTrackBar1.Maximum = 0;
                macTrackBar1.Minimum = 0;
            }
        }

        private void pushbtn_Click(object sender, EventArgs e)
        {
            if (playStatus == true)
            {
                mciSendString("pause MediaFile", null, 0, IntPtr.Zero);
                timer1.Stop();
                playStatus = false;
            }
        }
       
        private void timer1_Tick(object sender, EventArgs e)
        {
            if ((ulong)macTrackBar1.Value < fileLength)
                macTrackBar1.Value++;
            else
                timer1.Stop();
        }

        private void macTrackBar1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int position = (int)((this.macTrackBar1.Maximum * e.X) / this.macTrackBar1.Width);

                if (playStatus == true)
                {
                    mciSendString("pause MediaFile", null, 0, IntPtr.Zero);
                    string Pcommand = String.Format("seek MediaFile to {0}", macTrackBar1.Value * 1000);
                    mciSendString(Pcommand, null, 0, IntPtr.Zero);
                    mciSendString("play MediaFile", null, 0, IntPtr.Zero);
                }
                else
                {
                    mciSendString("pause MediaFile", null, 0, IntPtr.Zero);
                    string Pcommand = String.Format("seek MediaFile to {0}", macTrackBar1.Value * 1000);
                    mciSendString(Pcommand, null, 0, IntPtr.Zero);
                }
            }
        }

        private void functionPlay()
        {
            if (playStatus == true)
            {
                mciSendString("pause MediaFile", null, 0, IntPtr.Zero);
                timer1.Stop();
                playStatus = false;
            }else{
                string currentPath = Environment.CurrentDirectory + "\\save";
                string filePath = currentPath + "\\" + fileOb.getYear() + "\\" + fileOb.getMonth() + "\\" + fileOb.getTitle() + ".mp3";

                mciSendString("open \"" + filePath + "\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
                mciSendString("play MediaFile", null, 0, IntPtr.Zero);
                timer1.Start();
                playStatus = true;

                macTrackBar1.Minimum = 0;
                macTrackBar1.Maximum = (int)fileLength;
            }
        }

        private void article_KeyDown(object sender, KeyEventArgs e)
        {
            int position = article.SelectionStart;
            
            if (position < article.Text.Length)
            {
                if (article.Text.Substring(position, 1).Equals("_"))
                {
                    if (e.KeyValue >= 65 && e.KeyValue <= 90)
                    {
                        SendKeys.Send("{DELETE}");
                    }
                }
                else if (article.Text.Substring(position, 1).Equals("’") || article.Text.Substring(position, 1).Equals("“") || article.Text.Substring(position, 1).Equals("”"))
                {
                    if (e.KeyValue >= 65 && e.KeyValue <= 90)
                    {
                        article.Focus();
                        article.Select(position + 1, 0);
                    }
                }
            }

            if (e.KeyValue == 112)
            {
                functionPlay();
            }
        }

        private void AnserBt_Click(object sender, EventArgs e)
        {
            toolClass.answerCheck(article.Text, title);
        }

        private void wrongAnswerBt_Click(object sender, EventArgs e)
        {
            toolClass.wrongAnswerCheck(article.Text);
        }

        private void article_MouseDown(object sender, MouseEventArgs e)
        {
            RichTextBox rt = (RichTextBox)sender;
            Point p = rt.Location;
            int mouseLocationX = locationX + p.X + e.X + 20;
            int mouseLocationY = locationY + p.Y + e.Y + 50;

            string script = article.Text;

            if (e.Button == MouseButtons.Right)
            {
                
                if (script.Length != 0)
                {
                    if(article.SelectionLength != 0)
                        findword.Show(new Point(mouseLocationX, mouseLocationY));
                }
            }
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            Form1 fm = (Form1)sender;
            Point p = fm.Location;
            locationX = p.X;
            locationY = p.Y;
        }

        private void findWord_Event(object sender, EventArgs e)
        {
            string findWord = article.SelectedText;
            
            toolClass.findWord(findWord);
        }
        private void mp3PlayerStop()
        {
            if (playStatus == true)
            {
                mciSendString("close MediaFile", null, 0, IntPtr.Zero);
                timer1.Stop();
                macTrackBar1.Value = 0;
                playStatus = false;
                macTrackBar1.Maximum = 0;
                macTrackBar1.Minimum = 0;
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                mp3PlayerStop();
                wordListView.Items.Clear();
                string[,] wordList = toolClass.getWordText();
                Console.WriteLine(wordList.Length);
                
                for (int i = 0; i < (wordList.Length)/3 - 1; i++)
                {
                    wordListView.Items.Add(new ListViewItem(new string[] { wordList[i,0], wordList[i,1], wordList[i,2] }));
                }

            }
            else if (tabControl1.SelectedIndex == 3)
            {
                mp3PlayerStop();

                scoreLabel.Text = "";
                toolClass.printScore(scoreLabel);
            }
            else if (tabControl1.SelectedIndex == 0)
            {
                mp3PlayerStop();
            }
        }

        private void wordListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if(!orderSet)
            {
                orderSet = true;
                wordListView.ListViewItemSorter = new ListViewItemComparer(e.Column, "asc");
            }
            else
            {
                orderSet = false;
                wordListView.ListViewItemSorter = new ListViewItemComparer(e.Column, "desc");
            }
        }

        class ListViewItemComparer : IComparer
        {
            private int col;
            private string sort;
            public ListViewItemComparer()
            {
                col = 0;
            }
            public ListViewItemComparer(int column, string sort)
            {
                col = column;
                this.sort = sort;
            }
            public int Compare(object x, object y)
            {
                if(sort == "asc")
                    return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                else
                    return String.Compare(((ListViewItem)y).SubItems[col].Text, ((ListViewItem)x).SubItems[col].Text);
            }
        }

        private void configuration_Click(object sender, EventArgs e)
        {
            
        }

    }
}

