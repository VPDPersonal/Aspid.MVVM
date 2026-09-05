---
title: "Class LocalizeStringEventVariableMonoBinder"
sidebar_label: "LocalizeStringEventVariableMonoBinder"
description: "Class LocalizeStringEventVariableMonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class LocalizeStringEventVariableMonoBinder {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableMonoBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that writes the bound value into a named Smart String variable of
a [`LocalizeStringEvent`](https://docs.unity3d.com/ScriptReference/Localization-Components-LocalizeStringEvent.html) and refreshes the string.

```csharp
[AddBinderContextMenu(typeof(LocalizeStringEvent), new string[] { "m_StringReference" })]
[AddComponentMenu("Aspid/MVVM/Binders/UI/LocalizeStringEvent/LocalizeStringEvent Binder – Variable")]
public class LocalizeStringEventVariableMonoBinder : ComponentMonoBinder<LocalizeStringEvent>, IMonoBinderValidatable, IRebindableBinder, INumberBinder, IBinder<int>, IBinder<uint>, IBinder<long>, IBinder<ulong>, IBinder<byte>, IBinder<sbyte>, IBinder<short>, IBinder<ushort>, IBinder<float>, IBinder<double>, IBinder<bool>, IBinder<string>, IBinder<Object>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoBinder](Aspid.MVVM.MonoBinder.md) ← 
[ComponentMonoBinder\<LocalizeStringEvent\>](Aspid.MVVM.ComponentMonoBinder-1.md) ← 
[LocalizeStringEventVariableMonoBinder](Aspid.MVVM.StarterKit.LocalizeStringEventVariableMonoBinder.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[INumberBinder](Aspid.MVVM.StarterKit.INumberBinder.md), 
[IBinder\<int\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<uint\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<long\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<ulong\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<byte\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<sbyte\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<short\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<ushort\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<float\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<double\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<bool\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<string\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<Object\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<LocalizeStringEventVariableMonoBinder\>\(LocalizeStringEventVariableMonoBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<LocalizeStringEventVariableMonoBinder\>\(LocalizeStringEventVariableMonoBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
[BinderLogger.Log\(IBinder, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_Log_Aspid_MVVM_IBinder_System_String_UnityEngine_Object_), 
[BinderLogger.LogError\(IBinder, string, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_), 
[BinderLogger.LogError\(IBinder, Exception, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_Exception_System_String_UnityEngine_Object_), 
[BinderLogger.LogWarning\(IBinder, string, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogWarning_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_), 
[BinderMath.NonNegative\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_), 
[BinderMath.NonNegative\(IBinder, Vector2, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_), 
[BinderMath.NonNegative\(IBinder, Vector3, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_), 
[RebindableBinderExtensions.Rebind\(IBinder\)](Aspid.MVVM.RebindableBinderExtensions.md#Aspid_MVVM_RebindableBinderExtensions_Rebind_Aspid_MVVM_IBinder_), 
[BinderMath.RequireFinite\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_), 
[BinderMath.RequireFinite\(IBinder, Vector2, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_), 
[BinderMath.RequireFinite\(IBinder, Vector3, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_), 
[BinderMath.RequireFinite\(IBinder, Vector4, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector4_UnityEngine_Object_), 
[BinderMath.RequireFinite\(IBinder, Rect, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Rect_UnityEngine_Object_), 
[BinderMath.SafeClamp\(IBinder, float, float, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_SafeClamp_Aspid_MVVM_IBinder_System_Single_System_Single_System_Single_UnityEngine_Object_), 
[BinderMath.SafeClamp01\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_SafeClamp01_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_), 
[BinderExtensions.UnbindSafely\<LocalizeStringEventVariableMonoBinder\>\(LocalizeStringEventVariableMonoBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

A missing variable is created with the type of the bound value. A variable of another type is reported and
left unchanged.

## Fields

### SetValueMarker {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableMonoBinder_SetValueMarker}

```csharp
protected static readonly ProfilerMarker SetValueMarker
```

#### Field Value

 ProfilerMarker

## Properties

### IsDebug {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableMonoBinder_IsDebug}

```csharp
protected bool IsDebug { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### AddLog\(string\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableMonoBinder_AddLog_System_String_}

```csharp
protected void AddLog(string log)
```

#### Parameters

`log` [string](https://learn.microsoft.com/dotnet/api/system.string)

### SetValue\(bool\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableMonoBinder_SetValue_System_Boolean_}

```csharp
public void SetValue(bool value)
```

#### Parameters

`value` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### SetValue\(string\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableMonoBinder_SetValue_System_String_}

```csharp
public void SetValue(string value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)

### SetValue\(Object\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableMonoBinder_SetValue_UnityEngine_Object_}

```csharp
public void SetValue(Object value)
```

#### Parameters

`value` Object

### SetValue\(int\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableMonoBinder_SetValue_System_Int32_}

```csharp
public void SetValue(int value)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

### SetValue\(uint\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableMonoBinder_SetValue_System_UInt32_}

```csharp
public void SetValue(uint value)
```

#### Parameters

`value` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### SetValue\(long\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableMonoBinder_SetValue_System_Int64_}

```csharp
public void SetValue(long value)
```

#### Parameters

`value` [long](https://learn.microsoft.com/dotnet/api/system.int64)

### SetValue\(ulong\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableMonoBinder_SetValue_System_UInt64_}

```csharp
public void SetValue(ulong value)
```

#### Parameters

`value` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### SetValue\(byte\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableMonoBinder_SetValue_System_Byte_}

```csharp
public void SetValue(byte value)
```

#### Parameters

`value` [byte](https://learn.microsoft.com/dotnet/api/system.byte)

### SetValue\(sbyte\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableMonoBinder_SetValue_System_SByte_}

```csharp
public void SetValue(sbyte value)
```

#### Parameters

`value` [sbyte](https://learn.microsoft.com/dotnet/api/system.sbyte)

### SetValue\(short\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableMonoBinder_SetValue_System_Int16_}

```csharp
public void SetValue(short value)
```

#### Parameters

`value` [short](https://learn.microsoft.com/dotnet/api/system.int16)

### SetValue\(ushort\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableMonoBinder_SetValue_System_UInt16_}

```csharp
public void SetValue(ushort value)
```

#### Parameters

`value` [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

### SetValue\(float\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableMonoBinder_SetValue_System_Single_}

```csharp
public void SetValue(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

### SetValue\(double\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableMonoBinder_SetValue_System_Double_}

```csharp
public void SetValue(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

