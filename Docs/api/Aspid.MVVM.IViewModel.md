---
title: "Interface IViewModel"
sidebar_label: "IViewModel"
description: "Interface IViewModel — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IViewModel {#Aspid_MVVM_IViewModel}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.dll <br/>

Defines a ViewModel whose bindable members a View looks up by identifier.

```csharp
public interface IViewModel
```

#### Extension Methods

[MonoViewModelExtensions.DestroyViewModel\(IViewModel\)](Aspid.MVVM.MonoViewModelExtensions.md#Aspid_MVVM_MonoViewModelExtensions_DestroyViewModel_Aspid_MVVM_IViewModel_),
[MonoViewModelExtensions.DestroyViewModelAndGameObject\(IViewModel\)](Aspid.MVVM.MonoViewModelExtensions.md#Aspid_MVVM_MonoViewModelExtensions_DestroyViewModelAndGameObject_Aspid_MVVM_IViewModel_),
[MonoViewModelExtensions.DestroyViewModelAndGameObjectSafely\(IViewModel?\)](Aspid.MVVM.MonoViewModelExtensions.md#Aspid_MVVM_MonoViewModelExtensions_DestroyViewModelAndGameObjectSafely_Aspid_MVVM_IViewModel_),
[MonoViewModelExtensions.DestroyViewModelSafely\(IViewModel?\)](Aspid.MVVM.MonoViewModelExtensions.md#Aspid_MVVM_MonoViewModelExtensions_DestroyViewModelSafely_Aspid_MVVM_IViewModel_),
[ViewModelExtensions.DisposeViewModel\(IViewModel\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModel_Aspid_MVVM_IViewModel_),
[ViewModelExtensions.DisposeViewModelSafely\(IViewModel?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModelSafely_Aspid_MVVM_IViewModel_),
[ViewModelExtensions.InitializeView\(IViewModel, IView\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeView_Aspid_MVVM_IViewModel_Aspid_MVVM_IView_),
[ViewModelExtensions.InitializeViewSafely\(IViewModel?, IView?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViewSafely_Aspid_MVVM_IViewModel_Aspid_MVVM_IView_),
[ViewModelExtensions.InitializeViews\(IViewModel, params IView\[\]\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViews_Aspid_MVVM_IViewModel_Aspid_MVVM_IView___),
[ViewModelExtensions.InitializeViews\<T\>\(IViewModel, List\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViews__1_Aspid_MVVM_IViewModel_System_Collections_Generic_List___0__),
[ViewModelExtensions.InitializeViews\(IViewModel, IEnumerable\<IView\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViews_Aspid_MVVM_IViewModel_System_Collections_Generic_IEnumerable_Aspid_MVVM_IView__),
[ViewModelExtensions.InitializeViewsSafely\(IViewModel?, params IView?\[\]?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViewsSafely_Aspid_MVVM_IViewModel_Aspid_MVVM_IView___),
[ViewModelExtensions.InitializeViewsSafely\<T\>\(IViewModel?, List\<T\>?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_Collections_Generic_List___0__),
[ViewModelExtensions.InitializeViewsSafely\(IViewModel?, IEnumerable\<IView?\>?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViewsSafely_Aspid_MVVM_IViewModel_System_Collections_Generic_IEnumerable_Aspid_MVVM_IView__)

## Methods

### FindBindableMember\(in FindBindableMemberParameters\) {#Aspid_MVVM_IViewModel_FindBindableMember_Aspid_MVVM_FindBindableMemberParameters__}

Finds the bindable member with the identifier in <code class="paramref">parameters</code>.

```csharp
FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters)
```

#### Parameters

`parameters` [FindBindableMemberParameters](Aspid.MVVM.FindBindableMemberParameters.md)

The lookup query with a non-null identifier that is not empty or whitespace.

#### Returns

 [FindBindableMemberResult](Aspid.MVVM.FindBindableMemberResult.md)

The lookup result; [`FindBindableMemberResult.IsFound`](Aspid.MVVM.FindBindableMemberResult.md#Aspid_MVVM_FindBindableMemberResult_IsFound) indicates whether it contains a member.
