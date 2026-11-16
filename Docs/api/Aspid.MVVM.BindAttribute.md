---
title: "Class BindAttribute"
sidebar_label: "BindAttribute"
description: "Class BindAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BindAttribute {#Aspid_MVVM_BindAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.dll <br/>

Generates a bindable member for the field or property of a [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) class, and a property for a field.

```csharp
public sealed class BindAttribute : BaseBindAttribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ←
[BaseBindAttribute](Aspid.MVVM.BaseBindAttribute.md) ←
[BindAttribute](Aspid.MVVM.BindAttribute.md)



## Examples


```csharp
[Bind] private int _health;
```


```csharp
[Bind(BindMode.OneWay)] private string _title;
```


## Remarks

<p>
Without a mode, a mutable field binds [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) and a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/readonly">readonly</a> field
[`BindMode.OneTime`](Aspid.MVVM.BindMode.md); a property binds [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) when it is get-only,
[`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md) when it is set-only and [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) otherwise.
</p>
<p>
A mode the member cannot support skips the member with a generator warning: a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/readonly">readonly</a> field takes only
[`BindMode.OneTime`](Aspid.MVVM.BindMode.md), a get-only property [`BindMode.OneTime`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWay`](Aspid.MVVM.BindMode.md), and
a set-only property [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).
</p>

## Constructors

### BindAttribute\(\) {#Aspid_MVVM_BindAttribute__ctor}

Creates an attribute whose binding mode follows from the member.

```csharp
public BindAttribute()
```

### BindAttribute\(BindMode\) {#Aspid_MVVM_BindAttribute__ctor_Aspid_MVVM_BindMode_}

Creates an attribute with an explicit binding mode.

```csharp
public BindAttribute(BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode. A mode the member cannot support skips the member.

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

<code class="paramref">mode</code> is [`BindMode.None`](Aspid.MVVM.BindMode.md) or undefined.

## Properties

### Mode {#Aspid_MVVM_BindAttribute_Mode}

Gets the explicit binding mode; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to infer it from the member.

```csharp
public BindMode? Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)?
