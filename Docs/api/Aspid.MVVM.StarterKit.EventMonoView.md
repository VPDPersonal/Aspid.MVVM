---
title: "Class EventMonoView"
sidebar_label: "EventMonoView"
description: "Class EventMonoView — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class EventMonoView {#Aspid_MVVM_StarterKit_EventMonoView}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`MonoView`](Aspid.MVVM.MonoView.md) that raises [`UnityEvent`](https://docs.unity3d.com/ScriptReference/Events-UnityEvent.html)s when it is initialized and deinitialized.

```csharp
[ShowDesignViewModel]
[AddComponentMenu("Aspid/MVVM/Views/Event View")]
public class EventMonoView : MonoView, IDisposable, IView
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoView](Aspid.MVVM.MonoView.md) ← 
[EventMonoView](Aspid.MVVM.StarterKit.EventMonoView.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable), 
[IView](Aspid.MVVM.IView.md)


#### Extension Methods

[ViewExtensions.DeinitializeView\<EventMonoView\>\(EventMonoView?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DeinitializeView__1___0_), 
[MonoViewExtensions.DestroyView\<EventMonoView\>\(EventMonoView?\)](Aspid.MVVM.MonoViewExtensions.md#Aspid_MVVM_MonoViewExtensions_DestroyView__1___0_), 
[MonoViewExtensions.DestroyView\(IView?\)](Aspid.MVVM.MonoViewExtensions.md#Aspid_MVVM_MonoViewExtensions_DestroyView_Aspid_MVVM_IView_), 
[MonoViewExtensions.DestroyViewAndGameObject\<EventMonoView\>\(EventMonoView?\)](Aspid.MVVM.MonoViewExtensions.md#Aspid_MVVM_MonoViewExtensions_DestroyViewAndGameObject__1___0_), 
[MonoViewExtensions.DestroyViewAndGameObject\(IView?\)](Aspid.MVVM.MonoViewExtensions.md#Aspid_MVVM_MonoViewExtensions_DestroyViewAndGameObject_Aspid_MVVM_IView_), 
[ViewExtensions.DisposeView\<EventMonoView\>\(EventMonoView?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DisposeView__1___0_), 
[ViewExtensions.DisposeView\(IView?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DisposeView_Aspid_MVVM_IView_), 
[ViewExtensions.Reinitialize\(IView?, IViewModel?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_Reinitialize_Aspid_MVVM_IView_Aspid_MVVM_IViewModel_)

## Methods

### DeinitializeInternal\(\) {#Aspid_MVVM_StarterKit_EventMonoView_DeinitializeInternal}

```csharp
protected override void DeinitializeInternal()
```

### InitializeInternal\(IViewModel\) {#Aspid_MVVM_StarterKit_EventMonoView_InitializeInternal_Aspid_MVVM_IViewModel_}

```csharp
protected override void InitializeInternal(IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

