using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTest.Seriport
{
    public class SerialPortHelper
    {
        #region 属性

        //创建一个串口对象
        public SerialPort MyCom = new SerialPort();

        #endregion

        #region 连接和断开连接

        public bool Connect(int iBaudRate, string iPortName, int iDataBits, string iParity, string iStopBits)
        {
            //如果打开，先关闭

            if (MyCom.IsOpen)
            {
                MyCom.Close();
            }

            //设置串口属性

            MyCom.PortName = iPortName;
            MyCom.BaudRate = iBaudRate;
            MyCom.StopBits = (StopBits)Enum.Parse(typeof(StopBits), iStopBits, true);
            MyCom.Parity = (Parity)Enum.Parse(typeof(Parity), iParity, true);
            MyCom.DataBits = iDataBits;




            //打开串口
            try
            {
                MyCom.Open();
            }
            catch (Exception ex)
            {
                //写入日志
               throw new Exception(ex.Message);
            }

            return true;
        }

        public void DisConnect()
        {
            //如果打开，先关闭

            if (MyCom.IsOpen)
            {
                MyCom.Close();
            }
        }

        #endregion

        #region 数据接受，同步读取
        public string Recive()
        {
            int ByteToRead = MyCom.BytesToRead;

            //定义一个字节数组
            byte[] rcv = new byte[ByteToRead];

            //读取缓冲区里的数据放到字节数组中
            MyCom.Read(rcv, 0, ByteToRead);

            //这里如果想把字节数组给到主线程，需要使用委托
            return Encoding.ASCII.GetString(rcv);

        }

        #endregion

        #region 命令发送
        public void Write(byte[] SendBuf)
        {
            MyCom.Write(SendBuf, 0, SendBuf.Length);
        }
        #endregion

        #region 命令发送
        public void WriteText(string text)
        {
            MyCom.Write(text);
        }
        #endregion
    }
}
