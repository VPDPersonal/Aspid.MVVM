---
title: IBinder
type: entity
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/IBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/Binder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/IReverseBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/Impls/TargetBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/Impls/ViewBinder.cs
tags: [binder, contract, lifecycle, core]
updated_at: 2026-05-31
---

# IBinder

> The contract every binder implements: it knows how to attach to a [[ViewModel]]'s bindable member, receive values, and detach — the wire between View UI and ViewModel state.

## Why it exists

A [[ViewModel]] exposes typed [[Bindable Members]] but knows nothing about UI. A binder is the adapter that subscribes to one of those members and pushes its value somewhere (a `Text`, a `Slider`, another View). `IBinder` is the minimal contract that lets the framework manage that connection uniformly, with **zero reflection** — the [[Source Generator]] wires concrete binders to members by type.

## How it works

`IBinder` defines the lifecycle only:

- `Mode` — a [[BindMode]] (default `OneWay`) deciding data-flow direction.
- `Bind(IBinderAdder)` — registers the binder with a ViewModel; the adder returns an `IBinderRemover` for teardown.
- `Unbind()` — detaches.

Value transport is layered on top via generic sub-interfaces:

- `IBinder<in T>.SetValue(T?)` — receives a value **from** the ViewModel (OneWay).
- `IReverseBinder<out T>` — exposes a `ValueChanged` event to push values **back** to the ViewModel (defaults `Mode` to `TwoWay`). See [[Data Binding]] and `OneWayToSource` notes in [[BindMode]].

`Binder` (`[Serializable]`, abstract) is the standard base. It owns the serialized `_mode` field, tracks `IsBound`, guards against double-bind (logs an error in Unity, throws elsewhere), respects an overridable `IsBind` gate, and exposes hook methods `OnBinding`/`OnBound`/`OnUnbinding`/`OnUnbound` for subclasses. It also wires Unity Profiler markers and partial debug hooks (`OnBoundDebug`/`OnUnboundDebug`). Concrete binders implement `IBinder<T>` and act inside `SetValue`.

Two notable base subclasses live in `Impls/`:

- `TargetBinder<TTarget>` — adds a serialized `Target` reference (null-checked) that derived binders operate on. Most StarterKit binders (e.g. [[Text Binders]], [[Slider Binders]]) descend from this. See [[Binder Base Classes]].
- `ViewBinder : IBinder<IViewModel?>` — on `SetValue` it deinitializes its `IView` then re-initializes it with the incoming ViewModel (or just deinitializes if null). This is how a [[View]] nests another View bound to a child ViewModel; ties into [[View Initialization]].

## Key relationships

- Receives values from [[Bindable Members]] generated for `[Bind]` fields (see [[ViewModel to Generated Code]]).
- Registered/resolved at runtime via [[Runtime Binding Resolution]].
- For UI families, browse the [[Binders Catalog]] and [[Mono Binders]].
- Reverse binders feed back into the ViewModel; commands are bound separately via [[IRelayCommand]].

## Source

- `Source/Binders/IBinder.cs` — `IBinder`, `IBinder<T>`
- `Source/Binders/IReverseBinder.cs` — reverse contract
- `Source/Binders/Binder.cs` — base lifecycle
- `Source/Binders/Impls/TargetBinder.cs`, `Impls/ViewBinder.cs`
