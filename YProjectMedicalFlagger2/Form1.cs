namespace YProjectMedicalFlagger2
{
    public partial class Form1 : Form
    {
        String[] files;
        String selectedFile;

        //Directory
        private void Form1_Load(object sender, EventArgs e)
        {
            string directoryPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\files";//@"C:\Users\Hakan\source\repos\YProjectMedicalFlagger\YProjectMedicalFlagger\files";
            if (Directory.Exists(directoryPath))
            {
                files = Directory.GetDirectories(directoryPath);


                if (files.Length > 0)
                {
                    foreach (String file in files)
                    {

                        String[] strings = file.Split('\\'); // split the string by '\', makes it easier to select from list

                        listBox1.Items.Add(strings.Last());
                    }
                }
                else
                {
                    MessageBox.Show("No files found in the directory.");
                }
            }
            else
            {
                MessageBox.Show("Directory does not exist: " + directoryPath);
            }
        }

        public Form1()
        {

            InitializeComponent();
            this.Load += Form1_Load; // Removed the 'new EventHandler' part
            selectedFile = new String("");
            files = new String[0];
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string sel = listBox1.SelectedItem.ToString() ?? string.Empty; // done to prevent null reference exception
                selectedFile = sel;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (selectedFile != null)
            {
                int index = listBox1.SelectedIndex;
                Form2 form2 = new Form2(selectedFile, files, index + 1);

                form2.Show();
                //this.Close();

            }
        }
        private void InitializeListBox()
        {
            listBox1 = new ListBox();
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(12, 54);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(322, 364);
            listBox1.TabIndex = 0;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
        }
    }
}
