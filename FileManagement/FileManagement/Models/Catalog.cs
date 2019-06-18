using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    [Serializable]
    public class Catalog
    {
        public List<Node> nodelist; //目录中只含结点
        public int childrenNum;  // 子结点的数量
        public String name;  // 名字
        public String path;  //路径
        public int fileSize;  // 文件大小
        public DateTime createdTime;  // 创建时间
        public DateTime updatedTime;  // 修改时间
        public Catalog parent_catalog = null; //父母节点

        //其他目录
        public Catalog(String namedata, String fatherPath)
        {
            nodelist = new List<Node>();
            name = namedata;
            path = fatherPath + '\\' + namedata;
            createdTime = DateTime.Now;
            updatedTime = DateTime.Now;
            fileSize = 0;
            childrenNum = 0;
        }

        //根目录
        public Catalog(String namedata)
        {
            nodelist = new List<Node>();
            name = namedata;
            path = namedata + ":\\";
            createdTime = DateTime.Now;
            updatedTime = DateTime.Now;
            fileSize = 0;
            childrenNum = 0;
        }

        //添加文件夹结点
        public void addNode(Catalog par_catalog, String namedata, String fatherPath)
        {
            Node node = new Node(namedata, fatherPath);
            node.folder.parent_catalog = par_catalog;
            nodelist.Add(node);
            childrenNum += 1;
            updatedTime = DateTime.Now;
        }

        //添加文件结点
        public void addNode(String namedata, String fileType, String fatherPath)
        {
            Node node = new Node(namedata, fileType, fatherPath);
            nodelist.Add(node);
            childrenNum += 1;
            updatedTime = DateTime.Now;
        }
    }
}
