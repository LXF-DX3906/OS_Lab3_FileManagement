using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    public class BitMap
    {
        private const int BYTESIZE = 8;    //字长，模拟一字节8位
        private const int CAPCITY = 100 * 100 * 100;    //内存空间最大块数（容量），521B*1000,000约512MB
        private const int BYTENUMBER = 100 * 100 * 100 / 8; //125000个字节即可表示所有块数
        public Block[] blocks = new Block[CAPCITY];    //所有块
        private bool[] bitMap = new bool[CAPCITY];     //位图

        //构造函数
        public BitMap()
        {
            for (int i = 0; i < CAPCITY; i++)
            {
                //blocks[i].blockNumber = i;
                bitMap[i] = true;
            }
        }

        //寻找一个空块
        public int findFreeBlock()
        {
            int bytePointer = 0;    //字号
            int bitPointer = 0;     //位号
            while (bytePointer < BYTENUMBER)    //字号在限制范围内
            {
                if (bitMap[bytePointer * BYTESIZE + bitPointer])//寻找到空块
                {
                    return (bytePointer * BYTESIZE + bitPointer);
                }
                else
                {
                    bitPointer += 1;
                    if (bitPointer == BYTESIZE)//位号超出限制
                    {
                        bitPointer = bitPointer % BYTESIZE;
                        bytePointer += 1;
                    }
                }
            }
            return -1;
        }

        //置某一结点为空闲
        public void setFree(int i)
        {
            bitMap[i] = true;
        }

        //置某一结点为占用
        public void setOccupy(int i)
        {
            bitMap[i] = false;
        }
    }
}
