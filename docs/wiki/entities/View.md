---
title: View
type: entity
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Views/IView.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Views/Generation/ViewAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Views/Generation/AsBinderAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Views/Extensions/ViewExtensions.cs
tags: [view, mvvm, source-generation, binding]
updated_at: 2026-05-31
---

# View

> The presentation side of MVVM: a type that takes an [[IViewModel]] and wires its bindable members to UI binders. Marked `[View]`, most of its plumbing is source-generated.

## Why it exists

A ViewModel exposes state and commands but knows nothing about UI. The View is the bridge: it receives an [[IViewModel|ViewModel]], connects each [[Bindable Members|bindable member]] to one or more [[IBinder]] instances, and tears those connections down on deinitialization. Writing that wiring by hand for every screen is repetitive and error-prone, so Aspid generates it from a `[View]` attribute — keeping the View declaration focused on *which* binders it holds, not *how* they bind.

## How it works

`IView` defines the contract: a nullable `ViewModel` property plus `Initialize(IViewModel)` and `Deinitialize()`. `IView<in T>` adds a strongly-typed `Initialize(T)` overload for a specific ViewModel type.

You write a `partial` class (or struct) decorated with `[ViewAttribute|[View]]`. The Source Generator (a [[Committed DLLs|committed DLL]], not source in this repo) emits the `IView` / `IView<T>` implementation: the `ViewModel` backing field, `Initialize`/`Deinitialize` bodies, and the binding calls that hook each bindable member to its binder.

`[AsBinderAttribute|[AsBinder]]` decorates a field or property of a `[View]` type. It names the [[IBinder]] type (plus optional constructor `arguments`) the generator should use to bind that member. The `Type` reference is kept only under `UNITY_EDITOR || DEBUG`.

`ViewAttribute.AutoBinderFields` (default `true`) tells the generator to also emit binder fields for any `IView<TViewModel>` bindable members not already declared on the View — handy for inspector-driven layouts. Set it `false` for Views that wire binders manually.

`ViewExtensions` adds lifecycle helpers over any `IView`: `Reinitialize` (deinit then optionally init with a new ViewModel), `DeinitializeView` (deinit and return the old ViewModel), and `DisposeView` (dispose `IDisposable` views, else deinitialize). All return the previously associated ViewModel. Disposal is wrapped in Unity `ProfilerMarker`s when profiling is enabled.

## Key relationships

- Consumes an [[IViewModel]] passed into `Initialize`.
- Binds members to [[IBinder]] instances; see [[Runtime Binding Resolution]] and [[View Initialization]].
- `[View]` is processed by the [[Source Generator]]; see [[ViewModel to Generated Code]].
- Must be `partial` — see [[Must Be Partial]].
- Concrete UI binders live in the [[Binders Catalog]].

## Source

- `Source/Views/IView.cs`
- `Source/Views/Generation/ViewAttribute.cs`
- `Source/Views/Generation/AsBinderAttribute.cs`
- `Source/Views/Extensions/ViewExtensions.cs`
