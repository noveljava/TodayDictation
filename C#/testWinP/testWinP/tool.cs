using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Net;

using form2;
using form3;
using userFileClass;

namespace toolClass
{
    class tool
    {
        private int[] questionBox;
        private int questionCount;
        private string[] questionWord;

        public string createDictation(string script, string count)
        {
            Random random = new Random();
            questionCount = Convert.ToInt32(count);
            questionBox = new int[questionCount];
            questionWord = new string[questionCount];

            string[] splitScript = script.Split(' ');
            int max = splitScript.Length;
            int i = 0;
            int temp = 0;
            string newsScript = "";

            for (i = 0; i < questionCount; i++)
            {
                temp = random.Next(0, max);

                for (int j = 0; j < i; j++)
                {
                    while (temp == questionBox[j])
                    {
                        temp = random.Next(0, max);

                        if ((temp + 1) < splitScript.Length)
                        {
                            if (splitScript[temp].Equals(":"))
                                temp++;
                            else if (splitScript[temp + 1].Equals(":"))
                                temp += 2;
                            else if (splitScript[temp].IndexOf("-") >= 0)
                                temp++;
                            else if (splitScript[temp].Equals(""))
                                temp++;
                                
                        }
                    }
                }
                
                if(splitScript[temp].Equals(""))
                {
                    //if (temp + 1 < splitScript.Length)
                    //    Console.WriteLine(splitScript[temp - 1] + " " + splitScript[temp] + " " + splitScript[temp + 1]);
                    /*
                    bool isExsit = true;
                    int addNum = 0;
                    while (isExsit)
                    {
                        addNum++;
                        for (int j = 0; j < i; j++)
                        {
                            if (temp + addNum != questionBox[j])
                            {
                                if (temp + addNum < splitScript.Length)
                                {
                                    temp += addNum;
                                    isExsit = false;
                                }
                                else
                                    temp = random.Next(0, max);
                            }
                        }
                        Console.WriteLine(addNum);
                    }
                     */
                }

                questionBox[i] = temp;
            }

            for (i = 0; i < questionCount; i++)
            {
                int randomNumber = questionBox[i];
                temp = splitScript[randomNumber].Length;
                int case1Number = splitScript[randomNumber].IndexOf("’");
                int case2Number = splitScript[randomNumber].IndexOf("“");
                int case3Number = splitScript[randomNumber].IndexOf("”");
                int case4Number = splitScript[randomNumber].IndexOf(".");

                questionWord[i] = splitScript[randomNumber];
                
                splitScript[randomNumber] = "";
                
                for (int j = 0; j < temp; j++)
                {
                    if(j == case1Number)
                        splitScript[randomNumber] += "’";
                    else if(j == case2Number)
                        splitScript[randomNumber] += "“";
                    else if (j == case3Number)
                        splitScript[randomNumber] += "”";
                    else if (j == case4Number)
                        splitScript[randomNumber] += ".";
                    else
                        splitScript[randomNumber] += "_";
                }

                //Console.WriteLine("original word : " + questionWord[i] + "\n removeWord : " + splitScript[randomNumber]);
                //if (questionWord[i].Equals(""))
                //    Console.WriteLine("암것도 없졍 ?");
            }

            i=0;
            while (i < max)
            {
                newsScript += splitScript[i++] + " ";
            }

            return newsScript;
        }

        public void answerCheck(string newsScript, string title)
        {
            string[] splitNewScript = newsScript.Split(' ');
            string score = "";

            int i = 0;
            int ok = 0;
            int no = 0;

            for (i = 0; i < questionCount; i++)
            {
                if (questionWord[i].ToLower().Equals(splitNewScript[questionBox[i]].ToLower()))
                {
                    ok++;
                }
                else
                {
                    no++;
                }
            }

            MessageBox.Show(questionCount + "개 중에 " + ok + "개를 맞추셨습니다.");
            score = ok + " / " + questionCount;
            userFile uf = new userFile();
            uf.createScore(score, title);
        }

        public void wrongAnswerCheck(string newsScript)
        {
            string[] splitNewScript = newsScript.Split(' ');
            int i = 0;
            
            Form2 form = new Form2();
            
            for (i = 0; i < questionCount; i++)
            {
                if (!questionWord[i].ToLower().Equals(splitNewScript[questionBox[i]].ToLower()))
                {
                    form.writeListBox(questionWord[i].ToLower(), splitNewScript[questionBox[i]].ToLower());
                    
                }
            }

            form.ShowDialog();
        }

        public void findWord(string word)
        {
            string currentPath = Environment.CurrentDirectory + "\\Dictionary\\";
            string fileName = "";
            string[] splitWord = new string[2];
            bool notFindCheck = true;

            word = word.ToLower();
            byte[] index = Encoding.ASCII.GetBytes(word.Substring(0,1));
            
            if(index[0] >= 115 && index[0] <= 122)
                fileName = "sTOz.txt";
            else if(index[0] >= 104 && index[0] <= 114)
                fileName = "hTOr.txt";
            else
                fileName = "aTOg.txt";

            FileStream fs = new FileStream(currentPath + fileName, FileMode.Open);
            StreamReader sw = new StreamReader(fs, System.Text.Encoding.Default);

            string readLine;

            if (word.Substring(word.Length - 1, 1).Equals(" "))
                word = word.Substring(0, word.Length - 1);
            
            do
            {
                readLine = sw.ReadLine();
                if (readLine != null && readLine.IndexOf(word) >= 0)
                {
                    splitWord = readLine.Split('>');
                    
                    if (splitWord[0].Equals(word))
                        notFindCheck = false;
                }
            } while (readLine != null && notFindCheck);

            sw.Close();
            fs.Close();

            Form3 form3 = new Form3();

            if (notFindCheck)
                //form3.setLabel(splitWord[0] + "\n\n" + splitWord[1]);
                splitWord[1] = "단어 뜻이 없습니다.";

            form3.setLabel(splitWord[0], splitWord[1]);
            form3.ShowDialog();

        }

        public string[,] getWordText()
        {
            string currentPath = Environment.CurrentDirectory + "\\wordText\\wordText.txt";
            string tempStrLine = "";
            int i = 0;

            if (System.IO.File.Exists(currentPath))
            {
                FileStream fs = new FileStream(currentPath, FileMode.Open);
                StreamReader sw = new StreamReader(fs, System.Text.Encoding.Unicode);

                string readLine = "";

                do
                {
                    tempStrLine = sw.ReadLine();

                    if (tempStrLine != null)
                        readLine += tempStrLine + "\n";

                } while (tempStrLine != null);

                sw.Close();
                fs.Close();

                string[] wordLine = readLine.Split('\n');
                string[,] wordArr = new string[wordLine.Length, 3];

                for (i = 0; i < wordLine.Length - 1; i++)
                {
                    string[] temp = wordLine[i].Split('>');
                    Console.WriteLine(temp[0] + " : " + temp[1]);
                    for (int j = 0; j < 3; j++)
                    {
                        wordArr[i, j] = temp[j];
                    }
                }

                return wordArr;
            }
            else
            {
                string[,] temp = new string[1, 1];
                return temp;
            }
        }

        public void printScore(object e)
        {
            string pullPath = Environment.CurrentDirectory + "\\save\\score.txt";
            Label label = (Label)e;

            Console.WriteLine(System.IO.File.Exists(pullPath));
            if (!System.IO.File.Exists(pullPath))
            {
                label.Text = "기록이 없습니다.";
            }
            else
            {
                FileStream fs = new FileStream(pullPath, FileMode.Open);
                StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
                string temp = "";
                string text = "";

                do
                {
                    temp = sr.ReadLine();
                    if (temp != null)
                        text += temp + "\n";

                } while (temp != null);

                label.Text = text;

                fs.Close();
                sr.Close();
            }
        }
    }
}
