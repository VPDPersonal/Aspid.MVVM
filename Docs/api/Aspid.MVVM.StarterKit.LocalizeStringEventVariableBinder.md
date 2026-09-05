---
title: "Class LocalizeStringEventVariableBinder"
sidebar_label: "LocalizeStringEventVariableBinder"
description: "Class LocalizeStringEventVariableBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class LocalizeStringEventVariableBinder {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that writes the bound value into a named Smart String variable of
a [`LocalizeStringEvent`](https://docs.unity3d.com/ScriptReference/Localization-Components-LocalizeStringEvent.html) and refreshes the string.

```csharp
[Serializable]
public class LocalizeStringEventVariableBinder : TargetBinder<LocalizeStringEvent>, IRebindableBinder, INumberBinder, IBinder<int>, IBinder<uint>, IBinder<long>, IBinder<ulong>, IBinder<byte>, IBinder<sbyte>, IBinder<short>, IBinder<ushort>, IBinder<float>, IBinder<double>, IBinder<bool>, IBinder<string>, IBinder<Object>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<LocalizeStringEvent\>](Aspid.MVVM.TargetBinder-1.md) ← 
[LocalizeStringEventVariableBinder](Aspid.MVVM.StarterKit.LocalizeStringEventVariableBinder.md)

#### Implements

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

[BinderExtensions.BindSafely\<LocalizeStringEventVariableBinder\>\(LocalizeStringEventVariableBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<LocalizeStringEventVariableBinder\>\(LocalizeStringEventVariableBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<LocalizeStringEventVariableBinder\>\(LocalizeStringEventVariableBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

A missing variable is created with the type of the bound value. A variable of another type is reported and
left unchanged.

## Constructors

### LocalizeStringEventVariableBinder\(\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableBinder__ctor}

```csharp
protected LocalizeStringEventVariableBinder()
```

#### Remarks

For deserialization only.

### LocalizeStringEventVariableBinder\(LocalizeStringEvent, string, BindMode\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableBinder__ctor_UnityEngine_Localization_Components_LocalizeStringEvent_System_String_Aspid_MVVM_BindMode_}

```csharp
public LocalizeStringEventVariableBinder(LocalizeStringEvent target, string variableName = null, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` LocalizeStringEvent

`variableName` [string](https://learn.microsoft.com/dotnet/api/system.string)

`mode` [BindMode](Aspid.MVVM.BindMode.md)

## Methods

### SetValue\(bool\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableBinder_SetValue_System_Boolean_}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(bool value)
```

#### Parameters

`value` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The value received from the ViewModel.

### SetValue\(string\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableBinder_SetValue_System_String_}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(string value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)

The value received from the ViewModel.

### SetValue\(Object\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableBinder_SetValue_UnityEngine_Object_}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(Object value)
```

#### Parameters

`value` Object

The value received from the ViewModel.

### SetValue\(int\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableBinder_SetValue_System_Int32_}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(int value)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The value received from the ViewModel.

### SetValue\(uint\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableBinder_SetValue_System_UInt32_}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(uint value)
```

#### Parameters

`value` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The value received from the ViewModel.

### SetValue\(long\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableBinder_SetValue_System_Int64_}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(long value)
```

#### Parameters

`value` [long](https://learn.microsoft.com/dotnet/api/system.int64)

The value received from the ViewModel.

### SetValue\(ulong\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableBinder_SetValue_System_UInt64_}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(ulong value)
```

#### Parameters

`value` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

The value received from the ViewModel.

### SetValue\(byte\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableBinder_SetValue_System_Byte_}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(byte value)
```

#### Parameters

`value` [byte](https://learn.microsoft.com/dotnet/api/system.byte)

The value received from the ViewModel.

### SetValue\(sbyte\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableBinder_SetValue_System_SByte_}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(sbyte value)
```

#### Parameters

`value` [sbyte](https://learn.microsoft.com/dotnet/api/system.sbyte)

The value received from the ViewModel.

### SetValue\(short\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableBinder_SetValue_System_Int16_}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(short value)
```

#### Parameters

`value` [short](https://learn.microsoft.com/dotnet/api/system.int16)

The value received from the ViewModel.

### SetValue\(ushort\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableBinder_SetValue_System_UInt16_}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(ushort value)
```

#### Parameters

`value` [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

The value received from the ViewModel.

### SetValue\(float\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableBinder_SetValue_System_Single_}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value received from the ViewModel.

### SetValue\(double\) {#Aspid_MVVM_StarterKit_LocalizeStringEventVariableBinder_SetValue_System_Double_}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The value received from the ViewModel.

