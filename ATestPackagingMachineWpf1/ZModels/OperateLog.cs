using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATestPackagingMachineWpf1.ZModels
{
    public class OperateLog
    {
        public string LogIcon { get; set; }
        public string IconColor { get; set; }
        public string OperateTime { get; set; }
        public string OperateInfo { get; set; }
        public LogTpye LogTpye { get; set; }
    }

    public enum LogTpye
    {
        None = 0,
        LoadLog,
        Other

    }
}
