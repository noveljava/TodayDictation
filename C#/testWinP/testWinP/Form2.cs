using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace form2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void closeBt_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void writeListBox(string answer, string wrongAnswer)
        {
            wrongAnswerList.Items.Add(new ListViewItem(new string[] {answer, wrongAnswer}));
        }
    }
}
