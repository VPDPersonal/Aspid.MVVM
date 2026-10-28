---
title: "Class TargetBinder<TTarget>"
sidebar_label: "TargetBinder<TTarget>"
description: "Class TargetBinder<TTarget> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class TargetBinder\<TTarget\> {#Aspid_MVVM_TargetBinder_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

[`Binder`](Aspid.MVVM.Binder.md) that operates on a serialized <code class="typeparamref">TTarget</code>.

```csharp
[Serializable]
public abstract class TargetBinder<TTarget> : Binder, IBinder, IRebindableBinder
```

#### Type Parameters

`TTarget` 

The type of the object the binder operates on.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<TTarget\>](Aspid.MVVM.TargetBinder-1.md)

#### Implements

[IBinder](Aspid.MVVM.IBinder.md), 
[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<TargetBinder\<TTarget\>\>\(TargetBinder\<TTarget\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<TargetBinder\<TTarget\>\>\(TargetBinder\<TTarget\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<TargetBinder\<TTarget\>\>\(TargetBinder\<TTarget\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Constructors

### TargetBinder\(\) {#Aspid_MVVM_TargetBinder_1__ctor}

Creates a binder without a target for Unity deserialization, which then restores the serialized fields.

```csharp
protected TargetBinder()
```

### TargetBinder\(TTarget, BindMode\) {#Aspid_MVVM_TargetBinder_1__ctor__0_Aspid_MVVM_BindMode_}

Creates a binder for <code class="paramref">target</code>.

```csharp
protected TargetBinder(TTarget target, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` TTarget

The object to bind.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">target</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### CanBind {#Aspid_MVVM_TargetBinder_1_CanBind}

Gets a value indicating whether [`TargetBinder<T>.Target`](Aspid.MVVM.TargetBinder-1.md#Aspid_MVVM_TargetBinder_1_Target) is set and, for a Unity object, not destroyed.

```csharp
public override bool CanBind { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### Target {#Aspid_MVVM_TargetBinder_1_Target}

Gets the object the binder operates on.

```csharp
protected TTarget Target { get; }
```

#### Property Value

 TTarget

