---
title: "Class DynamicViewModel"
sidebar_label: "DynamicViewModel"
description: "Class DynamicViewModel — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class DynamicViewModel {#Aspid_MVVM_StarterKit_DynamicViewModel}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

An [`IViewModel`](Aspid.MVVM.IViewModel.md) whose typed properties are composed at runtime.

```csharp
public sealed class DynamicViewModel : IViewModel, IEnumerable<IDynamicProperty>, IEnumerable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[DynamicViewModel](Aspid.MVVM.StarterKit.DynamicViewModel.md)

#### Implements

[IViewModel](Aspid.MVVM.IViewModel.md), 
[IEnumerable\<IDynamicProperty\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1), 
[IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.ienumerable)


#### Extension Methods

[MonoViewModelExtensions.DestroyViewModel\(IViewModel\)](Aspid.MVVM.MonoViewModelExtensions.md#Aspid_MVVM_MonoViewModelExtensions_DestroyViewModel_Aspid_MVVM_IViewModel_), 
[ViewModelExtensions.DisposeViewModel\(IViewModel\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModel_Aspid_MVVM_IViewModel_), 

## Constructors

### DynamicViewModel\(bool, IEqualityComparer\<string\>?\) {#Aspid_MVVM_StarterKit_DynamicViewModel__ctor_System_Boolean_System_Collections_Generic_IEqualityComparer_System_String__}

Initializes an empty runtime-composed ViewModel.

```csharp
public DynamicViewModel(bool throwOnMissingMember = false, IEqualityComparer<string>? idComparer = null)
```

#### Parameters

`throwOnMissingMember` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether binder resolution should throw when a requested identifier is absent.

`idComparer` [IEqualityComparer](https://learn.microsoft.com/dotnet/api/system.collections.generic.iequalitycomparer-1)\<[string](https://learn.microsoft.com/dotnet/api/system.string)\>?

The comparer used for property identifiers.

## Properties

### Count {#Aspid_MVVM_StarterKit_DynamicViewModel_Count}

Gets the number of properties in the ViewModel.

```csharp
public int Count { get; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### Properties {#Aspid_MVVM_StarterKit_DynamicViewModel_Properties}

Gets all properties in the ViewModel.

```csharp
public IEnumerable<IDynamicProperty> Properties { get; }
```

#### Property Value

 [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<[IDynamicProperty](Aspid.MVVM.StarterKit.IDynamicProperty.md)\>

### this\[string\] {#Aspid_MVVM_StarterKit_DynamicViewModel_Item_System_String_}

Gets a property by its identifier.

```csharp
public IDynamicProperty this[string id] { get; }
```

#### Property Value

 [IDynamicProperty](Aspid.MVVM.StarterKit.IDynamicProperty.md)

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

Thrown when <code class="paramref">id</code> is empty.

 [KeyNotFoundException](https://learn.microsoft.com/dotnet/api/system.collections.generic.keynotfoundexception)

Thrown when no property has the specified identifier.

## Methods

### Add\<T\>\(string, T?, BindMode\) {#Aspid_MVVM_StarterKit_DynamicViewModel_Add__1_System_String___0_Aspid_MVVM_BindMode_}

Adds a typed property.

```csharp
public IDynamicProperty<T> Add<T>(string id, T? value = default, BindMode mode = BindMode.OneWay)
```

#### Parameters

`id` [string](https://learn.microsoft.com/dotnet/api/system.string)

The identifier used by binders.

`value` T?

The initial value.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding capability exposed by the property.

#### Returns

 [IDynamicProperty](Aspid.MVVM.StarterKit.IDynamicProperty-1.md)\<T\>

The property handle used to read, update, or observe the value.

#### Type Parameters

`T` 

The property's value type.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

Thrown when <code class="paramref">id</code> is empty, already exists, or <code class="paramref">mode</code> is
[`BindMode.None`](Aspid.MVVM.BindMode.md).

### Add\(IDynamicProperty\) {#Aspid_MVVM_StarterKit_DynamicViewModel_Add_Aspid_MVVM_StarterKit_IDynamicProperty_}

Adds a preconstructed or custom dynamic property.

```csharp
public void Add(IDynamicProperty property)
```

#### Parameters

`property` [IDynamicProperty](Aspid.MVVM.StarterKit.IDynamicProperty.md)

The property to add.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">property</code> is null.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

Thrown when the property identifier is empty or already exists.

### Contains\(string\) {#Aspid_MVVM_StarterKit_DynamicViewModel_Contains_System_String_}

Determines whether the ViewModel contains a property with the specified identifier.

```csharp
public bool Contains(string id)
```

#### Parameters

`id` [string](https://learn.microsoft.com/dotnet/api/system.string)

The property identifier.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when the property exists; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### FindBindableMember\(in FindBindableMemberParameters\) {#Aspid_MVVM_StarterKit_DynamicViewModel_FindBindableMember_Aspid_MVVM_FindBindableMemberParameters__}

Searches for a bindable member in the ViewModel based on the provided parameters.

```csharp
public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters)
```

#### Parameters

`parameters` [FindBindableMemberParameters](Aspid.MVVM.FindBindableMemberParameters.md)

The parameters specifying the bindable member to find.

#### Returns

 [FindBindableMemberResult](Aspid.MVVM.FindBindableMemberResult.md)

A [`FindBindableMemberResult`](Aspid.MVVM.FindBindableMemberResult.md) that contains information about the bindable member search result.

### Get\<T\>\(string\) {#Aspid_MVVM_StarterKit_DynamicViewModel_Get__1_System_String_}

Gets a typed property by its identifier.

```csharp
public IDynamicProperty<T> Get<T>(string id)
```

#### Parameters

`id` [string](https://learn.microsoft.com/dotnet/api/system.string)

The property identifier.

#### Returns

 [IDynamicProperty](Aspid.MVVM.StarterKit.IDynamicProperty-1.md)\<T\>

The typed property.

#### Type Parameters

`T` 

The expected property value type.

#### Exceptions

 [KeyNotFoundException](https://learn.microsoft.com/dotnet/api/system.collections.generic.keynotfoundexception)

Thrown when the property does not exist.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

Thrown when the property's value type is not <code class="typeparamref">T</code>.

### GetEnumerator\(\) {#Aspid_MVVM_StarterKit_DynamicViewModel_GetEnumerator}

Returns an enumerator over the properties in this ViewModel.

```csharp
public IEnumerator<IDynamicProperty> GetEnumerator()
```

#### Returns

 [IEnumerator](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerator-1)\<[IDynamicProperty](Aspid.MVVM.StarterKit.IDynamicProperty.md)\>

### TryGet\<T\>\(string, out IDynamicProperty\<T\>?\) {#Aspid_MVVM_StarterKit_DynamicViewModel_TryGet__1_System_String_Aspid_MVVM_StarterKit_IDynamicProperty___0___}

Attempts to get a typed property by its identifier.

```csharp
public bool TryGet<T>(string id, out IDynamicProperty<T>? property)
```

#### Parameters

`id` [string](https://learn.microsoft.com/dotnet/api/system.string)

The property identifier.

`property` [IDynamicProperty](Aspid.MVVM.StarterKit.IDynamicProperty-1.md)\<T\>?

The matching property, when found with the expected type.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when a property with the specified identifier and type exists;
otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

#### Type Parameters

`T` 

The expected property value type.

