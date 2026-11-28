---
title: "Class MonoViewMonoBinder<TView>"
sidebar_label: "MonoViewMonoBinder<TView>"
description: "Class MonoViewMonoBinder<TView> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class MonoViewMonoBinder\<TView\> {#Aspid_MVVM_MonoViewMonoBinder_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.Unity.dll <br/>

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that binds a component view to an [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public abstract class MonoViewMonoBinder<TView> : ComponentMonoBinder<TView>, IMonoBinderValidatable, IBinder<IViewModel>, IBinder where TView : Component, IView
```

#### Type Parameters

`TView`

The component view type.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
Object ←
Component ←
Behaviour ←
MonoBehaviour ←
[MonoBinder](Aspid.MVVM.MonoBinder.md) ←
[ComponentMonoBinder\<TView\>](Aspid.MVVM.ComponentMonoBinder-1.md) ←
[MonoViewMonoBinder\<TView\>](Aspid.MVVM.MonoViewMonoBinder-1.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md),
[IBinder\<IViewModel\>](Aspid.MVVM.IBinder-1.md),
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

## Fields

### SetValueMarker {#Aspid_MVVM_MonoViewMonoBinder_1_SetValueMarker}

```csharp
protected static readonly ProfilerMarker SetValueMarker
```

#### Field Value

 ProfilerMarker

## Properties

### IsDebug {#Aspid_MVVM_MonoViewMonoBinder_1_IsDebug}

```csharp
protected bool IsDebug { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### AddLog\(string\) {#Aspid_MVVM_MonoViewMonoBinder_1_AddLog_System_String_}

```csharp
protected void AddLog(string log)
```

#### Parameters

`log` [string](https://learn.microsoft.com/dotnet/api/system.string)

### OnDetach\(\) {#Aspid_MVVM_MonoViewMonoBinder_1_OnDetach}

Called after removal to deinitialize the view.

```csharp
protected override void OnDetach()
```

### SetValue\(IViewModel\) {#Aspid_MVVM_MonoViewMonoBinder_1_SetValue_Aspid_MVVM_IViewModel_}

Deinitializes the live view before applying the new ViewModel.

```csharp
public void SetValue(IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The new ViewModel. A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed ViewModel leaves the view deinitialized.

#### Remarks

A newer value or detachment during deinitialization cancels this update.
