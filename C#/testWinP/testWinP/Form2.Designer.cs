namespace form2
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.closeBt = new System.Windows.Forms.Button();
            this.wrongAnswerList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // closeBt
            // 
            this.closeBt.Location = new System.Drawing.Point(227, 216);
            this.closeBt.Name = "closeBt";
            this.closeBt.Size = new System.Drawing.Size(77, 38);
            this.closeBt.TabIndex = 1;
            this.closeBt.Text = "닫기";
            this.closeBt.UseVisualStyleBackColor = true;
            this.closeBt.Click += new System.EventHandler(this.closeBt_Click);
            // 
            // wrongAnswerList
            // 
            this.wrongAnswerList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.wrongAnswerList.Location = new System.Drawing.Point(10, 14);
            this.wrongAnswerList.Name = "wrongAnswerList";
            this.wrongAnswerList.Size = new System.Drawing.Size(294, 187);
            this.wrongAnswerList.TabIndex = 2;
            this.wrongAnswerList.UseCompatibleStateImageBehavior = false;
            this.wrongAnswerList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "정답";
            this.columnHeader1.Width = 130;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "오답";
            this.columnHeader2.Width = 125;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 266);
            this.Controls.Add(this.wrongAnswerList);
            this.Controls.Add(this.closeBt);
            this.Name = "Form2";
            this.Text = "오답";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button closeBt;
        private System.Windows.Forms.ListView wrongAnswerList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}