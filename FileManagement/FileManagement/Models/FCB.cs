using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    public class FCB
    {
        public enum FileType { text };  // 文件类型
        public int fileSize;  // 文件大小
        public String fileName;  // 文件名
        public DateTime createdTime;  // 创建时间
        public DateTime updatedTime;  // 修改时间
        public int blockSize;  // 占块数
        public List<int> dataBlockList;  // 数据块占用的数据块组
    }
}
