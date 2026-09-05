---
title: "Interface IDynamicProperty"
sidebar_label: "IDynamicProperty"
description: "Interface IDynamicProperty — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IDynamicProperty {#Aspid_MVVM_StarterKit_IDynamicProperty}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Provides non-generic access to a property stored by a [`DynamicViewModel`](Aspid.MVVM.StarterKit.DynamicViewModel.md).

```csharp
public interface IDynamicProperty
```


## Properties

### Id {#Aspid_MVVM_StarterKit_IDynamicProperty_Id}

Gets the identifier used by binders to resolve the property.

```csharp
string Id { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### Mode {#Aspid_MVVM_StarterKit_IDynamicProperty_Mode}

Gets the binding capability exposed by the property.

```csharp
BindMode Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

### UntypedValue {#Aspid_MVVM_StarterKit_IDynamicProperty_UntypedValue}

Gets or sets the current value without compile-time type information.

```csharp
object? UntypedValue { get; set; }
```

#### Property Value

 [object](https://learn.microsoft.com/dotnet/api/system.object)?

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

Thrown when the assigned value is incompatible with [`IDynamicProperty.ValueType`](Aspid.MVVM.StarterKit.IDynamicProperty.md#Aspid_MVVM_StarterKit_IDynamicProperty_ValueType).

### ValueType {#Aspid_MVVM_StarterKit_IDynamicProperty_ValueType}

Gets the property's value type.

```csharp
Type ValueType { get; }
```

#### Property Value

 [Type](https://learn.microsoft.com/dotnet/api/system.type)

## Methods

### GetAdder\(\) {#Aspid_MVVM_StarterKit_IDynamicProperty_GetAdder}

Gets the binding endpoint used to connect a binder to the property.

```csharp
IBinderAdder GetAdder()
```

#### Returns

 [IBinderAdder](Aspid.MVVM.IBinderAdder.md)

