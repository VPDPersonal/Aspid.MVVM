---
title: "Class ValueViewModel<T1, T2, T3, T4>"
sidebar_label: "ValueViewModel<T1, T2, T3, T4>"
description: "Class ValueViewModel<T1, T2, T3, T4> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ValueViewModel\<T1, T2, T3, T4\> {#Aspid_MVVM_StarterKit_ValueViewModel_4}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`IViewModel`](Aspid.MVVM.IViewModel.md) that holds four independent bindable values of types <code class="typeparamref">T1</code>, <code class="typeparamref">T2</code>, <code class="typeparamref">T3</code>, and <code class="typeparamref">T4</code>.

```csharp
[Serializable]
public class ValueViewModel<T1, T2, T3, T4> : IViewModel
```

#### Type Parameters

`T1` 

The type of the value exposed by [`ValueViewModel<T1, T2, T3, T4>.Value1`](Aspid.MVVM.StarterKit.ValueViewModel-4.md#Aspid_MVVM_StarterKit_ValueViewModel_4_Value1).

`T2` 

The type of the value exposed by [`ValueViewModel<T1, T2, T3, T4>.Value2`](Aspid.MVVM.StarterKit.ValueViewModel-4.md#Aspid_MVVM_StarterKit_ValueViewModel_4_Value2).

`T3` 

The type of the value exposed by [`ValueViewModel<T1, T2, T3, T4>.Value3`](Aspid.MVVM.StarterKit.ValueViewModel-4.md#Aspid_MVVM_StarterKit_ValueViewModel_4_Value3).

`T4` 

The type of the value exposed by [`ValueViewModel<T1, T2, T3, T4>.Value4`](Aspid.MVVM.StarterKit.ValueViewModel-4.md#Aspid_MVVM_StarterKit_ValueViewModel_4_Value4).

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ValueViewModel\<T1, T2, T3, T4\>](Aspid.MVVM.StarterKit.ValueViewModel-4.md)

#### Implements

[IViewModel](Aspid.MVVM.IViewModel.md)


#### Extension Methods

[MonoViewModelExtensions.DestroyViewModel\(IViewModel\)](Aspid.MVVM.MonoViewModelExtensions.md#Aspid_MVVM_MonoViewModelExtensions_DestroyViewModel_Aspid_MVVM_IViewModel_), 
[ViewModelExtensions.DisposeViewModel\(IViewModel\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModel_Aspid_MVVM_IViewModel_), 

## Constructors

### ValueViewModel\(bool\) {#Aspid_MVVM_StarterKit_ValueViewModel_4__ctor_System_Boolean_}

```csharp
public ValueViewModel(bool checkEquality = true)
```

#### Parameters

`checkEquality` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, skips change notification if the new value equals the current one.

### ValueViewModel\(\(T1?, T2?, T3?, T4?\), bool\) {#Aspid_MVVM_StarterKit_ValueViewModel_4__ctor_System_ValueTuple__0__1__2__3__System_Boolean_}

```csharp
public ValueViewModel((T1?, T2?, T3?, T4?) values, bool checkEquality = true)
```

#### Parameters

`values` \(T1?, T2?, T3?, T4?\)

The tuple containing the initial values for [`ValueViewModel<T1, T2, T3, T4>.Value1`](Aspid.MVVM.StarterKit.ValueViewModel-4.md#Aspid_MVVM_StarterKit_ValueViewModel_4_Value1), [`ValueViewModel<T1, T2, T3, T4>.Value2`](Aspid.MVVM.StarterKit.ValueViewModel-4.md#Aspid_MVVM_StarterKit_ValueViewModel_4_Value2), [`ValueViewModel<T1, T2, T3, T4>.Value3`](Aspid.MVVM.StarterKit.ValueViewModel-4.md#Aspid_MVVM_StarterKit_ValueViewModel_4_Value3), and [`ValueViewModel<T1, T2, T3, T4>.Value4`](Aspid.MVVM.StarterKit.ValueViewModel-4.md#Aspid_MVVM_StarterKit_ValueViewModel_4_Value4).

`checkEquality` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, skips change notification if the new value equals the current one.

### ValueViewModel\(T1?, T2?, T3?, T4?, bool\) {#Aspid_MVVM_StarterKit_ValueViewModel_4__ctor__0__1__2__3_System_Boolean_}

```csharp
public ValueViewModel(T1? value1, T2? value2, T3? value3, T4? value4, bool checkEquality = true)
```

#### Parameters

`value1` T1?

The initial value for [`ValueViewModel<T1, T2, T3, T4>.Value1`](Aspid.MVVM.StarterKit.ValueViewModel-4.md#Aspid_MVVM_StarterKit_ValueViewModel_4_Value1).

`value2` T2?

The initial value for [`ValueViewModel<T1, T2, T3, T4>.Value2`](Aspid.MVVM.StarterKit.ValueViewModel-4.md#Aspid_MVVM_StarterKit_ValueViewModel_4_Value2).

`value3` T3?

The initial value for [`ValueViewModel<T1, T2, T3, T4>.Value3`](Aspid.MVVM.StarterKit.ValueViewModel-4.md#Aspid_MVVM_StarterKit_ValueViewModel_4_Value3).

`value4` T4?

The initial value for [`ValueViewModel<T1, T2, T3, T4>.Value4`](Aspid.MVVM.StarterKit.ValueViewModel-4.md#Aspid_MVVM_StarterKit_ValueViewModel_4_Value4).

`checkEquality` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, skips change notification if the new value equals the current one.

## Properties

### CheckEquality {#Aspid_MVVM_StarterKit_ValueViewModel_4_CheckEquality}

Indicates whether equality checks are performed before raising change notifications.

```csharp
public bool CheckEquality { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### Value1 {#Aspid_MVVM_StarterKit_ValueViewModel_4_Value1}

Gets or sets the first value.

```csharp
public T1? Value1 { get; set; }
```

#### Property Value

 T1?

### Value1Bindable {#Aspid_MVVM_StarterKit_ValueViewModel_4_Value1Bindable}

```csharp
public IBindableMember<T1> Value1Bindable { get; }
```

#### Property Value

 [IBindableMember](Aspid.MVVM.IBindableMember-1.md)\<T1\>

### Value2 {#Aspid_MVVM_StarterKit_ValueViewModel_4_Value2}

Gets or sets the second value.

```csharp
public T2? Value2 { get; set; }
```

#### Property Value

 T2?

### Value2Bindable {#Aspid_MVVM_StarterKit_ValueViewModel_4_Value2Bindable}

```csharp
public IBindableMember<T2> Value2Bindable { get; }
```

#### Property Value

 [IBindableMember](Aspid.MVVM.IBindableMember-1.md)\<T2\>

### Value3 {#Aspid_MVVM_StarterKit_ValueViewModel_4_Value3}

Gets or sets the third value.

```csharp
public T3? Value3 { get; set; }
```

#### Property Value

 T3?

### Value3Bindable {#Aspid_MVVM_StarterKit_ValueViewModel_4_Value3Bindable}

```csharp
public IBindableMember<T3> Value3Bindable { get; }
```

#### Property Value

 [IBindableMember](Aspid.MVVM.IBindableMember-1.md)\<T3\>

### Value4 {#Aspid_MVVM_StarterKit_ValueViewModel_4_Value4}

Gets or sets the fourth value.

```csharp
public T4? Value4 { get; set; }
```

#### Property Value

 T4?

### Value4Bindable {#Aspid_MVVM_StarterKit_ValueViewModel_4_Value4Bindable}

```csharp
public IBindableMember<T4> Value4Bindable { get; }
```

#### Property Value

 [IBindableMember](Aspid.MVVM.IBindableMember-1.md)\<T4\>

## Methods

### FindBindableMember\(in FindBindableMemberParameters\) {#Aspid_MVVM_StarterKit_ValueViewModel_4_FindBindableMember_Aspid_MVVM_FindBindableMemberParameters__}

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

### NotifyAll\(\) {#Aspid_MVVM_StarterKit_ValueViewModel_4_NotifyAll}

```csharp
public virtual void NotifyAll()
```

### NotifyCanExecuteChangedAll\(\) {#Aspid_MVVM_StarterKit_ValueViewModel_4_NotifyCanExecuteChangedAll}

```csharp
public virtual void NotifyCanExecuteChangedAll()
```

