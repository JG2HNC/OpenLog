namespace prjOpenLog {
	partial class frmEditBand {
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
			this.txtBandF = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtUpper = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtBandL = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtLower = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.CmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.dgvBand = new System.Windows.Forms.DataGridView();
			this.cmdAddNew = new System.Windows.Forms.Button();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvBand)).BeginInit();
			this.SuspendLayout();
			// 
			// txtBandF
			// 
			this.txtBandF.Location = new System.Drawing.Point(6, 36);
			this.txtBandF.Name = "txtBandF";
			this.txtBandF.Size = new System.Drawing.Size(106, 19);
			this.txtBandF.TabIndex = 0;
			this.txtBandF.Text = "430MHz";
			this.txtBandF.Leave += new System.EventHandler(this.txtBandF_Leave);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(121, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "周波数帯(周波数表記)";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(136, 63);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "上限周波数";
			// 
			// txtUpper
			// 
			this.txtUpper.Location = new System.Drawing.Point(136, 78);
			this.txtUpper.Name = "txtUpper";
			this.txtUpper.Size = new System.Drawing.Size(80, 19);
			this.txtUpper.TabIndex = 3;
			this.txtUpper.Text = "439.999";
			this.txtUpper.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtUpper.Leave += new System.EventHandler(this.txtUpper_Leave);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.txtBandL);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.txtLower);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.txtUpper);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.txtBandF);
			this.groupBox2.Location = new System.Drawing.Point(12, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(268, 105);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "編集対象の周波数帯";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(134, 21);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(61, 12);
			this.label6.TabIndex = 9;
			this.label6.Text = "(波長表記)";
			// 
			// txtBandL
			// 
			this.txtBandL.Location = new System.Drawing.Point(131, 36);
			this.txtBandL.Name = "txtBandL";
			this.txtBandL.Size = new System.Drawing.Size(106, 19);
			this.txtBandL.TabIndex = 1;
			this.txtBandL.Text = "430MHz";
			this.txtBandL.Leave += new System.EventHandler(this.txtBandL_Leave);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(92, 85);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(27, 12);
			this.label4.TabIndex = 7;
			this.label4.Text = "MHz";
			// 
			// txtLower
			// 
			this.txtLower.Location = new System.Drawing.Point(6, 78);
			this.txtLower.Name = "txtLower";
			this.txtLower.Size = new System.Drawing.Size(80, 19);
			this.txtLower.TabIndex = 2;
			this.txtLower.Text = "430.000";
			this.txtLower.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtLower.Leave += new System.EventHandler(this.txtLower_Leave);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 63);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(65, 12);
			this.label5.TabIndex = 5;
			this.label5.Text = "下限周波数";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(222, 85);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(27, 12);
			this.label3.TabIndex = 4;
			this.label3.Text = "MHz";
			// 
			// CmdOK
			// 
			this.CmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CmdOK.Location = new System.Drawing.Point(249, 251);
			this.CmdOK.Name = "CmdOK";
			this.CmdOK.Size = new System.Drawing.Size(90, 23);
			this.CmdOK.TabIndex = 7;
			this.CmdOK.Text = "変更を反映";
			this.CmdOK.UseVisualStyleBackColor = true;
			this.CmdOK.Click += new System.EventHandler(this.CmdOK_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Location = new System.Drawing.Point(200, 252);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(43, 23);
			this.cmdCancel.TabIndex = 6;
			this.cmdCancel.Text = "取消";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// dgvBand
			// 
			this.dgvBand.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgvBand.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvBand.Location = new System.Drawing.Point(12, 123);
			this.dgvBand.Name = "dgvBand";
			this.dgvBand.ReadOnly = true;
			this.dgvBand.RowTemplate.Height = 21;
			this.dgvBand.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvBand.Size = new System.Drawing.Size(327, 122);
			this.dgvBand.TabIndex = 5;
			this.dgvBand.SelectionChanged += new System.EventHandler(this.dgvBand_SelectionChanged);
			// 
			// cmdAddNew
			// 
			this.cmdAddNew.Location = new System.Drawing.Point(296, 88);
			this.cmdAddNew.Name = "cmdAddNew";
			this.cmdAddNew.Size = new System.Drawing.Size(43, 23);
			this.cmdAddNew.TabIndex = 4;
			this.cmdAddNew.Text = "新規";
			this.cmdAddNew.UseVisualStyleBackColor = true;
			this.cmdAddNew.Click += new System.EventHandler(this.cmdAddNew_Click);
			// 
			// frmEditBand
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(351, 286);
			this.Controls.Add(this.cmdAddNew);
			this.Controls.Add(this.dgvBand);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.CmdOK);
			this.Controls.Add(this.groupBox2);
			this.Name = "frmEditBand";
			this.Text = "周波数帯を編集";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmEditBand_FormClosed);
			this.Load += new System.EventHandler(this.frmEditBand_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvBand)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TextBox txtBandF;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtUpper;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtLower;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button CmdOK;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtBandL;
		private System.Windows.Forms.DataGridView dgvBand;
		private System.Windows.Forms.Button cmdAddNew;
	}
}