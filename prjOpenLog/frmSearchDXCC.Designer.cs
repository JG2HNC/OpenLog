namespace prjOpenLog {
	partial class frmSearchDXCC {
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
			this.txtPrefix = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cmdSearch = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.lstDXCC = new System.Windows.Forms.ListBox();
			this.chkExceptDeletedEntities = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtPrefix
			// 
			this.txtPrefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPrefix.Location = new System.Drawing.Point(6, 18);
			this.txtPrefix.Name = "txtPrefix";
			this.txtPrefix.Size = new System.Drawing.Size(189, 19);
			this.txtPrefix.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cmdSearch);
			this.groupBox1.Controls.Add(this.txtPrefix);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(260, 48);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Put callsingn(prefix)";
			// 
			// cmdSearch
			// 
			this.cmdSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.cmdSearch.Location = new System.Drawing.Point(201, 18);
			this.cmdSearch.Name = "cmdSearch";
			this.cmdSearch.Size = new System.Drawing.Size(53, 19);
			this.cmdSearch.TabIndex = 2;
			this.cmdSearch.Text = "Search";
			this.cmdSearch.UseVisualStyleBackColor = true;
			this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Location = new System.Drawing.Point(223, 243);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(58, 19);
			this.cmdOK.TabIndex = 2;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// lstDXCC
			// 
			this.lstDXCC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstDXCC.FormattingEnabled = true;
			this.lstDXCC.ItemHeight = 12;
			this.lstDXCC.Location = new System.Drawing.Point(12, 88);
			this.lstDXCC.Name = "lstDXCC";
			this.lstDXCC.Size = new System.Drawing.Size(269, 148);
			this.lstDXCC.TabIndex = 3;
			// 
			// chkExceptDeletedEntities
			// 
			this.chkExceptDeletedEntities.AutoSize = true;
			this.chkExceptDeletedEntities.Checked = true;
			this.chkExceptDeletedEntities.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkExceptDeletedEntities.Location = new System.Drawing.Point(12, 66);
			this.chkExceptDeletedEntities.Name = "chkExceptDeletedEntities";
			this.chkExceptDeletedEntities.Size = new System.Drawing.Size(150, 16);
			this.chkExceptDeletedEntities.TabIndex = 4;
			this.chkExceptDeletedEntities.Text = "消滅エンティティを除外する";
			this.chkExceptDeletedEntities.UseVisualStyleBackColor = true;
			// 
			// frmSearchDXCC
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(293, 274);
			this.Controls.Add(this.chkExceptDeletedEntities);
			this.Controls.Add(this.lstDXCC);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.groupBox1);
			this.Name = "frmSearchDXCC";
			this.Text = "SearchDXCC";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSearchDXCC_FormClosed);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtPrefix;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button cmdSearch;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.ListBox lstDXCC;
		private System.Windows.Forms.CheckBox chkExceptDeletedEntities;
	}
}