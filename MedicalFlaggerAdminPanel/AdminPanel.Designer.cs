namespace MedicalFlaggerAdminPanel
{
    partial class AdminPanelForm
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
            Add = new Button();
            saveButton = new Button();
            deleteButton = new Button();
            textBox1 = new TextBox();
            listBox1 = new ListBox();
            upButton = new Button();
            downButton = new Button();
            SuspendLayout();
            // 
            // Add
            // 
            Add.Location = new Point(416, 12);
            Add.Name = "Add";
            Add.Size = new Size(94, 29);
            Add.TabIndex = 3;
            Add.Text = "Ekle";
            Add.UseVisualStyleBackColor = true;
            Add.Click += addButton_Click;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(416, 402);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(94, 29);
            saveButton.TabIndex = 4;
            saveButton.Text = "Kaydet";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Location = new Point(416, 47);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(94, 29);
            deleteButton.TabIndex = 5;
            deleteButton.Text = "Sil";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += deleteButton_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(398, 27);
            textBox1.TabIndex = 6;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(12, 47);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(398, 384);
            listBox1.TabIndex = 7;
            // 
            // upButton
            // 
            upButton.Location = new Point(416, 164);
            upButton.Name = "upButton";
            upButton.Size = new Size(94, 56);
            upButton.TabIndex = 8;
            upButton.Text = "Up\r\n";
            upButton.UseVisualStyleBackColor = true;
            upButton.Click += upButton_Click;
            // 
            // downButton
            // 
            downButton.Location = new Point(416, 226);
            downButton.Name = "downButton";
            downButton.Size = new Size(94, 56);
            downButton.TabIndex = 9;
            downButton.Text = "Down";
            downButton.UseVisualStyleBackColor = true;
            downButton.Click += downButton_Click;
            // 
            // AdminPanelForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(downButton);
            Controls.Add(upButton);
            Controls.Add(listBox1);
            Controls.Add(textBox1);
            Controls.Add(deleteButton);
            Controls.Add(saveButton);
            Controls.Add(Add);
            Name = "AdminPanelForm";
            Text = "Admin Panel";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button Add;
        private Button saveButton;
        private Button deleteButton;
        private TextBox textBox1;
        private ListBox listBox1;
        private Button upButton;
        private Button downButton;
    }
}
