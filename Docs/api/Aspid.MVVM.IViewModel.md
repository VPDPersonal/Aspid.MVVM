---
title: "Interface IViewModel"
sidebar_label: "IViewModel"
description: "Interface IViewModel — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IViewModel {#Aspid_MVVM_IViewModel}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Defines a ViewModel whose bindable members a View looks up by identifier.

```csharp
public interface IViewModel
```

#### Extension Methods

[MonoViewModelExtensions.DestroyViewModel\(IViewModel\)](Aspid.MVVM.MonoViewModelExtensions.md#Aspid_MVVM_MonoViewModelExtensions_DestroyViewModel_Aspid_MVVM_IViewModel_), 
[ViewModelExtensions.DisposeViewModel\(IViewModel\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModel_Aspid_MVVM_IViewModel_), 

## Methods

### FindBindableMember\(in FindBindableMemberParameters\) {#Aspid_MVVM_IViewModel_FindBindableMember_Aspid_MVVM_FindBindableMemberParameters__}

Finds the bindable member with the identifier in <code class="paramref">parameters</code>.

```csharp
FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters)
```

#### Parameters

`parameters` [FindBindableMemberParameters](Aspid.MVVM.FindBindableMemberParameters.md)

The lookup query.

#### Returns

 [FindBindableMemberResult](Aspid.MVVM.FindBindableMemberResult.md)

A result holding the member; a result whose [`FindBindableMemberResult.IsFound`](Aspid.MVVM.FindBindableMemberResult.md#Aspid_MVVM_FindBindableMemberResult_IsFound) is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>
when no member has the identifier.

