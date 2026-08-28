# Migration Guide

Upgrade notes for moving an existing project from **Aspid.MVVM 1.0** to **Aspid.MVVM 1.1**.

For the full list of changes see [CHANGELOG.md](CHANGELOG.md).

> 🌐 Русская версия: [MIGRATION.ru.md](MIGRATION.ru.md)

> Unity asset references (prefabs, scenes, ScriptableObjects) survive the upgrade because every relocated script kept its original `.meta` GUID. Source-code references to renamed classes do **not** survive — search-and-replace is required.

> **Minimum Unity is now `6000.0`**.

---

## TL;DR

1. Add the required git packages `tech.aspid.collections` and `tech.aspid.fasttools` to your `manifest.json` — they are not auto-resolved (see § 3.1).
2. Rename `ViewModelObservableList*` → `ObservableList*ViewModel`, and the same shape for Dictionary / Collection (see § 1.1).
3. Replace every `[AddComponentContextMenu(typeof(X), "path")]` with `[AddBinderContextMenu(typeof(X), Path = "path")]`, and fold any `[AddPropertyContextMenu(typeof(X), "m_Field")]` into the same attribute's `serializePropertyNames` argument (see § 1.2).
4. Audit every `view.Dispose()` call: the GameObject is no longer destroyed automatically (see § 2.2).
5. Audit every `view.DestroyView()` call: it now destroys only the View component, not the GameObject — use `view.DestroyViewAndGameObject()` for the old behaviour (see § 2.3).

---

## 1. Compilation breakers

### 1.1 Renamed StarterKit binder classes

`.meta` GUIDs are intact, so existing prefabs / scenes keep working. Only your own source code needs updates.

| 1.0 | 1.1 |
|-----|-----|
| `ViewModelObservableListMonoBinder` (incl. generic `<T>`, `<T, TViewFactory>`) | `ObservableListViewModelMonoBinder` |
| `ViewModelObservableListBinder` | `ObservableListViewModelBinder` |
| `ViewModelObservableDictionaryBinder` | `ObservableDictionaryViewModelBinder` |
| `ViewModelCollectionMonoBinder` | `CollectionViewModelMonoBinder` |

Suggested approach: a single global rename per row (regex / IDE refactor). Namespace `Aspid.MVVM.StarterKit` is unchanged.

### 1.2 `AddComponentContextMenuAttribute` removed

`AddComponentContextMenuAttribute` and `AddPropertyContextMenuAttribute` were both removed and merged into a single `AddBinderContextMenuAttribute` (plus the type-only variant `AddBinderContextMenuByTypeAttribute`, which registers a binder purely by its target component type). The menu path moves to the named `Path` property; the serialized-property name(s) that `[AddPropertyContextMenu]` provided move into the `serializePropertyNames` constructor parameter (`params string[]`, so several are allowed).

```csharp
// BEFORE — Aspid.MVVM 1.0
[AddPropertyContextMenu(typeof(CanvasGroup), "m_Alpha")]        // optional
[AddComponentContextMenu(typeof(CanvasGroup), "Add CanvasGroup Binder/Alpha")]
public partial class MyAlphaBinder : MonoBinder { }

// AFTER — Aspid.MVVM 1.1 (one attribute; both arguments carry over, Path is optional)
[AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Alpha", Path = "Add CanvasGroup Binder/Alpha")]
public partial class MyAlphaBinder : MonoBinder { }
```

If a binder only had `[AddComponentContextMenu(typeof(X), "path")]`, the mechanical replacement is `[AddBinderContextMenu(typeof(X), Path = "path")]`.

---

### 1.3 Typed binder bases replaced by binder interfaces

The bases that existed only to restate the `SetValue` overloads are gone. The conversions now live in the binder
interfaces as default interface implementations, so a binder names the interface instead of inheriting a base.

| 1.0 / earlier 1.1 | Now |
|-----|-----|
| `TargetVector3Binder<T>` | `TargetBinder<T, Vector3>, IVector3Binder` |
| `TargetVector2Binder<T>` | `TargetBinder<T, Vector2>, IVector2Binder` |
| `ComponentVector3MonoBinder<T>` | `ComponentMonoBinder<T, Vector3>, IVector3Binder` |
| `ComponentVector2MonoBinder<T>` | `ComponentMonoBinder<T, Vector2>, IVector2Binder` |
| `ComponentQuaternionMonoBinder<T>` | `ComponentMonoBinder<T, Quaternion>, IRotationBinder` |
| `Vector3Binder` / `Vector2Binder` | `Binder<Vector3>, IVector3Binder` / `Binder<Vector2>, IVector2Binder` |
| `Vector3MonoBinder` / `Vector2MonoBinder` | `MonoBinder<Vector3>, IVector3Binder` / `MonoBinder<Vector2>, IVector2Binder` |
| `TargetQuaternionBinder<T>` | `TargetBinder<T, Quaternion>, IRotationBinder` |
| `ComponentColorMonoBinder<T>` | `ComponentMonoBinder<T, Color>, IColorBinder` |
| `TargetColorBinder<T>` | `TargetBinder<T, Color>, IColorBinder` |
| `ComponentBoolMonoBinder<T>` / `ComponentStringMonoBinder<T>` | `ComponentMonoBinder<T, bool>` / `ComponentMonoBinder<T, string>` |
| `ColorMonoBinder` / `QuaternionMonoBinder` | `MonoBinder<Color>, IColorBinder` / `MonoBinder<Quaternion>, IRotationBinder` |
| `BoolMonoBinder` / `StringMonoBinder` | `MonoBinder<bool>` / `MonoBinder<string>` |
| `TargetBoolBinder<T>` / `TargetStringBinder<T>` | `TargetBinder<T, bool>` / `TargetBinder<T, string>` |

`.meta` GUIDs of the concrete binders are untouched, so prefabs and scenes keep working.

`TargetQuaternionBinder<T>` rejected `BindMode.TwoWay` in its constructor — a rotation property raises no change
event. A rotation binder now carries that check itself; a custom one built on the removed base has to add
`mode.ThrowExceptionIfMatches(BindMode.TwoWay);` to its own constructor.

A default interface implementation is not a class member, so the extra `SetValue` entry points are reachable only
through the interface. Call sites that used them directly need a cast:

```csharp
// BEFORE
vector2Binder.SetValue(5f);
vector3Binder.SetValue(new Vector2(1f, 2f));

// AFTER
((IBinder<float>)vector2Binder).SetValue(5f);
((IBinder<Vector2>)vector3Binder).SetValue(new Vector2(1f, 2f));
```

The same applies to the numeric bases: `SetValue(int)`, `SetValue(long)`, `SetValue(float)` and `SetValue(double)`
now come from `IIntBinder` / `ILongBinder` / `IFloatBinder` / `IDoubleBinder`, and out-of-range values saturate at the
target type's bounds instead of wrapping.

---

### 1.4 `TargetBinderWithConverter<T, TProperty>` merged into `TargetBinder<T, TProperty>`

The two-argument `TargetBinder` now holds the converter itself, the way `ComponentMonoBinder<T, TProperty>` and
`MonoBinder<TProperty>` always have. `TargetBinderWithConverter<T, TProperty>` and
`TargetObjectBinderWithConverter<T, TObject>` are removed — rename them to `TargetBinder<T, TProperty>` and
`TargetObjectBinder<T, TObject>`; nothing else about those binders changes.

Its constructor takes the converter between the target and the mode, so a binder built directly on the
two-argument base passes one more argument:

```csharp
// BEFORE
public MyBinder(Image target, BindMode mode = BindMode.OneWay)
    : base(target, mode) { }

// AFTER
public MyBinder(Image target, IConverter<Image.Type, Image.Type>? converter = null, BindMode mode = BindMode.OneWay)
    : base(target, converter, mode) { }
```

Callers that passed the mode positionally as the second argument now have to name it: `new MyBinder(target,
mode: BindMode.OneWayToSource)`. Binders that had no converter before gain a serialized one, which starts empty
and changes nothing until it is filled in.

---

### 1.5 `BinderMath` methods name the binder they sanitise for

`SafeClamp`, `SafeClamp01` and `NonNegative` replaced a non-finite value without a word in the console, which is
the opposite of what the converters do for a value they cannot convert. They now report the replacement, and to do
that they need to know who is calling: each is an extension on `IBinder`, with a `Type` overload for a helper
reporting on another binder's behalf — the same pair `BinderLogger` offers.

```csharp
// BEFORE
Target.pitch = BinderMath.SafeClamp(value, -3f, 3f);

// AFTER — inside a binder; a serializable binder passes its target as the object to ping
Target.pitch = this.SafeClamp(value, -3f, 3f, Target);

// AFTER — inside a static helper
audioSource.time = BinderMath.SafeClamp(typeof(AudioSourceTimeSetters), value, 0f, end, audioSource);
```

`BinderMath.IsFinite(float)` stays a plain predicate. The new `RequireFinite` is the reporting form of it: it
returns `false` and logs, and replaces the `if (!BinderMath.IsFinite(value)) return;` guard that silently dropped
the write. Overloads cover `float`, `Vector2`, `Vector3`, `Vector4` and `Rect`, and a vector is reported once
rather than once per component.

Only the non-finite path reports. A finite value outside the range still saturates at the bound in silence — that
is the documented contract, and a slider driven every frame would otherwise fill the console.

---

## 2. Runtime / behavioural changes

### 2.1 `MonoView` is no longer abstract

```csharp
// 1.0
public abstract partial class MonoView : MonoBehaviour, IDisposable

// 1.1
public partial class MonoView : MonoBehaviour, IDisposable
```

Existing subclasses keep working — newly added serialized fields (`_bindersList`, `_designViewModel`, `_designViewModelAssemblyQualifiedNames`) appear empty. Either populate them in the inspector or keep the legacy override style — both are supported.

### 2.2 `MonoView.Dispose()` no longer destroys the GameObject

```csharp
// 1.0
public virtual void Dispose() {
    Deinitialize();
    if (this) Destroy(gameObject); // <-- removed
}

// 1.1
public virtual void Dispose() => Deinitialize();
```

If your code relied on `view.Dispose()` to free the host object, switch to:

```csharp
view.Dispose();
Object.Destroy(view.gameObject);
```

(or override `Dispose` in your subclass to restore the old behaviour).

### 2.3 `DestroyView()` no longer destroys the GameObject

Mirroring § 2.2, the `DestroyView` extension method changed. In 1.0 `view.DestroyView()` tore down the whole GameObject; in 1.1 it deinitializes the View (or calls `Dispose()` if the View is `IDisposable`) and destroys only the View **component**, leaving the GameObject alive. A new `DestroyViewAndGameObject()` restores the old behaviour.

```csharp
// 1.0 — destroyed the GameObject
view.DestroyView();

// 1.1 — destroys only the View component; to also destroy the GameObject:
view.DestroyViewAndGameObject();
```

Both methods are now null/destroyed-safe (they return `null` instead of throwing) and, in the Editor outside play mode, use `DestroyImmediate`. The same pair exists for the generic `DestroyView<T>()` / `DestroyViewAndGameObject<T>()` overloads.

### 2.4 `CollectionBinderBase<T>` forwards granular change events

In 1.0, `CollectionBinderBase<T>` exposed only `OnAdded(IReadOnlyCollection<T>)` and `OnReset()`, and did not subscribe to `CollectionChanged`. In 1.1 it subscribes to `CollectionChanged` and adds six new abstract hooks:

- `OnAdded(T?)`, `OnAdded(IReadOnlyList<T?>)`
- `OnRemoved(T?)`, `OnRemoved(IReadOnlyList<T?>)`
- `OnReplace(T? oldItem, T? newItem, int newStartingIndex)`
- `OnMove(T? oldItem, T? newItem, int oldStartingIndex, int newStartingIndex)`

Batch `Replace` events are unrolled into per-item `OnReplace` calls.

**Compile impact:** any class deriving from `CollectionBinderBase<T>` must implement all six new abstract methods or it will not compile. Empty bodies preserve the 1.0 behaviour. `CollectionMonoBinder<T>` itself is unchanged (still only `OnAdded` / `OnReset`).

### 2.5 `ViewInitializer` overhaul

The `ViewInitializer` family was reworked: view/container resolution moved into `ViewInitializerBase`, edit-mode `Views` / `ViewModel` resolve lazily, and container `Resolve` became `TryResolve` (a failed DI resolve no longer throws). A new `InitializeStage.DiConstructor` stage was added (compiled only when a Zenject or VContainer integration define is set). The default initialization stage is **unchanged** — it is still `Awake`.

The serialized resolution data was also restructured: the per-target resolution entries are now `ViewInitializeComponent` items (with the target type stored as a type-name string) instead of the old inline `InitializeComponent<IView>` fields. Re-check the resolution settings on existing `ViewInitializer` / `ViewInitializerManual` components in the inspector after upgrading.

### 2.6 Addressable seamless swap

`AddressableMonoBinder<TAsset>` / `AddressableMonoBinder<TAsset, TComponent>` gain a serialized `_seamlessSwap` flag (default `false`, so opt-in). With it off, a new bind still resets to the default asset before loading, as in 1.0; with it on, the previously loaded asset stays on screen until the new load completes. The load lifecycle was rewritten even on the default path (a single internal handle became separate current/pending handles), so if you subclass an Addressable binder and override the asset-set or release flow, re-check it against the new field and handle lifecycle.

### 2.7 `[AddComponentMenu]` paths

A number of menu paths were normalised:

- "Collections/…" → "Collection/…" (singular).
- ASCII hyphen `-` between words → en-dash `–`.

Tooling that searches the Add Component dialog or menu paths by exact string needs to be updated.

### 2.8 Behavioural fixes that change runtime output

Two 1.0 bugs were fixed, so the same source now behaves differently at runtime — no recompile needed:

- **`NumberToBoolConverter` with `Comparisons.Inequality`** returned the same result as `Comparisons.Equal` in 1.0 (the comparison was inverted). It now correctly returns `true` when the values are *not* approximately equal. Review binders configured with `Inequality` and remove any compensating inversion you added downstream.
- **`DynamicViewModel.Create<…>`** forced every property to `BindMode.OneTime` in 1.0, discarding the configured mode. It now honours each `DynamicPropertyData`'s `BindMode`, so properties created without an explicit mode update live. Pass `BindMode.OneTime` explicitly if you relied on bind-once behaviour.

---

## 3. Project / infrastructure

### 3.1 Required packages

1.1 is distributed as a UPM package (`tech.aspid.mvvm`). Its assemblies depend on two external git packages that `package.json` does not declare, so add them to your `Packages/manifest.json` before importing 1.1:

```json
"tech.aspid.collections": "https://github.com/VPDPersonal/Aspid.Collections.git#upm",
"tech.aspid.fasttools": "https://github.com/VPDPersonal/Aspid.FastTools.git#upm"
```

The `Aspid.Collections` source that previously shipped inside the package was removed; it is now the separate `tech.aspid.collections` package. Its assembly name (`Aspid.Collections.Observable`) and namespaces are unchanged, so `using` directives and type references need no edits once the package is present.

### 3.2 Unity project relocated

The Unity project tree moved from the repository root into `Aspid.MVVM/`, and the framework also moved out of the `Plugins/` layer:

```
1.0:  <repo>/Assets/Plugins/Aspid/MVVM/...
1.1:  <repo>/Aspid.MVVM/Packages/tech.aspid.mvvm/...
```

(Third-party plugins such as Zenject stay under `Assets/Plugins/`.) `.meta` GUIDs were preserved, so prefab / scene / ScriptableObject references survive — only textual path strings (CI/CD scripts, IDE workspaces, build pipelines, path constants) need updating.

### 3.3 Unity Editor versions

`package.json` now declares `"unity": "6000.0"`, formally setting the minimum supported Unity to `6000.0`. 1.0 shipped without a UPM `package.json`, so it declared no minimum (its repository project file was already on Unity `6000.2.7f2`). Projects still on Unity 2022 / 2023 must upgrade the Editor before adopting 1.1.

---

## 4. Architectural notes

### 4.1 `BindSafely` / `UnbindSafely`

Optional `owner` and `memberName` parameters (defaulting to `null`) were appended to the existing `BindSafely` / `UnbindSafely` methods, so the null-binder diagnostic can name the owning View (its type and GameObject name), the field that holds the binders, and the element index. Existing source call sites compile unchanged.

### 4.2 Bindable Properties

Existing `[Bind]` fields keep working. Bindable Properties (PR #46) are additive — opt in **per property** by applying `[Bind]` (or `[OneWayBind]` / `[TwoWayBind]` / `[OneTimeBind]` / `[OneWayToSourceBind]`) directly to a property instead of a field. In 1.0 these attributes targeted fields only; in 1.1 they also accept properties. No ViewModel-level change is required.

### 4.3 `RelayCommand`

`RelayCommand.Empty` is preserved (still non-executable). New `RelayCommand.EmptyExecution` returns a command that is executable but does nothing; both exist on every arity (`RelayCommand`, `RelayCommand<T>`, … up to four type arguments). The internal empty constructor changed from a parameterless `RelayCommand()` to `RelayCommand(bool value = false)` — invisible through the public API, but reflection that looks up the private parameterless constructor by signature must be updated.

---

## 5. Converters

The converter subsystem was rebuilt: 14 converters became 148, the contract gained a reverse direction, and the pre-2023.1 compatibility layer was deprecated. Almost all of it is source-code work — the names that appear in serialized data were deliberately left alone, and the deprecated types are still implemented. Three exceptions need authoring time: `DateTimeCompareConverter` and `DateTimeOffsetFormatConverter` lose their settings to the enum change and `NumberCompareConverter` loses its threshold to a widened field (§ 5.4), and a prefab-instance override on a renamed or re-encapsulated type needs the repair tool (§ 5.1).

### 5.1 Renames

Nine names changed. Four have no serialized footprint at all; the rest are types that `[MovedFrom]` migrates for an object's own data:

| Was | Is | Notes |
|-----|-----|-------|
| `Vector2ToVector3Converter.Values`, `Vector3ToVector2Converter.Values` | `Mode` | Nested enum type; a nested type name is not serialized |
| `Comparisons.Inequality` | `ComparisonMode.NotEqual` | An enum serializes as an ordinal, which is unchanged |
| `EnumMatch.Equals` | `EnumMatch.Equal` | Same — an ordinal. The member was hiding the inherited `object.Equals` |
| `ConverterExtensions.ToConvert` | `ToConverter` | Extension method; code only. The method returns a converter, so the old name read as an imperative |
| `WrapMode` | `NumberWrapMode` | Enum **type** rename; the value stays an ordinal, so authored data is unaffected. The old name was ambiguous against `UnityEngine.WrapMode` |
| `ListToStringConverter` | `CollectionJoinToStringConverter` | Carries `[MovedFrom]`. It accepts any `IEnumerable<T>`, and every sibling is named `Collection*` |
| `NumberToBoolConverter` | `NumberCompareConverter` | Class rename, plus `Comparisons` → `ComparisonMode` and a threshold widened from `float` to `double` — that one costs authoring time (§ 5.4) |
| `BoxColliderCentreCombineConverter`, `CapsuleColliderCentreCombineConverter`, `SphereColliderCentreCombineConverter` | `…CenterCombineConverter` | Carry `[MovedFrom]`. American spelling, and it matches Unity's own `center` property |

Search-and-replace the first five and you are done. For everything carrying `[MovedFrom]`, read the warning below before assuming scenes are untouched.

> **A wider rename wave was attempted and reverted, and the reason is worth knowing if you maintain
> your own `[SerializeReference]` types.** `[MovedFrom]` and `[FormerlySerializedAs]` cover an
> object's own serialized data. They do **not** cover a prefab-instance override, which is keyed by
> the stored type string and the property path. Renaming `SequenceConverters` emptied a converter in
> the package's own Hello World sample — 24 console errors and a binder that stopped converting,
> with `[MovedFrom]` present and correct. So `SequenceConverters`, `GenericToString`,
> `_preConvertor` / `_postConvertor` and `_values` keep their names, spelling and all.
>
> The same caveat applies to `ListToStringConverter` → `CollectionJoinToStringConverter`, and to
> `EnumToValueConverter.Entry` / `LookupEntry`, whose public fields became private `[SerializeField]`
> with `[FormerlySerializedAs]`. None of the three is authored in any scene or prefab shipped with
> the package, so nothing inside it needed migrating. If **your** project authored one as a
> prefab-instance override, run the repair tool that rewrites the stored type strings and property
> paths over every scene and prefab — otherwise the override is dropped on load with no diagnostic.

### 5.2 Deprecated: the named converter aliases

The 40 `IConverterXToY` interfaces and the 70 `ToConvert` / `ToConvertSpecific` wrappers are `[Obsolete]`. They existed because Unity before 2023.1 could not serialize a `[SerializeReference]` field of an open generic; 1.1 requires Unity 6000.0.

```csharp
// before
[SerializeReference] private IConverterFloat _converter;
IConverterFloat c = ((Func<float, float>)(x => x * 2f)).ToConvert();

// after
[SerializeReference] private IConverter<float, float> _converter;
IConverter<float, float> c = ((Func<float, float>)(x => x * 2f)).ToConverter();
```

The generic `ConverterExtensions.ToConverter<TFrom, TTo>` is the replacement and is not deprecated. It was called `ToConvert` before this release — the name said "convert" but the method hands back a converter.

**You have one release to act.** The package's own converters still implement the aliases, so a field declared as `IConverterFloat` keeps deserializing today. When the aliases are removed, such a field resolves to `null` on load — silently, with no exception and no console entry. The compiler warning is the only notice you get.

### 5.3 Reverse binding now converts

A binder in `BindMode.TwoWay` or `BindMode.OneWayToSource` used to send the View's value back to the ViewModel unconverted. It now calls `ITwoWayConverter.ConvertBack` when the assigned converter implements it, and warns in the console when it does not.

This changes runtime output where a two-way binding had a converter attached. If you compensated for the missing reverse conversion in the ViewModel — a setter that undid the converter's work — remove that compensation.

### 5.4 Behavioural fixes that change runtime output

- **`StringFormatConverter` with `_formatEmptyValues` enabled** formats null and empty input again. Between the 1.1 previews it returned `null` instead of the formatted empty string.
- **A `FormatException` in a converter no longer stops unrelated binders.** If your scene had a broken format string, binders behind it in the dispatch order were silently not updating; they will now update.
- **The `Vector3CombineConverter` family returns the input unchanged** instead of throwing when its scene reference is missing, and reports it on every push.
- **A misconfigured converter now reports on every conversion.** Expect new console output from converters that were already misconfigured and silently returned a fallback — an empty token list, an inverted `min`/`max`, a missing inner converter, a duplicate lookup key. The messages name the converter and what it did instead; they are pointing at authoring that was already broken, not at a new failure.
- **`SafeConverter` lost its `_logErrors` field.** A caught exception is always logged, in full. If you relied on that switch to keep a scene quiet, the noise it was hiding will now appear.
- **`NumberCompareConverter` needs its threshold re-authored.** The former `NumberToBoolConverter` kept its `_value` field name but widened it from `float` to `double`, and Unity does not carry a float across to a double field: every authored threshold reads back as `0`. The comparison itself survives — `ComparisonMode` has the same members in the same order as `Comparisons`.
- **`DateTimeCompareConverter` and `DateTimeOffsetFormatConverter` need re-authoring.** Their bool pairs became the `ReferenceSource` and `OffsetSource` enums, and the old booleans do **not** migrate: each instance reverts to its default and the intended source has to be picked again in the Inspector. Both converters are worth a scene search before you upgrade.
- **Two-way bindings gained a reverse direction they did not have.** `DateTimeToUnixTimestampConverter`, `StringToDateTimeConverter` and `StringToTimeSpanConverter` used to hand the value back untouched in a `TwoWay` binding; they now convert. If your ViewModel was compensating for that, remove the compensation.
- **An undeclared enum value raises `InvalidOperationException` instead of `ArgumentOutOfRangeException`.** Only relevant if you catch it — the arm reports corrupt serialized state, not a bad argument.

### 5.5 `GenericToString.ToStringValue` is gone

`protected virtual string ToStringValue(TFrom)` became a private detail when formatting moved to the
`Format` hook. A subclass that overrode it no longer compiles:

```csharp
// before
protected override string ToStringValue(float value) => value.ToString("F2");

// after — Format receives the typed value and runs for every non-blank format
protected override string? Format(float value) => value.ToString("F2");
```

The two hooks are not called on the same schedule. `ToStringValue` ran only when the format was
blank; `Format` runs whenever it is **not** blank, and a blank one falls back to `ToString()`. If the
override existed to change the no-format rendering, it now belongs in the subclass's own `Convert`.

---

## Upgrade checklist

- [ ] Add the `tech.aspid.collections` and `tech.aspid.fasttools` git packages to `manifest.json` (required; not auto-resolved)
- [ ] Upgrade the Editor to Unity `6000.0` or newer
- [ ] Update CI / build scripts and path constants: `Assets/Plugins/Aspid/...` → `Aspid.MVVM/Assets/Aspid/...` (repo-root `Assets/` → `Aspid.MVVM/Assets/`)
- [ ] Global rename of StarterKit binder classes (see § 1.1)
- [ ] Replace `[AddComponentContextMenu(...)]` with `[AddBinderContextMenu(..., Path = ...)]`
- [ ] Move `[AddPropertyContextMenu(..., "m_Field")]` arguments into `[AddBinderContextMenu(..., serializePropertyNames: "m_Field")]`
- [ ] Add explicit `Object.Destroy(view.gameObject)` where `view.Dispose()` was used to free objects
- [ ] Replace `view.DestroyView()` with `view.DestroyViewAndGameObject()` where you relied on it to destroy the host GameObject
- [ ] Implement the six new abstract hooks on any custom `CollectionBinderBase<T>` subclass (`OnAdded(T?)`, `OnAdded(IReadOnlyList<T?>)`, `OnRemoved(T?)`, `OnRemoved(IReadOnlyList<T?>)`, `OnReplace`, `OnMove`)
- [ ] Review `ViewInitializer` setups: resolution moved into `ViewInitializerBase`, container `Resolve` became `TryResolve`, and an `InitializeStage.DiConstructor` stage was added (the default stage is unchanged — `Awake`)
- [ ] Re-check `ViewInitializer` / `ViewInitializerManual` inspector data — the serialized resolution components changed type, so existing view/viewModel resolution settings may not carry over- [ ] Review `NumberToBoolConverter` (`Inequality`) and `DynamicViewModel.Create` usages for the corrected runtime behaviour
- [ ] Smoke-test scenes that use `ImageSpriteSwitcherBinder`, Addressable binders and `VirtualizedList*`
- [ ] Update tests / tooling that look up components by `AddComponentMenu` path
- [ ] Rename `Values` → `Mode`, `Comparisons.Inequality` → `NotEqual`, `EnumMatch.Equals` → `Equal`, `ToConvert` → `ToConverter`, `WrapMode` → `NumberWrapMode` and `ListToStringConverter` → `CollectionJoinToStringConverter` in your own code (see § 5.1)
- [ ] Re-author `NumberCompareConverter` thresholds (see § 5.4)
- [ ] Re-author `DateTimeCompareConverter` and `DateTimeOffsetFormatConverter` in every scene and prefab — their bool settings do not migrate to the new enums (see § 5.4)
- [ ] Run the serialized-reference repair tool if any scene or prefab overrides `CollectionJoinToStringConverter`, `EnumToValueConverter.Entry` or `LookupEntry` on a prefab instance (see § 5.1)
- [ ] Expect and triage new console errors from converters that were already misconfigured — they no longer report only once (see § 5.4)
- [ ] Move `[SerializeReference]` converter fields and code off the `[Obsolete]` `IConverterXToY` aliases onto `IConverter<TFrom, TTo>` — you have one release, and the failure after that is silent (see § 5.2)
- [ ] Review every two-way binding that has a converter: the reverse direction now converts (see § 5.3)
- [ ] Move any `ToStringValue` override to `Format` (see § 5.5)
