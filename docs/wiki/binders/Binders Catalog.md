---
title: Binders Catalog
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders
tags: [binders, catalog]
updated_at: 2026-05-31
---

# Binders Catalog

> Landing page for the StarterKit binders — ~593 files across ~33 UI-element categories. Documented **per category, not per file** (a per-class page would be 500+ duplicate stubs).

## What a binder is

A small object connecting **one** View UI element to **one** ViewModel bindable member, in a direction set by [[BindMode]]. The ViewModel exposes members via [[IViewModel]]; binders resolve and subscribe to them.

## Variant axes (recur across every category)

- **Plain vs `…MonoBinder`** — plain binders are constructed in code; `Mono` variants are `MonoBehaviour`-backed and configured in the Unity Inspector.
- **`Switcher`** — selects between values based on the bound value.
- **`Enum` / `EnumGroup`** — drive a property from an enum.
- **`OneWayToSource`** — push View → ViewModel (e.g. input fields).

## Categories

Text ([[Text Binders]]), Images, RawImages, Toggles, Sliders, Scrollbars, Scrollrects, Dropdowns, InputFields, Buttons, Selectables, Transforms, Animators, AudioSources, CanvasGroups, Graphics, Renderers, LineRenderers, Layouts, GameObjects, Objects, Colliders, Behaviours, EventTriggers, UnityEvents, LocalizeStringEvents, VirtualizedLists, Collections, Casters, Generics, Debugs, Mono helpers.

> Per-category pages are written during ingest; unresolved links above mark pages still to create.

## Related

- Value transforms applied inside binders: [[Converters]].
- How binders attach at startup: [[View Initialization]].

## Source

`StarterKit/Unity/Runtime/Binders/` (one folder per category) and the pure-C# binder bases under `StarterKit/Runtime/Binders/`.
