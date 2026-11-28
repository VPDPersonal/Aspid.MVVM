---
title: "Class RequireBinderAttribute"
sidebar_label: "RequireBinderAttribute"
description: "Class RequireBinderAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RequireBinderAttribute {#Aspid_MVVM_RequireBinderAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.Unity.dll <br/>

Configures the binding ID and accepted value types for Inspector validation of a serialized binder field.

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



## Examples


```csharp
[RequireBinder(nameof(_valueTypeName), Id = nameof(_memberId))]
[UnityEngine.SerializeField] private MonoBinder[] _binders;
```


## Remarks

Attribute applications are omitted unless <code>DEBUG</code> or <code>UNITY_EDITOR</code> is defined.

## Constructors

### RequireBinderAttribute\(\) {#Aspid_MVVM_RequireBinderAttribute__ctor}

Creates an attribute without value type restrictions.

```csharp
public RequireBinderAttribute()
```

### RequireBinderAttribute\(Type\) {#Aspid_MVVM_RequireBinderAttribute__ctor_System_Type_}

Creates the attribute for a single value type.

```csharp
public RequireBinderAttribute(Type type)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The bound value type to accept.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">type</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">type</code> has no assembly-qualified name.

### RequireBinderAttribute\(params Type\[\]\) {#Aspid_MVVM_RequireBinderAttribute__ctor_System_Type___}

Creates the attribute for multiple value types.

```csharp
public RequireBinderAttribute(params Type[] types)
```

#### Parameters

`types` [Type](https://learn.microsoft.com/dotnet/api/system.type)\[\]

The bound value types to accept. Each binder must support at least one.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">types</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

An element of <code class="paramref">types</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or has no assembly-qualified name.

### RequireBinderAttribute\(string\) {#Aspid_MVVM_RequireBinderAttribute__ctor_System_String_}

Creates the attribute with one value type name or a member supplying it.

```csharp
public RequireBinderAttribute(string assemblyQualifiedName)
```

#### Parameters

`assemblyQualifiedName` [string](https://learn.microsoft.com/dotnet/api/system.string)

The assembly-qualified value type name or the name of a string field or property containing it.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">assemblyQualifiedName</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### RequireBinderAttribute\(params string\[\]\) {#Aspid_MVVM_RequireBinderAttribute__ctor_System_String___}

Creates the attribute with value type names or members supplying them.

```csharp
public RequireBinderAttribute(params string[] assemblyQualifiedNames)
```

#### Parameters

`assemblyQualifiedNames` [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]

The value type names or names of string fields or properties containing them.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">assemblyQualifiedNames</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### AssemblyQualifiedNames {#Aspid_MVVM_RequireBinderAttribute_AssemblyQualifiedNames}

Gets the accepted type names or the names of string members that supply them.
The parameterless constructor leaves this property <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public string[]? AssemblyQualifiedNames { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]?

### Id {#Aspid_MVVM_RequireBinderAttribute_Id}

Gets or sets the binding ID or the name of a string member containing it.
A blank value derives the ID from the decorated field name.

```csharp
public string? Id { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?
