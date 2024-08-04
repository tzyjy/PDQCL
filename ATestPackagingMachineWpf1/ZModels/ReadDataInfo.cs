using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATestPackagingMachineWpf1.ZModels
{
    public class ReadDataInfo : BindableBase
    {
    
        /// <summary>
        /// 传动速度 D2050
        /// </summary>
        public float CDSpeed { get; set; }


        /// <summary>
        /// 喷砂速度 D2051
        /// </summary>
        public float PSSpeed { get; set; }


        /// <summary>
        /// 微蚀1  M214
        /// </summary>


        private bool _WS1;
        public bool WS1
        {
            get { return _WS1; }
            set { SetProperty(ref _WS1, value); }
        }




        /// <summary>
        /// 微蚀2  M204
        /// </summary>
    

        private bool _WS2;
        public bool WS2
        {
            get { return _WS2; }
            set { SetProperty(ref _WS2, value); }
        }


        /// <summary>
        /// 高压水洗  M213
        /// </summary>
  
        private bool _GYSX1;
        public bool GYSX1
        {
            get { return _GYSX1; }
            set { SetProperty(ref _GYSX1, value); }
        }


        /// <summary>
        /// 高压水洗2  M203
        /// </summary>

        private bool _GYSX2;
        public bool GYSX2
        {
            get { return _GYSX2; }
            set { SetProperty(ref _GYSX2, value); }
        }

        /// <summary>
        /// 传动速度D7900,
        /// </summary>
        public float CDSpeedXF { get; set; }


        /// <summary>
        /// 微蚀下发D7902
        /// </summary>
        public int WXUseXF { get; set; }

        /// <summary>
        /// 高压下发D7904
        /// </summary>
        public int GYUseXF { get; set; }






    }
}
