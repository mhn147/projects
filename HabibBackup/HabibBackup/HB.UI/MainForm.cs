using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HB.UI
{
    public partial class HabibBackup : Form
    {
        public HabibBackup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void srcPathBrowseBtn_Click(object sender, EventArgs e)
        {
            this.srcPathTextBox.Text = "";
            try
            {
                this.srcPathTextBox.Text = _getFolderPath();
                if (_bothPathsAreEqual())
                {
                    MessageBox.Show("Source and destination paths cannot be the same.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.srcPathTextBox.Text = "";
                    return;
                }
            } catch (Exception ex)
            {
                throw;
            }
        }

        private void destPathBrowseBtn_Click(object sender, EventArgs e)
        {
            this.destPathTextBox.Text = "";
            try
            {
                this.destPathTextBox.Text = _getFolderPath();
                if (_bothPathsAreEqual())
                {
                    MessageBox.Show("Source and destination paths cannot be the same.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.destPathTextBox.Text = "";
                    return;
                }
                this.destPathTextBox.Text = Path.Combine(
                    this.destPathTextBox.Text, $"HabibNasri Backup - {DateTime.Now.Ticks}");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private string _getFolderPath()
        {
            using (var folderBrowserDialog = new CommonOpenFileDialog() 
            { 
                IsFolderPicker = true, 
                RestoreDirectory = true 
            })
            {
                var result = folderBrowserDialog.ShowDialog();
                if (result != CommonFileDialogResult.Ok ||
                    string.IsNullOrWhiteSpace(folderBrowserDialog.FileName))
                {
                    return "";
                }
                return folderBrowserDialog.FileName;
            }
        }

        private bool _bothPathsAreEqual()
        {
            // ignoring initial case
            if (this.destPathTextBox.Text == "" && this.srcPathTextBox.Text == "")
                return false;

            return this.destPathTextBox.Text == this.srcPathTextBox.Text;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void srcPathTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Start_Click_1(object sender, EventArgs e)
        {
            this.feebackLabel.Visible = false;
            if (string.IsNullOrWhiteSpace(this.srcPathTextBox.Text) ||
                    string.IsNullOrWhiteSpace(this.destPathTextBox.Text))
            {
                MessageBox.Show("Source and destination paths must be not empty.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Directory.CreateDirectory(this.destPathTextBox.Text);
            CopyFiles(this.srcPathTextBox.Text, this.destPathTextBox.Text);
            this.destPathTextBox.Text = "";
            this.feebackLabel.Visible = true;
        }

        #region unmanaged copy directory code
        private enum FO_Func: uint
        {
            FO_COPY = 0x0002,
            FO_DELETE = 0x0003,
            FO_MOVE = 0x0001,
            FO_RENAME = 0x0004
        }

        private struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            public FO_Func wFunc;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pFrom;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pTo;
            public ushort fFlags;
            public bool fAnyOperationsAborted;
            public IntPtr hNameMappings;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpszProgressTitle;
        }

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        static extern int SHFileOperation([In] ref SHFILEOPSTRUCT
            lpFileOp);

        private static SHFILEOPSTRUCT _ShFile;

        public static void CopyFiles(string sSource, string sTarget)
        {
            //Link: https://social.msdn.microsoft.com/Forums/en-US/1c370cae-d7e1-46cf-afd6-dfd9d1a09899/shfileoperation-does-not-working-at-all?forum=windowsgeneraldevelopmentissues
            //Apparently, paths should be terminated by the null character
            sSource += '\0';
            sTarget += '\0';

            try
            {
                _ShFile.wFunc = FO_Func.FO_COPY;
                _ShFile.pFrom = sSource;
                _ShFile.pTo = sTarget;
                SHFileOperation(ref _ShFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }

}
