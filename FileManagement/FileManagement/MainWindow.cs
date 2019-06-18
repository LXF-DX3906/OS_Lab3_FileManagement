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
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Formatters.Binary;

namespace FileManagement
{
    [Serializable]
    public partial class MainWindow : Form
    {
        TreeNode root_node;
        public BitMap bitmap = new BitMap();
        Catalog root_catalog = new Catalog("root");
        public Catalog current_catalog;
        private List<ListViewItem> listViewItems = new List<ListViewItem>();
        public string dir = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Directory.GetCurrentDirectory()));
        //在当前目录创建catalog文件夹

        public MainWindow()
        {
            InitializeComponent();
            BinaryDeserialize();
            current_catalog = root_catalog;
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
            updateListView();
        }

        //初始化文件目录
        public void InitiallizeTreeView()
        {
            updateTreeView();
        }

        //更新视图
        public void updateView()
        {
            updateTreeView();
            updateListView();
            updateAddress();
        }

        //更新文件目录
        public void updateTreeView()
        {
            treeView1.Nodes.Clear();
            root_node = new TreeNode("root");
            addTreeNode(root_node, root_catalog);
            treeView1.Nodes.Add(root_node);
            root_node.ExpandAll();
        }

        //更新视图目录
        public void updateListView()
        {
            listViewItems = new List<ListViewItem>();
            listView1.Items.Clear();
            if (current_catalog.nodelist != null)
            {
                for(int i = 0; i< current_catalog.nodelist.Count(); i +=1)
                {
                    ListViewItem node = new ListViewItem();
                    if (current_catalog.nodelist[i].nodeType == Node.NodeType.file)
                    {
                        node.ImageIndex = 0;
                        node.Text = current_catalog.nodelist[i].name + ".txt";
                    }
                    else
                    {
                        node.ImageIndex = 1;
                        node.Text = current_catalog.nodelist[i].name;
                    }
                        listViewItems.Add(node);
                    listView1.Items.Add(node);
                }
            }
        }

        //更新地址
        public void updateAddress()
        {
            textBox1.Text = current_catalog.path;
        }

        //序列化
        private void BinarySerialize()
        {
            FileStream fileStream2, fileStream3;
            BinaryFormatter bf = new BinaryFormatter();

            //fileStream1 = new FileStream(System.IO.Path.Combine(dir, "root_node.dat"), FileMode.Create);
            //bf.Serialize(fileStream1, root_node);
            //fileStream1.Close();

            fileStream2 = new FileStream(System.IO.Path.Combine(dir, "root_catalog.dat"), FileMode.Create);
            bf.Serialize(fileStream2, root_catalog);
            fileStream2.Close();

            fileStream3 = new FileStream(System.IO.Path.Combine(dir, "bitmap.dat"), FileMode.Create);
            bf.Serialize(fileStream3, bitmap);
            fileStream3.Close();
        }

        //反序列化
        private void BinaryDeserialize()
        {
            FileStream fileStream2, fileStream3;
            BinaryFormatter bf = new BinaryFormatter();

            //fileStream1 = new FileStream(System.IO.Path.Combine(dir, "root_node.dat"), FileMode.Open, FileAccess.Read, FileShare.Read);
            //root_node = bf.Deserialize(fileStream1) as Node;
            //fileStream1.Close();
            bool rootb = System.IO.File.Exists(System.IO.Path.Combine(dir, "root_catalog.dat"));


            if (System.IO.File.Exists(System.IO.Path.Combine(dir, "root_catalog.dat")) && System.IO.File.Exists(System.IO.Path.Combine(dir, "bitmap.dat")))
            {
                fileStream2 = new FileStream(System.IO.Path.Combine(dir, "root_catalog.dat"), FileMode.Open, FileAccess.Read, FileShare.Read);
                root_catalog = bf.Deserialize(fileStream2) as Catalog;
                fileStream2.Close();

                fileStream3 = new FileStream(System.IO.Path.Combine(dir, "bitmap.dat"), FileMode.Open, FileAccess.Read, FileShare.Read);
                bitmap = bf.Deserialize(fileStream3) as BitMap;
                fileStream3.Close();
            }
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
            String file_name = "New File";
            String fileType = "txt";
            file_name = nameCheck(file_name, fileType);
            InputBox.operationType otype = InputBox.operationType.newfile;
            InputBox newfile = new InputBox(current_catalog, file_name, fileType, otype);
            newfile.Show();
            newfile.CallBack = updateView;
        }

        private void 文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String file_name = "New folder";
            String fileType = "";
            file_name = nameCheck(file_name, fileType);
            InputBox.operationType otype = InputBox.operationType.newfile;
            InputBox newfile = new InputBox(current_catalog, file_name, fileType, otype);
            newfile.Show();
            newfile.CallBack = updateView;
        }

        //视图项双击
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

        //打开节点下视图
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


        //检测重名
        private String nameCheck(String name, String type)
        {
            int counter = 0;
            if (type == "")
            {
                for (int i = 0; i < current_catalog.nodelist.Count(); i += 1)
                {
                    if (current_catalog.nodelist[i].nodeType == Node.NodeType.folder)
                    {
                        string[] sArray = current_catalog.nodelist[i].folder.name.Split('(');
                        if (sArray[0] == name)
                        {
                            counter++;
                        }
                    }

                }
            }
            else
            {
                for (int i = 0; i < current_catalog.nodelist.Count(); i += 1)
                {
                    if (current_catalog.nodelist[i].nodeType == Node.NodeType.file)
                    {
                        string[] sArray = current_catalog.nodelist[i].file.getName().Split('(');
                        if (sArray[0] == name)
                        {
                            counter++;
                        }
                    }

                }
            }
            if (counter > 0)
                name += "(" + counter.ToString() + ")";
            return name;
        }

        //返回按钮
        private void 返回ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(current_catalog == root_catalog)
            {
                MessageBox.Show("已是根目录！", "提示信息", MessageBoxButtons.YesNo);
            }
            else
            {
                current_catalog = current_catalog.parent_catalog;
            }
            updateView();
        }

        //打开按钮
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
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
            for (int i = 0; i < current_catalog.nodelist.Count(); i += 1)
            {
                if (listViewItems[i] == current_item)
                {
                    Node current_node = current_catalog.nodelist[i];
                    openListViewItem(ref current_node);
                    updataFolderSize(ref current_catalog);
                    break;
                }
            }
        }

        //更新文件夹大小
        private void updataFolderSize(ref Catalog current_catalog)
        {
            current_catalog.fileSize = 0;
            for (int j = 0; j < current_catalog.nodelist.Count(); j++)
            {
                if (current_catalog.nodelist[j].nodeType == Node.NodeType.file)
                    current_catalog.fileSize += current_catalog.nodelist[j].file.fcb.fileSize;
                else
                    current_catalog.fileSize += current_catalog.nodelist[j].folder.fileSize;
            }
        }

        private void 文件ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            String file_name = "New File";
            String fileType = "txt";
            file_name = nameCheck(file_name, fileType);
            InputBox.operationType otype = InputBox.operationType.newfile;
            InputBox newfile = new InputBox(current_catalog, file_name, fileType, otype);
            newfile.Show();
            newfile.CallBack = updateView;
        }

        private void 文件夹ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            String file_name = "New folder";
            String fileType = "";
            file_name = nameCheck(file_name, fileType);
            InputBox.operationType otype = InputBox.operationType.newfile;
            InputBox newfile = new InputBox(current_catalog, file_name, fileType, otype);
            newfile.Show();
            newfile.CallBack = updateView;
        }

        private void 重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem current_item = new ListViewItem();
            String fileType = "";
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
            for (int i = 0; i < current_catalog.nodelist.Count(); i += 1)
            {
                if (listViewItems[i] == current_item)
                {
                    switch (current_catalog.nodelist[i].nodeType)
                    {
                        case Node.NodeType.folder:
                            fileType = "";
                            break;
                        case Node.NodeType.file:
                            fileType = "txt";
                            break;
                        default:
                            break;
                    }
                    InputBox.operationType otype = InputBox.operationType.rename;
                    InputBox newfile = new InputBox(current_catalog, current_catalog.nodelist[i].name, fileType, otype);
                    newfile.Show();
                    newfile.CallBack = updateView;
                    break;
                }
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem current_item = new ListViewItem();
            String fileType = "";
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
            for (int i = 0; i < current_catalog.nodelist.Count(); i += 1)
            {
                if (listViewItems[i] == current_item)
                {
                    current_catalog.updatedTime = DateTime.Now;
                    delete(ref current_catalog.nodelist, i);
                    updataFolderSize(ref current_catalog);
                    updateView();
                    break;
                }
            }
        }

        public void delete(ref List<Node> nodelist, int i)
        {
            if (nodelist.Count() > 0)
            {
                if (nodelist[i].nodeType == Node.NodeType.file)
                {
                    nodelist[i].file.setEmpty(ref bitmap);
                    nodelist.RemoveAt(i);
                    return;
                }
                else if (nodelist[i].nodeType == Node.NodeType.folder)
                {
                    if (nodelist[i].folder.nodelist != null)
                    {
                        for (int j = 0; j < nodelist[i].folder.nodelist.Count(); j++)
                        {
                            delete(ref nodelist[i].folder.nodelist, j);
                        }
                        nodelist.RemoveAt(i);
                    }
                    else
                    {
                        nodelist.RemoveAt(i);
                        return;
                    }
                }
            }
            return;
        }

        private void 格式化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delete(ref current_catalog.nodelist, 0);
            if (root_catalog.nodelist.Count() != 0)
            {
                for(int i = 0; i<root_catalog.nodelist.Count(); i++)
                {
                    delete(ref root_catalog.nodelist, i);
                }
            }
            root_catalog = new Catalog("root");
            current_catalog = root_catalog;
            updateView();
        }

        private void 详细信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem current_item = new ListViewItem();
            String fileType = "";
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
            for (int i = 0; i < current_catalog.nodelist.Count(); i += 1)
            {
                if (listViewItems[i] == current_item)
                {
                    Details details = new Details(current_catalog.nodelist[i]);
                    details.Show();
                }
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            BinarySerialize();
        }
    }
}
