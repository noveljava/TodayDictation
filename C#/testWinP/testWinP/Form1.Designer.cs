namespace testWinP
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.todayNews = new System.Windows.Forms.TabPage();
            this.newUpdateBt = new System.Windows.Forms.Button();
            this.todayNewsList = new System.Windows.Forms.ListBox();
            this.dictation = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.wrongAnswerBt = new System.Windows.Forms.Button();
            this.AnserBt = new System.Windows.Forms.Button();
            this.macTrackBar1 = new EConTech.Windows.MACUI.MACTrackBar();
            this.stopbtn = new System.Windows.Forms.Button();
            this.pushbtn = new System.Windows.Forms.Button();
            this.palybtn = new System.Windows.Forms.Button();
            this.article = new System.Windows.Forms.RichTextBox();
            this.words = new System.Windows.Forms.TabPage();
            this.wordListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.recordDictation = new System.Windows.Forms.TabPage();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.findword = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.단어찾기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.todayNews.SuspendLayout();
            this.dictation.SuspendLayout();
            this.words.SuspendLayout();
            this.recordDictation.SuspendLayout();
            this.findword.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.todayNews);
            this.tabControl1.Controls.Add(this.dictation);
            this.tabControl1.Controls.Add(this.words);
            this.tabControl1.Controls.Add(this.recordDictation);
            this.tabControl1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tabControl1.ItemSize = new System.Drawing.Size(64, 18);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(772, 447);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // todayNews
            // 
            this.todayNews.Controls.Add(this.newUpdateBt);
            this.todayNews.Controls.Add(this.todayNewsList);
            this.todayNews.Location = new System.Drawing.Point(4, 22);
            this.todayNews.Name = "todayNews";
            this.todayNews.Padding = new System.Windows.Forms.Padding(3);
            this.todayNews.Size = new System.Drawing.Size(764, 421);
            this.todayNews.TabIndex = 0;
            this.todayNews.Text = "오늘의뉴스";
            this.todayNews.UseVisualStyleBackColor = true;
            // 
            // newUpdateBt
            // 
            this.newUpdateBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.newUpdateBt.Font = new System.Drawing.Font("굴림", 8F);
            this.newUpdateBt.Location = new System.Drawing.Point(637, 376);
            this.newUpdateBt.Name = "newUpdateBt";
            this.newUpdateBt.Size = new System.Drawing.Size(100, 23);
            this.newUpdateBt.TabIndex = 2;
            this.newUpdateBt.Text = "뉴스업데이트";
            this.newUpdateBt.UseVisualStyleBackColor = true;
            this.newUpdateBt.Click += new System.EventHandler(this.newsUpdateBt_Click);
            // 
            // todayNewsList
            // 
            this.todayNewsList.FormattingEnabled = true;
            this.todayNewsList.ItemHeight = 12;
            this.todayNewsList.Location = new System.Drawing.Point(21, 16);
            this.todayNewsList.Name = "todayNewsList";
            this.todayNewsList.Size = new System.Drawing.Size(716, 316);
            this.todayNewsList.TabIndex = 1;
            this.todayNewsList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.todayNewsList_MouseDoubleClick);
            // 
            // dictation
            // 
            this.dictation.Controls.Add(this.label1);
            this.dictation.Controls.Add(this.wrongAnswerBt);
            this.dictation.Controls.Add(this.AnserBt);
            this.dictation.Controls.Add(this.macTrackBar1);
            this.dictation.Controls.Add(this.stopbtn);
            this.dictation.Controls.Add(this.pushbtn);
            this.dictation.Controls.Add(this.palybtn);
            this.dictation.Controls.Add(this.article);
            this.dictation.Location = new System.Drawing.Point(4, 22);
            this.dictation.Name = "dictation";
            this.dictation.Padding = new System.Windows.Forms.Padding(3);
            this.dictation.Size = new System.Drawing.Size(764, 421);
            this.dictation.TabIndex = 1;
            this.dictation.Text = "딕테이션";
            this.dictation.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(557, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 30);
            this.label1.TabIndex = 8;
            this.label1.Text = "※ F1 버튼으로 일시정지 및 재생이 가능합니다.";
            // 
            // wrongAnswerBt
            // 
            this.wrongAnswerBt.Location = new System.Drawing.Point(621, 371);
            this.wrongAnswerBt.Name = "wrongAnswerBt";
            this.wrongAnswerBt.Size = new System.Drawing.Size(124, 26);
            this.wrongAnswerBt.TabIndex = 7;
            this.wrongAnswerBt.Text = "오답확인";
            this.wrongAnswerBt.UseVisualStyleBackColor = true;
            this.wrongAnswerBt.Click += new System.EventHandler(this.wrongAnswerBt_Click);
            // 
            // AnserBt
            // 
            this.AnserBt.Location = new System.Drawing.Point(621, 328);
            this.AnserBt.Name = "AnserBt";
            this.AnserBt.Size = new System.Drawing.Size(124, 26);
            this.AnserBt.TabIndex = 6;
            this.AnserBt.Text = "정답확인";
            this.AnserBt.UseVisualStyleBackColor = true;
            this.AnserBt.Click += new System.EventHandler(this.AnserBt_Click);
            // 
            // macTrackBar1
            // 
            this.macTrackBar1.BackColor = System.Drawing.Color.Transparent;
            this.macTrackBar1.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.macTrackBar1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.macTrackBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.macTrackBar1.IndentHeight = 6;
            this.macTrackBar1.LargeChange = 1;
            this.macTrackBar1.Location = new System.Drawing.Point(559, 10);
            this.macTrackBar1.Maximum = 10;
            this.macTrackBar1.Minimum = 0;
            this.macTrackBar1.Name = "macTrackBar1";
            this.macTrackBar1.Size = new System.Drawing.Size(199, 28);
            this.macTrackBar1.TabIndex = 5;
            this.macTrackBar1.TextTickStyle = System.Windows.Forms.TickStyle.None;
            this.macTrackBar1.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(146)))), ((int)(((byte)(148)))));
            this.macTrackBar1.TickHeight = 4;
            this.macTrackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.macTrackBar1.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(130)))), ((int)(((byte)(198)))));
            this.macTrackBar1.TrackerSize = new System.Drawing.Size(16, 16);
            this.macTrackBar1.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(93)))), ((int)(((byte)(90)))));
            this.macTrackBar1.TrackLineHeight = 3;
            this.macTrackBar1.Value = 0;
            this.macTrackBar1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.macTrackBar1_MouseClick);
            // 
            // stopbtn
            // 
            this.stopbtn.Location = new System.Drawing.Point(721, 44);
            this.stopbtn.Name = "stopbtn";
            this.stopbtn.Size = new System.Drawing.Size(24, 22);
            this.stopbtn.TabIndex = 4;
            this.stopbtn.Text = "■";
            this.stopbtn.UseVisualStyleBackColor = true;
            this.stopbtn.Click += new System.EventHandler(this.stopbtn_Click);
            // 
            // pushbtn
            // 
            this.pushbtn.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.pushbtn.Location = new System.Drawing.Point(691, 44);
            this.pushbtn.Name = "pushbtn";
            this.pushbtn.Size = new System.Drawing.Size(24, 22);
            this.pushbtn.TabIndex = 3;
            this.pushbtn.Text = "||";
            this.pushbtn.UseVisualStyleBackColor = true;
            this.pushbtn.Click += new System.EventHandler(this.pushbtn_Click);
            // 
            // palybtn
            // 
            this.palybtn.Location = new System.Drawing.Point(661, 44);
            this.palybtn.Name = "palybtn";
            this.palybtn.Size = new System.Drawing.Size(24, 22);
            this.palybtn.TabIndex = 2;
            this.palybtn.Text = "▶";
            this.palybtn.UseVisualStyleBackColor = true;
            this.palybtn.Click += new System.EventHandler(this.playbtn_Click);
            // 
            // article
            // 
            this.article.BackColor = System.Drawing.SystemColors.Window;
            this.article.Location = new System.Drawing.Point(9, 10);
            this.article.Name = "article";
            this.article.Size = new System.Drawing.Size(522, 402);
            this.article.TabIndex = 0;
            this.article.Text = "";
            this.article.KeyDown += new System.Windows.Forms.KeyEventHandler(this.article_KeyDown);
            this.article.MouseDown += new System.Windows.Forms.MouseEventHandler(this.article_MouseDown);
            // 
            // words
            // 
            this.words.Controls.Add(this.wordListView);
            this.words.Location = new System.Drawing.Point(4, 22);
            this.words.Name = "words";
            this.words.Size = new System.Drawing.Size(764, 421);
            this.words.TabIndex = 2;
            this.words.Text = "단어장";
            this.words.UseVisualStyleBackColor = true;
            // 
            // wordListView
            // 
            this.wordListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.wordListView.Location = new System.Drawing.Point(14, 17);
            this.wordListView.Name = "wordListView";
            this.wordListView.Size = new System.Drawing.Size(584, 387);
            this.wordListView.TabIndex = 0;
            this.wordListView.UseCompatibleStateImageBehavior = false;
            this.wordListView.View = System.Windows.Forms.View.Details;
            this.wordListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.wordListView_ColumnClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "단어";
            this.columnHeader1.Width = 165;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "뜻";
            this.columnHeader2.Width = 337;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "검색횟수";
            this.columnHeader3.Width = 69;
            // 
            // recordDictation
            // 
            this.recordDictation.Controls.Add(this.scoreLabel);
            this.recordDictation.Location = new System.Drawing.Point(4, 22);
            this.recordDictation.Name = "recordDictation";
            this.recordDictation.Size = new System.Drawing.Size(764, 421);
            this.recordDictation.TabIndex = 3;
            this.recordDictation.Text = "딕테이션기록";
            this.recordDictation.UseVisualStyleBackColor = true;
            // 
            // scoreLabel
            // 
            this.scoreLabel.Location = new System.Drawing.Point(7, 10);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(747, 400);
            this.scoreLabel.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // findword
            // 
            this.findword.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.단어찾기ToolStripMenuItem});
            this.findword.Name = "findword";
            this.findword.Size = new System.Drawing.Size(123, 26);
            // 
            // 단어찾기ToolStripMenuItem
            // 
            this.단어찾기ToolStripMenuItem.Name = "단어찾기ToolStripMenuItem";
            this.단어찾기ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.단어찾기ToolStripMenuItem.Text = "단어찾기";
            this.단어찾기ToolStripMenuItem.Click += new System.EventHandler(this.findWord_Event);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 446);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "오늘의 듣기 ver1.0";
            this.LocationChanged += new System.EventHandler(this.Form1_LocationChanged);
            this.tabControl1.ResumeLayout(false);
            this.todayNews.ResumeLayout(false);
            this.dictation.ResumeLayout(false);
            this.words.ResumeLayout(false);
            this.recordDictation.ResumeLayout(false);
            this.findword.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage todayNews;
        private System.Windows.Forms.TabPage dictation;
        private System.Windows.Forms.TabPage words;
        private System.Windows.Forms.TabPage recordDictation;
        private System.Windows.Forms.Button newUpdateBt;
        private System.Windows.Forms.ListBox todayNewsList;
        private System.Windows.Forms.RichTextBox article;
        private System.Windows.Forms.Button stopbtn;
        private System.Windows.Forms.Button pushbtn;
        private System.Windows.Forms.Button palybtn;
        private System.Windows.Forms.Timer timer1;
        private EConTech.Windows.MACUI.MACTrackBar macTrackBar1;
        private System.Windows.Forms.Button AnserBt;
        private System.Windows.Forms.Button wrongAnswerBt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip findword;
        private System.Windows.Forms.ToolStripMenuItem 단어찾기ToolStripMenuItem;
        private System.Windows.Forms.ListView wordListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label scoreLabel;

    }
}

