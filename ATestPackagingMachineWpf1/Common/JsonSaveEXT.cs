using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZModels;

namespace ATestPackagingMachineWpf1.Common
{
    public class JsonSaveEXT
    {
        /// <summary>
        /// 设备配置参数
        /// </summary>
        public static DeviceParameterJson deviceParameterJsonGv;

        private static  JsonSaveHelper jsonSaveHelper = new JsonSaveHelper();
        public static readonly string path1 = AppDomain.CurrentDomain.BaseDirectory + "Json";
        public static readonly string path2 = "\\设备参数设置.json";
        public static readonly string path = path1 + path2;
        public static DeviceParameterJson ReadDeviceJson()
        {
            DeviceParameterJson deviceParameterJson = jsonSaveHelper.ReadOneJson<DeviceParameterJson>(path);
            return deviceParameterJson;
        }


        public static void SaveDeviceJson()
        {
       
            string fp = path;
            try
            {
                jsonSaveHelper.WriteJson(deviceParameterJsonGv, fp);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }


        public static void SerializeObjectToFile(string fileName, object obj, string keyString)
        {
            jsonSaveHelper.SerializeObjectToFile( fileName,  obj,  keyString);

        }

        public static object DeserializeObjectFromFile(string fileName, string keyString)
        {

           return jsonSaveHelper.DeserializeObjectFromFile(fileName, keyString);
        }

    }
}
