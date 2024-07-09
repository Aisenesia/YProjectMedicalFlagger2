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

namespace YProjectMedicalFlagger2
{
    public partial class Form2 : Form
    {
        private static string filesPath = @"C:\Users\Hakan\source\repos\YProjectMedicalFlagger\YProjectMedicalFlagger\files";
        private string directoryPath;
        private List<string> files;

        private string[] imageFiles;
        private int currentIndex;

        public Form2(String fileName)
        {
            InitializeComponent();
            InitializeFileList(fileName);
            InitializeImageList();

        }

        private void InitializeFileList(String fileName)
        {
            directoryPath = filesPath + "\\" + fileName;
            files = Directory.GetFiles(directoryPath).ToList();
            //InitializeImageList();
        }
        private void InitializeImageList()
        {

            imageFiles = files.Where(file => file.EndsWith(".png") || file.EndsWith(".jpg")).ToArray();
            currentIndex = 0;
        }

        private void DisplayCurrentImage()
        {
            if (imageFiles.Length > 0 && currentIndex >= 0 && currentIndex < imageFiles.Length)
            {
                pictureBox1.ImageLocation = imageFiles[currentIndex];
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            label1.Text = "Ek notlar:";
            listBoxInit();
            DisplayCurrentImage();
        }


        private void listBoxInit()
        {
            checkedListBox1.Items.Add("Kanama");
            checkedListBox1.Items.Add("Kızarıklık");
            checkedListBox1.Items.Add("Ağrı");
            checkedListBox1.Items.Add("Şişme");
            checkedListBox1.Items.Add("Yanma");
            checkedListBox1.Items.Add("Kaşıntı");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void prev_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
            }
            else {
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


    }
}
