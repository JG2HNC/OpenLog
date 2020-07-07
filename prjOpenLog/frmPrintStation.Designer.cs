namespace prjOpenLog {
	partial class frmPrintStation {
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
			this.lstStationList = new System.Windows.Forms.CheckedListBox();
			this.cmdDoPrint = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lstStationList
			// 
			this.lstStationList.FormattingEnabled = true;
			this.lstStationList.Location = new System.Drawing.Point(12, 24);
			this.lstStationList.Name = "lstStationList";
			this.lstStationList.Size = new System.Drawing.Size(261, 186);
			this.lstStationList.TabIndex = 0;
			// 
			// cmdDoPrint
			// 
			this.cmdDoPrint.Location = new System.Drawing.Point(197, 226);
			this.cmdDoPrint.Name = "cmdDoPrint";
			this.cmdDoPrint.Size = new System.Drawing.Size(75, 23);
			this.cmdDoPrint.TabIndex = 1;
			this.cmdDoPrint.Text = "印刷する";
			this.cmdDoPrint.UseVisualStyleBackColor = true;
			this.cmdDoPrint.Click += new System.EventHandler(this.cmdDoPrint_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Location = new System.Drawing.Point(116, 226);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 2;
			this.cmdCancel.Text = "キャンセル";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(261, 12);
			this.label1.TabIndex = 3;
			this.label1.Text = "カード印刷対象のコールサイン一覧(括弧内は交信数)";
			// 
			// frmPrintStation
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdDoPrint);
			this.Controls.Add(this.lstStationList);
			this.Name = "frmPrintStation";
			this.Text = "印刷対象局一覧";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPrintStation_FormClosed);
			this.Load += new System.EventHandler(this.frmPrintStation_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckedListBox lstStationList;
		private System.Windows.Forms.Button cmdDoPrint;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Label label1;
	}
}