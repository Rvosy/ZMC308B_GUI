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

        // 读取绝对位置
        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_GetDpos(IntPtr handle, int axis, ref float dpos);

        public float GetDpos(int axis)
        {
            float dpos = 0;
            ZAux_Direct_GetDpos(Handle, axis, ref dpos);
            return dpos;
        }

        // 设置脉冲当量
        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_SetUnits(IntPtr handle, int axis, float units);

        public int SetUnits(int axis, float units)
        {
            return ZAux_Direct_SetUnits(Handle, axis, units);
        }

        // 设置速度
        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_SetSpeed(IntPtr handle, int axis, float speed);

        public int SetSpeed(int axis, float speed)
        {
            return ZAux_Direct_SetSpeed(Handle, axis, speed);
        }

        // 设置加速度
        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_SetAccel(IntPtr handle, int axis, float accel);

        public int SetAccel(int axis, float accel)
        {
            return ZAux_Direct_SetAccel(Handle, axis, accel);
        }

        // 设置减速度
        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_SetDecel(IntPtr handle, int axis, float decel);

        public int SetDecel(int axis, float decel)
        {
            return ZAux_Direct_SetDecel(Handle, axis, decel);
        }

        // 单轴相对运动
        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_Single_Move(IntPtr handle, int axis, float distance);

        public int SingleMoveRel(int axis, float distance)
        {
            return ZAux_Direct_Single_Move(Handle, axis, distance);
        }

        // 单轴绝对定位运动
        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_Single_MoveAbs(IntPtr handle, int axis, float distance);

        public int SingleMoveAbs(int axis, float distance)
        {
            return ZAux_Direct_Single_MoveAbs(Handle, axis, distance);
        }

        // 急停
        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_Rapidstop(IntPtr handle, int mode);

        public int Rapidstop(int mode)
        {
            return ZAux_Direct_Rapidstop(Handle, mode);
        }

        // 设置绝对位置（设为零点时传入0）
        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_SetDpos(IntPtr handle, int axis, float value);

        public int SetDpos(int axis, float value)
        {
            return ZAux_Direct_SetDpos(Handle, axis, value);
        }

        // 查询轴是否空闲（1=停止，0=运动中）
        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_GetIfIdle(IntPtr handle, int axis, ref int value);

        public int GetIfIdle(int axis)
        {
            int v = 0;
            ZAux_Direct_GetIfIdle(Handle, axis, ref v);
            return v;
        }

        // 读取单个数字输入口状态
        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_GetIn(IntPtr handle, int ionum, ref uint piValue);

        // 设置单个数字输出口状态（0=关，1=开）
        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_SetOp(IntPtr handle, int ionum, uint iValue);

        // 读取单个数字输出口状态
        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_GetOp(IntPtr handle, int ionum, ref uint piValue);

        public uint GetIn(int ioNum) { uint v = 0; ZAux_Direct_GetIn(Handle, ioNum, ref v); return v; }
        public int SetOp(int ioNum, uint value) => ZAux_Direct_SetOp(Handle, ioNum, value);
        public uint GetOp(int ioNum) { uint v = 0; ZAux_Direct_GetOp(Handle, ioNum, ref v); return v; }
    }
}