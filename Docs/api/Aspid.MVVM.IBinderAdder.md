---
title: "Interface IBinderAdder"
sidebar_label: "IBinderAdder"
description: "Interface IBinderAdder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IBinderAdder {#Aspid_MVVM_IBinderAdder}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.dll <br/>

Defines the side of a bindable member that accepts binders.

```csharp
public interface IBinderAdder
```


## Properties

### Mode {#Aspid_MVVM_IBinderAdder_Mode}

Gets the binding mode of the member.

```csharp
BindMode Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

## Methods

### Add\(IBinder\) {#Aspid_MVVM_IBinderAdder_Add_Aspid_MVVM_IBinder_}

Attaches <code class="paramref">binder</code> using the binding directions supported by both the binder and the member.

```csharp
IBinderRemover? Add(IBinder binder)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The binder to add.

#### Returns

 [IBinderRemover](Aspid.MVVM.IBinderRemover.md)?

The remover that detaches <code class="paramref">binder</code>; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when nothing stays subscribed,
as for [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

#### Remarks

When the effective mode reaches the View, pushes the current value to the binder during attachment.
