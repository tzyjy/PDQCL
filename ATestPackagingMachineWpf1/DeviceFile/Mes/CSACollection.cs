using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATestPackagingMachineWpf1.DeviceFile.Mes
{


    public static class Ex
    {
        public static byte[] GetBytes_UTF8(this string txt)
        {
            return Encoding.UTF8.GetBytes(txt);
        }
        public static byte[] GetBytes_ASCII(this string txt)
        {
            return Encoding.ASCII.GetBytes(txt);
        }
        public static byte[] GetBytes_Default(this string txt)
        {
            return Encoding.Default.GetBytes(txt);
        }
        public static string GetString_Default(this byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }
        public static string GetString_UTF8(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
        public static string GetString_ASCII(this byte[] bytes)
        {
            return Encoding.ASCII.GetString(bytes);
        }
        public static string GetString_Unicode(this byte[] bytes)
        {
            return Encoding.Unicode.GetString(bytes);
        }



        public static int ToInt32(this object obj)
        {
            return Convert.ToInt32(obj);
        }


        public static decimal ToDecimal(this object obj)
        {
            return Convert.ToDecimal(obj);
        }



    }
    public class CSACollection
    {

        public string this[string key]
        {
            get
            {
                var dd = Get(key);
                if (dd != null)
                {
                    return dd.CSValue1;
                }
                return string.Empty;
                /* getter代码 */
            }
            set
            {

                var dd = Get(key);
                if (dd != null)
                {
                    dd.CSValue1 = value;
                }
                Add(new CSA(key, value));
                /* setter代码 */
            }
        }



        public List<CSA> ListCSA { get; private set; }
        public CSACollection(List<CSA> cSAs)
        {
            ListCSA = cSAs;
        }
        public CSACollection()
        {
            ListCSA = new List<CSA>();
        }

        public string txnname
        {
            get
            {
                return Get("txnname")?.CSValue1;
            }
            set
            {
                var cs = Get("txnname");
                if (cs != null)
                    cs.CSValue1 = value;
                else
                    Add("txnname", value);
            }
        }
        public int returncode
        {
            get
            {
                return Get("returncode")?.CSValue1.ToInt32() ?? 0;
            }
            set
            {
                var cs = Get("returncode");
                if (cs != null)
                    cs.CSValue1 = value.ToString();
                else
                    Add("returncode", value.ToString());
            }
        }
        public string returnmessage
        {
            get
            {
                return Get("returnmessage")?.CSValue1;
            }
            set
            {
                var cs = Get("returnmessage");
                if (cs != null)
                    cs.CSValue1 = value;
                else
                    Add("returnmessage", value);
            }
        }

        public CSA AmmeterParm1
        {
            get
            {
                return Get("AmmeterParm1");
            }
        }
        public CSA AmmeterParm2
        {
            get
            {
                return Get("AmmeterParm2");
            }
        }
        public CSA AmmeterParm3
        {
            get
            {
                return Get("AmmeterParm3");
            }
        }
        public CSA AmmeterParm4
        {
            get
            {
                return Get("AmmeterParm4");
            }
        }

        public CSA AmmeterParm5
        {
            get
            {
                return Get("AmmeterParm5");
            }
        }


        public CSA AmmeterParm6
        {
            get
            {
                return Get("AmmeterParm6");
            }
        }


        public CSA AmmeterParm7
        {
            get
            {
                return Get("AmmeterParm7");
            }
        }
        public CSA AmmeterParm8
        {
            get
            {
                return Get("AmmeterParm8");
            }
        }

        public CSA Get(string name)
        {
            return this.ListCSA.Where(i => i.CSName == name).FirstOrDefault();
        }
        public void Add(CSA csa)
        {
            var cs = Get(csa.CSName);
            if (cs != null)
            {
                throw new Exception("此Key已存在");
            }
            ListCSA.Add(csa);
        }

        public CSA Add(string name, string vaule)
        {
            var zcs = new CSA(name, vaule);
            ListCSA.Add(zcs);
            return zcs;
        }

        public string GetCSText()
        {
            StringBuilder sb = new StringBuilder();
            if (ListCSA.Count > 0)
            {
                foreach (var cs in ListCSA)
                {
                    if (cs.CSValue1 != null)
                    {
                        sb.Append($"{cs.CSName} @= {cs.CSValue1} @< ");
                    }
                    else if (cs.CSValue2.Count > 0)
                    {
                        sb.Append($"{cs.CSName} @= ");
                        foreach (var item in cs.CSValue2)
                        {
                            sb.Append($"{item.CSName} ={item.CSValue1} ,@ ");
                        }
                        sb.Remove(sb.Length - 3, 3);
                        sb.Append($"@< ");
                    }
                }
                if (sb.Length >= 4)
                    sb.Remove(sb.Length - 4, 4);
            }
            return sb.ToString();
        }

        public byte[] GetCSBytes()
        {
            var cstext = this.GetCSText();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < cstext.Length; i++)
            {
                sb.Append(cstext[i]);
                sb.Append('\0');
            }
            cstext = sb.ToString();
            return cstext.GetBytes_ASCII();
        }

        public static CSACollection GetCS(string txt)
        {
            txt = txt.Replace("\0", "");
            List<CSA> list = new List<CSA>();
            var txts = txt.Split(new string[] { "@<" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in txts)
            {
                var txts1 = item.Split(new string[] { "@=" }, StringSplitOptions.RemoveEmptyEntries);
                if (txts1.Length == 2)
                {
                    CSA cSA = new CSA();
                    cSA.CSName = txts1[0].Trim();
                    if (txts1[1].Contains(",@"))
                    {
                        var txts2 = txts1[1].Split(new string[] { ",@" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string item1 in txts2)
                        {
                            var txts4 = item1.Split('=');
                            if (txts4.Length == 2)
                            {
                                CSA cSA1 = new CSA();
                                cSA1.CSName = txts4[0].Trim();
                                cSA1.CSValue1 = txts4[1].Trim();
                                cSA.CSValue2.Add(cSA1);
                            }
                        }
                    }
                    else if (txts1[1].Contains("="))
                    {
                        var txts3 = txts1[1].Split('=');
                        if (txts3.Length == 2)
                        {
                            CSA cSA2 = new CSA();
                            cSA2.CSName = txts3[0].Trim();
                            cSA2.CSValue1 = txts3[1].Trim();
                            cSA.CSValue2.Add(cSA2);
                        }
                    }
                    else
                    {
                        cSA.CSValue1 = txts1[1].Trim();
                    }
                    list.Add(cSA);
                }
            }
            return new CSACollection(list);
        }


    }

    public class CSA
    {
        public string CSName;
        public string CSValue1;

        public List<CSA> CSValue2 = new List<CSA>();
        public CSA()
        {
        }
        public CSA(string cSName, string cSValue1)
        {
            CSName = cSName;
            CSValue1 = cSValue1;
        }

        public void Add(string cSName, string cSValue1)
        {
            var cs = GetFromsecod(cSName);
            if (cs != null)
            {
                throw new Exception("此Key已存在");
            }
            CSValue2.Add(new CSA(cSName, cSValue1));
        }

        public string this[string key]
        {
            get
            {
                var dd = GetFromsecod(key);
                if (dd != null)
                {
                    return dd.CSValue1;
                }
                return string.Empty;
                /* getter代码 */
            }
            set
            {

                var dd = GetFromsecod(key);
                if (dd != null)
                {
                    dd.CSValue1 = value;
                }
                Add(key, value);
                /* setter代码 */
            }
        }
        public CSA GetFromsecod(string name)
        {
            return this.CSValue2.Where(i => i.CSName == name).FirstOrDefault();
        }

        #region DCR参数
        public decimal DCR_MAX
        {
            get
            {
                var csa2 = GetFromsecod("DCR_MAX");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1.ToDecimal();
                }
                else
                {
                    return 0;
                }


            }
        }
        public decimal DCR_MIN
        {
            get
            {
                var csa2 = GetFromsecod("DCR_MIN");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1.ToDecimal();
                }
                else
                {
                    return 0;
                }


            }
        }
        public string DCR_RANGE
        {
            get
            {
                var csa2 = GetFromsecod("DCR_RANGE");
                return csa2?.CSValue1 ?? string.Empty;
            }
        }
        #endregion

        


        #region 低感1

        /// <summary>
        /// Ls频率
        /// </summary>
        public string L_FREQUENCY
        {
            get
            {
                var csa2 = GetFromsecod("L_FREQUENCY");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1;
                }
                else
                {
                    return string.Empty;
                }


            }
        }

        /// <summary>
        /// Ls电压
        /// </summary>
        public string L_VOLTAGE
        {
            get
            {
                var csa2 = GetFromsecod("L_VOLTAGE");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1;
                }
                else
                {
                    return string.Empty;
                }


            }
        }

        /// <summary>
        /// Ls最大值
        /// </summary>
        public decimal LCR_MAX
        {
            get
            {
                var csa2 = GetFromsecod("LCR_MAX");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1.ToDecimal();
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Ls最小值
        /// </summary>
        public decimal LCR_MIN
        {
            get
            {
                var csa2 = GetFromsecod("LCR_MIN");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1.ToDecimal();
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Ls标准值
        /// </summary>
        public decimal LCR_NORMAL
        {
            get
            {
                var csa2 = GetFromsecod("LCR_NORMAL");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1.ToDecimal();
                }
                else
                {
                    return 0;
                }
            }
        }

        #endregion


        #region 低感2

        /// <summary>
        /// Ls频率
        /// </summary>
        public string LUH1_FREQUENCY
        {
            get
            {
                var csa2 = GetFromsecod("LUH1_FREQUENCY");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1;
                }
                else
                {
                    return string.Empty;
                }


            }
        }

        /// <summary>
        /// Ls电压
        /// </summary>
        public string LUH1_VOLTAGE
        {
            get
            {
                var csa2 = GetFromsecod("LUH1_VOLTAGE");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1;
                }
                else
                {
                    return string.Empty;
                }


            }
        }

        /// <summary>
        /// Ls最小值
        /// </summary>
        public decimal LCRUH1_MIN
        {
            get
            {
                var csa2 = GetFromsecod("LCRUH1_MIN");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1.ToDecimal();
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Ls最大值
        /// </summary>
        public decimal LCRUH1_MAX
        {
            get
            {
                var csa2 = GetFromsecod("LCRUH1_MAX");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1.ToDecimal();
                }
                else
                {
                    return 0;
                }
            }
        }



        /// <summary>
        /// Ls标准值
        /// </summary>
        public decimal LCRUH1_NORMAL
        {
            get
            {
                var csa2 = GetFromsecod("LCRUH1_NORMAL");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1.ToDecimal();
                }
                else
                {
                    return 0;
                }
            }
        }


        #endregion

        #region 感值Rs值
        /// <summary>
        /// RS频率
        /// </summary>
        public string ESR_FREQUENCY
        {
            get
            {
                var csa2 = GetFromsecod("ESR_FREQUENCY");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1;
                }
                else
                {
                    return string.Empty;
                }


            }
        }

        /// <summary>
        /// RS电压
        /// </summary>
        public string ESR_VOLTAGE
        {
            get
            {
                var csa2 = GetFromsecod("ESR_VOLTAGE");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1;
                }
                else
                {
                    return string.Empty;
                }


            }
        }

        /// <summary>
        /// Ls最大值
        /// </summary>
        public decimal ESR_MAX
        {
            get
            {
                var csa2 = GetFromsecod("ESR_MAX");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1.ToDecimal();
                }
                else
                {
                    return 0;
                }
            }
        }






        #endregion



        #region 高感

        public decimal LCRUH3_MAX
        {
            get
            {
                var csa2 = GetFromsecod("LCRUH3_MAX");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1.ToDecimal();
                }
                else
                {
                    return 0;
                }


            }
        }

        public decimal LCRUH3_MIN
        {
            get
            {
                var csa2 = GetFromsecod("LCRUH3_MIN");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1.ToDecimal();
                }
                else
                {
                    return 0;
                }


            }
        }


        public decimal LCRUH3_NORMAL
        {
            get
            {
                var csa2 = GetFromsecod("LCRUH3_NORMAL");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1.ToDecimal();
                }
                else
                {
                    return 0;
                }


            }
        }


        public string LUH3_FREQUENCY
        {
            get
            {
                var csa2 = GetFromsecod("LUH3_FREQUENCY");

                //return csa2?.CSValue1 ?? string.Empty;

                return csa2?.CSValue1 ?? string.Empty;


            }
        }


        public decimal ESRUH_MAX
        {
            get
            {
                var csa2 = GetFromsecod("ESRUH_MAX");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1.ToDecimal();
                }
                else
                {
                    return 0;
                }


            }
        }


        public decimal ESRUH_MIN
        {
            get
            {
                var csa2 = GetFromsecod("ESRUH_MIN");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1.ToDecimal();
                }
                else
                {
                    return 0;
                }


            }
        }

        #endregion



        #region 感值

        /// <summary>
        /// Ls频率
        /// </summary>
        public string LUH2_FREQUENCY
        {
            get
            {
                var csa2 = GetFromsecod("LUH2_FREQUENCY");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1;
                }
                else
                {
                    return string.Empty;
                }


            }
        }

        /// <summary>
        /// Ls电压
        /// </summary>
        public string LUH2_VOLTAGE
        {
            get
            {
                var csa2 = GetFromsecod("LUH2_VOLTAGE");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1;
                }
                else
                {
                    return string.Empty;
                }


            }
        }

        /// <summary>
        /// Ls最大值
        /// </summary>
        public decimal LCRUH2_MAX
        {
            get
            {
                var csa2 = GetFromsecod("LCRUH2_MAX");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1.ToDecimal();
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Ls最小值
        /// </summary>
        public decimal LCRUH2_MIN
        {
            get
            {
                var csa2 = GetFromsecod("LCRUH2_MIN");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1.ToDecimal();
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Ls标准值
        /// </summary>
        public decimal LCRUH2_NORMAL
        {
            get
            {
                var csa2 = GetFromsecod("LCRUH2_NORMAL");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1.ToDecimal();
                }
                else
                {
                    return 0;
                }
            }
        }

        #endregion





        #region 极性
        /// <summary>
        /// 极性频率
        /// </summary>
        public string THETA_FREQUENCY
        {
            get
            {
                var csa2 = GetFromsecod("THETA_FREQUENCY");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1;
                }
                else
                {
                    return string.Empty;
                }


            }
        }

        /// <summary>
        /// 极性电压
        /// </summary>
        public string THETA_VOLTAGE
        {
            get
            {
                var csa2 = GetFromsecod("THETA_VOLTAGE");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1;
                }
                else
                {
                    return string.Empty;
                }


            }
        }
        /// <summary>
        /// 极性最小值
        /// </summary>
        public decimal THETA_MIN
        {
            get
            {
                var csa2 = GetFromsecod("THETA_MIN");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1.ToDecimal();
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 极性最大值
        /// </summary>
        public decimal THETA_MAX
        {
            get
            {
                var csa2 = GetFromsecod("THETA_MAX");

                //return csa2?.CSValue1 ?? string.Empty;

                if (csa2 != null)
                {
                    return csa2.CSValue1.ToDecimal();
                }
                else
                {
                    return 0;
                }
            }
        }





        #endregion


        //LCRUH3_MAX  LCRUH3_NORMAL  LCRUH3_MIN   LUH3_FREQUENCY   ESRUH_MAX  ESRUH_MIN





        #region IR
        /// <summary>
        /// IR电压
        /// </summary>

        public string TP_IR_VOLT
        {
            get
            {
                var csa2 = GetFromsecod("TP_IR_VOLT");
                if (csa2 != null)
                {
                    return csa2.CSValue1;
                }
                else
                {
                    return string.Empty;
                }


            }
        }



        /// <summary>
        /// 测试时间
        /// </summary>
        public decimal TP_IR_TESTTIME
        {
            get
            {
                var csa2 = GetFromsecod("TP_IR_TESTTIME");


                if (csa2 != null)
                {
                    return csa2.CSValue1.ToDecimal();
                }
                else
                {
                    return 0;
                }
            }
        }



        /// <summary>
        /// 上限
        /// </summary>

        public string TP_IR_MAX
        {
            get
            {
                var csa2 = GetFromsecod("TP_IR_MAX");
                if (csa2 != null)
                {


                    return csa2.CSValue1;
                }
                else
                {
                    return string.Empty;
                }


            }
        }

        /// <summary>
        /// 下限
        /// </summary>

        public string TP_IR_MIN
        {
            get
            {
                var csa2 = GetFromsecod("TP_IR_MIN");
                if (csa2 != null)
                {
                    return csa2.CSValue1;
                }
                else
                {
                    return string.Empty;
                }


            }
        }

        #endregion



















































































    }
}
