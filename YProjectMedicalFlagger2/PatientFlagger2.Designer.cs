
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
            next = new Button();
            prev = new Button();
            patientTextBox = new RichTextBox();
            savePatient_Button = new Button();
            indexLabel = new Label();
            nextPatientButton = new Button();
            imageListBox = new CheckedListBox();
            imageTextBox = new RichTextBox();
            saveImageAttributes_Button = new Button();
            PreviousPatientButton = new Button();
            patientListView = new ListView();
            Categories = new ColumnHeader();
            Values = new ColumnHeader();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            patientLabel = new Label();
            patientNameLabel = new Label();
            pictureBox1 = new PictureBox();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // next
            // 
            next.Location = new Point(576, 3);
            next.Name = "next";
            next.Size = new Size(122, 28);
            next.TabIndex = 1;
            next.Text = "Sonraki Resim";
            next.UseVisualStyleBackColor = true;
            next.Click += NextImageButton_Click;
            // 
            // prev
            // 
            prev.Location = new Point(3, 3);
            prev.Name = "prev";
            prev.Size = new Size(122, 28);
            prev.TabIndex = 2;
            prev.Text = "Önceki Resim";
            prev.UseVisualStyleBackColor = true;
            prev.Click += PreviousImageButton_Click;
            // 
            // patientTextBox
            // 
            patientTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            patientTextBox.Location = new Point(265, 392);
            patientTextBox.Name = "patientTextBox";
            patientTextBox.Size = new Size(257, 203);
            patientTextBox.TabIndex = 4;
            patientTextBox.Text = "";
            patientTextBox.TextChanged += patientTextBox_TextChanged;
            // 
            // savePatient_Button
            // 
            savePatient_Button.Anchor = AnchorStyles.None;
            savePatient_Button.Location = new Point(346, 603);
            savePatient_Button.Name = "savePatient_Button";
            savePatient_Button.Size = new Size(94, 29);
            savePatient_Button.TabIndex = 6;
            savePatient_Button.Text = "Kaydet";
            savePatient_Button.UseVisualStyleBackColor = true;
            savePatient_Button.Click += SavePatient_Button_Click;
            // 
            // indexLabel
            // 
            indexLabel.Anchor = AnchorStyles.None;
            indexLabel.AutoSize = true;
            indexLabel.Location = new Point(284, 7);
            indexLabel.Name = "indexLabel";
            indexLabel.Size = new Size(132, 20);
            indexLabel.TabIndex = 7;
            indexLabel.Text = "Resim bulunamadı";
            indexLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // nextPatientButton
            // 
            nextPatientButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            nextPatientButton.Location = new Point(1125, 712);
            nextPatientButton.Name = "nextPatientButton";
            nextPatientButton.Size = new Size(122, 29);
            nextPatientButton.TabIndex = 10;
            nextPatientButton.Text = "Sonraki Hasta";
            nextPatientButton.UseVisualStyleBackColor = true;
            nextPatientButton.Click += NextPatientButton_Click;
            // 
            // imageListBox
            // 
            imageListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            imageListBox.FormattingEnabled = true;
            imageListBox.Location = new Point(3, 3);
            imageListBox.Name = "imageListBox";
            imageListBox.Size = new Size(256, 378);
            imageListBox.TabIndex = 14;
            imageListBox.ItemCheck += imageListBox_ItemCheck;
            // 
            // imageTextBox
            // 
            imageTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            imageTextBox.Location = new Point(3, 392);
            imageTextBox.Name = "imageTextBox";
            imageTextBox.Size = new Size(256, 203);
            imageTextBox.TabIndex = 15;
            imageTextBox.Text = "";
            imageTextBox.TextChanged += imageTextBox_TextChanged;
            // 
            // saveImageAttributes_Button
            // 
            saveImageAttributes_Button.Anchor = AnchorStyles.None;
            saveImageAttributes_Button.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            saveImageAttributes_Button.Location = new Point(84, 603);
            saveImageAttributes_Button.Name = "saveImageAttributes_Button";
            saveImageAttributes_Button.Size = new Size(94, 29);
            saveImageAttributes_Button.TabIndex = 16;
            saveImageAttributes_Button.Text = "Kaydet";
            saveImageAttributes_Button.UseVisualStyleBackColor = true;
            saveImageAttributes_Button.Click += SaveImageAttributes_Button_Click;
            // 
            // PreviousPatientButton
            // 
            PreviousPatientButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            PreviousPatientButton.Location = new Point(18, 712);
            PreviousPatientButton.Name = "PreviousPatientButton";
            PreviousPatientButton.Size = new Size(122, 29);
            PreviousPatientButton.TabIndex = 0;
            PreviousPatientButton.Text = "Önceki Hasta";
            PreviousPatientButton.UseVisualStyleBackColor = true;
            PreviousPatientButton.Click += PreviousPatientButton_Click;
            // 
            // patientListView
            // 
            patientListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            patientListView.Columns.AddRange(new ColumnHeader[] { Categories, Values });
            patientListView.FullRowSelect = true;
            patientListView.GridLines = true;
            patientListView.Location = new Point(265, 3);
            patientListView.Name = "patientListView";
            patientListView.Size = new Size(257, 383);
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
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(imageTextBox, 0, 1);
            tableLayoutPanel1.Controls.Add(patientTextBox, 1, 1);
            tableLayoutPanel1.Controls.Add(saveImageAttributes_Button, 0, 2);
            tableLayoutPanel1.Controls.Add(imageListBox, 0, 0);
            tableLayoutPanel1.Controls.Add(patientListView, 1, 0);
            tableLayoutPanel1.Controls.Add(savePatient_Button, 1, 2);
            tableLayoutPanel1.Location = new Point(725, 47);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 65F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(525, 659);
            tableLayoutPanel1.TabIndex = 17;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(tableLayoutPanel2, 0, 2);
            tableLayoutPanel4.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel4.Controls.Add(pictureBox1, 0, 1);
            tableLayoutPanel4.Location = new Point(12, 47);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 3;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel4.Size = new Size(707, 659);
            tableLayoutPanel4.TabIndex = 19;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 128F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 128F));
            tableLayoutPanel2.Controls.Add(indexLabel, 1, 0);
            tableLayoutPanel2.Controls.Add(next, 2, 0);
            tableLayoutPanel2.Controls.Add(prev, 0, 0);
            tableLayoutPanel2.Location = new Point(3, 622);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(701, 34);
            tableLayoutPanel2.TabIndex = 20;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 64F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(patientLabel, 0, 0);
            tableLayoutPanel3.Controls.Add(patientNameLabel, 1, 0);
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(698, 20);
            tableLayoutPanel3.TabIndex = 18;
            // 
            // patientLabel
            // 
            patientLabel.Anchor = AnchorStyles.Left;
            patientLabel.AutoSize = true;
            patientLabel.Location = new Point(3, 0);
            patientLabel.Name = "patientLabel";
            patientLabel.Size = new Size(54, 20);
            patientLabel.TabIndex = 12;
            patientLabel.Text = "Hasta: ";
            // 
            // patientNameLabel
            // 
            patientNameLabel.Anchor = AnchorStyles.Left;
            patientNameLabel.AutoSize = true;
            patientNameLabel.Location = new Point(67, 0);
            patientNameLabel.Name = "patientNameLabel";
            patientNameLabel.Size = new Size(35, 20);
            patientNameLabel.TabIndex = 13;
            patientNameLabel.Text = "doe";
            patientNameLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.Location = new Point(3, 33);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(701, 583);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += PictureBox_Click;
            // 
            // PatientFlagger2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1262, 753);
            Controls.Add(tableLayoutPanel4);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(PreviousPatientButton);
            Controls.Add(nextPatientButton);
            Icon = Properties.Resources.icon;
            Name = "PatientFlagger2";
            FormClosing += PatientFlagger2_FormClosing;
            Load += Form2_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        private void selectPatientLabel_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

#endregion
        private Button next;
        private Button prev;
        private RichTextBox patientTextBox;
        private Button savePatient_Button;
        private Label indexLabel;
        private Button nextPatientButton;
        private CheckedListBox imageListBox;
        private RichTextBox imageTextBox;
        private Button saveImageAttributes_Button;
        private Button PreviousPatientButton;
        private ListView patientListView;
        private ColumnHeader Categories;
        private ColumnHeader Values;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private Label patientLabel;
        private Label patientNameLabel;
        private PictureBox pictureBox1;
    }
}