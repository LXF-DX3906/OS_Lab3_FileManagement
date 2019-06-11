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
    public partial class MainWindow : Form
    {
        TreeNode root_node;


        public MainWindow()
        {
            InitializeComponent();
            InitializeWindow();
        }

        public void InitializeWindow()
        {
            InitiallizeTreeView();
        }

        public void InitiallizeTreeView()
        {
            treeView1.Nodes.Clear();
            root_node = new TreeNode("root");
            //treeView1.SelectedNode.Nodes.Add(root_node);
            treeView1.Nodes.Add(root_node);
        }

        private void AddLabel(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;

            listView1.BeginUpdate();

            for (int i = 0; i < 10; i++)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.ImageIndex = 0;

                lvi.Text = "item" + i;

                listView1.Items.Add(lvi);
            }

            listView1.EndUpdate();
        }
    }
}
