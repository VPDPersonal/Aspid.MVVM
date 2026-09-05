---
title: "Class SliderMinMaxBinder"
sidebar_label: "SliderMinMaxBinder"
description: "Class SliderMinMaxBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class SliderMinMaxBinder {#Aspid_MVVM_StarterKit_SliderMinMaxBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`minValue`](https://docs.unity3d.com/ScriptReference/UI-Slider-minValue.html) and
[`maxValue`](https://docs.unity3d.com/ScriptReference/UI-Slider-maxValue.html) as <code>(min, max)</code>.

```csharp
[Serializable]
public class SliderMinMaxBinder : TargetBinder<Slider, Vector2>, IRebindableBinder, IReverseBinder<Vector2>, IVector2Binder, IVectorBinder, IBinder<Vector2>, IBinder<Vector3>, IFloatBinder, INumberBinder, IBinder<int>, IBinder<uint>, IBinder<long>, IBinder<ulong>, IBinder<byte>, IBinder<sbyte>, IBinder<short>, IBinder<ushort>, IBinder<float>, IBinder<double>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<Slider\>](Aspid.MVVM.TargetBinder-1.md) ← 
[TargetBinder\<Slider, Vector2\>](Aspid.MVVM.StarterKit.TargetBinder-2.md) ← 
[SliderMinMaxBinder](Aspid.MVVM.StarterKit.SliderMinMaxBinder.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IReverseBinder\<Vector2\>](Aspid.MVVM.IReverseBinder-1.md), 
[IVector2Binder](Aspid.MVVM.StarterKit.IVector2Binder.md), 
[IVectorBinder](Aspid.MVVM.StarterKit.IVectorBinder.md), 
[IBinder\<Vector2\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<Vector3\>](Aspid.MVVM.IBinder-1.md), 
[IFloatBinder](Aspid.MVVM.StarterKit.IFloatBinder.md), 
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
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<SliderMinMaxBinder\>\(SliderMinMaxBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<SliderMinMaxBinder\>\(SliderMinMaxBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<SliderMinMaxBinder\>\(SliderMinMaxBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

See [`SliderExtensions.SetMinMax`](Aspid.MVVM.StarterKit.SliderExtensions.md) for how the range is validated.

## Constructors

### SliderMinMaxBinder\(\) {#Aspid_MVVM_StarterKit_SliderMinMaxBinder__ctor}

```csharp
protected SliderMinMaxBinder()
```

#### Remarks

For deserialization only.

### SliderMinMaxBinder\(Slider, SliderRangeMode, IConverter\<Vector2, Vector2\>, BindMode\) {#Aspid_MVVM_StarterKit_SliderMinMaxBinder__ctor_UnityEngine_UI_Slider_Aspid_MVVM_StarterKit_SliderRangeMode_Aspid_MVVM_StarterKit_IConverter_UnityEngine_Vector2_UnityEngine_Vector2__Aspid_MVVM_BindMode_}

```csharp
public SliderMinMaxBinder(Slider target, SliderRangeMode rangeMode = SliderRangeMode.Range, IConverter<Vector2, Vector2> converter = null, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` Slider

`rangeMode` [SliderRangeMode](Aspid.MVVM.StarterKit.SliderRangeMode.md)

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<Vector2, Vector2\>

`mode` [BindMode](Aspid.MVVM.BindMode.md)

## Properties

### Property {#Aspid_MVVM_StarterKit_SliderMinMaxBinder_Property}

Gets or sets the bound property of [`TargetBinder<T>.Target`](Aspid.MVVM.TargetBinder-1.md#Aspid_MVVM_TargetBinder_1_Target).

```csharp
protected override sealed Vector2 Property { get; set; }
```

#### Property Value

 Vector2

