using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YProjectMedicalFlagger2
{
    public partial class FullScreenImageForm : Form
    {
        public FullScreenImageForm(Image image)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None; // No borders
            this.WindowState = FormWindowState.Maximized; // Full screen
            this.BackColor = Color.Black; // Background color

            PictureBox pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                Image = image,
                SizeMode = PictureBoxSizeMode.Zoom // Adjust to fit screen
            };

            this.Controls.Add(pictureBox);

            // KeyPress event to close the form on ESC
            this.KeyDown += new KeyEventHandler(FullScreenImageForm_KeyDown);
        }

        private void FullScreenImageForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close(); // Close the full-screen image on ESC key press
            }
        }
    }

}
