using ATestPackagingMachineWpf1.Common;
using ATestPackagingMachineWpf1.Paramater.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZModels;

namespace ATestPackagingMachineWpf1.DeviceFile
{
    public class InstrumentCommon
    {


        #region 计算总数
        public static AllProductNumber GetAllNumber()
        {

            if (!DV.DCR1.Enable && !DV.DCR2.Enable && !DV.IR.Enable && !DV.GanZhi1.Enable && !DV.GanZhi2.Enable)
            {

                return new AllProductNumber();

            }
            AllProductNumber allProductNumber = new AllProductNumber();

            List<int> ints = new List<int>();
            CountDeal(DV.DCR1.Enable, DV.DCR1.InstrumentDataParentClassList[0].TotalNuber, ints);
            CountDeal(DV.IR.Enable, DV.IR.InstrumentDataParentClassList[0].TotalNuber, ints);
            CountDeal(DV.DCR3.Enable, DV.DCR3.InstrumentDataParentClassList[0].TotalNuber, ints);
            CountDeal(DV.GanZhi1.Enable, DV.GanZhi1.InstrumentDataParentClassList[0].TotalNuber, ints);
            CountDeal(DV.GanZhi2.Enable, DV.GanZhi2.InstrumentDataParentClassList[0].TotalNuber, ints);

            allProductNumber.TatalNumber = GV.AllProductNumber.TatalNumber = ints.Max();

            List<int> ints1 = new List<int>();

            CountDeal(DV.DCR1.Enable, DV.DCR1.InstrumentDataParentClassList[0].NumberGoodProducts, ints1);
            CountDeal(DV.IR.Enable, DV.IR.InstrumentDataParentClassList[0].NumberGoodProducts, ints1);
            CountDeal(DV.DCR3.Enable, DV.DCR3.InstrumentDataParentClassList[0].NumberGoodProducts, ints1);
            CountDeal(DV.GanZhi1.Enable, DV.GanZhi1.InstrumentDataParentClassList[0].NumberGoodProducts, ints1);
            CountDeal(DV.GanZhi2.Enable, DV.GanZhi2.InstrumentDataParentClassList[0].NumberGoodProducts, ints1);

            allProductNumber.GoodNumber = GV.AllProductNumber.GoodNumber = ints1.Where(x => x >= 0).ToList().Min();




            return allProductNumber;



        }

        public static void CountDeal(bool isEnable, int count, List<int> intlist)
        {
            if (isEnable)
            {
                intlist.Add(count);
            }
            else
            {
                intlist.Add(-1);

            }

        }

        #endregion











        public static Decimal ChangeDataToD(string strData)
        {
            Decimal dData = 0.0M;
            if (strData.Contains("E"))
            {
                try
                {
                    dData = Convert.ToDecimal(Decimal.Parse(strData.ToString(), System.Globalization.NumberStyles.Float));
                }
                catch (Exception)
                {
                    dData = -99999999999;

                }

            }
            else
            {
                try
                {
                    dData = Convert.ToDecimal(strData);
                }
                catch (Exception)
                {

                    dData = -99999999999;
                }

            }
            return dData;
        }

        public static void ClearData()
        {

            DV.DCR1.ClearAllNumber();
            DV.IR.ClearAllNumber();
            DV.DCR3.ClearAllNumber();
            DV.GanZhi1.ClearAllNumber();
            DV.GanZhi2.ClearAllNumber();


        }

        public static List<decimal> GetAllPassRate()
        {

            List<decimal> decimals = new List<decimal>();
            decimal realData0 = DV.DCR1.InstrumentDataParentClassList[0].CountPassRate();
            decimal realData1 = DV.DCR3.InstrumentDataParentClassList[0].CountPassRate();
            decimal realData2 = DV.IR.InstrumentDataParentClassList[0].CountPassRate();

            decimal realData4 = DV.GanZhi1.InstrumentDataParentClassList[0].CountPassRate();
            decimal realData5 = DV.GanZhi2.InstrumentDataParentClassList[0].CountPassRate();


            decimals.Add(realData0);
            decimals.Add(realData1);
            decimals.Add(realData2);

            decimals.Add(realData4);
            decimals.Add(realData5);


            return decimals;


        }

        public static List<string> GetAllPassRateString()
        {
            List<string> strings = new List<string>
            {
                DV.DCR1.InstrumentDataParentClassList[0].CountPassRate().ToString("F2"),
                  DV.IR.InstrumentDataParentClassList[0].CountPassRate().ToString("F2"),
                DV.DCR3.InstrumentDataParentClassList[0].CountPassRate().ToString("F2"),





                DV.GanZhi1.InstrumentDataParentClassList[0].CountPassRate().ToString("F2"),

                DV.GanZhi2.InstrumentDataParentClassList[0].CountPassRate().ToString("F2"),


            };
            return strings;

        }









        public static List<GanzhiParameter> GetComboxParameter()
        {
            List<GanzhiParameter> ganzhiParameters = new List<GanzhiParameter>();
            ganzhiParameters.Add(new GanzhiParameter() { Key = "Z", Name = "Z" });
            ganzhiParameters.Add(new GanzhiParameter() { Key = "LS", Name = "LS" });
            ganzhiParameters.Add(new GanzhiParameter() { Key = "θ", Name = "PHASE" });
            ganzhiParameters.Add(new GanzhiParameter() { Key = "RS", Name = "RS" });
            ganzhiParameters.Add(new GanzhiParameter() { Key = "OFF", Name = "OFF" });
            return ganzhiParameters;


        }

        public static DataTable GetComboxRange()
        {
            List<string> nameList = new List<string>()
            {
                "100mΩ",
                "1Ω",
                "10Ω",
               "300Ω",
                 "1KΩ",
                  "3KΩ",
                   "10KΩ",
                    "30KΩ",
                     "100KΩ",
                      "1MΩ",
                       "10MΩ",
                         "100MΩ",
            };
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Value");
            for (int i = 1; i < nameList.Count + 1; i++)
            {
                DataRow dataRow = dt.NewRow();
                dataRow["Name"] = nameList[i - 1];
                dataRow["Value"] = i.ToString();
                dt.Rows.Add(dataRow);
            }
            return dt;


        }




        #region DCRMes第一次转换，因为Mes发过来的是欧
        public static List<decimal> DcrDataConvert(decimal dcrmin, decimal dcrmax, string range)
        {
            decimal dcrlow = 0m;
            decimal dcrhigh = 0m;
            switch (range)
            {
                case "R0":
                    dcrlow = dcrmin * 1000m;
                    dcrhigh = dcrmax * 1000m;
                    break;
                case "R1":
                    dcrlow = dcrmin * 1000m;
                    dcrhigh = dcrmax * 1000m;
                    break;
                case "R2":
                    dcrlow = dcrmin * 1000m;
                    dcrhigh = dcrmax * 1000m;
                    break;

                default:
                    dcrlow = dcrmin * 1m;
                    dcrhigh = dcrmax * 1m;
                    break;
            }
            List<decimal> decimals = new List<decimal>();
            decimals.Add(dcrlow);
            decimals.Add(dcrhigh);
            return decimals;
        }
        #endregion

        #region  第二次转换，给电表
        public static List<int> LowHighDeal(string testRange, decimal dcrlow, decimal dcrhigh)
        {
            List<int> intList = new List<int>();
            int low = 0;
            int high = 0;

            switch (testRange)
            {
                case "R0":
                    if (dcrhigh > 1.5m || dcrlow > 1.5m)
                    {
                        throw new Exception("R0档范围在0-1.5毫欧！");

                    }
                    low = (int)(dcrlow * 10000);
                    high = (int)(dcrhigh * 10000);
                    break;



                case "R1":
                    if (dcrhigh > 15m || dcrlow > 15m)
                    {
                        throw new Exception("R1档范围在0-15毫欧！");

                    }
                    low = (int)(dcrlow * 1000);
                    high = (int)(dcrhigh * 1000);
                    break;
                case "R2":
                    if (dcrhigh > 150m || dcrlow > 150m)
                    {
                        throw new Exception("R2档范围在0-150毫欧！");


                    }
                    low = (int)(dcrlow * 100);
                    high = (int)(dcrhigh * 100);
                    break;
                case "R3":
                    if (dcrhigh > 1500m|| dcrlow > 1500m)
                    {
                        throw new Exception("R3档范围在0-15000欧！");

                    }
                    low = (int)(dcrlow * 10);
                    high = (int)(dcrhigh * 10);
                    break;
                case "R4":
                    if (dcrhigh > 15m || dcrlow > 15m)
                    {
                        throw new Exception("R4档范围在0-15欧！");

                    }
                    low = (int)(dcrlow * 1000);
                    high = (int)(dcrhigh * 1000);
                    break;

                case "R5":
                    if (dcrhigh > 150m || dcrlow > 150m)
                    {
                        throw new Exception("R5档范围在0-150欧！");

                    }
                    low = (int)(dcrlow * 100);
                    high = (int)(dcrhigh * 100);
                    break;
                case "R6":
                    if (dcrhigh > 1500m || dcrlow > 1500m)
                    {
                        throw new Exception("R6档范围在0-1500欧！");

                    }
                    low = (int)(dcrlow * 10);
                    high = (int)(dcrhigh * 10);
                    break;

                default:
                    break;
            }

            intList.Add(low);
            intList.Add(high);

            return intList;
        }
        #endregion

        #region 数据显示
        public static List<decimal> LowHighShow(string testScale, decimal dcrlow, decimal dcrhigh)
        {
            List<decimal> decimals = new List<decimal>();

            decimal low = 0;
            decimal high = 0;
            switch (testScale)
            {
                case "R0":
                    low = dcrlow / 10000;
                    high = dcrhigh / 10000;

                    break;

                case "R1":
                    low = dcrlow / 1000;
                    high = dcrhigh / 1000;

                    break;
                case "R2":
                    low = dcrlow / 100;
                    high = dcrhigh / 100;

                    break;
                case "R3":
                    low = dcrlow / 10000;
                    high = dcrhigh / 10000;

                    break;
                case "R4":
                    low = dcrlow / 1000;
                    high = dcrhigh / 1000;

                    break;
                case "R5":
                    low = dcrlow / 100;
                    high = dcrhigh / 100;

                    break;
                case "R6":
                    low = dcrlow / 10;
                    high = dcrhigh / 10;
                    break;
                default:
                    break;
            }
            decimals.Add(low);
            decimals.Add(high);

            return decimals;

        }




        #endregion




    }



    public class GanzhiParameter
    {
        public string Key { get; set; }
        public string Name { get; set; }

    }

    public class GanzhiRange
    {
        public string Key { get; set; }
        public string Name { get; set; }

    }


    //public class GanzhiParameterList : ObservableCollection<GanzhiParameter>
    //{

    //    public GanzhiParameterList()
    //    {
    //        Add(new GanzhiParameter() { Key = "Z", Name = "Z" });
    //        Add(new GanzhiParameter() { Key = "LS", Name = "LS" });
    //        Add(new GanzhiParameter() { Key = "θ", Name = "PHASE" });
    //        Add(new GanzhiParameter() { Key = "RS", Name = "RS" });
    //        Add(new GanzhiParameter() { Key = "OFF", Name = "OFF" });
    //    }
    //}
}
