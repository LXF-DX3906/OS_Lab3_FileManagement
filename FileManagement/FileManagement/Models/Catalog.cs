using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    public class Catalog
    {
        public List<Node> nodelist; //目录中只含结点
        public int childrenNum;  // 子结点的数量
        public String name;  // 名字
        public String path;  //路径

        public Catalog(String namedata, String fatherPath)
        {
            name = namedata;
            path = fatherPath + '\\' + namedata;
            childrenNum = 0;
        }
        
        //添加文件夹结点
        public void addNode(String namedata, String fatherPath)
        {
            Node node = new Node(namedata, fatherPath);
            nodelist.Add(node);
            childrenNum += 1;
        }

        //添加文件结点
        public void addNode(String namedata, String fileType, String fatherPath)
        {
            Node node = new Node(namedata, fileType, fatherPath);
            nodelist.Add(node);
            childrenNum += 1;
        }

        //删除结点
        public void deleteNode(int i)
        {
            nodelist.RemoveAt(i);
            childrenNum -= 1;
        }
    }
}
