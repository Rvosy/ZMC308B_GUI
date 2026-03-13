using System;
using System.Runtime.InteropServices;
using System.Text;

public class ZmcDll
{
    public IntPtr Handle = IntPtr.Zero;

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

    // ==================== 轴参数设置 ====================

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
    private static extern int ZAux_Direct_GetIn(IntPtr handle, int ioNum, ref int value);

    [DllImport("zauxdll.dll")]
    private static extern int ZAux_Direct_SetOp(IntPtr handle, int ioNum, int value);

    [DllImport("zauxdll.dll")]
    private static extern int ZAux_Direct_GetOp(IntPtr handle, int ioNum, ref int value);

    [DllImport("zauxdll.dll")]
    private static extern int ZAux_Direct_SetDA(IntPtr handle, int ioNum, float value);

    [DllImport("zauxdll.dll")]
    private static extern int ZAux_Direct_GetAD(IntPtr handle, int ioNum, ref float value);

    // ==================== BASE / 运动指令 ====================

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

    // ==================== 公开方法封装 ====================

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

    public int OpenCom(uint comId)
    {
        return ZAux_OpenCom(comId, ref Handle);
    }

    public int OpenPci(int cardNum)
    {
        return ZAux_OpenPci(cardNum, ref Handle);
    }

    public int FastOpen(int type, string connectStr, int timeoutMs)
    {
        return ZAux_FastOpen(type, connectStr, timeoutMs, ref Handle);
    }

    public (int ret, string response) Execute(string command)
    {
        StringBuilder sb = new StringBuilder(2048);
        int ret = ZAux_Execute(Handle, command, sb, 2048);
        return (ret, sb.ToString());
    }

    public (int ret, string response) DirectCommand(string command)
    {
        StringBuilder sb = new StringBuilder(2048);
        int ret = ZAux_DirectCommand(Handle, command, sb, 2048);
        return (ret, sb.ToString());
    }

    // 轴参数
    public int SetSpeed(int axis, float value) => ZAux_Direct_SetSpeed(Handle, axis, value);
    public float GetSpeed(int axis) { float v = 0; ZAux_Direct_GetSpeed(Handle, axis, ref v); return v; }

    public int SetAccel(int axis, float value) => ZAux_Direct_SetAccel(Handle, axis, value);
    public float GetAccel(int axis) { float v = 0; ZAux_Direct_GetAccel(Handle, axis, ref v); return v; }

    public int SetDecel(int axis, float value) => ZAux_Direct_SetDecel(Handle, axis, value);
    public float GetDecel(int axis) { float v = 0; ZAux_Direct_GetDecel(Handle, axis, ref v); return v; }

    public int SetUnits(int axis, float value) => ZAux_Direct_SetUnits(Handle, axis, value);
    public int SetDpos(int axis, float value) => ZAux_Direct_SetDpos(Handle, axis, value);

    public float GetDpos(int axis) { float v = 0; ZAux_Direct_GetDpos(Handle, axis, ref v); return v; }
    public float GetMpos(int axis) { float v = 0; ZAux_Direct_GetMpos(Handle, axis, ref v); return v; }

    public int GetIfIdle(int axis) { int v = 0; ZAux_Direct_GetIfIdle(Handle, axis, ref v); return v; }
    public int GetAxisStatus(int axis) { int v = 0; ZAux_Direct_GetAxisStatus(Handle, axis, ref v); return v; }

    public int SetAtype(int axis, int value) => ZAux_Direct_SetAtype(Handle, axis, value);
    public int SetSramp(int axis, float value) => ZAux_Direct_SetSramp(Handle, axis, value);
    public int SetLspeed(int axis, float value) => ZAux_Direct_SetLspeed(Handle, axis, value);
    public int SetMerge(int axis, int value) => ZAux_Direct_SetMerge(Handle, axis, value);

    public float GetEndMoveBuffer(int axis) { float v = 0; ZAux_Direct_GetEndMoveBuffer(Handle, axis, ref v); return v; }
    public float GetVpSpeed(int axis) { float v = 0; ZAux_Direct_GetVpSpeed(Handle, axis, ref v); return v; }

    // IO
    public int GetIn(int ioNum) { int v = 0; ZAux_Direct_GetIn(Handle, ioNum, ref v); return v; }
    public int SetOp(int ioNum, int value) => ZAux_Direct_SetOp(Handle, ioNum, value);
    public int GetOp(int ioNum) { int v = 0; ZAux_Direct_GetOp(Handle, ioNum, ref v); return v; }
    public int SetDA(int ioNum, float value) => ZAux_Direct_SetDA(Handle, ioNum, value);
    public float GetAD(int ioNum) { float v = 0; ZAux_Direct_GetAD(Handle, ioNum, ref v); return v; }

    // 运动指令
    public int Base(int[] axisList) => ZAux_Direct_Base(Handle, axisList.Length, axisList);
    public int Move(int[] axisList, float[] distList) => ZAux_Direct_Move(Handle, axisList.Length, axisList, distList);
    public int MoveAbs(int[] axisList, float[] distList) => ZAux_Direct_MoveAbs(Handle, axisList.Length, axisList, distList);
    public int MoveSp(int[] axisList, float[] distList) => ZAux_Direct_MoveSp(Handle, axisList.Length, axisList, distList);
    public int MoveAbsSp(int[] axisList, float[] distList) => ZAux_Direct_MoveAbsSp(Handle, axisList.Length, axisList, distList);

    public int MoveCirc(int[] axisList, float end1, float end2, float center1, float center2, int dir)
        => ZAux_Direct_MoveCirc(Handle, axisList.Length, axisList, end1, end2, center1, center2, dir);

    public int MoveCircAbs(int[] axisList, float end1, float end2, float center1, float center2, int dir)
        => ZAux_Direct_MoveCircAbs(Handle, axisList.Length, axisList, end1, end2, center1, center2, dir);

    public int MoveCirc2(int[] axisList, float mid1, float mid2, float end1, float end2)
        => ZAux_Direct_MoveCirc2(Handle, axisList.Length, axisList, mid1, mid2, end1, end2);

    public int MoveCirc2Abs(int[] axisList, float mid1, float mid2, float end1, float end2)
        => ZAux_Direct_MoveCirc2Abs(Handle, axisList.Length, axisList, mid1, mid2, end1, end2);

    public int MSpherical(int[] axisList, float end1, float end2, float end3,
        float center1, float center2, float center3, int mode, float param4, float param5)
        => ZAux_Direct_MSpherical(Handle, axisList.Length, axisList, end1, end2, end3, center1, center2, center3, mode, param4, param5);

    // 单轴运动
    public int SingleMove(int axis, float dist) => ZAux_Direct_Single_Move(Handle, axis, dist);
    public int SingleMoveAbs(int axis, float dist) => ZAux_Direct_Single_MoveAbs(Handle, axis, dist);
    public int SingleVmove(int axis, int dir) => ZAux_Direct_Single_Vmove(Handle, axis, dir);
    public int SingleCancel(int axis, int mode) => ZAux_Direct_Single_Cancel(Handle, axis, mode);
    public int SingleDatum(int axis, int mode) => ZAux_Direct_Single_Datum(Handle, axis, mode);
    public int Rapidstop(int mode) => ZAux_Direct_Rapidstop(Handle, mode);

    public int Trigger() => ZAux_Trigger(Handle);

    // TABLE / VR
    public int SetTable(int start, float[] values) => ZAux_Direct_SetTable(Handle, start, values.Length, values);
    public float[] GetTable(int start, int count) { float[] v = new float[count]; ZAux_Direct_GetTable(Handle, start, count, v); return v; }
    public int SetVrf(int start, float[] values) => ZAux_Direct_SetVrf(Handle, start, values.Length, values);
    public float[] GetVrf(int start, int count) { float[] v = new float[count]; ZAux_Direct_GetVrf(Handle, start, count, v); return v; }
}