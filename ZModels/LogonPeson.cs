using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZModels
{
    [Table("LogonPeson")]   // 标识数据库创建的表名
    public class LogonPeson
    {
        /// <summary>
        /// 用户ID，主键
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]       // 自增列
        public int ID { get; set; } = 1000;

        /// <summary>
        /// 用户登录账号
        /// </summary>
        public string LoginAccount { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPwd { get; set; }

        /// <summary>
        /// 仪器禁用
        /// </summary>
        public bool FunctionPermission100 { get; set; }

        /// <summary>
        /// 仪器参数设置
        /// </summary>
        public bool FunctionPermission101 { get; set; }

        /// <summary>
        /// 产品清零
        /// </summary>
        public bool FunctionPermission102 { get; set; }

        /// <summary>
        /// 警戒值设置
        /// </summary>
        public bool FunctionPermission103 { get; set; }


        /// <summary>
        /// 温度设置
        /// </summary>
        public bool FunctionPermission104 { get; set; }

        /// <summary>
        /// 设备参数配置
        /// </summary>
        public bool FunctionPermission105 { get; set; }

        /// <summary>
        /// 用户管理
        /// </summary>
        public bool FunctionPermission106 { get; set; }

        /// <summary>
        /// 从厂家权限
        /// </summary>
        public bool FunctionPermission777 { get; set; } = false;
    }
}
