namespace prjOpenLog {
	partial class frmPrintCards {
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
			this.cmdSetPrint = new System.Windows.Forms.Button();
			this.lblPaperSize = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtSetPrintDef = new System.Windows.Forms.TextBox();
			this.cmdSetPrintDef = new System.Windows.Forms.Button();
			this.ppcCard = new System.Windows.Forms.PrintPreviewControl();
			this.cmdPgNext = new System.Windows.Forms.Button();
			this.cmdPgPrev = new System.Windows.Forms.Button();
			this.cmdPrint = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.cmdSetPrint);
			this.groupBox1.Controls.Add(this.lblPaperSize);
			this.groupBox1.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(360, 67);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "①プリンタ・用紙設定";
			// 
			// cmdSetPrint
			// 
			this.cmdSetPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdSetPrint.Location = new System.Drawing.Point(310, 24);
			this.cmdSetPrint.Name = "cmdSetPrint";
			this.cmdSetPrint.Size = new System.Drawing.Size(44, 25);
			this.cmdSetPrint.TabIndex = 1;
			this.cmdSetPrint.Text = "設定";
			this.cmdSetPrint.UseVisualStyleBackColor = true;
			this.cmdSetPrint.Click += new System.EventHandler(this.cmdSetPrint_Click);
			// 
			// lblPaperSize
			// 
			this.lblPaperSize.AutoSize = true;
			this.lblPaperSize.Location = new System.Drawing.Point(6, 21);
			this.lblPaperSize.Name = "lblPaperSize";
			this.lblPaperSize.Size = new System.Drawing.Size(267, 17);
			this.lblPaperSize.TabIndex = 0;
			this.lblPaperSize.Text = "用紙未設定(印刷設定ボタンから設定してください)";
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.txtSetPrintDef);
			this.groupBox2.Controls.Add(this.cmdSetPrintDef);
			this.groupBox2.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.groupBox2.Location = new System.Drawing.Point(12, 85);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(360, 67);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "②印刷定義ファイル";
			// 
			// txtSetPrintDef
			// 
			this.txtSetPrintDef.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSetPrintDef.Location = new System.Drawing.Point(9, 24);
			this.txtSetPrintDef.Name = "txtSetPrintDef";
			this.txtSetPrintDef.Size = new System.Drawing.Size(295, 25);
			this.txtSetPrintDef.TabIndex = 2;
			// 
			// cmdSetPrintDef
			// 
			this.cmdSetPrintDef.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdSetPrintDef.Location = new System.Drawing.Point(310, 24);
			this.cmdSetPrintDef.Name = "cmdSetPrintDef";
			this.cmdSetPrintDef.Size = new System.Drawing.Size(44, 25);
			this.cmdSetPrintDef.TabIndex = 1;
			this.cmdSetPrintDef.Text = "参照";
			this.cmdSetPrintDef.UseVisualStyleBackColor = true;
			this.cmdSetPrintDef.Click += new System.EventHandler(this.cmdSetPrintDef_Click);
			// 
			// ppcCard
			// 
			this.ppcCard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ppcCard.Location = new System.Drawing.Point(12, 187);
			this.ppcCard.Name = "ppcCard";
			this.ppcCard.Size = new System.Drawing.Size(360, 444);
			this.ppcCard.TabIndex = 2;
			// 
			// cmdPgNext
			// 
			this.cmdPgNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdPgNext.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.cmdPgNext.Location = new System.Drawing.Point(314, 156);
			this.cmdPgNext.Name = "cmdPgNext";
			this.cmdPgNext.Size = new System.Drawing.Size(58, 25);
			this.cmdPgNext.TabIndex = 3;
			this.cmdPgNext.Text = "次へ >";
			this.cmdPgNext.UseVisualStyleBackColor = true;
			this.cmdPgNext.Click += new System.EventHandler(this.cmdPgNext_Click);
			// 
			// cmdPgPrev
			// 
			this.cmdPgPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdPgPrev.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.cmdPgPrev.Location = new System.Drawing.Point(250, 156);
			this.cmdPgPrev.Name = "cmdPgPrev";
			this.cmdPgPrev.Size = new System.Drawing.Size(58, 25);
			this.cmdPgPrev.TabIndex = 4;
			this.cmdPgPrev.Text = "< 前へ";
			this.cmdPgPrev.UseVisualStyleBackColor = true;
			this.cmdPgPrev.Click += new System.EventHandler(this.cmdPgPrev_Click);
			// 
			// cmdPrint
			// 
			this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdPrint.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.cmdPrint.Location = new System.Drawing.Point(12, 644);
			this.cmdPrint.Name = "cmdPrint";
			this.cmdPrint.Size = new System.Drawing.Size(354, 25);
			this.cmdPrint.TabIndex = 5;
			this.cmdPrint.Text = "印　刷";
			this.cmdPrint.UseVisualStyleBackColor = true;
			this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
			// 
			// frmPrintCards
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 681);
			this.Controls.Add(this.cmdPrint);
			this.Controls.Add(this.cmdPgPrev);
			this.Controls.Add(this.cmdPgNext);
			this.Controls.Add(this.ppcCard);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "frmPrintCards";
			this.Text = "OpenLog -カード印刷";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPrintCards_FormClosed);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button cmdSetPrint;
		private System.Windows.Forms.Label lblPaperSize;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox txtSetPrintDef;
		private System.Windows.Forms.Button cmdSetPrintDef;
		private System.Windows.Forms.PrintPreviewControl ppcCard;
		private System.Windows.Forms.Button cmdPgNext;
		private System.Windows.Forms.Button cmdPgPrev;
		private System.Windows.Forms.Button cmdPrint;
	}
}