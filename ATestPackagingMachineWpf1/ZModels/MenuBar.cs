using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ATestPackagingMachineWpf1.ZModels
{
    public class MenuBar
    {/// <summary>
     /// 菜单图标
     /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// 是否弹窗
        /// </summary>
        public bool IsDiolog { get; set; }
    }
}
