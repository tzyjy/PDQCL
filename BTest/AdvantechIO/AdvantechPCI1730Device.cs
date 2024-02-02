using Automation.BDaq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTest
{
    public class AdvantechPCI1730Device
    {
        #region 定义变量
        public  InstantDiCtrl instantDiCtrl ;

        public  InstantDoCtrl instantDoCtrl1 ;
        #endregion

        #region 构造方法
        public AdvantechPCI1730Device()
        {
            try
            {
                instantDiCtrl = new InstantDiCtrl();
                instantDoCtrl1 = new InstantDoCtrl();
                instantDiCtrl.SelectedDevice = new DeviceInformation("PCI-1730,BID#0");
                instantDoCtrl1.SelectedDevice = new DeviceInformation("PCI-1730,BID#0");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 写入单个IO板卡输出
        /// <summary>
        /// 通过PLC地址来写入DO
        /// </summary>
        /// <param name="plcAdress"></param>
        /// <param name="WriteData"></param>
        public  void WriteDObit(byte dioValue, byte WriteData)
        {
            try
            {
                int port = dioValue / 8;
                int bit = dioValue % 8;
                if (instantDoCtrl1!=null)
                {
                    instantDoCtrl1.WriteBit(port, bit, WriteData);
                }
               

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 读16个DI地址（PLC的输出）

        public  object obj = new object();
        public  bool[] ReadAllDi()
        {
            byte[] portData2 = new Byte[2];
            lock (obj)
            {
                instantDiCtrl.Read(0, 2, portData2);
            }
            bool[] boolArray = new bool[16];
            short values = System.BitConverter.ToInt16(portData2, 0);
            for (int i = 0; i < 16; i++)
            {
                boolArray[i] = (values & (1 << i)) != 0;
            }
            return boolArray;
        }

        #endregion 读15个DI地址（PLC的输出）

        #region 读取15个DO地址

        public  bool[] ReadAllDO()
        {
            try
            {
                byte[] portDataDO = new Byte[2];
                for (int j = 0; j < 2; j++)
                {
                    instantDoCtrl1.Read(0, 2, portDataDO);
                }
                bool[] boolArray = new bool[16];
                Int16 values = Convert.ToInt16(System.BitConverter.ToInt16(portDataDO, 0).ToString());
                for (int i = 0; i < 16; i++)
                {
                    boolArray[i] = (values & (1 << i)) != 0;
                }
                return boolArray;
            }
            catch (Exception)
            {

                throw new Exception();
            }


        }

        #endregion 读取15个DO地址

        #region 读取某一个地址

        public  bool ReadBit(byte diValue)
        {
            byte data2 = 0;

            int port = diValue / 8;
            int bit = diValue % 8;
             instantDiCtrl.ReadBit(port, bit, out data2);
            if (data2==1)
            {
                return true;
            }
            else
            {
                return false;

            }
        
        }


        #endregion
    }
}
