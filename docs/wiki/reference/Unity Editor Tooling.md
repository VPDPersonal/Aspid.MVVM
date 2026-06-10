---
title: Unity Editor Tooling
type: reference
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Unity/Editor/Scripts/Binders/MonoBinderEditor.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Unity/Editor/Scripts/Views/ViewEditor.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Unity/Editor/Scripts/Views/ViewAndMonoBinderSyncValidator.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Unity/Editor/Scripts/Binders/AddBinderContextMenu.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Unity/Editor/Scripts/ViewModels/Debugs/DebugViewModelPanel.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Unity/Editor/Scripts/Settings/AspidMvvmSettings.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Unity/Editor/Scripts/Settings/AspidMvvmSettingsWindow.cs
tags: [reference, unity, editor, inspector, debug, uitoolkit]
updated_at: 2026-05-31
---

# Unity Editor Tooling

> The Editor-only layer (~108 files under `Unity/Editor/`) that turns raw serialized fields into the guided binder/view inspectors, the runtime ViewModel debug panel, and the settings window.

All Editor code is wrapped in `#if !ASPID_MVVM_EDITOR_DISABLED` and lives in `Aspid.MVVM.Unity.Editor.asmdef`. Inspectors are built with UI Toolkit (UIElements); styling comes from the `.uss` sheets under `Editor/Resources/Styles/`. This is tooling for editing [[View]]/[[Binder Base Classes|MonoBinder]] components and inspecting generated [[ViewModel]] state — it does not run in builds.

## Custom inspectors (`CustomEditor`)

- **Binder inspector** — `MonoBinderEditor` (applies to all [[Binder Base Classes|MonoBinder]] subclasses via `editorForChildClasses`). Renders the **ID** and **View** dropdowns instead of plain string/object fields, recolors them when a value falls back to a "previous" entry, and keeps the chosen [[Bindable Members|bindable member]] id valid against the resolved [[View]].
- **View inspectors** — generic `ViewEditor<T,TEditor>` base with `MonoViewEditor` / `ScriptableViewEditor` concrete editors; drive the binder-list UI and surface unassigned binders.
- **ViewModel inspectors** — `MonoViewModelEditor` / `ScriptableViewModelEditor` over a `ViewModelEditor` base.

## Custom property drawers

`BindModeDrawer` draws the [[BindMode]] enum; `AspidSlider`/`AspidSliderInt`/`AspidToggle`/`AspidDelegateField` are styled UIElements field variants reused across inspectors.

## View/binder synchronization

`ViewAndMonoBinderSyncValidator` keeps a [[View]]'s required-binder slots in sync with the [[Binder Base Classes|MonoBinder]] components in its scene scope — adding, removing, or restoring references as the inspector changes. `ValidableBindersById` and `ViewAndMonoBinderSyncValidator` back the [[Runtime Binding Resolution|id-based resolution]] that wiring relies on.

## Context menu

`AddBinderContextMenu` registers an "Add Binder" right-click entry on serialized properties, scanning all assemblies for compatible binder types (driven by `AddBinderContextMenuAttribute` / `RequireBinderAttribute`). _(Inference: built from attribute scanning at script reload.)_

## ViewModel debug panel

`DebugViewModelPanel` reflects over a live [[IViewModel]] at play time and renders one debug field per member: generated [[Bindable Members|bind]] members, [[Relay Commands|relay commands]], and plain fields go to separate tabs, with a `t:`-prefixed search filter. The `Debug*Field` family (~45 files: `DebugIntegerField`, `DebugVector3Field`, `DebugEnumField`, `DebugRelayCommandField`, ...) covers each value type; backing fields of generated members are hidden.

## Settings

`AspidMvvmSettings` toggles three scripting-define symbols — profiler, binder log, and the `ASPID_MVVM_EDITOR_DISABLED` editor-checks switch; `AspidMvvmSettingsWindow` is the `EditorWindow` exposing them.

## Source

- `Editor/Scripts/Binders/` — `MonoBinderEditor`, drawers, dropdown data
- `Editor/Scripts/Views/` — view editors + sync validator
- `Editor/Scripts/ViewModels/Debugs/` — debug panel + `Debug*Field` family
- `Editor/Scripts/Settings/` — settings window

See [[View Initialization]], [[ViewModel to Generated Code]], [[Committed DLLs]].
