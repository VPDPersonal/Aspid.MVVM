---
title: "Interface IBinderAdder"
sidebar_label: "IBinderAdder"
description: "Interface IBinderAdder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IBinderAdder {#Aspid_MVVM_IBinderAdder}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Interface for adding event bindings to a bindable member.

```csharp
public interface IBinderAdder
```


## Properties

### Mode {#Aspid_MVVM_IBinderAdder_Mode}

Gets the binding mode for this member.

```csharp
BindMode Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

## Methods

### Add\(IBinder\) {#Aspid_MVVM_IBinderAdder_Add_Aspid_MVVM_IBinder_}

Adds a binding to the bindable member using the specified binder.

```csharp
IBinderRemover? Add(IBinder binder)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The binder to be used for adding the binding.

#### Returns

 [IBinderRemover](Aspid.MVVM.IBinderRemover.md)?

An [`IBinderRemover`](Aspid.MVVM.IBinderRemover.md) that can remove the added binding, 
or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if the binding could not be added.

