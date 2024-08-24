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
    public partial class TestForm : Form
    {
        private ListView listView;
        private TextBox editBox;
        private ListViewItem.ListViewSubItem subItemToEdit;


        public TestForm()
        {
            InitializeComponent();
        }

        public void AddItem(string category, string value)
        {
            ListViewItem item = new ListViewItem(category);
            item.SubItems.Add(value);
            listView.Items.Add(item);

        }

        private void ListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo hitTestInfo = listView.HitTest(e.Location);
            if (hitTestInfo.SubItem != null && hitTestInfo.Item.SubItems.IndexOf(hitTestInfo.SubItem) == 1)
            {
                subItemToEdit = hitTestInfo.SubItem;
                Rectangle subItemBounds = subItemToEdit.Bounds;
                editBox.Bounds = new Rectangle(subItemBounds.X, subItemBounds.Y, subItemBounds.Width, subItemBounds.Height);
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
}
