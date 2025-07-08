using System.Windows.Forms;

namespace opendef
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public void AddFiles(string[] files)
        {
            if (files == null || files.Length == 0)
            {
                //MessageBox.Show(@"No files to display.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Text += $@"- {files.Length} files found";

            foreach (var file in files)
            {
                listBox.Items.Add(file);
            }

            listBox.SelectedIndex = 0;
        }

        public string GetSelectedFile()
        {
            return listBox.SelectedItem as string;
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}