namespace MachiCoroUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button2 = new Button();
            textBox5 = new TextBox();
            label5 = new Label();
            listBox1 = new ListBox();
            SuspendLayout();
            // 
            // button2
            // 
            button2.Location = new Point(871, 177);
            button2.Margin = new Padding(2, 1, 2, 1);
            button2.Name = "button2";
            button2.Size = new Size(390, 75);
            button2.TabIndex = 1;
            button2.Text = "Подключиться";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(407, 203);
            textBox5.Margin = new Padding(2, 1, 2, 1);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(428, 23);
            textBox5.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(352, 207);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(27, 15);
            label5.TabIndex = 11;
            label5.Text = "ник";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(152, 308);
            listBox1.Margin = new Padding(2, 1, 2, 1);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(184, 184);
            listBox1.TabIndex = 12;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1401, 629);
            Controls.Add(listBox1);
            Controls.Add(label5);
            Controls.Add(textBox5);
            Controls.Add(button2);
            Margin = new Padding(2, 1, 2, 1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button2;
        private TextBox textBox5;
        private Label label5;
        private ListBox listBox1;
    }
}
