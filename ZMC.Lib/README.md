# ZMC.Lib — ZMC 运动控制器 SDK

封装 `zauxdll.dll` P/Invoke 调用的 .NET Framework 4.8 类库，提供完整的 ZMC 控制器操作接口。

## 快速开始

### 1. 引用

在你的项目中添加对 `ZMC.Lib.csproj` 的项目引用，或引用编译后的 `ZMC.Lib.dll`。

> `zauxdll.dll` 和 `zmotion.dll` 会自动复制到输出目录，无需手动处理。

### 2. 基本用法

```csharp
using ZMC.Lib;

// 创建设备实例（实现了 IDisposable，推荐使用 using）
using (var device = new ZmcDevice())
{
    // 连接
    int ret = device.OpenEth("192.168.0.11");
    if (ret != 0)
        throw new ZmcException(ret, "连接");

    // 设置轴参数
    device.SetUnits(0, 1000f);   // 脉冲当量
    device.SetSpeed(0, 100f);    // 速度
    device.SetAccel(0, 500f);    // 加速度
    device.SetDecel(0, 500f);    // 减速度

    // 单轴绝对定位
    device.SingleMoveAbs(0, 50f);

    // 读取当前位置
    float pos = device.GetDpos(0);

    // 断开连接（using 块结束时也会自动调用）
    device.Close();
}
```

---

## API 参考

### 连接管理

| 方法 | 说明 |
|------|------|
| `int OpenEth(string ip)` | 以太网连接，传入 IP 地址 |
| `int OpenCom(uint comId)` | 串口连接 |
| `int OpenPci(int cardNum)` | PCI 卡连接 |
| `int FastOpen(int type, string connectStr, int timeoutMs)` | 通用连接 |
| `int Close()` | 关闭连接（安全释放 Handle） |
| `void Disconnect()` | 强制断开（仅置零 Handle，用于异常断开场景） |
| `string SearchEthList(uint timeoutMs = 200)` | 扫描局域网 ZMC 设备，返回 IP 列表（换行分隔） |
| `bool IsConnected` | 属性：是否已连接 |

### 通用命令

| 方法 | 说明 |
|------|------|
| `int Execute(string command, out string response)` | 执行脚本命令 |
| `int DirectCommand(string command, out string response)` | 执行直接命令 |

### 轴参数

每个参数都有三种形式：

```csharp
// 1. Setter — 返回错误码
int ret = device.SetSpeed(0, 100f);

// 2. Getter（带错误码）— 返回错误码 + out 参数
int ret = device.GetSpeed(0, out float speed);

// 3. Getter（便捷）— 直接返回值，忽略错误码
float speed = device.GetSpeed(0);
```

| 参数 | Setter | Getter |
|------|--------|--------|
| Speed（速度） | `SetSpeed(axis, value)` | `GetSpeed(axis)` / `GetSpeed(axis, out value)` |
| Accel（加速度） | `SetAccel(axis, value)` | `GetAccel(axis)` / `GetAccel(axis, out value)` |
| Decel（减速度） | `SetDecel(axis, value)` | `GetDecel(axis)` / `GetDecel(axis, out value)` |
| Units（脉冲当量） | `SetUnits(axis, value)` | `GetUnits(axis)` / `GetUnits(axis, out value)` |
| Dpos（编程位置） | `SetDpos(axis, value)` | `GetDpos(axis)` / `GetDpos(axis, out value)` |
| Mpos（机械位置） | `SetMpos(axis, value)` | `GetMpos(axis)` / `GetMpos(axis, out value)` |
| IfIdle（空闲状态） | — | `GetIfIdle(axis)` → 1=停止, 0=运动中 |
| AxisStatus（轴状态） | — | `GetAxisStatus(axis)` |
| Atype（轴类型） | `SetAtype(axis, value)` | `GetAtype(axis, out value)` |
| VpSpeed（实际速度） | — | `GetVpSpeed(axis)` |
| EndMoveBuffer | — | `GetEndMoveBuffer(axis)` |

其他 Setter（无对应 Getter）：

| 方法 | 说明 |
|------|------|
| `SetSramp(axis, value)` | S 曲线加减速 |
| `SetLspeed(axis, value)` | 起始速度 |
| `SetMerge(axis, value)` | 轴合并 |

### 单轴运动

| 方法 | 说明 |
|------|------|
| `int SingleMove(int axis, float distance)` | 相对运动 |
| `int SingleMoveAbs(int axis, float position)` | 绝对定位 |
| `int SingleVmove(int axis, int dir)` | 速度模式连续运动（1=正方向，-1=负方向） |
| `int SingleCancel(int axis, int mode)` | 取消运动 |
| `int SingleDatum(int axis, int mode)` | 回零/找原点 |
| `int Rapidstop(int mode)` | 急停（mode=2 为立即停止） |

### 多轴插补运动

```csharp
// 设置轴组基准
device.Base(new[] { 0, 1 });

// 两轴直线插补（相对）
device.Move(new[] { 0, 1 }, new[] { 100f, 200f });

// 两轴直线插补（绝对）
device.MoveAbs(new[] { 0, 1 }, new[] { 100f, 200f });

// 带速度前瞻的直线插补
device.MoveSp(new[] { 0, 1 }, new[] { 100f, 200f });
device.MoveAbsSp(new[] { 0, 1 }, new[] { 100f, 200f });
```

| 方法 | 说明 |
|------|------|
| `int Base(int[] axisList)` | 设置运动基准轴组 |
| `int Move(int[] axisList, float[] distList)` | 多轴相对直线插补 |
| `int MoveAbs(int[] axisList, float[] distList)` | 多轴绝对直线插补 |
| `int MoveSp(int[] axisList, float[] distList)` | 带速度前瞻的相对插补 |
| `int MoveAbsSp(int[] axisList, float[] distList)` | 带速度前瞻的绝对插补 |

#### 圆弧插补

```csharp
// 圆心圆弧（相对），direction: 0=逆时针, 1=顺时针
device.MoveCirc(new[] { 0, 1 }, end1, end2, center1, center2, direction);
device.MoveCircAbs(new[] { 0, 1 }, end1, end2, center1, center2, direction);

// 三点圆弧（相对）
device.MoveCirc2(new[] { 0, 1 }, mid1, mid2, end1, end2);
device.MoveCirc2Abs(new[] { 0, 1 }, mid1, mid2, end1, end2);
```

#### 空间运动

| 方法 | 说明 |
|------|------|
| `MSpherical(...)` | 空间圆弧/螺旋 |
| `MHelical(...)` | 螺旋插补（相对） |
| `MHelicalAbs(...)` | 螺旋插补（绝对） |

### IO 操作

```csharp
// 数字输入
uint inState = device.GetIn(0);       // 便捷读取
device.GetIn(0, out uint inValue);    // 带错误码

// 数字输出
device.SetOp(0, 1);                  // 开启
device.SetOp(0, 0);                  // 关闭
uint outState = device.GetOp(0);     // 读取

// 模拟 IO
device.SetDA(0, 3.3f);               // DA 输出
float adValue = device.GetAD(0);     // AD 输入
```

| 方法 | 说明 |
|------|------|
| `uint GetIn(int ioNum)` | 读取数字输入（0/1） |
| `int SetOp(int ioNum, uint value)` | 设置数字输出 |
| `uint GetOp(int ioNum)` | 读取数字输出状态 |
| `int SetDA(int ioNum, float value)` | 设置模拟输出 |
| `float GetAD(int ioNum)` | 读取模拟输入 |

### TABLE / VR 内存

```csharp
// 写入 TABLE 数据
device.SetTable(0, new[] { 1.0f, 2.0f, 3.0f });

// 读取 TABLE 数据
float[] data = device.GetTable(0, 3);

// VR 数据区同理
device.SetVrf(0, new[] { 10f, 20f });
float[] vrData = device.GetVrf(0, 2);
```

### 示波器

```csharp
device.Trigger();  // 触发采样
```

---

## 错误处理

所有方法返回 `int` 错误码，`0` = 成功，非零 = 错误（参阅 ZMC 手册）。

### 方式一：检查错误码（推荐）

```csharp
int ret = device.SetSpeed(0, 100f);
if (ret != 0)
    Console.WriteLine($"设置速度失败，错误码: {ret}");
```

### 方式二：使用 ZmcException

```csharp
int ret = device.OpenEth("192.168.0.11");
if (ret != 0)
    throw new ZmcException(ret, "以太网连接");
```

### 方式三：便捷方法（忽略错误码）

```csharp
// 适合对错误不敏感的场景（如 UI 轮询显示位置）
float pos = device.GetDpos(0);
```

---

## 轮询模式（WinForms 推荐模式）

在 WinForms 中使用 Timer + 后台线程轮询设备状态，避免 UI 卡死：

```csharp
private ZmcDevice device = new ZmcDevice();
private System.Windows.Forms.Timer pollTimer = new System.Windows.Forms.Timer();

private void StartPolling()
{
    pollTimer.Interval = 100; // 100ms
    pollTimer.Tick += (s, e) =>
    {
        pollTimer.Stop();
        if (!device.IsConnected) return;

        System.Threading.Tasks.Task.Run(() =>
        {
            try
            {
                // 后台线程读取设备数据
                int ret = device.GetDpos(0, out float pos);
                if (ret != 0)
                {
                    // 通信异常 → 断开
                    BeginInvoke(new Action(() => {
                        device.Disconnect();
                        // 更新 UI...
                    }));
                    return;
                }

                int idle = device.GetIfIdle(0);

                // 回到 UI 线程更新控件
                BeginInvoke(new Action(() =>
                {
                    lblPos.Text = pos.ToString("F3");
                    lblState.Text = idle != 0 ? "停止" : "运动中";
                    pollTimer.Start(); // 完成后重启定时器
                }));
            }
            catch (Exception ex)
            {
                try
                {
                    BeginInvoke(new Action(() => {
                        device.Disconnect();
                        // 处理错误...
                    }));
                }
                catch (ObjectDisposedException) { }
            }
        });
    };
    pollTimer.Start();
}
```

**关键要点：**
- `Timer.Stop()` 在 Tick 开头调用，防止重入
- DLL 调用放在 `Task.Run` 后台线程，避免网络超时卡 UI
- `BeginInvoke` 回到 UI 线程更新控件
- `Timer.Start()` 在 BeginInvoke 回调末尾重启
- 使用 `GetDpos(axis, out value)` 带错误码版本检测断连

---

## 连接生命周期

```
OpenEth() ─── IsConnected == true ─── Close()
                    │                     │
                    │  通信异常            │
                    ▼                     ▼
              Disconnect()        IsConnected == false
                    │
                    ▼
           IsConnected == false
```

- `Close()`: 正常断开，调用 DLL 关闭连接后置零 Handle
- `Disconnect()`: 异常断开，仅置零 Handle（不调用 DLL，因为通信已失败）
- 所有方法内部通过 `_handle` 访问，外部无法直接修改

---

## 项目引用方式

### 方式一：项目引用（同解决方案）

```xml
<!-- 在你的 .csproj 中添加 -->
<ProjectReference Include="..\ZMC.Lib\ZMC.Lib.csproj">
  <Project>{A1B2C3D4-E5F6-7890-ABCD-EF1234567890}</Project>
  <Name>ZMC.Lib</Name>
</ProjectReference>
```

### 方式二：DLL 引用

将 `ZMC.Lib.dll`、`zauxdll.dll`、`zmotion.dll` 复制到你的项目目录，添加对 `ZMC.Lib.dll` 的引用，并确保两个原生 DLL 配置为"复制到输出目录"。

---

## 原生 DLL 依赖

| 文件 | 说明 |
|------|------|
| `zauxdll.dll` | ZAux 硬件通信库（必须部署） |
| `zmotion.dll` | ZMotion 支持库（必须部署） |

这两个 DLL 在 `ZMC.Lib.csproj` 中已配置为 `CopyToOutputDirectory = PreserveNewest`，构建时自动复制。
