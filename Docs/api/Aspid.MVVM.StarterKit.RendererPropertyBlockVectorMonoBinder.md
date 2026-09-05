---
title: "Class RendererPropertyBlockVectorMonoBinder"
sidebar_label: "RendererPropertyBlockVectorMonoBinder"
description: "Class RendererPropertyBlockVectorMonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RendererPropertyBlockVectorMonoBinder {#Aspid_MVVM_StarterKit_RendererPropertyBlockVectorMonoBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`RendererPropertyBlockMonoBinder<T>`](Aspid.MVVM.StarterKit.RendererPropertyBlockMonoBinder-1.md) that writes a vector shader property, also from
[`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html) and [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html).

```csharp
[AddBinderContextMenu(typeof(Renderer), new string[] { })]
[AddComponentMenu("Aspid/MVVM/Binders/Renderer/PropertyBlock Binder – Vector")]
public class RendererPropertyBlockVectorMonoBinder : RendererPropertyBlockMonoBinder<Vector4>, IMonoBinderValidatable, IRebindableBinder, IBinder<Vector4>, IVectorBinder, IBinder<Vector2>, IBinder<Vector3>, IFloatBinder, INumberBinder, IBinder<int>, IBinder<uint>, IBinder<long>, IBinder<ulong>, IBinder<byte>, IBinder<sbyte>, IBinder<short>, IBinder<ushort>, IBinder<float>, IBinder<double>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoBinder](Aspid.MVVM.MonoBinder.md) ← 
[ComponentMonoBinder\<Renderer\>](Aspid.MVVM.ComponentMonoBinder-1.md) ← 
[RendererPropertyBlockMonoBinder\<Vector4\>](Aspid.MVVM.StarterKit.RendererPropertyBlockMonoBinder-1.md) ← 
[RendererPropertyBlockVectorMonoBinder](Aspid.MVVM.StarterKit.RendererPropertyBlockVectorMonoBinder.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<Vector4\>](Aspid.MVVM.IBinder-1.md), 
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

[BinderExtensions.BindSafely\<RendererPropertyBlockVectorMonoBinder\>\(RendererPropertyBlockVectorMonoBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<RendererPropertyBlockVectorMonoBinder\>\(RendererPropertyBlockVectorMonoBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<RendererPropertyBlockVectorMonoBinder\>\(RendererPropertyBlockVectorMonoBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

Missing components are written as zero.

## Methods

### SetValue\(Vector2\) {#Aspid_MVVM_StarterKit_RendererPropertyBlockVectorMonoBinder_SetValue_UnityEngine_Vector2_}

Writes <code class="paramref">value</code> with Z and W set to zero.

```csharp
public void SetValue(Vector2 value)
```

#### Parameters

`value` Vector2

The value received from the ViewModel.

### SetValue\(Vector3\) {#Aspid_MVVM_StarterKit_RendererPropertyBlockVectorMonoBinder_SetValue_UnityEngine_Vector3_}

Writes <code class="paramref">value</code> with W set to zero.

```csharp
public void SetValue(Vector3 value)
```

#### Parameters

`value` Vector3

The value received from the ViewModel.

### Write\(Vector4\) {#Aspid_MVVM_StarterKit_RendererPropertyBlockVectorMonoBinder_Write_UnityEngine_Vector4_}

Writes <code class="paramref">value</code> into [`RendererPropertyBlockMonoBinder<T>.Block`](Aspid.MVVM.StarterKit.RendererPropertyBlockMonoBinder-1.md#Aspid_MVVM_StarterKit_RendererPropertyBlockMonoBinder_1_Block) under [`RendererPropertyBlockMonoBinder<T>.PropertyId`](Aspid.MVVM.StarterKit.RendererPropertyBlockMonoBinder-1.md#Aspid_MVVM_StarterKit_RendererPropertyBlockMonoBinder_1_PropertyId).

```csharp
protected override void Write(Vector4 value)
```

#### Parameters

`value` Vector4

The value to write.

