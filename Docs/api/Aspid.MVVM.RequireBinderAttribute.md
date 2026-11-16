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

Associates a serialized binder field with a binding identifier and accepted value types for Inspector validation.

```csharp
[Conditional("DEBUG")]
[Conditional("UNITY_EDITOR")]
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
[SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
public sealed class RequireBinderAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ←
[RequireBinderAttribute](Aspid.MVVM.RequireBinderAttribute.md)



## Examples


```csharp
[RequireBinder(typeof(int), Id = "Health")]
[UnityEngine.SerializeField] private MonoBinder[] _health;
```


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

The bound value type that assigned binders must support.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">type</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

A type is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or has no assembly-qualified name.

### RequireBinderAttribute\(params Type\[\]\) {#Aspid_MVVM_RequireBinderAttribute__ctor_System_Type___}

Creates the attribute for multiple value types.

```csharp
public RequireBinderAttribute(params Type[] types)
```

#### Parameters

`types` [Type](https://learn.microsoft.com/dotnet/api/system.type)\[\]

The bound value types that assigned binders must support.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">types</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

A type is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or has no assembly-qualified name.

### RequireBinderAttribute\(string\) {#Aspid_MVVM_RequireBinderAttribute__ctor_System_String_}

Creates the attribute with a single assembly-qualified type name.

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

Creates the attribute with multiple assembly-qualified type names.

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

Gets the accepted value type names or names of string fields or properties containing those names.
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when the attribute is used without specifying types.

```csharp
public string[]? AssemblyQualifiedNames { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]?

### Id {#Aspid_MVVM_RequireBinderAttribute_Id}

Gets or sets the binding identifier or the name of a string field or property containing it.
When absent or blank, the identifier is derived from the decorated field name.

```csharp
public string? Id { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?
