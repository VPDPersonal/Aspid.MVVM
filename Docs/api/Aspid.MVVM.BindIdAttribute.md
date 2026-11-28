---
title: "Class BindIdAttribute"
sidebar_label: "BindIdAttribute"
description: "Class BindIdAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BindIdAttribute {#Aspid_MVVM_BindIdAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.dll <br/>

Sets the identifier used to match a View binder to a ViewModel member.

```csharp
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
[AttributeUsage(AttributeTargets.Method|AttributeTargets.Property|AttributeTargets.Field)]
public sealed class BindIdAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ←
[BindIdAttribute](Aspid.MVVM.BindIdAttribute.md)



## Examples


```csharp
[Bind]
[BindId("Health")]
private int _hp;
```


```csharp
[BindId("Health")]
[SerializeField] private MonoBinder[] _hpBinders;
```


## Remarks

The generator uses the default identifier when the supplied name is blank. The default strips the member prefix,
capitalizes the name and adds <code>Command</code> for [`[RelayCommand]`](Aspid.MVVM.RelayCommandAttribute.md) methods.

## Constructors

### BindIdAttribute\(string\) {#Aspid_MVVM_BindIdAttribute__ctor_System_String_}

Creates an attribute with the identifier <code class="paramref">id</code>.

```csharp
public BindIdAttribute(string id)
```

#### Parameters

`id` [string](https://learn.microsoft.com/dotnet/api/system.string)

The identifier that replaces the generated one.

## Fields

### Id {#Aspid_MVVM_BindIdAttribute_Id}

The binding identifier.

```csharp
public readonly string Id
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Exists only when <code>UNITY_EDITOR</code> or <code>DEBUG</code> is defined.
