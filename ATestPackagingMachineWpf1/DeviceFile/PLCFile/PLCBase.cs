using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZModels;

namespace ATestPackagingMachineWpf1.DeviceFile.PLCFile
{
    public abstract class PLCBase 
    {

        #region 读取PLC参数
        #region  第一板块


        /// <summary>
        /// 探针1使用次数
        /// </summary>
        public int TanZhen1Use { get; set; }



        /// <summary>
        /// 探针1次数
        /// </summary>
        public int TanZhen1Count { get; set; }



        /// <summary>
        /// 探针2使用次数
        /// </summary>
        public int TanZhen2Use { get; set; }


        /// <summary>
        /// 探针2次数
        /// </summary>
        public int TanZhen2Count { get; set; }

        /// <summary>
        /// 探针3使用次数
        /// </summary>
        public int TanZhen3Use { get; set; }

        /// <summary>
        /// 探针3次数
        /// </summary>
        public int TanZhen3Count { get; set; }

        /// <summary>
        /// 探针4使用次数
        /// </summary>
        public int TanZhen4Use { get; set; }


        /// <summary>
        /// 探针4次数
        /// </summary>
        public int TanZhen4Count { get; set; }



        /// <summary>
        /// 探针5使用次数
        /// </summary>
        public int TanZhen5Use { get; set; }
        /// <summary>
        /// 探针5次数
        /// </summary>
        public int TanZhen5Count { get; set; }

        /// <summary>
        /// 探针6使用次数
        /// </summary>
        public int TanZhen6Use { get; set; }

        /// <summary>
        /// 探针6次数
        /// </summary>
        public int TanZhen6Count { get; set; }
        /// <summary>
        /// 探针7使用次数
        /// </summary>
        public int TanZhen7Use { get; set; }

        /// <summary>
        /// 探针7次数
        /// </summary>
        public int TanZhen7Count { get; set; }

        /// <summary>
        /// 探针8使用次数
        /// </summary>
        public int TanZhen8Use { get; set; }


        /// <summary>
        /// 探针8次数
        /// </summary>
        public int TanZhen8Count { get; set; }
        #endregion

        #region 第二板块
        /// <summary>
        /// 剔除1气缸使用次数
        /// </summary>
        public int Tichu1QigangUse { get; set; }

        /// <summary>
        /// 剔除1气缸次数
        /// </summary>
        public int Tichu1Qigang { get; set; }


        /// <summary>
        /// 剔除2气缸使用次数
        /// </summary>
        public int Tichu2QigangUse { get; set; }

        /// <summary>
        /// 剔除2气缸次数
        /// </summary>
        public int Tichu2Qigang { get; set; }


        /// <summary>
        /// 剔除3气缸使用次数
        /// </summary>
        public int Tichu3QigangUse { get; set; }

        /// <summary>
        /// 剔除3气缸次数
        /// </summary>
        public int Tichu3Qigang { get; set; }

        /// <summary>
        /// 剔除4气缸使用次数
        /// </summary>
        public int Tichu4QigangUse { get; set; }

        /// <summary>
        /// 剔除4气缸次数
        /// </summary>
        public int Tichu4Qigang { get; set; }


        /// <summary>
        /// 剔除5气缸使用次数
        /// </summary>
        public int Tichu5QigangUse { get; set; }

        /// <summary>
        /// 剔除5气缸次数
        /// </summary>
        public int Tichu5Qigang { get; set; }


        /// <summary>
        /// 剔除6气缸使用次数
        /// </summary>
        public int Tichu6QigangUse { get; set; }

        /// <summary>
        /// 剔除6气缸次数
        /// </summary>
        public int Tichu6Qigang { get; set; }

        /// <summary>
        /// 剔除7气缸使用次数
        /// </summary>
        public int Tichu7QigangUse { get; set; }

        /// <summary>
        /// 剔除7气缸次数
        /// </summary>
        public int Tichu7Qigang { get; set; }


        /// <summary>
        /// 剔除8气缸使用次数
        /// </summary>
        public int Tichu8QigangUse { get; set; }

        /// <summary>
        /// 剔除8气缸次数
        /// </summary>
        public int Tichu8Qigang { get; set; }
        #endregion

        #region 第三板块

        /// <summary>
        /// 后封刀气缸使用次数
        /// </summary>
        public int HouFengDaoQiGangUse { get; set; }

        /// <summary>
        /// 后封刀气缸次数
        /// </summary>
        public int HouFengDaoQiGang { get; set; }

        /// <summary>
        /// 后封刀使用次数
        /// </summary>
        public int HouFengDaoUse { get; set; }

        /// <summary>
        /// 后封刀次数
        /// </summary>
        public int HouFengDao { get; set; }

        /// <summary>
        /// 校正1片使用次数
        /// </summary>
        public int JZ1pianUse { get; set; }

        /// <summary>
        /// 校正1片次数
        /// </summary>
        public int JZ1pian { get; set; }

        /// <summary>
        /// 极性1片使用次数
        /// </summary>
        public int JiXing1PianUse { get; set; }

        /// <summary>
        /// 极性1片次数
        /// </summary>
        public int JiXing1Pian { get; set; }


        /// <summary>
        /// 校正2片使用次数
        /// </summary>
        public int JZ2pianUse { get; set; }

        /// <summary>
        /// 校正2片次数
        /// </summary>
        public int JZ2pian { get; set; }

        #endregion

        #region 第四板块

        /// <summary>
        /// 校正3片使用次数
        /// </summary>
        public int JZ3pianUse { get; set; }

        /// <summary>
        /// 校正3片次数
        /// </summary>
        public int JZ3pian { get; set; }
        #endregion

        #region 第五板块

        /// <summary>
        /// 吸嘴使用次数
        /// </summary>
        public int XIzuiUse { get; set; }

        /// <summary>
        /// 吸嘴次数
        /// </summary>
        public int XIzui { get; set; }

        /// <summary>
        /// 吸嘴弹簧使用次数
        /// </summary>
        public int XIzuiTanhuangUse { get; set; }

        /// <summary>
        /// 吸嘴弹簧次数
        /// </summary>
        public int XIzuiTanhuang { get; set; }

        /// <summary>
        /// 校正1气缸使用次数
        /// </summary>
        public int JZ1QiGangUse { get; set; }

        /// <summary>
        /// 校正1气缸次数
        /// </summary>
        public int JZ1QiGang { get; set; }

        /// <summary>
        /// 校正2气缸使用次数
        /// </summary>
        public int JZ2QiGangUse { get; set; }

        /// <summary>
        /// 校正2气缸次数
        /// </summary>
        public int JZ2QiGang { get; set; }

        /// <summary>
        /// 前封刀气缸使用次数
        /// </summary>
        public int QianFengDaoQiGangUse { get; set; }

        /// <summary>
        /// 前封刀气缸次数
        /// </summary>
        public int QianFengDaoQiGang { get; set; }

        #endregion

        #region 第六板块
        /// <summary>
        /// 目标卷数
        /// </summary>
        public int MuBiaoJuan { get; set; }

        /// <summary>
        /// 当前卷数
        /// </summary>
        public int DangqianJuanshu { get; set; }

        /// <summary>
        /// 目标包装数量
        /// </summary>
        public int MubiaoBaozhuangCount { get; set; }

        /// <summary>
        /// 当前包装数量
        /// </summary>
        public int BaozhuangCount { get; set; }

        /// <summary>
        /// 目标前空格数量
        /// </summary>
        public int MubiaoQianKongCount { get; set; }

        /// <summary>
        /// 当前前空格数量
        /// </summary>
        public int QianKongCount { get; set; }


        /// <summary>
        /// 目标后空格数量
        /// </summary>
        public int MubiaoHouKongCount { get; set; }

        /// <summary>
        /// 当前后空格数量
        /// </summary>
        public int HouKongCount { get; set; }



        #endregion

        #region 第7板块

        /// <summary>
        /// 前封刀使用次数
        /// </summary>
        public int QianFengDaoUse { get; set; }

        /// <summary>
        /// 前封刀次数
        /// </summary>
        public int QianFengDao { get; set; }

        #endregion
        #endregion


        /// <summary>
        /// PLC类型
        /// </summary>
        public PLCTpye PLCTpye { get; set; }


        /// <summary>
        /// 是否连接成功
        /// </summary>
        public bool isConnect { get; set; }


        /// <summary>
        /// PLC连接
        /// </summary>
        /// <returns></returns>
        public abstract void Connect();

        /// <summary>
        /// 仪器禁用
        /// </summary>
        /// <returns></returns>
        public abstract bool WriteEnable(List<bool> boolList);

        /// <summary>
        /// 仪器禁用
        /// </summary>
        /// <returns></returns>
        public abstract bool WriteEnableInt(List<int> boolList);



        /// <summary>
        /// 写入Mes参数，前空格，后空格等
        /// </summary>
        /// <returns></returns>
        public abstract bool WriteMesData(MesInfo returnToView);


        /// <summary>
        /// 读取系统状态
        /// </summary>
        /// <returns></returns>
        public abstract void ReadSystemData();

        /// <summary>
        /// 气压
        /// </summary>
        public abstract List<int> ReadPressure();


        /// <summary>
        /// 气压佛点数
        /// </summary>
        public abstract List<float> ReadPressureFloat();


        /// <summary>
        /// Mes报警
        /// </summary>
        public abstract bool WriteMesAlarm();


        /// <summary>
        /// 读取设备状态
        /// </summary>
        public abstract int ReadDeviceStues();
        /// <summary>
        /// 写入地址
        /// </summary>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract bool WriteYTest(string address, bool value);


  
    }


    public enum PLCTpye
    {
        None = 0,
        FX3U,
        FX5U,


    }
}
