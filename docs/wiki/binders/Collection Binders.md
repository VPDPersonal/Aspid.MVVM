---
title: Collection Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Collections/CollectionBinderBase.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Collections/Lists/ObservableListBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Collections/Dictionaries/ObservableDictionaryBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Collections/ObservableLists/ObservableListMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Collections/Collections/ViewModel/CollectionViewModelBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Collections/ObservableLists/ViewModel/ObservableListViewModelBinder.cs
tags: [binder, collection, observable, starterkit]
updated_at: 2026-05-31
---

# Collection Binders

> The binder family that turns a bound collection (observable list, dictionary, or read-only collection) into live View output — spawning, reusing, and tearing down child Views as items are added, removed, replaced, moved, or reset.

## What it binds

These binders implement [[IBinder]]`<IReadOnlyCollection<T>>` (and list/dictionary variants) instead of a single value. The bound member is a collection on a [[ViewModel]]; typically a `tech.aspid.collections` observable collection (see [[External Dependencies]]). The binder subscribes to that collection's `CollectionChanged` event and forwards each change to the View. On unbind/dispose it unsubscribes to prevent handler leaks.

## Shared base classes

All variants extend a small set of abstract bases that own subscription plumbing and expose `protected abstract` hooks — `OnAdded`, `OnRemoved`, `OnReplace`, `OnMove`, `OnReset` — for subclasses to implement (see [[Binder Base Classes]]):

| Base | Bound type | Notable behavior |
|---|---|---|
| `CollectionBinderBase<T>` | `IReadOnlyCollection<T>` | `IDisposable`; reset-then-replay on any change; `OnReplace`/`OnMove` reorder slots |
| `ObservableListBinder<T>` | `IReadOnlyList` / `IReadOnlyObservableList` / `IReadOnlyFilteredList<T>` | Granular index-aware add/remove; `GetFilterList` hook to wrap input |
| `ObservableDictionaryBinder<TKey,TValue>` | `IReadOnlyObservableDictionary` | Granular key/value events; `Move` throws `NotImplementedException` |

## Variant axes

- **Plain Binder vs MonoBinder.** Plain bases extend [[IBinder|Binder]] (created in code, e.g. inside a [[View]]). The MonoBinder variants (`ObservableListMonoBinder<T>`, `CollectionViewModelMonoBinder`) extend [[Mono Binders|MonoBinder]] so they can be wired in the Inspector and emit `[BinderLog]` diagnostics.
- **Base hook binder vs ViewModel-distributing concrete binder.** The bases above are abstract. The shipped concrete binders bind a collection of [[IViewModel]] and render one child View per item:
  - `CollectionViewModelBinder<T>` — distributes items across a **fixed, pre-instantiated `T[]` array** of Views, activating/deactivating slots; rejects [[BindMode|BindMode.TwoWay]].
  - `ObservableListViewModelBinder<T, TViewFactory>` — **instantiates/releases** Views on demand via an `IViewFactory<T>`, with optional filter/comparer (sorting). Convenience subclasses default `T = MonoView` and the standard factory.
- **Filtering/sorting.** `ObservableListBinder<T>.GetFilterList` lets a subclass wrap the source in an `IReadOnlyFilteredList<T>`; the factory-based ViewModel binder accepts filter and comparer types directly.

> Switcher / Enum / EnumGroup / OneWayToSource axes do not apply here — those belong to scalar binder families. Collection binders are inherently one-way ([[BindMode|OneWay]]) source-to-View.

## Notable behavior

- Initial state is **replayed**: existing items are forwarded to `OnAdded` immediately on `SetValue`.
- Non-granular sources (plain `IReadOnlyList`, filtered lists) trigger a full **reset-then-readd** on every change; observable lists/dictionaries use fine-grained per-item events.
- For per-item Views, child View lifecycle uses [[View Initialization]] (`Initialize`/`Deinitialize`); related per-item rendering also appears in [[VirtualizedList Binders]].

## Source

- Plain bases: `StarterKit/Runtime/Binders/Collections/`
- Mono + ViewModel binders: `StarterKit/Unity/Runtime/Binders/Collections/`

See also [[StarterKit ViewModels]], [[Binders Catalog]].
