namespace YProjectMedicalFlagger2
{
    partial class PatientFlagger
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
            saveButton = new Button();
            indexLabel = new Label();
            reMarkButton = new Button();
            saveMarksButton = new Button();
            nextPatientButton = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(800, 600);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // next
            // 
            next.Location = new Point(718, 618);
            next.Name = "next";
            next.Size = new Size(94, 29);
            next.TabIndex = 1;
            next.Text = "Sonraki";
            next.UseVisualStyleBackColor = true;
            next.Click += next_Click;
            // 
            // prev
            // 
            prev.Location = new Point(12, 618);
            prev.Name = "prev";
            prev.Size = new Size(94, 29);
            prev.TabIndex = 2;
            prev.Text = "Önceki";
            prev.UseVisualStyleBackColor = true;
            prev.Click += prev_Click;
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(975, 12);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(275, 444);
            checkedListBox1.TabIndex = 3;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(975, 492);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(275, 120);
            richTextBox1.TabIndex = 4;
            richTextBox1.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(975, 468);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 5;
            label1.Text = "label1";
            label1.Click += label1_Click;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(975, 618);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(94, 29);
            saveButton.TabIndex = 6;
            saveButton.Text = "Kaydet";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // indexLabel
            // 
            indexLabel.AutoSize = true;
            indexLabel.Location = new Point(342, 618);
            indexLabel.Name = "indexLabel";
            indexLabel.Size = new Size(132, 20);
            indexLabel.TabIndex = 7;
            indexLabel.Text = "Resim bulunamadı";
            indexLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // reMarkButton
            // 
            reMarkButton.Location = new Point(818, 12);
            reMarkButton.Name = "reMarkButton";
            reMarkButton.Size = new Size(94, 29);
            reMarkButton.TabIndex = 8;
            reMarkButton.Text = "Reset";
            reMarkButton.UseVisualStyleBackColor = true;
            reMarkButton.Click += reMarkButton_Click;
            // 
            // saveMarksButton
            // 
            saveMarksButton.Location = new Point(818, 47);
            saveMarksButton.Name = "saveMarksButton";
            saveMarksButton.Size = new Size(94, 29);
            saveMarksButton.TabIndex = 9;
            saveMarksButton.Text = "Kayıt";
            saveMarksButton.UseVisualStyleBackColor = true;
            saveMarksButton.Click += saveMarksButton_Click;
            // 
            // nextPatientButton
            // 
            nextPatientButton.Location = new Point(1156, 618);
            nextPatientButton.Name = "nextPatientButton";
            nextPatientButton.Size = new Size(94, 29);
            nextPatientButton.TabIndex = 10;
            nextPatientButton.Text = "Sonraki";
            nextPatientButton.UseVisualStyleBackColor = true;
            nextPatientButton.Click += nextPatientButton_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1262, 673);
            Controls.Add(nextPatientButton);
            Controls.Add(saveMarksButton);
            Controls.Add(reMarkButton);
            Controls.Add(indexLabel);
            Controls.Add(saveButton);
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
        private Button saveButton;
        private Label indexLabel;
        private Button reMarkButton;
        private Button saveMarksButton;
        private Button nextPatientButton;
    }
}