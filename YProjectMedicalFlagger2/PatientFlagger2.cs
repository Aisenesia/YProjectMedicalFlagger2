﻿using System.Data;
using System.Text;
using System.Diagnostics;

namespace YProjectMedicalFlagger2
{
    public partial class PatientFlagger2 : Form
    {
        private static readonly string? filesPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "files");
        private string directoryPath;
        private List<string> files;

        // Settings
        private bool autoSaveWithNavigate = false;
        // private bool showSaveDialogWithNavigate = false;
        private const string patientSaveFileName = "Hastalar.csv";
        private const string imageSaveFileName = "Resimler.csv";
        private const string imageCategoryFileName = "Kategoriler.csv";

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
        private string imageCategoryFile;

        private readonly Dictionary<string, StringNode> dataMap = new();
        private readonly Dictionary<string, DataNode> imageMap = [];
        private string[] fileCategories;
        private string[] imageCategories;
        private ListViewItem.ListViewSubItem subItemToEdit;
        private TextBox editBox;
        private bool isDataChanged = false;

        // Set to false to disable backups
        private const bool takeBackups = true;

        public static string? FilesPath => filesPath;

        // take the file name and the files list from patient select form as parameters
        public PatientFlagger2(string fileName, string[] filesFromBefore, int index)
        {



            currentPatientName = fileName;
            this.filesFromBefore = filesFromBefore;
            this.index = index;

            InitializeComponent();
            InitializeFileList();
            FetchCategories();
            InitializeImageList();
            patientNameLabel.Text = currentPatientName;
            InitializeListBox();
            InitializeImageListBox();
            InitializeEditBox();

            CheckForSavedFile();
            SetIfSaved();
        }


        // Initialize the file list and the save file for accessing patient data
        private void InitializeFileList()
        {
            directoryPath = Path.Combine(FilesPath, currentPatientName);
            files = new List<string>(Directory.GetFiles(directoryPath));
            saveFile = Path.Combine(FilesPath, patientSaveFileName);
            imageSaveFile = Path.Combine(directoryPath, imageSaveFileName);
            imageCategoryFile = Path.Combine(FilesPath, imageCategoryFileName);

            EnsureFileExists(saveFile); // absolutely required
            EnsureFileExists(imageCategoryFile); // absolutely required
            
        }

        // Fetch image files from the directory and initialize the image list
        private void InitializeImageList()
        {
            imageFiles = files.Where(file => file.EndsWith(".png") || file.EndsWith(".jpg")).ToArray();
            isImageSaved = new bool[imageFiles.Length];
            currentIndex = 0;
        }

        // Display the current image in the picture box
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
                isDataChanged = false;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DisplayCurrentImage();
        }

        private void InitializeListBox() // get categories from the first line of the save file, initialize patient field names.
        {
            try
            {
                foreach (string data in fileCategories)
                {
                    patientListView.Items.Add(data);
                }
                patientListView.AutoArrange = true;
            }
            catch (Exception)
            {
                MessageBox.Show("No categories found in the save file.");
            }

        }

        private void InitializeImageListBox() // get categories from the first line of the categories file, initialize patient field names.
        {
            try
            {
                foreach (string data in imageCategories)
                {
                    imageListBox.Items.Add(data);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No categories found in the save file, use admin panel to create categories.");
            }
        }

        // Creates a dynamic textBox component that will be used to enter string data for patient fields
        private void InitializeEditBox()
        {
            editBox = new TextBox();
            editBox.Visible = false;
            this.editBox.LostFocus += new EventHandler(EditBox_LostFocus);
            this.editBox.KeyPress += new KeyPressEventHandler(EditBox_KeyPress);
            editBox.KeyPress += EditBox_KeyPress;

            Controls.Add(editBox);
        }

        // Access the save file and check if the current patient data is saved
        private void CheckForSavedFile()
        {
            dataMap.Clear();
            isSaved = false;

            using StreamReader reader = new(saveFile, Encoding.UTF8);
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
                    StringNode node = new(fields[0], data, fields[^1]);
                    dataMap.Add(fields[0], node);
                    break;
                }
            }
        }

        private void FetchCategories()
        {
            // patient categories
            string patientLine = File.ReadLines(saveFile).First();
            string[] firstLine = patientLine.Split(';');
            fileCategories = firstLine.Skip(1).Take(firstLine.Length - 2).ToArray();

            // image categories
            using StreamReader reader = new(imageCategoryFile, Encoding.UTF8);
            string? line = reader.ReadLine();
            if (line != null)
            {
                string[] fields = ParseCsvLine(line);

                imageCategories = fields.Skip(1).Take(fields.Length - 2).ToArray();
            }

        }

        private void CheckForSavedImageFile()
        {
            isImageSaved[currentIndex] = false;
            if (!EnsureFileExists(imageSaveFile, false))
            {
                File.Create(imageSaveFile).Close();
            }

            using StreamReader reader = new(imageSaveFile, Encoding.UTF8);

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] fields = ParseCsvLine(line); // Use the corrected ParseCsvLine method
                if (fields.Length > 0 && fields[0] == currentImageName)
                {
                    isImageSaved[currentIndex] = true;
                    bool[] data = new bool[fields.Length - 2];
                    for (int i = 1; i < fields.Length - 2; i++)
                    {
                        
                        // Attempt to parse each field to boolean safely
                        if (bool.TryParse(fields[i], out bool result))
                        {
                            data[i - 1] = result;
                        }
                        else
                        {
                            MessageBox.Show("Invalid boolean value found in the save file: " + fields[i]);
                            data[i - 1] = false; // Default to false if parsing fails
                        }
                    }
                    DataNode node = new(fields[0], data, fields[^1]);
                    if (!imageMap.TryAdd(currentImageName, node))
                    {
                        imageMap[currentImageName] = node;
                    }
                    break;
                }
            }
        }


        // ...

        private static void EnsureFileExists(string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("Dosya bulunamadı: " + path, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Environment.Exit(0);
            }
        }

        private static bool EnsureFileExists(string path, bool showMessageBox)
        {
            if (!File.Exists(path))
            {
                if (showMessageBox)
                {
                    MessageBox.Show("Dosya bulunamadı: " + path, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            return true;
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
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '\"')
                    {
                        // Handle escaped double quote within a quoted field
                        sb.Append('\"');
                        i++;
                    }
                    else
                    {
                        // Toggle the inQuotes flag
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == ';' && !inQuotes)
                {
                    // End of a field
                    fields.Add(sb.ToString());
                    sb.Clear();
                }
                else
                {
                    // Regular character within the field
                    sb.Append(c);
                }
            }

            // Add the last field
            fields.Add(sb.ToString());
            return fields.ToArray();
        }




        // Update the SetIfSaved method to clear the editBox when patient data is not found
        private void SetIfSaved()
        {
            if (isSaved && dataMap.TryGetValue(currentPatientName, out StringNode? value))
            {
                StringNode node = value;
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
                editBox.Clear(); // Clear the editBox for the current patient
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
                        //item.SubItems.RemoveAt(1);
                    }

                    if (item.SubItems.Count <= 1)
                    {
                        item.SubItems.Add("");
                    }
                }
                editBox.Clear(); // Clear the editBox when patient data is not found
            }
        }


        private void SetIfImageSaved()
        {
            // Check if the current image is marked as saved
            if (isImageSaved[currentIndex])
            {
                if (imageMap.TryGetValue(currentImageName, out DataNode? node))
                {
                    bool[] data = node.data;

                    // Ensure we do not exceed the bounds of the imageListBox items
                    int count = Math.Min(data.Length, imageListBox.Items.Count);
                    for (int i = 0; i < count; i++)
                    {
                        // Safely set the checked state
                        imageListBox.SetItemChecked(i, data[i]);
                    }

                    // Set the description text
                    imageTextBox.Text = node.description;
                }
            }
            else
            {
                // Ensure all items in the imageListBox are unchecked if the image is not saved
                for (int i = 0; i < imageListBox.Items.Count; i++)
                {
                    imageListBox.SetItemChecked(i, false);
                }
                // Clear the description text box
                imageTextBox.Text = string.Empty;
            }
        }


  private void SavePatient()
{
    // Prepare data to save
    string data = currentPatientName + ";";
    for (int i = 0; i < patientListView.Items.Count; i++)
    {
        ListViewItem item = patientListView.Items[i];
        string subItem = item.SubItems.Count > 1 ? item.SubItems[1].Text : "";
        data += subItem + ";";
    }

    string description = patientTextBox.Text;
    data += description;

    isDataChanged = false;
    TakeBackups(saveFile);

    bool isUpdated = false; // Flag to check if patient data is updated

    string[] lines = File.ReadAllLines(saveFile);
    for (int i = 0; i < lines.Length; i++)
    {
        string[] fields = ParseCsvLine(lines[i]);

        // If patient already exists, update their data
        if (fields[0] == currentPatientName)
        {
            lines[i] = data;
            isUpdated = true;
            break;
        }
    }

    if (isUpdated)
    {
        // Write updated lines back to file
        File.WriteAllLines(saveFile, lines);
    }
    else
    {
        // Add new patient data
        using (FileStream fs = new FileStream(saveFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
        {
            // Move to the end of the file
            fs.Seek(0, SeekOrigin.End);

            // Check if the file already ends with a newline
            if (fs.Length > 0)
            {
                fs.Seek(-1, SeekOrigin.End); // Move one character back from the end
                int lastByte = fs.ReadByte();
                
                // If the last byte is not a newline, write a newline
                if (lastByte != '\n')
                {
                    fs.WriteByte((byte)'\n');
                }
            }

            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.WriteLine(data);
            }
        }
    }
}
     private void SaveImage()
        {
            string data = currentImageName + ";";
            for (int i = 0; i < imageListBox.Items.Count; i++)
            {
                data += (imageListBox.GetItemChecked(i) ? "true" : "false") + ";";
            }

            string description = (imageTextBox.Text);
            data += description;

            TakeBackups(imageSaveFile);
            isDataChanged = false;

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

                using StreamWriter writer = new(imageSaveFile, true);
                writer.WriteLine(data);
            }
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

        private void LoadPatientData(string patientName, int patientIndex)
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
            isDataChanged = false;
        }

        private void NextPatientButton_Click(object sender, EventArgs e)
        {
            if (autoSaveWithNavigate)
            {
                SavePatient();
                SaveImage();
            }
           
            index++;

            // Check if we are at the last patient
            if (index >= filesFromBefore.Length)
            {
                MessageBox.Show("Gösterilecek hasta kalmadı.");
                index = filesFromBefore.Length - 1; // Ensure the index doesn't go out of bounds
                return;
            }

            // Clear existing form data
            ClearFormData();

            // Load the next patient data into the form
            string nextPatient = Path.GetFileNameWithoutExtension(filesFromBefore[index]);
            LoadPatientData(nextPatient, index);
        }

        private void PreviousPatientButton_Click(object sender, EventArgs e)
        {
            if (autoSaveWithNavigate) {
                SavePatient();
                SaveImage();
            }

            // Decrement the index to move to the previous patient

            index--;

            // Check if we are at the first patient
            if (index < 0)
            {
                MessageBox.Show("Gösterilecek hasta kalmadı.");
                index = 0; // Ensure the index doesn't go below 0
                return;
            }
            // Clear existing form data
            ClearFormData();
            // Load the previous patient data into the form
            string previousPatient = Path.GetFileNameWithoutExtension(filesFromBefore[index]);
            LoadPatientData(previousPatient, index);
        }

        private void SaveImageAttributes_Button_Click(object sender, EventArgs e)
        {
            SaveImage();
        }
        private void PreviousImageButton_Click(object sender, EventArgs e)
        {
            if (autoSaveWithNavigate)
            {
                SaveImage();
            }
            currentIndex = currentIndex > 0 ? currentIndex - 1 : imageFiles.Length - 1;
            DisplayCurrentImage();
        }
        private void NextImageButton_Click(object sender, EventArgs e)
        {
            if (autoSaveWithNavigate)
            {
                SaveImage();
            }
            currentIndex = currentIndex < imageFiles.Length - 1 ? currentIndex + 1 : 0;

            DisplayCurrentImage();
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                FullScreenImageForm fullScreenImageForm = new(pictureBox1.Image);
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
        private void patientListView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (patientListView.SelectedItems.Count > 0)
                {
                    ListViewItem selectedItem = patientListView.SelectedItems[0];
                    if (selectedItem.SubItems.Count > 1)
                    {
                        subItemToEdit = selectedItem.SubItems[1];
                        Rectangle subItemBounds = subItemToEdit.Bounds;
                        Point subItemLocation = patientListView.PointToScreen(subItemBounds.Location);
                        Point editBoxLocation = this.PointToClient(subItemLocation);
                        editBox.Bounds = new Rectangle(editBoxLocation, subItemBounds.Size);
                        editBox.Text = subItemToEdit.Text;
                        editBox.Visible = true;
                        editBox.BringToFront();
                        editBox.Focus();
                    }
                    else
                    {
                        MessageBox.Show("No subitems found for the selected item. count: " + selectedItem.SubItems.Count);
                    }
                }
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
                if (editBox.Text != subItemToEdit.Text)
                {
                    subItemToEdit.Text = editBox.Text;
                    isDataChanged = true;
                }


                editBox.Visible = false;
                subItemToEdit = null;

            }
        }


        private void PatientFlagger2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isDataChanged)
            {
                SaveDialog();
            }


            Form? form = Application.OpenForms["PatientSelect"];
            if (form != null)
            {
                form.Show();
            }


        }

        private void SaveDialog()
        {
            DialogResult dialogResult = MessageBox.Show("Değişiklikler kaydedilsin mi?", "Kayıt", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                SavePatient();
                SaveImage();
            }

        }


        private void imageListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            isDataChanged = true;
        }

        private void patientTextBox_TextChanged(object sender, EventArgs e)
        {
            isDataChanged = true;
        }

        private void imageTextBox_TextChanged(object sender, EventArgs e)
        {
            isDataChanged = true;
        }

        private void TakeBackups(string save)
        {
            if (takeBackups)
            {
                string backupFile = save + ".bak";
                if (File.Exists(backupFile))
                {
                    File.Delete(backupFile);
                }
                File.Copy(save, backupFile);
            }
        }

        private void patientListView_Resize(object sender, EventArgs e)
        {
            int totalWidth = patientListView.ClientSize.Width;
            patientListView.Columns[0].Width = totalWidth / 2;
            patientListView.Columns[1].Width = totalWidth / 2;
        }
    }

    public class StringNode(string name, string[] data, string description)
    {
        public string[] data = data;
        public string description = description;
        public string name = name;
    }
}


