using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ZMC.Lib
{
    /// <summary>
    /// ZMC 运动控制器设备封装。通过以太网/COM/PCI 连接 ZMC 控制器，
    /// 提供轴运动控制、IO 操作、多轴插补等完整功能。
    /// <para>所有公开方法返回 int 错误码，0 表示成功，非零为错误码（参阅 ZMC 手册）。</para>
    /// </summary>
    public class ZmcDevice : IDisposable
    {
        private IntPtr _handle = IntPtr.Zero;

        /// <summary>设备是否已连接（Handle 有效）。</summary>
        public bool IsConnected => _handle != IntPtr.Zero;

        #region P/Invoke 声明

        // ==================== 连接/断开 ====================

        [DllImport("zauxdll.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private static extern int ZAux_OpenEth(string ipAddr, ref IntPtr handle);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Close(IntPtr handle);

        [DllImport("zauxdll.dll", CharSet = CharSet.Ansi)]
        private static extern int ZAux_SearchEthlist(StringBuilder ipList, uint bufferLength, uint timeoutMs);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_OpenCom(uint comId, ref IntPtr handle);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_OpenPci(int cardNum, ref IntPtr handle);

        [DllImport("zauxdll.dll", CharSet = CharSet.Ansi)]
        private static extern int ZAux_FastOpen(int type, string connectString, int timeoutMs, ref IntPtr handle);

        // ==================== 通用命令 ====================

        [DllImport("zauxdll.dll", CharSet = CharSet.Ansi)]
        private static extern int ZAux_Execute(IntPtr handle, string command, StringBuilder response, int responseLength);

        [DllImport("zauxdll.dll", CharSet = CharSet.Ansi)]
        private static extern int ZAux_DirectCommand(IntPtr handle, string command, StringBuilder response, int responseLength);

        // ==================== 轴参数 ====================

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_SetSpeed(IntPtr handle, int axis, float value);
        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_GetSpeed(IntPtr handle, int axis, ref float value);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_SetAccel(IntPtr handle, int axis, float value);
        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_GetAccel(IntPtr handle, int axis, ref float value);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_SetDecel(IntPtr handle, int axis, float value);
        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_GetDecel(IntPtr handle, int axis, ref float value);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_SetUnits(IntPtr handle, int axis, float value);
        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_GetUnits(IntPtr handle, int axis, ref float value);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_SetDpos(IntPtr handle, int axis, float value);
        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_GetDpos(IntPtr handle, int axis, ref float value);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_SetMpos(IntPtr handle, int axis, float value);
        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_GetMpos(IntPtr handle, int axis, ref float value);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_GetIfIdle(IntPtr handle, int axis, ref int value);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_GetAxisStatus(IntPtr handle, int axis, ref int value);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_SetAtype(IntPtr handle, int axis, int value);
        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_GetAtype(IntPtr handle, int axis, ref int value);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_SetSramp(IntPtr handle, int axis, float value);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_SetLspeed(IntPtr handle, int axis, float value);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_GetEndMoveBuffer(IntPtr handle, int axis, ref float value);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_GetVpSpeed(IntPtr handle, int axis, ref float value);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_SetMerge(IntPtr handle, int axis, int value);

        // ==================== IO ====================

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_GetIn(IntPtr handle, int ioNum, ref uint value);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_SetOp(IntPtr handle, int ioNum, uint value);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_GetOp(IntPtr handle, int ioNum, ref uint value);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_SetDA(IntPtr handle, int ioNum, float value);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_GetAD(IntPtr handle, int ioNum, ref float value);

        // ==================== 多轴运动 ====================

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_Base(IntPtr handle, int axisCount, int[] axisList);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_Move(IntPtr handle, int axisCount, int[] axisList, float[] distList);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_MoveAbs(IntPtr handle, int axisCount, int[] axisList, float[] distList);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_MoveSp(IntPtr handle, int axisCount, int[] axisList, float[] distList);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_MoveAbsSp(IntPtr handle, int axisCount, int[] axisList, float[] distList);

        // 圆心圆弧
        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_MoveCirc(IntPtr handle, int axisCount, int[] axisList,
            float end1, float end2, float center1, float center2, int direction);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_MoveCircAbs(IntPtr handle, int axisCount, int[] axisList,
            float end1, float end2, float center1, float center2, int direction);

        // 三点圆弧
        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_MoveCirc2(IntPtr handle, int axisCount, int[] axisList,
            float mid1, float mid2, float end1, float end2);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_MoveCirc2Abs(IntPtr handle, int axisCount, int[] axisList,
            float mid1, float mid2, float end1, float end2);

        // 空间圆弧 + 螺旋
        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_MSpherical(IntPtr handle, int axisCount, int[] axisList,
            float end1, float end2, float end3, float center1, float center2, float center3,
            int mode, float param4, float param5);

        // 螺旋插补
        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_MHelical(IntPtr handle, int axisCount, int[] axisList,
            float end1, float end2, float center1, float center2, float direction, float dist3, int mode);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_MHelicalAbs(IntPtr handle, int axisCount, int[] axisList,
            float end1, float end2, float center1, float center2, float direction, float dist3, int mode);

        // ==================== 单轴运动 ====================

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_Single_Move(IntPtr handle, int axis, float distance);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_Single_MoveAbs(IntPtr handle, int axis, float distance);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_Single_Vmove(IntPtr handle, int axis, int direction);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_Single_Cancel(IntPtr handle, int axis, int mode);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_Single_Datum(IntPtr handle, int axis, int mode);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_Rapidstop(IntPtr handle, int mode);

        // ==================== 示波器 ====================

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Trigger(IntPtr handle);

        // ==================== TABLE / VR ====================

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_SetTable(IntPtr handle, int start, int count, float[] values);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_GetTable(IntPtr handle, int start, int count, float[] values);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_SetVrf(IntPtr handle, int start, int count, float[] values);

        [DllImport("zauxdll.dll")]
        private static extern int ZAux_Direct_GetVrf(IntPtr handle, int start, int count, float[] values);

        #endregion

        #region 连接管理

        /// <summary>通过以太网连接控制器。</summary>
        /// <param name="ip">控制器 IP 地址，如 "192.168.0.11"。</param>
        /// <returns>0=成功，非零=错误码。</returns>
        public int OpenEth(string ip)
        {
            return ZAux_OpenEth(ip, ref _handle);
        }

        /// <summary>通过串口连接控制器。</summary>
        /// <param name="comId">COM 端口号。</param>
        public int OpenCom(uint comId)
        {
            return ZAux_OpenCom(comId, ref _handle);
        }

        /// <summary>通过 PCI 卡连接控制器。</summary>
        /// <param name="cardNum">PCI 卡号。</param>
        public int OpenPci(int cardNum)
        {
            return ZAux_OpenPci(cardNum, ref _handle);
        }

        /// <summary>通用连接方式。</summary>
        /// <param name="type">连接类型。</param>
        /// <param name="connectStr">连接字符串。</param>
        /// <param name="timeoutMs">超时毫秒数。</param>
        public int FastOpen(int type, string connectStr, int timeoutMs)
        {
            return ZAux_FastOpen(type, connectStr, timeoutMs, ref _handle);
        }

        /// <summary>关闭连接并释放 Handle。即使 DLL 返回错误，Handle 也会被置零。</summary>
        public int Close()
        {
            if (_handle == IntPtr.Zero) return 0;
            int ret = ZAux_Close(_handle);
            _handle = IntPtr.Zero;
            return ret;
        }

        /// <summary>
        /// 强制断开连接（仅置零 Handle，不调用 DLL 关闭）。
        /// 用于检测到通信异常后的快速断开。
        /// </summary>
        public void Disconnect()
        {
            _handle = IntPtr.Zero;
        }

        /// <summary>扫描局域网中的 ZMC 设备，返回 IP 列表（换行分隔）。</summary>
        /// <param name="timeoutMs">扫描超时（毫秒），默认 200。</param>
        public string SearchEthList(uint timeoutMs = 200)
        {
            var sb = new StringBuilder(10240);
            ZAux_SearchEthlist(sb, 10240, timeoutMs);
            return sb.ToString();
        }

        #endregion

        #region 通用命令

        /// <summary>执行脚本命令并获取响应。</summary>
        public int Execute(string command, out string response)
        {
            var sb = new StringBuilder(2048);
            int ret = ZAux_Execute(_handle, command, sb, 2048);
            response = sb.ToString();
            return ret;
        }

        /// <summary>执行直接命令并获取响应。</summary>
        public int DirectCommand(string command, out string response)
        {
            var sb = new StringBuilder(2048);
            int ret = ZAux_DirectCommand(_handle, command, sb, 2048);
            response = sb.ToString();
            return ret;
        }

        #endregion

        #region 轴参数 — 完整 Get/Set 对

        // --- Speed ---
        public int SetSpeed(int axis, float value) => ZAux_Direct_SetSpeed(_handle, axis, value);
        public int GetSpeed(int axis, out float value) { value = 0; return ZAux_Direct_GetSpeed(_handle, axis, ref value); }
        /// <summary>便捷读取（忽略错误码）。</summary>
        public float GetSpeed(int axis) { float v = 0; ZAux_Direct_GetSpeed(_handle, axis, ref v); return v; }

        // --- Accel ---
        public int SetAccel(int axis, float value) => ZAux_Direct_SetAccel(_handle, axis, value);
        public int GetAccel(int axis, out float value) { value = 0; return ZAux_Direct_GetAccel(_handle, axis, ref value); }
        public float GetAccel(int axis) { float v = 0; ZAux_Direct_GetAccel(_handle, axis, ref v); return v; }

        // --- Decel ---
        public int SetDecel(int axis, float value) => ZAux_Direct_SetDecel(_handle, axis, value);
        public int GetDecel(int axis, out float value) { value = 0; return ZAux_Direct_GetDecel(_handle, axis, ref value); }
        public float GetDecel(int axis) { float v = 0; ZAux_Direct_GetDecel(_handle, axis, ref v); return v; }

        // --- Units（脉冲当量）---
        public int SetUnits(int axis, float value) => ZAux_Direct_SetUnits(_handle, axis, value);
        public int GetUnits(int axis, out float value) { value = 0; return ZAux_Direct_GetUnits(_handle, axis, ref value); }
        public float GetUnits(int axis) { float v = 0; ZAux_Direct_GetUnits(_handle, axis, ref v); return v; }

        // --- Dpos（编程位置/绝对位置）---
        public int SetDpos(int axis, float value) => ZAux_Direct_SetDpos(_handle, axis, value);
        public int GetDpos(int axis, out float value) { value = 0; return ZAux_Direct_GetDpos(_handle, axis, ref value); }
        /// <summary>便捷读取当前位置（忽略错误码）。</summary>
        public float GetDpos(int axis) { float v = 0; ZAux_Direct_GetDpos(_handle, axis, ref v); return v; }

        // --- Mpos（机械位置）---
        public int SetMpos(int axis, float value) => ZAux_Direct_SetMpos(_handle, axis, value);
        public int GetMpos(int axis, out float value) { value = 0; return ZAux_Direct_GetMpos(_handle, axis, ref value); }
        public float GetMpos(int axis) { float v = 0; ZAux_Direct_GetMpos(_handle, axis, ref v); return v; }

        // --- IfIdle（轴空闲状态：1=停止，0=运动中）---
        public int GetIfIdle(int axis, out int value) { value = 0; return ZAux_Direct_GetIfIdle(_handle, axis, ref value); }
        /// <summary>便捷读取（忽略错误码）。返回 1=停止，0=运动中。</summary>
        public int GetIfIdle(int axis) { int v = 0; ZAux_Direct_GetIfIdle(_handle, axis, ref v); return v; }

        // --- AxisStatus ---
        public int GetAxisStatus(int axis, out int value) { value = 0; return ZAux_Direct_GetAxisStatus(_handle, axis, ref value); }
        public int GetAxisStatus(int axis) { int v = 0; ZAux_Direct_GetAxisStatus(_handle, axis, ref v); return v; }

        // --- Atype（轴类型）---
        public int SetAtype(int axis, int value) => ZAux_Direct_SetAtype(_handle, axis, value);
        public int GetAtype(int axis, out int value) { value = 0; return ZAux_Direct_GetAtype(_handle, axis, ref value); }

        // --- Sramp（S 曲线）---
        public int SetSramp(int axis, float value) => ZAux_Direct_SetSramp(_handle, axis, value);

        // --- Lspeed（起始速度）---
        public int SetLspeed(int axis, float value) => ZAux_Direct_SetLspeed(_handle, axis, value);

        // --- Merge（轴合并）---
        public int SetMerge(int axis, int value) => ZAux_Direct_SetMerge(_handle, axis, value);

        // --- EndMoveBuffer ---
        public int GetEndMoveBuffer(int axis, out float value) { value = 0; return ZAux_Direct_GetEndMoveBuffer(_handle, axis, ref value); }
        public float GetEndMoveBuffer(int axis) { float v = 0; ZAux_Direct_GetEndMoveBuffer(_handle, axis, ref v); return v; }

        // --- VpSpeed（实际速度）---
        public int GetVpSpeed(int axis, out float value) { value = 0; return ZAux_Direct_GetVpSpeed(_handle, axis, ref value); }
        public float GetVpSpeed(int axis) { float v = 0; ZAux_Direct_GetVpSpeed(_handle, axis, ref v); return v; }

        #endregion

        #region 单轴运动

        /// <summary>单轴相对运动。</summary>
        public int SingleMove(int axis, float distance) => ZAux_Direct_Single_Move(_handle, axis, distance);

        /// <summary>单轴绝对定位运动。</summary>
        public int SingleMoveAbs(int axis, float position) => ZAux_Direct_Single_MoveAbs(_handle, axis, position);

        /// <summary>单轴速度模式连续运动。dir: 1=正方向，-1=负方向。</summary>
        public int SingleVmove(int axis, int dir) => ZAux_Direct_Single_Vmove(_handle, axis, dir);

        /// <summary>取消单轴运动。</summary>
        public int SingleCancel(int axis, int mode) => ZAux_Direct_Single_Cancel(_handle, axis, mode);

        /// <summary>单轴回零/找原点。</summary>
        public int SingleDatum(int axis, int mode) => ZAux_Direct_Single_Datum(_handle, axis, mode);

        /// <summary>急停。mode: 2=立即停止。</summary>
        public int Rapidstop(int mode) => ZAux_Direct_Rapidstop(_handle, mode);

        #endregion

        #region 多轴插补运动

        /// <summary>设置运动基准轴组。</summary>
        public int Base(int[] axisList) => ZAux_Direct_Base(_handle, axisList.Length, axisList);

        /// <summary>多轴相对直线插补。</summary>
        public int Move(int[] axisList, float[] distList) => ZAux_Direct_Move(_handle, axisList.Length, axisList, distList);

        /// <summary>多轴绝对直线插补。</summary>
        public int MoveAbs(int[] axisList, float[] distList) => ZAux_Direct_MoveAbs(_handle, axisList.Length, axisList, distList);

        /// <summary>多轴相对直线插补（带速度前瞻）。</summary>
        public int MoveSp(int[] axisList, float[] distList) => ZAux_Direct_MoveSp(_handle, axisList.Length, axisList, distList);

        /// <summary>多轴绝对直线插补（带速度前瞻）。</summary>
        public int MoveAbsSp(int[] axisList, float[] distList) => ZAux_Direct_MoveAbsSp(_handle, axisList.Length, axisList, distList);

        /// <summary>圆心圆弧插补（相对）。direction: 0=逆时针，1=顺时针。</summary>
        public int MoveCirc(int[] axisList, float end1, float end2, float center1, float center2, int direction)
            => ZAux_Direct_MoveCirc(_handle, axisList.Length, axisList, end1, end2, center1, center2, direction);

        /// <summary>圆心圆弧插补（绝对）。</summary>
        public int MoveCircAbs(int[] axisList, float end1, float end2, float center1, float center2, int direction)
            => ZAux_Direct_MoveCircAbs(_handle, axisList.Length, axisList, end1, end2, center1, center2, direction);

        /// <summary>三点圆弧插补（相对）。</summary>
        public int MoveCirc2(int[] axisList, float mid1, float mid2, float end1, float end2)
            => ZAux_Direct_MoveCirc2(_handle, axisList.Length, axisList, mid1, mid2, end1, end2);

        /// <summary>三点圆弧插补（绝对）。</summary>
        public int MoveCirc2Abs(int[] axisList, float mid1, float mid2, float end1, float end2)
            => ZAux_Direct_MoveCirc2Abs(_handle, axisList.Length, axisList, mid1, mid2, end1, end2);

        /// <summary>空间圆弧/螺旋运动。</summary>
        public int MSpherical(int[] axisList, float end1, float end2, float end3,
            float center1, float center2, float center3, int mode, float param4, float param5)
            => ZAux_Direct_MSpherical(_handle, axisList.Length, axisList, end1, end2, end3, center1, center2, center3, mode, param4, param5);

        /// <summary>螺旋插补（相对）。</summary>
        public int MHelical(int[] axisList, float end1, float end2, float center1, float center2,
            float direction, float dist3, int mode)
            => ZAux_Direct_MHelical(_handle, axisList.Length, axisList, end1, end2, center1, center2, direction, dist3, mode);

        /// <summary>螺旋插补（绝对）。</summary>
        public int MHelicalAbs(int[] axisList, float end1, float end2, float center1, float center2,
            float direction, float dist3, int mode)
            => ZAux_Direct_MHelicalAbs(_handle, axisList.Length, axisList, end1, end2, center1, center2, direction, dist3, mode);

        #endregion

        #region IO 操作

        /// <summary>读取数字输入状态。</summary>
        public int GetIn(int ioNum, out uint value) { value = 0; return ZAux_Direct_GetIn(_handle, ioNum, ref value); }
        /// <summary>便捷读取数字输入（忽略错误码）。</summary>
        public uint GetIn(int ioNum) { uint v = 0; ZAux_Direct_GetIn(_handle, ioNum, ref v); return v; }

        /// <summary>设置数字输出。value: 0=关，1=开。</summary>
        public int SetOp(int ioNum, uint value) => ZAux_Direct_SetOp(_handle, ioNum, value);

        /// <summary>读取数字输出状态。</summary>
        public int GetOp(int ioNum, out uint value) { value = 0; return ZAux_Direct_GetOp(_handle, ioNum, ref value); }
        /// <summary>便捷读取数字输出（忽略错误码）。</summary>
        public uint GetOp(int ioNum) { uint v = 0; ZAux_Direct_GetOp(_handle, ioNum, ref v); return v; }

        /// <summary>设置模拟输出（DA）。</summary>
        public int SetDA(int ioNum, float value) => ZAux_Direct_SetDA(_handle, ioNum, value);

        /// <summary>读取模拟输入（AD）。</summary>
        public int GetAD(int ioNum, out float value) { value = 0; return ZAux_Direct_GetAD(_handle, ioNum, ref value); }
        /// <summary>便捷读取模拟输入（忽略错误码）。</summary>
        public float GetAD(int ioNum) { float v = 0; ZAux_Direct_GetAD(_handle, ioNum, ref v); return v; }

        #endregion

        #region TABLE / VR 内存

        /// <summary>写入 TABLE 数据区。</summary>
        public int SetTable(int start, float[] values) => ZAux_Direct_SetTable(_handle, start, values.Length, values);

        /// <summary>读取 TABLE 数据区。</summary>
        public int GetTable(int start, int count, float[] buffer) => ZAux_Direct_GetTable(_handle, start, count, buffer);

        /// <summary>便捷读取 TABLE（忽略错误码）。</summary>
        public float[] GetTable(int start, int count)
        {
            var v = new float[count];
            ZAux_Direct_GetTable(_handle, start, count, v);
            return v;
        }

        /// <summary>写入 VR 数据区。</summary>
        public int SetVrf(int start, float[] values) => ZAux_Direct_SetVrf(_handle, start, values.Length, values);

        /// <summary>读取 VR 数据区。</summary>
        public int GetVrf(int start, int count, float[] buffer) => ZAux_Direct_GetVrf(_handle, start, count, buffer);

        /// <summary>便捷读取 VR（忽略错误码）。</summary>
        public float[] GetVrf(int start, int count)
        {
            var v = new float[count];
            ZAux_Direct_GetVrf(_handle, start, count, v);
            return v;
        }

        #endregion

        #region 示波器

        /// <summary>触发示波器采样。</summary>
        public int Trigger() => ZAux_Trigger(_handle);

        #endregion

        #region IDisposable

        public void Dispose()
        {
            if (_handle != IntPtr.Zero)
                Close();
        }

        #endregion
    }
}
