using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InputBoxNamespace
{
    public class resultInput
    {
        bool clickCheck;
        string textResult;

        public resultInput()
        {
            clickCheck = false;
            textResult = "";
        }
        public void setClickCheck(bool value)
        {
            clickCheck = value;
        }

        public void setTextResult(string value)
        {
            textResult = value;
        }

        public bool getClickCheck()
        {
            return clickCheck;
        }

        public string getTextResult()
        {
            return textResult;
        }
    }

    public partial class InputBox : Form
    {
        resultInput result = new resultInput();

        public InputBox()
        {
            InitializeComponent();
        }

        private void InputBox_Load(object sender, EventArgs e)
        {
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            result.setClickCheck(true);
            result.setTextResult(textBox1.Text);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            result.setClickCheck(false);
            this.Close();
        }

        public resultInput Show(string label, string title, string prompt, int x, int y)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(x, y);
            this.Text = title;
            label1.Text = label;
            textBox1.Text = prompt;
            textBox1.TabIndex = 0;
            textBox1.Focus();
            
            this.ShowDialog();

            return result;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                result.setClickCheck(true);
                result.setTextResult(textBox1.Text);

                this.Close();
             }
        }
    }
}
