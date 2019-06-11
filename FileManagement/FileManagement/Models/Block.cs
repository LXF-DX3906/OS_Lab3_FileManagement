using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    public class Block
    {
        private const int BLOCKSIZE = 512;  // 数据块大小为512Bytes
        private char[] data;  // 一个数据块可以存储512个Unicode字符
        private int length;

        public Block()
        {
            data = new char[BLOCKSIZE];
        }

        public void setData(string new_data)
        {
            length = (new_data.Length > 512) ? 512 : new_data.Length;
            for (int i = 0; i < length; i++)
            {
                data[i] = new_data[i];
            }
        }

        public string getData()
        {
            string temp = new string(data);
            return temp;
        }
    }
}
