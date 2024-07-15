using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace YProjectMedicalFlagger2
{
    public partial class PatientFlagger : Form
    {
        private static string filesPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "files");
        private string directoryPath;
        private List<string> files;

        private string[] filesFromBefore;
        private int index;

        private string currentPatientName;
        private bool isSaved;
        private bool[] currentImageIsSaved;

        private string[] imageFiles;
        private int currentIndex;
        private string saveFile;
        private string currentImageFile;

        Dictionary<string, dataNode> dataMap;
        Dictionary<string, dataNode> imageMap;
        private string[] categories;
        private string[] imageCategories;

        private Dictionary<string, List<Point>> pointMap;
        private bool firstClickDone = false;

        public PatientFlagger(string fileName, string[] files, int index)
        {
            currentPatientName = fileName;
            this.filesFromBefore = files;
            this.index = index;

            InitializeComponent();
            InitializeFileList();
            InitializeImageList();

            patientNameLabel.Text = currentPatientName;

            saveFile = Path.Combine(filesPath, "saves.csv");
            categories = new string[] { };
            imageCategories = new string[] { };

            InitalizeListBox(saveFile, ref categories, patientListBox);
            InitalizeListBox(Path.Combine(filesPath, currentPatientName, "imageAttributes.csv"), ref imageCategories, imageListBox);

            checkForSavedFile();
            checkForSavedFilePicture();
            setIfSaved();
        }

        private void InitializeFileList()
        {
            directoryPath = Path.Combine(filesPath, currentPatientName);
            files = Directory.GetFiles(directoryPath).ToList();
        }

        private void InitializeImageList()
        {
            imageFiles = files.Where(file => file.EndsWith(".png") || file.EndsWith(".jpg")).ToArray();
            currentIndex = 0;
        }

        private void DisplayCurrentImage()
        {
            if (imageFiles != null && imageFiles.Length > 0 && currentIndex >= 0 && currentIndex < imageFiles.Length)
            {
                pictureBox1.ImageLocation = imageFiles[currentIndex];
                indexLabel.Text = (currentIndex + 1) + "/" + imageFiles.Length;
                checkForSavedFile2(Path.Combine(filesPath, currentPatientName, "imageAttributes.csv"));
                setIfSavedImage();
                currentImageFile = imageFiles[currentIndex];
                indexLabel.Show();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = "Ek notlar:";
            DisplayCurrentImage();
        }

        private void InitalizeListBox(string file, ref string[] categoryList, CheckedListBox checkBox)
        {
            try
            {
                string line = File.ReadLines(file).First();
                string[] firstLine = line.Split(';');
                categoryList = firstLine.Skip(1).Take(firstLine.Length - 2).ToArray();

                foreach (string data in categoryList)
                {
                    checkBox.Items.Add(data);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("No categories found in the save file, use admin panel to create categories: " + file);
            }
        }

        private void checkForSavedFile()
        {
            dataMap = new Dictionary<string, dataNode>();

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
                            dataNode node = new dataNode(fields[0], data, fields[fields.Length - 1]);
                            dataMap.Add(fields[0], node);
                            break;
                        }
                    }
                }
            }
            else
            {
                File.Create(saveFile).Dispose();
            }
        }

        private void checkForSavedFile2(string file)
        {
            imageMap = new Dictionary<string, dataNode>();

            if (File.Exists(file))
            {
                currentImageIsSaved[currentIndex] = false;

                using (StreamReader reader = new StreamReader(file))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] fields = ParseCsvLine(line);

                        if (fields.Length > 0 && fields[0] == currentIndex.ToString())
                        {
                            currentImageIsSaved[currentIndex] = true;
                            bool[] data = new bool[fields.Length - 2];
                            for (int i = 1; i < fields.Length - 1; i++)
                            {
                                data[i - 1] = Convert.ToBoolean(fields[i]);
                            }
                            dataNode node = new dataNode(fields[0], data, fields[fields.Length - 1]);
                            imageMap.Add(fields[0], node);
                            break;
                        }
                    }
                }
            }
            else
            {
                File.Create(saveFile).Dispose();
            }
        }

        private void checkForSavedFilePicture()
        {
            currentImageIsSaved = new bool[imageFiles.Length];

            string imageAttributesFile = Path.Combine(filesPath, currentPatientName, "imageAttributes.csv");
            if (File.Exists(imageAttributesFile))
            {
                string[] lines = File.ReadAllLines(imageAttributesFile);

                if (currentIndex + 1 < lines.Length)
                {
                    currentImageIsSaved[currentIndex] = true;
                    string line = lines[currentIndex + 1];
                    string[] fields = ParseCsvLine(line);

                    int numberOfCategories = fields.Length - 2;

                    if (numberOfCategories + 2 != fields.Length)
                    {
                        MessageBox.Show($"Unexpected number of fields in line: {line}");
                        return;
                    }

                    for (int i = 0; i < numberOfCategories; i++)
                    {
                        bool isChecked = false;
                        if (Boolean.TryParse(fields[i + 1], out isChecked))
                        {
                            imageListBox.SetItemChecked(i, isChecked);
                        }
                        else
                        {
                            MessageBox.Show($"Invalid boolean value in field {i + 1}: {fields[i + 1]}");
                        }
                    }

                    richTextBox2.Text = fields[fields.Length - 1];
                }
                else
                {
                    currentImageIsSaved[currentIndex] = false;

                    string newLine = currentIndex + ";";
                    for (int i = 0; i < imageListBox.Items.Count; i++)
                    {
                        newLine += "false;";
                    }
                    newLine += "No description";
                    using (StreamWriter writer = new StreamWriter(imageAttributesFile, true))
                    {
                        writer.WriteLine(newLine);
                    }
                }
            }
            else
            {
                MessageBox.Show("No saved image attributes found for the current picture.");
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
                dataNode node = dataMap[currentPatientName];
                bool[] data = node.data;
                for (int i = 0; i < data.Length; i++)
                {
                    patientListBox.SetItemChecked(i, data[i]);
                }
                richTextBox1.Text = node.description;
            }
        }

        private void setIfSavedImage()
        {
            if (currentImageIsSaved[currentIndex])
            {
                dataNode node = imageMap[currentIndex.ToString()];
                bool[] data = node.data;
                for (int i = 0; i < data.Length; i++)
                {
                    imageListBox.SetItemChecked(i, data[i]);
                }
                richTextBox2.Text = node.description;
            }
            else
            {
                for (int i = 0; i < imageListBox.Items.Count; i++)
                {
                    imageListBox.SetItemChecked(i, false);
                }
                richTextBox2.Clear();
            }
        }

        private void savePatientBtn_Click(object sender, EventArgs e)
        {
            SaveData(saveFile, currentPatientName, patientListBox, richTextBox1.Text);
            isSaved = true;
        }

        private void saveImageBtn_Click(object sender, EventArgs e)
        {
            string imageAttributesFile = Path.Combine(filesPath, currentPatientName, "imageAttributes.csv");
            SaveData(imageAttributesFile, currentIndex.ToString(), imageListBox, richTextBox2.Text);
            currentImageIsSaved[currentIndex] = true;
        }

        private void SaveData(string file, string name, CheckedListBox checkBox, string description)
        {
            List<string> newLines = new List<string>();
            bool entryFound = false;

            if (File.Exists(file))
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] fields = ParseCsvLine(line);

                        if (fields[0] == name)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append(name + ";");

                            for (int i = 0; i < checkBox.Items.Count; i++)
                            {
                                bool isChecked = checkBox.GetItemChecked(i);
                                sb.Append(isChecked + ";");
                            }

                            sb.Append("\"" + description +  "\"");
                            newLines.Add(sb.ToString());
                            entryFound = true;
                        }
                        else
                        {
                            newLines.Add(line);
                        }
                    }
                }
            }

            if (!entryFound)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(name + ";");

                for (int i = 0; i < checkBox.Items.Count; i++)
                {
                    bool isChecked = checkBox.GetItemChecked(i);
                    sb.Append(isChecked + ";");
                }

                sb.Append(description);
                newLines.Add(sb.ToString());
            }

            using (StreamWriter writer = new StreamWriter(file))
            {
                foreach (var newLine in newLines)
                {
                    writer.WriteLine(newLine);
                }
            }
        }

        private void previousImageBtn_Click(object sender, EventArgs e)
        {
            currentIndex = (currentIndex - 1 + imageFiles.Length) % imageFiles.Length;
            DisplayCurrentImage();
        }

        private void nextImageBtn_Click(object sender, EventArgs e)
        {
            currentIndex = (currentIndex + 1) % imageFiles.Length;
            DisplayCurrentImage();
        }

        private void next_Click(object sender, EventArgs e)
        {
            currentIndex = (currentIndex + 1) % imageFiles.Length;
            DisplayCurrentImage();
        }

        private void prev_Click(object sender, EventArgs e)
        {
            currentIndex = (currentIndex - 1 + imageFiles.Length) % imageFiles.Length;
            DisplayCurrentImage();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveData(saveFile, currentPatientName, patientListBox, richTextBox1.Text);
            isSaved = true;
        }

        private void nextPatientButton_Click(object sender, EventArgs e)
        {
            // Update the index to point to the next patient
            index = (index + 1) % filesFromBefore.Length;
            currentPatientName = filesFromBefore[index];

            // Clear the previous data and reset controls
            ClearForm();

            // Reload and display the new patient's data
            InitializeFileList();
            InitializeImageList();

            patientNameLabel.Text = currentPatientName;

            checkForSavedFile();
            setIfSaved();

            checkForSavedFilePicture();
            DisplayCurrentImage();
        }

        private void ClearForm()
        {
            // Clear patient list box
            patientListBox.Items.Clear();

            // Clear image list box
            imageListBox.Items.Clear();

            // Clear rich text boxes
            richTextBox1.Clear();
            richTextBox2.Clear();

            // Clear picture box
            pictureBox1.Image = null;

            // Hide index label
            indexLabel.Hide();
        }


        private void saveImageAttributesButton_Click(object sender, EventArgs e)
        {
            string imageAttributesFile = Path.Combine(filesPath, currentPatientName, "imageAttributes.csv");
            SaveData(imageAttributesFile, currentIndex.ToString(), imageListBox, richTextBox2.Text);
            currentImageIsSaved[currentIndex] = true;
        }
    }

    public class dataNode
    {
        public string name;
        public bool[] data;
        public string description;

        public dataNode(string name, bool[] data, string description)
        {
            this.name = name;
            this.data = data;
            this.description = description;
        }
    }
}
