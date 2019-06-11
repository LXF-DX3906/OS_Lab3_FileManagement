using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    public class BitMap
    {
        private List<bool> bitMap;  // 位图

        //获取位图
        public List<bool> getBitMap()
        {
            return bitMap;
        }

        //置某一结点为空闲0
        public void setFree(int i)
        {
            bitMap[i] = false;
        }

        //置某一结点为占用1
        public void setOccupy(int i)
        {
            bitMap[i] = true;
        }
    }
}
