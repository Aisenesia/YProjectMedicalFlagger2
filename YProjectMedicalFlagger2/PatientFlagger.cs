using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace YProjectMedicalFlagger2
{
    public partial class PatientFlagger : Form
    {
        private static string filesPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\files"; // @"C:\Users\Hakan\source\repos\YProjectMedicalFlagger\YProjectMedicalFlagger\files";
        private string directoryPath;
        private List<string> files;

        private string[] filesFromBefore;
        private int index;

        private string currentPatientName;
        private bool isSaved;
        private bool[] isImageSaved;

        private string[] imageFiles;
        private int currentIndex;
        private string saveFile;
        private string imageSaveFile;

        Dictionary<string, DataNode> dataMap;
        DataNode[] imageMap;
        private string[] categories;
        private string[] imageCategories;


        public PatientFlagger(string fileName, string[] files, int index)
        {
            currentPatientName = fileName;
            this.filesFromBefore = files;
            this.index = index;

            InitializeComponent();
            InitializeFileList();
            InitializeImageList();
            patientNameLabel.Text = currentPatientName;
            InitalizeListBox();
            InitalizeImageListBox();

            checkForSavedFile();
            setIfSaved();
        }

        private void InitializeFileList()
        {
            directoryPath = filesPath + "\\" + currentPatientName;
            files = Directory.GetFiles(directoryPath).ToList();
            saveFile = filesPath + "\\saves.csv";
            imageSaveFile = directoryPath + "\\imageAttributes.csv";
        }

        private void InitializeImageList()
        {
            imageFiles = files.Where(file => file.EndsWith(".png") || file.EndsWith(".jpg")).ToArray();
            imageMap = new DataNode[imageFiles.Length];
            isImageSaved = new bool[imageFiles.Length];
            currentIndex = 0;

        }

        private void DisplayCurrentImage()
        {
            if (imageFiles.Length > 0 && currentIndex >= 0 && currentIndex < imageFiles.Length)
            {
                pictureBox1.ImageLocation = imageFiles[currentIndex];
                indexLabel.Text = (currentIndex + 1) + "/" + imageFiles.Length;
                indexLabel.Show();

                checkForSavedImageFile();
                setIfImageSaved();
            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = "Ek notlar:";
            DisplayCurrentImage();
        }

        private void InitalizeListBox()
        {
            string line;
            try
            {
                line = File.ReadLines(saveFile).First();
            }
            catch (Exception e)
            {
                MessageBox.Show("No categories found in the save file, use admin panel to create categories.");
                return;
            }

            string[] firstLine = line.Split(';');
            categories = firstLine.Skip(1).Take(firstLine.Length - 2).ToArray(); // skip the first element, which is the image name, and last element, which is the description

            foreach (string data in categories)
            {
                patientListBox.Items.Add(data);
            }
        }

        private void InitalizeImageListBox()
        {
            string line;
            try
            {
                line = File.ReadLines(imageSaveFile).First();
            }
            catch (Exception e)
            {
                MessageBox.Show("No categories found in the save file, use admin panel to create categories.");
                return;
            }

            string[] firstLine = line.Split(';');
            categories = firstLine.Skip(1).Take(firstLine.Length - 2).ToArray(); // skip the first element, which is the image index, and last element, which is the description

            foreach (string data in categories)
            {
                imageListBox.Items.Add(data);
            }
        }

        private void checkForSavedFile()
        {
            dataMap = new Dictionary<string, DataNode>();

            if (File.Exists(saveFile))
            {
                isSaved = false;

                using (StreamReader reader = new StreamReader(saveFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] fields = ParseCsvLine(line);

                        if (fields.Length > 0 && fields[0] == currentPatientName)
                        {
                            isSaved = true;
                            bool[] data = new bool[fields.Length - 2];
                            for (int i = 1; i < fields.Length - 1; i++)
                            {
                                data[i - 1] = Convert.ToBoolean(fields[i]);
                            }
                            DataNode node = new DataNode(fields[0], data, fields[fields.Length - 1]);
                            dataMap.Add(fields[0], node);
                            break;
                        }
                    }
                }
            }
            else
            {
                File.Create(saveFile).Dispose(); // Ensure the file is properly closed after creation
            }
        }


        private void checkForSavedImageFile()
        {


            if (File.Exists(imageSaveFile))
            {
                isImageSaved[currentIndex] = false;

                using (StreamReader reader = new StreamReader(imageSaveFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] fields = ParseCsvLine(line);

                        if (fields.Length > 0 && fields[0] == currentIndex.ToString())
                        {
                            isImageSaved[currentIndex] = true;
                            bool[] data = new bool[fields.Length - 2];
                            for (int i = 1; i < fields.Length - 1; i++)
                            {
                                data[i - 1] = Convert.ToBoolean(fields[i]);
                            }
                            DataNode node = new DataNode(fields[0], data, fields[fields.Length - 1]);
                            imageMap[currentIndex] = node;
                            break;
                        }
                    }
                }
            }
            else
            {
                File.Create(imageSaveFile).Dispose(); // Ensure the file is properly closed after creation
            }
        }



        private string[] ParseCsvLine(string line)
        {
            var fields = new List<string>();
            var sb = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '\"')
                {
                    inQuotes = !inQuotes;
                    sb.Append(c);
                }
                else if (c == ';' && !inQuotes)
                {
                    fields.Add(sb.ToString().Trim('\"').Replace("\"\"", "\""));
                    sb.Clear();
                }
                else
                {
                    sb.Append(c);
                }
            }

            fields.Add(sb.ToString().Trim('\"').Replace("\"\"", "\""));
            return fields.ToArray();
        }

        private void setIfSaved()
        {
            if (isSaved)
            {
                DataNode node = dataMap[currentPatientName];
                bool[] data = node.data;
                for (int i = 0; i < data.Length; i++)
                {
                    patientListBox.SetItemChecked(i, data[i]);
                }
                patientTextBox.Text = node.description;
            }

        }


        private void setIfImageSaved()
        {
            if (isImageSaved == null) return;
            if (isImageSaved[currentIndex])
            {
                DataNode node = imageMap[currentIndex];
                bool[] data = node.data;
                for (int i = 0; i < data.Length; i++)
                {
                    imageListBox.SetItemChecked(i, data[i]);
                }
                imageTextBox.Text = node.description;
            }
            else
            {

                for (int i = 0; i < imageListBox.Items.Count; i++)
                {
                    imageListBox.SetItemChecked(i, false);
                }
            }
        }

        private void savePatient()
        {
            string data = currentPatientName + ";";
            for (int i = 0; i < patientListBox.Items.Count; i++)
            {
                data += (patientListBox.GetItemChecked(i) ? "true" : "false") + ";";
            }

            // Escape description by enclosing it in quotes if it contains a semicolon
            string description = patientTextBox.Text;
            if (description.Contains(";"))
            {
                description = "\"" + description.Replace("\"", "\"\"") + "\"";
            }
            data += description;

            if (isSaved)
            {
                string[] lines = File.ReadAllLines(saveFile);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] fields = lines[i].Split(';');
                    if (fields[0] == currentPatientName)
                    {
                        lines[i] = data;
                        break;
                    }
                }
                File.WriteAllLines(saveFile, lines);
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(saveFile, true))
                {
                    writer.WriteLine(data);
                }
            }
        }

        private void saveImage()
        {
            string data = currentIndex + ";";
            for (int i = 0; i < imageListBox.Items.Count; i++)
            {
                data += (imageListBox.GetItemChecked(i) ? "true" : "false") + ";";
            }

            // Escape description by enclosing it in quotes if it contains a semicolon
            string description = imageTextBox.Text;
            if (description.Contains(";"))
            {
                description = "\"" + description.Replace("\"", "\"\"") + "\"";
            }
            data += description;

            if (isImageSaved[currentIndex])
            {
                string[] lines = File.ReadAllLines(imageSaveFile);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] fields = lines[i].Split(';');
                    if (fields[0] == currentIndex.ToString())
                    {
                        lines[i] = data;
                        break;
                    }
                }
                File.WriteAllLines(imageSaveFile, lines);
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(imageSaveFile, true))
                {
                    writer.WriteLine(data);
                }
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void prev_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
            }
            else
            {
                currentIndex = imageFiles.Length - 1;
            }
            DisplayCurrentImage();
        }

        private void next_Click(object sender, EventArgs e)
        {
            if (currentIndex < imageFiles.Length - 1)
            {
                currentIndex++;
            }
            else
            {
                currentIndex = 0;
            }
            DisplayCurrentImage();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            savePatient();
        }

        private void saveImageAttributes_Button_Click(object sender, EventArgs e)
        {
            saveImage();
        }


        private void nextPatientButton_Click(object sender, EventArgs e)
        {
            this.Close();

            if (index < filesFromBefore.Length)
            {
                string filename = filesFromBefore[index];
                string[] strings = filename.Split('\\');

                PatientFlagger newForm = new PatientFlagger(strings.Last(), filesFromBefore, index + 1);

                newForm.Show();
            }
            else
            {
                MessageBox.Show("No more patients to show.");
            }
        }

       
    }

    class DataNode
    {
        public string name;
        public bool[] data;
        public string description;

        public DataNode(string name, bool[] data, string description)
        {
            this.name = name;
            this.data = data;
            this.description = description;
        }
    }
}
