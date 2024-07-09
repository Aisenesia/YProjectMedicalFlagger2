namespace YProjectMedicalFlagger2
{
    public partial class Form1 : Form
    {
        String[] files;
        String selectedFile;

        //Directory
        private void Form1_Load(object sender, EventArgs e)
        {
            string directoryPath = @"C:\Users\Hakan\source\repos\YProjectMedicalFlagger\YProjectMedicalFlagger\files";
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
                MessageBox.Show("Directory does not exist.");
            }
        }

        public Form1()
        {

            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedFile = listBox1.SelectedItem.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(selectedFile != null)
            {
               Form2 form2 = new Form2(selectedFile);

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
