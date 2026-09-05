---
title: "Class RectTransformSizeDeltaBinder"
sidebar_label: "RectTransformSizeDeltaBinder"
description: "Class RectTransformSizeDeltaBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RectTransformSizeDeltaBinder {#Aspid_MVVM_StarterKit_RectTransformSizeDeltaBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`TargetBinder<T1, T2>`](Aspid.MVVM.StarterKit.TargetBinder-2.md) that binds [`sizeDelta`](https://docs.unity3d.com/ScriptReference/RectTransform-sizeDelta.html).

```csharp
[Serializable]
public class RectTransformSizeDeltaBinder : TargetBinder<RectTransform, Vector3>, IRebindableBinder, IReverseBinder<Vector3>, IVector3Binder, IVectorBinder, IBinder<Vector2>, IBinder<Vector3>, IFloatBinder, INumberBinder, IBinder<int>, IBinder<uint>, IBinder<long>, IBinder<ulong>, IBinder<byte>, IBinder<sbyte>, IBinder<short>, IBinder<ushort>, IBinder<float>, IBinder<double>, IReverseBinder<Vector2>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<RectTransform\>](Aspid.MVVM.TargetBinder-1.md) ← 
[TargetBinder\<RectTransform, Vector3\>](Aspid.MVVM.StarterKit.TargetBinder-2.md) ← 
[RectTransformSizeDeltaBinder](Aspid.MVVM.StarterKit.RectTransformSizeDeltaBinder.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IReverseBinder\<Vector3\>](Aspid.MVVM.IReverseBinder-1.md), 
[IVector3Binder](Aspid.MVVM.StarterKit.IVector3Binder.md), 
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
[IReverseBinder\<Vector2\>](Aspid.MVVM.IReverseBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<RectTransformSizeDeltaBinder\>\(RectTransformSizeDeltaBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<RectTransformSizeDeltaBinder\>\(RectTransformSizeDeltaBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<RectTransformSizeDeltaBinder\>\(RectTransformSizeDeltaBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

Only a finite value is applied. In [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md) the size is reported both as
[`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) and as [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html), so the ViewModel binds whichever type it holds.

## Constructors

### RectTransformSizeDeltaBinder\(\) {#Aspid_MVVM_StarterKit_RectTransformSizeDeltaBinder__ctor}

```csharp
protected RectTransformSizeDeltaBinder()
```

#### Remarks

For deserialization only.

### RectTransformSizeDeltaBinder\(RectTransform, SizeDeltaMode, IConverter\<Vector3, Vector3\>, BindMode\) {#Aspid_MVVM_StarterKit_RectTransformSizeDeltaBinder__ctor_UnityEngine_RectTransform_Aspid_MVVM_StarterKit_SizeDeltaMode_Aspid_MVVM_StarterKit_IConverter_UnityEngine_Vector3_UnityEngine_Vector3__Aspid_MVVM_BindMode_}

```csharp
public RectTransformSizeDeltaBinder(RectTransform target, SizeDeltaMode sizeMode = SizeDeltaMode.SizeDelta, IConverter<Vector3, Vector3> converter = null, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` RectTransform

`sizeMode` [SizeDeltaMode](Aspid.MVVM.StarterKit.SizeDeltaMode.md)

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<Vector3, Vector3\>

`mode` [BindMode](Aspid.MVVM.BindMode.md)

## Properties

### Property {#Aspid_MVVM_StarterKit_RectTransformSizeDeltaBinder_Property}

Gets or sets the bound property of [`TargetBinder<T>.Target`](Aspid.MVVM.TargetBinder-1.md#Aspid_MVVM_TargetBinder_1_Target).

```csharp
protected override sealed Vector3 Property { get; set; }
```

#### Property Value

 Vector3

## Methods

### OnBound\(\) {#Aspid_MVVM_StarterKit_RectTransformSizeDeltaBinder_OnBound}

Sends the initial property value to the ViewModel in [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

```csharp
protected override void OnBound()
```

#### Remarks

When overriding, always call <code>base.OnBound()</code>. To change what is sent, override [`TargetBinder<T1, T2>.SendInitialValueToSource`](Aspid.MVVM.StarterKit.TargetBinder-2.md#Aspid_MVVM_StarterKit_TargetBinder_2_SendInitialValueToSource) instead.

