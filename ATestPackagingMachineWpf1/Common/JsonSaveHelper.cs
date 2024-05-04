
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Shapes;
using ZModels;

namespace ATestPackagingMachineWpf1.Common
{
    public class JsonSaveHelper
    {
       


        /// <summary>
        /// 读取配置参数
        /// </summary>
        /// <returns></returns>
        public  List<T> ReadMoreJson<T>(string path)
        {
            //string path = Application.StartupPath + "\\Json\\参数设置.json";
            if (File.Exists(path))
            {
                StreamReader sr = new StreamReader(path);
                string content = sr.ReadToEnd();//如果使用这种方法要根据实际情况读取所有的行
                sr.Dispose();
                List<T> modelList = new List<T>();//创建泛型集合
                modelList = JsonConvert.DeserializeObject<List<T>>(content);
                return modelList;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 写入一个对象
        /// </summary>
        /// <param name="myobj"></param>
        public  void WriteJson(object myobj, string fp)
        {

            if (!Directory.Exists(JsonSaveEXT.path1))
            {
                Directory.CreateDirectory(JsonSaveEXT.path1);
            }
            //string fp = Application.StartupPath + "\\Json\\" + fileName +".json";
            if (!File.Exists(fp))  // 判断是否已有相同文件
            {
                FileStream fs1 = new FileStream(fp, FileMode.Create, FileAccess.ReadWrite);
                fs1.Close();
            }

            File.WriteAllText(fp, JsonConvert.SerializeObject(myobj, Formatting.Indented));
        }

        /// <summary>
        /// 读取一个对象
        /// </summary>s
        /// <param name="myobj"></param>
        public  T ReadOneJson<T>(string path)
        {
            if (File.Exists(path))
            {
                StreamReader sr = new StreamReader(path);
                string content = sr.ReadToEnd();//如果使用这种方法要根据实际情况读取所有的行
                sr.Dispose();

                var model = JsonConvert.DeserializeObject<T>(content);
                if (model != null)
                {
                    return (T)(object)model;
                }
                else
                {
                    return default(T);
                }
            }
            else
            {
                return default(T);
            }
        }


        /// <summary>
        /// 实体对象转换成JSON字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"></param>
        /// <returns></returns>
        public  string EntityToJSON<T>(T x)
        {
            string result = string.Empty;

            try
            {
                result = JsonConvert.SerializeObject(x);
            }
            catch (Exception)
            {
                result = string.Empty;
            }
            return result;

        }

        /// <summary>
        /// JSON字符串转换成实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public  T JSONToEntity<T>(string json)
        {
            T t = default(T);
            try
            {
                t = (T)JsonConvert.DeserializeObject(json, typeof(T));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return t;
        }


        /// <summary>
        /// 把对象序列化到文件(AES加密)
        /// </summary>
        /// <param name="keyString">密钥(16位)</param>
        public  void SerializeObjectToFile(string fileName, object obj, string keyString)
        {
            using (AesCryptoServiceProvider crypt = new AesCryptoServiceProvider())
            {
                crypt.Key = Encoding.ASCII.GetBytes(keyString);
                crypt.IV = Encoding.ASCII.GetBytes(keyString);
                using (ICryptoTransform transform = crypt.CreateEncryptor())
                {
                    FileStream fs = new FileStream(fileName, FileMode.Create);
                    using (CryptoStream cs = new CryptoStream(fs, transform, CryptoStreamMode.Write))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(cs, obj);
                    }
                }
            }
        }

        /// <summary>
        /// 把文件反序列化成对象(AES解密)
        /// </summary>
        /// <param name="keyString">密钥(16位)</param>
        public  object DeserializeObjectFromFile(string fileName, string keyString)
        {
            using (AesCryptoServiceProvider crypt = new AesCryptoServiceProvider())
            {
                crypt.Key = Encoding.ASCII.GetBytes(keyString);
                crypt.IV = Encoding.ASCII.GetBytes(keyString);
                using (ICryptoTransform transform = crypt.CreateDecryptor())
                {
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                    using (CryptoStream cs = new CryptoStream(fs, transform, CryptoStreamMode.Read))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        object obj = formatter.Deserialize(cs);
                        return obj;
                    }
                }
            }
        }


    }
}