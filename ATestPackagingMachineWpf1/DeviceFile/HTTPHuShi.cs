using ATestPackagingMachineWpf1.Common;
using BTest;
using BTest.LogHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZModels;

namespace ATestPackagingMachineWpf1.DeviceFile
{
    public class HTTPHuShi
    {
        JsonSaveHelper jsonSaveHelper=new JsonSaveHelper();


        public ReturnWorkOrderInfo Get(RequestWorkOrderInfoPra requestParameter)
        {
            if (!JsonSaveEXT.deviceParameterJsonGv.MesTest)
            {
                string result = HttpClientHelper.Get($"http://172.16.100.15:8023/AHChuangFengPre/GetWorkOrderInfo?wo={requestParameter.wo}&mach_code={requestParameter.mach_code}&op_name={requestParameter.op_name}");

            
                ReturnWorkOrderInfo returnWorkOrderInfo = jsonSaveHelper.JSONToEntity<ReturnWorkOrderInfo>(result);
           
                return returnWorkOrderInfo;
            }
            else
            {
                string result = HttpClientHelper.Get($"http://127.0.0.1:8023/AHChuangFengPre/GetWorkOrderInfo?wo={requestParameter.wo}&mach_code={requestParameter.mach_code}&op_name={requestParameter.op_name}");

            
                ReturnWorkOrderInfo returnWorkOrderInfo = jsonSaveHelper.JSONToEntity<ReturnWorkOrderInfo>(result);
                jsonSaveHelper.WriteJson(returnWorkOrderInfo, @"\Mes信息.json");
                return returnWorkOrderInfo;


            }
          
        }

        public ReturnUploadDataData Post(UploadData UploadData)
        {
            var result = HttpClientHelper.PostJSON("http://172.16.100.15:8023/AHChuangFengPre/UploadData", UploadData);
            return jsonSaveHelper.JSONToEntity<ReturnUploadDataData>(result);

        }










    }
}
