# Changelog

All notable changes to **Aspid.MVVM** are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

> 🌐 Русская версия: [CHANGELOG.ru.md](CHANGELOG.ru.md)

---

## [1.1.0] — Unreleased

Everything since `1.0.5`. Minimum Unity is now **`6000.0`**. Upgrade checklist: [MIGRATION.md](MIGRATION.md).
A preview, `1.1.0-beta.1`, was published on 2026-06-06 to the `upm-preview` channel.

### Highlights

- Source generator: bindable properties, `NotifyCanExecuteChangedAll()`, auto-emitted binder fields, `[GenerateSerializableBinder]`.
- StarterKit: binder types grown from ~360 to ~890, covering uGUI, TextMeshPro, UI Toolkit, 2D, physics, audio, animation, lighting and cameras.
- Converters: catalogue grown from 17 to ~190, `ITwoWayConverter`, `ConverterAsset`, composition primitives, culture-aware formatting.
- Editor: UI Toolkit inspectors, rebuilt `DebugViewModelPanel`, Settings window, FastTools type picker for `[SerializeReference]`.
- `MonoView` is concrete, `DynamicViewModel` is a typed property bag, `ViewInitializer` gained a DI construction stage.
- Package is an embedded UPM package; `Aspid.Collections` and `Aspid.FastTools` are UPM git dependencies; generators live in submodules.
- Documentation site on Docusaurus (`en` / `ru`), samples rebuilt as a learning path, ~3000 EditMode tests.

### Added

#### Core & generator

- **Bindable properties** — `[Bind]` on properties, not only fields.
- **`NotifyCanExecuteChangedAll()`** — refreshes every command with a `CanExecute`, including `IRelayCommand`-typed members.
- **Auto-emitted binder fields** — a `MonoBinder[]` slot for every bindable member of `IView<TViewModel>` not declared on the View; opt out with `[View(AutoBinderFields = false)]`.
- **`[GenerateSerializableBinder]`** — the serializable twin of a `MonoBinder` is generated; only the MonoBehaviour half is hand-written.
- `ValueViewModel` — a ViewModel around a single value.
- `RelayCommand.EmptyExecution` and `GetSelfOrEmptyExecution()` — a no-op command for a `null` slot.
- An interface can be picked as a design ViewModel.
- Generator accepts fields named with C# keywords.
- `ITwoWayConverter<TFrom, TTo>` — a binder in `TwoWay` / `OneWayToSource` applies `ConvertBack`, and warns when the converter has none.
- Non-generic `IConverter` as the root of the converter hierarchy.
- `TargetBinder<TTarget, TProperty>` / `ComponentMonoBinder<TComponent, TProperty>` property bases with a converter slot, plus `Target{Int,Float,Object}Binder` / `Component{Int,Float,Object}MonoBinder`; a destroyed object travels as `null` in both directions.
- `IIntBinder`, `ILongBinder`, `IFloatBinder`, `IDoubleBinder`, `IVector2Binder`, `IVector3Binder`, `IColorBinder`, `IRotationBinder` — the extra `SetValue` overloads as default interface members, saturating at the target type's bounds.
- `INumberReverseBinder` / `NumberReverseChannel` — one reverse channel for `int` / `long` / `float` / `double`, saturating at the target type's bounds.
- `BinderMath.SafeClamp` / `SafeClamp01` / `NonNegative` / `RequireFinite` and `BinderLogger` — a non-finite value is clamped and reported, naming the binder.
- `MonoBinder.DefaultMode` — the mode a binder starts in when added through the Inspector, applied from `Reset`.
- `ComponentMonoBinder.ResolveComponent` — narrows the `GetComponent` fallback so a binder typed on a base class never resolves itself.
- `ComponentIntMonoBinder<T>.RaiseNumberValueChanged` — an `int` binder can report to a `float` ViewModel field.
- `Selectable.SetInteractable` extension — one implementation of `InteractableMode` for every command binder.
- `DebugLogBinder` / `DebugLogMonoBinder` — log a bound value; the message is compiled out of release builds.
- `IAnyReverseBinder` — a reverse binder for any type; `null` reference values are forwarded, not thrown.
- `CollectionBinder<T>` hooks `OnAdded`, `OnRemoved`, `OnReplaced`, `OnMoved` — granular change forwarding with clean unsubscribe on `Unbind` / `Dispose`.
- `BindSafely` / `UnbindSafely` overloads with `owner` / `memberName`; failures are reported with View, member and index.
- `[HeaderGroup]`, `[HeaderGroupStart]` / `[HeaderGroupEnd]` — collapsible foldouts in the binder and ViewModel inspectors; stripped from player builds.
- `Source/Compatibility/UnityAttributesShim.cs` — `[SerializeField]`, `[SerializeReference]`, `[Tooltip]` compile outside Unity without `#if` guards.

#### Views

- `RelayCommand` support inside `View` / `MonoView`; `CommandsContainer` refactor.
- `ViewInitializer` overhaul: shared `ViewInitializerBase`, lazy edit-mode `Views` / `ViewModel`, `TryResolve` for containers, new `InitializeStage.DiConstructor`.
- `DestroyView()` destroys only the View component; `DestroyViewAndGameObject()` for the old behaviour.
- `PrefabViewFactory` / `PrefabViewPool` upgraded; `PrefabViewPool` is generic.
- `ViewModelPickerWindow` with dropdown and navigation; `DesignViewModel` upgrade.
- `[AddComponentMenu]` for `MonoView`.

#### Editor

- UI Toolkit inspectors for `MonoBinder`, `MonoView`, `MonoViewModel`; shared `AspidInspectorHeader`, `AspidPropertyField`, `AspidDividingLine`, `AspidToggle`.
- `DebugViewModelPanel` rewritten: tabs, persistent search by name and type, `RelayCommand` support, bindable and auto-property support.
- `Aspid.MVVM Settings` window in the FastTools Welcome style, under `Tools/Aspid 🐍`; version read from the package manifest.
- `[SerializeReference]` fields — converters, filters, orders, handlers, view factories, `PluralRule` — are drawn with the FastTools type picker, no attribute needed.
- Drag & Drop for unassigned binders with grouping, Auto-Assign and Select / Restore.
- `[RequireBinder]` and child View / Binder validation.
- `EnumMonoBinderEditor`; `EnumValues` drawer fixes.
- `BindMode` drawer: mode written unconditionally, label passed through, mixed value on multi-selection, no writes during layout.
- Binders sit in one `Aspid/MVVM/Binders` menu branch with a uniform `Component – Property` naming; contract tests pin it.

#### Binders

Each family ships the serializable binder and the `MonoBinder`; most also ship `Enum`, `EnumGroup` and `Switcher` variants.

- **UI Toolkit** — `VisualElementMonoBinder<TElement>` resolves an element in a `UIDocument` by name or USS class; `Label Text`, `Display`, `Enabled`, `Class`, `Button Command`, two-way `Slider Value` and `TextField Value`, `ListView.itemsSource` over an observable collection.
- **Collections** — `ObservableCollectionMonoBinder<T>` and `ObservableCollectionViewModelMonoBinder` for sets, queues and stacks; `ObservableDictionaryMonoBinder<TKey, TValue>` and `ObservableDictionaryViewModelMonoBinder`; `CollectionCountMonoBinder<T>` reports count and emptiness.
- **Aggregators** — `AndBool`, `OrBool`, `FormatString` over `BoolAggregatorInput` / `StringAggregatorInput`; `ConditionalFloat`, `ConditionalString`, `ConditionalColor` pick one of two Inspector values by a bound `bool`.
- **Rate limiting** — `Debounce`, `Throttle`, `Delay` casters in `Float` and `String`, unscaled clock by default.
- **Tweens** — `TweenFloat`, `TweenColor`, `TweenVector3` ease each value and retarget mid-flight; the first value passes through instantly.
- **Casters** — `StringToInt`, `StringToFloat`, `StringToEnum<TEnum>` parse a bound string, user culture first, invariant second; group separators, `NaN` and `Infinity` are refused.
- **Commands** — concrete `ButtonCommandInt` / `Float` / `String` / `Bool` / `Object`; `interactable` follows `CanExecute` for that parameter.
- **ToSource** — `…ToSourceMonoBinder` family hands the ViewModel the component; `SelectableToSource`, `GameObjectToSource`, `AudioSourceIsPlayingToSource`.
- **Global state** — `Time.timeScale`, `QualitySettings` level, `Application.targetFrameRate`, `Screen.fullScreen`.
- **GameObject / Transform** — `layer`, `parent` (keeps local position), sibling index, `Object.name`.
- **RectTransform** — `anchorMin`, `anchorMax`, `pivot`, `offsetMin`, `offsetMax`; `sizeDelta` reports `Vector2` as well.
- **Canvas & layout** — `Canvas.sortingOrder` / `overrideSorting`; `LayoutElement` preferred / flexible size and `ignoreLayout`; `LayoutGroup` binders; `CanvasScaler` mode, factor, resolution, match; `GridLayoutGroup` cell size, spacing, constraint; `ContentSizeFitter` axes; `AspectRatioFitter` mode and ratio; `RectMask2D.padding`.
- **Graphic** — `Graphic.raycastTarget`, `MaskableGraphic.maskable`, `Mask.showMaskGraphic`, `Shadow` / `Outline` color and distance, `GraphicMaterial` for any `Graphic`.
- **Image / RawImage** — `type`, `preserveAspect`, `fillOrigin`, `fillClockwise`, `RawImage.uvRect`.
- **Selectable** — `transition`, `targetGraphic`, `Dropdown` and `Selectable` binders, `ToggleGroup.allowSwitchOff`.
- **Toggle** — `Enum` / `EnumGroup` for `isOn`, written through `SetIsOnWithoutNotify`.
- **Slider / Scrollbar** — `Scrollbar.value` (`TwoWay`, `OneWayToSource`, all four numeric types), `Scrollbar.size`.
- **ScrollRect** — vertical and horizontal normalized position, `normalizedPosition`, axis enabled flags.
- **Dropdown** — `TMP_Dropdown.value` is two-way; options binders refresh the caption and keep the selection.
- **Text / InputField** — `fontStyle`, `enableAutoSizing`, `characterSpacing`, `lineSpacing`, `margin`, `maxVisibleCharacters`, `richText`; `caretPosition`, `placeholder`, `characterLimit`, `readOnly`; extra InputField binders.
- **Object** — `Object.name` binders.
- **SpriteRenderer** — `sprite`, `color`, `flipX`, `flipY`, `sortingOrder`, `size`.
- **Renderer** — `enabled`, `sortingOrder`, `sortingLayerName` (unknown name is reported), `shadowCastingMode`; `RendererPropertyBlock` `Float` / `Color` / `Vector` / `Texture` write through a `MaterialPropertyBlock`, leaving the material shared.
- **LineRenderer** — `widthMultiplier`, `loop`.
- **Light** — `color`, `intensity`, `range`, `spotAngle`.
- **Camera** — `fieldOfView`, `orthographicSize`, `backgroundColor`, `orthographic`.
- **Rigidbody** — `mass`, `useGravity`, `isKinematic`, `constraints`; **Rigidbody2D** — `mass`, `gravityScale`, `simulated`, `bodyType`.
- **Collider** — `CapsuleCollider.height` / `direction`, `contactOffset`, `includeLayers`, `excludeLayers`, `MeshCollider.cookingOptions`; **Collider2D** — `isTrigger`, `offset`, `density`, `sharedMaterial`, `BoxCollider2D.size`, `CircleCollider2D.radius`, `CapsuleCollider2D.size`.
- **AudioSource** — property binders, `Play` / `Stop` / `Pause` / `UnPause` as `Action` or `IRelayCommand`, `PlayOneShot` per published clip, `IsPlayingToSource`.
- **AudioMixer / AudioListener** — exposed float parameter, snapshot by index or name, `AudioListener.volume` / `pause`.
- **Animator** — `speed`, layer weight, `runtimeAnimatorController`, play a named state, reset trigger; parameter names are validated once per controller.
- **ParticleSystem** — `Play` / `Stop` / `Pause` / `Clear`, emission enabled, emission rate multiplier, start color.
- **VideoPlayer** — `clip`, `playbackSpeed` (0..10), `isLooping`; **NavMeshAgent** — `speed`, `isStopped`.
- **Addressables** — opt-in seamless swap with a destroyed-object guard; `GameObjectInstantiateAddressableMonoBinder`.

#### Converters

- **Catalogue** — ~190 converters grouped in the picker: `Aspid/Bool`, `Number`, `String`, `Time`, `Color`, `Vector`, `Rotation`, `Collection`, `Enum`, `Object`, `Texture`, `Layout`, `Localization`, `Asset`, `Composition`.
- **Two-way** — a converter with `ConvertBack` round-trips in `TwoWay` / `OneWayToSource`; dozens of shipped converters implement it.
- **Composition** — `Compose`, `Cached`, `Safe`, `NullGuard`, `Conditional`, `Passthrough`, `Sequence`; `Safe` and `Cached` are two-way.
- **`ConverterAsset<TFrom, TTo>`** — a converter as a `ScriptableObject`, referenced through `ConverterAssetReference`; ready-made subclasses under **Create → Aspid → MVVM → Converters**.
- **Failure handling** — a value a converter cannot convert is reported on every conversion and answered with an authored fallback; `ConverterFailureMode` chooses fallback or input where the types match.
- **`PluralRule`** — pluggable plural grammar (`SingleForm`, `English`, `French`, `EastSlavic`, `Polish`, `Czech`, `Arabic`); a project adds a language by subclassing.
- **`CultureInfoMode`** on every string and parsing converter, with `InvariantCulture` for round-trips.
- **Numeric width** — number, vector and Unity wrapper converters serve `int`, `long`, `float` and `double`; integer paths truncate and saturate.
- **Vector family** — one converter per operation serves `Vector2`, `Vector3` and `Vector4`; `VectorSwizzleConverter` replaces the per-dimension swizzles.
- `BoolInvertConverter` — inversion for bool binders, applied both ways.

#### Samples & documentation

- Samples rebuilt as a learning path: `01. Counter` → `06. CustomBinder`, plus `VirtualizedList`, `DynamicViewModel`, `DiIntegration`, `ExampleScripts`; each has a `README`, its own `.asmdef` and a compact `Sample SDF` font.
- Documentation site on Docusaurus (GitHub Pages), English original with a Russian translation, tutorials in the sample READMEs, DocFX API reference.
- XML docs on every public binder, converter and Editor type; every serialized field has a `[Tooltip]`; all four assemblies build clean with `-doc`.
- `Documentation/08-converters.md` rewritten around the shipped catalogue.
- ~3000 EditMode tests, including contract tests for tooltips, picker groups, menu paths, context-menu property names, `<include>` resolution and `BindMode` guard docs.

#### Project & infrastructure

- Package promoted to an embedded UPM package under `Packages/tech.aspid.mvvm`; Unity project moved into `Aspid.MVVM/`.
- `Aspid.MVVM.Generators`, `Aspid.MVVM.Analyzers`, `Aspid.MVVM.Unity.Generators` are git submodules.
- `Aspid.Collections` (`tech.aspid.collections`) and `Aspid.FastTools` (`tech.aspid.fasttools`, tag `upm-preview/1.0.0-rc.7`) are UPM git dependencies.
- Release workflow publishes `upm` (stable) and `upm-preview` subtrees with immutable `upm/<version>` tags, verifies generator DLL drift and takes release notes from this file.
- Claude PR Assistant and Code Review workflows; Unity Editor automation through Unity CLI with an `.mcp.json` server; root `CLAUDE.md`.
- Editor target `6000.4.0f1`.

### Changed

- **Breaking:** minimum Unity is `6000.0`.
- **Breaking:** `MonoView` is concrete — binder list, child validation and `[RequireBinder]` live on it; subclasses keep working.
- **Breaking:** `MonoView.Dispose()` only calls `Deinitialize()`; destroy the GameObject yourself.
- **Breaking:** `DestroyView()` no longer destroys the GameObject.
- **Breaking:** `MonoBinder.Bind()` on an already-bound binder logs an error instead of throwing.
- **Breaking:** numeric and vector `SetValue` overloads are default interface members on `IIntBinder`, `ILongBinder`, `IFloatBinder`, `IDoubleBinder`, `IVector2Binder`, `IVector3Binder`, `IColorBinder`, `IRotationBinder`; reach them through the interface: `((IBinder<float>)binder).SetValue(5f)`.
- **Breaking:** `TargetBinder<TTarget, TProperty>` and `ComponentMonoBinder<TComponent, TProperty>` are the property bases; both carry a `[SerializeReference]` converter slot, passed between the target and the mode.
- **Breaking:** numeric binders raise one `IReverseBinder<T>.ValueChanged` per width through `INumberReverseBinder`; subscribe as `((IReverseBinder<float>)binder).ValueChanged`.
- **Breaking:** `BinderMath` methods are extensions on `IBinder` (with a `Type` overload) so a sanitised value names its binder.
- **Breaking:** bool binders carry an `IConverter<bool, bool>` slot instead of `_isInvert`; a serialized inversion is re-authored with `BoolInvertConverter`. `*ByBind` binders keep their flag.
- **Breaking:** `DynamicViewModel` is a typed property bag — `Add<T>` / `Get<T>` / `TryGet<T>` hand out an `IDynamicProperty<T>` with `Value` and `ValueChanged`; all four `BindMode`s on one property type.
- **Breaking:** `Aspid.MVVM.StarterKit.Unity` and its Editor assembly merged into `Aspid.MVVM.StarterKit` / `Aspid.MVVM.StarterKit.Editor`; namespaces unchanged.
- **Breaking:** `[AddComponentMenu]` paths use the singular form and an en dash: `Collection/Observable List Binder – ViewModel`.
- **Breaking:** `ParseHtmlStringConverter` reports an unparsable colour; the fallback moved from `_defaultColor` to `_fallback` and must be re-authored.
- **Breaking:** renamed types carry no `[MovedFrom]`; a `[SerializeReference]` slot authored with an old type name needs the repair tool.
- `ValueToStringConverter<T>` (was `GenericToString<TFrom>`) exposes formatting as a virtual `Format(T value, string format)` hook; a blank or whitespace format falls back to `ToString()`.
- `ArithmeticNumberConverter` is `sealed`, exposes `Apply(double)` / `Undo(double)`; coefficient defaults to `1`.
- `Vector2ToVector3Converter` and `Vector3ToVector2Converter` became one two-way `Vector2Vector3Converter`; `Vector2/3SubstitutionConverter` became `VectorSwizzleConverter`.
- `NumberToBoolConverter` is `NumberCompareConverter`, `Comparisons` is `ComparisonMode` (`Inequality` → `NotEqual`); the threshold widened to `double` and must be re-authored.
- `ConverterExtensions.ToConvert` → `FuncConverterExtensions.ToConverter`.
- `CultureInfoMode` and `ToCultureStringExtensions` moved to `StarterKit/Runtime/Globalization`; namespace unchanged.
- A whitespace-only string is blank for every string converter that asks.
- Renderer colour binders read `sharedMaterial` and cache the `materials` array; collider material binders read `sharedMaterial` — no more instantiated copies on read.
- Slider and audio-source ranges: a crossed `min` / `max` pair is swapped and logged, a non-finite endpoint is refused.
- Slider, scrollbar and dropdown binders report the value the control actually holds after Unity clamps it.
- The Greeter sample uses the shipped `RichTextColorConverter`.
- Inspector attributes in Unity-independent layers are no longer wrapped in `#if UNITY_2022_1_OR_NEWER`.

#### Renamed

`.meta` GUIDs are preserved, so scenes and prefabs still bind. Source references need updating.

| 1.0 | 1.1 |
|-----|-----|
| `ViewModelObservableListMonoBinder` / `…Binder` | `ObservableListViewModelMonoBinder` / `…Binder` |
| `ViewModelObservableDictionaryBinder` | `ObservableDictionaryViewModelBinder` |
| `ViewModelCollectionMonoBinder` / `…Binder` | `CollectionViewModelMonoBinder` / `…Binder` |
| `CollectionBinderBase<T>` | `CollectionBinder<T>` |
| `OneWayValue<T>`, `OneTimeValue<T>`, `TwoWayValue<T>`, `OneWayToSourceValue<T>` | `ValueOneWayBinder<T>`, `ValueOneTimeBinder<T>`, `ValueTwoWayBinder<T>`, `ValueOneWayToSourceBinder<T>` |
| `Generic*Binder`, `UnityGeneric*Binder` | `Delegate*Binder` |
| `GenericCasterBinder`, `GenericToStringCasterBinder` | `CasterBinder`, `ValueToStringCasterBinder` |
| `MonoCommandBinder` | `CommandMonoBinder` |
| `ScrollBarCommandMonoBinder` | `ScrollbarCommandMonoBinder` |
| `RawImageMaterial*Binder` | `GraphicMaterial*Binder` |
| `RendererMaterialColorBinder` / `…SwitcherBinder` | `RendererMaterialsColorBinder` / `…SwitcherBinder` |
| `SliderValueMode` | `SliderRangeMode` |
| `ICanExecuteView`, `ColorInteractable`, `GameObjectVisibleInteractable`, `SequenceCanExecuteView` | `ICanExecuteHandler`, `ColorCanExecuteHandler`, `GameObjectVisibleCanExecuteHandler`, `SequenceCanExecuteHandler` |
| `RectTransformSetters`, `TransformSetters` | `RectTransformGettersAndSetters`, `TransformGettersAndSetters` |
| `Binder.IsBind` / `MonoBinder.IsBind` | `CanBind` |
| `ObservableListBinder.GetFilterList` | `GetFilteredList` |
| `IMonoBinderValidable.IsMonoExist`, `ValidableBindersById` | `IMonoBinderValidatable.IsMonoAlive`, `ValidatableBindersById` |
| `BinderFieldInfoExtensions.GetBinderId` | `BinderIdUtility.FromFieldName` (Editor assembly) |
| `ViewModelDebugPanel` | `DebugViewModelPanel` |
| `GenericFuncConverter`, `GenericToString`, `SequenceConverters` | `FuncConverter`, `ValueToStringConverter`, `SequenceConverter` |
| `TimeSpanToStringConverter`, `ObjectToStringConverter`, `ObjectNullToBoolConverter` | `TimeSpanFormatConverter`, `ValueToStringConverter<object>`, `EqualityToBoolConverter<T>` |

### Removed

- `AddComponentContextMenuAttribute` — use `AddBinderContextMenuAttribute` / `AddBinderContextMenuByTypeAttribute` (`Path = "..."`).
- `AddPropertyContextMenu` — property menus are handled by the Editor pipeline.
- `Aspid.Collections` source under the package — now the `tech.aspid.collections` package; with it `IViewModelCollectionFilter`, the composite filters and the collection comparers.
- `SerializeReferenceDropdown` integration and its dependency — replaced by the FastTools type picker.
- `DynamicPropertyData<T>`, `DynamicPropertyFactory`, `OneWayDynamicProperty<T>`, `TwoWayDynamicProperty<T>`, `OneTimeDynamicProperty<T>`, `DynamicViewModel.Create<…>`.
- The 40 `IConverterXToY` aliases and the 70 `ToConvert` / `ToConvertSpecific` wrappers — use `IConverter<TFrom, TTo>` and `ToConverter<TFrom, TTo>`; a `[SerializeReference]` field declared as an alias must be retyped.
- `IBindableValue<T>`, `IReadOnlyBindableValue<T>` — use the value binders' `Value`.
- `GenericToString<TFrom>.ToStringValue` — override `Format(T value, string format)` on `ValueToStringConverter<T>`.
- The pre-Unity-6 compatibility gate: `using Converter = …` aliases, `ToConvertSpecific()` branches, `UNITY_6000_0_OR_NEWER` branches, `PhysicMaterial` fallbacks.

### Fixed

#### Core

- `TwoWayValue<T>` (now `ValueTwoWayBinder<T>`) fed every ViewModel → View update straight back to the ViewModel with the converted value.
- `OneWayToSource` never reached the ViewModel for a numeric binder's own type; the other three widths worked.
- Reverse binding re-applied the *forward* converter on the way back.
- Numeric reverse channels converted out-of-range values through an undefined cast; they saturate now.
- `SetValue(Vector3)` on a Vector3 binder picked the `Vector2` overload and dropped Z.
- `BindSafely` / `UnbindSafely` stopped at the first throwing or destroyed binder; each is now reported and skipped.
- A destroyed `MonoBinder` stayed subscribed — `OnDestroy` now unbinds.
- A `MonoBinder` with no component to drive bound anyway and threw from a property setter; `CanBind` answers from the resolved component.
- A `TargetBinder` whose serialized target was destroyed threw from `OnBound`; the check now uses Unity's own conversion.
- `ComponentMonoBinder` skipped the `GetComponent` fallback for a dangling reference, and `OnValidate` never healed it.
- A binder added in the Inspector started in `TwoWay` even when its `[BindMode]` forbade it.
- Behaviour binders resolved themselves as their own target.
- `EnumGroup` binders threw on an unassigned entry and left the rest of the group stale.
- The Toggle, Slider and InputField echo guard was not restored after an exception, silencing the reverse channel for good.
- InputField binders subscribed from `OnValidate` while unbound, stacking duplicate listeners.
- InputField integer channel stayed silent for a number `int` cannot hold; a failed integer parse also silenced the float channels.
- `NaN` and infinities passed every `Mathf.Clamp` into `alpha`, `fillAmount`, `pitch` and 45 more sites.
- Collider extents stored negative sizes; `AudioSource.time` / `timeSamples` seeked past the clip with an error per frame.
- `SliderCommandBinder` accepted only `IRelayCommand<float>`; `int`, `long` and `double` commands threw.
- Slider / Scrollbar command binders reinterpreted `float` as `T` via `Unsafe.As` — garbage `CanExecute` for `long` / `double`.
- `UnityEventStringMonoBinder` threw on a `null` value.
- Generator ignored `enum` / `struct` constraints on generic bindable members.
- `MonoBinder.Unbind()` `ProfilerMarker` guard broke compilation before Unity 2022.1.

#### Collections

- `CollectionMonoBinder<T>` applied the collection once and never followed `CollectionChanged`.
- `CollectionViewModelMonoBinder<T>` ran past its view array when the collection outgrew it.
- A view inserted mid-list was parented last; it now takes the matching sibling index.
- `ObservableListBinder` unsubscribed from a different list than it subscribed to, leaking the subscription.
- `VirtualizedList` bounds check was off by one; `VirtualizedListItemSourceBinder` dispose fixes; `FilteredList` fixes.

#### Binders

- `Slider` values Unity clamped never reached the ViewModel — the echo guard swallowed the correction.
- `InteractableMode.Visible` on `*CommandMonoBinder` hid the binder's own GameObject instead of the target.
- `InteractableMode.Custom` with no `ICanExecuteView` threw from `CanExecuteChanged`, cutting other subscribers.
- Ten Transform binders wrote to their own `transform` instead of the assigned component.
- "Disabled when null" turned off force-enabled the component on every value in 14 sprite and texture binders.
- Animator `int` parameters compared through `Mathf.Approximately`, so a change of one past a million was skipped.
- Animator binders reset a parameter to zero from `OnEnable` before a value arrived.
- Animator binders wrote to a missing parameter with an error per frame; the name and type are validated once.
- `Renderer` material binders threw on an empty material set; `LineRenderer` colour binders threw on their default mode.
- `TextFontSwitcherBinder` was not `[Serializable]` and never appeared in the Inspector.
- Localization entry binders passed a silent `null` for an entry referenced by id; the binder now says why.
- `RectTransformSetters.SetSizeDelta` reported `Vector3(w, h, 0)` in `OneWayToSource`; a `Vector2` channel is raised too.
- `AudioSource` and `Slider` min / max could be left inverted.

#### Converters

- `NumberToBoolConverter` (now `NumberCompareConverter`) `Inequality` returned the same result as `Equal`.
- `SequenceConverter` dereferenced an empty Inspector slot.
- `Vector3CombineConverter` family threw when its scene reference was unassigned or destroyed.
- A `FormatException` in `GenericToString` cut the binder subscriber list; it now falls back to `ToString()`.

#### Editor

- `MonoBinderEditor.OnDisable` threw for a View that is not a `MonoView`.
- `BindMode` dropdown kept the choice only for rebindable owners and shared one cache across a multi-selection.
- Add Binder context menu passed the abstract target type to `AddComponent` for TextMeshPro fields.
- Sample paths in `package.json` pointed at a non-existent folder, so imports came out empty.

---

## [1.0.5] — 2025-10-17

### Added
- TextMeshPro binders: `TextFontBinder`, `TextFontSwitcherBinder`, `TextAlignmentBinder`, `TextAlignmentSwitcherBinder` and Mono variants (PR #30).
- Unity Localization binders: `LocalizeStringEventVariableBinder`, `TextLocalizationEntryBinder`, `TextLocalizationEntrySwitcherBinder`, Mono variants, `TextLocalizationExtensions` (PR #29).
- Profiler markers and improved logging across `BindableMember` types and `BindMode` (PR #15).

### Changed
- Editor project updated to Unity `6000.2.7f2` (PR #28).
- Vendored `com.unity.asset-store-tools` (packaging only).

### Fixed
- `RectTransformSetters.SetSizeDelta` wrote to `anchoredPosition` instead of `sizeDelta` (PR #27).

## [1.0.4] — 2025-09-19

### Fixed
- Component context-menu generation via `AddComponentContextMenuAttribute`; shipped in the rebuilt `Aspid.MVVM.Unity.Generators.dll` (PR #14).

## [1.0.3] — 2025-09-15

### Changed
- Unity-layer types (`MonoBinder`, `MonoViewModel`, `MonoView`, `ScriptableView`, editor classes) moved from `Aspid.MVVM.Unity` into the root `Aspid.MVVM` namespace for Asset Store packaging (PR #13).

### Removed
- `MonoBinderExtensions` (`BindSafely<T>` overloads) and the `OnBindingDebug` / `OnUnbindingDebug` hooks on `MonoBinder` (PR #13).

## [1.0.2] — 2025-09-11

### Fixed
- ViewModel source generator fix; shipped in the rebuilt `Aspid.MVVM.Generators.dll` (PR #12).

## [1.0.1] — 2025-09-10

### Changed
- C# language version reverted from 10 to 9, aligning with Unity's default compiler (PR #11).
- `AddressableMonoBinder<TAsset>` moved from UniTask to the `Addressables.LoadAssetAsync(...).Completed` callback; UniTask dependency dropped.
- `OneTimeBindableMember<T>` (and Enum / Struct variants) pooled through a static `Get(value)` factory.

### Fixed
- `ViewModelCollectionBinder` / `ViewModelCollectionMonoBinder` deactivate leftover pooled views when the collection shrinks.

## [1.0.0] — 2025-08-09

Initial public release.

[1.1.0]: https://github.com/VPDPersonal/Aspid.MVVM/compare/v1.0.5...HEAD
[1.0.5]: https://github.com/VPDPersonal/Aspid.MVVM/compare/1.0.4...v1.0.5
[1.0.4]: https://github.com/VPDPersonal/Aspid.MVVM/compare/1.0.3...1.0.4
[1.0.3]: https://github.com/VPDPersonal/Aspid.MVVM/compare/1.0.2...1.0.3
[1.0.2]: https://github.com/VPDPersonal/Aspid.MVVM/compare/1.0.1...1.0.2
[1.0.1]: https://github.com/VPDPersonal/Aspid.MVVM/compare/1.0.0...1.0.1
[1.0.0]: https://github.com/VPDPersonal/Aspid.MVVM/releases/tag/1.0.0
