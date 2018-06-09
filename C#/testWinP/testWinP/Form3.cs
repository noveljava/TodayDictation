using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using userFileClass;
namespace form3
{
    public partial class Form3 : Form
    {
        string formWord="";
        string formMean = "";

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        public void setLabel(string word, string mean)
        {
            formWord = word;
            formMean = mean;
           
            label1.Text = word + "\n\n" + mean;

            if (mean.Equals("단어 뜻이 없습니다."))
                button1.Visible = false;
            else
                button1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            userFile uf = new userFile();

            uf.createWordText(formWord, formMean);
            MessageBox.Show("등록 되었습니다.");
            this.Close();
        }
    }
}
