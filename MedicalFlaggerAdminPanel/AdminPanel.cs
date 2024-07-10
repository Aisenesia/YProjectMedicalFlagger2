namespace MedicalFlaggerAdminPanel
{
    public partial class AdminPanelForm : Form
    {

        
        public AdminPanelForm()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void Add_Click(object sender, EventArgs e)
        {
            AddItemToListView();
        }

        private void AddItemToListView()
        {
            // Add a new item to the ListView, with an empty label
            // (you can set any default properties that you want to here)
            ListViewItem item = listView1.Items.Add(String.Empty);

            // Place the newly-added item into edit mode immediately
            item.BeginEdit();
        }
    }
}
