namespace ComposerAdmin.Forms
{
    partial class FormAbout
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelProductName = new System.Windows.Forms.Label();
            this.labelProductTitle = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelCompanyName = new System.Windows.Forms.Label();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.pbProjectIcon = new System.Windows.Forms.PictureBox();
            this.textBoxDescription1 = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.linkBuyMeABeer = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbProjectIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.labelProductName);
            this.panel1.Controls.Add(this.labelProductTitle);
            this.panel1.Controls.Add(this.labelVersion);
            this.panel1.Controls.Add(this.labelCompanyName);
            this.panel1.Controls.Add(this.labelCopyright);
            this.panel1.Controls.Add(this.pbProjectIcon);
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(389, 112);
            this.panel1.TabIndex = 1;
            // 
            // labelProductName
            // 
            this.labelProductName.AutoSize = true;
            this.labelProductName.Location = new System.Drawing.Point(128, 28);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(67, 13);
            this.labelProductName.TabIndex = 1;
            this.labelProductName.Text = "Product Title";
            // 
            // labelProductTitle
            // 
            this.labelProductTitle.AutoSize = true;
            this.labelProductTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProductTitle.Location = new System.Drawing.Point(128, 9);
            this.labelProductTitle.Name = "labelProductTitle";
            this.labelProductTitle.Size = new System.Drawing.Size(80, 13);
            this.labelProductTitle.TabIndex = 0;
            this.labelProductTitle.Text = "Product Title";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(128, 47);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(51, 13);
            this.labelVersion.TabIndex = 2;
            this.labelVersion.Text = "Copyright";
            // 
            // labelCompanyName
            // 
            this.labelCompanyName.AutoSize = true;
            this.labelCompanyName.Location = new System.Drawing.Point(128, 85);
            this.labelCompanyName.Name = "labelCompanyName";
            this.labelCompanyName.Size = new System.Drawing.Size(82, 13);
            this.labelCompanyName.TabIndex = 4;
            this.labelCompanyName.Text = "Company Name";
            // 
            // labelCopyright
            // 
            this.labelCopyright.AutoSize = true;
            this.labelCopyright.Location = new System.Drawing.Point(128, 66);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(51, 13);
            this.labelCopyright.TabIndex = 3;
            this.labelCopyright.Text = "Copyright";
            // 
            // pbProjectIcon
            // 
            this.pbProjectIcon.Image = global::ActMon.Properties.Resources.clock;
            this.pbProjectIcon.Location = new System.Drawing.Point(10, 9);
            this.pbProjectIcon.Name = "pbProjectIcon";
            this.pbProjectIcon.Size = new System.Drawing.Size(92, 89);
            this.pbProjectIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbProjectIcon.TabIndex = 0;
            this.pbProjectIcon.TabStop = false;
            // 
            // textBoxDescription1
            // 
            this.textBoxDescription1.Location = new System.Drawing.Point(9, 118);
            this.textBoxDescription1.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.textBoxDescription1.Multiline = true;
            this.textBoxDescription1.Name = "textBoxDescription1";
            this.textBoxDescription1.ReadOnly = true;
            this.textBoxDescription1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxDescription1.Size = new System.Drawing.Size(378, 111);
            this.textBoxDescription1.TabIndex = 2;
            this.textBoxDescription1.TabStop = false;
            this.textBoxDescription1.Text = "Descrizione";
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Location = new System.Drawing.Point(315, 240);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // linkBuyMeABeer
            // 
            this.linkBuyMeABeer.AutoSize = true;
            this.linkBuyMeABeer.Location = new System.Drawing.Point(6, 245);
            this.linkBuyMeABeer.Name = "linkBuyMeABeer";
            this.linkBuyMeABeer.Size = new System.Drawing.Size(222, 13);
            this.linkBuyMeABeer.TabIndex = 1;
            this.linkBuyMeABeer.TabStop = true;
            this.linkBuyMeABeer.Text = "Like this Software? You can buy me a coffee!";
            this.linkBuyMeABeer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkBuyMeABeer_LinkClicked);
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 272);
            this.Controls.Add(this.linkBuyMeABeer);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.textBoxDescription1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormAbout";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbProjectIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbProjectIcon;
        private System.Windows.Forms.Label labelCompanyName;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.TextBox textBoxDescription1;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelProductTitle;
        private System.Windows.Forms.LinkLabel linkBuyMeABeer;
    }
}
