using ATestPackagingMachineWpf1.Common;
using BTest.Common;
using BTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZModels;
using TouchSocket.Sockets;
using TouchSocket.Core;
using System.Threading;
using BTest.LogHelper;
using ATestPackagingMachineWpf1.ZModels;
using Prism.Events;

namespace ATestPackagingMachineWpf1.DeviceFile.Mes
{
    public class MESServrMESAPI
    {
        #region 定义变量
        public static TcpService service;
        public static Dictionary<string, MethodInfo> DiCommand = new Dictionary<string, MethodInfo>();
        public static string LocalServiceIP = "127.0.0.1";
        public static string LocalServicePort = "13003";
        public static string timecsv = DateTime.Now.ToString("yyyy_MM_dd");
        #endregion


        private static IEventAggregator eventAggregator;
        public MESServrMESAPI(IEventAggregator eventAggregator1)
        {
            eventAggregator = eventAggregator1;
        }

        #region API服务器启动
        /// <summary>
        /// API服务器启动
        /// </summary>
        public static void Start()
        {
            if (service == null)
            {
                DiCommand.Clear();
                //命令注册
                //api特性
                var tx = typeof(MESAPIAttribute);
                //api集合类
                var type = typeof(MESServrMESAPI);
                //获取存在api特性的函数
                var ml = type.GetMethods().Where(i => i.GetCustomAttributes(tx, false).Length > 0).ToList();
                foreach (var item in ml)
                {
                    //api特性类
                    var txl = (MESAPIAttribute)item.GetCustomAttributes(tx, false).FirstOrDefault();
                    //特性的命名，函数对象
                    DiCommand.Add(txl.Command, item);
                }

                //服务器
                service = new TcpService();
                service.Connecting = (client, e) =>
                {

                    Console.WriteLine("设备尝试连接！" + client.IP);



                };//有客户端正在连接
                service.Connected = (client, e) =>
                {

                    Console.WriteLine("设备成功连接！" + client.IP);
                };//有客户端成功连接
                service.Disconnected = (client, e) =>
                {

                    Console.WriteLine("设备断开连接！" + client.IP);
                };//有客户端断开连接
                service.Received = (client, byteBlock, requestInfo) =>
                {
                    //从客户端收到信息
                    Console.WriteLine("收到信息！{0}" + client.IP, byteBlock.Len);
                    string mes = Encoding.Unicode.GetString(byteBlock.Buffer, 0, byteBlock.Len);
                    var cs = CSACollection.GetCS(mes);
                    if (cs != null)
                    {
                        //CSACollection fh = new CSACollection();
                        var zcs = cs.Get("txnname");
                        if (zcs != null)
                        {
                            //正常请求，MEs客户端请求：
                            Console.WriteLine("Mes客户端请求：{0}", zcs.CSValue1);

                            Console.WriteLine(cs.GetCSText());

                            List<string> list = new List<string>();
                            list.Add(DateTime.Now.ToString());
                            list.Add("Mes客户端发送消息给PC服务器：" + cs.GetCSText());
                            CsvHelper.WriteToCSVMesInfo(timecsv, list);
                            try
                            {
                                //获取命令是否存在
                                if (DiCommand.ContainsKey(zcs.CSValue1))
                                {
                                    //null, new object[] { cs }
                                    //命令函数执行，传入请求参数,,,,,,,,第一个参数输入null 是应为函数是静态函数。如果是普通函数，输入api集合类的new 对象就可以
                                    var fh1 = DiCommand[zcs.CSValue1].Invoke(null, new object[] { cs });
                                    if (fh1 != null)
                                    {
                                        Console.WriteLine("PC服务器发送数据给MES客户端:" + ((CSACollection)fh1).GetCSText());
                                        client.Send(((CSACollection)fh1).GetCSBytes());


                                        List<string> list2 = new List<string>();
                                        list2.Add(DateTime.Now.ToString());
                                        list2.Add("PC服务器发送数据给MES客户端:" + ((CSACollection)fh1).GetCSText());
                                        CsvHelper.WriteToCSVMesInfo(timecsv, list2);


                                    }
                                    else
                                    {
                                        CSACollection fh = new CSACollection();
                                        fh.txnname = zcs.CSValue1;
                                        fh.returncode = 1;
                                        fh.returnmessage = "MethodError！";
                                        client.Send(fh.GetCSBytes());
                                    }
                                }
                                else
                                {
                                    //未知命令
                                    CSACollection fh = new CSACollection();
                                    fh.Add(new CSA("txnname", zcs.CSValue1));
                                    fh.Add(new CSA("returncode", "2"));
                                    fh.Add(new CSA("returnmessage", "NotFoundCmd"));
                                    client.Send(fh.GetCSBytes());
                                }
                            }
                            catch (Exception ex)
                            {
                                CSACollection fh = new CSACollection();
                                fh.txnname = zcs.CSValue1;
                                fh.returncode = 1;
                                fh.returnmessage = "ExceptionError！" + ex.Message;
                                client.Send(fh.GetCSBytes());
                            }


                        }
                        else
                        {
                            //未知命令
                            CSACollection fh = new CSACollection();
                            fh.Add(new CSA("txnname", "serviceAPI"));
                            fh.Add(new CSA("returncode", "2"));
                            fh.Add(new CSA("returnmessage", "NotFoundCmd"));
                            client.Send(fh.GetCSBytes());
                            ////错误请求
                            //client.Close();
                        }
                        //client.Send(fh.GetCSBytes());
                    }
                    else
                    {
                        //错误请求
                        client.Close();
                    }

                };

                service.Setup(new TouchSocketConfig()//载入配置     
                    .SetListenIPHosts(new IPHost[] { new IPHost(string.Format($"tcp://{LocalServiceIP}:{LocalServicePort}")) })//同时监听两个地址
                                 .SetDataHandlingAdapter(() => { return new FixedHeaderPackageAdapter(); })//配置适配器
                    .ConfigureContainer(a =>//容器的配置顺序应该在最前面
                    {
                        a.AddConsoleLogger();//添加一个控制台日志注入（注意：在maui中控制台日志不可用）
                    })
                    .ConfigurePlugins(a =>
                    {
                        //a.Add();//此处可以添加插件
                    }))
                    .Start();//启动

            }
        }
        #endregion

        #region 服务器停止
        public static void Stop()
        {
            if (service != null)
            {
                service.Clear();
                service.Stop();
                service = null;
            }
        }
        #endregion



        #region mes连接API
        //MES連線請求API
        [MESAPI("connect")]
        public static CSACollection connect(CSACollection qq)
        {
            CSACollection fh = new CSACollection();
            fh.Add(new CSA("txnname", "connect"));
            fh.Add(new CSA("returncode", "0"));
            fh.Add(new CSA("returnmessage", "OK"));

            return fh;
        }
        #endregion

        #region  Mes机台参数查询API
        [MESAPI("GetMachinePara")]
        public static CSACollection GetMachinePara(CSACollection qq)
        {
            CSACollection fh = new CSACollection();
            //回复状态
            fh.Add("txnname", "GetMachinePara");
            fh.Add("returncode", "0");
            fh.Add("returnmessage", "OK");

            //仪器使用仪表类型
            CSA secondcs = fh.Add("AmmeterName", null);
            secondcs["1"] = "AX1152D_1";
            secondcs.Add("2", "AX1152D_2");
            secondcs.Add("3", "Chroma11210");
            secondcs.Add("4", "Chroma19301");
            secondcs.Add("5", "IM3570_1");
            secondcs.Add("6", "IM3570_2");
            secondcs.Add("7", "IM3570_3");

            //DCR1参数
            CSA dcr = fh.Add("AmmeterParm1", null);
            dcr.Add("DCR_MIN", DV.DCR1.aX11520DParameter.DCRLow.ToString());
            dcr.Add("DCR_MAX", DV.DCR1.aX11520DParameter.DCRHigh.ToString());
            dcr.Add("DCR_RANGE", DV.DCR1.aX11520DParameter.TestScale.ToString());

            //DCR1参数
            CSA dcr2 = fh.Add("AmmeterParm2", null);
            dcr2.Add("DCR_MIN", DV.DCR2.aX11520DParameter.DCRLow.ToString());
            dcr2.Add("DCR_MAX", DV.DCR2.aX11520DParameter.DCRHigh.ToString());
            dcr2.Add("DCR_RANGE", DV.DCR2.aX11520DParameter.TestScale.ToString());


            //IR参数
            CSA ir = fh.Add("AmmeterParm3", null);
            ir.Add("IR_MIN", DV.IR.ir11210Parameter.LowerLimit.ToString());
            ir.Add("IR_MAX", DV.IR.ir11210Parameter.HighLimit.ToString());



            //高感
            CSA gaogan = fh.Add("AmmeterParm4", null);
            gaogan.Add("LCR_MIN", DV.GanZhi1.IM3570PrameterList[0].LSLow.ToString());
            gaogan.Add("LCR_MAX", DV.GanZhi1.IM3570PrameterList[0].LSHIGH.ToString());
            gaogan.Add("LCR_NORMAL", DV.GanZhi1.IM3570PrameterList[0].LSstandard.ToString());

            //低感
            CSA digan = fh.Add("AmmeterParm5", null);
            digan.Add("LCRUH1_MIN", DV.GanZhi2.IM3570PrameterList[0].LSLow.ToString());
            digan.Add("LCRUH1_MAX", DV.GanZhi2.IM3570PrameterList[0].LSHIGH.ToString());
            digan.Add("LCRUH1_NORMAL", DV.GanZhi2.IM3570PrameterList[0].LSstandard.ToString());
            digan.Add("LCRUH2_MIN", DV.GanZhi2.IM3570PrameterList[1].LSLow.ToString());
            digan.Add("LCRUH2_MAX", DV.GanZhi2.IM3570PrameterList[1].LSHIGH.ToString());
            digan.Add("LCRUH2_NORMAL", DV.GanZhi2.IM3570PrameterList[1].LSstandard.ToString());


            //高感



            //极性1



            DV.PLC5U?.ReadSystemData();

            fh.Add("packageqty", DV.PLC5U?.BaozhuangCount.ToString());
            fh.Add("beforespaceqty", DV.PLC5U?.QianKongCount.ToString());
            fh.Add("afterspaceqty", DV.PLC5U?.HouKongCount.ToString());
            fh.Add("matno", GV.MesInfo.Wono == null ? "" : GV.MesInfo.Wono);
            fh.Add("tpno", GV.MesInfo.Tpno == null ? "" : GV.MesInfo.Tpno);
            string qwe = fh.GetCSText();
            return fh;
        }
        #endregion

        #region 机台状态查询Api
        //2.5. 機台狀態查詢API 
        [MESAPI("GetMachineStatus")]
        public static CSACollection GetMachineStatus(CSACollection qq)

        {
            CSACollection cs = new CSACollection();
        

            try
            {
                string status = string.Empty;
                status = DV.PLC5U?.ReadDeviceStues().ToString();

                DV.PLC5U?.ReadSystemData();

                cs.Add("txnname", "GetMachineStatus");
                cs.Add("returncode", "0");
                cs.Add("returnmessage", "OK");

                cs.Add("passqty", GV.AllProductNumber.GoodNumber.ToString());//总良品数
                cs.Add("failqty", GV.AllProductNumber.BadNumber.ToString());//总不良数
                cs.Add("inputqty", GV.AllProductNumber.TatalNumber.ToString());//总数
                cs.Add("status", "0.01");//一颗产品时间
                cs.Add("status", status);//设备状态
                cs.Add("statuscode", status);//设备状态
                cs.Add("matno", GV.MesInfo.Wono == null ? "" : GV.MesInfo.Wono);//工单号
                cs.Add("tpno", GV.MesInfo.Tpno == null ? "" : GV.MesInfo.Tpno);//工单数量
                cs.Add(new CSA("ReelQty", DV.PLC5U?.MuBiaoJuan.ToString()));//目标卷数
            }
            catch (Exception)
            {

                cs.Add("txnname", "GetMachineStatus");
                cs.Add("returncode", "1");
                cs.Add("returnmessage", "Error");
            }



            return cs;

        }
        #endregion

        #region 参数设置APi-------------------------------------------------111111

        private static string MesPath = "Json\\MesInfo.json";
        [MESAPI("SetPara")]
        public static CSACollection SetPara(CSACollection qq)
        {
            CSACollection fh = new CSACollection();
            fh.txnname = "SetPara";
            fh.returncode = 0;
            fh.returnmessage = "OK";
            try
            {


                #region 电表


                if (qq.AmmeterParm1 != null)
                {
                    List<decimal> decimals = InstrumentCommon.DcrDataConvert(qq.AmmeterParm1.DCR_MIN, qq.AmmeterParm1.DCR_MAX, qq.AmmeterParm1.DCR_RANGE);
                    List<int> intList = InstrumentCommon.LowHighDeal(qq.AmmeterParm1.DCR_RANGE, decimals[0], decimals[1]);
                    DV.DCR1.aX11520DParameter.DCRLow = intList[0];
                    DV.DCR1.aX11520DParameter.DCRHigh = intList[1];
                    DV.DCR1.aX11520DParameter.TestScale = qq.AmmeterParm1.DCR_RANGE;
                    DV.DCR1.WriteDeviceConfig();

                }
                if (qq.AmmeterParm2 != null)
                {
                    List<decimal> decimals = InstrumentCommon.DcrDataConvert(qq.AmmeterParm2.DCR_MIN, qq.AmmeterParm2.DCR_MAX, qq.AmmeterParm2.DCR_RANGE);
                    List<int> intList = InstrumentCommon.LowHighDeal(qq.AmmeterParm2.DCR_RANGE, decimals[0], decimals[1]);
                    DV.DCR2.aX11520DParameter.DCRLow = intList[0];
                    DV.DCR2.aX11520DParameter.DCRHigh = intList[1];
                    DV.DCR2.aX11520DParameter.TestScale = qq.AmmeterParm2.DCR_RANGE;
                    DV.DCR2.WriteDeviceConfig();

                }


                if (qq.AmmeterParm3 != null)
                {
                    DV.IR.ir11210Parameter.Voltage = qq.AmmeterParm3.TP_IR_VOLT;
                    DV.IR.ir11210Parameter.TestTime = qq.AmmeterParm3.TP_IR_TESTTIME;
                    DV.IR.ir11210Parameter.LowerLimit = qq.AmmeterParm3.TP_IR_MIN;
                    DV.IR.ir11210Parameter.HighLimit = (Convert.ToInt64(qq.AmmeterParm3.TP_IR_MAX)).ToString("e");

                    DV.IR.WriteDeviceConfig();


                }


                if (qq.AmmeterParm5 != null)
                {

                    DV.GanZhi1.IM3570PrameterList[0].LSstandard = qq.AmmeterParm5.LCR_NORMAL.ToString();
                    DV.GanZhi1.IM3570PrameterList[0].LSLow = qq.AmmeterParm5.LCR_MIN * 100;
                    DV.GanZhi1.IM3570PrameterList[0].LSHIGH = qq.AmmeterParm5.LCR_MAX * 100;
                    DV.GanZhi1.IM3570PrameterList[0].TestFrequency = qq.AmmeterParm5.L_FREQUENCY;
                    DV.GanZhi1.IM3570PrameterList[0].Voltage = qq.AmmeterParm5.L_VOLTAGE;



                    DV.GanZhi1.IM3570PrameterList[1].TestFrequency = qq.AmmeterParm5.ESR_FREQUENCY;
                    DV.GanZhi1.IM3570PrameterList[1].Voltage = qq.AmmeterParm5.ESR_VOLTAGE;
                    DV.GanZhi1.IM3570PrameterList[1].RSLow = 0m;
                    DV.GanZhi1.IM3570PrameterList[1].RSHIGH = qq.AmmeterParm5.ESR_MAX;

                    DV.GanZhi1.WriteDeviceConfigEX(DV.GanZhi1.IM3570PrameterList[0]);
                    Thread.Sleep(100);
                    DV.GanZhi1.WriteDeviceConfigEX(DV.GanZhi1.IM3570PrameterList[1]);


                }

                if (qq.AmmeterParm6 != null)
                {

                    DV.GanZhi2.IM3570PrameterList[0].LSstandard = qq.AmmeterParm6.LCR_NORMAL.ToString();
                    DV.GanZhi2.IM3570PrameterList[0].LSLow = qq.AmmeterParm6.LCR_MIN * 100;
                    DV.GanZhi2.IM3570PrameterList[0].LSHIGH = qq.AmmeterParm6.LCR_MAX * 100;
                    DV.GanZhi2.IM3570PrameterList[0].TestFrequency = qq.AmmeterParm6.L_FREQUENCY;
                    DV.GanZhi2.IM3570PrameterList[0].Voltage = qq.AmmeterParm6.L_VOLTAGE;



                    DV.GanZhi2.IM3570PrameterList[1].TestFrequency = qq.AmmeterParm6.ESR_FREQUENCY;
                    DV.GanZhi2.IM3570PrameterList[1].Voltage = qq.AmmeterParm6.ESR_VOLTAGE;
                    DV.GanZhi2.IM3570PrameterList[1].RSLow = 0m;
                    DV.GanZhi2.IM3570PrameterList[1].RSHIGH = qq.AmmeterParm6.ESR_MAX;

                    DV.GanZhi2.WriteDeviceConfigEX(DV.GanZhi2.IM3570PrameterList[0]);
                    Thread.Sleep(100);
                    DV.GanZhi2.WriteDeviceConfigEX(DV.GanZhi2.IM3570PrameterList[1]);
                }







                //是否禁用

                if (qq.Get("DCR_ENABLE")?.CSValue1 == "Y")
                {
                    DV.DCR1.Enable = true;
                    DV.DCR2.Enable = true;

                }
                else
                {
                    DV.DCR1.Enable = false;
                    DV.DCR2.Enable = false;
                }
                if (qq.Get("L_ENABLE")?.CSValue1 == "Y")
                {
                    DV.GanZhi1.Enable = true;
                    DV.GanZhi2.Enable = true;

                }
                else
                {
                    DV.GanZhi2.Enable = false;
                    DV.GanZhi1.Enable = false;

                }
                if (qq.Get("WAVE_ENABLE")?.CSValue1 == "Y")
                {

                }
                else
                {

                }
                if (qq.Get("POLARITY_ENABLE")?.CSValue1 == "Y")
                {

                }
                else
                {

                }
                if (qq.Get("IR_ENABLE")?.CSValue1 == "Y")
                {
                    DV.IR.Enable = true;
                }
                else
                {
                    DV.IR.Enable = false;
                }
                //保存所有仪器参数及禁用参数
                DV.SaveListDeviceBase();
                #endregion

            }
            catch (Exception ex)
            {
                LogPublish(new LogInfo() { OK = false, InfoText = "Mes带电表参数错误" + ex.Message });
                LOG.WriteLog("Mes带电表参数错误" + ex.Message);

                fh.txnname = "SetPara";
                fh.returncode = 1;
                fh.returnmessage = "DianBiao" + ex.Message;
            }


            //Mes数量
            GV.MesInfo.Packageqty = int.Parse(qq.Get("packageqty")?.CSValue1);
            GV.MesInfo.FrontSpace = int.Parse(qq.Get("beforespaceqty")?.CSValue1);
            GV.MesInfo.AfterSpace = int.Parse(qq.Get("afterspaceqty")?.CSValue1);
            GV.MesInfo.Blankqty = int.Parse(qq.Get("blankqty")?.CSValue1);
            GV.MesInfo.Checkqty = int.Parse(qq.Get("checkqty")?.CSValue1);


            //Mes信息
            GV.MesInfo.Wono = qq.Get("wono")?.CSValue1;
            GV.MesInfo.Woqty = qq.Get("woqty")?.CSValue1;
            GV.MesInfo.Equipmentid = qq.Get("equipmentid")?.CSValue1;
            GV.MesInfo.Matno = qq.Get("matno")?.CSValue1;
            GV.MesInfo.Tpno = qq.Get("tpno")?.CSValue1;

            //相机
            string mark1 = qq.Get("markmatno")?.CSValue1;
            string mark2 = qq.Get("markdatecode")?.CSValue1;
            string mark3 = qq.Get("markseqno")?.CSValue1;
            GV.MesInfo.MarkString = mark1 + mark2 + mark3;

            JsonSaveHelper jsonSaveHelper = new JsonSaveHelper();
            MesInfo info = jsonSaveHelper.ReadOneJson<MesInfo>(MesPath);


            #region 清除产品数量
            if (info != null && info.Wono != GV.MesInfo.Wono)//如果上次工单号与这次不等
            {
                //清除产品数量
                InstrumentCommon.ClearData();
                SaveCsv(info);

            }
            else
            {
                // OnRunLogEvent(1, "已刷工单，产品清零失效");
            }

            #endregion

            try
            {
                #region 写PLC
                List<bool> boolList = new List<bool>();
                boolList.Add(DV.DCR1.Enable);
                boolList.Add(DV.DCR2.Enable);
                boolList.Add(DV.IR.Enable);
                boolList.Add(DV.GanZhi1.Enable);
                boolList.Add(DV.GanZhi2.Enable);

                DV.PLC5U?.WriteEnable(boolList);

                DV.PLC5U?.WriteMesData(info);
                //扫工单
                PCI1730WriteAndRead.RrefeshWono();
                #endregion 
            }
            catch (Exception ex)
            {
                LogPublish(new LogInfo() { OK = false, InfoText = "Mes带PLC参数错误" + ex.Message });
                LOG.WriteLog("Mes带PLC参数错误" + ex.Message);
                fh.txnname = "SetPara";
                fh.returncode = 1;
                fh.returnmessage = "PLC" + ex.Message;
            }

            #region 相机
            try
            {
                DV.CCD.Send(info.MarkString);
            }
            catch (Exception ex)
            {

                LogPublish(new LogInfo() { OK = false, InfoText = "Mes带CCD字符错误" + ex.Message });
                LOG.WriteLog("Mes带CCD字符错误" + ex.Message);
                fh.txnname = "SetPara";
                fh.returncode = 1;
                fh.returnmessage = "CCD" + ex.Message;
            }

            #endregion


            jsonSaveHelper.WriteJson(info, MesPath);
            //把Mes信息刷新
            GV.MesInfoUpdate = true;




            return fh;
        }


        #endregion

        #region 启动设备=======1
        [MESAPI("Start")]
        public static CSACollection Start(CSACollection qq)
        {
            CSACollection fh = new CSACollection();
            try
            {
                bool[] boolDIATarray = DV.IO1730.ReadAllDi();

                string returnText = string.Empty;
                if (boolDIATarray[2])
                {

                    fh.txnname = "Start";
                    fh.returncode = 0;
                    fh.returnmessage = "OK";
                    GV.ButtonAtuo = true;



                }
                else
                {
                    fh.txnname = "Start";
                    fh.returncode = 0;
                    fh.returnmessage = "NotReady";

                }
            }
            catch (Exception)
            {


            }

            return fh;
        }



        #endregion

        #region 停止设备-------1
        [MESAPI("Pause")]
        public static CSACollection Pause(CSACollection qq)
        {
            CSACollection fh = new CSACollection();
            fh.txnname = "Pause";
            fh.returncode = 0;
            fh.returnmessage = "OK";
            try
            {



            }
            catch (Exception)
            {


            }


            return fh;
        }



        #endregion

        #region 设定工单资料

        [MESAPI("NewWo")]
        public static CSACollection NewWo(CSACollection qq)
        {
            CSACollection fh = new CSACollection();
            fh.txnname = "NewWo";
            fh.returncode = 0;
            fh.returnmessage = "OK";


            GV.MesInfo.Wono = qq.Get("wono")?.CSValue1;
            GV.MesInfo.Woqty = qq.Get("woqty")?.CSValue1;




            return fh;
        }

        #endregion

        #region 清料退出Api

        [MESAPI("clearout")]
        public static CSACollection clearout(CSACollection qq)
        {
            CSACollection fh = new CSACollection();
            fh.txnname = "clearout";
            fh.returncode = 0;
            fh.returnmessage = "OK";


            try
            {

            }
            catch (Exception)
            {


            }






            return fh;
        }

        #endregion

        #region 取消暂停

        [MESAPI("cancelpause")]
        public static CSACollection cancelpause(CSACollection qq)
        {
            CSACollection fh = new CSACollection();
            fh.txnname = "cancelpause";
            fh.returncode = 0;
            fh.returnmessage = "OK";

            GV.ButtonAtuo = true;

            try
            {

            }
            catch (Exception)
            {

            }




            return fh;
        }

        #endregion

        #region 工单完成Api

        [MESAPI("OrderFinish")]
        public static CSACollection OrderFinish(CSACollection qq)
        {
            CSACollection fh = new CSACollection();
            fh.txnname = "NewWo";
            fh.returncode = 0;
            fh.returnmessage = "OK";







            return fh;
        }

        #endregion

        #region NG重測指令API

        [MESAPI("TestReset")]
        public static CSACollection TestReset(CSACollection qq)
        {
            CSACollection fh = new CSACollection();
            fh.txnname = "TestReset";
            fh.returncode = 0;
            fh.returnmessage = "OK";











            return fh;
        }

        #endregion

        #region 设备装置保养标准值查询API-----------1111

        [MESAPI("GetMaintanceData")]
        public static CSACollection GetMaintanceData(CSACollection qq)
        {
            CSACollection fh = new CSACollection();
            fh.txnname = "GetMaintanceData";
            fh.returncode = 0;
            fh.returnmessage = "OK";


            DV.PLC5U?.ReadSystemData();
            fh.Add("Test1_pin_std", DV.PLC5U?.TanZhen1Use.ToString());
            fh.Add("Test2_pin_std", DV.PLC5U?.TanZhen2Use.ToString());
            fh.Add("Test3_pin_std", DV.PLC5U?.TanZhen3Use.ToString());
            fh.Add("Test4_pin_std", DV.PLC5U?.TanZhen4Use.ToString());
            fh.Add("Test5_pin_std", DV.PLC5U?.TanZhen5Use.ToString());
            fh.Add("Test6_pin_std", DV.PLC5U?.TanZhen6Use.ToString());
            fh.Add("Suction_std", DV.PLC5U?.XIzuiUse.ToString());
            fh.Add("Suction_spring_std", DV.PLC5U?.XIzuiTanhuangUse.ToString());
            fh.Add("Correct_clipcy1_std", DV.PLC5U?.JZ1QiGangUse.ToString());
            fh.Add("Correct_clipcy2_std", DV.PLC5U?.JZ2QiGangUse.ToString());
            fh.Add("Correct_clip1_std", DV.PLC5U?.JZ1pianUse.ToString());
            fh.Add("Correct_clip2_std", DV.PLC5U?.JZ2pianUse.ToString());
            fh.Add("Polarity_clip_std", DV.PLC5U?.JiXing1PianUse.ToString());
            fh.Add("Sealing1_upCY_std", DV.PLC5U?.QianFengDaoQiGangUse.ToString());
            fh.Add("Sealing2_upCY_std", DV.PLC5U?.HouFengDaoQiGangUse.ToString());
            fh.Add("Sealing1_std", DV.PLC5U?.QianFengDaoQiGangUse.ToString());
            fh.Add("Sealing2_std", DV.PLC5U?.HouFengDaoQiGangUse.ToString());

            //成功读取，委托显示













            return fh;
        }

        #endregion

        #region 标准件测试指令API--------------1

        [MESAPI("standtest")]
        public static CSACollection standtest(CSACollection qq)
        {
            CSACollection fh = new CSACollection();
            if (GV.ButtonAtuo)
            {
                fh.txnname = "standtest";
                fh.returncode = 1;
                fh.returnmessage = "Running";
                GV.standtestTest = false;


            }
            else
            {
                GV.standtestInfo = qq;
                GV.standtestTest = true;
                fh.txnname = "standtest";
                fh.returncode = 0;
                fh.returnmessage = "OK";
            }
            return fh;
        }
        #endregion







        #region 写入cSV模版
        public static void SaveCsv(MesInfo mesInfo)
        {
            List<WorkorderInfo> csvTempleList = new List<WorkorderInfo>();
            //DCR
            WorkorderInfo csvTempleDcr1 = new WorkorderInfo()
            {
                WONO = mesInfo.Wono,
                MATNO = mesInfo.Matno,
                 Equipmentid = mesInfo.Equipmentid,
                CREDATEDATE = DateTime.Now.ToString(),
                USERID = mesInfo.Tpno,
                LOWERLIMIT = DV.DCR1.aX11520DParameter.DCRLow.ToString(),
                UPPERLIMIT = DV.DCR1.aX11520DParameter.DCRHigh.ToString(),
                CENTERVALUE = DV.DCR1.aX11520DParameter.TestScale,

            };
            csvTempleList.Add(csvTempleDcr1);

            //DCR2
            WorkorderInfo csvTempleDcr2 = new WorkorderInfo()
            {
                WONO = mesInfo.Wono,
                MATNO = mesInfo.Matno,
                 Equipmentid = mesInfo.Equipmentid,
                CREDATEDATE = DateTime.Now.ToString(),
                USERID = mesInfo.Tpno,
                LOWERLIMIT = DV.DCR2.aX11520DParameter.DCRLow.ToString(),
                UPPERLIMIT = DV.DCR2.aX11520DParameter.DCRHigh.ToString(),
                CENTERVALUE = DV.DCR2.aX11520DParameter.TestScale,

            };
            csvTempleList.Add(csvTempleDcr2);


            WorkorderInfo csvTempleDcr3 = new WorkorderInfo()
            {
                WONO = mesInfo.Wono,
                MATNO = mesInfo.Matno,
                Equipmentid = mesInfo.Equipmentid,
                CREDATEDATE = DateTime.Now.ToString(),
                USERID = mesInfo.Tpno,
                LOWERLIMIT = DV.DCR3.aX11520DParameter.DCRLow.ToString(),
                UPPERLIMIT = DV.DCR3.aX11520DParameter.DCRHigh.ToString(),
                CENTERVALUE = DV.DCR3.aX11520DParameter.TestScale,

            };
            csvTempleList.Add(csvTempleDcr3);


            WorkorderInfo csvTempleDcr4 = new WorkorderInfo()
            {
                WONO = mesInfo.Wono,
                MATNO = mesInfo.Matno,
                Equipmentid = mesInfo.Equipmentid,
                CREDATEDATE = DateTime.Now.ToString(),
                USERID = mesInfo.Tpno,
                LOWERLIMIT = DV.DCR4.aX11520DParameter.DCRLow.ToString(),
                UPPERLIMIT = DV.DCR4.aX11520DParameter.DCRHigh.ToString(),
                CENTERVALUE = DV.DCR4.aX11520DParameter.TestScale,

            };
            csvTempleList.Add(csvTempleDcr4);





            //IR
            WorkorderInfo csvTempIR = new WorkorderInfo()
            {
                WONO = mesInfo.Wono,
                MATNO = mesInfo.Matno,
                Equipmentid = mesInfo.Equipmentid,
                CREDATEDATE = DateTime.Now.ToString(),
                USERID = mesInfo.Tpno,
                LOWERLIMIT = DV.IR.ir11210Parameter.LowerLimit.ToString(),
                UPPERLIMIT = DV.IR.ir11210Parameter.HighLimit.ToString(),
                CENTERVALUE = DV.IR.ir11210Parameter.Voltage.ToString(),

            };

            //感值1
            WorkorderInfo csvTempLS1 = new WorkorderInfo()
            {
                WONO = mesInfo.Wono,
                MATNO = mesInfo.Matno,
                Equipmentid = mesInfo.Equipmentid,
                CREDATEDATE = DateTime.Now.ToString(),
                USERID = mesInfo.Tpno,
                LOWERLIMIT = DV.GanZhi1.IM3570PrameterList[0].LSLow.ToString(),
                UPPERLIMIT = DV.GanZhi1.IM3570PrameterList[0].LSHIGH.ToString(),
                CENTERVALUE = DV.GanZhi1.IM3570PrameterList[0].LSstandard.ToString(),

            };

            csvTempleList.Add(csvTempLS1);




            //感值2
            WorkorderInfo csvTempLS2 = new WorkorderInfo()
            {
                WONO = mesInfo.Wono,
                MATNO = mesInfo.Matno,
                Equipmentid = mesInfo.Equipmentid,
                CREDATEDATE = DateTime.Now.ToString(),
                USERID = mesInfo.Tpno,
                LOWERLIMIT = DV.GanZhi2.IM3570PrameterList[0].LSLow.ToString(),
                UPPERLIMIT = DV.GanZhi2.IM3570PrameterList[0].LSHIGH.ToString(),
                CENTERVALUE = DV.GanZhi2.IM3570PrameterList[0].LSstandard.ToString(),

            };

            csvTempleList.Add(csvTempLS2);

            //感值对比
            WorkorderInfo csvTempGanZhiCompare = new WorkorderInfo()
            {
                WONO = mesInfo.Wono,
                MATNO = mesInfo.Matno,
                Equipmentid = mesInfo.Equipmentid,
                CREDATEDATE = DateTime.Now.ToString(),
                USERID = mesInfo.Tpno,
                LOWERLIMIT = JsonSaveEXT.deviceParameterJsonGv.LS1_LS2Low.ToString(),
                UPPERLIMIT = JsonSaveEXT.deviceParameterJsonGv.LS1_LS2High.ToString(),
                CENTERVALUE = "",

            };

            csvTempleList.Add(csvTempGanZhiCompare);





            Dictionary<string, WorkorderInfo> keyValuePairs = new Dictionary<string, WorkorderInfo>();
            keyValuePairs.Add(DV.DCR1.CsvList[0], csvTempleDcr1);
            keyValuePairs.Add(DV.DCR1.CsvList[1], csvTempleDcr2);
            keyValuePairs.Add(DV.DCR3.CsvList[0], csvTempleDcr3);
            keyValuePairs.Add(DV.DCR3.CsvList[1], csvTempleDcr4);
            keyValuePairs.Add(DV.IR.CsvList[0], csvTempIR);
            keyValuePairs.Add(DV.GanZhi1.CsvList[0], csvTempLS1);
            keyValuePairs.Add(DV.GanZhi2.CsvList[0], csvTempLS2);
            keyValuePairs.Add(DV.GanZhi2.CsvList[1], csvTempGanZhiCompare);
            CsvHelper.WriteAllTemplateCSV(mesInfo.Wono, keyValuePairs);

        }

        #endregion


        #region 事件发布
        private static void LogPublish(LogInfo logInfo)
        {

            eventAggregator.GetEvent<MessageEvent>().Publish(logInfo);




        }


        #endregion

    }


    #region 特性
    [AttributeUsage(AttributeTargets.Method)]
    public class MESAPIAttribute : Attribute
    {
        public string Command { get; set; }

        public MESAPIAttribute(string command)
        {
            Command = command;
        }
    }
    #endregion
}
