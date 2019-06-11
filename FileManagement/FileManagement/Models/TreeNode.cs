using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement

{
    public class TreeNode
    {
        public enum NodeType { folder, file };  // 结点类型
        public List<int> childInodeIndex;  // 记录子文件
        public int childrenNum;  // 子文件的数量
        public int fatherIndex;  // 父目录的编号
    }
}
