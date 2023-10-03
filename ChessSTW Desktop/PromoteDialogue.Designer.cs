namespace ChessSTW_Desktop
{
    partial class PromoteDialogue
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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 48F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(37, 67);
            button1.Name = "button1";
            button1.Padding = new Padding(6, 0, 0, 0);
            button1.Size = new Size(125, 125);
            button1.TabIndex = 0;
            button1.Text = "♕";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 48F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(168, 67);
            button2.Name = "button2";
            button2.Padding = new Padding(6, 0, 0, 0);
            button2.Size = new Size(125, 125);
            button2.TabIndex = 1;
            button2.Text = "♖";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI", 48F, FontStyle.Regular, GraphicsUnit.Point);
            button3.Location = new Point(299, 67);
            button3.Name = "button3";
            button3.Padding = new Padding(6, 0, 0, 0);
            button3.Size = new Size(125, 125);
            button3.TabIndex = 2;
            button3.Text = "♗";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 48F, FontStyle.Regular, GraphicsUnit.Point);
            button4.Location = new Point(430, 67);
            button4.Name = "button4";
            button4.Padding = new Padding(6, 0, 0, 0);
            button4.Size = new Size(125, 125);
            button4.TabIndex = 3;
            button4.Text = "♘";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // PromoteDialogue
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(595, 262);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PromoteDialogue";
            Text = "PromoteDialogue";
            Load += PromoteDialogue_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}