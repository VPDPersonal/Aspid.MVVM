---
title: "Class ValueViewModel<T1, T2>"
sidebar_label: "ValueViewModel<T1, T2>"
description: "Class ValueViewModel<T1, T2> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ValueViewModel\<T1, T2\> {#Aspid_MVVM_StarterKit_ValueViewModel_2}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`IViewModel`](Aspid.MVVM.IViewModel.md) that holds two independent bindable values of types <code class="typeparamref">T1</code> and <code class="typeparamref">T2</code>.

```csharp
[Serializable]
public class ValueViewModel<T1, T2> : IViewModel
```

#### Type Parameters

`T1` 

The type of the value exposed by [`ValueViewModel<T1, T2>.Value1`](Aspid.MVVM.StarterKit.ValueViewModel-2.md#Aspid_MVVM_StarterKit_ValueViewModel_2_Value1).

`T2` 

The type of the value exposed by [`ValueViewModel<T1, T2>.Value2`](Aspid.MVVM.StarterKit.ValueViewModel-2.md#Aspid_MVVM_StarterKit_ValueViewModel_2_Value2).

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ValueViewModel\<T1, T2\>](Aspid.MVVM.StarterKit.ValueViewModel-2.md)

#### Implements

[IViewModel](Aspid.MVVM.IViewModel.md)


#### Extension Methods

[MonoViewModelExtensions.DestroyViewModel\(IViewModel\)](Aspid.MVVM.MonoViewModelExtensions.md#Aspid_MVVM_MonoViewModelExtensions_DestroyViewModel_Aspid_MVVM_IViewModel_), 
[ViewModelExtensions.DisposeViewModel\(IViewModel\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModel_Aspid_MVVM_IViewModel_), 

## Constructors

### ValueViewModel\(bool\) {#Aspid_MVVM_StarterKit_ValueViewModel_2__ctor_System_Boolean_}

```csharp
public ValueViewModel(bool checkEquality = true)
```

#### Parameters

`checkEquality` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, skips change notification if the new value equals the current one.

### ValueViewModel\(\(T1?, T2?\), bool\) {#Aspid_MVVM_StarterKit_ValueViewModel_2__ctor_System_ValueTuple__0__1__System_Boolean_}

```csharp
public ValueViewModel((T1?, T2?) values, bool checkEquality = true)
```

#### Parameters

`values` \(T1?, T2?\)

The tuple containing the initial values for [`ValueViewModel<T1, T2>.Value1`](Aspid.MVVM.StarterKit.ValueViewModel-2.md#Aspid_MVVM_StarterKit_ValueViewModel_2_Value1) and [`ValueViewModel<T1, T2>.Value2`](Aspid.MVVM.StarterKit.ValueViewModel-2.md#Aspid_MVVM_StarterKit_ValueViewModel_2_Value2).

`checkEquality` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, skips change notification if the new value equals the current one.

### ValueViewModel\(T1?, T2?, bool\) {#Aspid_MVVM_StarterKit_ValueViewModel_2__ctor__0__1_System_Boolean_}

```csharp
public ValueViewModel(T1? value1, T2? value2, bool checkEquality = true)
```

#### Parameters

`value1` T1?

The initial value for [`ValueViewModel<T1, T2>.Value1`](Aspid.MVVM.StarterKit.ValueViewModel-2.md#Aspid_MVVM_StarterKit_ValueViewModel_2_Value1).

`value2` T2?

The initial value for [`ValueViewModel<T1, T2>.Value2`](Aspid.MVVM.StarterKit.ValueViewModel-2.md#Aspid_MVVM_StarterKit_ValueViewModel_2_Value2).

`checkEquality` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, skips change notification if the new value equals the current one.

## Properties

### CheckEquality {#Aspid_MVVM_StarterKit_ValueViewModel_2_CheckEquality}

Indicates whether equality checks are performed before raising change notifications.

```csharp
public bool CheckEquality { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### Value1 {#Aspid_MVVM_StarterKit_ValueViewModel_2_Value1}

Gets or sets the first value.

```csharp
public T1? Value1 { get; set; }
```

#### Property Value

 T1?

### Value1Bindable {#Aspid_MVVM_StarterKit_ValueViewModel_2_Value1Bindable}

```csharp
public IBindableMember<T1> Value1Bindable { get; }
```

#### Property Value

 [IBindableMember](Aspid.MVVM.IBindableMember-1.md)\<T1\>

### Value2 {#Aspid_MVVM_StarterKit_ValueViewModel_2_Value2}

Gets or sets the second value.

```csharp
public T2? Value2 { get; set; }
```

#### Property Value

 T2?

### Value2Bindable {#Aspid_MVVM_StarterKit_ValueViewModel_2_Value2Bindable}

```csharp
public IBindableMember<T2> Value2Bindable { get; }
```

#### Property Value

 [IBindableMember](Aspid.MVVM.IBindableMember-1.md)\<T2\>

## Methods

### FindBindableMember\(in FindBindableMemberParameters\) {#Aspid_MVVM_StarterKit_ValueViewModel_2_FindBindableMember_Aspid_MVVM_FindBindableMemberParameters__}

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

### NotifyAll\(\) {#Aspid_MVVM_StarterKit_ValueViewModel_2_NotifyAll}

```csharp
public virtual void NotifyAll()
```

### NotifyCanExecuteChangedAll\(\) {#Aspid_MVVM_StarterKit_ValueViewModel_2_NotifyCanExecuteChangedAll}

```csharp
public virtual void NotifyCanExecuteChangedAll()
```

