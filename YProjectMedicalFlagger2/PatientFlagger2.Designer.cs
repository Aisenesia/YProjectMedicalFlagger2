
namespace YProjectMedicalFlagger2
{
    partial class PatientFlagger2
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
            patientTextBox = new RichTextBox();
            label1 = new Label();
            savePatient_Button = new Button();
            indexLabel = new Label();
            nextPatientButton = new Button();
            patientLabel = new Label();
            patientNameLabel = new Label();
            imageListBox = new CheckedListBox();
            imageTextBox = new RichTextBox();
            saveImageAttributes_Button = new Button();
            PreviousPatientButton = new Button();
            patientListView = new ListView();
            Categories = new ColumnHeader();
            Values = new ColumnHeader();
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
            pictureBox1.Click += PictureBox_Click;
            // 
            // next
            // 
            next.Location = new Point(347, 680);
            next.Name = "next";
            next.Size = new Size(94, 29);
            next.TabIndex = 1;
            next.Text = "Sonraki";
            next.UseVisualStyleBackColor = true;
            next.Click += NextImageButton_Click;
            // 
            // prev
            // 
            prev.Location = new Point(247, 680);
            prev.Name = "prev";
            prev.Size = new Size(94, 29);
            prev.TabIndex = 2;
            prev.Text = "Önceki";
            prev.UseVisualStyleBackColor = true;
            prev.Click += PreviousImageButton_Click;
            // 
            // patientTextBox
            // 
            patientTextBox.Location = new Point(906, 527);
            patientTextBox.Name = "patientTextBox";
            patientTextBox.Size = new Size(344, 120);
            patientTextBox.TabIndex = 4;
            patientTextBox.Text = "";
            patientTextBox.TextChanged += patientTextBox_TextChanged;
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
            // savePatient_Button
            // 
            savePatient_Button.Location = new Point(975, 680);
            savePatient_Button.Name = "savePatient_Button";
            savePatient_Button.Size = new Size(94, 29);
            savePatient_Button.TabIndex = 6;
            savePatient_Button.Text = "Kaydet";
            savePatient_Button.UseVisualStyleBackColor = true;
            savePatient_Button.Click += SavePatient_Button_Click;
            // 
            // indexLabel
            // 
            indexLabel.AutoSize = true;
            indexLabel.Location = new Point(277, 650);
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
            nextPatientButton.Click += NextPatientButton_Click;
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
            imageListBox.ItemCheck += imageListBox_ItemCheck;
            // 
            // imageTextBox
            // 
            imageTextBox.Location = new Point(698, 497);
            imageTextBox.Name = "imageTextBox";
            imageTextBox.Size = new Size(202, 150);
            imageTextBox.TabIndex = 15;
            imageTextBox.Text = "";
            imageTextBox.TextChanged += imageTextBox_TextChanged;
            // 
            // saveImageAttributes_Button
            // 
            saveImageAttributes_Button.Location = new Point(698, 680);
            saveImageAttributes_Button.Name = "saveImageAttributes_Button";
            saveImageAttributes_Button.Size = new Size(94, 29);
            saveImageAttributes_Button.TabIndex = 16;
            saveImageAttributes_Button.Text = "Kaydet";
            saveImageAttributes_Button.UseVisualStyleBackColor = true;
            saveImageAttributes_Button.Click += SaveImageAttributes_Button_Click;
            // 
            // PreviousPatientButton
            // 
            PreviousPatientButton.Location = new Point(12, 680);
            PreviousPatientButton.Name = "PreviousPatientButton";
            PreviousPatientButton.Size = new Size(94, 29);
            PreviousPatientButton.TabIndex = 0;
            PreviousPatientButton.Text = "Önceki";
            PreviousPatientButton.UseVisualStyleBackColor = true;
            PreviousPatientButton.Click += PreviousPatientButton_Click;
            // 
            // patientListView
            // 
            patientListView.Columns.AddRange(new ColumnHeader[] { Categories, Values });
            patientListView.FullRowSelect = true;
            patientListView.GridLines = true;
            patientListView.Location = new Point(906, 47);
            patientListView.Name = "patientListView";
            patientListView.Size = new Size(344, 444);
            patientListView.TabIndex = 0;
            patientListView.UseCompatibleStateImageBehavior = false;
            patientListView.View = View.Details;
            patientListView.MouseDoubleClick += ListView_MouseDoubleClick;
            // 
            // Categories
            // 
            Categories.Text = "Kategori";
            Categories.Width = 130;
            // 
            // Values
            // 
            Values.Text = "Değer";
            Values.Width = 210;
            // 
            // PatientFlagger2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1262, 721);
            Controls.Add(patientListView);
            Controls.Add(PreviousPatientButton);
            Controls.Add(saveImageAttributes_Button);
            Controls.Add(imageTextBox);
            Controls.Add(imageListBox);
            Controls.Add(patientNameLabel);
            Controls.Add(patientLabel);
            Controls.Add(nextPatientButton);
            Controls.Add(indexLabel);
            Controls.Add(savePatient_Button);
            Controls.Add(label1);
            Controls.Add(patientTextBox);
            Controls.Add(prev);
            Controls.Add(next);
            Controls.Add(pictureBox1);
            Icon = Properties.Resources.icon;
            Name = "PatientFlagger2";
            FormClosing += PatientFlagger2_FormClosing;
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
        private RichTextBox patientTextBox;
        private Label label1;
        private Button savePatient_Button;
        private Label indexLabel;
        private Button nextPatientButton;
        private Label patientLabel;
        private Label patientNameLabel;
        private CheckedListBox imageListBox;
        private RichTextBox imageTextBox;
        private Button saveImageAttributes_Button;
        private Button PreviousPatientButton;
        private ListView patientListView;
        private ColumnHeader Categories;
        private ColumnHeader Values;
    }
}