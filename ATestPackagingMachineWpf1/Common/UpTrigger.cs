using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATestPackagingMachineWpf1.Common
{
    /// <summary>
    /// 上升沿触发
    /// </summary>
    public class UpTrigger
    {
        /// <summary>
        /// 这个属性存储上一次的bool状态
        /// </summary>
        public bool Last { get; private set; }


        /// <summary>
        /// 这个属性填被检测的bool量,set;相当于PLC的Input接口
        /// </summary>
        public bool Now
        {
            set
            {
                //我们知道上升沿是从0变1的一瞬间，所以本次扫描为真，上次为假时就产生了上升沿,
                //value&&!Last的意思就是   当前值与上次值不相等时，就为True，也就产生了上升沿
                OutPut = value && !Last;
                //每次给NOW刷新状态后，Last就刷新状态
                Last = value;
            }
        }
        /// <summary>
        /// 这个就是检测的状态，外部获取这个变量就知道上升沿有没有产生
        /// </summary>
        public bool OutPut { get; private set; }
    }
}
