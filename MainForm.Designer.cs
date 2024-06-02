namespace App
{
    partial class MainForm
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.encryptFileButton = new System.Windows.Forms.Button();
            this.decryptFileButton = new System.Windows.Forms.Button();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sendToServerButton = new System.Windows.Forms.Button();
            this.encryptFolderButton = new System.Windows.Forms.Button();
            this.decryptFolderButton = new System.Windows.Forms.Button();
            this.labelInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // encryptFileButton
            // 
            this.encryptFileButton.Location = new System.Drawing.Point(20, 88);
            this.encryptFileButton.Name = "encryptFileButton";
            this.encryptFileButton.Size = new System.Drawing.Size(111, 33);
            this.encryptFileButton.TabIndex = 0;
            this.encryptFileButton.Text = "Encrypt File";
            this.encryptFileButton.UseVisualStyleBackColor = true;
            // 
            // decryptFileButton
            // 
            this.decryptFileButton.Location = new System.Drawing.Point(137, 88);
            this.decryptFileButton.Name = "decryptFileButton";
            this.decryptFileButton.Size = new System.Drawing.Size(111, 33);
            this.decryptFileButton.TabIndex = 1;
            this.decryptFileButton.Text = "Decrypt File";
            this.decryptFileButton.UseVisualStyleBackColor = true;
            // 
            // comboBox
            // 
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Items.AddRange(new object[] {
            "Aes",
            "DES",
            "RC2",
            "Rijndael",
            "TripleDES"});
            this.comboBox.Location = new System.Drawing.Point(20, 43);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(228, 21);
            this.comboBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Encryption Method";
            // 
            // sendToServerButton
            // 
            this.sendToServerButton.Location = new System.Drawing.Point(20, 166);
            this.sendToServerButton.Name = "sendToServerButton";
            this.sendToServerButton.Size = new System.Drawing.Size(228, 33);
            this.sendToServerButton.TabIndex = 6;
            this.sendToServerButton.Text = "Send to Server";
            this.sendToServerButton.UseVisualStyleBackColor = true;
            // 
            // encryptFolderButton
            // 
            this.encryptFolderButton.Location = new System.Drawing.Point(20, 127);
            this.encryptFolderButton.Name = "encryptFolderButton";
            this.encryptFolderButton.Size = new System.Drawing.Size(111, 33);
            this.encryptFolderButton.TabIndex = 7;
            this.encryptFolderButton.Text = "Encrypt Folder";
            this.encryptFolderButton.UseVisualStyleBackColor = true;
            // 
            // decryptFolderButton
            // 
            this.decryptFolderButton.Location = new System.Drawing.Point(137, 127);
            this.decryptFolderButton.Name = "decryptFolderButton";
            this.decryptFolderButton.Size = new System.Drawing.Size(111, 33);
            this.decryptFolderButton.TabIndex = 8;
            this.decryptFolderButton.Text = "Decrypt Folder";
            this.decryptFolderButton.UseVisualStyleBackColor = true;
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(17, 234);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(0, 13);
            this.labelInfo.TabIndex = 10;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 274);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.decryptFolderButton);
            this.Controls.Add(this.encryptFolderButton);
            this.Controls.Add(this.sendToServerButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.decryptFileButton);
            this.Controls.Add(this.encryptFileButton);
            this.Name = "MainForm";
            this.Text = "App";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button encryptFileButton;
        private System.Windows.Forms.Button decryptFileButton;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button sendToServerButton;
        private System.Windows.Forms.Button encryptFolderButton;
        private System.Windows.Forms.Button decryptFolderButton;
        private System.Windows.Forms.Label labelInfo;
    }
}

