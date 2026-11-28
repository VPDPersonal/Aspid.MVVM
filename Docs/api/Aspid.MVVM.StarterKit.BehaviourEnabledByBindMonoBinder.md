---
title: "Class BehaviourEnabledByBindMonoBinder"
sidebar_label: "BehaviourEnabledByBindMonoBinder"
description: "Class BehaviourEnabledByBindMonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BehaviourEnabledByBindMonoBinder {#Aspid_MVVM_StarterKit_BehaviourEnabledByBindMonoBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

[`MonoBinder`](Aspid.MVVM.MonoBinder.md) that stays enabled while a binding exists and disables itself otherwise.

```csharp
[BindModeOverride(new BindMode[] { BindMode.OneTime })]
[AddBinderContextMenu(typeof(Behaviour), new string[] { })]
[AddComponentMenu("Aspid/MVVM/Binders/Behaviour/Behaviour Binder – Enabled By Bind")]
public sealed class BehaviourEnabledByBindMonoBinder : MonoBinder, IMonoBinderValidatable, IAnyBinder, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
Object ←
Component ←
Behaviour ←
MonoBehaviour ←
[MonoBinder](Aspid.MVVM.MonoBinder.md) ←
[BehaviourEnabledByBindMonoBinder](Aspid.MVVM.StarterKit.BehaviourEnabledByBindMonoBinder.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md),
[IAnyBinder](Aspid.MVVM.IAnyBinder.md),
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

## Remarks

The bound value is ignored; only the presence of a binding matters.

## Properties

### DefaultMode {#Aspid_MVVM_StarterKit_BehaviourEnabledByBindMonoBinder_DefaultMode}

Gets the initial binding mode. Defaults to [`BindMode.OneWay`](Aspid.MVVM.BindMode.md).

```csharp
protected override BindMode DefaultMode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

#### Remarks

Override if [`BindModeOverrideAttribute`](Aspid.MVVM.BindModeOverrideAttribute.md) excludes [`BindMode.OneWay`](Aspid.MVVM.BindMode.md).

## Methods

### OnBound\(\) {#Aspid_MVVM_StarterKit_BehaviourEnabledByBindMonoBinder_OnBound}

Called with the binding complete and [`MonoBinder.IsBound`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_IsBound) set. Override to notify or send an initial value.

```csharp
protected override void OnBound()
```

#### Remarks

Exceptions do not roll back the binding. This hook may unbind and bind again; finish resource changes before invoking external code.

### OnUnbound\(\) {#Aspid_MVVM_StarterKit_BehaviourEnabledByBindMonoBinder_OnUnbound}

Called after cleanup releases ownership. Override to notify that the binder is unbound.

```csharp
protected override void OnUnbound()
```

#### Remarks

This hook may bind again; finish resource changes before invoking external code.

### SetValue\<T\>\(T\) {#Aspid_MVVM_StarterKit_BehaviourEnabledByBindMonoBinder_SetValue__1___0_}

```csharp
public void SetValue<T>(T value)
```

#### Parameters

`value` T

#### Type Parameters

`T`
