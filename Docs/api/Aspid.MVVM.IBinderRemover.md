---
title: "Interface IBinderRemover"
sidebar_label: "IBinderRemover"
description: "Interface IBinderRemover — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IBinderRemover {#Aspid_MVVM_IBinderRemover}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Interface for removing event bindings from a bindable member.

```csharp
public interface IBinderRemover
```


## Methods

### Remove\(IBinder\) {#Aspid_MVVM_IBinderRemover_Remove_Aspid_MVVM_IBinder_}

Removes the binding associated with the specified binder.

```csharp
void Remove(IBinder binder)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The binder whose binding needs to be removed.

