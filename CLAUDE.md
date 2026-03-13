# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## 项目概述

ZMC 是一个基于 .NET Framework 4.8 的 Windows 窗体（WinForms）应用程序，用于通过以太网控制 ZMC 运动控制器，支持四轴（Z1/Z2/Z3/X）精确运动控制。

## 构建与运行

```bash
# 调试构建
msbuild ZMC.sln /p:Configuration=Debug

# 发布构建
msbuild ZMC.sln /p:Configuration=Release

# 指定平台（x86/x64/AnyCPU）
msbuild ZMC.sln /p:Configuration=Debug /p:Platform=x86
```

输出目录：`bin\Debug\` 或 `bin\Release\`

> 本项目无自动化测试框架，需连接真实 ZMC 硬件进行功能验证。

## 核心架构

### 分层结构

```
Form1.cs          ← UI 层：事件处理、状态展示、参数校验
    ↓
Zmcdll.cs         ← DLL 封装层：P/Invoke 包装，对上层屏蔽原生 DLL 细节
    ↓
zauxdll.dll       ← 原生驱动层：ZAux 硬件通信库（必须随程序一起部署）
zmotion.dll       ← 原生驱动层：ZMotion 支持库
```

### 关键设计点

**连接状态判断**：`ZmcDll.Handle != IntPtr.Zero` 即为已连接，`IsConnected` 属性封装此逻辑。所有操作前均需检查连接状态。

**DLL 返回值**：所有 `zauxdll.dll` 函数返回 `int`，`0` 表示成功，非零为错误码，需向用户展示错误码以便查阅 ZMC 手册排查。

**位置轮询**：连接成功后启动 `timer1`，定时调用 `GetDpos()` 刷新四轴当前位置显示；断开时停止定时器。

**轴号映射**：
```
轴号 0 → Z1 轴
轴号 1 → Z2 轴
轴号 2 → Z3 轴
轴号 3 → X  轴
```

**运动模式**：`isAbsoluteMode` 字段控制绝对/相对运动切换，单选框事件切换时会清空终点输入框防止混淆。

### temp.cs 的用途

`temp.cs` 是一个**扩展参考文件**，包含比 `Zmcdll.cs` 更完整的 P/Invoke 声明（多轴插补、圆弧运动、IO 控制、TABLE/VR 数据等）。当需要扩展新功能时，应将 `temp.cs` 中对应的方法迁移到 `Zmcdll.cs` 并整合到命名空间 `ZMC` 中。

### 原生 DLL 依赖

`zauxdll.dll` 和 `zmotion.dll` 已在 `.csproj` 中配置为 `CopyToOutputDirectory=PreserveNewest`，构建时会自动复制到输出目录，无需手动操作。
