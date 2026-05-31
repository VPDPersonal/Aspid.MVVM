---
title: BindMode
type: concept
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Mode/BindMode.cs
tags: [binding, bind-mode]
updated_at: 2026-05-31
---

# BindMode

> `BindMode` sets the **direction of data flow** between a ViewModel member and a View element for a single binding.

## The modes

| Mode | Value | Direction |
|---|---|---|
| `None` | 0 | No binding (default enum value). |
| `OneWay` | 1 | ViewModel → View. View edits do **not** reach the ViewModel. |
| `TwoWay` | 2 | Both directions. |
| `OneTime` | 3 | ViewModel → View **once**; later ViewModel changes are ignored. |
| `OneWayToSource` | 4 | View → ViewModel only. ViewModel changes do **not** reach the View. |

## Why it matters

Direction is a per-binding decision, not a global one. A label is usually `OneWay`; an input field that edits state is `TwoWay` or `OneWayToSource`; a value set at init and never changed is `OneTime` (cheapest — no ongoing subscription).

## How it's chosen

- `[Bind]` with no argument: `TwoWay` for mutable fields, `OneTime` for `readonly` fields. See [[ViewModel Generation]].
- Many [[Binders Catalog|binders]] constrain the modes they accept — e.g. [[Text Binders|TextBinder]] rejects `TwoWay` (a label has nothing to write back). Such binders throw at construction on an unsupported mode.

## Source

`Source/Mode/BindMode.cs` (enum); `Source/Mode/Extensions/` (validation helpers).
