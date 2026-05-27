# Changelog

All notable changes to **Aspid.MVVM** are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

## [1.1.0] — 2026-04-28

### Highlights

- Editor inspectors for `MonoBinder`, `MonoView`, `MonoViewModel` rewritten on top of UI Toolkit / `VisualElement`.
- Brand-new `ViewModelDebugPanel` with tabs, persistent search, `RelayCommand` support and bindable / auto-property support.
- `Aspid.MVVM Settings` window prototype with `AspidToggle` and shared styling.
- Bindable Properties supported in the source generator; new `NotifyCanExecuteChangedAll()`.
- `GeneralView` merged into `MonoView` — single non-abstract base view.
- New `ValueViewModel`, `AnyReverseBinder`, `OneWayToSourceComponentBinders`, AudioSource / LayoutGroup / Dropdown / Selectable / Object-Name binders.
- `Aspid.UnityFastTools` integrated, many editor visuals migrated to FastTools equivalents.
- All sub-projects extracted into git submodules (`Aspid.MVVM.Generators`, `Aspid.MVVM.Analyzers`, `Aspid.MVVM.Unity.Generators`); `Aspid.Collections` later moved to a UPM git package.
- Target Unity raised to `6000.3.0f1` (minimum stays `2022.3`).
- Mass XML documentation pass over the entire binder catalog plus `XmlDocConventions.md`.

### Added

#### ViewModel & Generator
- **Bindable Properties** support in the source generator (PR #46) — usable in code, Debug panel and samples (Todo sample updated).
- **`NotifyCanExecuteChangedAll()`** generator method (PR #52, fixed in #54).
- **`ValueViewModel`** — minimal ViewModel wrapper around a single value with full XML docs (PR #63).
- Keyword field support in the generator (PR #55).
- `EmptyExecution` static instance on `RelayCommand` (PR #36); try/catch in `RelayCommandField` (PR #43).
- Interface support for `ViewModel` (`IMyVm` can now be picked as a design ViewModel) (PR #53).
- Profiler markers expanded across binder lifecycle (PR #15).
- **Virtual binder fields** — generator auto-emits `MonoBinder[]` slots for `IView<TViewModel>` bindable members that are not declared on the View. Opt out via `[View(AutoBinderFields = false)]`; `ScriptableObject`-derived views are always skipped (PR #74, generator PR `Aspid.MVVM.Generators#13`).

#### Views
- **`GeneralView`** added (PR #43), then merged into `MonoView` (PR #48). `MonoView` is now non-abstract and self-contained.
- `RelayCommand` support inside `View` / `MonoView`; `CommandsContainer` refactor; `CommandContainer in View` (PR #43).
- `ViewInitializer` overhaul (PR #41, #50) — context for `ViewInitializers`.
- `DestroyView` mode in editor; `DestroyViewModel` extension fixes (PR #43, #53).
- `PrefabViewFactory` / `PrefabViewPool` upgraded.
- `ViewModelPickerWindow` with dropdown + improved navigation (PR #53).
- `[AddComponentMenu]` for `MonoView`; snake-style for settings menu (PR #47).
- `MonoView` editor refactor; fixed generated fields and base inspector display (PR #32).
- `DesignViewModel` upgrade (PR #53) including legacy Unity support.

#### Editor / Inspector
- New UI Toolkit inspectors for `MonoBinder`, `MonoView`, `MonoViewModel` (PR #31, #32, #35).
- `InspectorHeaderPanel`, `AspidInspectorHeader`, `AspidPropertyField`, `AspidDividingLine` shared visuals (PR #32, #40).
- USS-driven theme: `AspidToggle` (PR #47), IMGUI foldout drawer margin fix, IMGUIContainer wrapping in styled `AspidPropertyField`.
- `EnumMonoBinderEditor` (PR #57); `EnumValuesPropertyDrawer` fixes; `EnumValues` sample and `ComponentTypeSelector` documentation.
- Drag & Drop for unassigned and general binders (groups + Auto-Assign + Select / Restore buttons) (PR #43).
- `RequireBinder` and child View / Binder validation (alpha) (PR #43).
- `Aspid.MVVM Settings` window prototype (PR #47).
- **`HeaderGroup` foldout attributes** — `HeaderGroupAttribute` (single field), `HeaderGroupStartAttribute` / `HeaderGroupEndAttribute` (range) tag binder fields and VM members into named, collapsible inspector foldouts. New `HeaderGroupRouter` is consumed by `MonoViewVisualElement` / `AspidBaseInspectorVisualElement` instead of inline foldout layout. Stripped from non-`DEBUG` / non-`UNITY_EDITOR` builds (PR #74).

#### ViewModel Debug Panel (PR #45)
- Rewritten on UI Toolkit, with tabs.
- Search with persistence and improved logic; type-based search.
- `RelayCommand` support (`RelayCommandField`, correct meta containers).
- Bindable property and auto-property support.
- New styles: `Debug field`, `DisableTextFields`, `DebugStringField`.

#### Binders — new
- LayoutGroup binders (PR #56).
- AudioSource binders (PR #59).
- `OneWayToSourceComponentBinders` (PR #58).
- `AnyReverseBinder` with nullable support (PR #37).
- Object Name binders (PR #34).
- Additional InputField binders + large refactor (PR #51).
- Dropdown / Selectable binders (PR #61).
- `Addressable` binders gained an opt-in seamless swap mode.
- `GameObjectInstantiateAddressableMonoBinder` for prefab spawning via Addressables.

#### Binders — improvements
- `OnReplace` / `OnMove` events forwarded to binder hooks; batch `Replace` unrolled into per-item `OnReplace` calls.
- General binder upgrade (PR #60).
- `BindSafely` / `UnbindSafely` enriched with View + bindable Id; new `owner` / `memberName` overloads.
- `EventTriggerCommandMonoBinder`, `ImageSpriteSwitcherBinder`, `MonoBinderPropertyField` — fixes.
- `VirtualizedListItemSourceBinder` Dispose / lifecycle fixes.
- `ViewModelObservableListBinder` fixes.
- `MonoBinderVisualElement` polish; binder-in-script visualizations and animation upgrade.
- `BindMode` support for `VisualElement` (PR #39).
- `IAnyBinder` BinderLog support.

#### Collections
- `Aspid.Collections` initially extracted into a git submodule, then replaced with a UPM git package (`tech.aspid.collections`) (PR #79).
- `FilteredList` and `BindAlso` fixes.
- New collections tests.
- `Replace` / `Move` notifications surfaced to binders.

#### Project structure / infrastructure
- Submodules wired in (PR #38): `Aspid.MVVM.Generators`, `Aspid.MVVM.Analyzers`, `Aspid.MVVM.Unity.Generators`.
- `Aspid.Internal.Unity` submodule added, then removed — functionality absorbed into the main project.
- Unity project relocated from repo root into `Aspid.MVVM/`.
- MVVM package moved from `Plugins/Aspid/` to `Assets/Aspid/` (PR #77).
- `package.json` placed inside the package; `unity` field set to `2022.3`; version `1.1.0`.
- Root `CLAUDE.md` describing structure and conventions.
- GitHub Actions: Claude PR Assistant + Code Review workflows (PR #64).
- GitHub Actions: Release workflow for stable and preview UPM branches with DLL verification (PR #78).

#### Integrations / dependencies
- `Aspid.UnityFastTools` integration `0.0.1-alpha.3` (PR #26); subsequent bumps to `alpha.5` / `alpha.7`.
- `Aspid.MVVM.Generators`, `Aspid.MVVM.Analyzers`, `Aspid.Collections`, `Aspid.UnityFastTools` updated to current heads.
- `SerializeReferenceDropdown` updated to `1.2.7`.
- `Roboto-Bold SDF` font refreshed.
- Editor target lifted to `6000.2.7f2` (PR #24) and then to `6000.3.0f1`. Unity 2022 stays supported.

#### Documentation
- `XmlDocConventions.md` added and refined.
- Mass XML doc pass across all binder families: AudioSource, CanvasGroup, Collider, Animator, Behaviour, GameObject, Layout, UnityGeneric, Selectable, Graphic, Image, RawImage, Renderer, Transform, Slider, InputField, Toggle, Button, EventTrigger, ScrollBar, ScrollRect, Dropdown, Object, LineRenderer, Casters, LocalizeStringEvent, VirtualizedList plus base `MonoBinder` / Behaviour subfolders (PR #62).
- XML docs for converters.
- `ComponentTypeSelector` documentation and `EnumValues` sample.
- `Readme.md` relocated (PR #77) and tweaked (PR #71).

### Changed (behaviour)

- `MonoView` is no longer `abstract`; it is a concrete component with its own serialized binders list and `[RequireBinder]` validation. Existing subclasses still work (PR #48).
- `MonoView.Dispose()` no longer destroys the host GameObject — it only calls `Deinitialize()`. Call `Object.Destroy(gameObject)` explicitly if needed (PR #48).
- `MonoBinder.Bind()` no longer throws when called on an already-bound binder; it logs an error and returns instead (PR #62).
- `[AddComponentMenu]` paths reorganized — for example `Collections/Observable List Binder - ViewModel` → `Collection/Observable List Binder – ViewModel` (singular form, en-dash).

### Removed

- `AddComponentContextMenuAttribute` — replaced by `AddBinderContextMenuAttribute` / `AddBinderContextMenuByTypeAttribute` with a different signature (`Path = "..."` named property).
- `AddPropertyContextMenuAttribute`.
- Standalone `Aspid.Collections` source under the package — now consumed via submodule.

### Renamed (StarterKit class names)

`.meta` GUIDs are preserved, so prefabs and scenes continue to bind to the correct script. **Game code referencing the old class names will not compile until updated.**

| 1.0 | 1.1 |
|-----|-----|
| `ViewModelObservableListMonoBinder` | `ObservableListViewModelMonoBinder` |
| `ViewModelObservableListBinder` | `ObservableListViewModelBinder` |
| `ViewModelObservableDictionaryBinder` | `ObservableDictionaryViewModelBinder` |
| `ViewModelCollectionMonoBinder` | `CollectionViewModelMonoBinder` |
| `EnumMonoBinderEditorBase` | `EnumMonoBinderEditor` |

### Fixed

- `MonoView`: generated field handling, validation, infinite-loop validation.
- `ViewInitializers` — multiple fixes (PR #41, #50).
- `AddressableMonoBinder` — multiple fixes.
- `EnumValue`, `EnumValuesPropertyDrawer`, `SerializableTypeDrawer`, `SerializePropertyExtensions`.
- `BindAlso`, `MonoBinders`, base `Binder` — assorted fixes (PR #62).
- `EnumValue` validation downgraded from exception to `Debug.LogError`.
- Removed stray `Header("Target")` and obsolete files (`NewFields`, `Commands.meta`, `DebugViewModel`).
- `#if !ASPID_MVVM_EDITOR_DISABLED` guards added in missing places.
- Numerous meta-file fixes.
- `NumberToBoolConverter`: `Inequality` comparison was inverted — always returned the same result as `Equal` (PR #81).
- `CollectionBinderBase.Dispose()`: `CollectionChanged` subscription was not unsubscribed, causing callbacks to fire on disposed binders and preventing GC. `Dispose()` now delegates to `SetValue(null)` which handles full cleanup (PR #91).
- `CollectionBinderBase.OnCollectionChanged`: granular `Add`, `Remove`, and `Reset` events are now forwarded to the new abstract hooks `OnAdded(T?)`, `OnAdded(IReadOnlyList<T?>)`, `OnRemoved(T?)`, `OnRemoved(IReadOnlyList<T?>)` (PR #94).

### Migration

See [MIGRATION.md](MIGRATION.md) for a full upgrade checklist from 1.0 to 1.1.

---

## [1.0.0] — initial public release

Baseline release. Subsequent entries describe changes relative to 1.0.
