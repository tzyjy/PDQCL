using ATestPackagingMachineWpf1.Common;
using BTest.LogHelper;
using BTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZModels;
using ATestPackagingMachineWpf1.ZModels;

namespace ATestPackagingMachineWpf1.DeviceFile
{
    public class PDQCLAPI
    {
        private JsonSaveHelper jsonSaveHelper = new JsonSaveHelper();

        public ReturnPDQCLInfo Get(RequestWorkOrderInfoPra requestParameter)
        {
            try
            {
                string result = HttpClientHelper.Get($"http://{JsonSaveEXT.deviceParameterJsonGv.WebApi_Ipadress}:{JsonSaveEXT.deviceParameterJsonGv.WebApi_Port}/PDProcessing/GetWorkOrderInfo?wo={requestParameter.wo}&mach_code={requestParameter.mach_code}&op_name={requestParameter.op_name}");
                LOG.WriteMesLog("API:GetWorkOrderInfo+Mes回复：" + result);

                ReturnPDQCLInfo returnYXInfo = jsonSaveHelper.JSONToEntity<ReturnPDQCLInfo>(result);

                return returnYXInfo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}