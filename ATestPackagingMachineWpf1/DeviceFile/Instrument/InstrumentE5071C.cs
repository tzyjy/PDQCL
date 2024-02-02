using NationalInstruments.NI4882;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATestPackagingMachineWpf1.DeviceFile.Instrument
{
    public class InstrumentE5071C : DeviceBase
    {

        private NationalInstruments.NI4882.Device device = null;
        private string path=string.Empty;
        public InstrumentE5071C()
        {
            
        }
        public override void Conect()
        {
            try
            {
                device = new Device(this.BoardNumber, this.PrimaryAddress);
              
            }
            catch (Exception)
            {

          
            }
        }

        public override string ReadTestData()
        {
           return device.ReadString();
        }

        public override void Trigger()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(":INIT1:CONT ON\n");
            sb.Append(":TRIG:SOUR BUS\n");
            sb.Append("TRIG:SING\n");
            //sb.Append("*OPC?\n");
            device.Write(sb.ToString());
        }

        public override void WriteDeviceConfig()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string commandtext = ":MMEMory:LOAD ";
                sb.Append(commandtext);
                sb.Append(path);
                sb.Append("\n");
                device.Write(sb.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
