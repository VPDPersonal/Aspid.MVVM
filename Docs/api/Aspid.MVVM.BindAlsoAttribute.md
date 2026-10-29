---
title: "Class BindAlsoAttribute"
sidebar_label: "BindAlsoAttribute"
description: "Class BindAlsoAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BindAlsoAttribute {#Aspid_MVVM_BindAlsoAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Notifies the binders of another bindable property whenever the marked member of a [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) class changes.

```csharp
[AttributeUsage(AttributeTargets.Property|AttributeTargets.Field, AllowMultiple = true)]
public sealed class BindAlsoAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[BindAlsoAttribute](Aspid.MVVM.BindAlsoAttribute.md)



## Examples


```csharp
[Bind]
[BindAlso(nameof(FullName))]
private string _firstName;

private string FullName => $"{FirstName} {LastName}";
```


## Remarks

<p>
The marked member needs [`BindAttribute`](Aspid.MVVM.BindAttribute.md), [`OneWayBindAttribute`](Aspid.MVVM.OneWayBindAttribute.md) or
[`TwoWayBindAttribute`](Aspid.MVVM.TwoWayBindAttribute.md) with a mode that pushes values to the View.
</p>
<p>A property of the class without a bind attribute becomes a [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) bindable member when named here.</p>

## Constructors

### BindAlsoAttribute\(string\) {#Aspid_MVVM_BindAlsoAttribute__ctor_System_String_}

Creates an attribute that also notifies the property named <code class="paramref">propertyName</code>.

```csharp
public BindAlsoAttribute(string propertyName)
```

#### Parameters

`propertyName` [string](https://learn.microsoft.com/dotnet/api/system.string)

The name of the generated or declared property to notify.

## Properties

### PropertyName {#Aspid_MVVM_BindAlsoAttribute_PropertyName}

Gets the name of the property whose binders are also notified.

```csharp
public string PropertyName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Exists only when <code>UNITY_EDITOR</code> or <code>DEBUG</code> is defined.

