using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManagement
{
    public partial class FileEditor : Form
    {
        private BitMap bitmap;
        public File textFile;

        public FileEditor()
        {
            InitializeComponent();
        }

        public FileEditor(ref BitMap bitMap, ref File file)
        {
            InitializeComponent();
            bitmap = bitMap;
            textFile = file;
            showContent();
        }

        public void showContent()
        {
            richTextBox1.Text = textFile.getData();
        }

        private void FileEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("保存更改?", "提示信息", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                textFile.write(richTextBox1.Text, ref bitmap);
            }
        }
    }
}
