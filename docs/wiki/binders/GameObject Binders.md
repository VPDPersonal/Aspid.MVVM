---
title: GameObject Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/GameObjects
tags: [binders, gameobject]
updated_at: 2026-05-31
---

# GameObject Binders

> Binders that drive a `GameObject` itself — its active state, its tag, and instantiation — from a ViewModel member.

## What's in the family

| Sub-target | Binds | Primary binder |
|---|---|---|
| Visible | active state via `GameObject.SetActive` (`activeSelf`) | `GameObjectVisibleBinder` |
| Tag | the `GameObject.tag` | `GameObjectTagBinder` |
| Instantiate | addressable instantiation into the scene | `GameObjectInstantiateAddressableMonoBinder` |

All derive from `TargetBinder<GameObject>` — the base that holds the target and applies values per [[BindMode]].

## Variant axes

- **Plain vs `Mono`** — plain binders are constructed in code; `…MonoBinder` are `MonoBehaviour`-backed and configured in the Inspector. → [[Binders Catalog]]
- **`Switcher`** — `GameObjectTagSwitcherBinder` selects a tag from the bound value.
- **`Enum` / `EnumGroup`** — `GameObjectVisibleEnumMonoBinder`, `GameObjectTagEnumMonoBinder`, etc. drive the target from an enum.

## Notable behavior

- `GameObjectVisibleBinder` carries `[BindModeOverride(OneWay, OneTime, OneWayToSource)]` and **rejects `TwoWay`**. As an `IReverseBinder<bool>` it supports [[BindMode|OneWayToSource]]: on bind it sends the current `activeSelf` back to the ViewModel and raises `ValueChanged`.
- It exposes an `_isInvert` flag that inverts the bound `bool` before applying it — handy for "hide when true" bindings.
- The bound member is an ordinary [[Bindable Members|bindable member]] on the [[ViewModel]]; the binder reads it, no generator output lives here.

## Source

`StarterKit/Unity/Runtime/Binders/GameObjects/` (sub-folders `Visible/`, `Tag/`, `Instantiate/`, each with a `Mono/` subfolder).
