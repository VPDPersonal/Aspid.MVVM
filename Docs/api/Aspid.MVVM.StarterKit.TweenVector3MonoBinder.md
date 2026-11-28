---
title: "Class TweenVector3MonoBinder"
sidebar_label: "TweenVector3MonoBinder"
description: "Class TweenVector3MonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class TweenVector3MonoBinder {#Aspid_MVVM_StarterKit_TweenVector3MonoBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

[`TweenMonoBinder<T>`](Aspid.MVVM.StarterKit.TweenMonoBinder-1.md) that eases a [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html).

```csharp
[BindModeOverride(new BindMode[] { BindMode.OneWay, BindMode.OneTime })]
[AddBinderContextMenuByType(typeof(Vector3))]
[AddComponentMenu("Aspid/MVVM/Binders/Tween/Tween Binder – Vector3")]
public sealed class TweenVector3MonoBinder : TweenMonoBinder<Vector3>, IMonoBinderValidatable, IVector3Binder, IVectorBinder, IBinder<Vector2>, IBinder<Vector3>, IFloatBinder, INumberBinder, IBinder<int>, IBinder<uint>, IBinder<long>, IBinder<ulong>, IBinder<byte>, IBinder<sbyte>, IBinder<short>, IBinder<ushort>, IBinder<float>, IBinder<double>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
Object ←
Component ←
Behaviour ←
MonoBehaviour ←
[MonoBinder](Aspid.MVVM.MonoBinder.md) ←
[TweenMonoBinder\<Vector3\>](Aspid.MVVM.StarterKit.TweenMonoBinder-1.md) ←
[TweenVector3MonoBinder](Aspid.MVVM.StarterKit.TweenVector3MonoBinder.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md),
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
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\(IBinder?, in BindContext\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely_Aspid_MVVM_IBinder_Aspid_MVVM_BindContext__),
[BinderLogger.Log\(IBinder, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_Log_Aspid_MVVM_IBinder_System_String_UnityEngine_Object_),
[BinderLogger.LogError\(IBinder, string, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_),
[BinderLogger.LogError\(IBinder, Exception, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_Exception_System_String_UnityEngine_Object_),
[BinderLogger.LogWarning\(IBinder, string, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogWarning_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_),
[BinderMath.NonNegative\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_),
[BinderMath.NonNegative\(IBinder, Vector2, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_),
[BinderMath.NonNegative\(IBinder, Vector3, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, Vector2, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, Vector3, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, Vector4, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector4_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, Rect, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Rect_UnityEngine_Object_),
[BinderMath.SafeClamp\(IBinder, float, float, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_SafeClamp_Aspid_MVVM_IBinder_System_Single_System_Single_System_Single_UnityEngine_Object_),
[BinderMath.SafeClamp01\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_SafeClamp01_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_),
[BinderExtensions.UnbindSafely\(IBinder?, in UnbindContext\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely_Aspid_MVVM_IBinder_Aspid_MVVM_UnbindContext__)

## Methods

### Interpolate\(Vector3, Vector3, float\) {#Aspid_MVVM_StarterKit_TweenVector3MonoBinder_Interpolate_UnityEngine_Vector3_UnityEngine_Vector3_System_Single_}

Interpolates between <code class="paramref">from</code> and <code class="paramref">to</code>.

```csharp
protected override Vector3 Interpolate(Vector3 from, Vector3 to, float progress)
```

#### Parameters

`from` Vector3

The start value.

`to` Vector3

The target value.

`progress` [float](https://learn.microsoft.com/dotnet/api/system.single)

The progress in [0, 1].

#### Returns

 Vector3

The value to forward this frame.
