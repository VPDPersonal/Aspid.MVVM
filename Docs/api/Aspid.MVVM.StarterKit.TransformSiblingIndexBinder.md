---
title: "Class TransformSiblingIndexBinder"
sidebar_label: "TransformSiblingIndexBinder"
description: "Class TransformSiblingIndexBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class TransformSiblingIndexBinder {#Aspid_MVVM_StarterKit_TransformSiblingIndexBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that binds the sibling index of a [`Transform`](https://docs.unity3d.com/ScriptReference/Transform.html).

```csharp
[Serializable]
public class TransformSiblingIndexBinder : TargetBinder<Transform>, IRebindableBinder, IBinder<int>, IReverseBinder<int>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<Transform\>](Aspid.MVVM.TargetBinder-1.md) ← 
[TransformSiblingIndexBinder](Aspid.MVVM.StarterKit.TransformSiblingIndexBinder.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<int\>](Aspid.MVVM.IBinder-1.md), 
[IReverseBinder\<int\>](Aspid.MVVM.IReverseBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<TransformSiblingIndexBinder\>\(TransformSiblingIndexBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<TransformSiblingIndexBinder\>\(TransformSiblingIndexBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<TransformSiblingIndexBinder\>\(TransformSiblingIndexBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

The index is clamped to the existing siblings, so [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md) reports where
the object actually is.

## Constructors

### TransformSiblingIndexBinder\(\) {#Aspid_MVVM_StarterKit_TransformSiblingIndexBinder__ctor}

```csharp
protected TransformSiblingIndexBinder()
```

#### Remarks

For deserialization only.

### TransformSiblingIndexBinder\(Transform, BindMode\) {#Aspid_MVVM_StarterKit_TransformSiblingIndexBinder__ctor_UnityEngine_Transform_Aspid_MVVM_BindMode_}

Creates a binder for <code class="paramref">target</code>.

```csharp
public TransformSiblingIndexBinder(Transform target, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` Transform

The object to bind.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">target</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Methods

### OnBound\(\) {#Aspid_MVVM_StarterKit_TransformSiblingIndexBinder_OnBound}

Called after the binder attaches and receives the first value. Override to subscribe to the component.

```csharp
protected override void OnBound()
```

### SetValue\(int\) {#Aspid_MVVM_StarterKit_TransformSiblingIndexBinder_SetValue_System_Int32_}

Applies <code class="paramref">value</code> to the View.

```csharp
public void SetValue(int value)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The value received from the ViewModel.

### ValueChanged {#Aspid_MVVM_StarterKit_TransformSiblingIndexBinder_ValueChanged}

Occurs when the View produces a value for the ViewModel.

```csharp
public event Action<int> ValueChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[int](https://learn.microsoft.com/dotnet/api/system.int32)\>

