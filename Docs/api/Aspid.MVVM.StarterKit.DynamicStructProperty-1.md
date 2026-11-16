---
title: "Class DynamicStructProperty<T>"
sidebar_label: "DynamicStructProperty<T>"
description: "Class DynamicStructProperty<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class DynamicStructProperty\<T\> {#Aspid_MVVM_StarterKit_DynamicStructProperty_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

[`DynamicProperty<T>`](Aspid.MVVM.StarterKit.DynamicProperty-1.md) with typed and boxed struct binding channels.

```csharp
public class DynamicStructProperty<T> : DynamicProperty<T>, IDynamicProperty<T>, IDynamicProperty where T : struct
```

#### Type Parameters

`T`

The property's struct type.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[DynamicProperty\<T\>](Aspid.MVVM.StarterKit.DynamicProperty-1.md) ←
[DynamicStructProperty\<T\>](Aspid.MVVM.StarterKit.DynamicStructProperty-1.md)

#### Implements

[IDynamicProperty\<T\>](Aspid.MVVM.StarterKit.IDynamicProperty-1.md),
[IDynamicProperty](Aspid.MVVM.StarterKit.IDynamicProperty.md)



## Constructors

### DynamicStructProperty\(string, T, BindMode\) {#Aspid_MVVM_StarterKit_DynamicStructProperty_1__ctor_System_String__0_Aspid_MVVM_BindMode_}

Creates a property with typed and boxed binding channels.

```csharp
public DynamicStructProperty(string id, T value = default, BindMode mode = BindMode.OneWay)
```

#### Parameters

`id` [string](https://learn.microsoft.com/dotnet/api/system.string)

The identifier used by binders to resolve the property.

`value` T

The initial value.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode. Must not be None or undefined.

#### Remarks

OneWayToSource exposes a TwoWay member, matching [`DynamicProperty<T>`](Aspid.MVVM.StarterKit.DynamicProperty-1.md).

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">id</code> is blank or <code class="paramref">mode</code> is None.

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

<code class="paramref">mode</code> is undefined.
