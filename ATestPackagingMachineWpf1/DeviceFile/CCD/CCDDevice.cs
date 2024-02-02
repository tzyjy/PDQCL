using BTest.TCPIP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATestPackagingMachineWpf1.DeviceFile.CCD
{
    public class CCDDevice
    {


        TouchSocketHelper touchSocketHelper = new TouchSocketHelper();
        public string IP { get; set; }

        public string Port { get; set; }

        public CCDDevice(string iP, string port)
        {
            IP = iP;
            Port = port;
        }

        public void Connect()
        {

         touchSocketHelper.Connect(IP, Port);

        }


        public  string Send(string Text)
        {
         return touchSocketHelper.Send(Text);



        }
    }
}
