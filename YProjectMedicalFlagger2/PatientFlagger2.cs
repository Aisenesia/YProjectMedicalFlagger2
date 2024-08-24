using System.Data;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace YProjectMedicalFlagger2
{
    public partial class PatientFlagger2 : Form
    {
        private static readonly string filesPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "files");
        private string directoryPath;
        private List<string> files;

        private readonly string[] filesFromBefore; // All the files in the directory
        private int index; // Current patient's index in the files array

        private string currentPatientName;
        private bool isSaved;
        private bool[] isImageSaved;

        private string[] imageFiles;
        private int currentIndex;
        private string currentImageName;
        private string saveFile;
        private string imageSaveFile;

        private Dictionary<string, StringNode> dataMap = new();
        private readonly Dictionary<string, DataNode> imageMap = new();
        private string[] categories;
        private ListViewItem.ListViewSubItem subItemToEdit;
        private TextBox editBox;

        public PatientFlagger2(string fileName, string[] filesFromBefore, int index)
        {
            currentPatientName = fileName;
            this.filesFromBefore = filesFromBefore;
            this.index = index;

            InitializeComponent();
            InitializeFileList();
            InitializeImageList();
            patientNameLabel.Text = currentPatientName;
            InitializeListBox();
            InitializeImageListBox();
            InitializeEditBox();

            CheckForSavedFile();
            SetIfSaved();
        }
        private void InitializeFileList()
        {
            directoryPath = Path.Combine(filesPath, currentPatientName);
            files = new List<string>(Directory.GetFiles(directoryPath));
            saveFile = Path.Combine(filesPath, "saves.csv");
            imageSaveFile = Path.Combine(directoryPath, "imageAttributes.csv");

            EnsureFileExists(saveFile);
            EnsureFileExists(imageSaveFile);
        }

        private void InitializeImageList()
        {
            imageFiles = files.Where(file => file.EndsWith(".png") || file.EndsWith(".jpg")).ToArray();
            isImageSaved = new bool[imageFiles.Length];
            currentIndex = 0;
        }

        private void DisplayCurrentImage()
        {
            if (imageFiles.Length > 0 && currentIndex >= 0 && currentIndex < imageFiles.Length)
            {
                pictureBox1.ImageLocation = imageFiles[currentIndex];
                indexLabel.Text = $"{currentIndex + 1}/{imageFiles.Length}";
                indexLabel.Show();
                currentImageName = Path.GetFileName(imageFiles[currentIndex]);

                CheckForSavedImageFile();
                SetIfImageSaved();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = "Ek notlar:";
            DisplayCurrentImage();
        }

        private void InitializeListBox()
        {
            try
            {
                string line = File.ReadLines(saveFile).First();
                string[] firstLine = line.Split(';');
                categories = firstLine.Skip(1).Take(firstLine.Length - 2).ToArray();

                foreach (string data in categories)
                {
                    patientListView.Items.Add(data);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No categories found in the save file.");
            }

        }

        private void InitializeImageListBox()
        {
            try
            {
                string line = File.ReadLines(imageSaveFile).First();
                string[] firstLine = line.Split(';');
                categories = firstLine.Skip(1).Take(firstLine.Length - 2).ToArray();

                foreach (string data in categories)
                {
                    imageListBox.Items.Add(data);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No categories found in the save file, use admin panel to create categories.");
            }
        }

        private void InitializeEditBox()
        {
            editBox = new TextBox();
            editBox.Visible = false;
            this.editBox.LostFocus += new EventHandler(EditBox_LostFocus);
            this.editBox.KeyPress += new KeyPressEventHandler(EditBox_KeyPress);
            editBox.KeyPress += EditBox_KeyPress;

            Controls.Add(editBox);
        }

        private void CheckForSavedFile()
        {
            dataMap.Clear();
            isSaved = false;

            using StreamReader reader = new StreamReader(saveFile, Encoding.UTF8);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] fields = ParseCsvLine(line);

                if (fields.Length > 0 && fields[0] == currentPatientName)
                {
                    isSaved = true;
                    string[] data = new string[fields.Length - 2];
                    for (int i = 1; i < fields.Length - 1; i++)
                    {
                        data[i - 1] = fields[i];
                    }
                    StringNode node = new StringNode(fields[0], data, fields[^1]);
                    dataMap.Add(fields[0], node);
                    break;
                }
            }
        }

        private void CheckForSavedImageFile()
        {
            isImageSaved[currentIndex] = false;

            using StreamReader reader = new StreamReader(imageSaveFile, Encoding.UTF8);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] fields = ParseCsvLine(line);

                if (fields.Length > 0 && fields[0] == currentImageName)
                {
                    isImageSaved[currentIndex] = true;
                    bool[] data = new bool[fields.Length - 2];
                    for (int i = 1; i < fields.Length - 1; i++)
                    {
                        data[i - 1] = Convert.ToBoolean(fields[i]);
                    }
                    DataNode node = new DataNode(fields[0], data, fields[^1]);
                    if (!imageMap.TryAdd(currentImageName, node))
                    {
                        imageMap[currentImageName] = node;
                    }
                    break;
                }
            }
        }

        private static void EnsureFileExists(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
        }

        private static string[] ParseCsvLine(string line)
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

        private void SetIfSaved()
        {
            if (isSaved && dataMap.ContainsKey(currentPatientName))
            {
                StringNode node = dataMap[currentPatientName];
                string[] data = node.data;
                for (int i = 0; i < data.Length; i++)
                {
                    // Ensure the list has enough items before setting them as checked
                    if (i < patientListView.Items.Count)
                    {
                        //patientListView.SetItemChecked(i, data[i]);
                        patientListView.Items[i].SubItems.Add(data[i]);
                        //TODO: SetItemChecked Here.
                    }
                }
                patientTextBox.Text = node.description;
            }
            else
            {
                // Handle case where patient data is not found
                patientTextBox.Clear();
                //patientListView.ClearSelected();
                foreach (ListViewItem item in patientListView.Items)
                {
                    if (item.SubItems.Count > 1)
                    {
                        item.SubItems.RemoveAt(1);
                    }
                }
            }
        }


        private void SetIfImageSaved()
        {
            if (isImageSaved[currentIndex])
            {
                DataNode node = imageMap[currentImageName];
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
                imageTextBox.Text = string.Empty;
            }
        }

        private void SavePatient()
        {
            string data = currentPatientName + ";";
            for (int i = 0; i < patientListView.Items.Count; i++)
            {
                //data += (patientListView.GetItemChecked(i) ? "true" : "false") + ";";
                ListViewItem item = patientListView.Items[i];
                string subItem = item.SubItems.Count > 1 ? item.SubItems[1].Text : "";
                data += (subItem + ";");
            }

            string description = EscapeCsvField(patientTextBox.Text);
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
                using StreamWriter writer = new StreamWriter(saveFile, true);
                writer.WriteLine(data);
            }
        }

        private void SaveImage()
        {
            string data = currentImageName + ";";
            for (int i = 0; i < imageListBox.Items.Count; i++)
            {
                data += (imageListBox.GetItemChecked(i) ? "true" : "false") + ";";
            }

            string description = EscapeCsvField(imageTextBox.Text);
            data += description;

            if (isImageSaved[currentIndex])
            {
                string[] lines = File.ReadAllLines(imageSaveFile);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] fields = lines[i].Split(';');
                    if (fields[0] == currentImageName)
                    {
                        lines[i] = data;
                        break;
                    }
                }
                File.WriteAllLines(imageSaveFile, lines);
            }
            else
            {
                using StreamWriter writer = new StreamWriter(imageSaveFile, true);
                writer.WriteLine(data);
            }
        }

        private static string EscapeCsvField(string field)
        {
            if (field.Contains(';'))
            {
                return "\"" + field.Replace("\"", "\"\"") + "\"";
            }
            return field;
        }

        private void SavePatient_Button_Click(object sender, EventArgs e)
        {
            SavePatient();
        }

        private void ClearFormData()
        {
            // Clear list boxes and text boxes
            patientListView.Items.Clear();
            imageListBox.Items.Clear();
            patientTextBox.Clear();
            imageTextBox.Clear();

            // Reset other controls as needed
            pictureBox1.Image = null;
            indexLabel.Hide();
            isSaved = false;
            isImageSaved = null; // Reset the saved image state
            currentIndex = 0;

            // Clear dictionaries if needed
            dataMap.Clear();
            imageMap.Clear();
        }

        private void LoadPatientData(string patientName, string[] files, int patientIndex)
        {
            // Update class fields with the new patient info
            currentPatientName = patientName;
            this.index = patientIndex;

            // Reinitialize file lists and load data
            InitializeFileList();
            InitializeImageList();

            // Fetch categories for the new patient
            InitializeListBox();   // Reload the categories for the new patient
            InitializeImageListBox();

            // Update labels and display new patient data
            patientNameLabel.Text = currentPatientName;
            DisplayCurrentImage();
            CheckForSavedFile();
            SetIfSaved();
        }

        private void NextPatientButton_Click(object sender, EventArgs e)
        {
            // Save current patient data before moving to the next one
            //SavePatient();
            //SaveImage();

            // Increment the index to move to the next patient

            index++;

            // Check if we are at the last patient
            if (index >= filesFromBefore.Length)
            {
                MessageBox.Show("No more patients to show.");
                index = filesFromBefore.Length - 1; // Ensure the index doesn't go out of bounds
                return;
            }

            // Clear existing form data
            ClearFormData();

            // Load the next patient data into the form
            string nextPatient = Path.GetFileNameWithoutExtension(filesFromBefore[index]);
            LoadPatientData(nextPatient, filesFromBefore, index);

        }

        private void PreviousPatientButton_Click(object sender, EventArgs e)
        {
            // Save current patient data before moving to the previous one
            //SavePatient();
            SaveImage();

            // Decrement the index to move to the previous patient

            index--;

            // Check if we are at the first patient
            if (index < 0)
            {
                MessageBox.Show("No previous patients to show.");
                index = 0; // Ensure the index doesn't go below 0
                return;
            }

            // Clear existing form data
            ClearFormData();

            // Load the previous patient data into the form
            string previousPatient = Path.GetFileNameWithoutExtension(filesFromBefore[index]);
            LoadPatientData(previousPatient, filesFromBefore, index);

        }


        private void SaveImageAttributes_Button_Click(object sender, EventArgs e)
        {
            SaveImage();
        }

        private void PreviousImageButton_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                SaveImage();
                currentIndex--;
                DisplayCurrentImage();
            }
        }

        private void NextImageButton_Click(object sender, EventArgs e)
        {
            if (currentIndex < imageFiles.Length - 1)
            {
                //SaveImage();
                currentIndex++;
                DisplayCurrentImage();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                FullScreenImageForm fullScreenImageForm = new FullScreenImageForm(pictureBox1.Image);
                fullScreenImageForm.ShowDialog(); // Show the form as a modal dialog
            }
        }


        private void ListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo hitTestInfo = patientListView.HitTest(e.Location);
            if (hitTestInfo.SubItem != null && hitTestInfo.Item.SubItems.IndexOf(hitTestInfo.SubItem) == 1)
            {
                subItemToEdit = hitTestInfo.SubItem;
                Rectangle subItemBounds = subItemToEdit.Bounds;
                Point subItemLocation = patientListView.PointToScreen(subItemBounds.Location);
                Point editBoxLocation = this.PointToClient(subItemLocation);
                editBox.Bounds = new Rectangle(editBoxLocation, subItemBounds.Size);
                editBox.Text = subItemToEdit.Text;
                editBox.Visible = true;
                editBox.BringToFront();
                editBox.Focus();
            }
        }

        private void EditBox_LostFocus(object sender, EventArgs e)
        {
            UpdateSubItem();
        }

        private void EditBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                UpdateSubItem();
            }
        }
        

        private void UpdateSubItem()
        {
            if (subItemToEdit != null)
            {
                subItemToEdit.Text = editBox.Text;
                editBox.Visible = false;
                subItemToEdit = null;
            }
        }

        
    }

    public class StringNode
    {
        public string[] data;
        public string description;
        public string name;

        public StringNode(string name, string[] data, string description)
        {
            this.name = name;
            this.data = data;
            this.description = description;
        }
    }


}


