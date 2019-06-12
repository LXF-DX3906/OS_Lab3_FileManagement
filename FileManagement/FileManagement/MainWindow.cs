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
        Catalog root_catalog = new Catalog("root","root");
        public Catalog current_catalog;
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
            UpdateTreeView();
            UpdateListView();
        }

        //更新文件目录
        public void UpdateTreeView()
        {
            treeView1.Nodes.Clear();
            root_node = new TreeNode("root");
            addTreeNode(root_node, root_catalog);
            treeView1.Nodes.Add(root_node);
        }

        //更新视图目录
        public void UpdateListView()
        {
            listView1.Items.Clear();
            if (item.son != null)
            {
                FCB son = item.son;
                do
                {
                    File temp = catalog.getFile(son);
                    ListViewItem file = new ListViewItem(new string[]
                    {
                        temp.name,
                        temp.size,
                        temp.type,
                        temp.createTime.ToString()
                });
                    if (temp.type == "folder")
                        file.ImageIndex = 0;
                    else
                        file.ImageIndex = 1;

                    listMap(temp, file);
                    listView1.Items.Add(file);
                    son = son.next;
                } while (son != null);
            }
        }

        //递归增加子结点
        public void addTreeNode(TreeNode node, Catalog dir)
        {
            if (dir != null)
            { 
               for(int i = 1; i < dir.nodelist.Count(); i+=1)
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
            current_catalog = current_catalog.nodelist[current_catalog.nodelist.Count() - 1].folder;
            updateView();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        
    }
}
