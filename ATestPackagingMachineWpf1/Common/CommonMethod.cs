using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATestPackagingMachineWpf1.Common
{
    public class CommonMethod
    {
        /// <summary>
        /// 给Combox控件赋值  DI,DO用
        /// </summary>
        /// <returns></returns>
        public static  List<byte> ComboxData()

        {
            List<byte> bytes=  new List<byte>();
            for (byte i = 0; i < 16; i++)
            {
                bytes.Add(i);
            }

            return bytes;
        }

    }
}
