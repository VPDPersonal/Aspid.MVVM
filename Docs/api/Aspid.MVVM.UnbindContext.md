---
title: "Struct UnbindContext"
sidebar_label: "UnbindContext"
description: "Struct UnbindContext — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Struct UnbindContext {#Aspid_MVVM_UnbindContext}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.dll <br/>

Cleanup request identifying the owner releasing a binder.

```csharp
public readonly struct UnbindContext
```



## Constructors

### UnbindContext\(object\) {#Aspid_MVVM_UnbindContext__ctor_System_Object_}

Creates a cleanup context for the given owner.

```csharp
public UnbindContext(object owner)
```

#### Parameters

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)

The same owner instance that acquired the binding.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">owner</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### Owner {#Aspid_MVVM_UnbindContext_Owner}

Gets the owner identified by reference equality.

```csharp
public object Owner { get; }
```

#### Property Value

 [object](https://learn.microsoft.com/dotnet/api/system.object)

## Methods

### EnsureValid\(\) {#Aspid_MVVM_UnbindContext_EnsureValid}

Verifies that the context has an owner.

```csharp
[SuppressMessage("ReSharper", "NotResolvedInText")]
public void EnsureValid()
```

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

[`UnbindContext.Owner`](Aspid.MVVM.UnbindContext.md#Aspid_MVVM_UnbindContext_Owner) is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.
