using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZModels
{
    /// <summary>
    /// 我给IO板卡信号，给PLCX信号
    /// </summary>
    public class IOWriteX
    {
        /// <summary>
        /// 工控机Ready
        /// </summary>
        public byte Ready { get; set; }


        /// <summary>
        /// 测试Busy
        /// </summary>
        public byte Busy { get; set; }

        /// <summary>
        /// 扫工单
        /// </summary>
        public byte MesWono { get; set; }

        /// <summary>
        /// 感值对比结束
        /// </summary>
        public byte LS1LS2Over { get; set; }




        /// <summary>
        /// 感值对比OK
        /// </summary>
        public byte LS1LS2Value{ get; set; }


   

        /// <summary>
        /// DCR12旋转
        /// </summary>
        public byte DCR12XuanZhuan { get; set; }


 


        /// <summary>
        /// DCR34报警
        /// </summary>
        public byte DCR34Error { get; set; }


     





        /// <summary>
        /// 备用1处理完成
        /// </summary>
        public byte Spare3DealFinished { get; set; }



        /// <summary>
        /// 备用2处理完成
        /// </summary>
        public byte Spare4DealFinished { get; set; }



    }
}
