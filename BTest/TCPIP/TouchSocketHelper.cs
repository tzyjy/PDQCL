using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TouchSocket.Core;
using TouchSocket.Sockets;

namespace BTest.TCPIP
{
    public class TouchSocketHelper
    {
        public TouchSocket.Sockets.TcpClient tcpClient = null;

        /// <summary>
        /// 短连接
        /// </summary>
        /// <param name="ServiceIP"></param>
        /// <param name="ServicePort"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public string Send(string ServiceIP, string ServicePort, string text)
        {

            TouchSocket.Sockets.TcpClient tcpClient = new TouchSocket.Sockets.TcpClient();
            tcpClient.Connected += (client, e) => { };//成功连接到服务器
            tcpClient.Disconnected += (client, e) => { };//从服务器断开连接，当连接不成功时不会触发。

            //声明配置
            TouchSocketConfig config = new TouchSocketConfig();
            config.SetRemoteIPHost(new IPHost($"{ServiceIP}:{ServicePort}"));
            //配置适配器


            //载入配置
            tcpClient.Setup(config);

            tcpClient.Connect(1000);

            var waitClient = tcpClient.GetWaitingClient(new WaitingOptions()
            {

                BreakTrigger = true,//表示当连接断开时，会立即触发
                ThrowBreakException = true//表示当连接断开时，是否触发异常
            });

            byte[] send = Encoding.ASCII.GetBytes(text);
            try
            {
                Console.WriteLine("发送：" + text);
                var data = waitClient.SendThenResponse(send);
                Thread.Sleep(100);
                return Encoding.ASCII.GetString(data.Data);//同步收到的RequestInfo
            }
            catch (Exception ex)
            {
                Console.WriteLine("false");
                Console.WriteLine(ex.Message);
                return "false";

            }
            finally
            {

                tcpClient.Close();
            }








        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="ServiceIP"></param>
        /// <param name="ServicePort"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool Connect(string ServiceIP, string ServicePort)
        {
            bool isConnect = false;
            try
            {
               
                tcpClient = new TouchSocket.Sockets.TcpClient();
                tcpClient.Connected += (client, e) => { Console.WriteLine( "CCD连接成功！！"); };//成功连接到服务器
                tcpClient.Disconnected += (client, e) => { Console.WriteLine("CCD断开成功！！"); };//从服务器断开连接，当连接不成功时不会触发。

                //声明配置
                TouchSocketConfig config = new TouchSocketConfig();
                config.SetRemoteIPHost(new IPHost($"{ServiceIP}:{ServicePort}"));
                //配置适配器

                //载入配置
                tcpClient.Setup(config);
                tcpClient.Connect(1000);
                isConnect = true;
            }
            catch (Exception ex)
            {
                isConnect = false;
                throw new Exception(ex.Message);
            }

            return isConnect;

        }
        /// <summary>
        /// 断开连接
        /// </summary>
        public void Disconnect()
        {
            tcpClient.Close();

        }

        /// <summary>
        /// 发送并读取数据
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string Send(string text)
        {
            var waitClient = tcpClient.GetWaitingClient(new WaitingOptions()
            {
                BreakTrigger = true,//表示当连接断开时，会立即触发
                ThrowBreakException = true//表示当连接断开时，是否触发异常
            });
            byte[] send = Encoding.ASCII.GetBytes(text);
            try
            {
                var data = waitClient.SendThenResponse(send);
                return Encoding.ASCII.GetString(data.Data); //同步收到的RequestInfo
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// 只发送数据
        /// </summary>
        /// <param name="text"></param>
        /// <exception cref="Exception"></exception>
        public void SendOnly(string text)
        {
            byte[] send = Encoding.ASCII.GetBytes(text);
            try
            {
                tcpClient.Send(send);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }










    }
}
