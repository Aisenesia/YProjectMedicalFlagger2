
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
            patientListBox = new CheckedListBox();
            richTextBox1 = new RichTextBox();
            label1 = new Label();
            saveButton = new Button();
            indexLabel = new Label();
            nextPatientButton = new Button();
            patientLabel = new Label();
            patientNameLabel = new Label();
            imageListBox = new CheckedListBox();
            saveImageAttributesButton = new Button();
            richTextBox2 = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(12, 47);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(680, 600);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // next
            // 
            next.Location = new Point(598, 680);
            next.Name = "next";
            next.Size = new Size(94, 29);
            next.TabIndex = 1;
            next.Text = "Sonraki";
            next.UseVisualStyleBackColor = true;
            next.Click += next_Click;
            // 
            // prev
            // 
            prev.Location = new Point(12, 680);
            prev.Name = "prev";
            prev.Size = new Size(94, 29);
            prev.TabIndex = 2;
            prev.Text = "Önceki";
            prev.UseVisualStyleBackColor = true;
            prev.Click += prev_Click;
            // 
            // patientListBox
            // 
            patientListBox.FormattingEnabled = true;
            patientListBox.Location = new Point(975, 47);
            patientListBox.Name = "patientListBox";
            patientListBox.Size = new Size(275, 444);
            patientListBox.TabIndex = 3;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(975, 527);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(275, 120);
            richTextBox1.TabIndex = 4;
            richTextBox1.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(975, 504);
            label1.Name = "label1";
            label1.Size = new Size(125, 20);
            label1.TabIndex = 5;
            label1.Text = "descriptionsLabel";
            // 
            // saveButton
            // 
            saveButton.Location = new Point(975, 680);
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
            indexLabel.Location = new Point(261, 689);
            indexLabel.Name = "indexLabel";
            indexLabel.Size = new Size(132, 20);
            indexLabel.TabIndex = 7;
            indexLabel.Text = "Resim bulunamadı";
            indexLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // nextPatientButton
            // 
            nextPatientButton.Location = new Point(1156, 680);
            nextPatientButton.Name = "nextPatientButton";
            nextPatientButton.Size = new Size(94, 29);
            nextPatientButton.TabIndex = 10;
            nextPatientButton.Text = "Sonraki";
            nextPatientButton.UseVisualStyleBackColor = true;
            nextPatientButton.Click += nextPatientButton_Click;
            // 
            // patientLabel
            // 
            patientLabel.AutoSize = true;
            patientLabel.Location = new Point(12, 24);
            patientLabel.Name = "patientLabel";
            patientLabel.Size = new Size(54, 20);
            patientLabel.TabIndex = 12;
            patientLabel.Text = "Hasta: ";
            // 
            // patientNameLabel
            // 
            patientNameLabel.AutoSize = true;
            patientNameLabel.Location = new Point(82, 24);
            patientNameLabel.Name = "patientNameLabel";
            patientNameLabel.Size = new Size(35, 20);
            patientNameLabel.TabIndex = 13;
            patientNameLabel.Text = "doe";
            // 
            // imageListBox
            // 
            imageListBox.FormattingEnabled = true;
            imageListBox.Location = new Point(698, 47);
            imageListBox.Name = "imageListBox";
            imageListBox.Size = new Size(202, 444);
            imageListBox.TabIndex = 14;
            // 
            // saveImageAttributesButton
            // 
            saveImageAttributesButton.Location = new Point(698, 680);
            saveImageAttributesButton.Name = "saveImageAttributesButton";
            saveImageAttributesButton.Size = new Size(94, 29);
            saveImageAttributesButton.TabIndex = 15;
            saveImageAttributesButton.Text = "Kaydet";
            saveImageAttributesButton.UseVisualStyleBackColor = true;
            saveImageAttributesButton.Click += saveImageAttributesButton_Click;
            // 
            // richTextBox2
            // 
            richTextBox2.Location = new Point(698, 527);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new Size(202, 120);
            richTextBox2.TabIndex = 16;
            richTextBox2.Text = "";
            // 
            // PatientFlagger
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1262, 721);
            Controls.Add(richTextBox2);
            Controls.Add(saveImageAttributesButton);
            Controls.Add(imageListBox);
            Controls.Add(patientNameLabel);
            Controls.Add(patientLabel);
            Controls.Add(nextPatientButton);
            Controls.Add(indexLabel);
            Controls.Add(saveButton);
            Controls.Add(label1);
            Controls.Add(richTextBox1);
            Controls.Add(patientListBox);
            Controls.Add(prev);
            Controls.Add(next);
            Controls.Add(pictureBox1);
            Name = "PatientFlagger";
            Text = "Form2";
            Load += Form2_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void selectPatientLabel_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button next;
        private Button prev;
        private CheckedListBox patientListBox;
        private RichTextBox richTextBox1;
        private Label label1;
        private Button saveButton;
        private Label indexLabel;
        private Button nextPatientButton;
        private Label patientLabel;
        private Label patientNameLabel;
        private CheckedListBox imageListBox;
        private Button saveImageAttributesButton;
        private RichTextBox richTextBox2;
    }
}