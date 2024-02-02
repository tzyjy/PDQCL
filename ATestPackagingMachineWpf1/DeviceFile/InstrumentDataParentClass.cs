using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZModels
{
    public class InstrumentDataParentClass : BindableBase
    {



        private string _name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }







        /// <summary>
        /// 下限
        /// </summary>

        private decimal _lowValue;
        public decimal LowValue
        {
            get { return _lowValue; }
            set { SetProperty(ref _lowValue, value); }
        }

        /// <summary>
        /// 上限
        /// </summary>


        private decimal _highValue;
        public decimal HighValue
        {
            get { return _highValue; }
            set { SetProperty(ref _highValue, value); }
        }



        /// <summary>
        /// 测试值
        /// </summary>
        private decimal _testValue;
        public decimal TestValue
        {
            get { return _testValue; }
            set { SetProperty(ref _testValue, value); }
        }



        /// <summary>
        /// 判定
        /// </summary>
        private string _judge;
        public string Judge
        {
            get { return _judge; }
            set { SetProperty(ref _judge, value); }
        }





        /// <summary>
        /// 良品数
        /// </summary>
        private int _numberGoodProducts;
        public int NumberGoodProducts
        {
            get { return _numberGoodProducts; }
            set { SetProperty(ref _numberGoodProducts, value); }
        }

        /// <summary>
        /// 不良数
        /// </summary>
        private int _numberBadProducts;
        public int NumberBadProducts
        {
            get { return _numberBadProducts; }
            set { SetProperty(ref _numberBadProducts, value); }
        }

        /// <summary>
        /// 总数
        /// </summary>
        public int TotalNuber { get; set; }



        /// <summary>
        /// 计算良品率
        /// </summary>
        /// <param name="instrumentDataParentClass"></param>
        /// <returns></returns>
        public decimal CountPassRate()
        {
            if (this.TotalNuber == 0)
            {
                return 0;
            }
            double good = Convert.ToDouble(this.NumberGoodProducts);
            double tatal = Convert.ToDouble(this.TotalNuber);
            return Math.Round(Convert.ToDecimal((good / tatal) * 100), 4);
        }
    }
}
