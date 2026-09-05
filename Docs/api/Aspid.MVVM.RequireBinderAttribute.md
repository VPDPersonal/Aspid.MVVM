---
title: "Class RequireBinderAttribute"
sidebar_label: "RequireBinderAttribute"
description: "Class RequireBinderAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RequireBinderAttribute {#Aspid_MVVM_RequireBinderAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.Unity.dll  

Editor-only attribute applied to serialized fields to declare the required binder association
in the Unity Inspector. Enables the editor to validate that a [`MonoBinder`](Aspid.MVVM.MonoBinder.md)
of the expected type is assigned to the field.
Stripped from builds outside of <code>DEBUG</code> and <code>UNITY_EDITOR</code> configurations.

```csharp
[Conditional("DEBUG")]
[Conditional("UNITY_EDITOR")]
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public sealed class RequireBinderAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[RequireBinderAttribute](Aspid.MVVM.RequireBinderAttribute.md)



## Constructors

### RequireBinderAttribute\(\) {#Aspid_MVVM_RequireBinderAttribute__ctor}

Initializes the attribute without specifying a binder type.
The editor will accept any binder assigned to the field.

```csharp
public RequireBinderAttribute()
```

### RequireBinderAttribute\(Type\) {#Aspid_MVVM_RequireBinderAttribute__ctor_System_Type_}

Initializes the attribute for a single binder type.

```csharp
public RequireBinderAttribute(Type type)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The binder type that this field requires.

### RequireBinderAttribute\(params Type\[\]\) {#Aspid_MVVM_RequireBinderAttribute__ctor_System_Type___}

Initializes the attribute for multiple binder types.

```csharp
public RequireBinderAttribute(params Type[] types)
```

#### Parameters

`types` [Type](https://learn.microsoft.com/dotnet/api/system.type)\[\]

The binder types that this field accepts.

### RequireBinderAttribute\(string\) {#Aspid_MVVM_RequireBinderAttribute__ctor_System_String_}

Initializes the attribute with a single assembly-qualified type name.

```csharp
public RequireBinderAttribute(string assemblyQualifiedName)
```

#### Parameters

`assemblyQualifiedName` [string](https://learn.microsoft.com/dotnet/api/system.string)

The assembly-qualified name of the required binder type.

### RequireBinderAttribute\(params string\[\]\) {#Aspid_MVVM_RequireBinderAttribute__ctor_System_String___}

Initializes the attribute with multiple assembly-qualified type names.

```csharp
public RequireBinderAttribute(params string[] assemblyQualifiedNames)
```

#### Parameters

`assemblyQualifiedNames` [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]

The assembly-qualified names of the accepted binder types.

## Properties

### AssemblyQualifiedNames {#Aspid_MVVM_RequireBinderAttribute_AssemblyQualifiedNames}

Assembly-qualified type names of the binder types accepted by this field.
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when the attribute is used without specifying types.

```csharp
public string[]? AssemblyQualifiedNames { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]?

### Id {#Aspid_MVVM_RequireBinderAttribute_Id}

Optional identifier linking this field to a specific bindable member of the ViewModel.
When set, the editor uses this value to match against ViewModel property names.

```csharp
public string? Id { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

