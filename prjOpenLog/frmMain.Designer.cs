namespace prjOpenLog {
	partial class frmMain {
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.mnsMain = new System.Windows.Forms.MenuStrip();
			this.mnuMain_File = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuAddNewQSO = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSaveDB = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuFilePrintCard = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFilePrintStation = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_Search = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSearchCallsign = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_Tools = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_ToolsInportLogcs = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_ToolsInport = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuMain_ToolsSortDT = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuMain_ToolsBackup = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuMain_ToolsEditBands = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_ToolsEditCity = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_ToolsEditDxcc = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_ToolsEditRigAnt = new System.Windows.Forms.ToolStripMenuItem();
			this.dgvMain = new System.Windows.Forms.DataGridView();
			this.stsMain = new System.Windows.Forms.StatusStrip();
			this.stlQSL = new System.Windows.Forms.ToolStripStatusLabel();
			this.cmsGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cmsGrid_Received = new System.Windows.Forms.ToolStripMenuItem();
			this.cmsGrid_Sent = new System.Windows.Forms.ToolStripMenuItem();
			this.cmsGrid_Remove = new System.Windows.Forms.ToolStripMenuItem();
			this.cmsGrid_Print = new System.Windows.Forms.ToolStripMenuItem();
			this.mnsMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
			this.stsMain.SuspendLayout();
			this.cmsGrid.SuspendLayout();
			this.SuspendLayout();
			// 
			// mnsMain
			// 
			this.mnsMain.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.mnsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMain_File,
            this.mnuMain_Search,
            this.mnuMain_Tools});
			this.mnsMain.Location = new System.Drawing.Point(0, 0);
			this.mnsMain.Name = "mnsMain";
			this.mnsMain.Size = new System.Drawing.Size(784, 25);
			this.mnsMain.TabIndex = 0;
			this.mnsMain.Text = "menuStrip1";
			// 
			// mnuMain_File
			// 
			this.mnuMain_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddNewQSO,
            this.mnuSaveDB,
            this.toolStripSeparator3,
            this.mnuFilePrintCard,
            this.mnuFilePrintStation});
			this.mnuMain_File.Name = "mnuMain_File";
			this.mnuMain_File.Size = new System.Drawing.Size(57, 21);
			this.mnuMain_File.Text = "ファイル";
			// 
			// mnuAddNewQSO
			// 
			this.mnuAddNewQSO.Name = "mnuAddNewQSO";
			this.mnuAddNewQSO.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.mnuAddNewQSO.Size = new System.Drawing.Size(248, 22);
			this.mnuAddNewQSO.Text = "新規追加(交信)";
			this.mnuAddNewQSO.Click += new System.EventHandler(this.mnuAddNewQSO_Click);
			// 
			// mnuSaveDB
			// 
			this.mnuSaveDB.Name = "mnuSaveDB";
			this.mnuSaveDB.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.mnuSaveDB.Size = new System.Drawing.Size(248, 22);
			this.mnuSaveDB.Text = "保存(データベースファイル)";
			this.mnuSaveDB.Click += new System.EventHandler(this.mnuSaveDB_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(245, 6);
			// 
			// mnuFilePrintCard
			// 
			this.mnuFilePrintCard.Name = "mnuFilePrintCard";
			this.mnuFilePrintCard.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.mnuFilePrintCard.Size = new System.Drawing.Size(248, 22);
			this.mnuFilePrintCard.Text = "カード印刷";
			this.mnuFilePrintCard.Click += new System.EventHandler(this.mnuFilePrintCard_Click);
			// 
			// mnuFilePrintStation
			// 
			this.mnuFilePrintStation.Name = "mnuFilePrintStation";
			this.mnuFilePrintStation.Size = new System.Drawing.Size(248, 22);
			this.mnuFilePrintStation.Text = "印刷対象局一覧";
			this.mnuFilePrintStation.Click += new System.EventHandler(this.mnuFilePrintStation_Click);
			// 
			// mnuMain_Search
			// 
			this.mnuMain_Search.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSearchCallsign});
			this.mnuMain_Search.Name = "mnuMain_Search";
			this.mnuMain_Search.Size = new System.Drawing.Size(46, 21);
			this.mnuMain_Search.Text = "検索";
			// 
			// mnuSearchCallsign
			// 
			this.mnuSearchCallsign.Name = "mnuSearchCallsign";
			this.mnuSearchCallsign.ShortcutKeys = System.Windows.Forms.Keys.F3;
			this.mnuSearchCallsign.Size = new System.Drawing.Size(181, 22);
			this.mnuSearchCallsign.Text = "コールサイン検索";
			this.mnuSearchCallsign.Click += new System.EventHandler(this.mnuSearchCallsign_Click);
			// 
			// mnuMain_Tools
			// 
			this.mnuMain_Tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMain_ToolsInportLogcs,
            this.mnuMain_ToolsInport,
            this.toolStripSeparator1,
            this.mnuMain_ToolsSortDT,
            this.toolStripSeparator2,
            this.mnuMain_ToolsBackup,
            this.toolStripSeparator4,
            this.mnuMain_ToolsEditBands,
            this.mnuMain_ToolsEditCity,
            this.mnuMain_ToolsEditDxcc,
            this.mnuMain_ToolsEditRigAnt});
			this.mnuMain_Tools.Name = "mnuMain_Tools";
			this.mnuMain_Tools.Size = new System.Drawing.Size(49, 21);
			this.mnuMain_Tools.Text = "ツール";
			// 
			// mnuMain_ToolsInportLogcs
			// 
			this.mnuMain_ToolsInportLogcs.Name = "mnuMain_ToolsInportLogcs";
			this.mnuMain_ToolsInportLogcs.Size = new System.Drawing.Size(229, 22);
			this.mnuMain_ToolsInportLogcs.Text = "インポート(Logcs形式CSV)";
			this.mnuMain_ToolsInportLogcs.Click += new System.EventHandler(this.mnuMain_ToolsInportLogcs_Click);
			// 
			// mnuMain_ToolsInport
			// 
			this.mnuMain_ToolsInport.Name = "mnuMain_ToolsInport";
			this.mnuMain_ToolsInport.Size = new System.Drawing.Size(229, 22);
			this.mnuMain_ToolsInport.Text = "インポート(WSJT-X)";
			this.mnuMain_ToolsInport.Click += new System.EventHandler(this.mnuMain_ToolsInport_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(226, 6);
			// 
			// mnuMain_ToolsSortDT
			// 
			this.mnuMain_ToolsSortDT.Name = "mnuMain_ToolsSortDT";
			this.mnuMain_ToolsSortDT.Size = new System.Drawing.Size(229, 22);
			this.mnuMain_ToolsSortDT.Text = "ソート(交信日時順)";
			this.mnuMain_ToolsSortDT.Click += new System.EventHandler(this.mnuMain_ToolsSortDT_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(226, 6);
			// 
			// mnuMain_ToolsBackup
			// 
			this.mnuMain_ToolsBackup.Name = "mnuMain_ToolsBackup";
			this.mnuMain_ToolsBackup.Size = new System.Drawing.Size(229, 22);
			this.mnuMain_ToolsBackup.Text = "バックアップを作成";
			this.mnuMain_ToolsBackup.Click += new System.EventHandler(this.mnuMain_ToolsBackup_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(226, 6);
			// 
			// mnuMain_ToolsEditBands
			// 
			this.mnuMain_ToolsEditBands.Name = "mnuMain_ToolsEditBands";
			this.mnuMain_ToolsEditBands.Size = new System.Drawing.Size(229, 22);
			this.mnuMain_ToolsEditBands.Text = "周波数帯を編集";
			this.mnuMain_ToolsEditBands.Click += new System.EventHandler(this.mnuMain_ToolsEditBands_Click);
			// 
			// mnuMain_ToolsEditCity
			// 
			this.mnuMain_ToolsEditCity.Name = "mnuMain_ToolsEditCity";
			this.mnuMain_ToolsEditCity.Size = new System.Drawing.Size(229, 22);
			this.mnuMain_ToolsEditCity.Text = "市区町村リスト編集";
			this.mnuMain_ToolsEditCity.Click += new System.EventHandler(this.mnuMain_ToolsEditCity_Click);
			// 
			// mnuMain_ToolsEditDxcc
			// 
			this.mnuMain_ToolsEditDxcc.Name = "mnuMain_ToolsEditDxcc";
			this.mnuMain_ToolsEditDxcc.Size = new System.Drawing.Size(229, 22);
			this.mnuMain_ToolsEditDxcc.Text = "DXCCエンティティリストを編集";
			this.mnuMain_ToolsEditDxcc.Click += new System.EventHandler(this.mnuMain_ToolsEditDxcc_Click);
			// 
			// mnuMain_ToolsEditRigAnt
			// 
			this.mnuMain_ToolsEditRigAnt.Name = "mnuMain_ToolsEditRigAnt";
			this.mnuMain_ToolsEditRigAnt.Size = new System.Drawing.Size(229, 22);
			this.mnuMain_ToolsEditRigAnt.Text = "無線機・アンテナ登録";
			this.mnuMain_ToolsEditRigAnt.Click += new System.EventHandler(this.mnuMain_ToolsEditRigAnt_Click);
			// 
			// dgvMain
			// 
			this.dgvMain.AllowUserToAddRows = false;
			this.dgvMain.AllowUserToDeleteRows = false;
			this.dgvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvMain.DefaultCellStyle = dataGridViewCellStyle1;
			this.dgvMain.Location = new System.Drawing.Point(12, 27);
			this.dgvMain.MultiSelect = false;
			this.dgvMain.Name = "dgvMain";
			this.dgvMain.ReadOnly = true;
			this.dgvMain.RowTemplate.Height = 21;
			this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvMain.Size = new System.Drawing.Size(760, 350);
			this.dgvMain.TabIndex = 1;
			this.dgvMain.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMain_CellDoubleClick);
			this.dgvMain.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMain_CellValueChanged);
			// 
			// stsMain
			// 
			this.stsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stlQSL});
			this.stsMain.Location = new System.Drawing.Point(0, 389);
			this.stsMain.Name = "stsMain";
			this.stsMain.Size = new System.Drawing.Size(784, 22);
			this.stsMain.TabIndex = 2;
			this.stsMain.Text = "statusStrip1";
			// 
			// stlQSL
			// 
			this.stlQSL.Name = "stlQSL";
			this.stlQSL.Size = new System.Drawing.Size(89, 17);
			this.stlQSL.Text = "カード未発行:0件";
			// 
			// cmsGrid
			// 
			this.cmsGrid.Font = new System.Drawing.Font("Yu Gothic UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.cmsGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsGrid_Received,
            this.cmsGrid_Sent,
            this.cmsGrid_Remove,
            this.cmsGrid_Print});
			this.cmsGrid.Name = "cmsGrid";
			this.cmsGrid.Size = new System.Drawing.Size(199, 100);
			// 
			// cmsGrid_Received
			// 
			this.cmsGrid_Received.Name = "cmsGrid_Received";
			this.cmsGrid_Received.Size = new System.Drawing.Size(198, 24);
			this.cmsGrid_Received.Text = "QSL 受領 On/Off";
			this.cmsGrid_Received.Click += new System.EventHandler(this.cmsGrid_Received_Click);
			// 
			// cmsGrid_Sent
			// 
			this.cmsGrid_Sent.Name = "cmsGrid_Sent";
			this.cmsGrid_Sent.Size = new System.Drawing.Size(198, 24);
			this.cmsGrid_Sent.Text = "QSL 発送 On/Off";
			this.cmsGrid_Sent.Click += new System.EventHandler(this.cmsGrid_Sent_Click);
			// 
			// cmsGrid_Remove
			// 
			this.cmsGrid_Remove.Name = "cmsGrid_Remove";
			this.cmsGrid_Remove.Size = new System.Drawing.Size(198, 24);
			this.cmsGrid_Remove.Text = "レコード削除";
			this.cmsGrid_Remove.Click += new System.EventHandler(this.cmsGrid_Remove_Click);
			// 
			// cmsGrid_Print
			// 
			this.cmsGrid_Print.Name = "cmsGrid_Print";
			this.cmsGrid_Print.Size = new System.Drawing.Size(198, 24);
			this.cmsGrid_Print.Text = "QSL 印刷(1レコード)";
			this.cmsGrid_Print.Click += new System.EventHandler(this.cmsGrid_Print_Click);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 411);
			this.Controls.Add(this.stsMain);
			this.Controls.Add(this.dgvMain);
			this.Controls.Add(this.mnsMain);
			this.MainMenuStrip = this.mnsMain;
			this.Name = "frmMain";
			this.Text = "OpenLog";
			this.Activated += new System.EventHandler(this.frmMain_Activated);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.mnsMain.ResumeLayout(false);
			this.mnsMain.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
			this.stsMain.ResumeLayout(false);
			this.stsMain.PerformLayout();
			this.cmsGrid.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip mnsMain;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_File;
		private System.Windows.Forms.DataGridView dgvMain;
		private System.Windows.Forms.StatusStrip stsMain;
		private System.Windows.Forms.ToolStripMenuItem mnuAddNewQSO;
		private System.Windows.Forms.ToolStripMenuItem mnuSaveDB;
		private System.Windows.Forms.ContextMenuStrip cmsGrid;
		private System.Windows.Forms.ToolStripMenuItem cmsGrid_Received;
		private System.Windows.Forms.ToolStripMenuItem cmsGrid_Sent;
		private System.Windows.Forms.ToolStripMenuItem cmsGrid_Remove;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_Search;
		private System.Windows.Forms.ToolStripMenuItem mnuSearchCallsign;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_Tools;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_ToolsInportLogcs;
		private System.Windows.Forms.ToolStripStatusLabel stlQSL;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_ToolsInport;
		private System.Windows.Forms.ToolStripMenuItem mnuFilePrintCard;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_ToolsSortDT;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_ToolsBackup;
		private System.Windows.Forms.ToolStripMenuItem cmsGrid_Print;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem mnuFilePrintStation;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_ToolsEditBands;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_ToolsEditDxcc;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_ToolsEditRigAnt;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_ToolsEditCity;
	}
}

