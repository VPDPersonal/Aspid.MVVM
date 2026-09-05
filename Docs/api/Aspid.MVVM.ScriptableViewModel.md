---
title: "Class ScriptableViewModel"
sidebar_label: "ScriptableViewModel"
description: "Class ScriptableViewModel — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ScriptableViewModel {#Aspid_MVVM_ScriptableViewModel}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.Unity.dll  

Represents a base class for ViewModels in a Unity context derived from [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html).

```csharp
public abstract class ScriptableViewModel : ScriptableObject, IViewModel
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
ScriptableObject ← 
[ScriptableViewModel](Aspid.MVVM.ScriptableViewModel.md)

#### Implements

[IViewModel](Aspid.MVVM.IViewModel.md)


#### Extension Methods

[MonoViewModelExtensions.DestroyViewModel\(IViewModel\)](Aspid.MVVM.MonoViewModelExtensions.md#Aspid_MVVM_MonoViewModelExtensions_DestroyViewModel_Aspid_MVVM_IViewModel_), 
[ViewModelExtensions.DisposeViewModel\(IViewModel\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModel_Aspid_MVVM_IViewModel_), 

## Methods

### FindBindableMember\(in FindBindableMemberParameters\) {#Aspid_MVVM_ScriptableViewModel_FindBindableMember_Aspid_MVVM_FindBindableMemberParameters__}

Searches for a bindable member in the ViewModel based on the provided parameters.

```csharp
public virtual FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters)
```

#### Parameters

`parameters` [FindBindableMemberParameters](Aspid.MVVM.FindBindableMemberParameters.md)

The parameters specifying the bindable member to find.

#### Returns

 [FindBindableMemberResult](Aspid.MVVM.FindBindableMemberResult.md)

A [`FindBindableMemberResult`](Aspid.MVVM.FindBindableMemberResult.md) that contains information about the bindable member search result.

### NotifyAll\(\) {#Aspid_MVVM_ScriptableViewModel_NotifyAll}

```csharp
public virtual void NotifyAll()
```

### NotifyCanExecuteChangedAll\(\) {#Aspid_MVVM_ScriptableViewModel_NotifyCanExecuteChangedAll}

```csharp
public virtual void NotifyCanExecuteChangedAll()
```

### OnValidate\(\) {#Aspid_MVVM_ScriptableViewModel_OnValidate}

```csharp
protected virtual void OnValidate()
```

