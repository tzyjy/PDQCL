using BTest.LogHelper;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ZModels;

namespace ATestPackagingMachineWpf1.Common
{
    public class CommonMethod
    {
        public static void ShowBox(string text, MessageBoxImage messageBoxImage)
        {

           // LOG.WriteLog(text);
            MessageBox.Show(text, "提示", MessageBoxButton.OK, messageBoxImage);


        }

        public static string InnerExDeal(Exception ex)
        {
            string result = string.Empty;

            while (ex.InnerException != null)
            {
                result += ex.InnerException.Message;
                InnerExDeal(ex.InnerException);
                Thread.Sleep(1);
            }
            return result;

        }
        #region 封装权限方法
        public static bool Getlimit(Limit limitName, LogonPeson logonPeson)

        {
            bool result = false;
            switch (limitName)
            {
                case Limit.设备参数:
                    result = logonPeson.FunctionPermission100 == true ? true : false;
                    break;
                case Limit.工艺参数:
                    result = logonPeson.FunctionPermission101 == true ? true : false;
                    break;
                case Limit.用户管理:
                    result = logonPeson.FunctionPermission106 == true ? true : false;
                    break;
                default:
                    break;
            }

            return result;
        }


        #endregion








        #region MyRegion

        /// <summary>
        /// 等于1，有权限，等于2无权限，等于3根本没有登录成功
        /// </summary>
        /// <param name="dialogService"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static int ShowMinDialog(IDialogService dialogService, Limit limit)
        {
            int result = 0;
            dialogService.ShowDialog("MinLoginView", callback =>
            {
                if (callback.Result == ButtonResult.OK)
                {
                    result = Getlimit(limit, GV.CurrentLogonPeson) == true ? 1 : 2;
                }
                else
                {
                    result = 3;

                }
            });
            return result;
        }
        #endregion
    }

    public enum Limit
    {
        设备参数,
        工艺参数,
        用户管理
    }
}
