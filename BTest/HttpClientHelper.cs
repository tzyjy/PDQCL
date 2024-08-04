using BTest.Common;
using BTest.LogHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TouchSocket.Sockets;

namespace BTest
{
    public class HttpClientHelper

    {

        /// <summary>

        /// HttpClient的Get请求

        /// </summary>

        ///<param name="url">请求地址,含拼接数据 </param>

        /// <returns></returns>

        public static string Get(string url)

        {


            try
            {
                var http = HttpClientFactory.GetHttpClient();
                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response1 = http.GetAsync(url).Result;
                string result = response1.Content.ReadAsStringAsync().Result;
                return result;

            }
     
            catch (Exception ex) 
            
            {
                if (ex.InnerException!=null)
                {
                    CommonMethod.MutMessge = string.Empty;
                    CommonMethod.InnerExDeal(ex);
                var result=    CommonMethod.MutMessge;
                    throw new Exception(result);
                }
                else
                {
                    throw new Exception(ex.Message);
                }
              
            }
           






        }

        /// <summary>

        /// HttpClient的Post请求

        /// 表单提交模式[application/x-www-form-urlencoded]

        /// </summary>

        /// <param name="url">请求地址,单纯的地址,没有数据拼接</param>

        /// <param name="data">请求数据,格式为:"userName=admin&pwd=123456"</param>

        /// <returns></returns>

        public static string PostForm(string url, string data)

        {

            var http = HttpClientFactory.GetHttpClient();

            var content = new StringContent(data, Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = http.PostAsync(url, content).Result;

            return response.Content.ReadAsStringAsync().Result;

        }

        public static string Post(string url)

        {

            //var http = HttpClientFactory.GetHttpClient();



            //var response = http.PostAsync(url).Result;

            //return response.Content.ReadAsStringAsync().Result;
            return "";

        }



        /// <summary>

        /// HttpClient的Post请求

        /// Json提交模式[application/json]

        /// </summary>

        /// <param name="url">请求地址,单纯的地址,没有数据拼接</param>

        /// <param name="data">请求数据,格式为(Json)对象、或者类对象 </param>

        /// <returns></returns>

        public static string PostJSON(string url, object data)

        {
            //调用实例
            //var body = new
            //{
            //    userNameOrEmailAddress = "admin",
            //    password = "123qwe"
            //};
            //string token = HttpClientHelper.PostJSON("https://localhost:44302/api/TokenAuth/Authenticate", body);

            var http = HttpClientFactory.GetHttpClient();

            var text = JsonConvert.SerializeObject(data);

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var response = http.PostAsync(url, content).Result;

            return response.Content.ReadAsStringAsync().Result;

        }



    }


    public class HttpClientFactory

    {

        private static HttpClient _httpClient = null;



        /// <summary>

        /// 静态的构造函数：只能有一个，且是无参数的

        /// 由CLR保证，只有在程序第一次使用该类之前被调用，而且只能调用一次

        /// 说明： keep-alive关键字可以理解为一个长链接，超时时间也可以在上面进行设置，例如10秒的超时时间，当然并发量太大，这个10秒应该会抛弃很多请求

        /// 发送请求的代码没有了using，即这个httpclient不会被手动dispose，而是由系统控制它，当然你的程序重启时，这也就被回收了。

        /// </summary>

        static HttpClientFactory()

        {

            _httpClient = new System.Net.Http.HttpClient(new HttpClientHandler());

            _httpClient.Timeout = new TimeSpan(0, 0, 10);

            _httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");

        }



        /// <summary>

        /// 对外开放接口

        /// </summary>

        /// <returns></returns>

        public static HttpClient GetHttpClient()

        {

            return _httpClient;

        }

    }
}
