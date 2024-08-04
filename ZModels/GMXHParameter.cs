using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZModels
{
    public class GMXHParameter
    {
        /// <summary>
        /// 干膜型号
        /// </summary>
        public string GMMode { get; set; }


        /// <summary>
        /// 上限
        /// </summary>
        public decimal UpLimit { get; set; }

        /// <summary>
        /// 下限
        /// </summary>
        public decimal LowLimit { get; set; }


        /// <summary>
        /// 干膜参数
        /// </summary>
        private decimal _GMValue;

        public decimal GMValue
        {
            get { return _GMValue; }
            set
            {
                if (value >= UpLimit)
                {
                    _GMValue = UpLimit;
                }
                else if (value <= LowLimit)
                {
                    _GMValue = LowLimit;

                }
                else
                {
                    _GMValue = value;
                }

            }
        }



    }
}
