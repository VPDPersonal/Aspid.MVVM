---
title: VirtualizedList Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/VirtualizedLists/ItemSource/VirtualizedListItemSourceBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/VirtualizedLists/ItemSource/Mono/VirtualizedListItemSourceMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/VirtualizedLists/OneWayToSource/Mono/VirtualizedListToSourceMonoBinder.cs
tags: [binders, starterkit, virtualizedlist, collections]
updated_at: 2026-05-31
---

# VirtualizedList Binders

> StarterKit binders that feed a `VirtualizedList` component an observable list of child `IViewModel`s, with optional filtering and sorting.

## Why it exists

A `VirtualizedList` renders only the visible slice of a large collection, recycling item views as the user scrolls. These binders connect that component to a ViewModel property of type `IReadOnlyList<IViewModel>`, so each child view model drives one list item. This keeps large lists cheap and lets the View layer stay declarative. See [[Collection Binders]] for the non-virtualized counterparts.

## Family table

| Binder | Base | Target / binds | Mode |
| --- | --- | --- | --- |
| `VirtualizedListItemSourceBinder` | `TargetBinder<VirtualizedList>` | `IReadOnlyList<IViewModel>` -> `VirtualizedList.ItemsSource` | OneWay / OneTime (not TwoWay) |
| `VirtualizedListItemSourceMonoBinder` | `ComponentMonoBinder<VirtualizedList>` | same, as a MonoBehaviour | OneWay / OneTime |
| `VirtualizedListToSourceMonoBinder` | `ComponentToSourceMonoBinder<VirtualizedList>` | pushes current value back to the ViewModel | OneWayToSource |

## Variant axes

- **Plain vs Mono.** The plain `VirtualizedListItemSourceBinder` is a `[Serializable]` field-level binder constructed in code; the `MonoBinder` variant is a `MonoBehaviour` ([[Mono Binders]]) configured in the Inspector, with `[AddComponentMenu]` / `[AddBinderContextMenu]` entries under `Aspid/MVVM/Binders/UI/VirtualizedList/`. Both implement `IBinder<IReadOnlyList<IViewModel>>` and share identical `SetValue` logic.
- **OneWayToSource.** `VirtualizedListToSourceMonoBinder` is an empty subclass of `ComponentToSourceMonoBinder<VirtualizedList>` — the source-binding behavior lives entirely in that base class ([[Binder Base Classes]]).
- This family has **no** Switcher / Enum / EnumGroup variants (unlike most other binder categories).

## Notable behavior

- **Filter + comparer.** Both ItemSource binders expose `[SerializeReference]` `_filter` and `_comparer` fields. When either is set, `SetValue` wraps the incoming list in a `FilteredList<IViewModel>` (from the external `Aspid.Collections.Observable.Filtered` package — see [[External Dependencies]]) before assigning `ItemsSource`. The wrapper is disposed on rebind and on `OnUnbound`, which also nulls `ItemsSource`.
- **Unity-version aliases.** `Filter` / `Comparer` resolve to generic `ICollectionFilter<IViewModel>` / `ICollectionComparer<IViewModel>` on Unity 2023.1+, falling back to non-generic interfaces on older versions.
- The MonoBinder's `SetValue` carries `[BinderLog]`, so its assignments appear in [[Debug Binders|binder logging]] (inferred from the attribute name).
- `TwoWay` is rejected at construction (`mode.ThrowExceptionIfTwo()`); see [[BindMode]].

## Source

`StarterKit/Unity/Runtime/Binders/VirtualizedLists/` — `ItemSource/` (plain + `Mono/`) and `OneWayToSource/Mono/`. Compiled into the [[Committed DLLs|committed StarterKit assembly]]. Related: [[StarterKit ViewModels]], [[IBinder]], [[IViewModel]].
