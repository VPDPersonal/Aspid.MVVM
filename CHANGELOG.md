# Changelog

All notable changes to **Aspid.MVVM** are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

> 🌐 Русская версия: [Docs/i18n/ru/CHANGELOG.md](https://github.com/VPDPersonal/Aspid.MVVM/blob/main/Docs/i18n/ru/CHANGELOG.md)

---

## [1.1.0] — Unreleased

Everything since `1.0.5`. Minimum Unity is now **`6000.0`**. Upgrade checklist: [migration guide](https://vpdpersonal.github.io/Aspid.MVVM/migration/1.1).
A preview, `1.1.0-beta.1`, was published on 2026-06-06 to the `upm-preview` channel.

### General binders

- `TargetBinder<TTarget>` moved to the `Aspid.MVVM.Unity` assembly.
- Derived binders can use `BindingVersion` to detect binding changes during callbacks.

### Audit completion

- UI Toolkit sliders support all four numeric reverse channels. Rebuilt and late visual trees restore values and callbacks without rebinding the ViewModel.
- Renderer material binders own instance copies per consumer and slot and release them on unbind.
- FastTools is required; assembly constraints disable MVVM when it is absent, without per-file FastTools guards. Unity modules, UI packages and Collections remain optional. Serialized types retain their assemblies and GUIDs.
- Clean dependency profiles cover minimal installation, individual integrations, combined installation and package removal/reinstallation in Editor and Player.
- Installation instructions distinguish unreleased source from published previews. The minimum supported Unity patch is 6000.0.53f1; the development project pins Collections 1.1.1 without its former Test Framework dependency.

### Highlights

- Source generator: bindable properties, `NotifyCanExecuteChangedAll()`, auto-emitted binder fields, `[GenerateSerializableBinder]`.
- StarterKit: binder types grown from ~360 to ~890, covering uGUI, TextMeshPro, UI Toolkit, 2D, physics, audio, animation, lighting and cameras.
- Converters: catalogue grown from 17 to ~190, `ITwoWayConverter`, `ConverterAsset`, composition primitives, culture-aware formatting.
- Editor: UI Toolkit inspectors, rebuilt `DebugViewModelPanel`, Welcome / Settings window, FastTools type picker for `[SerializeReference]`.
- `MonoView` is concrete, `DynamicViewModel` is a typed property bag, `ViewInitializer` gained a DI construction stage.
- Package is an embedded UPM package; `Aspid.Collections` and `Aspid.FastTools` are UPM git dependencies; the generator and analyzer sources live in this repository.
- Documentation site on Docusaurus (`en` / `ru`), samples rebuilt as a learning path, ~3000 EditMode tests.

### Added

- `DynamicViewModel.AddEnum<T>` / `AddStruct<T>` and specialized dynamic properties expose typed and boxed channels without runtime generic reflection.
- API input/output fingerprints and signature checks detect stale generated documentation in CI.

#### Core & generator

- `IViewModel.InitializeView(view)` and `InitializeViews(views)` initialize Views from the ViewModel, including null-conditional calls and array, list, or enumerable inputs.
- View collections support initialization, reinitialization, deinitialization, disposal, and destruction extensions, including `Safely` variants that continue after individual failures.

- **Bindable properties** — `[Bind]` on properties, not only fields.
- **`NotifyCanExecuteChangedAll()`** — refreshes every command with a `CanExecute`, including `IRelayCommand`-typed members.
- **Auto-emitted binder fields** — a `MonoBinder[]` slot for every bindable member of `IView<TViewModel>` not declared on the View; opt out with `[View(AutoBinderFields = false)]`.
- **`[GenerateSerializableBinder]`** — the serializable twin of a `MonoBinder` is generated; only the MonoBehaviour half is hand-written. Custom binders can now use the attribute from `Aspid.MVVM` without referencing StarterKit.
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
- `MonoBinder.DefaultMode` — the mode a new binder starts in, including runtime `AddComponent`; `Mode` can be configured from code while unbound.
- `ComponentMonoBinder.ResolveComponent` — narrows the `GetComponent` fallback so a binder typed on a base class never resolves itself.
- `ComponentIntMonoBinder<T>.RaiseNumberValueChanged` — an `int` binder can report to a `float` ViewModel field.
- `Selectable.SetInteractable` extension — one implementation of `InteractableMode` for every command binder.
- `DebugLogBinder` / `DebugLogMonoBinder` — log a bound value; the message is compiled out of release builds.
- `IAnyReverseBinder` — a reverse binder for any type; `null` reference values are forwarded, not thrown.
- `CollectionBinder<T>` hooks `OnAdded`, `OnRemoved`, `OnReplaced`, `OnMoved` — granular change forwarding with clean unsubscribe on `Unbind` / `Dispose`.
- `BindSafely` / `UnbindSafely` accept binding contexts.
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
- `Aspid MVVM` window under `Tools/Aspid 🐍/MVVM`, in the FastTools workbench style: a **Welcome** tab with the samples list (Import / Remove cards, auto-opens once per package version, per-user opt-out) and a **Settings** tab with the build defines and the Welcome auto-show switch; keyboard navigation, tab shortcuts, version footer.
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
- **Toggle** — `Enum` / `EnumGroup` for `isOn`.
- **Notify Mode** — the Toggle, Slider, Scrollbar, InputField and Dropdown value binders and their `Enum` / `EnumGroup` / `Switcher` variants choose which writes from the ViewModel raise `onValueChanged`: `Always`, `AfterBind` (silent on bind, so a reused list item does not fire) or `Never`. A binder added in the Editor starts with `Never`; saved binders keep their old behavior. The serializable twins take a `notifyMode` constructor parameter before `mode`.
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

- Samples rebuilt as a learning path: `01. Counter` → `06. CustomBinder`, plus `VirtualizedList`, `DynamicViewModel`, `DiIntegration`, `ExampleScripts`; each has its own `.asmdef`, a compact `Sample SDF` font (iA Writer Quattro S, as in the Aspid.FastTools samples) and a tutorial on the docs site.
- Documentation site on Docusaurus (GitHub Pages), built like the Aspid.FastTools site: English original with a Russian translation, a tutorial per sample with a recording of its scene, the changelog and a DocFX API reference.
- XML docs on every public binder, converter and Editor type; every serialized field has a `[Tooltip]`; all four assemblies build clean with `-doc`.
- The Converters page rewritten around the shipped catalogue.
- ~3000 EditMode tests, including contract tests for tooltips, picker groups, menu paths, context-menu property names, `<include>` resolution and `BindMode` guard docs.

#### Project & infrastructure

- Package promoted to an embedded UPM package under `Packages/tech.aspid.mvvm`; Unity project moved into `Aspid.MVVM/`.
- `Aspid.MVVM.Generators`, `Aspid.MVVM.Analyzers`, `Aspid.MVVM.Unity.Generators` moved into this repository with their full history; they are no longer git submodules.
- `Aspid.Collections` (`tech.aspid.collections`) and `Aspid.FastTools` (`tech.aspid.fasttools`, tag `upm-preview/1.0.0-rc.9`) are UPM git dependencies.
- Release workflow publishes `upm` (stable) and `upm-preview` subtrees with immutable `upm/<version>` tags and takes release notes from this file; it runs the whole CI first.
- CI on every pull request: package files, version files, reproducible Roslyn DLLs rebuilt from their sources, .NET tests, and EditMode + PlayMode tests on every Unity 6000.x line; PR title and CHANGELOG checks.
- Claude PR Assistant and Code Review workflows; Unity Editor automation through Unity CLI; root `CLAUDE.md`.
- Editor target `6000.6.5f1`.

### Changed

- Shortened XML documentation across `Source` and `Unity`, documented missing public Editor contracts, and removed XML comments from internal implementation details.

- `DestroyView()` now destroys ScriptableObject views as well as components. View and ViewModel helpers share Unity object destruction while retaining their existing return values and null handling.

- Unity ViewModel disposal releases resources without destroying objects. Override `OnDispose()` instead of `Dispose()`; cleanup runs once and also runs on Unity destruction. `DestroyViewModel()` supports ScriptableObject ViewModels, and `DestroyViewModelAndGameObject()` additionally removes the owning GameObject.
- The `Destroy*Safely` extensions skip missing and destroyed objects, attempt Unity destruction even after cleanup fails, and propagate failures.
- `DestroyViewModel` and `DestroyViewModelAndGameObject` reject missing and destroyed models. Batch `DisposeViewModels`, `DestroyViewModels`, and `DestroyViewModelsAndGameObjects` process the remaining models and report failures after cleanup.
- ViewModel destruction extensions moved from `MonoViewModelExtensions` to `ViewModelExtensions` in the core assembly.

- Editor ViewModel validation reuses its deduplication buffer and static notification delegates, and defers nested dispatch to the next update.
- **Breaking:** `IBinder.Bind` and `Unbind` take `in BindContext` and `in UnbindContext`. Owners are reference-identified objects, allowing Views and composite binders; foreign cleanup is ignored. Safe helpers require an owner, debug rebind preserves it, and lifecycle guards prevent reattachment during cleanup. Generated Views bind and release a fixed binder set directly, without tracking lists.
- **Breaking:** Cleanup preserves a single original exception and reports multiple failures in `AggregateException` across binders, collections, Views and initializers. Error lists are allocated only after a second failure; failed binding and initialization keep their original exception.
- **Breaking:** For `IEnumerable<T>`, traversal failures can leave some binders unprocessed.
  `BindSafely` uses `foreach`, so an exception from `Dispose()` takes precedence over a binding or traversal failure.
  `UnbindSafely` collects both failures in an `AggregateException`.
- **Breaking:** Removed the scalar `BinderExtensions.Unbind` overload; generated Views call `IBinder.Unbind` directly.
- **Breaking:** `BindSafely` overloads for resolved members take `in BindContext` instead of separate member and owner arguments; `UnbindSafely` takes `in UnbindContext` instead of an owner. Collections reuse the context for every binder.
  Remove the `memberName` argument. Generated typed Views use the same binding API.
- **Breaking:** `Binder` and `MonoBinder` use exclusive lifecycle states. Acquire subscriptions in `OnAttach` and release resources in `OnDetach`; `OnBound` / `OnUnbound` notify after state transitions and allow rebinding after release. Completion notification failures do not roll back an established binding.
- **Breaking:** `IBinder` requires `IsBound`; `Binder` and `MonoBinder` already implement it. Ownership is checked by the binder; generated Views allocate no binding trackers or attempt records. Arrays and lists use indexed binding loops.

- **Breaking:** `BindMode.IsOne()` / `IsTwo()` are now `IsForwardOnly()` / `IncludesReverse()`. Mode guards use `EnsureValid`, `EnsureForwardOnly`, `EnsureForward`, `EnsureReverse` and `EnsureNot`; the old `ThrowExceptionIf*` names and unused `IsNone()` helper are removed.
- **Breaking:** `Binder` constructors reject `None` and undefined modes with `ArgumentOutOfRangeException`; mode attributes reject null arrays and invalid entries. Forward-only guards reject invalid modes too.
- **Breaking:** minimum Unity is `6000.0`.
- **Breaking:** `MonoView` is concrete — binder list, child validation and `[RequireBinder]` live on it; subclasses keep working.
- **Breaking:** `MonoView.Dispose()` only calls `Deinitialize()`; destroy the GameObject yourself.
- **Breaking:** `DestroyView()` no longer destroys the GameObject.
- **Breaking:** `Binder.Bind()` and `MonoBinder.Bind()` reject repeated or reentrant binding with `InvalidOperationException`, preserving an existing binding.
- **Breaking:** `BinderInvalidCastException.Class/Struct` and `ReverseBinderInvalidCastException<T>.Class/Struct` return exceptions without throwing; use `throw` at the call site.
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
- `Source` is compiled by Unity only: the `UnityEngine` stand-ins and the `#if UNITY_*` guards are gone, and inspector attributes are written unconditionally.
- Profiler markers of the core, `MonoBinder`, `DynamicViewModel` and the generated Views and ViewModels compile only under `ENABLE_PROFILER` (Editor and development builds), so a release player carries no marker fields or classes; `ASPID_MVVM_UNITY_PROFILER_DISABLED` now also turns off the markers of `Binder.Bind()` / `Unbind()`, `AnyToStringCasterBinder` and the generated `[BinderLog]` `SetValue`.
- `InitializeComponent<T>` checks the VContainer resolver for `null` explicitly, so Rider no longer reports a possibly unassigned local.

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

- Removed the `BindSafely` overloads taking `FindBindableMemberResult`. Create a `BindContext` from the resolved member after a successful lookup.

- `ComponentMonoBinder<TComponent>.OnValidate`. Custom binders can declare their own Unity validation callback directly.

- `ViewBinder.InitializeView` and `ViewBinder.DeinitializeView`. Subclasses can call `View.Initialize` and `View.Deinitialize` directly.

- The `Documentation` folder and the sample `README` files — the guide and the tutorials are on the [docs site](https://vpdpersonal.github.io/Aspid.MVVM/); the package holds no docs or media.
- `AddComponentContextMenuAttribute` — use `AddBinderContextMenuAttribute` / `AddBinderContextMenuByTypeAttribute` (`Path = "..."`).
- `AddPropertyContextMenu` — property menus are handled by the Editor pipeline.
- `Aspid.Collections` source under the package — now the `tech.aspid.collections` package; with it `IViewModelCollectionFilter`, the composite filters and the collection comparers.
- `SerializeReferenceDropdown` integration and its dependency — replaced by the FastTools type picker.
- `DynamicPropertyData<T>`, `DynamicPropertyFactory`, `OneWayDynamicProperty<T>`, `TwoWayDynamicProperty<T>`, `OneTimeDynamicProperty<T>`, `DynamicViewModel.Create<…>`.
- The 40 `IConverterXToY` aliases and the 70 `ToConvert` / `ToConvertSpecific` wrappers — use `IConverter<TFrom, TTo>` and `ToConverter<TFrom, TTo>`; a `[SerializeReference]` field declared as an alias must be retyped.
- `IBindableValue<T>`, `IReadOnlyBindableValue<T>` — use the value binders' `Value`.
- `GenericToString<TFrom>.ToStringValue` — override `Format(T value, string format)` on `ValueToStringConverter<T>`.
- The pre-Unity-6 compatibility gate: `using Converter = …` aliases, `ToConvertSpecific()` branches, `UNITY_6000_0_OR_NEWER` branches, `PhysicMaterial` fallbacks.
- The vendored `com.unity.asset-store-tools` in the Editor project — it is not part of the package.
- TMP Examples & Extras in the Editor project; TMP Essential Resources are back in the default `Assets/TextMesh Pro`.
- `BindSafelyNullReferenceException` and `UnbindSafelyNullReferenceException`, which Unity builds never threw.
- `Aspid.MVVM.Generators.Sample` — debug the generator through `Aspid.MVVM.Generators.Tests`.
- `OneTimeBindableMember<T>.Get`, `OneTimeStructBindableMember<T>.Get`, `OneTimeEnumBindableMember<T>.Get` and the `protected` `GetMarker` — create the member with its constructor, as the other bindable members.

### Fixed

- Binding analyzers inspect all attributes, share field naming with the generator, classify qualified writes and increments, and avoid unsafe property fixes for ref/out or nested struct writes. CI rejects empty .NET test runs.
- Slider value binders report actual rounded/clamped values and defer initial TwoWay normalization until reverse subscriptions exist.
- Reference converters preserve deliberate null results in input fields, string events and material assignments.
- Parameterized UI command binders refresh CanExecute when Param changes, including serializable variants.
- Delay stops processing the old queue when a callback unbinds or rebinds it.
- Addressable binders suppress default application during destruction; cancellation failures do not skip base teardown, owned instance cleanup, or current asset release. Failed handle releases are not retried.
- Inspector matching checks channel direction and generic variance consistently for drag/drop and automatic assignment.

- Property notification helpers inspect all ViewModel partial declarations while preserving `CallerLineNumber`. Ambiguous implicit calls report `ASPIDVM007`; explicit property names select the correct target, and unsupported calls report `ASPIDVM008`. Source-only property setters no longer call a missing forward-notification method.
- Source generators preserve complete namespaces and nested partial type chains, including generic parameters and constraints. Generated filenames distinguish containers and generic arity; BinderLog emits once across partial declarations. Serializable twins support the global namespace and namespace-scoped aliases. Unsupported containers report `ASPIDGEN001`, inaccessible or open generic context-menu targets report `ASPIDMENU001`, and generic typed Views compile with profiling enabled.

- `[RelayCommand]` supports ordinary bool-field predicates and validates overloads, generic constraints and inherited member hiding before generating commands (`ASPIDVM004`).

- Collection rollback retains both the primary failure and any recovery failure, including their stacks. Failed factory release triggers emergency View destruction with factory/type/index diagnostics; failed views must not be retained for reuse.

- Collection view binders release child bindings on detach, preserve inactive slots for null items, and finish cleanup after individual failures. Created views return to their original factory; failed prefab initialization is rolled back.
- VirtualizedList clears null sources, pauses subscriptions while disabled, releases owned views on destruction, and stops stale refreshes after source changes from callbacks. Moves across the viewport update every affected slot on both axes.

- View initializers reject cleanup and repeated initialization during initialization without losing the acquired views or the original ViewModel; rollback remains available. `MonoView` aggregates cleanup failures across all serialized binder groups.

- `DestroyViewAndGameObject` also destroys ScriptableObject views, including when cleanup fails; single-instance rollback reuses this cleanup path.
- View destruction extensions moved from `MonoViewExtensions` to `ViewExtensions` in the core assembly.

- View factories roll back failed clones and async batches; view destruction continues after cleanup failures. Header groups now agree between generated fields and the Inspector. Property context menus honor custom paths, and strict design ViewModels require concrete types.

- Editor ViewModel validation discards pending requests at mode exit and retains new requests until mode entry, including when Domain Reload is disabled.
- `Reinitialize` and `DisposeView` throw `ArgumentNullException` for a null receiver. These methods also reject destroyed Unity objects. A null replacement ViewModel still deinitializes the View.
- `ReinitializeSafely`, `DeinitializeViewSafely`, and `DisposeViewSafely` skip null Views and destroyed Unity objects. They log exceptions and return null when skipped or failed.

- Generated Views handle repeated cleanup safely and reject conflicting lifecycle operations. Direct cleanup covers every declared binder, including declared nested bindings established by callbacks.
- `[AsBinder]` reports invalid types and constructors instead of silently skipping them; generated arguments preserve escaping, numeric types and culture-independent literals. `[View]` rejects structs and reports invalid class declarations. Generic View lifecycle helpers require reference types.

- ViewModel configuration now reports generator diagnostics for invalid modes, accessor visibility, `BindAlso` targets, command signatures and unresolved `CanExecute`; unsupported bindings emit a warning. Editor metadata respects explicit and inferred modes, and `AccessAttribute` retains its constructor values.
- Built-in ViewModel lookups reject invalid/default queries; both `DisposeViewModel` overloads reject null and destroyed Unity objects. Initializing Views from a ViewModel uses the same argument handling and traversal as initializing them from a collection of Views.
- `InitializeViewSafely`, `InitializeViewsSafely`, and `DisposeViewModelSafely` skip missing and destroyed objects and propagate exceptions. Initialization stops at the first failure. ViewModels also support reinitializing Views and passing `Span<T>` or `ReadOnlySpan<T>` to batch methods.

- Parameter-bound command adapters forward `CanExecuteChanged` while observed, release their source subscription after the last listener, and delegate execution without a duplicate predicate check. Strict adapter factories reject a null source immediately; empty commands always carry a no-op action.

- Binding failures validate both directions before subscribing and roll back partial bindings; teardown finishes after lifecycle hook failures and preserves the original exception.
- Generated Views reset initialization guards, release only acquired binders after failed initialization, and remain reusable; both View initializers roll back partial child initialization and finish cleanup after a child throws.
- Runtime-created MonoBinders use their default binding mode without Editor `Reset`; authored serialized modes remain unchanged, and `Mode` can be configured from code while unbound.

#### Core

- `ComponentMonoBinder.Bind` reports an error when it cannot resolve a live target component. A later bind can retry after the component becomes available.

- `TargetBinder.Bind` reports a missing or destroyed target before installing the binding.

- `ScriptableViewMonoBinder.Bind` reports a missing or destroyed View as an invalid configuration.

- ViewBinder discards an older ViewModel update when the view's deinitialization triggers a newer value or detachment.

- Component binders fill their target field when added or reset in the Editor. Inspector validation leaves cleared targets empty.

- `ViewBinder` throws `ArgumentNullException` when constructed with a null view.

- Mode errors show the actual mode and allowed modes. `OneWay` / `OneTime` members reject undefined modes before emitting a value; generated binders honour `BindModeOverride.IsOne` / `IsTwo` and report invalid declarations as `ASPIDSB006`.
- Reverse binder compatibility messages list `IAnyReverseBinder`, and exception messages skip the GameObject path of a destroyed component instead of masking the compatibility error.
- Binder compatibility messages separate the failed direction, required interfaces and object path, using readable C# type names without repeating the component type in the path.
- `TwoWay` binders can attach to `OneWay` and `OneTime` members using only the supported forward updates, so a View can be reused across ViewModels with different binding capabilities.
- One-time bindable members were a single static instance per type shared by every ViewModel, so a binder bound later (a second binder of the same member after a nested View had bound, an Editor rebind, a member kept by user code) could get the value of another ViewModel. The generated code now keeps one member per ViewModel and refreshes it on access.
- `TwoWayValue<T>` (now `ValueTwoWayBinder<T>`) fed every ViewModel → View update straight back to the ViewModel with the converted value.
- `OneWayToSource` never reached the ViewModel for a numeric binder's own type; the other three widths worked.
- Reverse binding re-applied the *forward* converter on the way back.
- Numeric reverse channels converted out-of-range values through an undefined cast; they saturate now.
- `SetValue(Vector3)` on a Vector3 binder picked the `Vector2` overload and dropped Z.
- **Breaking:** The `Safely` overloads skip null collections, null binders and destroyed Unity binders. They propagate failures to the caller. `BindSafely` stops at the first failure and leaves earlier bindings in place. Collection `UnbindSafely` attempts the remaining binders before rethrowing the original exception or throwing an `AggregateException` for multiple failures. Generated Views roll back initialization after a binder failure.
- A destroyed `MonoBinder` stayed subscribed — `OnDestroy` now unbinds.
- A `MonoBinder` with no component to drive bound anyway and threw from a property setter; `CanBind` answers from the resolved component.
- A `TargetBinder` whose serialized target was destroyed threw from `OnBound`; the check now uses Unity's own conversion.
- `ComponentMonoBinder` skipped the `GetComponent` fallback for a dangling reference, and `OnValidate` never healed it.
- A binder added in the Inspector started in `TwoWay` even when its `[BindMode]` forbade it.
- Behaviour binders resolved themselves as their own target.
- `EnumGroup` binders threw on an unassigned entry and left the rest of the group stale.
- The Toggle, Slider and InputField echo guard was not restored after an exception, silencing the reverse channel for good.
- Serialized `ToggleIsOnBinder`, `SliderValueBinder` and `InputFieldBinder` never reported to the ViewModel in `OneWayToSource`: Unity runs no field initializers on deserialization, so the echo guard came out closed.
- InputField binders subscribed from `OnValidate` while unbound, stacking duplicate listeners.
- InputField integer channel stayed silent for a number `int` cannot hold; a failed integer parse also silenced the float channels.
- `NaN` and infinities passed every `Mathf.Clamp` into `alpha`, `fillAmount`, `pitch` and 45 more sites.
- Collider extents stored negative sizes; `AudioSource.time` / `timeSamples` seeked past the clip with an error per frame.
- `SliderCommandBinder` accepted only `IRelayCommand<float>`; `int`, `long` and `double` commands threw.
- Slider / Scrollbar command binders reinterpreted `float` as `T` via `Unsafe.As` — garbage `CanExecute` for `long` / `double`.
- `UnityEventStringMonoBinder` threw on a `null` value.
- Generator ignored `enum` / `struct` constraints on generic bindable members.
- `MonoBinder.Unbind()` `ProfilerMarker` guard broke compilation before Unity 2022.1.
- A `MonoView` added from code with `AddComponent` threw a `NullReferenceException` from `Initialize`: its binder list was never deserialized and stayed `null`.
- A derived View re-emitted the auto binder field its base View already had, so Unity serialized the slot twice.
- Generator skipped View members typed as the bare `IBinder` interface.
- A custom ViewModel interface property typed `IBindableMember<T>` was ignored, and a read-only member could back a two-way slot.
- `[IgnoreBind]` on a View member no longer excluded it: the generator bound ignored binder fields and properties anyway.
- Only the first `[BindAlso]` of a member counted when it named a property without a bind attribute; the other properties were never notified.
- `[BindAlso]` naming a `OneWayToSource` target generated a bare `();` call that did not compile; targets without change notification are skipped.

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
- `DebugLogBinder` logged a converter's `null` result as the value's `ToString()`; only an empty converter slot falls back to it now.

#### Converters

- `NumberToBoolConverter` (now `NumberCompareConverter`) `Inequality` returned the same result as `Equal`.
- `SequenceConverter` dereferenced an empty Inspector slot.
- `Vector3CombineConverter` family threw when its scene reference was unassigned or destroyed.
- A `FormatException` in `GenericToString` cut the binder subscriber list; it now falls back to `ToString()`.

#### Editor

- `MonoBinderEditor.OnDisable` threw for a View that is not a `MonoView`.
- `BindMode` dropdown kept the choice only for rebindable owners and shared one cache across a multi-selection.
- Add Binder context menu passed the abstract target type to `AddComponent` for TextMeshPro fields.
- Add Binder context menu ignored `SubPath` of `[AddBinderContextMenu]`: the shipped Unity generator DLL was built before it supported it.
- Sample paths in `package.json` pointed at a non-existent folder, so imports came out empty.
- Eleven editor `.uss.meta` files had no importer block, and the sample `README.ru.md` files had no `.meta`.

#### Samples & documentation

- `[AsBinder]` examples in the Views page and `ExampleScripts` left the component field unserialized, so the binder constructor threw on `null`; the page now covers constructor arguments and `nameof`.
- XML docs in `Source` follow the `asp-xmldoc` rules and no longer contradict the generator: `[Bind(BindMode.OneWay)]` on a `readonly` field is skipped rather than resolved to `OneTime`, `[ViewModel]` ignores structs and `[View]` targets classes only, and a `BaseBindAttribute` subclass generates nothing.
- Labeled crefs (`<see cref="X">label</see>`) in the API reference linked the namespace page instead of the type.

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
