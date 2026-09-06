# Migration Guide: 1.0 → 1.1

What to change in a project moving from **Aspid.MVVM 1.0.x** to **1.1.0**. The full list of changes is in [CHANGELOG.md](CHANGELOG.md).

> 🌐 Русская версия: [MIGRATION.ru.md](MIGRATION.ru.md)

Every relocated script kept its `.meta` GUID, so prefabs, scenes and ScriptableObjects keep their references. **Source code does not** — renamed types are a search-and-replace job. Renamed types carry no `[MovedFrom]`; a `[SerializeReference]` slot authored with an old type name needs the serialized-reference repair tool or re-authoring.

---

## TL;DR

1. Upgrade the Editor to Unity `6000.0` or newer.
2. Add `tech.aspid.collections` and `tech.aspid.fasttools` to `manifest.json` (§ 1).
3. Rename types from the table in § 2; the compiler finds the rest.
4. `view.Dispose()` and `view.DestroyView()` no longer destroy the GameObject (§ 4.2).
5. Custom `CollectionBinder<T>` subclasses implement six new hooks (§ 4.4).
6. Re-author `NumberCompareConverter` thresholds and `ParseHtmlStringConverter` fallbacks (§ 4.7).

---

## 1. Project and packages

- **Unity `6000.0`** is the minimum; the Editor project is on `6000.4.0f1`.
- Two git packages are required and not auto-resolved:

```json
"tech.aspid.collections": "https://github.com/VPDPersonal/Aspid.Collections.git#upm",
"tech.aspid.fasttools": "https://github.com/VPDPersonal/Aspid.FastTools.git#upm"
```

- `Aspid.Collections` no longer ships inside the package. Assembly (`Aspid.Collections.Observable`) and namespaces are unchanged.
- The framework moved from `Assets/Plugins/Aspid/MVVM/` to the UPM package `Packages/tech.aspid.mvvm/`. Update path constants and CI scripts; asset references survive.
- Assemblies `Aspid.MVVM.StarterKit.Unity` and `Aspid.MVVM.StarterKit.Unity.Editor` merged into `Aspid.MVVM.StarterKit` and `Aspid.MVVM.StarterKit.Editor`. Fix `.asmdef` references; the namespace `Aspid.MVVM.StarterKit` is unchanged.
- `SerializeReferenceDropdown` is no longer a dependency; `[SerializeReference]` fields are drawn by the FastTools type picker.

---

## 2. Renamed types and members

| 1.0 | 1.1 |
|-----|-----|
| `ViewModelObservableListMonoBinder` / `…Binder` | `ObservableListViewModelMonoBinder` / `…Binder` |
| `ViewModelObservableDictionaryBinder` | `ObservableDictionaryViewModelBinder` |
| `ViewModelCollectionMonoBinder` / `…Binder` | `CollectionViewModelMonoBinder` / `…Binder` |
| `CollectionBinderBase<T>` | `CollectionBinder<T>` |
| `OneWayValue<T>`, `OneTimeValue<T>`, `TwoWayValue<T>`, `OneWayToSourceValue<T>` | `ValueOneWayBinder<T>`, `ValueOneTimeBinder<T>`, `ValueTwoWayBinder<T>`, `ValueOneWayToSourceBinder<T>` |
| `GenericOneWayBinder`, `GenericOneTimeBinder`, `GenericTwoWayBinder`, `GenericOneWayToSourceBinder` (and `UnityGeneric*`) | `DelegateOneWayBinder`, `DelegateOneTimeBinder`, `DelegateTwoWayBinder`, `DelegateOneWayToSourceBinder` |
| `GenericCasterBinder` (and `UnityGenericCasterBinder`) | `CasterBinder` |
| `GenericToStringCasterBinder` / `…MonoBinder` | `ValueToStringCasterBinder` / `…MonoBinder` |
| `MonoCommandBinder` | `CommandMonoBinder` |
| `ScrollBarCommandMonoBinder` | `ScrollbarCommandMonoBinder` |
| `RawImageMaterial*Binder` | `GraphicMaterial*Binder` (any `Graphic`) |
| `RendererMaterialColorBinder` / `…SwitcherBinder` | `RendererMaterialsColorBinder` / `…SwitcherBinder` |
| `SliderValueMode` | `SliderRangeMode` (same members) |
| `ICanExecuteView`, `ColorInteractable`, `GameObjectVisibleInteractable`, `SequenceCanExecuteView` | `ICanExecuteHandler`, `ColorCanExecuteHandler`, `GameObjectVisibleCanExecuteHandler`, `SequenceCanExecuteHandler` |
| `RectTransformSetters`, `TransformSetters` | `RectTransformGettersAndSetters`, `TransformGettersAndSetters` |
| `IMonoBinderValidable.IsMonoExist`, `ValidableBindersById` | `IMonoBinderValidatable.IsMonoAlive`, `ValidatableBindersById` |
| `Binder.IsBind`, `MonoBinder.IsBind` | `CanBind` |
| `ObservableListBinder.GetFilterList` | `GetFilteredList` |
| `BinderFieldInfoExtensions.GetBinderId` | `BinderIdUtility.FromFieldName` (Editor assembly) |
| `ViewModelDebugPanel` | `DebugViewModelPanel` |
| `GenericFuncConverter<TFrom, TTo>` | `FuncConverter<TFrom, TTo>` |
| `ConverterExtensions.ToConvert` | `FuncConverterExtensions.ToConverter` |
| `GenericToString<TFrom>` | `ValueToStringConverter<T>` — override `Format(T value, string format)` instead of `ToStringValue` |
| `SequenceConverters<T>` | `SequenceConverter<T>` |
| `NumberToBoolConverter`, `Comparisons` (`Inequality`) | `NumberCompareConverter`, `ComparisonMode` (`NotEqual`) — threshold widened to `double`, see § 4.7 |
| `Vector2ToVector3Converter`, `Vector3ToVector2Converter` (`Values`) | `Vector2Vector3Converter` (`Mode`), two-way |
| `Vector2SubstitutionConverter`, `Vector3SubstitutionConverter` | `VectorSwizzleConverter` |
| `TimeSpanToStringConverter` | `TimeSpanFormatConverter` |
| `ObjectToStringConverter` | `ValueToStringConverter<object>` |
| `ObjectNullToBoolConverter` | `EqualityToBoolConverter<T>` against `null` |

---

## 3. Removed

| Removed | Use instead |
|---------|-------------|
| `AddComponentContextMenuAttribute`, `AddPropertyContextMenu` | `[AddBinderContextMenu(typeof(X), serializePropertyNames: "m_Field", Path = "path")]`; `AddBinderContextMenuByTypeAttribute` registers by target type only |
| `DynamicViewModel.Create<…>`, `DynamicPropertyData<T>`, `DynamicPropertyFactory`, `OneWay/TwoWay/OneTimeDynamicProperty<T>` | `DynamicViewModel.Add<T>` / `Get<T>` → `IDynamicProperty<T>` (§ 4.8) |
| `IConverterXToY` aliases (`IConverterFloat`, `IConverterIntToLong`, …), `ToConvert` / `ToConvertSpecific` wrappers | `IConverter<TFrom, TTo>` and `ToConverter<TFrom, TTo>`. A `[SerializeReference]` field declared as an alias resolves to `null` on load — retype it |
| `IBindableValue<T>`, `IReadOnlyBindableValue<T>` | `ValueTwoWayBinder<T>.Value` / `ValueOneWayBinder<T>.Value` |
| `IViewModelCollectionFilter`, `And/OrCompositeCollectionFilter`, `And/OrViewModelCompositeCollectionFilter`, `ICollectionComparer`, `NumberCollectionComparer` | Filtering and ordering from `tech.aspid.collections` |
| `UNITY_2022_1_OR_NEWER` / `UNITY_6000_0_OR_NEWER` compatibility branches, `PhysicMaterial` fallbacks | Unity 6 only |

```csharp
// 1.0
[AddPropertyContextMenu(typeof(CanvasGroup), "m_Alpha")]
[AddComponentContextMenu(typeof(CanvasGroup), "Add CanvasGroup Binder/Alpha")]
public partial class MyAlphaBinder : MonoBinder { }

// 1.1
[AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Alpha", Path = "Add CanvasGroup Binder/Alpha")]
public partial class MyAlphaBinder : MonoBinder { }
```

---

## 4. Behaviour changes

### 4.1 `MonoView` is concrete

`MonoView` is no longer `abstract`: the binder list, child validation and `[RequireBinder]` live on it. Subclasses keep working; the new serialized fields start empty.

### 4.2 `Dispose()` and `DestroyView()` keep the GameObject

```csharp
// 1.0 — both destroyed the GameObject
view.Dispose();
view.DestroyView();

// 1.1
view.Dispose();                   // Deinitialize() only
Object.Destroy(view.gameObject);  // if you still want the object gone

view.DestroyView();               // destroys only the View component
view.DestroyViewAndGameObject();  // the 1.0 behaviour
```

Both extension methods return `null` instead of throwing on a destroyed View and use `DestroyImmediate` outside play mode.

### 4.3 `MonoBinder.Bind()` on a bound binder

Logs an error and returns instead of throwing.

### 4.4 `CollectionBinder<T>` hooks

`CollectionBinder<T>` subscribes to `CollectionChanged` and adds six abstract hooks. A custom subclass must implement them; empty bodies restore the 1.0 behaviour.

```csharp
protected abstract void OnAdded(T? newItem);
protected abstract void OnAdded(IReadOnlyList<T?>? newItems);
protected abstract void OnRemoved(T? oldItem);
protected abstract void OnRemoved(IReadOnlyList<T?>? oldItems);
protected abstract void OnReplaced(T? oldItem, T? newItem, int index);
protected abstract void OnMoved(T? oldItem, T? newItem, int oldStartingIndex, int newStartingIndex);
```

`CollectionMonoBinder<T>` keeps `OnAdded(IReadOnlyCollection<T>)` / `OnReset()`, but now follows the collection and rebuilds on every change.

### 4.5 `ViewInitializer`

Resolution moved into `ViewInitializerBase`, container `Resolve` became `TryResolve` (a failed resolve no longer throws), and `InitializeStage.DiConstructor` was added. The default stage is still `Awake`. The serialized resolution entries changed type — re-check `ViewInitializer` / `ViewInitializerManual` in the Inspector.

### 4.6 Binders

- **`[AddComponentMenu]` paths**: `Collections/…` → `Collection/…`, hyphen → en dash (`Binder – ViewModel`). Update tooling that matches menu paths.
- **Addressable binders** gained an opt-in `_seamlessSwap`; the load lifecycle uses separate current/pending handles. Re-check subclasses that override the asset-set or release flow.
- **Sanitising is loud**: a `NaN` or infinity written to a binder is clamped and reported through `BinderLogger`. A finite out-of-range value still saturates silently.
- **Reverse binding converts**: in `TwoWay` / `OneWayToSource` a converter's `ConvertBack` is applied. Remove any compensation your ViewModel did for the missing reverse conversion.
- **Bool binders** carry an `IConverter<bool, bool>` slot instead of `_isInvert`. Re-author inversion by picking `BoolInvertConverter`; `*ByBind` binders keep the flag.
- **A `MonoBinder` added in the Inspector** starts in the mode its `DefaultMode` allows, not always `TwoWay`.
- **A destroyed `MonoBinder`** unbinds from `OnDestroy`; call `base.OnDestroy()` in overrides.

### 4.7 Converters

- **Misconfiguration reports on every conversion** and returns an authored fallback. Expect new console errors from setups that were silently broken.
- **`NumberCompareConverter`** (was `NumberToBoolConverter`): `Inequality` was inverted in 1.0 and now works; the `_value` threshold widened from `float` to `double`, so every authored threshold reads back as `0` — re-author it.
- **`ParseHtmlStringConverter`**: the fallback moved from `_defaultColor` to `_fallback` and must be re-authored; an unparsable colour is now reported.
- **`ValueToStringConverter<T>`** (was `GenericToString<TFrom>`): formatting is the virtual `Format(T value, string format)` hook; a blank or whitespace format falls back to `ToString()`. A `FormatException` is contained instead of cutting the binder subscriber list.
- **Whitespace is blank**: every string converter treats a whitespace-only string as empty.

### 4.8 `DynamicViewModel`

```csharp
var viewModel = new DynamicViewModel
{
    { "Title", "Hello" },
    { "Volume", 0.5f, BindMode.TwoWay }
};

IDynamicProperty<float> volume = viewModel.Get<float>("Volume");
volume.Value = 0.8f;
volume.ValueChanged += v => Debug.Log(v);
```

`Add<T>` returns the same `IDynamicProperty<T>` handle. All four `BindMode`s live on one property type; the number of properties is unlimited.

### 4.9 `RelayCommand`

`RelayCommand.Empty` stays non-executable; `RelayCommand.EmptyExecution` is executable and does nothing. The private empty constructor is now `RelayCommand(bool value = false)` — relevant only to reflection.

---

## 5. Writing custom binders in 1.1

- **Serializable twin is generated.** Write the `MonoBinder` half, mark it `[GenerateSerializableBinder]`, and `{Name}Binder` is emitted over the matching `Target*Binder`. A twin that already exists by name is left alone.
- **Property bases carry the converter.** `TargetBinder<TTarget, TProperty>` and `ComponentMonoBinder<TComponent, TProperty>` hold a `[SerializeReference] IConverter<TProperty, TProperty>` slot; typed bases `Target{Int,Float,Object}Binder<TTarget>` and `Component{Int,Float,Object}MonoBinder<TComponent>` sit on top.
- **Numeric and vector overloads come from interfaces.** `IIntBinder`, `ILongBinder`, `IFloatBinder`, `IDoubleBinder`, `IVector2Binder`, `IVector3Binder`, `IColorBinder`, `IRotationBinder` supply the extra `SetValue` overloads as default interface members; out-of-range values saturate. They are reachable only through the interface: `((IBinder<float>)binder).SetValue(5f)`.
- **One reverse channel for numbers.** `INumberReverseBinder` bridges `IReverseBinder<int/long/float/double>.ValueChanged` to a `NumberReverseChannel` the binder holds; subscribe via `((IReverseBinder<float>)binder).ValueChanged`.
- **`BinderMath` names the caller.**

```csharp
Target.pitch = this.SafeClamp(value, -3f, 3f, Target);            // inside a binder
if (!this.RequireFinite(value, Target)) return;                   // replaces a silent IsFinite guard
value = BinderMath.SafeClamp(typeof(MyHelper), value, 0f, 1f);    // inside a static helper
```

---

## Checklist

- [ ] Editor on Unity `6000.0`+; `tech.aspid.collections` and `tech.aspid.fasttools` in `manifest.json`
- [ ] Path constants and CI: `Assets/Plugins/Aspid/MVVM/` → `Packages/tech.aspid.mvvm/`; `.asmdef` references to `Aspid.MVVM.StarterKit.Unity*` → `Aspid.MVVM.StarterKit*`
- [ ] Rename types and members from § 2
- [ ] Replace `[AddComponentContextMenu]` / `[AddPropertyContextMenu]` with `[AddBinderContextMenu]`
- [ ] Retype `[SerializeReference]` fields declared as `IConverterXToY` aliases to `IConverter<TFrom, TTo>`
- [ ] Add `Object.Destroy(view.gameObject)` after `Dispose()`, or switch `DestroyView()` → `DestroyViewAndGameObject()` where the object must go
- [ ] Implement the six hooks on custom `CollectionBinder<T>` subclasses
- [ ] Re-check `ViewInitializer` Inspector data
- [ ] Replace `DynamicViewModel.Create` with `Add<T>` / collection initializer
- [ ] Re-author `NumberCompareConverter` thresholds, `ParseHtmlStringConverter` fallbacks and bool-binder inversion (`BoolInvertConverter`)
- [ ] Move `ToStringValue` overrides to `Format(T, string)`
- [ ] Remove ViewModel-side compensation for the missing reverse conversion in two-way bindings
- [ ] Run the serialized-reference repair tool over scenes and prefabs that hold renamed converters or binders in `[SerializeReference]` slots
- [ ] Update tooling that matches `AddComponentMenu` paths
- [ ] Triage new console errors from converters and binders that were already misconfigured
