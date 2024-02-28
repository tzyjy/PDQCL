using ATestPackagingMachineWpf1.Common;
using BTest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchSocket.Core;
using TouchSocket.Sockets;

namespace ATestPackagingMachineWpf1.DeviceFile.Mes
{
    public class PCClientMESAPI
    {
        public static string timecsv = DateTime.Now.ToString("yyyy_MM_dd");
        #region 连接Mes服务器
        /// <summary>
        /// 连接Mes服务器
        /// </summary>
        /// <returns></returns>
        public static CSACollection Connect()
        {
            CSACollection fh = new CSACollection();
            fh["txnname"] = "connect";
            return Send(fh);
        }
        #endregion

        #region 回复标准件测试结果--1
        public static CSACollection ReplyStandTestValue(CSACollection fh)
        {



            return Send(fh);

        }
        #endregion

        #region 清料完成回复
        public static CSACollection ReplyClearOutOver()
        {
            CSACollection fh = new CSACollection();

            fh.txnname = "ReplyClearOutOver";
            fh.returncode = 0;
            fh.returnmessage = "OK";
            return Send(fh);

        }
        #endregion

        #region NG重测完成回复
        public static CSACollection ReplyNGTestOver()
        {
            CSACollection fh = new CSACollection();

            fh.txnname = "ReplyNGTestOver";
            fh.returncode = 0;
            fh.returnmessage = "OK";
            return Send(fh);

        }
        #endregion

        #region 标准件完成测试回复--1
        public static CSACollection ReplySTDTestOver()
        {
            CSACollection fh = new CSACollection();

            fh.txnname = "ReplySTDTestOver";

            return Send(fh);

        }
        #endregion

        #region 工单完成回复
        public static CSACollection ReplyFinishOrder()
        {
            CSACollection fh = new CSACollection();

            fh.txnname = "ReplyFinishOrder";
            fh.returncode = 0;
            fh.returnmessage = "OK";
            fh.Add("wono", GV.MesInfo.Wono);
            fh.Add("passqty", "0");
            fh.Add("failqty", "0");
            fh.Add("inputqty", "0");

            return Send(fh);

        }
        #endregion

        #region 机台状态回复---1
        public static CSACollection ReplyMachineStatus()
        {
            CSACollection fh = new CSACollection();

         

            try
            {
                DV.PLC5U?.ReadSystemData();
                string status = string.Empty;
                status = DV.PLC5U?.ReadDeviceStues().ToString();

                fh.txnname = "ReplyMachineStatus";
                fh.returncode = 0;
                fh.returnmessage = "OK";
                fh.Add("Status", status);
                fh.Add("Statuscode", status);
                fh.Add("FPY", GV.AllProductNumber.CountPassRate().ToString());
                fh.Add("CycleTime", "0");//未做
                fh.Add("Suction_spring", DV.PLC5U?.XIzuiTanhuangUse.ToString());
                fh.Add("Suction", DV.PLC5U?.XIzuiUse.ToString());
                fh.Add("Correct_clipcy1", DV.PLC5U?.JZ1QiGangUse.ToString());
                fh.Add("Correct_clipcy2", DV.PLC5U?.JZ2QiGangUse.ToString());
                fh.Add("Correct_clip1", DV.PLC5U?.JZ1pianUse.ToString());
                fh.Add("Correct_clip2", DV.PLC5U?.JZ2pianUse.ToString());
                fh.Add("Polarity_clip", DV.PLC5U?.JiXing1PianUse.ToString());
                fh.Add("Sealing1_temp", "0");
                fh.Add("Sealing2_temp", "0");
                fh.Add("Sealing1_ upCY", DV.PLC5U?.QianFengDaoQiGangUse.ToString());
                fh.Add("Sealing2_ upCY", DV.PLC5U?.HouFengDaoQiGangUse.ToString());
                fh.Add("Sealing1", DV.PLC5U?.QianFengDaoUse.ToString());
                fh.Add("Sealing2", DV.PLC5U?.HouFengDaoUse.ToString());
                fh.Add("inputqty_Test1", DV.DCR1.InstrumentDataParentClassList[0].TotalNuber.ToString());
                fh.Add("passqty_ Test1", DV.DCR1.InstrumentDataParentClassList[0].NumberGoodProducts.ToString());

                fh.Add("inputqty_Test2", DV.IR.InstrumentDataParentClassList[0].TotalNuber.ToString());
                fh.Add("passqty_ Test2", DV.IR.InstrumentDataParentClassList[0].TotalNuber.ToString());

                fh.Add("inputqty_Test3", DV.GanZhi1.InstrumentDataParentClassList[0].TotalNuber.ToString());
                fh.Add("passqty_ Test3", DV.GanZhi1.InstrumentDataParentClassList[0].TotalNuber.ToString());

                fh.Add("inputqty_Test4", DV.GanZhi2.InstrumentDataParentClassList[0].TotalNuber.ToString());
                fh.Add("passqty_ Test4", DV.GanZhi2.InstrumentDataParentClassList[0].TotalNuber.ToString());





                fh.Add("Test1_pin", DV.PLC5U?.TanZhen1Use.ToString());
                fh.Add("Test2_pin", DV.PLC5U?.TanZhen2Use.ToString());
                fh.Add("Test3_pin", DV.PLC5U?.TanZhen3Use.ToString());

                fh.Add("Test4_pin", DV.PLC5U?.TanZhen4Use.ToString());
                fh.Add("Test5_pin", DV.PLC5U?.TanZhen5Use.ToString());
                fh.Add("Test6_pin", DV.PLC5U?.TanZhen6Use.ToString());

            }
            catch (Exception)
            {
                fh.txnname = "ReplyMachineStatus";
                fh.returncode = 1;
                fh.returnmessage = "Error";
               
            }






            return Send(fh);

        }
        #endregion

        #region 当卷Reel入料完成---1
        public static CSACollection ReelFinish()
        {
            CSACollection fh = new CSACollection();

            fh.txnname = "ReelFinish";
            fh.returncode = 0;
            fh.returnmessage = "OK";
            return Send(fh);

        }
        #endregion

        #region PC客户端发送数据给Mes服务器

        public static string MesServiceIP = "127.0.0.1";
        public static string MesServicePort = "12003";
        public static CSACollection Send(CSACollection qq)
        {

            TouchSocket.Sockets.TcpClient tcpClient = new TouchSocket.Sockets.TcpClient();
            tcpClient.Connected += (client, e) => { };//成功连接到服务器
            tcpClient.Disconnected += (client, e) => { };//从服务器断开连接，当连接不成功时不会触发。
            //tcpClient.Received += (client, byteBlock, requestInfo) =>
            //{
            //    //从服务器收到信息
            //    string mes = Encoding.UTF8.GetString(byteBlock.Buffer, 0, byteBlock.Len);
            //    Console.WriteLine($"接收到信息：{mes}");
            //};
            //声明配置
            TouchSocketConfig config = new TouchSocketConfig();
            config.SetRemoteIPHost(new IPHost($"{MesServiceIP}:{MesServicePort}"))
                .SetDataHandlingAdapter(() => { return new FixedHeaderPackageAdapter(); })//配置适配器
                .UsePlugin();

            //载入配置
            try
            {
                tcpClient.Setup(config);
                tcpClient.Connect();
                var waitClient = tcpClient.GetWaitingClient(new WaitingOptions()
                {
                    AdapterFilter = AdapterFilter.AllAdapter,//表示发送和接收的数据都会经过适配器
                    BreakTrigger = true,//表示当连接断开时，会立即触发
                    ThrowBreakException = true//表示当连接断开时，是否触发异常
                });
                var data = waitClient.SendThenResponse(qq.GetCSBytes());
                Console.WriteLine("PC发送：" + qq.GetCSText());
                List<string> list2 = new List<string>();
                list2.Add(DateTime.Now.ToString());
                list2.Add("PC客户端发送消息至Mes服务器:" + qq.GetCSText());
                CsvHelper.WriteToCSVMesInfo(timecsv, list2);



                var cs = CSACollection.GetCS(data.Data.GetString_Unicode());
                Console.WriteLine("PC客户端接受Mes服务器消息：" + cs.GetCSText());

                List<string> list3 = new List<string>();
                list3.Add(DateTime.Now.ToString());
                list3.Add("PC客户端接受Mes服务器消息:" + cs.GetCSText());
                CsvHelper.WriteToCSVMesInfo(timecsv, list3);



                tcpClient.Close();
                return cs;


            }
            catch (Exception)
            {

                return null;

            }
        }
        #endregion


    }
}
