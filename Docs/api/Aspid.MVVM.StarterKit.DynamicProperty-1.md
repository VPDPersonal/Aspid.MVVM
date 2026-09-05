---
title: "Class DynamicProperty<T>"
sidebar_label: "DynamicProperty<T>"
description: "Class DynamicProperty<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class DynamicProperty\<T\> {#Aspid_MVVM_StarterKit_DynamicProperty_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

A typed, observable property that can be added to a [`DynamicViewModel`](Aspid.MVVM.StarterKit.DynamicViewModel.md).

```csharp
public sealed class DynamicProperty<T> : IDynamicProperty<T>, IDynamicProperty
```

#### Type Parameters

`T` 

The property's value type.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[DynamicProperty\<T\>](Aspid.MVVM.StarterKit.DynamicProperty-1.md)

#### Implements

[IDynamicProperty\<T\>](Aspid.MVVM.StarterKit.IDynamicProperty-1.md), 
[IDynamicProperty](Aspid.MVVM.StarterKit.IDynamicProperty.md)



## Constructors

### DynamicProperty\(string, T?, BindMode\) {#Aspid_MVVM_StarterKit_DynamicProperty_1__ctor_System_String__0_Aspid_MVVM_BindMode_}

```csharp
public DynamicProperty(string id, T? value = default, BindMode mode = BindMode.OneWay)
```

#### Parameters

`id` [string](https://learn.microsoft.com/dotnet/api/system.string)

The identifier used by binders to resolve the property.

`value` T?

The initial value.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding capability exposed by the property.

#### Remarks

[`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md) shares the two-way member: the property still pushes its value to the View.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

Thrown when <code class="paramref">id</code> is empty or <code class="paramref">mode</code> is
[`BindMode.None`](Aspid.MVVM.BindMode.md).

## Properties

### Id {#Aspid_MVVM_StarterKit_DynamicProperty_1_Id}

Gets the identifier used by binders to resolve the property.

```csharp
public string Id { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### Mode {#Aspid_MVVM_StarterKit_DynamicProperty_1_Mode}

Gets the binding capability exposed by the property.

```csharp
public BindMode Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

### UntypedValue {#Aspid_MVVM_StarterKit_DynamicProperty_1_UntypedValue}

Gets or sets the current value without compile-time type information.

```csharp
public object? UntypedValue { get; set; }
```

#### Property Value

 [object](https://learn.microsoft.com/dotnet/api/system.object)?

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

Thrown when the assigned value is incompatible with [`IDynamicProperty.ValueType`](Aspid.MVVM.StarterKit.IDynamicProperty.md#Aspid_MVVM_StarterKit_IDynamicProperty_ValueType).

### Value {#Aspid_MVVM_StarterKit_DynamicProperty_1_Value}

Gets or sets the current value.

```csharp
public T? Value { get; set; }
```

#### Property Value

 T?

### ValueType {#Aspid_MVVM_StarterKit_DynamicProperty_1_ValueType}

Gets the property's value type.

```csharp
public Type ValueType { get; }
```

#### Property Value

 [Type](https://learn.microsoft.com/dotnet/api/system.type)

## Methods

### GetAdder\(\) {#Aspid_MVVM_StarterKit_DynamicProperty_1_GetAdder}

Gets the binding endpoint used to connect a binder to the property.

```csharp
public IBinderAdder GetAdder()
```

#### Returns

 [IBinderAdder](Aspid.MVVM.IBinderAdder.md)

### ValueChanged {#Aspid_MVVM_StarterKit_DynamicProperty_1_ValueChanged}

Raised after the property's value changes.

```csharp
public event Action<T?>? ValueChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>?

