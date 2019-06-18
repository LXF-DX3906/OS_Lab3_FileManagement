using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement

{
    [Serializable]
    public class Node
    {
        public enum NodeType { folder, file };  // 结点类型
        public NodeType nodeType;   //结点类型
        public File file;           //节点内容
        public Catalog folder;      //节点内容
        public String path;  // 路径
        public String name;  // 名字

        public Node(String namedata, String fatherPath)   //文件夹结点
        {
            nodeType = NodeType.folder;
            path = fatherPath + "\\" + namedata;
            name = namedata;
            folder = new Catalog(namedata, fatherPath);
        }

        public Node(String namedata, String fileType, String fatherPath)    //文件结点
        {
            nodeType = NodeType.file;
            path = fatherPath + '\\' + namedata;
            name = namedata;
            file = new File(name, fileType, fatherPath);
        }

        public void changeName(String changed_name)
        {
            name = changed_name;
            switch (nodeType)
            {
                case Node.NodeType.folder:
                    folder.path = folder.path.Remove(folder.path.Length-folder.name.Length-1,folder.name.Length+1);
                    folder.name = changed_name;
                    folder.path = folder.path + "\\" + folder.name;
                    break;
                case Node.NodeType.file:
                    file.fcb.path = file.fcb.path.Remove(file.fcb.path.Length - file.fcb.fileName.Length - 1, file.fcb.fileName.Length + 1);
                    file.fcb.fileName = changed_name;
                    file.fcb.path = file.fcb.path + "\\" + file.fcb.fileName;
                    break;
                default:
                    break;
            }
        }
    }
}
