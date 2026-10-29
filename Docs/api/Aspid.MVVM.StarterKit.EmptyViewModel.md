---
title: "Class EmptyViewModel"
sidebar_label: "EmptyViewModel"
description: "Class EmptyViewModel — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class EmptyViewModel {#Aspid_MVVM_StarterKit_EmptyViewModel}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`IViewModel`](Aspid.MVVM.IViewModel.md) with no bindable members: every lookup fails, so bound binders stay at their defaults.

```csharp
public sealed class EmptyViewModel : IViewModel
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EmptyViewModel](Aspid.MVVM.StarterKit.EmptyViewModel.md)

#### Implements

[IViewModel](Aspid.MVVM.IViewModel.md)


#### Extension Methods

[MonoViewModelExtensions.DestroyViewModel\(IViewModel\)](Aspid.MVVM.MonoViewModelExtensions.md#Aspid_MVVM_MonoViewModelExtensions_DestroyViewModel_Aspid_MVVM_IViewModel_), 
[ViewModelExtensions.DisposeViewModel\(IViewModel\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModel_Aspid_MVVM_IViewModel_), 

## Methods

### FindBindableMember\(in FindBindableMemberParameters\) {#Aspid_MVVM_StarterKit_EmptyViewModel_FindBindableMember_Aspid_MVVM_FindBindableMemberParameters__}

Finds the bindable member with the identifier in <code class="paramref">parameters</code>.

```csharp
public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters)
```

#### Parameters

`parameters` [FindBindableMemberParameters](Aspid.MVVM.FindBindableMemberParameters.md)

The lookup query.

#### Returns

 [FindBindableMemberResult](Aspid.MVVM.FindBindableMemberResult.md)

A result holding the member; a result whose [`FindBindableMemberResult.IsFound`](Aspid.MVVM.FindBindableMemberResult.md#Aspid_MVVM_FindBindableMemberResult_IsFound) is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>
when no member has the identifier.

