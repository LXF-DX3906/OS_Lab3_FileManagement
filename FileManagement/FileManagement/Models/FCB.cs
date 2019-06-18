using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    [Serializable]
    public class FCB
    {
        public enum FileType { txt };  // 文件类型枚举
        public FileType fileType;   //文件类型
        public int fileSize;  // 文件大小
        public String fileName;  // 文件名
        public DateTime createdTime;  // 创建时间
        public DateTime updatedTime;  // 修改时间
        public List<Block> blocklist;      // 文件指针
        public String path;
    }
}
