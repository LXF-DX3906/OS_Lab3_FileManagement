using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    public class File
    {
        public FCB fcb = new FCB();                // FCB


        //构造函数
        public File(String name, String type, String fatherPath)
        {
            fcb.fileType = FCB.FileType.txt;
            fcb.fileName = name;
            fcb.createdTime = DateTime.Now;
            fcb.updatedTime = DateTime.Now;
            fcb.fileSize = 0;
            fcb.blocklist = new List<Block>();
            fcb.path = fatherPath + '\\' + name;
        }

        //清空文件块
        public void setEmpty(ref BitMap bitmap)
        {
            for(int i=0; i < fcb.blocklist.Count(); i+=1)
            {
                bitmap.setFree(bitmap.findFreeBlock()); //置该块为空闲
            }
            fcb.blocklist.Clear();                      //清空块链表
            fcb.fileSize = 0;                           //文件大小置为0
        }

        //写文件
        public void write(String data, ref BitMap bitmap)
        {
            setEmpty(ref bitmap);
            while (data.Count() > 512)
            {
                bitmap.blocks[bitmap.findFreeBlock()] = new Block();
                bitmap.blocks[bitmap.findFreeBlock()].setData(data.Substring(0, 512));   //每次截取512个字符加入寻找到的块中
                fcb.blocklist.Add(bitmap.blocks[bitmap.findFreeBlock()]);                //将块加入块链表
                bitmap.setOccupy(bitmap.findFreeBlock());                                //置此块为占用状态
                fcb.fileSize += 512;
                data = data.Remove(0,512);
            }
            bitmap.blocks[bitmap.findFreeBlock()] = new Block();
            bitmap.blocks[bitmap.findFreeBlock()].setData(data);
            fcb.blocklist.Add(bitmap.blocks[bitmap.findFreeBlock()]);                //将块加入块链表
            bitmap.setOccupy(bitmap.findFreeBlock());
            fcb.fileSize += data.Length;
            fcb.updatedTime = DateTime.Now;
        }

        //读文件
        //获取文件内容
        public String getData()
        {
            string content = "";
            for (int i = 0; i < fcb.blocklist.Count(); i += 1)
            {
                content += fcb.blocklist[i].getData();
            }
            return content;
        }
        //获取文件名称
        public String getName()
        {
            return fcb.fileName;
        }
        //获取文件大小,单位为Byte
        public int getfileSize()
        {
            return fcb.fileSize;
        }
        //获取文件创建时间
        public DateTime getcreatedTime()
        {
            return fcb.createdTime;
        }
        //获取文件修改时间
        public DateTime getupdatedTime()
        {
            return fcb.updatedTime;
        }
    }
}