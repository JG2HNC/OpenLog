namespace prjOpenLog {
	partial class frmModeSelect {
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
			this.lstModes = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cmdOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lstModes
			// 
			this.lstModes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstModes.FormattingEnabled = true;
			this.lstModes.ItemHeight = 12;
			this.lstModes.Location = new System.Drawing.Point(12, 24);
			this.lstModes.Name = "lstModes";
			this.lstModes.Size = new System.Drawing.Size(193, 112);
			this.lstModes.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(196, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "Select mode./モードを選択してください。";
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Location = new System.Drawing.Point(12, 143);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(193, 23);
			this.cmdOK.TabIndex = 2;
			this.cmdOK.Text = "O K";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// frmModeSelect
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(217, 178);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lstModes);
			this.Name = "frmModeSelect";
			this.Text = "SelectMode";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmModeSelect_FormClosed);
			this.Load += new System.EventHandler(this.frmModeSelect_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox lstModes;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cmdOK;
	}
}