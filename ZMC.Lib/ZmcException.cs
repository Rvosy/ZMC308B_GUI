using System;

namespace ZMC.Lib
{
    /// <summary>
    /// ZMC 控制器操作异常，包含原生 DLL 返回的错误码。
    /// </summary>
    public class ZmcException : Exception
    {
        /// <summary>zauxdll.dll 返回的错误码（非零），可查阅 ZMC 手册获取含义。</summary>
        public int ErrorCode { get; }

        public ZmcException(int errorCode)
            : base($"ZMC 操作失败，错误码: {errorCode}")
        {
            ErrorCode = errorCode;
        }

        public ZmcException(int errorCode, string operation)
            : base($"ZMC {operation} 失败，错误码: {errorCode}")
        {
            ErrorCode = errorCode;
        }
    }
}
