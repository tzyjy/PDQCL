/*
此代码由Rpc工具直接生成，非必要请不要修改此处代码
*/
#pragma warning disable
using System;
using TouchSocket.Core;
using TouchSocket.Sockets;
using TouchSocket.Rpc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
namespace WebApiProxy
{
public interface IPDXianYing:TouchSocket.Rpc.IRemoteServer
{
///<summary>
///无注释信息
///</summary>
/// <exception cref="System.TimeoutException">调用超时</exception>
/// <exception cref="TouchSocket.Rpc.RpcInvokeException">Rpc调用异常</exception>
/// <exception cref="System.Exception">其他异常</exception>
ReturnYXInfo GetWorkOrderInfo(System.String wo,System.String mach_code,System.String op_name,IInvokeOption invokeOption = default);
///<summary>
///无注释信息
///</summary>
/// <exception cref="System.TimeoutException">调用超时</exception>
/// <exception cref="TouchSocket.Rpc.RpcInvokeException">Rpc调用异常</exception>
/// <exception cref="System.Exception">其他异常</exception>
Task<ReturnYXInfo> GetWorkOrderInfoAsync(System.String wo,System.String mach_code,System.String op_name,IInvokeOption invokeOption = default);

}
public class PDXianYing :IPDXianYing
{
public PDXianYing(IRpcClient client)
{
this.Client=client;
}
public IRpcClient Client{get;private set; }
///<summary>
///无注释信息
///</summary>
/// <exception cref="System.TimeoutException">调用超时</exception>
/// <exception cref="TouchSocket.Rpc.RpcInvokeException">Rpc调用异常</exception>
/// <exception cref="System.Exception">其他异常</exception>
public ReturnYXInfo GetWorkOrderInfo(System.String wo,System.String mach_code,System.String op_name,IInvokeOption invokeOption = default)
{
if(Client==null)
{
throw new RpcException("IRpcClient为空，请先初始化或者进行赋值");
}
object[] parameters = new object[]{wo,mach_code,op_name};
ReturnYXInfo returnData=(ReturnYXInfo)Client.Invoke(typeof(ReturnYXInfo),"GET:/pdxianying/getworkorderinfo?wo={0}&mach_code={1}&op_name={2}",invokeOption, parameters);
return returnData;
}
///<summary>
///无注释信息
///</summary>
public async Task<ReturnYXInfo> GetWorkOrderInfoAsync(System.String wo,System.String mach_code,System.String op_name,IInvokeOption invokeOption = default)
{
if(Client==null)
{
throw new RpcException("IRpcClient为空，请先初始化或者进行赋值");
}
object[] parameters = new object[]{wo,mach_code,op_name};
return (ReturnYXInfo) await Client.InvokeAsync(typeof(ReturnYXInfo),"GET:/pdxianying/getworkorderinfo?wo={0}&mach_code={1}&op_name={2}",invokeOption, parameters);
}

}
public static class PDXianYingExtensions
{
///<summary>
///无注释信息
///</summary>
/// <exception cref="System.TimeoutException">调用超时</exception>
/// <exception cref="TouchSocket.Rpc.RpcInvokeException">Rpc调用异常</exception>
/// <exception cref="System.Exception">其他异常</exception>
public static ReturnYXInfo GetWorkOrderInfo<TClient>(this TClient client,System.String wo,System.String mach_code,System.String op_name,IInvokeOption invokeOption = default) where TClient:
TouchSocket.WebApi.IWebApiClientBase{
object[] parameters = new object[]{wo,mach_code,op_name};
ReturnYXInfo returnData=(ReturnYXInfo)client.Invoke(typeof(ReturnYXInfo),"GET:/pdxianying/getworkorderinfo?wo={0}&mach_code={1}&op_name={2}",invokeOption, parameters);
return returnData;
}
///<summary>
///无注释信息
///</summary>
public static async Task<ReturnYXInfo> GetWorkOrderInfoAsync<TClient>(this TClient client,System.String wo,System.String mach_code,System.String op_name,IInvokeOption invokeOption = default) where TClient:
TouchSocket.WebApi.IWebApiClientBase{
object[] parameters = new object[]{wo,mach_code,op_name};
return (ReturnYXInfo) await client.InvokeAsync(typeof(ReturnYXInfo),"GET:/pdxianying/getworkorderinfo?wo={0}&mach_code={1}&op_name={2}",invokeOption, parameters);
}

}
public class ReturnYXInfo
{
public System.Int32 status_code { get; set; }
public System.String message { get; set; }
public System.String gmxh { get; set; }
public System.String gmhd { get; set; }
}

}
