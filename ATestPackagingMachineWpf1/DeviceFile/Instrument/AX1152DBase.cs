using ATestPackagingMachineWpf1.DeviceFile.DeviceParameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATestPackagingMachineWpf1.DeviceFile.Instrument
{
    public class AX1152DBase : DeviceBase
    {
        public AX11520DParameter aX11520DParameter = null;
        public AX1152DBase()
        {

            aX11520DParameter = new AX11520DParameter();
        }


        public override void Conect()
        {
            throw new NotImplementedException();
        }

        public override string ReadTestData()
        {
            throw new NotImplementedException();
        }

        public override void Trigger()
        {
            throw new NotImplementedException();
        }

        public override void WriteDeviceConfig()
        {
            throw new NotImplementedException();
        }
    }
}
