---
title: "Class ColliderExcludeLayersMonoBinder"
sidebar_label: "ColliderExcludeLayersMonoBinder"
description: "Class ColliderExcludeLayersMonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ColliderExcludeLayersMonoBinder {#Aspid_MVVM_StarterKit_ColliderExcludeLayersMonoBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ComponentIntMonoBinder<T>`](Aspid.MVVM.StarterKit.ComponentIntMonoBinder-1.md) that binds [`excludeLayers`](https://docs.unity3d.com/ScriptReference/Collider-excludeLayers.html).

```csharp
[AddBinderContextMenu(typeof(Collider), new string[] { "m_ExcludeLayers" })]
[AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – Exclude Layers")]
public class ColliderExcludeLayersMonoBinder : ComponentIntMonoBinder<Collider>, IMonoBinderValidatable, IRebindableBinder, IIntBinder, INumberBinder, IBinder<int>, IBinder<uint>, IBinder<long>, IBinder<ulong>, IBinder<byte>, IBinder<sbyte>, IBinder<short>, IBinder<ushort>, IBinder<float>, IBinder<double>, INumberReverseBinder, IReverseBinder<int>, IReverseBinder<long>, IReverseBinder<float>, IReverseBinder<double>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoBinder](Aspid.MVVM.MonoBinder.md) ← 
[ComponentMonoBinder\<Collider\>](Aspid.MVVM.ComponentMonoBinder-1.md) ← 
[ComponentMonoBinder\<Collider, int\>](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) ← 
[ComponentIntMonoBinder\<Collider\>](Aspid.MVVM.StarterKit.ComponentIntMonoBinder-1.md) ← 
[ColliderExcludeLayersMonoBinder](Aspid.MVVM.StarterKit.ColliderExcludeLayersMonoBinder.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IIntBinder](Aspid.MVVM.StarterKit.IIntBinder.md), 
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
[INumberReverseBinder](Aspid.MVVM.StarterKit.INumberReverseBinder.md), 
[IReverseBinder\<int\>](Aspid.MVVM.IReverseBinder-1.md), 
[IReverseBinder\<long\>](Aspid.MVVM.IReverseBinder-1.md), 
[IReverseBinder\<float\>](Aspid.MVVM.IReverseBinder-1.md), 
[IReverseBinder\<double\>](Aspid.MVVM.IReverseBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<ColliderExcludeLayersMonoBinder\>\(ColliderExcludeLayersMonoBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<ColliderExcludeLayersMonoBinder\>\(ColliderExcludeLayersMonoBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<ColliderExcludeLayersMonoBinder\>\(ColliderExcludeLayersMonoBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

The mask travels as an <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">int</a>, which [`LayerMask`](https://docs.unity3d.com/ScriptReference/LayerMask.html) converts to and from.

## Properties

### Property {#Aspid_MVVM_StarterKit_ColliderExcludeLayersMonoBinder_Property}

Gets or sets the bound property.

```csharp
protected override sealed int Property { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

