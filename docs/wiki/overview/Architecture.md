---
title: Architecture
type: overview
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source
  - CLAUDE.md
  - Readme.md
tags: [overview, architecture]
updated_at: 2026-05-31
---

# Architecture

> Aspid.MVVM separates **View**, **ViewModel**, and business logic in Unity, generating the binding glue at build time so bindings use **zero reflection** and minimal allocations.

## Why it exists

Classic MVVM in Unity drags in reflection-based binding (slow, GC-heavy) and a pile of boilerplate (`INotifyPropertyChanged`, backing fields, command wrappers). Aspid moves that work to **build time** via Roslyn source generators, so the runtime path is plain method calls.

## The three layers

- **ViewModel** — a `partial` class marked `[ViewModel]`. Fields marked `[Bind]` become observable bindable members; methods marked `[RelayCommand]` become `IRelayCommand` properties. The generator emits the [[IViewModel]] implementation. → [[ViewModel Generation]]
- **View** — a component holding [[Binders Catalog|binders]]. Each binder is a small object connecting one UI element (a `TMP_Text`, `Image`, `Toggle`, …) to one bindable member, in a direction set by [[BindMode]].
- **Business logic** — plain C# the ViewModel calls; the framework never reaches into it.

## How a change flows

ViewModel field changes → bindable member raises → bound binders update the View (and, for `TwoWay`/`OneWayToSource`, View edits flow back). Direction is per-binding via [[BindMode]]. The full build-time walkthrough is [[ViewModel to Generated Code]].

## Where the pieces live

- Core contracts & attributes: `Source/` (small, stable, high-value) — [[IViewModel]], [[IRelayCommand]], [[IBinder]].
- Ready-made binders & converters: `StarterKit/` (~73% of the code) — [[Binders Catalog]].
- Code generation: three git submodules (source generator, Unity generators, analyzer) — see [[Source Generation Pipeline]].
- `Aspid.Collections` (observable collections) is an **external UPM package**, not in this repo — see [[External Dependencies]].

## What not to break

Generated types must be `partial` ([[Must Be Partial]]); generator DLLs are committed and consumed by Unity ([[Committed DLLs]]).
