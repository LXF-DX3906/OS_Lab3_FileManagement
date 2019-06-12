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

namespace FileManagement
{
    public partial class MainWindow : Form
    {
        TreeNode root_node;
        public BitMap bitmap = new BitMap();
        Catalog root_catalog = new Catalog("root");
        public Catalog current_catalog;
        private List<ListViewItem> listViewItems = new List<ListViewItem>();
       // private Dictionary<int, ListViewItem> list_table = new Dictionary<int, ListViewItem>();
        //在当前目录创建catalog文件夹

        public MainWindow()
        {
            InitializeComponent();
            InitializeWindow();
        }

        //初始化窗口
        public void InitializeWindow()
        {
            InitiallizeTreeView();
            InitializeListView();
            textBox1.Text = current_catalog.path;
        }

        //初始化视图
        public void InitializeListView()
        {
            listView1.Items.Clear();
        }

        //初始化文件目录
        public void InitiallizeTreeView()
        {
            current_catalog = root_catalog;
            treeView1.Nodes.Clear();
            root_node = new TreeNode("root");
            treeView1.Nodes.Add(root_node);
            treeView1.SelectedNode = null;
        }

        //更新视图
        public void updateView()
        {
            updateTreeView();
            updateListView();
        }

        //更新文件目录
        public void updateTreeView()
        {
            treeView1.Nodes.Clear();
            root_node = new TreeNode("root");
            addTreeNode(root_node, root_catalog);
            treeView1.Nodes.Add(root_node);
        }

        //更新视图目录
        public void updateListView()
        {
            listViewItems = new List<ListViewItem>();
            //list_table = new Dictionary<int, ListViewItem>();
            listView1.Items.Clear();
            if (current_catalog.nodelist != null)
            {
                for(int i = 0; i< current_catalog.nodelist.Count(); i +=1)
                {
                    ListViewItem node = new ListViewItem();
                    if (current_catalog.nodelist[i].nodeType == Node.NodeType.file)
                        node.ImageIndex = 0;
                    else
                        node.ImageIndex = 1;
                    node.Text = current_catalog.nodelist[i].name;
                    listViewItems.Add(node);
                    //list_table[i] = node;
                    listView1.Items.Add(node);
                }
            }
        }

        //更新地址
        public void updateAddress()
        {
            textBox1.Text = current_catalog.path;
        }

        //递归增加子结点
        public void addTreeNode(TreeNode node, Catalog dir)
        {
            if (dir.nodelist != null)
            { 
               for(int i = 0; i < dir.nodelist.Count(); i+=1)
               {
                    if(dir.nodelist[i].nodeType == Node.NodeType.folder)
                    {
                        TreeNode new_node = new TreeNode(dir.nodelist[i].name);
                        addTreeNode(new_node, dir.nodelist[i].folder);
                        node.Nodes.Add(new_node);
                    }
               }
            }
        }

        private void 文件ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            String file_name = "NewTXT";
            String fatherPath;
            String fileType = "txt";
            fatherPath = current_catalog.path;
            current_catalog.addNode(file_name,  fileType, fatherPath);
            updateView();
        }

        private void 文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String file_name = "New folder";
            String fatherPath;
            fatherPath = current_catalog.path;
            current_catalog.addNode(file_name, fatherPath);
            updateView();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //public int getPointer(ListViewItem item)
        //{
        //    if (list_table.ContainsValue(item))
        //    {
        //        foreach (KeyValuePair<int, ListViewItem> kvp in list_table)
        //        {
        //            if (kvp.Value.Equals(item))
        //                return kvp.Key;
        //        }
        //        return -1;
        //    }
        //    else
        //    {
        //        MessageBox.Show("Can't get the pointer");
        //        return -1;
        //    }
        //}

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem current_item = new ListViewItem();
            if (listView1.SelectedItems.Count != 0)
            {
                current_item = listView1.SelectedItems[0];
            }
            else
            {
                MessageBox.Show("请选择一个项", "提示信息",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            for(int i = 0; i<current_catalog.nodelist.Count(); i+=1)
            {
                if(listViewItems[i] == current_item)
                {
                    Node current_node = current_catalog.nodelist[i];
                    openListViewItem(ref current_node);
                    break;
                }
            }
        }

        private void openListViewItem(ref Node node)
        {
            switch (node.nodeType)
            {
                case Node.NodeType.folder:
                    current_catalog = node.folder;
                    updateAddress();
                    updateListView();
                    break;
                case Node.NodeType.file:
                    FileEditor fileEditor = new FileEditor(ref bitmap, ref node.file);
                    fileEditor.Show();
                    break;
                default:
                    break;
            }
        }
    }
}
