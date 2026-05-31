---
title: Text Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Texts
tags: [binders, text, tmp]
updated_at: 2026-05-31
---

# Text Binders

> Binders that connect ViewModel members to TextMeshPro (`TMP_Text`) properties — the text itself, font, size, alignment, and localization.

## Why this is one page, not 28

The `Texts/` folder holds ~28 files, but they're the **same handful of ideas** repeated across sub-targets and variants. Documenting the *pattern* once is far more useful than 28 near-identical stubs. (Granularity rule: [[Binders Catalog]].)

## What's in the family

| Sub-target | Binds | Primary binder |
|---|---|---|
| Text | the string content (`TMP_Text.text`) | `TextBinder` |
| Font | the `TMP_FontAsset` | `TextFontBinder` |
| FontSize | the font size | `TextFontSizeBinder` |
| Alignment | text alignment | `TextAlignmentBinder` |
| Localizations | a localization entry | `TextLocalizationEntryBinder` |

## The variant axes (apply across the family)

- **Plain vs `Mono`** — plain binders are constructed in code; `…MonoBinder` are `MonoBehaviour`-backed so they're configured in the Unity Inspector. → [[Binders Catalog]]
- **`Switcher`** — picks between values based on the bound value.
- **`Enum` / `EnumGroup`** — drive the property from an enum (e.g. `TextEnumMonoBinder`).
- **`OneWayToSource`** — e.g. `TextToSourceMonoBinder`, pushing View → ViewModel.

## Notable behavior

- `TextBinder` sets `TMP_Text.text` and also implements `INumberBinder`, so numeric values are culture-formatted (via `CultureInfoMode`) and bound as text.
- It **rejects `TwoWay`** (`mode.ThrowExceptionIfMatches(BindMode.TwoWay)`) — a label has nothing to write back. See [[BindMode]].
- The whole family is gated on `UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION` (TextMeshPro dependency).

## Source

`StarterKit/Unity/Runtime/Binders/Texts/` (sub-folders: `Text/`, `Font/`, `FontSize/`, `Alignment/`, `Localizations/`, each with a `Mono/` subfolder).
