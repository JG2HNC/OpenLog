namespace prjOpenLog {
	partial class frmSearchCityCode {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkExceptOld = new System.Windows.Forms.CheckBox();
			this.cmdSearch = new System.Windows.Forms.Button();
			this.txtArea = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtKeyword = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cmdOK = new System.Windows.Forms.Button();
			this.lstResult = new System.Windows.Forms.ListBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkExceptOld);
			this.groupBox1.Controls.Add(this.cmdSearch);
			this.groupBox1.Controls.Add(this.txtArea);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtKeyword);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(391, 74);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "JCC/JCG検索";
			// 
			// chkExceptOld
			// 
			this.chkExceptOld.AutoSize = true;
			this.chkExceptOld.Checked = true;
			this.chkExceptOld.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkExceptOld.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.chkExceptOld.Location = new System.Drawing.Point(9, 45);
			this.chkExceptOld.Margin = new System.Windows.Forms.Padding(2);
			this.chkExceptOld.Name = "chkExceptOld";
			this.chkExceptOld.Size = new System.Drawing.Size(202, 19);
			this.chkExceptOld.TabIndex = 4;
			this.chkExceptOld.Text = "廃止市区町村を除外する";
			this.chkExceptOld.UseVisualStyleBackColor = true;
			// 
			// cmdSearch
			// 
			this.cmdSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdSearch.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.cmdSearch.Location = new System.Drawing.Point(334, 17);
			this.cmdSearch.Name = "cmdSearch";
			this.cmdSearch.Size = new System.Drawing.Size(51, 23);
			this.cmdSearch.TabIndex = 3;
			this.cmdSearch.Text = "検索";
			this.cmdSearch.UseVisualStyleBackColor = true;
			this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
			// 
			// txtArea
			// 
			this.txtArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtArea.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.txtArea.Location = new System.Drawing.Point(292, 18);
			this.txtArea.Name = "txtArea";
			this.txtArea.Size = new System.Drawing.Size(36, 22);
			this.txtArea.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(238, 21);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55, 15);
			this.label2.TabIndex = 2;
			this.label2.Text = "エリア";
			// 
			// txtKeyword
			// 
			this.txtKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtKeyword.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.txtKeyword.Location = new System.Drawing.Point(131, 18);
			this.txtKeyword.Name = "txtKeyword";
			this.txtKeyword.Size = new System.Drawing.Size(105, 22);
			this.txtKeyword.TabIndex = 1;
			this.txtKeyword.TextChanged += new System.EventHandler(this.txtKeyword_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(6, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(119, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "よみ(前方一致)";
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.cmdOK.Location = new System.Drawing.Point(339, 297);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(64, 26);
			this.cmdOK.TabIndex = 4;
			this.cmdOK.Text = "O K";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// lstResult
			// 
			this.lstResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstResult.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lstResult.FormattingEnabled = true;
			this.lstResult.ItemHeight = 15;
			this.lstResult.Location = new System.Drawing.Point(12, 92);
			this.lstResult.Name = "lstResult";
			this.lstResult.Size = new System.Drawing.Size(391, 199);
			this.lstResult.TabIndex = 5;
			// 
			// frmSearchCityCode
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(415, 330);
			this.Controls.Add(this.lstResult);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.groupBox1);
			this.Name = "frmSearchCityCode";
			this.Text = "JCC/JCG検索";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSearchCityCode_FormClosed);
			this.Load += new System.EventHandler(this.frmSearchDomesticCode_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button cmdSearch;
		private System.Windows.Forms.TextBox txtArea;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtKeyword;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.ListBox lstResult;
		private System.Windows.Forms.CheckBox chkExceptOld;
	}
}