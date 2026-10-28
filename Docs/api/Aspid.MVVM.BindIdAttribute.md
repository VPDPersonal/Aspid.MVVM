---
title: "Class BindIdAttribute"
sidebar_label: "BindIdAttribute"
description: "Class BindIdAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BindIdAttribute {#Aspid_MVVM_BindIdAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Sets the identifier that pairs a ViewModel member with the View binders of the same identifier.

```csharp
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

Without it, the identifier is the member name without its prefix, capitalized, with a <code>Command</code> suffix for a
[`[RelayCommand]`](Aspid.MVVM.RelayCommandAttribute.md) method; the attribute value replaces all of it.

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

Gets the identifier.

```csharp
public readonly string Id
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Exists only when <code>UNITY_EDITOR</code> or <code>DEBUG</code> is defined.

