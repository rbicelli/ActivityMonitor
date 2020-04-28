namespace ActMon.Forms
{
    partial class FormSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.gBDatabase = new System.Windows.Forms.GroupBox();
            this.textDBPassword = new System.Windows.Forms.TextBox();
            this.lbDBPassword = new System.Windows.Forms.Label();
            this.textDBUsername = new System.Windows.Forms.TextBox();
            this.lbDBUsername = new System.Windows.Forms.Label();
            this.textDBDatabase = new System.Windows.Forms.TextBox();
            this.lbDBDatabase = new System.Windows.Forms.Label();
            this.textDBServer = new System.Windows.Forms.TextBox();
            this.lbDBServer = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gBGeneral = new System.Windows.Forms.GroupBox();
            this.chkAutostart = new System.Windows.Forms.CheckBox();
            this.gBDatabase.SuspendLayout();
            this.gBGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // gBDatabase
            // 
            this.gBDatabase.Controls.Add(this.textDBPassword);
            this.gBDatabase.Controls.Add(this.lbDBPassword);
            this.gBDatabase.Controls.Add(this.textDBUsername);
            this.gBDatabase.Controls.Add(this.lbDBUsername);
            this.gBDatabase.Controls.Add(this.textDBDatabase);
            this.gBDatabase.Controls.Add(this.lbDBDatabase);
            this.gBDatabase.Controls.Add(this.textDBServer);
            this.gBDatabase.Controls.Add(this.lbDBServer);
            resources.ApplyResources(this.gBDatabase, "gBDatabase");
            this.gBDatabase.Name = "gBDatabase";
            this.gBDatabase.TabStop = false;
            // 
            // textDBPassword
            // 
            resources.ApplyResources(this.textDBPassword, "textDBPassword");
            this.textDBPassword.Name = "textDBPassword";
            this.textDBPassword.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // lbDBPassword
            // 
            resources.ApplyResources(this.lbDBPassword, "lbDBPassword");
            this.lbDBPassword.Name = "lbDBPassword";
            // 
            // textDBUsername
            // 
            resources.ApplyResources(this.textDBUsername, "textDBUsername");
            this.textDBUsername.Name = "textDBUsername";
            // 
            // lbDBUsername
            // 
            resources.ApplyResources(this.lbDBUsername, "lbDBUsername");
            this.lbDBUsername.Name = "lbDBUsername";
            // 
            // textDBDatabase
            // 
            resources.ApplyResources(this.textDBDatabase, "textDBDatabase");
            this.textDBDatabase.Name = "textDBDatabase";
            // 
            // lbDBDatabase
            // 
            resources.ApplyResources(this.lbDBDatabase, "lbDBDatabase");
            this.lbDBDatabase.Name = "lbDBDatabase";
            this.lbDBDatabase.Click += new System.EventHandler(this.lbDBDatabase_Click);
            // 
            // textDBServer
            // 
            resources.ApplyResources(this.textDBServer, "textDBServer");
            this.textDBServer.Name = "textDBServer";
            // 
            // lbDBServer
            // 
            resources.ApplyResources(this.lbDBServer, "lbDBServer");
            this.lbDBServer.Name = "lbDBServer";
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gBGeneral
            // 
            this.gBGeneral.Controls.Add(this.chkAutostart);
            resources.ApplyResources(this.gBGeneral, "gBGeneral");
            this.gBGeneral.Name = "gBGeneral";
            this.gBGeneral.TabStop = false;
            // 
            // chkAutostart
            // 
            resources.ApplyResources(this.chkAutostart, "chkAutostart");
            this.chkAutostart.Name = "chkAutostart";
            this.chkAutostart.UseVisualStyleBackColor = true;
            // 
            // FormSettings
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.gBGeneral);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gBDatabase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.gBDatabase.ResumeLayout(false);
            this.gBDatabase.PerformLayout();
            this.gBGeneral.ResumeLayout(false);
            this.gBGeneral.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gBDatabase;
        private System.Windows.Forms.Label lbDBServer;
        private System.Windows.Forms.TextBox textDBServer;
        private System.Windows.Forms.TextBox textDBDatabase;
        private System.Windows.Forms.Label lbDBDatabase;
        private System.Windows.Forms.TextBox textDBPassword;
        private System.Windows.Forms.Label lbDBPassword;
        private System.Windows.Forms.TextBox textDBUsername;
        private System.Windows.Forms.Label lbDBUsername;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox gBGeneral;
        private System.Windows.Forms.CheckBox chkAutostart;
    }
}