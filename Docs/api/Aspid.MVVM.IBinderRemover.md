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

Defines the side of a bindable member that detaches binders.

```csharp
public interface IBinderRemover
```


## Methods

### Remove\(IBinder\) {#Aspid_MVVM_IBinderRemover_Remove_Aspid_MVVM_IBinder_}

Unsubscribes <code class="paramref">binder</code> from the member.

```csharp
void Remove(IBinder binder)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The binder previously passed to [`IBinderAdder.Add`](Aspid.MVVM.IBinderAdder.md#Aspid_MVVM_IBinderAdder_Add_Aspid_MVVM_IBinder_).

