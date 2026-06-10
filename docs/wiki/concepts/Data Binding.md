---
title: Data Binding
type: concept
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/IBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/IReverseBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/IAnyBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/IAnyReverseBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/Binder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/Impls/TargetBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/Impls/ViewBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/BindableMembers/IBinderAdder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/BindableMembers/IBinderRemover.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/BindableMembers/Classes/OneWayBindableMember.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/BindableMembers/Classes/TwoWayBindableMember.cs
tags: [binding, binder, bindable-member, lifecycle, concept]
updated_at: 2026-05-31
---

# Data Binding

> How a single View element gets wired to a single ViewModel member, who pushes values which way, and when the subscription is torn down.

## Why it exists

The point of MVVM is that the View never reaches into the ViewModel and vice versa. A *binder* is the one-to-one adapter that closes this gap: it owns a reference to a UI element and knows how to copy a value into (or out of) it, while staying ignorant of the concrete ViewModel. This keeps bindings reflection-free, allocation-light, and statically typed.

## How it works

A binder implements [[IBinder]]. Forward flow (ViewModel to View) comes from `IBinder<T>.SetValue(T)`; reverse flow (View to ViewModel) comes from `IReverseBinder<T>.ValueChanged`. `IAnyBinder` / `IAnyReverseBinder` are the untyped escape hatches (their value contract is checked only at runtime).

Each binder reports a `Mode` ([[BindMode]]): default `OneWay` for forward binders, `TwoWay` for reverse ones.

The connection point on the ViewModel side is a [[Bindable Members|bindable member]] — a generated field exposed as an `IBinderAdder`. Binding runs through a tiny lifecycle in [[Binder Base Classes|Binder]]:

1. `Bind(IBinderAdder)` guards against double-bind and the `IsBind` flag, calls `OnBinding()`.
2. `binderAdder.Add(this)` pushes the current value immediately, then subscribes the binder to the member's `Changed` event (and, for `TwoWay`/`OneWayToSource`, subscribes the member to the binder's `ValueChanged`). It returns an `IBinderRemover`.
3. `Unbind()` calls the stored remover, detaching every subscription, then `OnUnbound()`.

`OneTime` binders receive the value once and `Add` returns `null` — there is nothing to unsubscribe.

Two ready binders ship here: [[Binder Base Classes|TargetBinder<TTarget>]] exposes a serialized `Target` so subclasses bind a concrete object, and `ViewBinder` is an `IBinder<IViewModel?>` that (de)initializes a nested [[View]] when a child ViewModel arrives — enabling nested views.

## Key relationships

- Which member type accepts which mode: `OneWayBindableMember` rejects anything but `OneWay`/`OneTime`; `TwoWayBindableMember` handles all four directions.
- Members and their `[Bind]`-driven creation: see [[Bindable Members]] and [[ViewModel to Generated Code]].
- Concrete binders live in [[Binders Catalog]] (e.g. [[Text Binders]], [[Slider Binders]]); commands use [[Relay Commands]].
- How a `View` discovers and calls `Bind` at startup: [[Runtime Binding Resolution]] and [[View Initialization]].

## Source

`Source/Binders/` (interfaces + `Binder`, `TargetBinder`, `ViewBinder`) and `Source/BindableMembers/` (`IBinderAdder`/`IBinderRemover`, the per-mode `*BindableMember` classes).
