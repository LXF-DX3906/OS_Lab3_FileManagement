using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    public class File
    {
        private List<Block> blocklist;  // 数据块列表
        private FCB fcb;                // FCB

        //构造函数
        public void File()
        {
            fcb.fileType = FCB.FileType.text;
            fcb.fileName = "Newfile" + Enum.GetName(typeof(FCB.FileType), fcb.fileType);
            fcb.createdTime = DateTime.Now;
            fcb.updatedTime = DateTime.Now;
        }
        //创建文件
        public void createFile()
        {
            fcb.fileType = FCB.FileType.text;
            fcb.fileName = "Newfile" + Enum.GetName(typeof(FCB.FileType), fcb.fileType);
            fcb.createdTime = DateTime.Now;
            fcb.updatedTime = DateTime.Now;

        }
        //写文件

        //打开文件&读文件
        //删除文件
    }
}