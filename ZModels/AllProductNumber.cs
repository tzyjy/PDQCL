using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZModels
{
    public class AllProductNumber
    {

        public int TatalNumber { get; set; }

        public int GoodNumber { get; set; }

        public int BadNumber { get; set; }




        public decimal PassRate
        {
            get { return CountPassRate(); }
          
        }


        /// <summary>
        /// 计算良品率
        /// </summary>
        /// <param name="instrumentDataParentClass"></param>
        /// <returns></returns>
        public decimal CountPassRate()
        {
            if (this.TatalNumber == 0)
            {
                return 0;
            }
            double good = Convert.ToDouble(this.GoodNumber);
            double tatal = Convert.ToDouble(this.TatalNumber);
            return Math.Round(Convert.ToDecimal((good / tatal) * 100), 4);
        }


    }
}
