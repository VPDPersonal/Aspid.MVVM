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
1.1:  <repo>/Aspid.MVVM/Assets/Aspid/MVVM/...
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
