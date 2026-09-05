---
title: "Class SliderCommandMonoBinder<T1, T2>"
sidebar_label: "SliderCommandMonoBinder<T1, T2>"
description: "Class SliderCommandMonoBinder<T1, T2> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class SliderCommandMonoBinder\<T1, T2\> {#Aspid_MVVM_StarterKit_SliderCommandMonoBinder_2}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on
[`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Slider-onValueChanged.html) with the slider value and [`SliderCommandMonoBinder<T1, T2>.Param1`](Aspid.MVVM.StarterKit.SliderCommandMonoBinder-2.md#Aspid_MVVM_StarterKit_SliderCommandMonoBinder_2_Param1), [`SliderCommandMonoBinder<T1, T2>.Param2`](Aspid.MVVM.StarterKit.SliderCommandMonoBinder-2.md#Aspid_MVVM_StarterKit_SliderCommandMonoBinder_2_Param2).

```csharp
public abstract class SliderCommandMonoBinder<T1, T2> : ComponentMonoBinder<Slider>, IMonoBinderValidatable, IRebindableBinder, IBinder<IRelayCommand<int, T1, T2>>, IBinder<IRelayCommand<long, T1, T2>>, IBinder<IRelayCommand<float, T1, T2>>, IBinder<IRelayCommand<double, T1, T2>>, IBinder
```

#### Type Parameters

`T1` 

The type of the first extra parameter.

`T2` 

The type of the second extra parameter.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoBinder](Aspid.MVVM.MonoBinder.md) ← 
[ComponentMonoBinder\<Slider\>](Aspid.MVVM.ComponentMonoBinder-1.md) ← 
[SliderCommandMonoBinder\<T1, T2\>](Aspid.MVVM.StarterKit.SliderCommandMonoBinder-2.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<IRelayCommand\<int, T1, T2\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IRelayCommand\<long, T1, T2\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IRelayCommand\<float, T1, T2\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IRelayCommand\<double, T1, T2\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<SliderCommandMonoBinder\<T1, T2\>\>\(SliderCommandMonoBinder\<T1, T2\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<SliderCommandMonoBinder\<T1, T2\>\>\(SliderCommandMonoBinder\<T1, T2\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<SliderCommandMonoBinder\<T1, T2\>\>\(SliderCommandMonoBinder\<T1, T2\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

Accepts [`IRelayCommand<T1, T2, T3>`](Aspid.MVVM.IRelayCommand-3.md) with an <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">int</a>, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">long</a>,
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">float</a> or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">double</a> value; integers are truncated.

## Fields

### SetValueMarker {#Aspid_MVVM_StarterKit_SliderCommandMonoBinder_2_SetValueMarker}

```csharp
protected static readonly ProfilerMarker SetValueMarker
```

#### Field Value

 ProfilerMarker

## Properties

### IsDebug {#Aspid_MVVM_StarterKit_SliderCommandMonoBinder_2_IsDebug}

```csharp
protected bool IsDebug { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### Param1 {#Aspid_MVVM_StarterKit_SliderCommandMonoBinder_2_Param1}

Gets or sets the extra parameter passed after the slider value.

```csharp
public virtual T1 Param1 { get; set; }
```

#### Property Value

 T1

### Param2 {#Aspid_MVVM_StarterKit_SliderCommandMonoBinder_2_Param2}

Gets or sets the extra parameter passed after the slider value.

```csharp
public virtual T2 Param2 { get; set; }
```

#### Property Value

 T2

## Methods

### AddLog\(string\) {#Aspid_MVVM_StarterKit_SliderCommandMonoBinder_2_AddLog_System_String_}

```csharp
protected void AddLog(string log)
```

#### Parameters

`log` [string](https://learn.microsoft.com/dotnet/api/system.string)

### OnBound\(\) {#Aspid_MVVM_StarterKit_SliderCommandMonoBinder_2_OnBound}

Called after binding is established and the first value is applied. Override to subscribe to the component.

```csharp
protected override void OnBound()
```

### OnUnbound\(\) {#Aspid_MVVM_StarterKit_SliderCommandMonoBinder_2_OnUnbound}

Called after unbinding. Override to release a subscription taken in [`MonoBinder.OnBound`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnBound).

```csharp
protected override void OnUnbound()
```

### OnValidate\(\) {#Aspid_MVVM_StarterKit_SliderCommandMonoBinder_2_OnValidate}

Called by Unity in the Editor when a serialized value changes. Fills the empty component field outside Play mode.

```csharp
protected override void OnValidate()
```

#### Remarks

When overriding, always call <code>base.OnValidate()</code>.

### SetValue\(IRelayCommand\<int, T1, T2\>\) {#Aspid_MVVM_StarterKit_SliderCommandMonoBinder_2_SetValue_Aspid_MVVM_IRelayCommand_System_Int32__0__1__}

```csharp
public void SetValue(IRelayCommand<int, T1, T2> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-3.md)\<[int](https://learn.microsoft.com/dotnet/api/system.int32), T1, T2\>

### SetValue\(IRelayCommand\<long, T1, T2\>\) {#Aspid_MVVM_StarterKit_SliderCommandMonoBinder_2_SetValue_Aspid_MVVM_IRelayCommand_System_Int64__0__1__}

```csharp
public void SetValue(IRelayCommand<long, T1, T2> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-3.md)\<[long](https://learn.microsoft.com/dotnet/api/system.int64), T1, T2\>

### SetValue\(IRelayCommand\<float, T1, T2\>\) {#Aspid_MVVM_StarterKit_SliderCommandMonoBinder_2_SetValue_Aspid_MVVM_IRelayCommand_System_Single__0__1__}

```csharp
public void SetValue(IRelayCommand<float, T1, T2> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-3.md)\<[float](https://learn.microsoft.com/dotnet/api/system.single), T1, T2\>

### SetValue\(IRelayCommand\<double, T1, T2\>\) {#Aspid_MVVM_StarterKit_SliderCommandMonoBinder_2_SetValue_Aspid_MVVM_IRelayCommand_System_Double__0__1__}

```csharp
public void SetValue(IRelayCommand<double, T1, T2> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-3.md)\<[double](https://learn.microsoft.com/dotnet/api/system.double), T1, T2\>

