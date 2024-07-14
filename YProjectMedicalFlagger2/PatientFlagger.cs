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

        private string[] imageFiles;
        private int currentIndex;
        private string saveFile;

        Dictionary<string, dataNode> dataMap;
        private string[] categories;

        private Dictionary<string, List<Point>> pointMap;
        private Point lastClickLocation;
        private bool firstClickDone = false;

        public PatientFlagger(String fileName, string[] files, int index)
        {
            currentPatientName = fileName;
            this.filesFromBefore = files;
            this.index = index;

            InitializeComponent();
            InitializeFileList();
            InitializeImageList();
            InitalizeListBox();
            InitalizePointMap();
            checkForSavedFile();
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
            lastClickLocation = new Point();
        }

        private void DisplayCurrentImage()
        {
            if (imageFiles.Length > 0 && currentIndex >= 0 && currentIndex < imageFiles.Length)
            {
                pictureBox1.ImageLocation = imageFiles[currentIndex];
                indexLabel.Text = (currentIndex + 1) + "/" + imageFiles.Length;
                indexLabel.Show();
            }
            lastClickLocation = new Point();
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
                checkedListBox1.Items.Add(data);
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
                    checkedListBox1.SetItemChecked(i, data[i]);
                }
                richTextBox1.Text = node.description;
            }
        }

        private void savePatient()
        {
            string data = currentPatientName + ";";
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                data += (checkedListBox1.GetItemChecked(i) ? "true" : "false") + ";";
            }

            // Escape description by enclosing it in quotes if it contains a semicolon
            string description = richTextBox1.Text;
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

        private void label1_Click(object sender, EventArgs e) { }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;

            // Calculate the actual displayed image size
            Size imgSize = pictureBox1.Image.Size;
            Size boxSize = pictureBox1.ClientSize;

            float ratioX = (float)boxSize.Width / imgSize.Width;
            float ratioY = (float)boxSize.Height / imgSize.Height;
            float ratio = Math.Min(ratioX, ratioY);

            Size displayedImageSize = new Size((int)(imgSize.Width * ratio), (int)(imgSize.Height * ratio));

            // Calculate padding
            int padX = (boxSize.Width - displayedImageSize.Width) / 2;
            int padY = (boxSize.Height - displayedImageSize.Height) / 2;

            // Adjust the click coordinates to match the image coordinates
            int adjustedX = (int)((me.X - padX) / ratio);
            int adjustedY = (int)((me.Y - padY) / ratio);

            Point coordinates = new Point(adjustedX, adjustedY);

            //Debug.WriteLine("X: " + coordinates.X + " Y: " + coordinates.Y);
            if (!pointMap.ContainsKey(imageFiles[currentIndex]))
            {
                pointMap[imageFiles[currentIndex]] = new List<Point>();
            }
            pointMap[imageFiles[currentIndex]].Add(coordinates);

            // Draw the line
            if (lastClickLocation.IsEmpty)
            {
                lastClickLocation = coordinates;
            }
            else
            {
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    g.DrawLine(new Pen(Color.Red, 2), lastClickLocation, coordinates);
                }
                pictureBox1.Invalidate();
                lastClickLocation = coordinates;
            }
        }

        private void InitalizePointMap()
        {
            pointMap = new Dictionary<string, List<Point>>();
            foreach (string image in imageFiles)
            {
                pointMap.Add(image, new List<Point>());
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

        private void reMarkButton_Click(object sender, EventArgs e)
        {
            if (imageFiles.Length > 0 && currentIndex >= 0 && currentIndex < imageFiles.Length)
            {
                // Clear the points for the current image
                pointMap[imageFiles[currentIndex]].Clear();

                // Reset last click location
                lastClickLocation = new Point();

                // Redraw the image without lines
                pictureBox1.ImageLocation = imageFiles[currentIndex];
                pictureBox1.Load();
            }
        }

        private void saveMarksButton_Click(object sender, EventArgs e)
        {
            if (imageFiles.Length > 0 && currentIndex >= 0 && currentIndex < imageFiles.Length)
            {
                string originalFilePath = imageFiles[currentIndex];
                string savePath = Path.Combine(Path.GetDirectoryName(originalFilePath), "coordinates_" + Path.GetFileNameWithoutExtension(originalFilePath) + ".csv");

                using (StreamWriter writer = new StreamWriter(savePath))
                {
                    // Write header
                    writer.WriteLine("X, Y");

                    // Write points
                    if (!pointMap.ContainsKey(imageFiles[currentIndex]))
                    {
                        MessageBox.Show("No points marked on this image.");
                        return;
                    }
                    foreach (Point point in pointMap[imageFiles[currentIndex]])
                    {
                        //Debug.WriteLine("Writing point:" + point.X + " , " + point.Y);
                        writer.WriteLine($"{point.X}, {point.Y}");
                    }
                    writer.Flush(); // Ensure that all data is written to the file
                }

                MessageBox.Show("Coordinates saved as " + savePath);
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            if (imageFiles.Length > 0 && currentIndex >= 0 && currentIndex < imageFiles.Length)
            {
                string originalFilePath = imageFiles[currentIndex];
                string loadPath = Path.Combine(Path.GetDirectoryName(originalFilePath), "coordinates_" + Path.GetFileNameWithoutExtension(originalFilePath) + ".csv");

                if (File.Exists(loadPath))
                {
                    using (StreamReader reader = new StreamReader(loadPath))
                    {
                        // Skip the header line
                        reader.ReadLine();

                        // Clear any existing points for the current image
                        if (pointMap.ContainsKey(imageFiles[currentIndex]))
                        {
                            pointMap[imageFiles[currentIndex]].Clear();
                        }
                        else
                        {
                            pointMap[imageFiles[currentIndex]] = new List<Point>();
                        }

                        // Read the points
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] fields = line.Split(';');
                            if (fields.Length == 2)
                            {
                                
                                int x = int.Parse(fields[0].Trim());
                                int y = int.Parse(fields[1].Trim());

                                
                                pointMap[imageFiles[currentIndex]].Add(new Point(x, y));
                                
                            }
                        }
                    }

                    // Redraw the image with the loaded points
                    pictureBox1.ImageLocation = imageFiles[currentIndex];
                    pictureBox1.Load();

                    using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                    {
                        Point? lastPoint = null;
                        foreach (Point point in pointMap[imageFiles[currentIndex]])
                        {
                            Debug.WriteLine("Drawing point:" + point.X + " , " + point.Y);
                            if (lastPoint != null)
                            {
                                g.DrawLine(new Pen(Color.Red, 2), lastPoint.Value, point);
                            }
                            lastPoint = point;
                        }
                        lastClickLocation = lastPoint ?? new Point();
                    }

                    pictureBox1.Invalidate();
                    MessageBox.Show("Coordinates loaded from " + loadPath);
                }
                else
                {
                    MessageBox.Show("No saved coordinates found for this image.");
                }
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
