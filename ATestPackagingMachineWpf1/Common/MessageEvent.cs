using ATestPackagingMachineWpf1.ZModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATestPackagingMachineWpf1.Common
{
    public class MessageEvent: PubSubEvent<LogInfo>
    {
    }
}
