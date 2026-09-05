---
title: "Interface IDynamicProperty<T>"
sidebar_label: "IDynamicProperty<T>"
description: "Interface IDynamicProperty<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IDynamicProperty\<T\> {#Aspid_MVVM_StarterKit_IDynamicProperty_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Provides typed access to a property stored by a [`DynamicViewModel`](Aspid.MVVM.StarterKit.DynamicViewModel.md).

```csharp
public interface IDynamicProperty<T> : IDynamicProperty
```

#### Type Parameters

`T` 

The property's value type.

#### Implements

[IDynamicProperty](Aspid.MVVM.StarterKit.IDynamicProperty.md)


## Properties

### Value {#Aspid_MVVM_StarterKit_IDynamicProperty_1_Value}

Gets or sets the current value.

```csharp
T? Value { get; set; }
```

#### Property Value

 T?

### ValueChanged {#Aspid_MVVM_StarterKit_IDynamicProperty_1_ValueChanged}

Raised after the property's value changes.

```csharp
event Action<T?>? ValueChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>?

