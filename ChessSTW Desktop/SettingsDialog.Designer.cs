namespace ChessSTW_Desktop
{
    partial class SettingsDialog
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
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            PortTextBox = new TextBox();
            IPTextBox = new TextBox();
            offlineGroup = new GroupBox();
            rotateBox = new CheckBox();
            onlineGroup = new GroupBox();
            saveButton = new Button();
            cancelButton = new Button();
            offlineGroup.SuspendLayout();
            onlineGroup.SuspendLayout();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(197, 52);
            label3.Name = "label3";
            label3.Size = new Size(11, 17);
            label3.TabIndex = 14;
            label3.Text = ":";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(214, 24);
            label2.Name = "label2";
            label2.Size = new Size(32, 17);
            label2.TabIndex = 13;
            label2.Text = "Port";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 24);
            label1.Name = "label1";
            label1.Size = new Size(59, 17);
            label1.TabIndex = 12;
            label1.Text = "Server IP";
            // 
            // PortTextBox
            // 
            PortTextBox.Location = new Point(211, 41);
            PortTextBox.Name = "PortTextBox";
            PortTextBox.Size = new Size(76, 25);
            PortTextBox.TabIndex = 11;
            // 
            // IPTextBox
            // 
            IPTextBox.Location = new Point(7, 41);
            IPTextBox.Name = "IPTextBox";
            IPTextBox.Size = new Size(181, 25);
            IPTextBox.TabIndex = 10;
            // 
            // offlineGroup
            // 
            offlineGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            offlineGroup.Controls.Add(rotateBox);
            offlineGroup.Location = new Point(12, 12);
            offlineGroup.Name = "offlineGroup";
            offlineGroup.Padding = new Padding(6);
            offlineGroup.Size = new Size(296, 100);
            offlineGroup.TabIndex = 15;
            offlineGroup.TabStop = false;
            offlineGroup.Text = "Offline settings";
            // 
            // rotateBox
            // 
            rotateBox.AutoSize = true;
            rotateBox.Location = new Point(10, 27);
            rotateBox.Name = "rotateBox";
            rotateBox.Size = new Size(98, 21);
            rotateBox.TabIndex = 15;
            rotateBox.Text = "Rotate table";
            rotateBox.UseVisualStyleBackColor = true;
            // 
            // onlineGroup
            // 
            onlineGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            onlineGroup.Controls.Add(label1);
            onlineGroup.Controls.Add(IPTextBox);
            onlineGroup.Controls.Add(label3);
            onlineGroup.Controls.Add(PortTextBox);
            onlineGroup.Controls.Add(label2);
            onlineGroup.Location = new Point(12, 118);
            onlineGroup.Name = "onlineGroup";
            onlineGroup.Padding = new Padding(6);
            onlineGroup.Size = new Size(296, 123);
            onlineGroup.TabIndex = 16;
            onlineGroup.TabStop = false;
            onlineGroup.Text = "Online settings";
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            saveButton.Location = new Point(152, 261);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(75, 29);
            saveButton.TabIndex = 17;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.Location = new Point(233, 261);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 29);
            cancelButton.TabIndex = 18;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // SettingsDialog
            // 
            AcceptButton = saveButton;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(320, 302);
            Controls.Add(cancelButton);
            Controls.Add(saveButton);
            Controls.Add(onlineGroup);
            Controls.Add(offlineGroup);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "SettingsDialog";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SettingsDialog";
            Load += SettingsDialog_Load;
            offlineGroup.ResumeLayout(false);
            offlineGroup.PerformLayout();
            onlineGroup.ResumeLayout(false);
            onlineGroup.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox PortTextBox;
        private TextBox IPTextBox;
        private GroupBox offlineGroup;
        private CheckBox rotateBox;
        private GroupBox onlineGroup;
        private Button saveButton;
        private Button cancelButton;
    }
}