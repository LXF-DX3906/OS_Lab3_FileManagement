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
    public partial class Details : Form
    {
        public Details(Node node)
        {
            InitializeComponent();
            if(node.nodeType == Node.NodeType.folder)
            {
                textBox1.Text = node.folder.path;
                textBox2.Text = node.folder.name;
                textBox3.Text = node.folder.createdTime.ToString();
                textBox4.Text = "文件夹";
                textBox5.Text = node.folder.fileSize.ToString() + "B";

            }
            else if(node.nodeType == Node.NodeType.file)
            {
                textBox1.Text = node.file.fcb.path;
                textBox2.Text = node.file.fcb.fileName + ".txt";
                textBox3.Text = node.file.fcb.createdTime.ToString();
                textBox4.Text = ".txt";
                textBox5.Text = node.file.fcb.fileSize.ToString() + "B";
            }
        }

    }
}
