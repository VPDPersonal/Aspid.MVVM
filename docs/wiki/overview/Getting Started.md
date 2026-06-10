---
title: Getting Started
type: overview
status: active
source_paths:
  - Readme.md
  - "Aspid.MVVM/Assets/Aspid/MVVM/Samples/01. Counter/Scripts/CounterViewModel.cs"
  - "Aspid.MVVM/Assets/Aspid/MVVM/Samples/01. Counter/Scripts/CounterView.cs"
  - "Aspid.MVVM/Assets/Aspid/MVVM/Samples/01. Counter/Scripts/Bootstrap.cs"
tags: [overview, onboarding, install, sample]
updated_at: 2026-05-31
---

# Getting Started

> Install Aspid.MVVM, then wire your first source-generated ViewModel to a View and binders — no reflection, no boilerplate.

## Install

Aspid.MVVM ships as a UPM package targeting Unity 2022.3+. The generator and analyzer are committed [[Committed DLLs]] consumed by Unity directly, but the *repo* depends on three git [[Submodule Init|submodules]]. Clone with:

```bash
git submodule update --init --recurse
```

Without submodules, Unity won't compile. The observable collections come from an [[External Dependencies|external UPM package]] (`tech.aspid.collections`), not this repo.

## Your first ViewModel

A ViewModel is a plain `partial` class — no base class. `[ViewModel]` triggers the [[Source Generation Pipeline]] (see [[ViewModel to Generated Code]]):

```csharp
[ViewModel]
[Serializable]
public sealed partial class CounterViewModel
{
    [Bind] private int _count;

    [RelayCommand] private void Increment() => Count++;
    [RelayCommand] private void Reset()     => Count = 0;
}
```

The generator emits what you never write: a public `Count` property over `_count` (see [[Bindable Members]]), the [[IViewModel]] implementation, and an `IncrementCommand` / `ResetCommand` of type [[IRelayCommand]] (see [[Relay Commands]]). `[Bind]` can take a [[BindMode]] to restrict allowed [[Data Binding]] directions.

> The class **must be `partial`** — the generator emits a second partial half. See [[Must Be Partial]].

## Your first View

A View is a `partial` [[View|MonoView]] marked `[View]`. It exposes `MonoBinder` fields the inspector fills; `[RequireBinder]` constrains the bound type:

```csharp
[View]
public sealed partial class CounterView : MonoView
{
    [RequireBinder(typeof(int))]            [SerializeField] private MonoBinder[] _count;
    [RequireBinder(typeof(IRelayCommand))]  [SerializeField] private MonoBinder _incrementCommand;
}
```

Each [[IBinder]] (from the [[Binders Catalog]]) connects a bindable member to a Unity component (Text, Button, etc.).

## Wire it up

Construct the ViewModel and hand it to the View. Initialization resolves bindings at runtime ([[Runtime Binding Resolution]], [[View Initialization]]):

```csharp
private void Awake() => _counterView.Initialize(new CounterViewModel());
private void OnDestroy() => _counterView.DeinitializeView()?.DisposeViewModel();
```

For container-driven wiring instead of manual `new`, see [[DI Integration]].

## Next steps

- [[Samples|01. Counter]] — the full minimal example above (ViewModel + View + Bootstrap).
- HelloWorld and the other [[Samples]] — progressively richer scenes.
- [[Architecture]] — how View, ViewModel, and binders fit together.

## Source

- `Samples/01. Counter/Scripts/` — `CounterViewModel.cs`, `CounterView.cs`, `Bootstrap.cs`
- `Readme.md` — feature overview and install links
