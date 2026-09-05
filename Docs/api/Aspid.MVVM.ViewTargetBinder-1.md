---
title: "Class ViewTargetBinder<TView>"
sidebar_label: "ViewTargetBinder<TView>"
description: "Class ViewTargetBinder<TView> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ViewTargetBinder\<TView\> {#Aspid_MVVM_ViewTargetBinder_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.Unity.dll  

Abstract base [`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that initializes the target view with the bound [`IViewModel`](Aspid.MVVM.IViewModel.md)
and deinitializes it on unbind.

```csharp
public abstract class ViewTargetBinder<TView> : TargetBinder<TView>, IRebindableBinder, IBinder<IViewModel>, IBinder where TView : Object, IView
```

#### Type Parameters

`TView` 

The type of [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) that implements [`IView`](Aspid.MVVM.IView.md).

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<TView\>](Aspid.MVVM.TargetBinder-1.md) ← 
[ViewTargetBinder\<TView\>](Aspid.MVVM.ViewTargetBinder-1.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<IViewModel\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<ViewTargetBinder\<TView\>\>\(ViewTargetBinder\<TView\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<ViewTargetBinder\<TView\>\>\(ViewTargetBinder\<TView\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<ViewTargetBinder\<TView\>\>\(ViewTargetBinder\<TView\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Constructors

### ViewTargetBinder\(TView, BindMode\) {#Aspid_MVVM_ViewTargetBinder_1__ctor__0_Aspid_MVVM_BindMode_}

```csharp
public ViewTargetBinder(TView target, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` TView

The view to bind.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode. Must be [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">target</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Thrown when <code class="paramref">mode</code> is not [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

## Methods

### DeinitializeView\(\) {#Aspid_MVVM_ViewTargetBinder_1_DeinitializeView}

Deinitializes the target view.

```csharp
protected void DeinitializeView()
```

### InitializeView\(IViewModel\) {#Aspid_MVVM_ViewTargetBinder_1_InitializeView_Aspid_MVVM_IViewModel_}

Initializes the target view with <code class="paramref">viewModel</code>.

```csharp
protected void InitializeView(IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to initialize the view with.

### OnUnbound\(\) {#Aspid_MVVM_ViewTargetBinder_1_OnUnbound}

Deinitializes the view.

```csharp
protected override void OnUnbound()
```

### SetValue\(IViewModel\) {#Aspid_MVVM_ViewTargetBinder_1_SetValue_Aspid_MVVM_IViewModel_}

Deinitializes the view, then initializes it with <code class="paramref">viewModel</code> unless it is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public void SetValue(IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel received from the binding, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to deinitialize only.

