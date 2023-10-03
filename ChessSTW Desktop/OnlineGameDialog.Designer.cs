namespace ChessSTW_Desktop
{
    partial class OnlineGameDialog
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
            whiteRadio = new RadioButton();
            blackRadio = new RadioButton();
            colorBox = new GroupBox();
            randomRadio = new RadioButton();
            okButton = new Button();
            textBox1 = new TextBox();
            label4 = new Label();
            opponLabel = new Label();
            opponTextBox = new TextBox();
            specOpponBox = new CheckBox();
            colorBox.SuspendLayout();
            SuspendLayout();
            // 
            // whiteRadio
            // 
            whiteRadio.AutoSize = true;
            whiteRadio.Location = new Point(19, 51);
            whiteRadio.Name = "whiteRadio";
            whiteRadio.Size = new Size(74, 21);
            whiteRadio.TabIndex = 0;
            whiteRadio.TabStop = true;
            whiteRadio.Text = "White ⬜";
            whiteRadio.UseVisualStyleBackColor = true;
            whiteRadio.CheckedChanged += whiteRadio_CheckedChanged;
            // 
            // blackRadio
            // 
            blackRadio.AutoSize = true;
            blackRadio.Location = new Point(19, 78);
            blackRadio.Name = "blackRadio";
            blackRadio.Size = new Size(75, 21);
            blackRadio.TabIndex = 1;
            blackRadio.TabStop = true;
            blackRadio.Text = "Black  🞕";
            blackRadio.UseVisualStyleBackColor = true;
            blackRadio.CheckedChanged += blackRadio_CheckedChanged;
            // 
            // colorBox
            // 
            colorBox.Controls.Add(randomRadio);
            colorBox.Controls.Add(whiteRadio);
            colorBox.Controls.Add(blackRadio);
            colorBox.Location = new Point(12, 217);
            colorBox.Name = "colorBox";
            colorBox.Size = new Size(110, 113);
            colorBox.TabIndex = 5;
            colorBox.TabStop = false;
            colorBox.Text = "Chose color";
            // 
            // randomRadio
            // 
            randomRadio.AutoSize = true;
            randomRadio.Location = new Point(19, 24);
            randomRadio.Name = "randomRadio";
            randomRadio.Size = new Size(75, 21);
            randomRadio.TabIndex = 2;
            randomRadio.TabStop = true;
            randomRadio.Text = "Random";
            randomRadio.UseVisualStyleBackColor = true;
            randomRadio.CheckedChanged += randomRadio_CheckedChanged;
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.Location = new Point(198, 298);
            okButton.Name = "okButton";
            okButton.Size = new Size(81, 32);
            okButton.TabIndex = 6;
            okButton.Text = "Connect";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 39);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(268, 25);
            textBox1.TabIndex = 0;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 19);
            label4.Name = "label4";
            label4.Size = new Size(67, 17);
            label4.TabIndex = 11;
            label4.Text = "Username";
            // 
            // opponLabel
            // 
            opponLabel.AutoSize = true;
            opponLabel.Enabled = false;
            opponLabel.Location = new Point(12, 137);
            opponLabel.Name = "opponLabel";
            opponLabel.Size = new Size(137, 17);
            opponLabel.TabIndex = 13;
            opponLabel.Text = "Opponent's username";
            // 
            // opponTextBox
            // 
            opponTextBox.Enabled = false;
            opponTextBox.Location = new Point(12, 157);
            opponTextBox.Name = "opponTextBox";
            opponTextBox.Size = new Size(268, 25);
            opponTextBox.TabIndex = 4;
            opponTextBox.TextChanged += opponTextBox_TextChanged;
            // 
            // specOpponBox
            // 
            specOpponBox.AutoSize = true;
            specOpponBox.Location = new Point(12, 102);
            specOpponBox.Name = "specOpponBox";
            specOpponBox.Size = new Size(235, 21);
            specOpponBox.TabIndex = 3;
            specOpponBox.Text = "Want to play with specific opponent";
            specOpponBox.UseVisualStyleBackColor = true;
            specOpponBox.CheckedChanged += specOpponBox_CheckedChanged;
            // 
            // OnlineGameDialog
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(291, 342);
            Controls.Add(specOpponBox);
            Controls.Add(opponLabel);
            Controls.Add(opponTextBox);
            Controls.Add(label4);
            Controls.Add(textBox1);
            Controls.Add(okButton);
            Controls.Add(colorBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "OnlineGameDialog";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "New Online Game";
            Load += OnlineGameDialog_Load;
            colorBox.ResumeLayout(false);
            colorBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RadioButton whiteRadio;
        private RadioButton blackRadio;
        private GroupBox colorBox;
        private Button okButton;
        private RadioButton randomRadio;
        private TextBox textBox1;
        private Label label4;
        private Label opponLabel;
        private TextBox opponTextBox;
        private CheckBox specOpponBox;
    }
}