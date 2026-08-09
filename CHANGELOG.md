# Changelog

All notable changes to **Aspid.MVVM** are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

> 🌐 Русская версия: [CHANGELOG.ru.md](CHANGELOG.ru.md)

---

## [Unreleased]

### Changed

- **`Aspid.MVVM Settings`** window restyled to match the Aspid.FastTools **Welcome** window — animated dot background, animated logo (links to the Asset Store) and title, themed cards, gradient `Apply` / `Revert` buttons and a footer with version and links.
- Moved the settings window into the shared `Tools/Aspid 🐍` top-menu submenu, next to `Welcome FastTools`.
- Settings window version is now read from the package manifest instead of a hard-coded constant; `AspidToggle` colors aligned with the theme.
- `[SerializeReference]` fields in the `MonoView` / `MonoViewModel` / `MonoBinder` inspectors are now drawn with the FastTools type-picker dropdown instead of Unity's default managed-reference UI. The inspectors route them through `SerializeReferenceEditorGUI`, so no `[TypeSelector]` attribute is needed on any field and the candidate set is the field's own declared type. Nested managed references *inside* an assigned instance keep Unity's default UI — FastTools draws an instance's own children with a plain `PropertyField`.
- `Aspid.FastTools` is no longer embedded under `Packages/`. It is consumed as a UPM git dependency pinned to the immutable per-release tag `upm-preview/1.0.0-rc.7`, up from the embedded `1.0.0-rc.4`. rc.6 brought `[TypeSelector]` support for `[SerializeReference]` fields — the replacement for the removed `SerializeReferenceDropdown` integration — and rc.7 adds three type-picker fixes this project depends on: a candidate the field cannot close is no longer offered, generic arguments are inferred through the field's interfaces, and Unity's built-in types are accepted as generic arguments. Two upstream API renames were followed: the `Aspid.FastTools.Reflection` namespace collapsed into `Aspid.FastTools`, and `SerializedProperty.GetClassInstance()` became `GetDeclaringInstance()`.
- `GenericToString<TFrom>` exposes formatting as a virtual `Format` hook instead of hard-coding the decision in `Convert`. An empty format now falls back to `ToString()` rather than producing an empty string, and `Format` still receives the typed value, so numeric and date specifiers (`{0:F2}`, `{0:hh\:mm}`) keep working. `formatEmptyValues` moved down to `StringFormatConverter`, the only converter with an opinion about blank input; `ObjectToStringConverter` and `TimeSpanToStringConverter` gained the format constructor they were missing.
- Inspector attributes (`[SerializeField]`, `[SerializeReference]`, `[Tooltip]`) in the Unity-independent layers are no longer wrapped in `#if UNITY_2022_1_OR_NEWER`. No-op stubs in `Source/Compatibility/UnityAttributesShim.cs` stand in for them outside Unity, so 22 preprocessor blocks across 14 files could be dropped. Directives guarding real Unity API (`Debug`, `Component`, `ProfilerMarker`) are unchanged.

### Fixed

- A binder added through the inspector now starts in a mode its own `[BindModeOverride]` allows. The serialized `_mode` began at `BindMode.TwoWay`, which the field's own `[BindMode(OneWay, OneTime)]` forbids, so an ordinary binder added and left alone was rejected by the bindable member it pointed at. The default is now per-type: `MonoBinder` exposes a virtual `DefaultMode` and applies it from Unity's `Reset` callback. No single constant would do — the 30 `*ToSourceMonoBinder` types allow only `OneWayToSource` and two `*ByBindMonoBinder` types only `OneTime`, so flipping the constant to `OneWay` would merely move the breakage. Binders created with `AddComponent` at runtime still get the raw field initializer.
- A destroyed `MonoBinder` now unbinds itself. `MonoBinder` had no `OnDestroy`, so a binder removed independently of its View — pooling, or a `Destroy` on a child object — stayed subscribed to the bindable member: the ViewModel kept a managed reference to a dead `MonoBehaviour` and kept calling it, so a destroyed binder went on writing to its target. Unbinding is now driven from `OnDestroy`; override it and call `base.OnDestroy()` to keep that behaviour. `CommandMonoBinder` needed no separate change — its `OnUnbound` already releases the command.
- `Renderer` material binders no longer throw when the material set is empty or null. `SetMaterials` assigned `renderer.materials = null`, which Unity rejects with `ArgumentNullException` from `SetMaterialArray`; an untouched `Material[]` field in the inspector is exactly that case, so the binder failed on its first value. The single-material branch now assigns a one-element array instead of clearing first.
- `LineRenderer` color binders no longer throw on their own default. `GetColor` rejected `LineRendererColorMode.StartAndEnd` — the MonoBinder's default — and `BindMode.OneWayToSource` reads the property back the moment binding is established, so the View's initialization aborted before the ViewModel saw a value. That mode keeps both endpoints in step, so the start color is now returned.
- `TextFontSwitcherBinder` and `DropdownOptionsSwitcherBinder` are marked `[Serializable]`. Unity does not inherit the flag from a base class, so both were invisible in the inspector and arrived at `Bind` as `null` fields — the binding silently did nothing. Their 53 siblings were marked.
- `SliderCommandBinder` now accepts `IRelayCommand<int>`, `IRelayCommand<long>` and `IRelayCommand<double>`. All four arities declared only `IBinder<IRelayCommand<float>>` while exposing `SetValue` overloads for the other three numeric types, so binding a non-float command threw `BinderInvalidCastException` and the whole View failed to initialise. `ScrollbarCommandBinder` already declared all four interfaces.
- `SetValue(Vector3)` called directly on a Vector3 binder no longer drops the Z component. `ComponentVector3MonoBinder<TComponent>` and `TargetVector3Binder<TTarget>` declared `SetValue(Vector2)` while inheriting `SetValue(Vector3)` from their base; C# builds the overload candidate set from the most derived type that declares an applicable member and looks no further, and `Vector3` converts implicitly to `Vector2`, so the 2D overload won and Z was silently zeroed. Both classes now redeclare `SetValue(Vector3)`. Binding through a ViewModel was never affected — it dispatches via `IBinder<Vector3>`, whose interface map points at the base method.
- Ten Transform binders now write to the component assigned in the inspector instead of the binder's own `transform`. `TransformScaleMonoBinder`, `TransformRotationMonoBinder` and the `Switcher` / `Enum` variants of Scale, Position, EulerAngles and Rotation dereferenced the inherited `Component.transform`, so a binder pointed at a child moved, scaled or rotated its own GameObject and — in `BindMode.OneWayToSource` — reported its own value back to the ViewModel. Their siblings in the same families already used `CachedComponent`.
- `BindMode.OneWayToSource` now reaches the ViewModel for the numeric binders' own type. `TargetFloatBinder` / `ComponentFloatMonoBinder` publish `float`, and `TargetIntBinder` / `ComponentIntMonoBinder` publish `int`, through the `ValueChanged` event inherited from `TargetBinder<TTarget, TProperty>` / `ComponentMonoBinder<TComponent, TProperty>`: a class member wins over a default interface implementation, so the bridge `INumberReverseBinder` declares for that instantiation never applied, and `OnBound` did not chain to `base.OnBound()`. A ViewModel field of the binder's own numeric type silently kept its default value while the other three numeric types worked — retyping the field made the same binder appear to start working. The native event is now an alias of the inherited one and `OnBound` calls the base implementation.
- Private `[SerializeReference]` fields no longer disappear from the `MonoView` / `MonoViewModel` / `MonoBinder` inspectors. The reflected field map admitted a private field only when it carried `[SerializeField]`, so a polymorphic field resolved to a null `FieldInfo` and was skipped for every property — a binder's `_converter` or `_customInteractable` was simply not drawn.
- Converters that can only be built in code are no longer offered by the type picker. `GenericFuncConverter` and the private types behind `ToConvert` / `ToConvertSpecific` wrap a delegate no inspector can supply, so picking one produced an instance with a null delegate; they now carry `[TypeSelectorDisplay(Hidden = true)]`.

### Removed

- Internal `FloatingBackgroundElement`, superseded by the FastTools animated dot background.
- `SerializeReferenceDropdown` integration: the `com.alexeytaranov.serializereferencedropdown` dependency, the `[SerializeReferenceDropdown]` attributes on `[SerializeReference]` fields, the assembly references and the `ASPID_MVVM_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION` version defines. A replacement built into Aspid.FastTools will take its place.

## [1.1.0-beta.1] — 2026-06-06

First preview cut of `1.1.0`, published to the `upm-preview` channel. The API is largely stabilised but may still change before the final `1.1.0` release.

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
- MVVM package moved from `Plugins/Aspid/` to `Assets/Aspid/` (PR #77), then promoted to an embedded local UPM package under `Packages/tech.aspid.mvvm` (PR #117).
- `package.json` placed inside the package; `unity` field set to `6000.0`, `unityRelease` pinned; version `1.1.0-beta.1`.
- Samples shipped under `Samples~` and registered in `package.json`: HelloWorld, Stats, TodoList, VirtualizedList, plus the Counter / Greeter walkthroughs.
- Root `CLAUDE.md` describing structure and conventions.
- GitHub Actions: Claude PR Assistant + Code Review workflows (PR #64).
- GitHub Actions: Release workflow publishing stable (`upm`) and preview (`upm-preview`) UPM subtrees with immutable `upm/<version>` tags, generator-DLL drift verification and CHANGELOG-driven release notes (PR #78); the Readme gained matching Stable / Preview version badges.

#### Integrations / dependencies
- `Aspid.FastTools` integrated (PR #26) and later embedded as a local UPM package under `Packages/tech.aspid.fasttools`; many editor visuals migrated to FastTools equivalents.
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
