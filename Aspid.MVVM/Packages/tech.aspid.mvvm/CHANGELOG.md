# Changelog

All notable changes to **Aspid.MVVM** are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

> 🌐 Русская версия: [CHANGELOG.ru.md](CHANGELOG.ru.md)

---

## [Unreleased]

### Highlights

- Editor inspectors for `MonoBinder`, `MonoView`, `MonoViewModel` rewritten on top of UI Toolkit / `VisualElement`.
- Brand-new `DebugViewModelPanel` with tabs, persistent search, `RelayCommand` support and bindable / auto-property support.
- `Aspid.MVVM Settings` window prototype with `AspidToggle` and shared styling.
- Bindable Properties supported in the source generator; new `NotifyCanExecuteChangedAll()`.
- `MonoView` is now non-abstract — a single, self-contained base view.
- New `ValueViewModel`, `AnyReverseBinder`, OneWayToSource component binders (`…ToSourceMonoBinder` family), AudioSource / LayoutGroup / Dropdown / Selectable / Object-Name binders.
- `Aspid.FastTools` integrated, many editor visuals migrated to FastTools equivalents.
- All sub-projects extracted into git submodules (`Aspid.MVVM.Generators`, `Aspid.MVVM.Analyzers`, `Aspid.MVVM.Unity.Generators`); `Aspid.Collections` consumed as a UPM git package (`tech.aspid.collections`).
- Minimum Unity raised to `6000.0`.

### Added

#### ViewModel & Generator
- **Bindable Properties** support in the source generator (PR #46) — usable in code, Debug panel and samples (Todo sample updated).
- **`NotifyCanExecuteChangedAll()`** generator method (PR #52, #54) — emits backing-field names with a null-conditional guard, skips commands without a `CanExecute`, and includes `IRelayCommand`-typed members.
- **`ValueViewModel`** — minimal ViewModel wrapper around a single value with full XML docs (PR #63).
- Keyword field support in the generator (PR #55).
- `EmptyExecution` static instance on `RelayCommand` (PR #36, #93) — an executable command that does nothing; `GetSelfOrEmptyExecution` falls back to it when the command is null. Plus try/catch in `RelayCommandField` (PR #43).
- Interface support for `ViewModel` (`IMyVm` can now be picked as a design ViewModel) (PR #53).
- Generic enum / struct bindable members now resolve their effective type kind from generic-parameter constraints instead of defaulting to the class member type (PR #44).
- **Virtual binder fields** — generator auto-emits `MonoBinder[]` slots for `IView<TViewModel>` bindable members that are not declared on the View. Opt out via `[View(AutoBinderFields = false)]`; `ScriptableObject`-derived views are always skipped (PR #74, generator PR `Aspid.MVVM.Generators#13`).

#### Views
- `MonoView` is now non-abstract and self-contained — the inspector-driven binder list, child validation and `[RequireBinder]` integration live directly on it (PR #48).
- `RelayCommand` support inside `View` / `MonoView`; `CommandsContainer` refactor; `CommandContainer in View` (PR #43).
- `ViewInitializer` overhaul (PR #41, #50) — hoisted view/container resolution into `ViewInitializerBase`, lazy edit-mode `Views` / `ViewModel`, container `Resolve` switched to `TryResolve`, and a new `InitializeStage.DiConstructor` injection stage.
- `DestroyView` mode in editor; `DestroyViewModel` extension fixes (PR #43, #53).
- `PrefabViewFactory` / `PrefabViewPool` upgraded.
- `ViewModelPickerWindow` with dropdown + improved navigation (PR #53).
- `[AddComponentMenu]` for `MonoView`; snake-style for settings menu (PR #47).
- `MonoView` editor refactor; fixed generated fields and base inspector display (PR #32).
- `DesignViewModel` upgrade (PR #53) including legacy Unity support.

#### Editor / Inspector
- New UI Toolkit inspectors for `MonoBinder`, `MonoView`, `MonoViewModel` (PR #31, #32, #35).
- `AspidInspectorHeader`, `AspidPropertyField`, `AspidDividingLine` shared visuals (PR #32, #40).
- USS-driven theme: `AspidToggle` (PR #47), IMGUI foldout drawer margin fix, IMGUIContainer wrapping in styled `AspidPropertyField`.
- `EnumMonoBinderEditor` (PR #57); `EnumValuesPropertyDrawer` fixes; `EnumValues` sample and `ComponentTypeSelector` documentation.
- Drag & Drop for unassigned and general binders (groups + Auto-Assign + Select / Restore buttons) (PR #43).
- `RequireBinder` and child View / Binder validation (alpha) (PR #43).
- `Aspid.MVVM Settings` window prototype (PR #47).
- **`HeaderGroup` foldout attributes** — `HeaderGroupAttribute` (single field), `HeaderGroupStartAttribute` / `HeaderGroupEndAttribute` (range) tag binder fields and VM members into named, collapsible inspector foldouts. New `HeaderGroupRouter` is consumed by `MonoViewVisualElement` / `AspidBaseInspectorVisualElement` instead of inline foldout layout. Stripped from non-`DEBUG` / non-`UNITY_EDITOR` builds (PR #74).

#### ViewModel Debug Panel (PR #45)
- Rewritten on UI Toolkit, with tabs (`DebugViewModelPanel`).
- Search with persistence and improved logic; type-based search.
- `RelayCommand` support (`RelayCommandField`, correct meta containers).
- Bindable property and auto-property support.
- New styles: `Debug field`, `DisableTextFields`, `DebugStringField`.

#### Binders — new
- LayoutGroup binders (PR #56).
- AudioSource binders (PR #59).
- OneWayToSource component binders (`…ToSourceMonoBinder` family) (PR #58).
- `AnyReverseBinder` with nullable support (PR #37) — reverse binders now forward `null` reference values via `OnValueChanged(default)` instead of throwing (PR #95).
- Object Name binders (PR #34).
- Additional InputField binders + large refactor (PR #51).
- Dropdown / Selectable binders (PR #61).
- `Addressable` binders gained an opt-in seamless swap mode, with a destroyed-object guard in the async completion callback (PR #86).
- `GameObjectInstantiateAddressableMonoBinder` for prefab spawning via Addressables.

#### Binders — improvements
- `OnReplace` / `OnMove` events forwarded to binder hooks; batch `Replace` unrolled into per-item `OnReplace` calls.
- Reactive collection binders: `CollectionBinderBase<T>` now subscribes to `CollectionChanged` and forwards granular `Add`, `Remove`, and `Reset` events to the new abstract hooks `OnAdded(T?)`, `OnAdded(IReadOnlyList<T?>)`, `OnRemoved(T?)`, `OnRemoved(IReadOnlyList<T?>)` (PR #94), and cleanly unsubscribes on `Unbind` and `Dispose` (PR #88, #91).
- General binder upgrade (PR #60).
- `BindSafely` / `UnbindSafely` enriched with View + bindable Id; new `owner` / `memberName` overloads.
- `EventTriggerCommandMonoBinder`, `ImageSpriteSwitcherBinder`, `MonoBinderPropertyField` — fixes.
- `VirtualizedListItemSourceBinder` Dispose / lifecycle fixes.
- `ViewModelObservableListBinder` fixes.
- `MonoBinderVisualElement` polish; binder-in-script visualizations and animation upgrade.
- `BindMode` support for `VisualElement` (PR #39).
- `IAnyBinder` BinderLog support.

#### Collections
- `Aspid.Collections` is now consumed as a UPM git package (`tech.aspid.collections`) instead of being shipped as source under the package (PR #79).
- `FilteredList` and `BindAlso` fixes.
- New collections tests.
- `Replace` / `Move` notifications surfaced to binders.

#### Project structure / infrastructure
- Submodules wired in (PR #38): `Aspid.MVVM.Generators`, `Aspid.MVVM.Analyzers`, `Aspid.MVVM.Unity.Generators`.
- Unity project relocated from repo root into `Aspid.MVVM/`.
- MVVM package moved from `Plugins/Aspid/` to `Assets/Aspid/` (PR #77).
- `package.json` placed inside the package; `unity` field set to `6000.0`; version `1.1.0`.
- Root `CLAUDE.md` describing structure and conventions.
- GitHub Actions: Claude PR Assistant + Code Review workflows (PR #64).
- GitHub Actions: Release workflow for stable and preview UPM branches with DLL verification (PR #78).

#### Integrations / dependencies
- `Aspid.FastTools` integrated as a UPM package (PR #26); many editor visuals migrated to FastTools equivalents.
- `Aspid.MVVM.Generators`, `Aspid.MVVM.Analyzers`, `Aspid.Collections`, `Aspid.FastTools` updated to current heads.
- `SerializeReferenceDropdown` updated to `1.2.7`.
- `Roboto-Bold SDF` font refreshed.
- Editor target lifted to `6000.4.0f1`; minimum supported Unity raised to `6000.0`.

#### Documentation
- Mass XML doc pass across all binder families: AudioSource, CanvasGroup, Collider, Animator, Behaviour, GameObject, Layout, UnityGeneric, Selectable, Graphic, Image, RawImage, Renderer, Transform, Slider, InputField, Toggle, Button, EventTrigger, ScrollBar, ScrollRect, Dropdown, Object, LineRenderer, Casters, LocalizeStringEvent, VirtualizedList plus base `MonoBinder` / Behaviour subfolders (PR #62).
- XML docs for converters.
- `ComponentTypeSelector` documentation and `EnumValues` sample.
- `Readme.md` relocated (PR #77) and tweaked (PR #71).

### Changed

- `MonoView` is no longer `abstract`; it is a concrete component with its own serialized binders list and `[RequireBinder]` validation. Existing subclasses still work (PR #48).
- `MonoView.Dispose()` no longer destroys the host GameObject — it only calls `Deinitialize()`. Call `Object.Destroy(gameObject)` explicitly if needed (PR #48).
- `MonoBinder.Bind()` no longer throws when called on an already-bound binder; it logs an error and returns instead (PR #62).
- `[AddComponentMenu]` paths reorganized — for example `Collections/Observable List Binder - ViewModel` → `Collection/Observable List Binder – ViewModel` (singular form, en-dash).

### Removed

- `AddComponentContextMenuAttribute` — replaced by `AddBinderContextMenuAttribute` / `AddBinderContextMenuByTypeAttribute` with a different signature (`Path = "..."` named property).
- `AddPropertyContextMenu` attribute — no replacement; the new editor pipeline handles property menus internally.
- Standalone `Aspid.Collections` source under the package — now consumed via a UPM git package (`tech.aspid.collections`).

### Renamed (StarterKit class names)

`.meta` GUIDs are preserved, so prefabs and scenes continue to bind to the correct script. **Game code referencing the old class names will not compile until updated.**

| 1.0 | 1.1 |
|-----|-----|
| `ViewModelObservableListMonoBinder` | `ObservableListViewModelMonoBinder` |
| `ViewModelObservableListBinder` | `ObservableListViewModelBinder` |
| `ViewModelObservableDictionaryBinder` | `ObservableDictionaryViewModelBinder` |
| `ViewModelCollectionMonoBinder` | `CollectionViewModelMonoBinder` |

### Fixed

<!-- Only fixes for bugs that actually shipped in a released version (1.0.0–1.0.5) are listed here. Fixes for code introduced during 1.1.0 development are intentionally folded into their corresponding feature bullets above, not listed as standalone fixes. -->

- `NumberToBoolConverter`: the `Inequality` comparison was inverted — it returned the same result as `Equal` instead of its negation. It now returns `true` when the values are not approximately equal (PR #81).
- `DynamicViewModel.Create<…>`: the factory overloads passed only `DynamicPropertyData.Value`, which forced every property to `BindMode.OneTime` and discarded the user-specified `Mode`. The full `DynamicPropertyData` is now passed so the configured `BindMode` is honoured (PR #83).
- `MonoBinder.Unbind()`: the `ProfilerMarker` block was guarded only by `!ASPID_MVVM_UNITY_PROFILER_DISABLED`, breaking compilation on Unity earlier than 2022.1. It now also requires `UNITY_2022_1_OR_NEWER`, matching `Bind()` (PR #84).
- `VirtualizedList`: `OnAdded` / `OnRemoved` bounds-checked the computed view-pool index against `ItemsSource.Count` with a too-loose `<=`. The check now compares `viewIndex < _views.Length` so it picks `Refresh` vs `ResizeContent` correctly (PR #89).
- `ObservableListBinder`: `InitializeList` subscribed to `CollectionChanged` on the original `list` argument while `DeinitializeList` unsubscribed on `List` (which may be a filtered wrapper), leaking the subscription. The subscribe switch now uses `List` (PR #90).
- Slider / Scrollbar command binders: `OnCanExecuteChanged` reinterpreted the 4-byte `float` `Target.value` as the command's generic type `T` via `Unsafe.As`, causing out-of-bounds reads and garbage `CanExecute` values for `long` / `double` commands. Typed overloads now perform proper numeric casts via `ApplyCanExecute` (PR #92).
- Source generator: bindable members whose type was a generic type parameter fell through to the default class case and ignored `enum` / `struct` constraints. The generator now resolves the effective type kind from the parameter's constraints and emits the correct bindable member type (PR #44).

### Migration

See [MIGRATION.md](MIGRATION.md) for a full upgrade checklist from 1.0 to 1.1.

---

## [1.0.5] — 2025-10-17

### Added
- New TextMeshPro text binders: `TextFontBinder`, `TextFontSwitcherBinder`, `TextAlignmentBinder`, `TextAlignmentSwitcherBinder`, plus Mono variants (`TextFontMonoBinder`, `TextFontEnumMonoBinder`, `TextFontEnumGroupMonoBinder`, `TextFontSwitcherMonoBinder`) — for binding TMP font and alignment (PR #30).
- New Unity Localization binders: `LocalizeStringEventVariableBinder` (+ Mono variant), `TextLocalizationEntryBinder`, `TextLocalizationEntrySwitcherBinder` and Mono variants, with `TextLocalizationExtensions` (PR #29).
- Profiler markers and improved logging across `BindableMember` types and `BindMode` (`BindModeExtensions.Throw`, `LoggerHelper`) (PR #15).

### Changed
- Editor project updated to Unity `6000.2.7f2` (PR #28).
- Vendored the `com.unity.asset-store-tools` package into the repository (packaging only, no framework code change).

### Fixed
- `RectTransformSetters.SetSizeDelta` wrote the computed value to `anchoredPosition` instead of `sizeDelta`, repositioning the `RectTransform` instead of resizing it (PR #27).

---

## [1.0.4] — 2025-09-19

### Fixed
- Component context-menu generation (the "Add Component" entries produced via `AddComponentContextMenuAttribute`) — fix shipped in the rebuilt `Aspid.MVVM.Unity.Generators.dll` (PR #14).

---

## [1.0.3] — 2025-09-15

### Changed
- Moved Unity-layer types (`MonoBinder`, `MonoViewModel`, `MonoView`, `ScriptableView`, editor classes) from the `Aspid.MVVM.Unity` namespace into the root `Aspid.MVVM` namespace, to satisfy Asset Store packaging requirements (PR #13).

### Removed
- `MonoBinderExtensions` (the `BindSafely<T>(...)` helper overloads) and the `OnBindingDebug` / `OnUnbindingDebug` partial debug hooks on `MonoBinder` (PR #13).

---

## [1.0.2] — 2025-09-11

### Fixed
- ViewModel source generator fix — shipped in the rebuilt `Aspid.MVVM.Generators.dll` (PR #12).

---

## [1.0.1] — 2025-09-10

### Changed
- Reverted the C# language version from C# 10 back to C# 9 (removed `-langversion:10` from the `csc.rsp` files), aligning with Unity's default compiler (PR #11).
- `AddressableMonoBinder<TAsset>` reworked from a UniTask/async model (`LoadAssetAsync` / `CancellationToken`) to a synchronous `Addressables.LoadAssetAsync(...).Completed` callback, dropping the UniTask dependency for Addressable binders.
- `OneTimeBindableMember<T>` (and Enum / Struct variants) turned into a pooled singleton via a static `Get(value)` factory instead of allocating per bind.

### Fixed
- `ViewModelCollectionBinder` / `ViewModelCollectionMonoBinder` now deactivate (`SetActive(false)`) leftover pooled views beyond the current item count, so stale views are no longer left visible when the bound collection shrinks.

---

## [1.0.0] — 2025-08-09

Initial public release. Subsequent entries describe changes relative to 1.0.0.
