---
title: "Class ViewBinder"
sidebar_label: "ViewBinder"
description: "Class ViewBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ViewBinder {#Aspid_MVVM_ViewBinder}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

[`Binder`](Aspid.MVVM.Binder.md) that initializes an [`IView`](Aspid.MVVM.IView.md) with each ViewModel it receives.

```csharp
public class ViewBinder : Binder, IRebindableBinder, IBinder<IViewModel?>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[ViewBinder](Aspid.MVVM.ViewBinder.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<IViewModel?\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<ViewBinder\>\(ViewBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<ViewBinder\>\(ViewBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<ViewBinder\>\(ViewBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Constructors

### ViewBinder\(IView\) {#Aspid_MVVM_ViewBinder__ctor_Aspid_MVVM_IView_}

Creates a binder for <code class="paramref">view</code>.

```csharp
public ViewBinder(IView view)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)

The view to bind.

## Fields

### View {#Aspid_MVVM_ViewBinder_View}

The view the binder initializes.

```csharp
protected readonly IView View
```

#### Field Value

 [IView](Aspid.MVVM.IView.md)

## Methods

### DeinitializeView\(\) {#Aspid_MVVM_ViewBinder_DeinitializeView}

Deinitializes [`ViewBinder.View`](Aspid.MVVM.ViewBinder.md#Aspid_MVVM_ViewBinder_View).

```csharp
protected void DeinitializeView()
```

### InitializeView\(IViewModel\) {#Aspid_MVVM_ViewBinder_InitializeView_Aspid_MVVM_IViewModel_}

Initializes [`ViewBinder.View`](Aspid.MVVM.ViewBinder.md#Aspid_MVVM_ViewBinder_View) with <code class="paramref">viewModel</code>.

```csharp
protected void InitializeView(IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to initialize the view with.

### OnUnbound\(\) {#Aspid_MVVM_ViewBinder_OnUnbound}

Called after the binder detaches to deinitialize [`ViewBinder.View`](Aspid.MVVM.ViewBinder.md#Aspid_MVVM_ViewBinder_View).

```csharp
protected override void OnUnbound()
```

### SetValue\(IViewModel?\) {#Aspid_MVVM_ViewBinder_SetValue_Aspid_MVVM_IViewModel_}

Deinitializes [`ViewBinder.View`](Aspid.MVVM.ViewBinder.md#Aspid_MVVM_ViewBinder_View) and initializes it with <code class="paramref">viewModel</code>.

```csharp
public void SetValue(IViewModel? viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel received from the bindable member; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> leaves the view deinitialized.

