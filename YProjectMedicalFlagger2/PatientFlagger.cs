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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

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
        //private Point lastClickLocation;
        private bool firstClickDone = false;

        public PatientFlagger(String fileName, string[] files, int index)
        {
            currentPatientName = fileName;
            this.filesFromBefore = files;
            this.index = index;

            InitializeComponent();
            InitializeFileList();
            InitializeImageList();
            patientNameLabel.Text = currentPatientName;
            //InitalizeListBox();

            InitalizeListBox(saveFile, categories, patientListBox);
            InitalizeListBox(filesPath + "\\" +  currentPatientName + "\\" + "imageAttributes.csv", imageCategories, imageListBox);

            checkForSavedFile();
            checkForSavedFilePicture();

            

            //InitalizePointMap();
            
            setIfSaved();
        }

        private void InitializeFileList()
        {
            directoryPath = filesPath + "\\" + currentPatientName;
            files = Directory.GetFiles(directoryPath).ToList();
            saveFile = filesPath + "\\saves.csv";
        }

        private void InitializeImageList()
        {
            imageFiles = files.Where(file => file.EndsWith(".png") || file.EndsWith(".jpg")).ToArray();
            currentIndex = 0;
            //lastClickLocation = new Point();
        }

        private void DisplayCurrentImage()
        {
            if (imageFiles.Length > 0 && currentIndex >= 0 && currentIndex < imageFiles.Length)
            {
                pictureBox1.ImageLocation = imageFiles[currentIndex];
                indexLabel.Text = (currentIndex + 1) + "/" + imageFiles.Length;
                checkForSavedFilePicture();

                // check for saved file for current picture, if it exists load it into imageListBox

                currentImageFile = imageFiles[currentIndex];
                indexLabel.Show();
            }
            //lastClickLocation = new Point();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = "Ek notlar:";
            DisplayCurrentImage();
        }

        private void InitalizeListBox(string file, string[] categoryList, CheckedListBox checkBox)
        {
            string line;
            try
            {
                line = File.ReadLines(file).First();
                Debug.WriteLine("First line: " + file);
            }
            catch (Exception e)
            {
                MessageBox.Show("No categories found in the save file, use admin panel to create categories: " + file);
                return;
            }

            string[] firstLine = line.Split(';');
            categoryList = firstLine.Skip(1).Take(firstLine.Length - 2).ToArray(); // skip the first element, which is the image name, and last element, which is the description

            foreach (string data in categoryList)
            {
                checkBox.Items.Add(data);
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
                File.Create(saveFile).Dispose(); // Ensure the file is properly closed after creation
            }
        }

      /*  private void checkForSavedFile2(string file)
        {
            imageMap = new Dictionary<string, dataNode>();

            if (File.Exists(file))
            {
                //isSaved = false;

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
        } */

        private void checkForSavedFilePicture()
        {
            currentImageIsSaved = new bool[imageListBox.Items.Count];

            string imageAttributesFile = filesPath + "\\" + currentPatientName + "\\imageAttributes.csv";
            if (File.Exists(imageAttributesFile))
            {
                // Load the saved image attributes
                string[] lines = File.ReadAllLines(imageAttributesFile);

                if (currentIndex + 1 < lines.Length) // Ensure currentIndex + 1 is within bounds to skip the header
                {
                    currentImageIsSaved[currentIndex] = true;
                    string line = lines[currentIndex + 1]; // Skip header by accessing currentIndex + 1
                    string[] fields = ParseCsvLine(line);

                    //
                     // 0 is the first line which contains the categories

                    // The number of categories is fields.Length - 2 (excluding index and description)
                    int numberOfCategories = fields.Length - 2;

                    // Ensure that we have the correct number of fields to process
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
                            imageListBox.SetItemChecked(i, isChecked); // Adjusted index by +1 to skip index
                        }
                        else
                        {
                            MessageBox.Show($"Invalid boolean value in field {i + 1}: {fields[i + 1]}");
                        }
                    }

                    // Update the richTextBox2 with the description, which is the last element
                    richTextBox2.Text = fields[fields.Length - 1];
                }
                else
                {
                    currentImageIsSaved[currentIndex] = false;
                    // Index out of range, no saved data for this image
                    // create a new line for the current image
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

                    //MessageBox.Show("No saved image attributes found for the current picture.");
                }
            }
            else
            {
                // No saved image attributes found
                MessageBox.Show("No saved image attributes found for the current picture.");
            }
        }

        private void initializeImageListBox(string line) {
        
            string[] categories = line.Split(';').Skip(1).Take(line.Length - 2).ToArray();
            foreach (string data in categories)
            {
                imageListBox.Items.Add(data);
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

            Debug.WriteLine("Save button clicked: " + saveFile);
            savePatient(currentPatientName, patientListBox, richTextBox1, saveFile, isSaved);
        }

        private void saveImageAttributesButton_Click(object sender, EventArgs e)
        {
            string file = filesPath + "\\" +currentPatientName+ "\\imageAttributes.csv";
            MessageBox.Show("Saving image attributes to " + file);
            savePatient(currentPatientName + "_" + currentIndex, patientListBox, richTextBox2, file, false);
        }

        private void savePatient(string name, CheckedListBox checkBox, RichTextBox box, string filename, bool saveStatus)
        {
            string data = name + ";";
            for (int i = 0; i < checkBox.Items.Count; i++)
            {
                data += (checkBox.GetItemChecked(i) ? "true" : "false") + ";";
            }

            // Escape description by enclosing it in quotes if it contains a semicolon
            string description = box.Text;
            if (description.Contains(";"))
            {
                description = "\"" + description.Replace("\"", "\"\"") + "\"";
            }
            data += description;

            if (saveStatus)
            {
                string[] lines = File.ReadAllLines(filename);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] fields = lines[i].Split(';');
                    if (fields[0] == name)
                    {
                        lines[i] = data;
                        break;
                    }
                }
                File.WriteAllLines(filename, lines);
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(filename, true))
                {
                    writer.WriteLine(data);
                }
            }
        }




        private void nextPatientButton_Click(object sender, EventArgs e)
        {
            this.Close();

            if (index < filesFromBefore.Length)
            {
                string filename = filesFromBefore[index];
                String[] strings = filename.Split('\\');

                PatientFlagger newForm = new PatientFlagger(strings.Last(), filesFromBefore, index + 1);

                newForm.Show();
            }
            else
            {
                MessageBox.Show("No more patients to show.");
            }
        }

       
    }

    class dataNode
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
