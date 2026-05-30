# Migration Guide

Upgrade notes for moving an existing project from **Aspid.MVVM 1.0** to **Aspid.MVVM 1.1**.

For the full list of changes see [CHANGELOG.md](CHANGELOG.md).

> **BREAKING — package id renamed.** The UPM package id changed from `com.aspid.mvvm` to `tech.aspid.mvvm`. Update the entry in `Packages/manifest.json` and any UPM git URL that referenced the old id.

> Unity asset references (prefabs, scenes, ScriptableObjects) survive the upgrade because every relocated script kept its original `.meta` GUID. Source-code references to renamed classes do **not** survive — search-and-replace is required.

---

## TL;DR

```bash
git pull
git submodule update --init --recursive
```

Then in your codebase:

1. Rename `ViewModelObservableList*` → `ObservableList*ViewModel`, and the same shape for Dictionary / Collection — see the table below.
2. Replace every `[AddComponentContextMenu(typeof(X), "path")]` with `[AddBinderContextMenu(typeof(X), Path = "path")]`.
3. Drop any `[AddPropertyContextMenu]` attributes — the API was removed.
4. Audit every `view.Dispose()` call: the GameObject is no longer destroyed automatically.
5. If you ever caught the `"This Binder is already bound."` exception, switch to checking `binder.IsBound` first — it now logs and returns silently.
6. Rebuild generators with .NET 9 SDK (only if you modify generator source; the bundled DLLs are already up to date).

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
| `EnumMonoBinderEditorBase` | `EnumMonoBinderEditor` |

Suggested approach: a single global rename per row (regex / IDE refactor). Namespace `Aspid.MVVM.StarterKit` is unchanged.

### 1.2 `AddComponentContextMenuAttribute` removed

The attribute was replaced with `AddBinderContextMenuAttribute` (and the byte-type variant `AddBinderContextMenuByTypeAttribute`). The signature changed: the path is now a named property.

```csharp
// BEFORE — Aspid.MVVM 1.0
[AddComponentContextMenu(typeof(Component), "Add General Binder/My/Path")]
public class MyBinder : MonoBinder { }

// AFTER — Aspid.MVVM 1.1
[AddBinderContextMenu(typeof(Component), Path = "Add General Binder/My/Path")]
public class MyBinder : MonoBinder { }
```

`AddPropertyContextMenuAttribute` has no replacement and should be removed; the new editor pipeline handles property menus internally.

### 1.3 Submodules are required

`Aspid.MVVM.Generators`, `Aspid.MVVM.Analyzers`, `Aspid.MVVM.Unity.Generators` are now consumed via git submodules. Run:

```bash
git submodule update --init --recursive
```

after pulling. Without this Unity will not compile. `Aspid.Collections` is no longer a submodule — it is consumed as a UPM git package (`tech.aspid.collections`).

### 1.4 `Aspid.FastTools` namespace pulled in

`MonoView`, the new editor visuals and several runtime helpers reference types under `Aspid.FastTools.Types` (`TypeSelector`, `EnumValue`, `ComponentTypeSelector`, `SerializableType`, `IId`, `IdRegistry`, `UniqueIdAttribute`).

Risks:
- Name collisions if your project ships its own `TypeSelector` / `EnumValue` / `IId`.
- Custom IMGUI drawers referencing the old `Aspid.MVVM.*` versions of these types must switch to `Aspid.FastTools.*`.

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

### 2.3 `MonoBinder.Bind()` no longer throws on double-bind

```csharp
// 1.0
if (IsBound) throw new Exception("This Binder is already bound.");

// 1.1
if (IsBound) {
    Debug.LogError($"Binder is already bound. Type: {GetType().Name}, Name: {name}");
    return;
}
```

If you had a `try / catch` around `Bind()`, replace it with an `if (!binder.IsBound)` guard.

### 2.4 `OnReplace` / `OnMove` hooks fire natively

`CollectionMonoBinder<T>` derivatives now receive `OnReplace` / `OnMove` directly (instead of the previous `Remove + Insert` translation). Custom binders that hand-rolled `Replace` may need to override the new hooks or accept the default behaviour.

Batch `Replace` events are also unrolled into per-item `OnReplace` calls.

### 2.5 Addressable seamless swap

`AddressableMonoBinder` ships an opt-in seamless-swap mode. Default behaviour is unchanged. If you subclass an Addressable binder and override the swap path, double-check the new flag and lifecycle.

### 2.6 `[AddComponentMenu]` paths

A number of menu paths were normalised:

- "Collections/…" → "Collection/…" (singular).
- ASCII hyphen `-` between words → en-dash `–`.

Tooling that searches the Add Component dialog or menu paths by exact string needs to be updated.

---

## 3. Project / infrastructure

### 3.1 Unity project relocated

The Unity project tree moved from the repository root into `Aspid.MVVM/`:

```
1.0:  <repo>/Assets, <repo>/ProjectSettings, <repo>/Packages
1.1:  <repo>/Aspid.MVVM/Assets, <repo>/Aspid.MVVM/ProjectSettings, ...
```

Update CI/CD scripts, IDE workspaces, build pipelines and any path constants that pointed at the repo-root `Assets/`.

### 3.2 .NET 9 SDK

`global.json` (inside the generator / analyzer submodules) pins `9.0.x`. Required only if you rebuild the generators yourself; the bundled `Aspid.MVVM.Generators.dll` and `Aspid.MVVM.Analyzers.dll` are committed and consumed directly by Unity.

### 3.3 Unity Editor versions

- `package.json` declares `"unity": "2022.3"` — minimum unchanged.
- Repository project file is now on Unity `6000.3.0f1`.

Unity 2022.3 / 2023.x stay supported in code (`#if UNITY_2023_1_OR_NEWER` branches retained). Opening on older 6000.x releases may require dismissing the version-mismatch dialog.

---

## 4. Architectural notes

### 4.1 `GeneralView` no longer exists

`GeneralView` lived briefly between PR #43 and PR #48 in the 1.1 development branch. It was merged back into `MonoView`. If you adopted `GeneralView` from a pre-release build, replace `GeneralView` with `MonoView` — the inspector-driven binder list, child validation and `[RequireBinder]` integration carried over.

### 4.2 `BindSafely` / `UnbindSafely`

New overloads accept `owner` and `memberName` so error messages include the View and bindable Id. Existing call sites compile unchanged.

### 4.3 Bindable Properties

Existing `[Bind]` fields keep working. Bindable Properties (PR #46) are an additive feature — opt in per ViewModel.

### 4.4 `RelayCommand`

`RelayCommand.Empty` is preserved. New `RelayCommand.EmptyExecution` returns a command that is executable but does nothing. The internal "empty" constructor was rewritten; this is invisible from the outside but worth knowing if you use reflection.

---

## Upgrade checklist

- [ ] Rename the package id `com.aspid.mvvm` → `tech.aspid.mvvm` in `Packages/manifest.json` and any UPM git URL
- [ ] `git pull && git submodule update --init --recursive`
- [ ] Update CI / build scripts: `Assets/` → `Aspid.MVVM/Assets/`
- [ ] Install .NET 9 SDK on build agents (only if rebuilding generators)
- [ ] Global rename of StarterKit binder classes (see § 1.1)
- [ ] Replace `[AddComponentContextMenu(...)]` with `[AddBinderContextMenu(..., Path = ...)]`
- [ ] Remove `[AddPropertyContextMenu]` usages
- [ ] Add explicit `Object.Destroy(view.gameObject)` where `view.Dispose()` was used to free objects
- [ ] Replace `try { binder.Bind(...) } catch` with `if (!binder.IsBound) binder.Bind(...)`
- [ ] Audit custom `CollectionMonoBinder<T>` for `OnReplace` / `OnMove`
- [ ] Re-enter inspector data for `MonoView`s now that the base class exposes its own binders
- [ ] Smoke-test scenes that use `ImageSpriteSwitcherBinder`, Addressable binders and `VirtualizedList*`
- [ ] Update tests / tooling that look up components by `AddComponentMenu` path
