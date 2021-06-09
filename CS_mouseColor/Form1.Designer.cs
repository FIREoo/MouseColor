namespace CS_mouseColor
{
	partial class Form1
	{
		/// <summary>
		/// 設計工具所需的變數。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清除任何使用中的資源。
		/// </summary>
		/// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 設計工具產生的程式碼

		/// <summary>
		/// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
		/// 這個方法的內容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblCoordX = new System.Windows.Forms.Label();
            this.lblCoordY = new System.Windows.Forms.Label();
            this.lblA = new System.Windows.Forms.Label();
            this.lblR = new System.Windows.Forms.Label();
            this.lblG = new System.Windows.Forms.Label();
            this.lblB = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblH = new System.Windows.Forms.Label();
            this.lblS = new System.Windows.Forms.Label();
            this.lblV = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblCoordX
            // 
            this.lblCoordX.AutoSize = true;
            this.lblCoordX.Location = new System.Drawing.Point(13, 13);
            this.lblCoordX.Name = "lblCoordX";
            this.lblCoordX.Size = new System.Drawing.Size(68, 17);
            this.lblCoordX.TabIndex = 0;
            this.lblCoordX.Text = "lblCoordX";
            // 
            // lblCoordY
            // 
            this.lblCoordY.AutoSize = true;
            this.lblCoordY.Location = new System.Drawing.Point(13, 38);
            this.lblCoordY.Name = "lblCoordY";
            this.lblCoordY.Size = new System.Drawing.Size(68, 17);
            this.lblCoordY.TabIndex = 0;
            this.lblCoordY.Text = "lblCoordY";
            // 
            // lblA
            // 
            this.lblA.AutoSize = true;
            this.lblA.Location = new System.Drawing.Point(160, 13);
            this.lblA.Name = "lblA";
            this.lblA.Size = new System.Drawing.Size(31, 17);
            this.lblA.TabIndex = 0;
            this.lblA.Text = "lblA";
            // 
            // lblR
            // 
            this.lblR.AutoSize = true;
            this.lblR.Location = new System.Drawing.Point(160, 36);
            this.lblR.Name = "lblR";
            this.lblR.Size = new System.Drawing.Size(30, 17);
            this.lblR.TabIndex = 0;
            this.lblR.Text = "lblR";
            // 
            // lblG
            // 
            this.lblG.AutoSize = true;
            this.lblG.Location = new System.Drawing.Point(160, 59);
            this.lblG.Name = "lblG";
            this.lblG.Size = new System.Drawing.Size(32, 17);
            this.lblG.TabIndex = 0;
            this.lblG.Text = "lblG";
            // 
            // lblB
            // 
            this.lblB.AutoSize = true;
            this.lblB.Location = new System.Drawing.Point(160, 82);
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(30, 17);
            this.lblB.TabIndex = 0;
            this.lblB.Text = "lblB";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(223, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(86, 89);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // lblH
            // 
            this.lblH.AutoSize = true;
            this.lblH.Location = new System.Drawing.Point(98, 13);
            this.lblH.Name = "lblH";
            this.lblH.Size = new System.Drawing.Size(32, 17);
            this.lblH.TabIndex = 0;
            this.lblH.Text = "lblH";
            // 
            // lblS
            // 
            this.lblS.AutoSize = true;
            this.lblS.Location = new System.Drawing.Point(98, 36);
            this.lblS.Name = "lblS";
            this.lblS.Size = new System.Drawing.Size(29, 17);
            this.lblS.TabIndex = 0;
            this.lblS.Text = "lblS";
            // 
            // lblV
            // 
            this.lblV.AutoSize = true;
            this.lblV.Location = new System.Drawing.Point(98, 59);
            this.lblV.Name = "lblV";
            this.lblV.Size = new System.Drawing.Size(31, 17);
            this.lblV.TabIndex = 0;
            this.lblV.Text = "lblV";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 111);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblCoordY);
            this.Controls.Add(this.lblV);
            this.Controls.Add(this.lblB);
            this.Controls.Add(this.lblS);
            this.Controls.Add(this.lblG);
            this.Controls.Add(this.lblH);
            this.Controls.Add(this.lblR);
            this.Controls.Add(this.lblA);
            this.Controls.Add(this.lblCoordX);
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Mouse Color";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label lblCoordX;
		private System.Windows.Forms.Label lblCoordY;
		private System.Windows.Forms.Label lblA;
		private System.Windows.Forms.Label lblR;
		private System.Windows.Forms.Label lblG;
		private System.Windows.Forms.Label lblB;
		private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblH;
        private System.Windows.Forms.Label lblS;
        private System.Windows.Forms.Label lblV;
    }
}

