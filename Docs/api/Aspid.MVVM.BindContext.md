---
title: "Struct BindContext"
sidebar_label: "BindContext"
description: "Struct BindContext — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Struct BindContext {#Aspid_MVVM_BindContext}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.dll <br/>

Binding request containing a bindable member and its owner.

```csharp
public readonly struct BindContext
```



## Constructors

### BindContext\(IBinderAdder, object\) {#Aspid_MVVM_BindContext__ctor_Aspid_MVVM_IBinderAdder_System_Object_}

Creates a binding context for the given member and owner.

```csharp
public BindContext(IBinderAdder adder, object owner)
```

#### Parameters

`adder` [IBinderAdder](Aspid.MVVM.IBinderAdder.md)

The bindable member to attach to.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)

The owner instance that will release the binding.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">adder</code> or <code class="paramref">owner</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### Adder {#Aspid_MVVM_BindContext_Adder}

Gets the bindable member to attach to.

```csharp
public IBinderAdder Adder { get; }
```

#### Property Value

 [IBinderAdder](Aspid.MVVM.IBinderAdder.md)

### Owner {#Aspid_MVVM_BindContext_Owner}

Gets the owner identified by reference equality.

```csharp
public object Owner { get; }
```

#### Property Value

 [object](https://learn.microsoft.com/dotnet/api/system.object)

## Methods

### EnsureValid\(\) {#Aspid_MVVM_BindContext_EnsureValid}

Verifies that the context has a bindable member and an owner.

```csharp
[SuppressMessage("ReSharper", "NotResolvedInText")]
public void EnsureValid()
```

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

[`BindContext.Adder`](Aspid.MVVM.BindContext.md#Aspid_MVVM_BindContext_Adder) or [`BindContext.Owner`](Aspid.MVVM.BindContext.md#Aspid_MVVM_BindContext_Owner) is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.
