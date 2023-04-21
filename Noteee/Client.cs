using System.Windows.Forms;

namespace Noteee
{
    public partial class Client : Form
    {
        Font defaultFont;

        public Client()
        {
            InitializeComponent();
            defaultFont = (Font)rtbText.Font.Clone();
        }

        // Reseting Font and Color
        private void ResetFontColor()
        {
            rtbText.Font = defaultFont;
            rtbText.ForeColor = Color.Black;
        }

        // File Operations
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbText.Clear();
            ResetFontColor();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                RichTextBoxStreamType fileType;
                switch (openFileDialog.FilterIndex)
                {
                    case 1:
                        fileType = RichTextBoxStreamType.RichText;
                        break;
                    case 2:
                        fileType = RichTextBoxStreamType.UnicodePlainText;
                        ResetFontColor();
                        break;
                    default:
                        fileType = RichTextBoxStreamType.PlainText;
                        ResetFontColor();
                        break;
                }
                rtbText.LoadFile(openFileDialog.FileName, fileType);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                RichTextBoxStreamType fileType;
                switch (saveFileDialog.FilterIndex)
                {
                    case 1:
                        fileType = RichTextBoxStreamType.RichText;
                        break;
                    case 2:
                        fileType = RichTextBoxStreamType.UnicodePlainText;
                        break;
                    default:
                        fileType = RichTextBoxStreamType.PlainText;
                        break;
                }
                rtbText.SaveFile(saveFileDialog.FileName, fileType);
            }
        }

        // Undo and Redo
        private void undoToolStripMenuItem_Click(object sender, EventArgs e) => rtbText.Undo();

        private void redoToolStripMenuItem_Click(object sender, EventArgs e) => rtbText.Redo();

        // Changes the state Enable State of Undo/Redo Buttons
        private void rtbText_TextChanged(object sender, EventArgs e)
        {
            undoToolStripMenuItem.Enabled = rtbText.CanUndo;
            redoToolStripMenuItem.Enabled = rtbText.CanRedo;
        }

        // Cut, Copy, Paste, Delete
        private void cutToolStripMenuItem_Click(object sender, EventArgs e) => rtbText.Cut();

        private void copyToolStripMenuItem_Click(object sender, EventArgs e) => rtbText.Copy();

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e) => rtbText.Paste();

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) => rtbText.SelectedText = String.Empty;

        // Selects all text
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e) => rtbText.SelectAll();

        // Prints the Current DateTime
        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e) => rtbText.Text += DateTime.Now;

        // Font and Color Changing
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog.Font = rtbText.Font;
            fontDialog.Color = rtbText.ForeColor;

            if (fontDialog.ShowDialog() != DialogResult.Cancel)
            {
                rtbText.Font = fontDialog.Font;
                rtbText.ForeColor = fontDialog.Color;
            }
        }

        // Zoom functions
        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e) => rtbText.ZoomFactor *= 1.5f;

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e) => rtbText.ZoomFactor /= 1.5f;

        private void restoreDefaultZoomToolStripMenuItem_Click(object sender, EventArgs e) => rtbText.ZoomFactor = 1;

        // Word wrap Enable/Disable
        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbText.WordWrap = !rtbText.WordWrap;
            wordWrapToolStripMenuItem.Checked = rtbText.WordWrap;
        }

        // Context Menu
        private void cutToolStripMenuItem1_Click(object sender, EventArgs e) => rtbText.Cut();

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e) => rtbText.Copy();

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e) => rtbText.Paste();

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e) => rtbText.SelectedText = String.Empty;

        // Closing Code
        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Close();

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!String.IsNullOrEmpty(rtbText.Text))
            {
                switch (MessageBox.Show("There is unsaved work! Do you want to save before closing?",
                    "Warning!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
                {
                    case DialogResult.Yes:
                        saveToolStripMenuItem_Click(null!, null!);
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }
    }
}