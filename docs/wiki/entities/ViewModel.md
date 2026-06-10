---
title: ViewModel
type: entity
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Generation/ViewModelAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Generation/BindAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Generation/BaseBindAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Generation/BindAlsoAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Generation/AccessAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Generation/RelayCommandAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/IViewModel.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Extensions/ViewModelExtensions.cs
tags: [viewmodel, entity, attributes, source-generation]
updated_at: 2026-05-31
---

# ViewModel

> A `partial` class (or struct) marked `[ViewModel]` that holds `[Bind]` state and `[RelayCommand]` methods; the Source Generator turns it into an [[IViewModel]].

## Why it exists

A ViewModel is the unit a developer actually writes: the presentation-side state and behavior for a screen, widget, or list item. It lets you express *what* the UI exposes (fields, commands) without hand-writing the change-notification plumbing, property wrappers, or member-lookup that a [[View]] needs to bind against. The generator removes that boilerplate, so a ViewModel stays a plain class focused on logic.

## How it works

You mark a type with `ViewModelAttribute` (valid on `class` or `struct`, `Inherited = false`). Inside it you annotate members:

- `[Bind]` on a field — generator emits a bindable property. Default mode is `BindMode.TwoWay` for mutable fields, `BindMode.OneTime` for `readonly` fields; a mode argument overrides this (see [[BindMode]]). `[Bind]` derives from `BaseBindAttribute`; sibling shortcuts are `[OneWayBind]`, `[TwoWayBind]`, `[OneTimeBind]`, `[OneWayToSourceBind]`.
- `[RelayCommand]` on a method — generator emits a matching [[IRelayCommand]] property (a generic overload is picked by parameter count). Its `CanExecute` field names a `bool`-returning gate method. `AllowMultiple = true`.
- `[BindAlso("PropertyName")]` — also raises another generated property's change event when this field changes (requires a companion bind attribute).
- `[Access(...)]` — overrides the generated property accessors' default `private` modifier.

Because the generator emits a *second* partial part of the type, the class **must be `partial`** (see [[Must Be Partial]]). The emitted part implements [[IViewModel]] — chiefly `FindBindableMember(...)`, the runtime hook a View uses to locate members. The mechanics of that emission live in [[ViewModel Generation]] and [[ViewModel to Generated Code]].

`ViewModelExtensions.DisposeViewModel(...)` provides a lifecycle helper: it disposes the ViewModel if it implements `IDisposable`, wrapped in a profiler marker. *(Inference: disposal is opt-in — `[ViewModel]` does not itself implement `IDisposable`.)*

## Key relationships

- Implements [[IViewModel]] (the contract) via generated code.
- `[Bind]` members surface as [[Bindable Members]] consumed through [[Data Binding]].
- `[RelayCommand]` methods become [[Relay Commands]] / [[IRelayCommand]].
- Bound to UI by a [[View]] during [[View Initialization]] / [[Runtime Binding Resolution]].
- Pre-built ready ViewModels live under [[StarterKit ViewModels]].

## Source

- `Source/ViewModels/Generation/ViewModelAttribute.cs` — the marker.
- `Source/ViewModels/Generation/` — `Bind`, `RelayCommand`, `BindAlso`, `Access`, mode shortcuts.
- `Source/ViewModels/IViewModel.cs`, `Extensions/ViewModelExtensions.cs`.
