---
title: "Class ScriptableViewMonoBinder<TView>"
sidebar_label: "ScriptableViewMonoBinder<TView>"
description: "Class ScriptableViewMonoBinder<TView> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ScriptableViewMonoBinder\<TView\> {#Aspid_MVVM_ScriptableViewMonoBinder_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.Unity.dll  

Abstract base [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that initializes a serialized [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html) view with the bound [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public abstract class ScriptableViewMonoBinder<TView> : MonoBinder, IMonoBinderValidatable, IRebindableBinder, IBinder<IViewModel>, IBinder where TView : ScriptableObject, IView
```

#### Type Parameters

`TView` 

The type of [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html) that implements [`IView`](Aspid.MVVM.IView.md).

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoBinder](Aspid.MVVM.MonoBinder.md) ← 
[ScriptableViewMonoBinder\<TView\>](Aspid.MVVM.ScriptableViewMonoBinder-1.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<IViewModel\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<ScriptableViewMonoBinder\<TView\>\>\(ScriptableViewMonoBinder\<TView\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<ScriptableViewMonoBinder\<TView\>\>\(ScriptableViewMonoBinder\<TView\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<ScriptableViewMonoBinder\<TView\>\>\(ScriptableViewMonoBinder\<TView\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Fields

### SetValueMarker {#Aspid_MVVM_ScriptableViewMonoBinder_1_SetValueMarker}

```csharp
protected static readonly ProfilerMarker SetValueMarker
```

#### Field Value

 ProfilerMarker

## Properties

### CanBind {#Aspid_MVVM_ScriptableViewMonoBinder_1_CanBind}

Indicates whether binding is allowed: <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a> when no view is assigned.

```csharp
public override bool CanBind { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### IsDebug {#Aspid_MVVM_ScriptableViewMonoBinder_1_IsDebug}

```csharp
protected bool IsDebug { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### AddLog\(string\) {#Aspid_MVVM_ScriptableViewMonoBinder_1_AddLog_System_String_}

```csharp
protected void AddLog(string log)
```

#### Parameters

`log` [string](https://learn.microsoft.com/dotnet/api/system.string)

### DeinitializeView\(\) {#Aspid_MVVM_ScriptableViewMonoBinder_1_DeinitializeView}

Deinitializes the view.

```csharp
protected void DeinitializeView()
```

### InitializeView\(IViewModel\) {#Aspid_MVVM_ScriptableViewMonoBinder_1_InitializeView_Aspid_MVVM_IViewModel_}

Initializes the view with <code class="paramref">viewModel</code>.

```csharp
protected void InitializeView(IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to initialize the view with.

### OnUnbound\(\) {#Aspid_MVVM_ScriptableViewMonoBinder_1_OnUnbound}

Deinitializes the view.

```csharp
protected override void OnUnbound()
```

### SetValue\(IViewModel\) {#Aspid_MVVM_ScriptableViewMonoBinder_1_SetValue_Aspid_MVVM_IViewModel_}

Deinitializes the view, then initializes it with <code class="paramref">viewModel</code> unless it is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public void SetValue(IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel received from the binding, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to deinitialize only.

