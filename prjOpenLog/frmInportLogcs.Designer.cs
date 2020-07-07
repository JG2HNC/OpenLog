namespace prjOpenLog {
	partial class frmInportLogcs {
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
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.dgvInport = new System.Windows.Forms.DataGridView();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.cmdSetInCSV = new System.Windows.Forms.Button();
			this.txtSetInCSV = new System.Windows.Forms.TextBox();
			this.cmdInport = new System.Windows.Forms.Button();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvInport)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.dgvInport);
			this.groupBox3.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.groupBox3.Location = new System.Drawing.Point(12, 72);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(417, 219);
			this.groupBox3.TabIndex = 5;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Preview";
			// 
			// dgvInport
			// 
			this.dgvInport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgvInport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvInport.Location = new System.Drawing.Point(6, 21);
			this.dgvInport.Name = "dgvInport";
			this.dgvInport.RowTemplate.Height = 21;
			this.dgvInport.Size = new System.Drawing.Size(405, 192);
			this.dgvInport.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.cmdSetInCSV);
			this.groupBox2.Controls.Add(this.txtSetInCSV);
			this.groupBox2.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.groupBox2.Location = new System.Drawing.Point(12, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(417, 54);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Logcsから出力したCSVファイルを設定";
			// 
			// cmdSetInCSV
			// 
			this.cmdSetInCSV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdSetInCSV.Font = new System.Drawing.Font("游ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.cmdSetInCSV.Location = new System.Drawing.Point(362, 21);
			this.cmdSetInCSV.Name = "cmdSetInCSV";
			this.cmdSetInCSV.Size = new System.Drawing.Size(49, 23);
			this.cmdSetInCSV.TabIndex = 1;
			this.cmdSetInCSV.Text = "参照";
			this.cmdSetInCSV.UseVisualStyleBackColor = true;
			this.cmdSetInCSV.Click += new System.EventHandler(this.cmdSetInCSV_Click);
			// 
			// txtSetInCSV
			// 
			this.txtSetInCSV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSetInCSV.Location = new System.Drawing.Point(6, 21);
			this.txtSetInCSV.Name = "txtSetInCSV";
			this.txtSetInCSV.Size = new System.Drawing.Size(350, 22);
			this.txtSetInCSV.TabIndex = 0;
			// 
			// cmdInport
			// 
			this.cmdInport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdInport.Location = new System.Drawing.Point(309, 297);
			this.cmdInport.Name = "cmdInport";
			this.cmdInport.Size = new System.Drawing.Size(120, 23);
			this.cmdInport.TabIndex = 6;
			this.cmdInport.Text = "インポート実行";
			this.cmdInport.UseVisualStyleBackColor = true;
			this.cmdInport.Click += new System.EventHandler(this.cmdInport_Click);
			// 
			// frmInportLogcs
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(441, 332);
			this.Controls.Add(this.cmdInport);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Name = "frmInportLogcs";
			this.Text = "OpenLog -Inport(Logcs)";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmInportLogcs_FormClosed);
			this.Load += new System.EventHandler(this.frmInportLogcs_Load);
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvInport)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.DataGridView dgvInport;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button cmdSetInCSV;
		private System.Windows.Forms.TextBox txtSetInCSV;
		private System.Windows.Forms.Button cmdInport;
	}
}