namespace YProjectMedicalFlagger2
{
    partial class PatientSelect
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
            listBox1 = new ListBox();
            selectButton = new Button();
            selectPatientLabel = new Label();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(8, 50);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(588, 424);
            listBox1.TabIndex = 0;
            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
            // 
            // selectButton
            // 
            selectButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            selectButton.Location = new Point(694, 445);
            selectButton.Name = "selectButton";
            selectButton.Size = new Size(94, 29);
            selectButton.TabIndex = 1;
            selectButton.Text = "Seç";
            selectButton.UseVisualStyleBackColor = true;
            selectButton.Click += SelectButton_Click;
            // 
            // selectPatientLabel
            // 
            selectPatientLabel.AutoSize = true;
            selectPatientLabel.Location = new Point(12, 9);
            selectPatientLabel.Name = "selectPatientLabel";
            selectPatientLabel.Size = new Size(100, 20);
            selectPatientLabel.TabIndex = 2;
            selectPatientLabel.Text = "Hasta Seçiniz:";
            // 
            // PatientSelect
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 496);
            Controls.Add(listBox1);
            Controls.Add(selectButton);
            Controls.Add(selectPatientLabel);
            Icon = Properties.Resources.icon;
            Name = "PatientSelect";
            Text = "Hasta Seçici";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox1;
        private Button selectButton;
        private Label selectPatientLabel;
    }
}
