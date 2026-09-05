---
title: "Class InitializeComponent<T>"
sidebar_label: "InitializeComponent<T>"
description: "Class InitializeComponent<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class InitializeComponent\<T\> {#Aspid_MVVM_StarterKit_InitializeComponent_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Serializable slot that resolves a <code class="typeparamref">T</code> from a component, a plain reference,
a [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html) or the DI container, as chosen by [`ResolveType`](Aspid.MVVM.StarterKit.ResolveType.md).

```csharp
[Serializable]
public abstract class InitializeComponent<T> where T : class
```

#### Type Parameters

`T` 

The resolved type.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[InitializeComponent\<T\>](Aspid.MVVM.StarterKit.InitializeComponent-1.md)



## Properties

### ResolveType {#Aspid_MVVM_StarterKit_InitializeComponent_1_ResolveType}

Gets the chosen source of the instance.

```csharp
protected ResolveType ResolveType { get; }
```

#### Property Value

 [ResolveType](Aspid.MVVM.StarterKit.ResolveType.md)

## Methods

### GetTypeForDi\(\) {#Aspid_MVVM_StarterKit_InitializeComponent_1_GetTypeForDi}

Gets the type requested from the DI container.

```csharp
protected abstract Type? GetTypeForDi()
```

#### Returns

 [Type](https://learn.microsoft.com/dotnet/api/system.type)?

The type, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when none is configured.

### Resolve\(\) {#Aspid_MVVM_StarterKit_InitializeComponent_1_Resolve}

Resolves the instance from the chosen source.

```csharp
public T? Resolve()
```

#### Returns

 T?

The instance, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when the reference is empty or of the wrong type.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Thrown when the DI type is missing or not registered.

### Validate\(\) {#Aspid_MVVM_StarterKit_InitializeComponent_1_Validate}

Clears the references that do not belong to the chosen [`InitializeComponent<T>.ResolveType`](Aspid.MVVM.StarterKit.InitializeComponent-1.md#Aspid_MVVM_StarterKit_InitializeComponent_1_ResolveType) source.

```csharp
public virtual void Validate()
```

