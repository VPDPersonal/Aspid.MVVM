---
title: StarterKit ViewModels
type: reference
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/ViewModels/EmptyViewModel.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/ViewModels/ValueViewModel.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/ViewModels/Dynamic/DynamicViewModel.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/ViewModels/Dynamic/DynamicViewModel.Create.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/ViewModels/Dynamic/DynamicPropertyData.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/ViewModels/Dynamic/DynamicPropertyFactory.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/ViewModels/Dynamic/IDynamicProperty.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/ViewModels/Dynamic/TwoWayDynamicProperty.cs
tags: [starterkit, viewmodel, dynamic, reference]
updated_at: 2026-05-31
---

# StarterKit ViewModels

> Ready-made [[IViewModel]] implementations so you can bind without hand-writing a `[ViewModel]` class — for one-off values, runtime-defined property sets, or an explicit no-op.

## Why it exists

The framework's normal path is a `[ViewModel]`-decorated class whose bindable members are emitted by the [[Source Generation Pipeline|Source Generator]] (see [[ViewModel to Generated Code]]). That is ideal for fixed schemas. The StarterKit adds three pragmatic alternatives for cases where authoring a full class is overkill or impossible.

## The three view models

### ValueViewModel\<T> (and \<T1,T2>, \<T1,T2,T3>, \<T1,T2,T3,T4>)
A generated view model holding 1–4 independent bindable values named `Value` / `Value1`…`Value4`, each marked `[TwoWayBind]`. Because the class is `[ViewModel] partial`, the generator emits the matching bindable members and the `OnValuePropertyChanged()` notify hooks the setters call (invisible in source — see [[Bindable Members]]). Setters optionally short-circuit via `EqualityComparer<T>.Default` when the constructor's `checkEquality` is `true`. Use it when a screen needs to expose a couple of values without a dedicated class.

### DynamicViewModel
A `sealed` view model whose members are not known at compile time. It holds a `Dictionary<string, IDynamicProperty>` and resolves bindings by id at runtime: `FindBindableMember` looks up `parameters.Id` and returns the property's `IBinderAdder` (see [[Runtime Binding Resolution]]). Missing ids return `default`, or throw `ArgumentException` when `throwErrorIfNotFindProperty` is `true`. An implicit operator converts a dictionary straight to a `DynamicViewModel`.

The static `Create<T1…T8>(...)` overloads build the dictionary from `DynamicPropertyData<T>` entries (each carrying `Id`, `Value`, and a [[BindMode]]); an optional leading `bool` sets the throw behavior. `DynamicPropertyFactory` maps the mode to a concrete `IDynamicProperty`:

| BindMode | Property type | Backing member |
|----------|--------------|----------------|
| `OneTime` | `OneTimeDynamicProperty<T>` | `OneTimeBindableMember<T>` |
| `OneWay` | `OneWayDynamicProperty<T>` | `OneWayBindableMember<T>` |
| `TwoWay` | `TwoWayDynamicProperty<T>` | `TwoWayBindableMember<T>` |

`None` and `OneWayToSource` are rejected by `DynamicPropertyData`'s constructor. Note `DynamicPropertyData<T>` is a `readonly ref struct`, so it is stack-only and meant for passing to `Create` inline.

### EmptyViewModel
A `sealed` no-op: `FindBindableMember` always returns `default`. Useful as a non-null placeholder where a [[View]] expects an [[IViewModel]] but no data should bind yet.

## Key relationships

- Implements [[IViewModel]]; consumed by [[View]] during [[View Initialization]].
- Property modes mirror [[BindMode]] and wrap the same bindable-member types described in [[Bindable Members]].
- `DynamicViewModel` is the runtime-flexible counterpart to generator-driven [[ViewModel Generation]].

## Source

`Assets/Aspid/MVVM/StarterKit/Runtime/ViewModels/` — `ValueViewModel.cs`, `EmptyViewModel.cs`, and `Dynamic/`.
