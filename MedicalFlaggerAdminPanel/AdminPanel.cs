namespace MedicalFlaggerAdminPanel
{
    public partial class AdminPanelForm : Form
    {


        public AdminPanelForm()
        {
            InitializeComponent();

        }



        private void addButton_Click(object sender, EventArgs e)
        {
            //AddItemToListView();
            addItemToListBox();
        }

        private void addItemToListBox()
        {
            if (textBox1.Text != "")
            {
                listBox1.Items.Add(textBox1.Text);
                textBox1.Clear();
            }

        }

        /* private void AddItemToListView()
         {
             // Add a new item to the ListView, with an empty label
             // (you can set any default properties that you want to here)
             ListViewItem item = listView1.Items.Add(String.Empty);
             //ListViewItem item = listView1.Columns.Add(String.Empty);

             // Place the newly-added item into edit mode immediately
             item.BeginEdit();
         }*/

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
                listBox1.Items.Remove(listBox1.SelectedItem);
            //listView1.SelectedItems[0].Remove();
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                while (listBox1.SelectedIndex > 0)
                {
                    int newIndex = listBox1.SelectedIndex - 1;
                    object selectedItem = listBox1.SelectedItem;
                    listBox1.Items.Remove(selectedItem);
                    listBox1.Items.Insert(newIndex, selectedItem);
                    listBox1.SelectedIndex = newIndex;
                }
            }

        }

        private void downButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                while (listBox1.SelectedIndex < listBox1.Items.Count - 1)
                {
                    int newIndex = listBox1.SelectedIndex + 1;
                    object selectedItem = listBox1.SelectedItem;
                    listBox1.Items.Remove(selectedItem);
                    listBox1.Items.Insert(newIndex, selectedItem);
                    listBox1.SelectedIndex = newIndex;
                }
            }


        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //save into csv file
            string filename = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\files" + "\\saves.csv";

            if (filename != null)
            {
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    sw.Write("name,");
                    sw.Write(string.Join(",", listBox1.Items.Cast<string>()));
                    sw.Write(",description\n");
                }
            }
            MessageBox.Show("Saved to file: " + filename);
        }   
        
    }
}
