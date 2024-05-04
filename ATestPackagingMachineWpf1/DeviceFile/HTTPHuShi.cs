using ATestPackagingMachineWpf1.Common;
using BTest;
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
            string result = HttpClientHelper.Get($"http://127.0.0.1:8023/AHChuangFengPre/GetWorkOrderInfo?wo={requestParameter.wo}&mach_code={requestParameter.mach_code}&op_name={requestParameter.op_name}");
            string result2 = "{\"status_code\":200,\"message\":\"成功\",\"cp_rev\":\"MQ12678E2\",\"dept_code\":\"AH20-D\",\"speed\":\"2.4m/min\",\"wc_switch_off\":\"N\",\"gysx_switch_off\":\"N\"}";
            ReturnWorkOrderInfo returnWorkOrderInfo1 = new JsonSaveHelper().JSONToEntity<ReturnWorkOrderInfo>(result2);
            if (result== result2)
            {

            }
            ReturnWorkOrderInfo returnWorkOrderInfo= jsonSaveHelper.JSONToEntity<ReturnWorkOrderInfo>(result);
     
            return returnWorkOrderInfo;
        }

        public ReturnUploadDataData Post(UploadData UploadData)
        {
            var result = HttpClientHelper.PostJSON("http://127.0.0.1:8023/AHChuangFengPre/UploadData", UploadData);
            return jsonSaveHelper.JSONToEntity<ReturnUploadDataData>(result);

        }










    }
}
