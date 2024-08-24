namespace YProjectMedicalFlagger2
{
    public partial class PatientSelect : Form
    {
        private string[] files;
        private string selectedFile;

        public PatientSelect()
        {
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            InitializeComponent();
            this.Load += Form1_Load;
            selectedFile = string.Empty;
            files = [];
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
                    MessageBox.Show("Hasta klasörü bulunamadý.");
                }
            }
            else
            {
                MessageBox.Show("Directory does not exist: " + directoryPath);
            }
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0 && listBox1.SelectedItem != null)
            {
                selectedFile = listBox1.SelectedItem.ToString();
            }
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedFile))
            {
                int index = listBox1.SelectedIndex;
                PatientFlagger2 patientFlagger = new(selectedFile, files, index);
                //TODO - Switch when building for release
                this.Hide();
                patientFlagger.Show();
            }
        }
    }
}
