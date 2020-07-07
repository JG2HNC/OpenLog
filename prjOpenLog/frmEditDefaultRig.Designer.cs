namespace prjOpenLog {
	partial class frmEditDefaultRig {
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
			this.cmdOk = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.dgvDefaultRig = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.dgvDefaultRig)).BeginInit();
			this.SuspendLayout();
			// 
			// cmdOk
			// 
			this.cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOk.Location = new System.Drawing.Point(306, 269);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(93, 23);
			this.cmdOk.TabIndex = 0;
			this.cmdOk.Text = "変更を反映する";
			this.cmdOk.UseVisualStyleBackColor = true;
			this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Location = new System.Drawing.Point(238, 269);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(62, 23);
			this.cmdCancel.TabIndex = 1;
			this.cmdCancel.Text = "キャンセル";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// dgvDefaultRig
			// 
			this.dgvDefaultRig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgvDefaultRig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvDefaultRig.Location = new System.Drawing.Point(12, 12);
			this.dgvDefaultRig.Name = "dgvDefaultRig";
			this.dgvDefaultRig.RowTemplate.Height = 21;
			this.dgvDefaultRig.Size = new System.Drawing.Size(387, 251);
			this.dgvDefaultRig.TabIndex = 2;
			this.dgvDefaultRig.SelectionChanged += new System.EventHandler(this.dgvDefaultRig_SelectionChanged);
			// 
			// frmEditDefaultRig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(411, 304);
			this.Controls.Add(this.dgvDefaultRig);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOk);
			this.Name = "frmEditDefaultRig";
			this.Text = "規定の無線機・アンテナを編集";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmEditDefaultRig_FormClosed);
			this.Load += new System.EventHandler(this.frmEditDefaultRig_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgvDefaultRig)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button cmdOk;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.DataGridView dgvDefaultRig;
	}
}