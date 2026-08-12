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

- `NaN` and infinities no longer slip past a clamp into the component. `Mathf.Clamp` and `Mathf.Clamp01` are written as comparisons, and every comparison against `NaN` is false, so a non-finite value passed through all 48 clamped binder sites untouched — into `alpha`, `fillAmount`, `pitch`, `spatialBlend`, `panStereo` and the rest. A ViewModel only has to divide by zero once to produce one, and the symptom surfaces later and elsewhere: a handle that will not move, a graphic that vanishes. The new `BinderMath.SafeClamp` / `SafeClamp01` map non-finite input to the lower bound and to `0`. The six integer `priority` binders are untouched — `int` cannot be `NaN`.
- The input field binders no longer subscribe to the field from `OnValidate` while unbound. That callback re-wires the subscriptions after the bind mode is changed in the inspector during play mode, but it did so without checking whether the binder was bound — and `Unbind` returns immediately when it is not, so `OnUnbound`, and with it the unsubscribe, never ran: the listener stayed on the field with nothing left to remove it. Repeating the inspector edit stacked duplicates as well, since `UnityEvent` accepts the same listener more than once, and every user keystroke then reached the ViewModel twice. Five arities across the text and command binders.
- A `*MonoBinder` on a GameObject that has no component for it to drive now refuses to bind. `MonoBinder.IsBind` returned `true` unconditionally and no component binder overrode it, so binding succeeded and the first value threw from inside a leaf class's property setter — a message naming neither the binder nor the GameObject. `ComponentMonoBinder` now answers `IsBind` from the resolved component, which is the guard the serializable `TargetBinder` has had all along.
- Turning off "disabled when null" no longer force-enables the component. All 14 sprite and texture binders wrote `enabled = !_disabledWhenNull || value`, which is unconditionally `true` once the option is off — so instead of leaving the component alone, the binder switched it on again on every value. The option now guards the assignment. The serializable variants default the option to `false`, so a binder configured in a `MonoView` hit this on every update.
- `GraphicMaterialBinder` and `GraphicMaterialSwitcherBinder` can now be constructed for any `Graphic`. Both are declared for `Graphic` but their only constructor took a `RawImage`, so an `Image` or a `Text` could not be bound from code at all. The XML docs said `RawImage` too.
- Removed the `NotImplementedException` override in `SwitcherFloatBinder`. It suppressed the compiler's demand that subclasses implement `SetValue`, and was unreachable — all 16 shipped subclasses override it. Deleting it restores the compile-time check and matches `SwitcherIntBinder`.
- An integer Animator parameter now changes when the value changes by one. Both int binders compared the incoming value with the current one through `Mathf.Approximately`, whose tolerance is *relative*: past roughly a million it exceeds 1, so a change of one counted as no change and the write was skipped; past 2^24 the two operands are indistinguishable as `float` at all, and the parameter could never update again. They now compare as integers.
- `OnEnable` no longer resets an Animator parameter to zero. It re-applied the stored value unconditionally, and that value starts at `default(T)` — so enabling a binder before it had received anything wrote a zero over whatever the ViewModel had set, without telling it. Re-applying now waits until a value has actually arrived, and both reverse paths record the value they push, so the restored state is what the ViewModel last asked for.
- `TwoWayValue<T>` no longer feeds a ViewModel update straight back to the ViewModel. `IBinder<T>.SetValue` assigned through the `Value` property, whose setter is the View-side entry point and raises the reverse channel — so every update travelling ViewModel → View turned around immediately and travelled back, carrying the **converted** value. The model was overwritten with what the display shows, and a converter that is not idempotent kept going until the generated setter's equality check happened to stop it. `SetValue` now writes the backing field; setting `Value` from the View still notifies as before.
- The converter overload of `OneWayToSourceValue<T>` is marked `[Obsolete]`. The inherited converter is applied on the ViewModel → View path, which this mode does not have, so one passed here was silently ignored and values reached the ViewModel unchanged. The overload still compiles — it now warns instead of doing nothing quietly.
- The binders that accept any bound type no longer throw on a `null` value. `UnityEventStringMonoBinder.SetValue<T>` called `value.ToString()` and both debug log binders dereferenced the value to format it. That path — `IAnyBinder` — is the one taken whenever a binder has no overload for the member's own type, which is every reference type other than the ones spelled out, and a bindable member of such a type publishes `null` the moment the binder is added. So `null` was the first thing these binders saw, not an edge case. The event now forwards an empty string and the log prints `null`.
- `new DebugLogBinder()` now gets the default converter its own documentation promises. The constructor assigned its parameter straight over the field initializer, so the parameterless call left the binder with no converter at all — the opposite of "pass `null` to use `ObjectToStringConverter`".
- The collider material binders no longer replace the assigned asset with a clone just by reading it. `Collider.material` is an instancing property: reading it makes Unity swap in a private copy named `"… (Instance)"` and keep it until the collider is destroyed. Both binders read it in `BindMode.OneWayToSource`, so the ViewModel received a clone that no longer compared equal to the asset it had handed over — `material == iceAsset` was false — and every distinct collider bound this way left one behind. The two getters now read `sharedMaterial`. The setters are unchanged: assigning does not clone, and assigning `sharedMaterial` would edit the asset for every other collider using it.
- A binder whose serialized target has been destroyed now refuses to bind instead of throwing from `OnBound`. The constructor's `ArgumentNullException` guard never runs for a serialized instance — Unity assigns `Target` directly — and the only protection was `IsBind => Target is not null` in 25 command binders, written with C#'s null check, which a destroyed `UnityEngine.Object` passes: the managed wrapper outlives the native object. Every other `TargetBinder` descendant had no protection at all. The check now lives in `TargetBinder` itself and uses Unity's own conversion, so all of them get it, and the 25 hand-written overrides are gone.
- A destroyed `MonoBinder` in a View's array is now treated as an empty slot. `BindSafely` and `UnbindSafely` tested each element with `is null`, so a binder destroyed independently of its View was handed to `Bind`, which threw `MissingReferenceException` from the first Unity API it touched. The same three-line check now covers both cases.
- 89 constructor docs across 23 files no longer understate their `BindMode` constraint. They promised only that `BindMode.TwoWay` was refused, while the guard they run — `ThrowExceptionIfTwo` — rejects `BindMode.OneWayToSource` as well, because `IsTwo()` covers both. Anyone following the documentation would pick `OneWayToSource` for a reverse binding and get an exception the docs said could not happen. The wording now names both modes, and a test walks the inheritance graph and fails if the two ever diverge again.
- `UnityGenericOneWayBinder` now validates its bind mode, as its own documentation already claimed. Its non-Unity twin `GenericOneWayBinder` calls `ThrowExceptionIfTwo`; the Unity variant called nothing, so a `TwoWay` or `OneWayToSource` mode was accepted and then failed later and less clearly, at the bindable member that could not find a reverse binder. Its constructors were also `protected` while the twin's are `public`, which left a public, non-abstract class that no consumer could instantiate; both are now `public`.
- Collider extents and `AudioSource.timeSamples` are no longer written out of range. Unity refuses a non-finite collider extent on its own, but stores a negative `BoxCollider.size` or `SphereCollider`/`CapsuleCollider.radius` silently and leaves the physics engine with inverted geometry; those are now raised to `0` through the new `BinderMath.NonNegative`. `AudioSource.timeSamples` likewise stores a negative position or one past the end of the clip, and `AudioSource.time` refuses such a seek — but refuses it loudly, with an audio-engine error per assignment, leaving the playhead where it was. Bound to a seek slider or a `progress * duration` calculation with a stale duration, that is an error per frame while the control appears not to work. Both playback properties now clamp to the current clip, and skip the write when no clip is assigned. 30 sites across the Collider, Time and TimeSamples families.
- A binder whose target component reference is empty or broken now falls back to `GetComponent`, as its documentation always promised. `ComponentMonoBinder.CachedComponent` tested the serialized field with `is not null`, but an unassigned or dangling Unity object reference reaches managed code as a wrapper that is not `null` to C# while pointing at nothing — so the fallback was skipped and every caller received a component it could not use. Delete the target component and the binder went dead instead of re-resolving; `OnValidate` had the same test, so the inspector never healed it either. Both now decide with Unity's own conversion. This sits under every `*MonoBinder` in the framework.
- `EnumGroup` binders no longer throw on an entry whose element is unassigned. `SetValue` walked the table and dereferenced each element, so one empty slot — an ordinary state while editing the table — raised a `NullReferenceException` from inside the loop and the entries after it never received their value, leaving the group in a mixed state. Empty entries are now skipped, naming the enum key once per binder, and the rest of the group is still updated. This is one change in the shared base, so all 71 `EnumGroup` binders get it.
- Animator binders no longer address a parameter their controller does not have. The name is serialized and nothing checked it, so a typo or an empty field produced a Unity error on every single assignment — 60 a second in the editor for a value that changes per frame, and complete silence in a build with the animation simply not moving. The binders now verify the name and the parameter type once per controller, refuse the assignment and report it once. Two paths that skipped the check entirely were closed at the same time: `OnEnable`, which re-applied the last value directly, and the plain `Action` handed to the ViewModel in `OneWayToSource`, which bypassed `CanExecute` while the `IRelayCommand` path honoured it.
- A command binder set to `InteractableMode.Custom` with no `ICanExecuteView` assigned now reports the misconfiguration instead of throwing. The programmatic path was guarded — the constructor rejects a null view and refuses `Custom` without one — but `_interactableMode` and `_customInteractable` are serialized, so the inspector let the mode be chosen with the reference left empty, and the first `CanExecuteChanged` threw a `NullReferenceException` naming neither the binder nor the object. Because the throw happened inside a `CanExecuteChanged` handler, it also cut the notification short for every other subscriber of that command.
- `InteractableMode.Visible` on the `*CommandMonoBinder` variants now hides the control it was pointed at rather than the binder's own GameObject. The branch called `gameObject.SetActive` while the serializable twins used `Target.gameObject`; since the target `Selectable` is a serialized field that may live on any object, a binder placed beside its control hid itself and left the control visible and clickable. The `Interactable` branch in the same switch already used `CachedComponent`.
- All 50 of those switches — 12 files across Buttons, Toggles, Sliders, Scrollbars, Dropdowns and InputFields — were replaced by one `Selectable.SetInteractable` extension, which also names the binder when the target `Selectable` itself is missing or destroyed.
- A slider or audio source can no longer be left with its range inverted. `SetMinMax` and `SetMinMaxDistance` assigned the two endpoints in order, and neither Unity property enforces `min <= max`, so a `Vector2` arriving as `(10, 2)` — or a `Min`/`Max` mode update crossing the endpoint already on the component — produced a slider whose `normalizedValue` reads backwards and an audio source silent at every distance. Both now swap a crossed pair and log which one they saw; a non-finite endpoint is refused outright rather than written. Distances are additionally floored at `0`.
- `AudioSource.dopplerLevel` binders no longer pass `NaN` to the component. All six were missed by the clamp audit above because they had no clamp to convert: Unity enforces the 0..5 range inside the property setter, which hid that it lets a non-finite value straight through, corrupting the doppler effect for the whole source. They now route through `BinderMath.SafeClamp`.
- One failing binder no longer takes the rest of the collection with it. `BindSafely` and `UnbindSafely` were safe only with respect to `null` elements — the loop called into each binder unguarded, so the first exception abandoned every binder after it. On bind that left the View half-initialised; on unbind, which `MonoView.OnDestroy` drives, it left the remaining binders subscribed to a ViewModel that was going away. Each loop now reports the failure with the owner, member name and index, and carries on.
- Replacing a dropdown's options no longer leaves a stale caption. The `IEnumerable<OptionData>` overloads of both dropdown options binders mutated `TMP_Dropdown.options` directly, bypassing the `RefreshShownValue` that `AddOptions` and `ClearOptions` perform — the MonoBinder showed an empty caption and the serializable twin kept showing the previous set's text.
- The dropdown's selected index now survives an options update. `ClearOptions` resets it to 0 (or -1 with a placeholder) and raises nothing, so a ViewModel holding the previous index silently disagreed with the control after every refresh. The selection is now restored, clamped to the new list, without a notification.
- The View → ViewModel channel of the Toggle, Slider and InputField binders no longer goes permanently silent after an exception. Each suppresses its own echo by clearing a flag around the assignment — the component raises `onValueChanged` synchronously and the binder must not read its own write as user input — but the flag was restored without `try`/`finally`. An exception from any other listener on that event, or from a converter, left it cleared for good: the binder went on applying values yet never reported a single user edit again, with nothing in the log to explain it. All six sites are now wrapped.
- `CollectionMonoBinder<T>` now tracks the collection it is bound to. It applied the contents once at `SetValue` and never subscribed to `CollectionChanged`, so the View stopped following the list the moment it was bound — adds and removes simply did not show. Its serializable twin `CollectionBinderBase<T>` had subscribed from the start. Change notifications rebuild the View; `OnUnbound` unsubscribes.
- `CollectionViewModelMonoBinder<T>` no longer runs past the end of its serialized view array when the bound collection holds more items than there are views. The bounds check its serializable twin already had was missing.
- A view inserted anywhere but the end of an observable list now takes the matching sibling index. The factory parents each new object last in the hierarchy, so the visual order — and any LayoutGroup driven by it — drifted away from the model. `OnMove` in the same helper already did this.
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
