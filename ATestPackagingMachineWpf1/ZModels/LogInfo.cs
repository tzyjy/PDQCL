using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATestPackagingMachineWpf1.ZModels
{
    public class LogInfo
    {
        public bool OK { get; set; }

        public string InfoText { get; set; }
    }

    public class MessageEvent : PubSubEvent<LogInfo>
    {
    }
}