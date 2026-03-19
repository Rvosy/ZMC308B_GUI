可以。我按“**所有 IO 相关函数**”给你分成 4 类列出来：

1. 普通数字量 / 模拟量 IO
2. 特殊 IO 映射
3. 批量 IO 的 Modbus 接口
4. 其它和 IO 有关但不算常规读写的接口

手册在 **2.3 特殊 IO 设置**、**2.7 IO 与 AD/DA 设置读取**、**3.3.4 特殊信号参数函数**、**3.4 输入输出函数介绍** 里把这一组接口分开列了出来。

---

# 一、普通数字量 / 模拟量 IO

## 1) `ZAux_Direct_GetIn`

**功能**：读取单个输入口状态。

**原型**

```c
int32 __stdcall ZAux_Direct_GetIn(ZMC_HANDLE handle, int ionum, uint32 *piValue);
```

**输入参数**

* `handle`：链接标识
* `ionum`：IO 口编号

**输出参数**

* `piValue`：返回状态值

**返回值**

* `int32`：错误码

---

## 2) `ZAux_Direct_SetOp`

**功能**：设置单个输出口状态。手册写的是“打开输出口”，本质就是给输出点赋值。

**原型**

```c
int32 __stdcall ZAux_Direct_SetOp(ZMC_HANDLE handle, int ionum, uint32 iValue);
```

**输入参数**

* `handle`：链接标识
* `ionum`：IO 口编号
* `iValue`：设置值，`0` 关闭，`1` 打开

**输出参数**

* 无

**返回值**

* `int32`：错误码

---

## 3) `ZAux_Direct_GetOp`

**功能**：读取单个输出口状态。

**原型**

```c
int32 __stdcall ZAux_Direct_GetOp(ZMC_HANDLE handle, int ionum, uint32 *piValue);
```

**输入参数**

* `handle`：链接标识
* `ionum`：IO 口编号

**输出参数**

* `piValue`：返回状态值

**返回值**

* `int32`：错误码

**注意**
手册这里的函数说明写成了“读取输入口状态”，但结合函数名和上下文，应当理解为“读取输出口状态”。

---

## 4) `ZAux_Direct_GetAD`

**功能**：读取单个 AD 输入值。

**原型**

```c
int32 __stdcall ZAux_Direct_GetAD(ZMC_HANDLE handle, int ionum, float *pfValue);
```

**输入参数**

* `handle`：链接标识
* `ionum`：IO 口编号 / AD 通道号

**输出参数**

* `pfValue`：返回模拟量值

**返回值**

* `int32`：错误码

---

## 5) `ZAux_Direct_SetDA`

**功能**：设置单个 DA 输出值。

**原型**

```c
int32 __stdcall ZAux_Direct_SetDA(ZMC_HANDLE handle, int ionum, float fValue);
```

**输入参数**

* `handle`：链接标识
* `ionum`：IO 口编号 / DA 通道号
* `fValue`：设置值

**输出参数**

* 无

**返回值**

* `int32`：错误码

---

## 6) `ZAux_Direct_GetDA`

**功能**：读取单个 DA 输出值。

**原型**

```c
int32 __stdcall ZAux_Direct_GetDA(ZMC_HANDLE handle, int ionum, float *pfValue);
```

**输入参数**

* `handle`：链接标识
* `ionum`：IO 口编号 / DA 通道号

**输出参数**

* `pfValue`：返回设置的输出值

**返回值**

* `int32`：错误码

**注意**
手册这里写成“读取模拟量输入口值”，但原型和说明里的 `AOUT`、返回“设置的输出值”说明它实际是读 **DA 输出值**。

---

## 7) `ZAux_Direct_SetPwmFreq`

**功能**：设置 PWM 频率。

**原型**

```c
int32 __stdcall ZAux_Direct_SetPwmFreq(ZMC_HANDLE handle, int ionum, float fValue);
```

**输入参数**

* `handle`：链接标识
* `ionum`：PWM 口编号
* `fValue`：设置值

**输出参数**

* 无

**返回值**

* `int32`：错误码

---

## 8) `ZAux_Direct_GetPwmFreq`

**功能**：读取 PWM 频率。

**原型**

```c
int32 __stdcall ZAux_Direct_GetPwmFreq(ZMC_HANDLE handle, int ionum, float *pfValue);
```

**输入参数**

* `handle`：链接标识
* `ionum`：PWM 口编号

**输出参数**

* `pfValue`：返回 PWM 频率值

**返回值**

* `int32`：错误码

---

# 二、特殊 IO 映射函数

这组不是直接读某个 IO 点当前高低电平，而是**设置 / 读取某个轴的特殊信号绑到了哪个输入口**。手册明确说：正运动控制器没有专用特殊 IO，都是分配到通用 IO 上的。

## 9) `ZAux_Direct_SetInvertIn`

**功能**：设置输入口信号反转状态。

**原型**

```c
int32 __stdcall ZAux_Direct_SetInvertIn(ZMC_HANDLE handle, int ionum, int bifInvert);
```

**输入参数**

* `handle`：链接标识
* `ionum`：输入口编号
* `bifInvert`：状态，`0` 常开，`1` 常闭

**输出参数**

* 无

**返回值**

* `int32`：错误码

**注意**
手册参数说明里把第二个参数写成了 `iaxis 轴号`，但原型是 `ionum`，结合函数语义应按 **输入口编号** 理解。

---

## 10) `ZAux_Direct_GetInvertIn`

**功能**：读取输入口反转状态。

**原型**

```c
int32 __stdcall ZAux_Direct_GetInvertIn(ZMC_HANDLE handle, int iaxis, int iValue);
```

**输入参数**

* `handle`：链接标识
* `iaxis`：手册如此记载

**输出参数**

* `iValue`：返回状态值

**返回值**

* `int32`：错误码

**注意**
这个原型看起来明显有问题，按常规写法更像应该是：

```c
int32 __stdcall ZAux_Direct_GetInvertIn(ZMC_HANDLE handle, int ionum, int *piValue);
```

所以你实际开发时应以 `zauxdll.h` 为准。

---

## 11) `ZAux_Direct_SetAlmIn`

**功能**：设置轴报警信号对应输入口。`-1` 表示取消。

**原型**

```c
int32 __stdcall ZAux_Direct_SetAlmIn(ZMC_HANDLE handle, int iaxis, int iValue);
```

**输入参数**

* `handle`：链接标识
* `iaxis`：轴号
* `iValue`：IO 口编号，`-1` 为取消设置

**输出参数**

* 无

**返回值**

* `int32`：错误码

---

## 12) `ZAux_Direct_GetAlmIn`

**功能**：读取轴报警信号对应输入口。

**原型**
手册这里写成了：

```c
int32 __stdcall ZAux_Direct_GetMerge(ZMC_HANDLE handle, int iaxis, int *piValue);
```

但函数标题是 `ZAux_Direct_GetAlmIn`，这显然是排版笔误。

**按函数含义应理解为**

```c
int32 __stdcall ZAux_Direct_GetAlmIn(ZMC_HANDLE handle, int iaxis, int *piValue);
```

**输入参数**

* `handle`：链接标识
* `iaxis`：轴号

**输出参数**

* `piValue`：返回对应输入口编号，`-1` 表示没有设置

**返回值**

* `int32`：错误码

---

## 13) `ZAux_Direct_SetDatumIn`

**功能**：设置轴原点信号对应输入口。`-1` 表示取消。

**原型**

```c
int32 __stdcall ZAux_Direct_SetDatumIn(ZMC_HANDLE handle, int iaxis, int iValue);
```

**输入参数**

* `handle`：链接标识
* `iaxis`：轴号
* `iValue`：IO 口编号，`-1` 为取消设置

**输出参数**

* 无

**返回值**

* `int32`：错误码

---

## 14) `ZAux_Direct_GetDatumIn`

**功能**：读取轴原点信号对应输入口。

**原型**

```c
int32 __stdcall ZAux_Direct_GetDatumIn(ZMC_HANDLE handle, int iaxis, int *piValue);
```

**输入参数**

* `handle`：链接标识
* `iaxis`：轴号

**输出参数**

* `piValue`：返回对应输入口编号，`-1` 表示没有设置

**返回值**

* `int32`：错误码

---

## 15) `ZAux_Direct_SetFwdIn`

**功能**：设置轴正向限位信号对应输入口。手册在特殊 IO 表中明确列出。

**原型**
手册片段里未展开到这一页的 DLL 原型，但从同组接口风格可整理为：

```c
int32 __stdcall ZAux_Direct_SetFwdIn(ZMC_HANDLE handle, int iaxis, int iValue);
```

**输入参数**

* `handle`：链接标识
* `iaxis`：轴号
* `iValue`：IO 口编号，`-1` 为取消设置

**输出参数**

* 无

**返回值**

* `int32`：错误码

**说明**
函数名与功能在特殊 IO 表中被明确列出。

---

## 16) `ZAux_Direct_GetFwdIn`

**功能**：读取轴正向限位信号对应输入口。

**原型**
手册片段未完整展开该页，但同组接口格式可归纳为：

```c
int32 __stdcall ZAux_Direct_GetFwdIn(ZMC_HANDLE handle, int iaxis, int *piValue);
```

**输入参数**

* `handle`：链接标识
* `iaxis`：轴号

**输出参数**

* `piValue`：返回对应输入口编号，`-1` 表示没有设置

**返回值**

* `int32`：错误码

---

## 17) `ZAux_Direct_SetRevIn`

**功能**：设置轴负向限位信号对应输入口。手册在特殊 IO 表中明确列出。

**原型**

```c
int32 __stdcall ZAux_Direct_SetRevIn(ZMC_HANDLE handle, int iaxis, int iValue);
```

**输入参数**

* `handle`：链接标识
* `iaxis`：轴号
* `iValue`：IO 口编号，`-1` 为取消设置

**输出参数**

* 无

**返回值**

* `int32`：错误码

---

## 18) `ZAux_Direct_GetRevIn`

**功能**：读取轴负向限位信号对应输入口。

**原型**

```c
int32 __stdcall ZAux_Direct_GetRevIn(ZMC_HANDLE handle, int iaxis, int *piValue);
```

**输入参数**

* `handle`：链接标识
* `iaxis`：轴号

**输出参数**

* `piValue`：返回对应输入口编号，`-1` 表示没有设置

**返回值**

* `int32`：错误码

---

# 三、批量 IO / Modbus 相关函数

手册说明：输入输出、模拟量都有对应的 Modbus 寄存器，可以直接通过连续寄存器批量操作多个 IO、AD、DA。`10000` 段映射 `in()`，`20000` 段映射 `out()`，`13000` 段映射 `DA()`，`14000` 段映射 `AD()`。

## 19) `ZAux_Modbus_Get0x`

**功能**：读取 Modbus 位寄存器，可批量读取输入/输出位状态。手册示例明确用它批量读取 `in(0-15)`。

**示例调用**

```c
ZAux_Modbus_Get0x(g_handle,10000,16,&iostate1[0]);
```

**可归纳的参数含义**

* `handle`：链接句柄
* 起始地址：例如 `10000`
* 长度：例如 `16`
* 缓冲区指针：返回读取结果

**返回值**

* `int32`：错误码

**说明**
当前你给我的 PDF 片段里没有展开到它的完整原型页，但手册示例已经明确了它属于 IO 相关接口。

---

## 20) `ZAux_Modbus_Set0x`

**功能**：设置 Modbus 位寄存器，可批量设置输出位。手册在数据交互函数表中明确列出。

**原型**
当前片段未展开到完整声明页。

**输入参数**

* 至少包含：句柄、起始位地址、数量、数据缓冲区

**输出参数**

* 无或写入确认信息

**返回值**

* `int32`：错误码

---

## 21) `ZAux_Modbus_Set4x`

**功能**：设置 Modbus 字寄存器，可批量设置 AD/DA 对应寄存器或其它 4x 数据。

**原型**
当前片段未展开到完整声明页。

**输入参数**

* 至少包含：句柄、起始字寄存器地址、数量、数据缓冲区

**输出参数**

* 无或写入确认信息

**返回值**

* `int32`：错误码

---

## 22) `ZAux_Modbus_Set4x_Float`

**功能**：按浮点方式写 4x 寄存器。手册例程直接用了它去写 `modbus_ieee`。

**示例调用**

```c
ZAux_Modbus_Set4x_Float(g_handle,20,2,&array[0]);
```

**参数含义**

* `g_handle`：句柄
* `20`：起始寄存器
* `2`：浮点数量
* `&array[0]`：数据首地址

**返回值**

* `int32`：错误码

**说明**
虽然它在 2.8 例子里出现，不在 2.7 表里，但实际开发时经常拿来批量写模拟量或浮点寄存器。

---

# 四、和 IO 有关联的其它接口

## 23) `ZAux_Direct_Regist`

**功能**：位置锁存，可根据 Z 脉冲或输入信号 `R0/R1` 的上升沿/下降沿锁存位置。这个接口不是普通 IO 读写，但和输入信号触发密切相关。

**原型**

```c
int32 __stdcall ZAux_Direct_Regist(ZMC_HANDLE handle, int imode);
```

**输入参数**

* `handle`：链接标识
* `imode`：锁存方式
  例如：

  * `3`：输入信号 `R0` 上升沿锁存
  * `4`：输入信号 `R0` 下降沿锁存
  * `10`：`R0` 上升沿锁存到 `REG_POS`，`R1` 上升沿锁存到 `REG_POSB`
  * `13`：`R0` 下降沿锁存到 `REG_POS`，`R1` 下降沿锁存到 `REG_POSB`

**输出参数**

* 无

**返回值**

* `int32`：错误码

---

# 五、整理成一张总表

## 普通 IO

* `ZAux_Direct_GetIn`
* `ZAux_Direct_SetOp`
* `ZAux_Direct_GetOp`
* `ZAux_Direct_GetAD`
* `ZAux_Direct_SetDA`
* `ZAux_Direct_GetDA`
* `ZAux_Direct_SetPwmFreq`
* `ZAux_Direct_GetPwmFreq`

## 特殊 IO 映射

* `ZAux_Direct_SetInvertIn`
* `ZAux_Direct_GetInvertIn`
* `ZAux_Direct_SetAlmIn`
* `ZAux_Direct_GetAlmIn`
* `ZAux_Direct_SetDatumIn`
* `ZAux_Direct_GetDatumIn`
* `ZAux_Direct_SetFwdIn`
* `ZAux_Direct_GetFwdIn`
* `ZAux_Direct_SetRevIn`
* `ZAux_Direct_GetRevIn`

## 批量 / Modbus

* `ZAux_Modbus_Get0x`
* `ZAux_Modbus_Set0x`
* `ZAux_Modbus_Set4x`
* `ZAux_Modbus_Set4x_Float`

## 输入触发相关

* `ZAux_Direct_Regist` 

---

# 六、你写代码时要注意的几个手册笔误

这本 PDF 在 IO 这部分有几处不严谨，直接照抄容易踩坑：

* `ZAux_Direct_GetOp` 的说明写成了“读取输入口状态”，应理解为读取输出口状态。
* `ZAux_Direct_GetDA` 的说明把“输入口值”和“AOUT”混在一起，实际应按 **读取 DA 输出值** 理解。
* `ZAux_Direct_GetInvertIn` 的原型疑似缺少指针，参数名也前后不一致。
* `ZAux_Direct_GetAlmIn` 的原型被错写成了 `ZAux_Direct_GetMerge(...)`。

所以真正下手写 WinForm / C# PInvoke 时，**最好以 `zauxdll.h` 为准，PDF 只拿来理解功能**。

我下一步可以直接把这 23 个接口整理成 **C# 的 `[DllImport]` 声明版本**。
