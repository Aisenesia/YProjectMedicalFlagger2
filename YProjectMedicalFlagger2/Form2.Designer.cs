namespace YProjectMedicalFlagger2
{
    partial class Form2
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
            pictureBox1 = new PictureBox();
            next = new Button();
            prev = new Button();
            checkedListBox1 = new CheckedListBox();
            richTextBox1 = new RichTextBox();
            label1 = new Label();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(457, 400);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // next
            // 
            next.Location = new Point(375, 426);
            next.Name = "next";
            next.Size = new Size(94, 29);
            next.TabIndex = 1;
            next.Text = "next";
            next.UseVisualStyleBackColor = true;
            next.Click += next_Click;
            // 
            // prev
            // 
            prev.Location = new Point(12, 426);
            prev.Name = "prev";
            prev.Size = new Size(94, 29);
            prev.TabIndex = 2;
            prev.Text = "prev";
            prev.UseVisualStyleBackColor = true;
            prev.Click += prev_Click;
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(513, 12);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(275, 224);
            checkedListBox1.TabIndex = 3;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(513, 292);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(275, 120);
            richTextBox1.TabIndex = 4;
            richTextBox1.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(513, 269);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 5;
            label1.Text = "label1";
            // 
            // button1
            // 
            button1.Location = new Point(694, 426);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 6;
            button1.Text = "Save";
            button1.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 467);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(richTextBox1);
            Controls.Add(checkedListBox1);
            Controls.Add(prev);
            Controls.Add(next);
            Controls.Add(pictureBox1);
            Name = "Form2";
            Text = "Form2";
            Load += Form2_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button next;
        private Button prev;
        private CheckedListBox checkedListBox1;
        private RichTextBox richTextBox1;
        private Label label1;
        private Button button1;
    }
}