---
title: Bindable Members
type: concept
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/BindableMembers
tags: [bindable-member, binding, generated, bindmode, source-generation]
updated_at: 2026-05-31
---

# Bindable Members

> The lightweight value wrappers that `[Bind]` emits per field — each holds a value, raises `Changed`, and connects binders to the ViewModel in one specific [[BindMode]].

## Why it exists

A View needs to react when a ViewModel value changes, without reflection or `INotifyPropertyChanged` plumbing. A bindable member is the small object that sits between a `[Bind]` field and the [[IBinder]]s watching it: it owns the current value, fires `Changed`, and knows how to attach/detach binders for exactly one binding direction. Picking a concrete wrapper per field lets the runtime stay allocation-light and avoid casting on the hot path.

## How it works

Every wrapper implements `IBinderAdder` (`Mode` + `Add(IBinder)` → `IBinderRemover?`). The interface hierarchy layers capability:

- `IBinderAdder` — `Mode` and `Add`
- `IReadOnlyValueBindableMember<out T>` — adds `Value` getter
- `IReadOnlyBindableMember<out T>` — adds `Changed` event
- `IBindableMember<T>` — adds `Value` setter

The four [[BindMode]] variants differ in what they accept and how they wire binders:

- **OneWay** — get/set `Value`; on `Add` pushes the value once, then for `OneWay` binders subscribes their `SetValue` to `Changed` (`OneTime` binders get the value and return `null`). Rejects reverse modes.
- **TwoWay** — supports every mode except `None`; forward via `IBinder<T>`/`IAnyBinder`, reverse via `IReverseBinder<T>`/`IAnyReverseBinder` calling a captured setter `Action<T>`.
- **OneTime** — a per-`T` singleton; `Get(value)` sets the value, `Add` pushes it once and returns `null` (no removal). Forward modes only.
- **OneWayToSource** — has only a setter `Action<T>`; reverse binders feed View changes back to the ViewModel. `TwoWay`/`OneWayToSource` only.

Each direction comes in three payload flavors:

- **value** (`Classes/`) — reference types; single `Action<T?>` event.
- **struct** (`Struct/`) — `T : struct`; the generic base also raises a `BoxedChanged` event so `IBinder<TBoxed>` (default `ValueType`) subscribers receive the value pre-boxed, avoiding repeated boxing.
- **enum** (`Enum/`) — `sealed` subclasses of the struct base with `TBoxed` fixed to `Enum`, so `IBinder<Enum>` can bind alongside the typed `T`.

So one `[Bind]` field yields one wrapper chosen by the field type and its declared `BindMode`. (Inference: the generator picks the variant; generator source lives in the [[Source Generator]] submodule.)

## Key relationships

- Emitted during [[ViewModel Generation]] from a `[Bind]` field.
- Consumed by [[Data Binding]] / [[Runtime Binding Resolution]] when a [[View]] connects.
- `Add`/`Remove` operate on [[IBinder]]; modes defined in [[BindMode]].
- Profiler-marked under Unity (`PROFILER` define).

## Source

`Aspid.MVVM/Assets/Aspid/MVVM/Source/BindableMembers` — `IBindableMember.cs`, `IBinderAdder.cs`, `IBinderRemover.cs`, and the `Classes/`, `Struct/`, `Enum/` folders.
