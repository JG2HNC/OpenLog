namespace prjOpenLog {
	partial class frmSearchCallsign {
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.txtCall = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.dgvSearch = new System.Windows.Forms.DataGridView();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdSearch = new System.Windows.Forms.Button();
			this.cmsGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cmsGrid_Received = new System.Windows.Forms.ToolStripMenuItem();
			this.cmsGrid_Sent = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).BeginInit();
			this.cmsGrid.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtCall
			// 
			this.txtCall.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.txtCall.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.txtCall.Location = new System.Drawing.Point(89, 6);
			this.txtCall.Name = "txtCall";
			this.txtCall.Size = new System.Drawing.Size(154, 22);
			this.txtCall.TabIndex = 0;
			this.txtCall.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCall_KeyUp);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 15);
			this.label1.TabIndex = 4;
			this.label1.Text = "Callsign";
			// 
			// dgvSearch
			// 
			this.dgvSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgvSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvSearch.DefaultCellStyle = dataGridViewCellStyle1;
			this.dgvSearch.Location = new System.Drawing.Point(12, 34);
			this.dgvSearch.Name = "dgvSearch";
			this.dgvSearch.ReadOnly = true;
			this.dgvSearch.RowHeadersVisible = false;
			this.dgvSearch.RowTemplate.Height = 21;
			this.dgvSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvSearch.Size = new System.Drawing.Size(561, 292);
			this.dgvSearch.TabIndex = 2;
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Location = new System.Drawing.Point(477, 332);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(96, 27);
			this.cmdOK.TabIndex = 3;
			this.cmdOK.Text = "O K";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdSearch
			// 
			this.cmdSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdSearch.Location = new System.Drawing.Point(249, 6);
			this.cmdSearch.Name = "cmdSearch";
			this.cmdSearch.Size = new System.Drawing.Size(40, 22);
			this.cmdSearch.TabIndex = 1;
			this.cmdSearch.Text = "検索";
			this.cmdSearch.UseVisualStyleBackColor = true;
			this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
			// 
			// cmsGrid
			// 
			this.cmsGrid.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.cmsGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsGrid_Received,
            this.cmsGrid_Sent});
			this.cmsGrid.Name = "cmsGrid";
			this.cmsGrid.Size = new System.Drawing.Size(195, 48);
			// 
			// cmsGrid_Received
			// 
			this.cmsGrid_Received.Name = "cmsGrid_Received";
			this.cmsGrid_Received.Size = new System.Drawing.Size(194, 22);
			this.cmsGrid_Received.Text = "QSL 受領 On/Off";
			this.cmsGrid_Received.Click += new System.EventHandler(this.cmsGrid_Received_Click);
			// 
			// cmsGrid_Sent
			// 
			this.cmsGrid_Sent.Name = "cmsGrid_Sent";
			this.cmsGrid_Sent.Size = new System.Drawing.Size(194, 22);
			this.cmsGrid_Sent.Text = "QSL 発送 On/Off";
			this.cmsGrid_Sent.Click += new System.EventHandler(this.cmsGrid_Sent_Click);
			// 
			// frmSearchCallsign
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(585, 364);
			this.Controls.Add(this.cmdSearch);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.dgvSearch);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtCall);
			this.Name = "frmSearchCallsign";
			this.Text = "OpenLog -コールサイン検索";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSearchCallsign_FormClosed);
			this.Load += new System.EventHandler(this.frmSearchCallsign_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).EndInit();
			this.cmsGrid.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtCall;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridView dgvSearch;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdSearch;
		private System.Windows.Forms.ContextMenuStrip cmsGrid;
		private System.Windows.Forms.ToolStripMenuItem cmsGrid_Received;
		private System.Windows.Forms.ToolStripMenuItem cmsGrid_Sent;
	}
}