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

Represents the result of a binding operation, indicating whether a bindable member was successfully located.

```csharp
public readonly struct FindBindableMemberResult
```



## Constructors

### FindBindableMemberResult\(IBinderAdder?\) {#Aspid_MVVM_FindBindableMemberResult__ctor_Aspid_MVVM_IBinderAdder_}

Initializes a new instance of the [`FindBindableMemberResult`](Aspid.MVVM.FindBindableMemberResult.md) struct.

```csharp
public FindBindableMemberResult(IBinderAdder? adder = null)
```

#### Parameters

`adder` [IBinderAdder](Aspid.MVVM.IBinderAdder.md)?

The event adder for the bindable member, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if not found.

## Fields

### Adder {#Aspid_MVVM_FindBindableMemberResult_Adder}

Gets the binder adder for the bindable member, if found.

```csharp
public readonly IBinderAdder? Adder
```

#### Field Value

 [IBinderAdder](Aspid.MVVM.IBinderAdder.md)?

### IsFound {#Aspid_MVVM_FindBindableMemberResult_IsFound}

Indicates whether the bindable member was successfully found.

```csharp
public readonly bool IsFound
```

#### Field Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

