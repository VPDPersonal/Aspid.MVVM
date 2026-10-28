---
title: "Struct FindBindableMemberResult"
sidebar_label: "FindBindableMemberResult"
description: "Struct FindBindableMemberResult — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Struct FindBindableMemberResult {#Aspid_MVVM_FindBindableMemberResult}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Represents the result of [`IViewModel.FindBindableMember`](Aspid.MVVM.IViewModel.md).

```csharp
public readonly struct FindBindableMemberResult
```



## Constructors

### FindBindableMemberResult\(IBinderAdder?\) {#Aspid_MVVM_FindBindableMemberResult__ctor_Aspid_MVVM_IBinderAdder_}

Creates a result for <code class="paramref">adder</code>.

```csharp
public FindBindableMemberResult(IBinderAdder? adder = null)
```

#### Parameters

`adder` [IBinderAdder](Aspid.MVVM.IBinderAdder.md)?

The found bindable member; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when none was found.

## Fields

### Adder {#Aspid_MVVM_FindBindableMemberResult_Adder}

The found bindable member; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when none was found.

```csharp
public readonly IBinderAdder? Adder
```

#### Field Value

 [IBinderAdder](Aspid.MVVM.IBinderAdder.md)?

### IsFound {#Aspid_MVVM_FindBindableMemberResult_IsFound}

Indicates whether the bindable member was found.

```csharp
public readonly bool IsFound
```

#### Field Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

