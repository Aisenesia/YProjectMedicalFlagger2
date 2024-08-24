namespace YProjectMedicalFlagger2
{
    partial class TestForm
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
            this.listView = new ListView();
            this.editBox = new TextBox();

            this.SuspendLayout();

            // ListView configuration
            this.listView.View = View.Details;
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.LabelEdit = true;
            this.listView.Columns.Add("Name", 100);
            this.listView.Columns.Add("Value", 200); // Ensure this is the last column
            this.listView.MouseDoubleClick += new MouseEventHandler(ListView_MouseDoubleClick);

            // Add items
            ListViewItem item1 = new ListViewItem("Name");
            item1.SubItems.Add("John");
            this.listView.Items.Add(item1);

            ListViewItem item2 = new ListViewItem("Surname");
            item2.SubItems.Add("Doe");
            this.listView.Items.Add(item2);

            // Add ListView to the form
            this.listView.Dock = DockStyle.Fill;
            this.Controls.Add(this.listView);

            // TextBox configuration
            this.editBox.BorderStyle = BorderStyle.FixedSingle;
            this.editBox.Visible = false;
            this.editBox.LostFocus += new EventHandler(EditBox_LostFocus);
            this.editBox.KeyPress += new KeyPressEventHandler(EditBox_KeyPress);
            this.Controls.Add(this.editBox);

            // Form configuration
            this.Text = "Divided List Box Example";
            this.ClientSize = new System.Drawing.Size(400, 200);
            this.ResumeLayout(false);
        }


        #endregion

        private ListView listView1;
        private ColumnHeader Category;
        private ColumnHeader Value;
    }
}