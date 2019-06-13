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
    public partial class InputBox : Form
    {
        public Catalog current_catalog;
        String file_name;
        String fileType;
        public enum operationType {newfile,rename};
        public operationType operation_type;
        public DelegateMethod.delegateFunction CallBack;

        public InputBox(Catalog currentCatalog, String name, String type, operationType otype)
        {
            InitializeComponent();
            current_catalog = currentCatalog;
            textBox1.Text = name;
            file_name = name;
            fileType = type;
            operation_type = otype;
        }

        private void InputBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            String fatherPath = current_catalog.path;
            textBox1.Text = nameCheck(textBox1.Text, fileType);
            if (operation_type == operationType.newfile)
            {
                if (fileType == "")
                    current_catalog.addNode(current_catalog, textBox1.Text, fatherPath);
                else
                    current_catalog.addNode(textBox1.Text, fileType, fatherPath);
            }
            else if (operation_type == operationType.rename)
            {
                if (fileType == "")
                {
                    for (int i = 0; i < current_catalog.nodelist.Count(); i += 1)
                    {
                        if (current_catalog.nodelist[i].name == file_name && current_catalog.nodelist[i].nodeType == Node.NodeType.folder)
                        {
                            current_catalog.nodelist[i].changeName(textBox1.Text);
                            break;
                        }
                    }
                }
                else if(fileType == "txt")
                    for (int i = 0; i < current_catalog.nodelist.Count(); i += 1)
                    {
                        if (current_catalog.nodelist[i].name == file_name && current_catalog.nodelist[i].nodeType == Node.NodeType.file)
                        {
                            current_catalog.nodelist[i].changeName(textBox1.Text);
                            break;
                        }
                    }
            }
            callBack();
        }

        private void callBack()
        {
            if (CallBack != null)
                this.CallBack();
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
    }

    public class DelegateMethod
    {
        public delegate void delegateFunction();
    }
}
