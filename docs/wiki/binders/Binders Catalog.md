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

[[Text Binders|Text]], [[Image Binders|Images]], [[RawImage Binders|RawImages]], [[Toggle Binders|Toggles]], [[Slider Binders|Sliders]], [[Scrollbar Binders|Scrollbars]], [[Scrollrect Binders|Scrollrects]], [[Dropdown Binders|Dropdowns]], [[InputField Binders|InputFields]], [[Button Binders|Buttons]], [[Selectable Binders|Selectables]], [[Transform Binders|Transforms]], [[Animator Binders|Animators]], [[AudioSource Binders|AudioSources]], [[CanvasGroup Binders|CanvasGroups]], [[Graphic Binders|Graphics]], [[Renderer Binders|Renderers]], [[LineRenderer Binders|LineRenderers]], [[Layout Binders|Layouts]], [[GameObject Binders|GameObjects]], [[Object Binders|Objects]], [[Collider Binders|Colliders]], [[Behaviour Binders|Behaviours]], [[EventTrigger Binders|EventTriggers]], [[UnityEvent Binders|UnityEvents]], [[LocalizeStringEvent Binders|LocalizeStringEvents]], [[VirtualizedList Binders|VirtualizedLists]], [[Collection Binders|Collections]], [[Caster Binders|Casters]], [[Generic Binders|Generics]], [[Debug Binders|Debugs]], [[Mono Binders|Mono helpers]].

> Each category links to its own page; every binder in a family shares the variant axes above.

## Related

- Value transforms applied inside binders: [[Converters]].
- How binders attach at startup: [[View Initialization]].

## Source

`StarterKit/Unity/Runtime/Binders/` (one folder per category) and the pure-C# binder bases under `StarterKit/Runtime/Binders/`.
