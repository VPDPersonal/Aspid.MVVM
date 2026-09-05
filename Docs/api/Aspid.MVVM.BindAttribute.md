---
title: "Class BindAttribute"
sidebar_label: "BindAttribute"
description: "Class BindAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BindAttribute {#Aspid_MVVM_BindAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Sealed [`BaseBindAttribute`](Aspid.MVVM.BaseBindAttribute.md) applied to fields of a type carrying [`ViewModelAttribute`](Aspid.MVVM.ViewModelAttribute.md);
directs the Source Generator to emit a bindable property for the field.
The default constructor selects [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) for mutable fields and [`BindMode.OneTime`](Aspid.MVVM.BindMode.md)
for <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/readonly">readonly</a> fields. When the mode-taking constructor is used on a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/readonly">readonly</a> field,
[`BindMode.OneTime`](Aspid.MVVM.BindMode.md) and [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) both resolve to [`BindMode.OneTime`](Aspid.MVVM.BindMode.md);
any other mode is rejected.

```csharp
public sealed class BindAttribute : BaseBindAttribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[BaseBindAttribute](Aspid.MVVM.BaseBindAttribute.md) ← 
[BindAttribute](Aspid.MVVM.BindAttribute.md)



## Constructors

### BindAttribute\(\) {#Aspid_MVVM_BindAttribute__ctor}

Initializes a new instance of the [`BindAttribute`](Aspid.MVVM.BindAttribute.md) class with the default binding mode.
For non-readonly fields, the default mode is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md).
For readonly fields, the default mode is [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

```csharp
public BindAttribute()
```

### BindAttribute\(BindMode\) {#Aspid_MVVM_BindAttribute__ctor_Aspid_MVVM_BindMode_}

Initializes a new instance of the [`BindAttribute`](Aspid.MVVM.BindAttribute.md) class with the specified binding mode.
For readonly fields, only [`BindMode.OneTime`](Aspid.MVVM.BindMode.md) and [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) are supported, 
both of which will behave as [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).
Other modes are not supported for readonly fields.

```csharp
public BindAttribute(BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The desired binding mode for the field.

