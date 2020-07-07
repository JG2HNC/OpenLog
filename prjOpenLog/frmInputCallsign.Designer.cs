namespace prjOpenLog {
	partial class frmInputCallsign {
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
			this.cmdInput = new System.Windows.Forms.Button();
			this.txtCallsign = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// cmdInput
			// 
			this.cmdInput.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.cmdInput.Location = new System.Drawing.Point(12, 57);
			this.cmdInput.Name = "cmdInput";
			this.cmdInput.Size = new System.Drawing.Size(164, 23);
			this.cmdInput.TabIndex = 0;
			this.cmdInput.Text = "O K";
			this.cmdInput.UseVisualStyleBackColor = true;
			this.cmdInput.Click += new System.EventHandler(this.cmdInput_Click);
			// 
			// txtCallsign
			// 
			this.txtCallsign.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.txtCallsign.Location = new System.Drawing.Point(12, 29);
			this.txtCallsign.Name = "txtCallsign";
			this.txtCallsign.Size = new System.Drawing.Size(164, 22);
			this.txtCallsign.TabIndex = 1;
			this.txtCallsign.Leave += new System.EventHandler(this.txtCallsign_Leave);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(111, 15);
			this.label1.TabIndex = 2;
			this.label1.Text = "Your Callsign";
			// 
			// frmInputCallsign
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(188, 92);
			this.ControlBox = false;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtCallsign);
			this.Controls.Add(this.cmdInput);
			this.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmInputCallsign";
			this.Text = "Input your Callsign";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmInputCallsign_FormClosed);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cmdInput;
		private System.Windows.Forms.TextBox txtCallsign;
		private System.Windows.Forms.Label label1;
	}
}