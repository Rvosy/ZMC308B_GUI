using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ZMC
{
    public class ZmcDll
    {
        public IntPtr Handle = IntPtr.Zero;

        [DllImport("zauxdll.dll", CharSet = CharSet.Ansi)]
        private static extern int ZAux_OpenEth(string ipAddr, ref IntPtr handle);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Close(IntPtr handle);

        [DllImport("zauxdll.dll", CharSet = CharSet.Ansi)]
        private static extern int ZAux_SearchEthlist(StringBuilder ipList, uint bufferLength, uint timeoutMs);

        public bool IsConnected => Handle != IntPtr.Zero;

        public int OpenEth(string ip)
        {
            return ZAux_OpenEth(ip, ref Handle);
        }

        public int Close()
        {
            int ret = ZAux_Close(Handle);
            Handle = IntPtr.Zero;
            return ret;
        }

        public string SearchEthList(uint timeoutMs = 200)
        {
            StringBuilder sb = new StringBuilder(10240);
            ZAux_SearchEthlist(sb, 10240, timeoutMs);
            return sb.ToString();
        }
    }
}