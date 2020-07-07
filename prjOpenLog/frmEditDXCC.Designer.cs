namespace prjOpenLog {
	partial class frmEditDXCC {
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dgvDxcc = new System.Windows.Forms.DataGridView();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.CmdOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtPrefix = new System.Windows.Forms.TextBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtPattern = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtCode = new System.Windows.Forms.TextBox();
			this.cmdCreateNew = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgvDxcc)).BeginInit();
			this.SuspendLayout();
			// 
			// dgvDxcc
			// 
			this.dgvDxcc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgvDxcc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvDxcc.DefaultCellStyle = dataGridViewCellStyle1;
			this.dgvDxcc.Location = new System.Drawing.Point(12, 122);
			this.dgvDxcc.MultiSelect = false;
			this.dgvDxcc.Name = "dgvDxcc";
			this.dgvDxcc.ReadOnly = true;
			this.dgvDxcc.RowTemplate.Height = 21;
			this.dgvDxcc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvDxcc.Size = new System.Drawing.Size(447, 198);
			this.dgvDxcc.TabIndex = 0;
			this.dgvDxcc.SelectionChanged += new System.EventHandler(this.dgvDxcc_SelectionChanged);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Location = new System.Drawing.Point(320, 326);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(43, 23);
			this.cmdCancel.TabIndex = 5;
			this.cmdCancel.Text = "取消";
			this.cmdCancel.UseVisualStyleBackColor = true;
			// 
			// CmdOK
			// 
			this.CmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CmdOK.Location = new System.Drawing.Point(369, 325);
			this.CmdOK.Name = "CmdOK";
			this.CmdOK.Size = new System.Drawing.Size(90, 23);
			this.CmdOK.TabIndex = 4;
			this.CmdOK.Text = "変更を反映";
			this.CmdOK.UseVisualStyleBackColor = true;
			this.CmdOK.Click += new System.EventHandler(this.CmdOK_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 15);
			this.label1.TabIndex = 6;
			this.label1.Text = "Prefix";
			// 
			// txtPrefix
			// 
			this.txtPrefix.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.txtPrefix.Location = new System.Drawing.Point(79, 6);
			this.txtPrefix.Name = "txtPrefix";
			this.txtPrefix.Size = new System.Drawing.Size(68, 22);
			this.txtPrefix.TabIndex = 10;
			this.txtPrefix.Leave += new System.EventHandler(this.txtPrefix_Leave);
			// 
			// txtName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtName.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.txtName.Location = new System.Drawing.Point(79, 34);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(380, 22);
			this.txtName.TabIndex = 12;
			this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(12, 37);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(39, 15);
			this.label2.TabIndex = 11;
			this.label2.Text = "Name";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label3.Location = new System.Drawing.Point(12, 65);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(63, 15);
			this.label3.TabIndex = 13;
			this.label3.Text = "Pattern";
			// 
			// txtPattern
			// 
			this.txtPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPattern.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.txtPattern.Location = new System.Drawing.Point(79, 62);
			this.txtPattern.Name = "txtPattern";
			this.txtPattern.Size = new System.Drawing.Size(380, 22);
			this.txtPattern.TabIndex = 14;
			this.txtPattern.Leave += new System.EventHandler(this.txtPattern_Leave);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label4.Location = new System.Drawing.Point(12, 97);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(39, 15);
			this.label4.TabIndex = 15;
			this.label4.Text = "Code";
			// 
			// txtCode
			// 
			this.txtCode.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.txtCode.Location = new System.Drawing.Point(79, 94);
			this.txtCode.Name = "txtCode";
			this.txtCode.Size = new System.Drawing.Size(68, 22);
			this.txtCode.TabIndex = 16;
			this.txtCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtCode.Leave += new System.EventHandler(this.txtCode_Leave);
			// 
			// cmdCreateNew
			// 
			this.cmdCreateNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCreateNew.Location = new System.Drawing.Point(320, 7);
			this.cmdCreateNew.Name = "cmdCreateNew";
			this.cmdCreateNew.Size = new System.Drawing.Size(139, 23);
			this.cmdCreateNew.TabIndex = 17;
			this.cmdCreateNew.Text = "新規Entityを作成する";
			this.cmdCreateNew.UseVisualStyleBackColor = true;
			this.cmdCreateNew.Click += new System.EventHandler(this.cmdCreateNew_Click);
			// 
			// frmEditDXCC
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(471, 360);
			this.Controls.Add(this.cmdCreateNew);
			this.Controls.Add(this.txtCode);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtPattern);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtPrefix);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.CmdOK);
			this.Controls.Add(this.dgvDxcc);
			this.Name = "frmEditDXCC";
			this.Text = "DXCCエンティティリストを編集";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmEditDXCC_FormClosed);
			this.Load += new System.EventHandler(this.frmEditDXCC_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgvDxcc)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dgvDxcc;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button CmdOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtPrefix;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtPattern;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtCode;
		private System.Windows.Forms.Button cmdCreateNew;
	}
}