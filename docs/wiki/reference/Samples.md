---
title: Samples
type: reference
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Samples/01. Counter/Scripts
  - Aspid.MVVM/Assets/Aspid/MVVM/Samples/02. Greeter/Scripts
  - Aspid.MVVM/Assets/Aspid/MVVM/Samples/HelloWorld/MVVM/Scripts
  - Aspid.MVVM/Assets/Aspid/MVVM/Samples/Stats/Scripts/ViewModels/StatsViewModel.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Samples/TodoList/Scripts/Todos/Storages/TodoStorageViewModel.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Samples/VirtualizedList/Scripts/ViewModels/ListViewModel.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/package.json
tags: [samples, learning, examples]
updated_at: 2026-05-31
---

# Samples

> Six runnable scenes, ordered from trivial to advanced, that teach the framework one feature at a time — read these before the source.

The bundled samples live under `Assets/Aspid/MVVM/Samples/`. Four (HelloWorld, Stats, TodoList, VirtualizedList) are registered as importable UPM samples in `package.json`; the numbered folders (`01. Counter` … `06. VirtualizedList`) are a tutorial-ordered restructuring (newly added — git-tracked but not yet in the UPM manifest, so likely a 1.1.0 work-in-progress).

## What each one teaches

| Sample | Demonstrates |
|--------|--------------|
| **Counter** | Smallest possible loop: `[Bind] int _count` plus three `[RelayCommand]` methods (`Increment`/`Decrement`/`Reset`). [[View]] declares `MonoBinder[]` slots with `[RequireBinder(typeof(int))]`. |
| **Greeter** | A `MonoViewModel` (ViewModel that lives on a MonoBehaviour), two-way text input, a `partial void OnNameChanged` change hook recomputing `Greeting`, and a custom `IConverterString` (`PaintNameConverter`). |
| **HelloWorld** | Same task built three ways — `General` (plain), `MVP`, and `MVVM` — so you can contrast architectures. The MVVM `SpeakerViewModel` is heavily commented and is the canonical "what does the generator emit" reference. |
| **Stats** | Many `[OneWayBind]` fields, `[RelayCommand(CanExecute = …)]` gating, and `NotifyCanExecuteChanged()` driven by change hooks. Read-only vs editable views over one [[IViewModel]]. |
| **TodoList** | An observable collection synced to child ViewModels via `CreateSync`, `[OneTimeBind]`/`[TwoWayBind]` members, search filtering, and a reusable edit dialog. |
| **VirtualizedList** | Pooled rendering of large lists using `ObservableList` + `FilteredList`, `IComponentInitializable`, and `IViewModelCollectionFilter` for runtime filter/sort. |

## How they fit together

Every sample follows the same shape: a `Bootstrap` MonoBehaviour news up the ViewModel and calls `view.Initialize(viewModel)`, then `DeinitializeView()?.DisposeViewModel()` on destroy. The View is `[View] partial : MonoView` exposing serialized `MonoBinder` slots; the ViewModel is `[ViewModel] partial` whose `[Bind]`/`[RelayCommand]` members generate the properties, `Set…` methods, `…Changed` events, and `…Command` properties the binders consume at runtime (see [[ViewModel to Generated Code]]).

Progression mirrors the learning path: Counter (binds + commands) → Greeter (MonoViewModel + converters + hooks) → HelloWorld (architecture contrast) → Stats (`CanExecute`) → TodoList → VirtualizedList (collections). HelloWorld is the recommended first read.

Collection samples depend on the external [[External Dependencies|tech.aspid.collections]] UPM package (`Aspid.Collections.Observable.*`), not on repo source.

## Source

- `Assets/Aspid/MVVM/Samples/` — all sample folders
- `Assets/Aspid/MVVM/package.json` — UPM `samples` manifest

## See also

[[Getting Started]] · [[Data Binding]] · [[Relay Commands]] · [[Converters]] · [[VirtualizedList Binders]] · [[Collection Binders]] · [[BindMode]]
