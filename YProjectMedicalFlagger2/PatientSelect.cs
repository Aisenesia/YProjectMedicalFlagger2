namespace YProjectMedicalFlagger2
{
    public partial class PatientSelect : Form
    {
        private string[] files;
        private string selectedFile;

        public PatientSelect()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            selectedFile = string.Empty;
            files = new string[0];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string directoryPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "files");

            if (Directory.Exists(directoryPath))
            {
                files = Directory.GetDirectories(directoryPath);

                if (files.Length > 0)
                {
                    foreach (string file in files)
                    {
                        listBox1.Items.Add(Path.GetFileName(file));
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                selectedFile = listBox1.SelectedItem.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedFile))
            {
                int index = listBox1.SelectedIndex;
                PatientFlagger2 form2 = new PatientFlagger2(selectedFile, files, index);
                //TODO - Switch when building for release
                form2.Show();
            }
        }

        private void InitializeListBox()
        {
            listBox1 = new ListBox
            {
                FormattingEnabled = true,
                Location = new Point(12, 54),
                Name = "listBox1",
                Size = new Size(322, 364),
                TabIndex = 0
            };
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
        }

      
        
    }
}
