---
title: "Class ValueViewModel<T>"
sidebar_label: "ValueViewModel<T>"
description: "Class ValueViewModel<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ValueViewModel\<T\> {#Aspid_MVVM_StarterKit_ValueViewModel_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`IViewModel`](Aspid.MVVM.IViewModel.md) that holds a single bindable value of type <code class="typeparamref">T</code>.

```csharp
[Serializable]
public class ValueViewModel<T> : IViewModel
```

#### Type Parameters

`T` 

The type of the value exposed by [`ValueViewModel<T>.Value`](Aspid.MVVM.StarterKit.ValueViewModel-1.md#Aspid_MVVM_StarterKit_ValueViewModel_1_Value).

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ValueViewModel\<T\>](Aspid.MVVM.StarterKit.ValueViewModel-1.md)

#### Implements

[IViewModel](Aspid.MVVM.IViewModel.md)


#### Extension Methods

[MonoViewModelExtensions.DestroyViewModel\(IViewModel\)](Aspid.MVVM.MonoViewModelExtensions.md#Aspid_MVVM_MonoViewModelExtensions_DestroyViewModel_Aspid_MVVM_IViewModel_), 
[ViewModelExtensions.DisposeViewModel\(IViewModel\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModel_Aspid_MVVM_IViewModel_), 

## Constructors

### ValueViewModel\(bool\) {#Aspid_MVVM_StarterKit_ValueViewModel_1__ctor_System_Boolean_}

```csharp
public ValueViewModel(bool checkEquality = true)
```

#### Parameters

`checkEquality` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, skips change notification if the new value equals the current one.

### ValueViewModel\(T?, bool\) {#Aspid_MVVM_StarterKit_ValueViewModel_1__ctor__0_System_Boolean_}

```csharp
public ValueViewModel(T? value, bool checkEquality = true)
```

#### Parameters

`value` T?

The initial value.

`checkEquality` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, skips change notification if the new value equals the current one.

## Properties

### CheckEquality {#Aspid_MVVM_StarterKit_ValueViewModel_1_CheckEquality}

Indicates whether equality checks are performed before raising change notifications.

```csharp
public bool CheckEquality { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### Value {#Aspid_MVVM_StarterKit_ValueViewModel_1_Value}

Gets or sets the value.

```csharp
public T? Value { get; set; }
```

#### Property Value

 T?

### ValueBindable {#Aspid_MVVM_StarterKit_ValueViewModel_1_ValueBindable}

```csharp
public IBindableMember<T> ValueBindable { get; }
```

#### Property Value

 [IBindableMember](Aspid.MVVM.IBindableMember-1.md)\<T\>

## Methods

### FindBindableMember\(in FindBindableMemberParameters\) {#Aspid_MVVM_StarterKit_ValueViewModel_1_FindBindableMember_Aspid_MVVM_FindBindableMemberParameters__}

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

### NotifyAll\(\) {#Aspid_MVVM_StarterKit_ValueViewModel_1_NotifyAll}

```csharp
public virtual void NotifyAll()
```

### NotifyCanExecuteChangedAll\(\) {#Aspid_MVVM_StarterKit_ValueViewModel_1_NotifyCanExecuteChangedAll}

```csharp
public virtual void NotifyCanExecuteChangedAll()
```

