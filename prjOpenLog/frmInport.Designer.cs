namespace prjOpenLog {
	partial class frmInport {
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
			this.cmdInport = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.dgvInport = new System.Windows.Forms.DataGridView();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.chkExceptDupe = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cboFileType = new System.Windows.Forms.ComboBox();
			this.cmdSetInCSV = new System.Windows.Forms.Button();
			this.txtSetInCSV = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkUseDefault = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cboQSL = new System.Windows.Forms.ComboBox();
			this.txtGL = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.cmdDCode = new System.Windows.Forms.Button();
			this.txtDcode = new System.Windows.Forms.TextBox();
			this.txtPrefix_My = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cmdQTH_My = new System.Windows.Forms.Button();
			this.txtQTH_My = new System.Windows.Forms.TextBox();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvInport)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdInport
			// 
			this.cmdInport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdInport.Location = new System.Drawing.Point(334, 494);
			this.cmdInport.Name = "cmdInport";
			this.cmdInport.Size = new System.Drawing.Size(120, 23);
			this.cmdInport.TabIndex = 22;
			this.cmdInport.Text = "インポート実行";
			this.cmdInport.UseVisualStyleBackColor = true;
			this.cmdInport.Click += new System.EventHandler(this.cmdInport_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.dgvInport);
			this.groupBox3.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.groupBox3.Location = new System.Drawing.Point(8, 242);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(446, 246);
			this.groupBox3.TabIndex = 8;
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
			this.dgvInport.Size = new System.Drawing.Size(434, 219);
			this.dgvInport.TabIndex = 21;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.chkExceptDupe);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.cboFileType);
			this.groupBox2.Controls.Add(this.cmdSetInCSV);
			this.groupBox2.Controls.Add(this.txtSetInCSV);
			this.groupBox2.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.groupBox2.Location = new System.Drawing.Point(14, 123);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(440, 113);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "入力ファイル設定";
			// 
			// chkExceptDupe
			// 
			this.chkExceptDupe.AutoSize = true;
			this.chkExceptDupe.Checked = true;
			this.chkExceptDupe.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkExceptDupe.Location = new System.Drawing.Point(6, 81);
			this.chkExceptDupe.Name = "chkExceptDupe";
			this.chkExceptDupe.Size = new System.Drawing.Size(138, 19);
			this.chkExceptDupe.TabIndex = 14;
			this.chkExceptDupe.Text = "重複を除外する";
			this.chkExceptDupe.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(215, 15);
			this.label1.TabIndex = 3;
			this.label1.Text = "インポートするファイル形式";
			// 
			// cboFileType
			// 
			this.cboFileType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboFileType.FormattingEnabled = true;
			this.cboFileType.Location = new System.Drawing.Point(227, 24);
			this.cboFileType.Name = "cboFileType";
			this.cboFileType.Size = new System.Drawing.Size(207, 23);
			this.cboFileType.TabIndex = 11;
			// 
			// cmdSetInCSV
			// 
			this.cmdSetInCSV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdSetInCSV.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.cmdSetInCSV.Location = new System.Drawing.Point(385, 53);
			this.cmdSetInCSV.Name = "cmdSetInCSV";
			this.cmdSetInCSV.Size = new System.Drawing.Size(49, 23);
			this.cmdSetInCSV.TabIndex = 13;
			this.cmdSetInCSV.Text = "参照";
			this.cmdSetInCSV.UseVisualStyleBackColor = true;
			this.cmdSetInCSV.Click += new System.EventHandler(this.cmdSetInCSV_Click);
			// 
			// txtSetInCSV
			// 
			this.txtSetInCSV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSetInCSV.Location = new System.Drawing.Point(6, 53);
			this.txtSetInCSV.Name = "txtSetInCSV";
			this.txtSetInCSV.Size = new System.Drawing.Size(373, 22);
			this.txtSetInCSV.TabIndex = 12;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.chkUseDefault);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.cboQSL);
			this.groupBox1.Controls.Add(this.txtGL);
			this.groupBox1.Controls.Add(this.label15);
			this.groupBox1.Controls.Add(this.cmdDCode);
			this.groupBox1.Controls.Add(this.txtDcode);
			this.groupBox1.Controls.Add(this.txtPrefix_My);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.cmdQTH_My);
			this.groupBox1.Controls.Add(this.txtQTH_My);
			this.groupBox1.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(440, 105);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "自局情報・QSL交換方法";
			// 
			// chkUseDefault
			// 
			this.chkUseDefault.AutoSize = true;
			this.chkUseDefault.Checked = true;
			this.chkUseDefault.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkUseDefault.Location = new System.Drawing.Point(14, 77);
			this.chkUseDefault.Name = "chkUseDefault";
			this.chkUseDefault.Size = new System.Drawing.Size(378, 19);
			this.chkUseDefault.TabIndex = 264;
			this.chkUseDefault.Text = "バンド毎に登録した無線機・アンテナを使用する";
			this.chkUseDefault.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(272, 52);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(31, 15);
			this.label2.TabIndex = 263;
			this.label2.Text = "QSL";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// cboQSL
			// 
			this.cboQSL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboQSL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboQSL.FormattingEnabled = true;
			this.cboQSL.Location = new System.Drawing.Point(309, 49);
			this.cboQSL.Name = "cboQSL";
			this.cboQSL.Size = new System.Drawing.Size(125, 23);
			this.cboQSL.TabIndex = 7;
			// 
			// txtGL
			// 
			this.txtGL.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.txtGL.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.txtGL.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.txtGL.Location = new System.Drawing.Point(185, 49);
			this.txtGL.Name = "txtGL";
			this.txtGL.Size = new System.Drawing.Size(63, 22);
			this.txtGL.TabIndex = 6;
			this.txtGL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtGL.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtGL_KeyUp);
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label15.Location = new System.Drawing.Point(162, 52);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(23, 15);
			this.label15.TabIndex = 261;
			this.label15.Text = "GL";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// cmdDCode
			// 
			this.cmdDCode.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.cmdDCode.Location = new System.Drawing.Point(11, 48);
			this.cmdDCode.Name = "cmdDCode";
			this.cmdDCode.Size = new System.Drawing.Size(72, 23);
			this.cmdDCode.TabIndex = 4;
			this.cmdDCode.TabStop = false;
			this.cmdDCode.Text = "JCC/JCG";
			this.cmdDCode.UseVisualStyleBackColor = true;
			// 
			// txtDcode
			// 
			this.txtDcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.txtDcode.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.txtDcode.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.txtDcode.Location = new System.Drawing.Point(89, 49);
			this.txtDcode.Name = "txtDcode";
			this.txtDcode.Size = new System.Drawing.Size(63, 22);
			this.txtDcode.TabIndex = 5;
			this.txtDcode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDcode_KeyUp);
			// 
			// txtPrefix_My
			// 
			this.txtPrefix_My.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.txtPrefix_My.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.txtPrefix_My.Location = new System.Drawing.Point(377, 21);
			this.txtPrefix_My.Name = "txtPrefix_My";
			this.txtPrefix_My.Size = new System.Drawing.Size(40, 22);
			this.txtPrefix_My.TabIndex = 3;
			this.txtPrefix_My.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPrefix_My_KeyUp);
			this.txtPrefix_My.Leave += new System.EventHandler(this.txtPrefix_My_Leave);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label3.Location = new System.Drawing.Point(360, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(15, 15);
			this.label3.TabIndex = 256;
			this.label3.Text = "/";
			// 
			// cmdQTH_My
			// 
			this.cmdQTH_My.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.cmdQTH_My.Location = new System.Drawing.Point(11, 20);
			this.cmdQTH_My.Name = "cmdQTH_My";
			this.cmdQTH_My.Size = new System.Drawing.Size(72, 23);
			this.cmdQTH_My.TabIndex = 1;
			this.cmdQTH_My.Text = "QTH";
			this.cmdQTH_My.UseVisualStyleBackColor = true;
			this.cmdQTH_My.Click += new System.EventHandler(this.cmdQTH_My_Click);
			// 
			// txtQTH_My
			// 
			this.txtQTH_My.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtQTH_My.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.txtQTH_My.ImeMode = System.Windows.Forms.ImeMode.On;
			this.txtQTH_My.Location = new System.Drawing.Point(89, 21);
			this.txtQTH_My.Name = "txtQTH_My";
			this.txtQTH_My.Size = new System.Drawing.Size(268, 22);
			this.txtQTH_My.TabIndex = 2;
			this.txtQTH_My.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtQTH_My_KeyUp);
			// 
			// frmInport
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(464, 529);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.cmdInport);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Name = "frmInport";
			this.Text = "OpenLog -Inport";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmInport_FormClosed);
			this.Load += new System.EventHandler(this.frmInport_Load);
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvInport)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button cmdInport;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.DataGridView dgvInport;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button cmdSetInCSV;
		private System.Windows.Forms.TextBox txtSetInCSV;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cboFileType;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button cmdQTH_My;
		private System.Windows.Forms.TextBox txtQTH_My;
		private System.Windows.Forms.TextBox txtGL;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Button cmdDCode;
		private System.Windows.Forms.TextBox txtDcode;
		private System.Windows.Forms.CheckBox chkExceptDupe;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cboQSL;
		private System.Windows.Forms.TextBox txtPrefix_My;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox chkUseDefault;
	}
}