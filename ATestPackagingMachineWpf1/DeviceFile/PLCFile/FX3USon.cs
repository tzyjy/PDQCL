﻿using ATestPackagingMachineWpf1.Common;
using BTest.LogHelper;
using HslCommunication.Profinet.Melsec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using thinger.DataConvertLib;
using ZModels;

namespace ATestPackagingMachineWpf1.DeviceFile.PLCFile
{
    /// <summary>
    /// FX3U子类
    /// </summary>
    public class FX3USon : PLCBase
    {

        MelsecFxSerial melsecFxSerial = new MelsecFxSerial();
        public override void Connect()
        {
            melsecFxSerial.SerialPortInni(sp =>
            {
                sp.PortName = "COM33";
                sp.BaudRate = 9600;
                sp.DataBits = 7;
                sp.StopBits = System.IO.Ports.StopBits.One;
                sp.Parity = System.IO.Ports.Parity.Even;
                sp.RtsEnable = false;

            });

            melsecFxSerial.ReceiveTimeout = 5000;   // 接收超时，单位毫秒
            melsecFxSerial.IsNewVersion = true;
            melsecFxSerial.AutoChangeBaudRate = false;
            melsecFxSerial.SleepTime = 200;

            melsecFxSerial.Open();//连接
            //判断是否连接成功
            if (melsecFxSerial.IsOpen())
            {
               
            }
            else
            {
                throw new Exception("连接失败！");
               
            }
        }

        private object objectLock = new object();
        public override void ReadSystemData()
        {
            lock (objectLock)
            {

                #region 第7板块

                HslCommunication.OperateResult<Int32[]> part7 = melsecFxSerial.ReadInt32("D466", 2);
                if (part7.IsSuccess)
                {

                    QianFengDaoUse = part7.Content[0];
                    QianFengDao = part7.Content[1];

                }
                else
                {

                    LOG.WriteLog("PLC读取系统参数失败！");
                }

                #endregion


                #region 第6板块
                HslCommunication.OperateResult<Int32[]> part6 = melsecFxSerial.ReadInt32("D400", 10);
                if (part6.IsSuccess)
                {

                    MuBiaoJuan = part6.Content[0];
                    DangqianJuanshu = part6.Content[1];

                    MubiaoBaozhuangCount = part6.Content[2];
                    BaozhuangCount = part6.Content[3];

                    MubiaoQianKongCount = part6.Content[4];
                    QianKongCount = part6.Content[5];

                    MubiaoHouKongCount = part6.Content[6];
                    HouKongCount = part6.Content[7];

                    QianFengDaoQiGangUse = part6.Content[8];
                    QianFengDaoQiGang = part6.Content[9];



                }
                else
                {
                    LOG.WriteLog("PLC读取系统参数失败！");
                }
                #endregion
                Thread.Sleep(200);
                #region 第5板块
                HslCommunication.OperateResult<Int32[]> part5 = melsecFxSerial.ReadInt32("D480", 10);
                if (part5.IsSuccess)
                {

                    XIzuiUse = part5.Content[0];
                    XIzui = part5.Content[1];

                    XIzuiTanhuangUse = part5.Content[2];
                    XIzuiTanhuang = part5.Content[3];

                    JZ1QiGangUse = part5.Content[4];
                    JZ1QiGang = part5.Content[5];

                    JZ2QiGangUse = part5.Content[6];
                    JZ2QiGang = part5.Content[7];

                    QianFengDaoQiGangUse = part5.Content[8];
                    QianFengDaoQiGang = part5.Content[9];



                }
                else
                {

                    LOG.WriteLog("PLC读取系统参数失败！");
                }
                #endregion

                Thread.Sleep(200);

                #region 第四板块

                HslCommunication.OperateResult<Int32[]> part4 = melsecFxSerial.ReadInt32("D374", 2);
                if (part4.IsSuccess)
                {

                    JZ3pianUse = part4.Content[0];
                    JZ3pian = part4.Content[1];





                }
                else
                {

                    LOG.WriteLog("PLC读取系统参数失败！");
                }

                #endregion
                Thread.Sleep(200);

                #region 第三板块
                HslCommunication.OperateResult<Int32[]> part3 = melsecFxSerial.ReadInt32("D300", 10);
                if (part3.IsSuccess)
                {

                    HouFengDaoQiGangUse = part3.Content[0];
                    HouFengDaoQiGang = part3.Content[1];

                    HouFengDaoUse = part3.Content[2];
                    HouFengDao = part3.Content[3];

                    JZ1pianUse = part3.Content[4];
                    JZ1pian = part3.Content[5];

                    JiXing1PianUse = part3.Content[6];
                    JiXing1Pian = part3.Content[7];

                    JZ2pianUse = part3.Content[8];
                    JZ2pian = part3.Content[9];



                }
                else
                {

                    LOG.WriteLog("PLC读取系统参数失败！");
                }
                #endregion
                Thread.Sleep(200);

                #region 第二板块
                HslCommunication.OperateResult<Int32[]> Tichu = melsecFxSerial.ReadInt32("D320", 16);
                if (Tichu.IsSuccess)
                {

                    Tichu1QigangUse = Tichu.Content[0];
                    Tichu1Qigang = Tichu.Content[1];
                    Tichu2QigangUse = Tichu.Content[2];
                    Tichu2Qigang = Tichu.Content[3];

                    Tichu3QigangUse = Tichu.Content[4];
                    Tichu3Qigang = Tichu.Content[5];

                    Tichu4QigangUse = Tichu.Content[6];
                    Tichu4Qigang = Tichu.Content[7];

                    Tichu5QigangUse = Tichu.Content[8];
                    Tichu5Qigang = Tichu.Content[9];

                    Tichu6QigangUse = Tichu.Content[10];
                    Tichu6Qigang = Tichu.Content[11];

                    Tichu7QigangUse = Tichu.Content[12];
                    Tichu7Qigang = Tichu.Content[13];

                    Tichu8QigangUse = Tichu.Content[14];
                    Tichu8Qigang = Tichu.Content[15];

                }
                else
                {

                    LOG.WriteLog("PLC读取系统参数失败！");
                }


                #endregion
                Thread.Sleep(200);

                #region 第一板块
                HslCommunication.OperateResult<Int32[]> Tanzhen = melsecFxSerial.ReadInt32("D442", 16);
                if (Tanzhen.IsSuccess)
                {

                    TanZhen1Use = Tanzhen.Content[0];
                    TanZhen1Count = Tanzhen.Content[1];
                    TanZhen2Use = Tanzhen.Content[2];
                    TanZhen2Count = Tanzhen.Content[3];

                    TanZhen3Use = Tanzhen.Content[4];
                    TanZhen3Count = Tanzhen.Content[5];

                    TanZhen4Use = Tanzhen.Content[6];
                    TanZhen4Count = Tanzhen.Content[7];

                    TanZhen5Use = Tanzhen.Content[8];
                    TanZhen5Count = Tanzhen.Content[9];

                    TanZhen6Use = Tanzhen.Content[10];
                    TanZhen6Count = Tanzhen.Content[11];

                    TanZhen7Use = Tanzhen.Content[12];
                    TanZhen7Count = Tanzhen.Content[13];

                    TanZhen8Use = Tanzhen.Content[14];
                    TanZhen8Count = Tanzhen.Content[15];

                }
                else
                {

                    LOG.WriteLog("PLC读取系统参数失败！");
                }
                #endregion
                Thread.Sleep(200);



            }
        }

        public override bool WriteEnable(List<bool> boolList)
        {
            byte data = ByteLib.GetByteFromBoolArray(boolList.ToArray());

            HslCommunication.OperateResult write = melsecFxSerial.Write("D162", data);
            if (write.IsSuccess)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

 

        public override List<int> ReadPressure()
        {
            List<int> ints = new List<int>();
            HslCommunication.OperateResult<Int32[]> readPressure = melsecFxSerial.ReadInt32("D90", 2);
            if (readPressure.IsSuccess)
            {
                ints.Add(readPressure.Content[0]);
                ints.Add(readPressure.Content[1]);

            }
            else
            {

            }

            return ints;
        }

        public override bool WriteMesAlarm()
        {
            HslCommunication.OperateResult write = melsecFxSerial.Write("D6000", 1);
            if (write.IsSuccess)
            {
                return true;
            }
            else
            {
                return false;
            }
        }





        public override bool WriteYTest(string address, bool value)
        {

            Thread thread = new Thread(() => WriteToPLC_Y(address, value));
            thread.Start();

            return false;


        }



        private bool WriteToPLC_Y(string address, bool value)
        {
            HslCommunication.OperateResult write = melsecFxSerial.Write(address, value);
            if (write.IsSuccess)
            {
                Thread.Sleep(500);
                HslCommunication.OperateResult write2 = melsecFxSerial.Write(address, false);

                if (write2.IsSuccess)
                {
                    return true;
                }
                else
                {
                    throw new Exception(write2.Message);

                }



            }
            else
            {
                throw new Exception(write.Message);
            }
        }

        public override bool WriteMesData(MesInfo returnToView)
        {
            List<Int32> intList = new List<Int32>();
            intList.Add((Int32)returnToView.Packageqty);//数量
            intList.Add((Int32)returnToView.FrontSpace);//前空格
            intList.Add((Int32)returnToView.AfterSpace);//后空格
            intList.Add((Int32)returnToView.Blankqty);//不封膜数量
            intList.Add((Int32)returnToView.Checkqty);//尾数

            HslCommunication.OperateResult write = melsecFxSerial.Write("D150", intList.ToArray());
            if (write.IsSuccess)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool WriteEnableInt(List<int> boolList)
        {
            throw new NotImplementedException();
        }
        public override int ReadDeviceStues()
        {

            HslCommunication.OperateResult<int> PressureList = melsecFxSerial.ReadInt32(JsonSaveEXT.deviceParameterJsonGv.PLCDataConfig.DeviceStuesAddress);
            if (PressureList.IsSuccess)
            {
                return PressureList.Content;
            }
            else
            {
                LOG.WriteLog("PLC读取设备状态失败！");
                return 0;
            }


        }

        public override List<float> ReadPressureFloat()
        {
            throw new NotImplementedException();
        }
    }
}
