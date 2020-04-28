namespace ActMon.Forms
{
    partial class ApplicationView
    {
        /// <summary> 
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione componenti

        /// <summary> 
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lbApplicationName = new System.Windows.Forms.Label();
            this.pbApplicationUsage = new System.Windows.Forms.ProgressBar();
            this.lbAppUsage = new System.Windows.Forms.Label();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.pbIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lbApplicationName
            // 
            this.lbApplicationName.AutoSize = true;
            this.lbApplicationName.Location = new System.Drawing.Point(53, 3);
            this.lbApplicationName.Name = "lbApplicationName";
            this.lbApplicationName.Size = new System.Drawing.Size(0, 13);
            this.lbApplicationName.TabIndex = 1;
            this.lbApplicationName.Click += new System.EventHandler(this.lbApplicationName_Click);
            // 
            // pbApplicationUsage
            // 
            this.pbApplicationUsage.Location = new System.Drawing.Point(56, 22);
            this.pbApplicationUsage.Name = "pbApplicationUsage";
            this.pbApplicationUsage.Size = new System.Drawing.Size(244, 13);
            this.pbApplicationUsage.TabIndex = 2;
            // 
            // lbAppUsage
            // 
            this.lbAppUsage.Location = new System.Drawing.Point(306, 22);
            this.lbAppUsage.Name = "lbAppUsage";
            this.lbAppUsage.Size = new System.Drawing.Size(92, 13);
            this.lbAppUsage.TabIndex = 3;
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Interval = 1000;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // pbIcon
            // 
            this.pbIcon.Location = new System.Drawing.Point(8, 3);
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.Size = new System.Drawing.Size(39, 39);
            this.pbIcon.TabIndex = 0;
            this.pbIcon.TabStop = false;
            // 
            // ApplicationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbAppUsage);
            this.Controls.Add(this.pbApplicationUsage);
            this.Controls.Add(this.lbApplicationName);
            this.Controls.Add(this.pbIcon);
            this.Name = "ApplicationView";
            this.Size = new System.Drawing.Size(384, 50);
            this.Load += new System.EventHandler(this.ApplicationView_Load);
            this.Resize += new System.EventHandler(this.ApplicationView_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbApplicationName;
        private System.Windows.Forms.ProgressBar pbApplicationUsage;
        private System.Windows.Forms.Label lbAppUsage;
        private System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.PictureBox pbIcon;
    }
}
