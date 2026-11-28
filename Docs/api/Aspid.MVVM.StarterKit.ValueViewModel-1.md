---
title: "Class ValueViewModel<T>"
sidebar_label: "ValueViewModel<T>"
description: "Class ValueViewModel<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ValueViewModel\<T\> {#Aspid_MVVM_StarterKit_ValueViewModel_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

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

[ViewModelExtensions.DestroyViewModel\(IViewModel\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModel_Aspid_MVVM_IViewModel_),
[ViewModelExtensions.DestroyViewModelAndGameObject\(IViewModel\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModelAndGameObject_Aspid_MVVM_IViewModel_),
[ViewModelExtensions.DestroyViewModelAndGameObjectSafely\(IViewModel?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModelAndGameObjectSafely_Aspid_MVVM_IViewModel_),
[ViewModelExtensions.DestroyViewModelSafely\(IViewModel?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModelSafely_Aspid_MVVM_IViewModel_),
[ViewModelExtensions.DisposeViewModel\(IViewModel\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModel_Aspid_MVVM_IViewModel_),
[ViewModelExtensions.DisposeViewModelSafely\(IViewModel?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModelSafely_Aspid_MVVM_IViewModel_),
[ViewModelExtensions.InitializeView\(IViewModel, IView\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeView_Aspid_MVVM_IViewModel_Aspid_MVVM_IView_),
[ViewModelExtensions.InitializeViewSafely\(IViewModel?, IView?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViewSafely_Aspid_MVVM_IViewModel_Aspid_MVVM_IView_),
[ViewModelExtensions.InitializeViews\<T\>\(IViewModel, params T\[\]\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViews__1_Aspid_MVVM_IViewModel___0___),
[ViewModelExtensions.InitializeViews\<T\>\(IViewModel, Span\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViews__1_Aspid_MVVM_IViewModel_System_Span___0__),
[ViewModelExtensions.InitializeViews\<T\>\(IViewModel, ReadOnlySpan\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViews__1_Aspid_MVVM_IViewModel_System_ReadOnlySpan___0__),
[ViewModelExtensions.InitializeViews\<T\>\(IViewModel, List\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViews__1_Aspid_MVVM_IViewModel_System_Collections_Generic_List___0__),
[ViewModelExtensions.InitializeViews\<T\>\(IViewModel, IEnumerable\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViews__1_Aspid_MVVM_IViewModel_System_Collections_Generic_IEnumerable___0__),
[ViewModelExtensions.InitializeViewsSafely\<T\>\(IViewModel?, params T\[\]?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViewsSafely__1_Aspid_MVVM_IViewModel___0___),
[ViewModelExtensions.InitializeViewsSafely\<T\>\(IViewModel?, Span\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_Span___0__),
[ViewModelExtensions.InitializeViewsSafely\<T\>\(IViewModel?, ReadOnlySpan\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_ReadOnlySpan___0__),
[ViewModelExtensions.InitializeViewsSafely\<T\>\(IViewModel?, List\<T\>?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_Collections_Generic_List___0__),
[ViewModelExtensions.InitializeViewsSafely\<T\>\(IViewModel?, IEnumerable\<T\>?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_Collections_Generic_IEnumerable___0__),
[ViewModelExtensions.ReinitializeView\(IViewModel?, IView\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_ReinitializeView_Aspid_MVVM_IViewModel_Aspid_MVVM_IView_),
[ViewModelExtensions.ReinitializeViewSafely\(IViewModel?, IView?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_ReinitializeViewSafely_Aspid_MVVM_IViewModel_Aspid_MVVM_IView_),
[ViewModelExtensions.ReinitializeViews\<T\>\(IViewModel?, T\[\]\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_ReinitializeViews__1_Aspid_MVVM_IViewModel___0___),
[ViewModelExtensions.ReinitializeViews\<T\>\(IViewModel?, Span\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_ReinitializeViews__1_Aspid_MVVM_IViewModel_System_Span___0__),
[ViewModelExtensions.ReinitializeViews\<T\>\(IViewModel?, ReadOnlySpan\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_ReinitializeViews__1_Aspid_MVVM_IViewModel_System_ReadOnlySpan___0__),
[ViewModelExtensions.ReinitializeViews\<T\>\(IViewModel?, List\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_ReinitializeViews__1_Aspid_MVVM_IViewModel_System_Collections_Generic_List___0__),
[ViewModelExtensions.ReinitializeViews\<T\>\(IViewModel?, IEnumerable\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_ReinitializeViews__1_Aspid_MVVM_IViewModel_System_Collections_Generic_IEnumerable___0__),
[ViewModelExtensions.ReinitializeViewsSafely\<T\>\(IViewModel?, T\[\]?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_ReinitializeViewsSafely__1_Aspid_MVVM_IViewModel___0___),
[ViewModelExtensions.ReinitializeViewsSafely\<T\>\(IViewModel?, Span\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_ReinitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_Span___0__),
[ViewModelExtensions.ReinitializeViewsSafely\<T\>\(IViewModel?, ReadOnlySpan\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_ReinitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_ReadOnlySpan___0__),
[ViewModelExtensions.ReinitializeViewsSafely\<T\>\(IViewModel?, List\<T\>?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_ReinitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_Collections_Generic_List___0__),
[ViewModelExtensions.ReinitializeViewsSafely\<T\>\(IViewModel?, IEnumerable\<T\>?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_ReinitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_Collections_Generic_IEnumerable___0__)

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

Finds the bindable member with the identifier in <code class="paramref">parameters</code>.

```csharp
public virtual FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters)
```

#### Parameters

`parameters` [FindBindableMemberParameters](Aspid.MVVM.FindBindableMemberParameters.md)

The query with a nonblank binding identifier.

#### Returns

 [FindBindableMemberResult](Aspid.MVVM.FindBindableMemberResult.md)

A result containing the matching member, or an empty result if the identifier is unknown.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

The query identifier is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

The query identifier is empty or whitespace.

### NotifyAll\(\) {#Aspid_MVVM_StarterKit_ValueViewModel_1_NotifyAll}

```csharp
public virtual void NotifyAll()
```

### NotifyCanExecuteChangedAll\(\) {#Aspid_MVVM_StarterKit_ValueViewModel_1_NotifyCanExecuteChangedAll}

```csharp
public virtual void NotifyCanExecuteChangedAll()
```
