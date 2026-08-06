<img src="https://raw.githubusercontent.com/VPDPersonal/Aspid.FastTools/main/docs/images/aspid_fasttools_readme_banner.gif" alt="Aspid.FastTools" />

[English](README.md) | [Русский](../RU/README.md)

**Aspid.FastTools** is a Unity toolset that eliminates routine boilerplate. Inside: a convenient `SerializeReference` workflow (an inspector type picker and a project-wide reference audit window), Roslyn source generators and analyzers, and runtime and editor utilities — from a serializable `System.Type` to fluent UI Toolkit extensions.

[Source Code](https://github.com/VPDPersonal/Aspid.FastTools) · [Unity Asset Store](https://assetstore.unity.com/packages/slug/365584) · [Releases](https://github.com/VPDPersonal/Aspid.FastTools/releases)

## Table of Contents

- **Getting Started**
  - [Installation](#installation) — UPM git URL, `.unitypackage`, Asset Store
- **Features**
  - [Serializable Type System](#serializable-type-system) — `System.Type` as a serialized field, with a searchable type-picker window
  - [SerializeReference Selector](#serializereference-selector) — a type-picker dropdown for `[SerializeReference]` fields, plus tooling that finds and repairs broken references
  - [ProfilerMarker](#profilermarker) — source-generated, per-call-site profiler markers
  - [Enum System](#enum-system) — serializable enum → value maps, `[Flags]`-aware
  - [ID System (Beta)](#id-system-beta) — asset-assignable names mapped to stable integer IDs
  - [VisualElement Extensions](#visualelement-extensions) — fluent UI Toolkit tree building in code
  - [SerializedProperty Extensions](#serializedproperty-extensions) — chainable typed setters and reflection helpers
  - [IMGUI Layout Scopes](#imgui-layout-scopes) — disposable `Begin*`/`End*` wrappers with `Rect` access
  - [Editor Helper Extensions](#editor-helper-extensions) — display names for scripts in custom editors
- **Extras**
  - [Claude Code Plugin](#claude-code-plugin) — skills that teach Claude Code this package
  - [Donate](#donate)
  - [License](#license)

---

## Installation

Install Aspid.FastTools via UPM: in the Package Manager click **+ → Install package from git URL…** and paste one of the URLs below.

> **Migrating from `com.aspid.fasttools`:** the package was renamed to `tech.aspid.fasttools` in May 2026. Unity treats it as a different package, so installs of the old id never receive updates — remove the `com.aspid.fasttools` entry from `Packages/manifest.json` and install `tech.aspid.fasttools` via one of the URLs below.

### Stable

The `upm` branch always points to the latest **stable** release:

```
https://github.com/VPDPersonal/Aspid.FastTools.git#upm
```

To install a specific version, target the immutable per-release tag `upm/<version>` — e.g. `upm/1.0.0` once the 1.0.0 release is out (see [Releases](https://github.com/VPDPersonal/Aspid.FastTools/releases) for the list of available versions):

```
https://github.com/VPDPersonal/Aspid.FastTools.git#upm/<version>
```

Prefer a manual install? Download the `.unitypackage` from the [Releases](https://github.com/VPDPersonal/Aspid.FastTools/releases) page, or get the package from the [Unity Asset Store](https://assetstore.unity.com/packages/slug/365584).

<details>
<summary><strong>Preview</strong></summary>

<br>

The `upm-preview` branch always points to the latest **preview** release (rc, beta, alpha, …):

```
https://github.com/VPDPersonal/Aspid.FastTools.git#upm-preview
```

Specific preview versions use the same per-release tag scheme:

```
https://github.com/VPDPersonal/Aspid.FastTools.git#upm-preview/1.0.0-rc.6
```

</details>

---

## Serializable Type System

Allows serializing a `System.Type` reference in the Unity Inspector. The selected type is stored as an assembly-qualified name and resolved lazily on first access.

### SerializableType

Two variants are available:

- **`SerializableType`** — stores any type
- **`SerializableType<T>`** — stores a type constrained to `T` or its subclasses

Both support implicit conversion to `System.Type`.

```csharp
using UnityEngine;
using Aspid.FastTools.Types;

public abstract class Ability : MonoBehaviour
{
    public abstract void Activate();
}

public sealed class AbilitySelector : MonoBehaviour
{
    [SerializeField] private SerializableType<Ability> _abilityType;

    private void Start()
    {
        var ability = (Ability)gameObject.AddComponent(_abilityType.Type);
        ability.Activate();
    }
}
```
![SerializableType field with the type picker in the Inspector](../Images/aspid_fasttools_serializable_type.gif)

### TypeSelectorAttribute

Adds a type-picker button next to a field in the Inspector: it opens a hierarchical, searchable window listing only the types compatible with the given base types (with several bases, a candidate must satisfy all of them; with no arguments, any type qualifies). What picking a type does depends on the field shape:

- `string` — the field receives the assembly-qualified name of the chosen type;
- `SerializableType` / `SerializableType<T>` — narrows the built-in selector; the attribute's base types are intersected with the generic argument `T`;
- a `[SerializeReference]` managed reference — the chosen type is instantiated into the field right away (see [SerializeReference Selector](#serializereference-selector)).

The attribute is editor-only (`[Conditional("UNITY_EDITOR")]`) and carries no runtime cost.

```csharp
using UnityEngine;
using Aspid.FastTools.Types;

public interface IStackable { }

public abstract class AbilityModifier
{
    public abstract void Apply();
}

public sealed class AbilitySelector : MonoBehaviour
{
    // string — stores the assembly-qualified name of the chosen type.
    // Each element of the array is its own picker constrained to AbilityModifier.
    [TypeSelector(typeof(AbilityModifier))]
    [SerializeField] private string[] _modifierTypes;

    // SerializableType — narrows the picker the field already has.
    [TypeSelector(typeof(AbilityModifier))]
    [SerializeField] private SerializableType _modifierType;

    // SerializableType<T> — T already narrows the picker on its own; the base
    // types of the attribute intersect with it: only AbilityModifier
    // implementations that are also IStackable qualify.
    [TypeSelector(typeof(IStackable))]
    [SerializeField] private SerializableType<AbilityModifier> _stackableModifierType;

    // For a [SerializeReference] field picking a type immediately creates
    // an instance and assigns it to the field. With no arguments the attribute
    // offers subtypes of the field's own type (here — AbilityModifier).
    // Required = true flags an unset field: an inspector warning
    // plus a violation for the build/CI gate.
    [TypeSelector(Required = true)]
    [SerializeReference] private AbilityModifier _modifier;
}
```

Beyond the base types, the attribute exposes `Allow` (whether abstract classes and interfaces are listed) and `Required` (an unset field — an inline warning and a violation for the build/CI gate). Covered separately in the reference: [dynamic base types via member references](Types.md#dynamic-base-types-via-member-references) and tuning a candidate type's look in the picker with [`[TypeSelectorDisplay]`](Types.md#typeselectordisplay).

### TypeSelectorWindow

A searchable, namespace-hierarchical type-picker popup — the same picker opened by `[TypeSelector]` and `SerializableType`, also available as a public API. The window offers:

- Hierarchical namespace organization
- Text search with filtering
- Keyboard navigation (Arrow keys, Enter, Escape; Space toggles a favorite)
- Breadcrumb trail with back navigation (Left arrow or a click on a crumb)
- Assembly disambiguation for types with identical names
- **Favorites** (★ on hover) and **Recent** (last picks) sections on the root page — stored locally per project (`EditorPrefs`, never committed), hidden while searching
- A `<None>` option pinned at the top and a ✓ mark on the current value — its row is pre-selected on open
- Type counters on namespace/group rows and section headers
- Generic type support — picking an open generic walks through its type parameters and emits the constructed type
- Favorites/Recent tuning (on/off, Recent capacity) in the Settings tab of the SerializeReference window

![Type Selector Window](../Images/aspid_fasttools_type_selector_window.png)

Picking an open generic walks through its argument page and returns the constructed type:

![Picking an open generic via its argument page](../Images/aspid_fasttools_type_selector_generic.gif)

The window is available as a public API (`TypeSelectorWindow.Show`) — open it from any editor code (custom inspectors, `EditorWindow`, menu items) when you need a type picker outside the standard `SerializableType` / `[TypeSelector]` flow. Signature and parameters: [Types.md](Types.md#typeselectorwindow).

### ComponentTypeSelector

A serializable struct that renders a type-switching dropdown in the Inspector. Add it as a field to a base class — picking a subtype rewrites `m_Script` on the `SerializedObject`, effectively changing the component or ScriptableObject to the chosen subtype.

The dropdown is automatically constrained to subtypes of the class that declares the field. No additional configuration is required.

```csharp
using UnityEngine;
using Aspid.FastTools.Types;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] private ComponentTypeSelector _enemyType;
    [SerializeField] [Min(0)] private float _health = 100f;

    public abstract void Attack();
}

public sealed class FastEnemy : EnemyBase
{
    [SerializeField] [Min(0)] private float _speed = 25f;

    public override void Attack() =>
        Debug.Log($"Fast enemy strikes! (speed: {_speed})");
}
```

![ComponentTypeSelector switching a component's type in the Inspector](../Images/aspid_fasttools_component_type_selector.gif)

Notes on the type-switching dropdown's behavior:

- Because the dropdown owns type-switching, the Inspector's built-in **Script** row is hidden while the selector is present — you change the type only through the dropdown (UIToolkit inspectors only; the legacy IMGUI inspector draws that row itself).

> Full reference: [Types.md](Types.md)

---

## SerializeReference Selector

A drop-in type-picker dropdown for `[SerializeReference]` fields. Add `[TypeSelector]` next to `[SerializeReference]` and the Inspector replaces the default managed-reference UI with a searchable, hierarchical [type-picker window](#typeselectorwindow). You choose which concrete implementation of the field's type is instantiated, right in the Inspector; `<None>` clears the reference.

```csharp
using System;
using UnityEngine;
using System.Collections.Generic;
using Aspid.FastTools.Types;

public interface IWeapon
{
    void Fire();
}

[Serializable]
public sealed class Pistol : IWeapon
{
    [SerializeField] [Min(0)] private int _damage = 10;

    public void Fire() => Debug.Log($"Pistol: {_damage} dmg");
}

public sealed class Loadout : MonoBehaviour
{
    [SerializeReference] [TypeSelector]
    private IWeapon _primary;

    [SerializeReference] [TypeSelector]
    private List<IWeapon> _sidearms;
}
```

The attribute is editor-only (`[Conditional("UNITY_EDITOR")]`) and carries no runtime cost. It works on single fields, arrays, and `List<T>`, in both IMGUI and UIToolkit inspectors. The same attribute also drives `string` and `SerializableType` fields — see [TypeSelectorAttribute](#typeselectorattribute).

### Capabilities

| Capability | What it does |
|---|---|
| **Pick an implementation** | The list shows the concrete, non-`UnityEngine.Object` classes assignable to the field's type. `[TypeSelector(typeof(IMelee))]` narrows it to `IMelee` implementations. |
| **Inline inspector** | The selected instance's serialized fields are drawn under a foldout. |
| **Open generics** | `Modifier<T>` and the like: arguments are inferred from a closed-generic field, or picked on a second page inside the picker. |
| **Data preserved** | Switching type carries over fields shared by name and serialized shape instead of resetting them to defaults. |
| **Copy / Paste** | Right-click the header to copy the value and paste it as an independent instance into any compatible field. |
| **Multi-object editing** | A mixed selection shows a mixed dropdown; picking a type or pasting applies an independent instance to each object in one Undo group. |
| **Compile-time checks** | Roslyn analyzer: `AFT0004` (error) — the type derives from `UnityEngine.Object`; `AFT0005` (warning) — the picker would be empty. |

### Repairing broken references

A missing type (renamed or deleted) shows a yellow notice instead of a silent clear: **Fix** re-points the type while keeping its data, and **Smart Fix** suggests the most likely replacement (`[MovedFrom]`, a moved namespace, a near-miss name) — applied in one click, never automatically. A shared reference (two fields holding one instance) is flagged and split with **Make unique**. Bulk repair lives in the **Asset References** and **Project References** tabs (`Tools → Aspid 🐍 → FastTools`): the first maps an asset's whole managed-reference graph from its YAML, the second scans every `.prefab` / `.asset` / `.unity` in the project, fixes broken references group-by-group and bakes `[MovedFrom]` renames into the files.

### Project settings & the build/CI gate

**`Project Settings → Aspid FastTools → SerializeReference`** controls breakage detection, auto de-aliasing of duplicated list elements, excluded scan folders, and the build/CI gate (`Off` / `Warn` / `Fail`) — committed values live in `ProjectSettings/SerializeReferenceSharedSettings.asset`, so teammates and CI behave identically. The same options are mirrored in the window's **Settings** tab and at **`Preferences → Aspid FastTools`**; headless CI runs the same check via `SerializeReferenceCiGate.RunCheck`.

> Full reference: [SerializeReferences.md](SerializeReferences.md) · window tabs, settings & CI gate: [SerializeReferenceTooling.md](SerializeReferenceTooling.md)

---

## ProfilerMarker

Provides source-generated `ProfilerMarker` registration. The generator creates a static marker per call-site, identified by the calling method and line number.

```csharp
using UnityEngine;

public class MyBehaviour : MonoBehaviour
{
    private void DoSomething1()
    {
        using var _ = this.Marker();
        // Some code
    }

    private void DoSomething2()
    {
        using (this.Marker())
        {
            // Some code
            using var _ = this.Marker().WithName("Calculate");
            // Some code
        }
    }
}
```

<details>
<summary><b>Generated code</b></summary>
<br/>

```csharp
using Unity.Profiling;
using System.Runtime.CompilerServices;

internal static class __MyBehaviourProfilerMarkerExtensions
{
    private static readonly ProfilerMarker DoSomething1_Marker_Line_7 = new("MyBehaviour.DoSomething1 (7)");
    private static readonly ProfilerMarker DoSomething2_Marker_Line_13 = new("MyBehaviour.DoSomething2 (13)");
    private static readonly ProfilerMarker DoSomething2_Marker_Line_16 = new("MyBehaviour.Calculate (16)");

    public static ProfilerMarker.AutoScope Marker(this MyBehaviour _, [CallerLineNumberAttribute] int line = -1)
    {
#if ENABLE_PROFILER
        if (line is 7) return DoSomething1_Marker_Line_7.Auto();
        if (line is 13) return DoSomething2_Marker_Line_13.Auto();
        if (line is 16) return DoSomething2_Marker_Line_16.Auto();
#endif
        return default;
    }
}
```

</details>

### Result

![Generated markers in the Unity Profiler window](../Images/aspid_fasttools_profiler_markers.png)

---

## Enum System

Provides serializable enum-to-value mappings configurable from the Inspector.

### EnumValues\<TValue\>

A serializable collection of `EnumValue<TValue>` entries with a configurable default value. Implements `IEnumerable<KeyValuePair<Enum, TValue>>`.

`GetValue` returns the mapped value, falling back to the configured default when the key is missing. `[Flags]` enums are supported: matching uses `HasFlag` and treats `0`-valued members correctly.

```csharp
using System;
using UnityEngine;
using Aspid.FastTools.Enums;

public enum DamageType { Physical, Fire, Ice, Poison }

[Flags]
public enum StatusEffect { None = 0, Burning = 1, Frozen = 2, Slowed = 4, Stunned = 8 }

public sealed class DamageDealer : MonoBehaviour
{
    [SerializeField] private EnumValues<float> _damageMultipliers;

    // Flag combinations (e.g. Burning | Slowed) match via HasFlag and first-hit wins,
    // so list composite entries BEFORE their constituent flags.
    [SerializeField] private EnumValues<float> _speedMultipliersByStatus;

    public float GetMultiplier(DamageType type) => _damageMultipliers.GetValue(type);

    public float GetSpeedModifier(StatusEffect effects) => _speedMultipliersByStatus.GetValue(effects);
}
```
![EnumValues in the Inspector](../Images/aspid_fasttools_enum_values.png)

In the Inspector, select the enum type in the `EnumValues` header, then assign a value for each enum member. Right-click the property to open a context menu with **Populate Missing Enum Members** — it appends an entry for every enum member not yet in the list, seeded with the current Default Value.

### EnumValues\<TEnum, TValue\>

The typed counterpart of `EnumValues<TValue>` for the common case where the enum type is already known in code. The enum is fixed by the generic argument, so the Inspector's type picker is disabled and lookups are compile-time safe. Lookups are also boxing-free — keys are compared as cached numeric values — and `foreach` over either variant binds to a struct enumerator, so iteration does not allocate. Implements `IEnumerable<KeyValuePair<TEnum, TValue>>`.

```csharp
public sealed class HitEffect : MonoBehaviour
{
    // The type picker in the Inspector is disabled — the enum is fixed to DamageType.
    [SerializeField] private EnumValues<DamageType, Color> _damageColors;

    public Color GetColor(DamageType type) => _damageColors.GetValue(type);
}
```

Lookup semantics (including `[Flags]` handling) are identical to `EnumValues<TValue>`.

---

## ID System (Beta)

> **Beta:** the ID System is currently in beta. The public API, generated code layout and editor workflow may change in future releases.

Maps asset-assignable names to stable integer IDs stored in an `IdRegistry` ScriptableObject, with full `int ↔ string` lookups at runtime. Use the resulting `int` in `switch` statements and `Dictionary` keys without paying for string comparisons.

### Setup

**1.** Declare a `partial struct` implementing `IId`. The source generator adds the required fields and property automatically:

```csharp
using Aspid.FastTools.Ids;

public partial struct EnemyId : IId { }
```

<details>
<summary><b>Generated code</b></summary>
<br/>

```csharp
public partial struct EnemyId
{
    [SerializeField] private string __stringId; // editor-only field, stripped from player builds
    [SerializeField] private int _id;

    public int Id => _id;
}
```

</details>

Misuse is caught at compile time by generator diagnostics (`AFID001` — missing `partial`, `AFID002` — a colliding member); generic structs and containing types are supported.

**2.** Create the registry asset and bind it to the struct type in its Inspector:
- `Assets → Create → Aspid → Id Registry`

**3.** Use the struct as a serialized field. The Inspector shows a dropdown of registered names; the selector window also lets you create new entries on the fly:

```csharp
using UnityEngine;
using Aspid.FastTools.Ids;

[CreateAssetMenu]
public class EnemyDefinition : ScriptableObject
{
    [UniqueId] [SerializeField] private EnemyId _id;
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyId _targetEnemy;

    private void Spawn()
    {
        int id = _targetEnemy.Id; // stable integer, safe for switch / Dictionary
    }
}
```

![Id selector dropdown in the Inspector](../Images/aspid_fasttools_id_selector.gif)

### UniqueIdAttribute

Marks a field as requiring a unique value across all assets of the declaring type. The Inspector shows a warning if two assets share the same ID.

```csharp
[Conditional("UNITY_EDITOR")]
public sealed class UniqueIdAttribute : PropertyAttribute { }
```

![Duplicate-ID warning in the Inspector](../Images/aspid_fasttools_id_collision.gif)

### IdRegistry

`ScriptableObject` in `Aspid.FastTools.Ids` that stores `(int, string)` entries and keeps the lookup tables available at runtime. Each name is assigned a stable, auto-incrementing ID that never changes when other entries are added or removed.

Runtime lookups cover both directions — `TryGetId` / `TryGetName`, `Contains`, enumeration of `(id, name)` pairs — and the generic counterpart `IdRegistry<T>` adds typed overloads. Entries are added, renamed and removed only through the registry inspector, not a public runtime API.

![IdRegistry inspector](../Images/aspid_fasttools_id_registry.png)

> Full reference: [Ids.md](Ids.md)

---

## VisualElement Extensions

Fluent extension methods for building UIToolkit trees in code. All methods return `T` (the element itself) for chaining.

### Example

A reactive editor for an `AbilityConfig` `ScriptableObject` — title and status pill in the header, and a Warning `HelpBox` that toggles based on `ManaCost`.

```csharp
[CustomEditor(typeof(AbilityConfig))]
internal sealed class AbilityConfigEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        var config = (AbilityConfig)target;

        var badge = new Label()
            .SetFontSize(10).SetUnityFontStyleAndWeight(FontStyle.Bold)
            .SetPaddingX(10).SetPaddingY(3).SetBorderRadius(10).SetBorderWidth(1);

        var helpBox = new HelpBox("This ability costs no mana — is that intentional?", HelpBoxMessageType.Warning)
            .SetMarginTop(8).SetBorderRadius(6);

        Refresh();
        return new VisualElement()
            .SetBorderRadius(10).SetBorderWidth(1).SetPaddingX(14).SetPaddingY(12)
            .AddChild(new VisualElement()
                .SetFlexDirection(FlexDirection.Row).SetAlignItems(Align.Center)
                .AddChild(new Label(target.GetScriptName()).SetFlexGrow(1).SetFontSize(15))
                .AddChild(badge))
            .AddChild(new PropertyField(serializedObject.FindProperty("_manaCost")).AddValueChanged(_ => Refresh()))
            .AddChild(helpBox);

        void Refresh()
        {
            var isFree = config.ManaCost is 0;
            badge.SetText(isFree ? "FREE" : $"{config.ManaCost} MP");
            helpBox.SetDisplay(isFree ? DisplayStyle.Flex : DisplayStyle.None);
        }
    }
}
```

### Result

![The AbilityConfig inspector built with the fluent extensions](../Images/aspid_fasttools_visual_element.gif)

> Full reference: [VisualElementExtensions.md](VisualElementExtensions.md)

---

## SerializedProperty Extensions

Chainable extensions on `SerializedProperty` for synchronizing the owning `SerializedObject`, writing typed values, and reflecting on the underlying field.

```csharp
property
    .Update()
    .SetVector3(Vector3.up)
    .SetBool(true)
    .ApplyModifiedProperties();
```

The package covers:

- **Update / Apply** — `Update`, `UpdateIfRequiredOrScript`, `ApplyModifiedProperties`.
- **Typed setters** — `SetValue` (generic dispatch) and `SetXxx` for every common Unity-serializable type, from primitives to `Gradient` and `AnimationCurve` — each with a paired `SetXxxAndApply` variant.
- **Enum setters** — `SetEnumFlag` and `SetEnumIndex` (each + `AndApply`).
- **Arrays** — `SetArraySize`, `AddArraySize`, `RemoveArraySize` (each + `AndApply`).
- **References** — `SetManagedReference`, `SetObjectReference`, `SetExposedReference`, and `SetBoxed` (Unity 6+).
- **Reflection helpers** — `GetPropertyType`, `GetFieldInfo`, `GetDeclaringInstance` for resolving the C# member and runtime instance behind a property.

> Full reference: [SerializedPropertyExtensions.md](SerializedPropertyExtensions.md)

---

## IMGUI Layout Scopes

Three `ref struct` scopes — `VerticalScope`, `HorizontalScope`, `ScrollViewScope` — wrap `EditorGUILayout.Begin*` / `End*`. Each exposes a `Rect` property and calls the matching `End*` method on `Dispose`:

```csharp
using (VerticalScope.Begin())
{
    EditorGUILayout.LabelField("Item 1");
    EditorGUILayout.LabelField("Item 2");
}

var scrollPos = Vector2.zero;
using (ScrollViewScope.Begin(ref scrollPos))
{
    EditorGUILayout.LabelField("Scrollable content");
}
```

All `Begin` overloads match the corresponding `EditorGUILayout.Begin*` signatures (optional `GUIStyle`, `GUILayoutOption[]`, scroll view options), plus an `out Rect` variant for drawing into the group's rect.

---

## Editor Helper Extensions

Display-name helpers for Unity objects in custom editors:

| Method | Returns |
|---|---|
| `GetScriptName()` | The object's display name — `ObjectNames.GetInspectorTitle` when the type has `[AddComponentMenu]`, otherwise the nicified type name |
| `GetScriptNameWithIndex()` | The same name plus a count suffix when the GameObject holds several components of the same type — e.g. `"Audio Source (2)"` |

```csharp
[CustomEditor(typeof(MyBehaviour))]
public class MyBehaviourEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        // "My Behaviour" — or "Custom Name" if [AddComponentMenu("Custom Name")] is present
        var name = target.GetScriptName();

        // "My Behaviour (2)" when a second component of the same type exists
        var nameWithIndex = ((Component)target).GetScriptNameWithIndex();

        return new Label(name);
    }
}
```

---

## Claude Code Plugin

If you use [Claude Code](https://docs.claude.com/en/docs/claude-code), the companion [Aspid.Claude.Plugins](https://github.com/VPDPersonal/Aspid.Claude.Plugins) marketplace ships the `aspid-fasttools` plugin — a set of skills that teach Claude Code this package's conventions and APIs.

> [!WARNING]
> The plugin is still in beta — its skills and commands may change between releases.

Add the marketplace and install the plugin:

```sh
/plugin marketplace add VPDPersonal/Aspid.Claude.Plugins
```

```sh
/plugin install aspid-fasttools@aspid-claude-plugins
```

Included skills:

- **`aspid-id-struct`** — scaffold a new `IId` struct and `[UniqueId]` fields for the [ID System](#id-system-beta).
- **`aspid-profiler-marker`** — insert `this.Marker()` call sites with the right `using`/scope shape.
- **`aspid-visual-element-fluent`** — build editor or runtime UI using the fluent `VisualElement` extensions.

---

## Donate

This project is developed on a voluntary basis. If you find it useful, you can support its development by purchasing the package on the [Unity Asset Store](https://assetstore.unity.com/packages/slug/365584) — that helps allocate more time to improving and maintaining **Aspid.FastTools**.

---

## License

**Aspid.FastTools** is distributed under the [MIT License](https://github.com/VPDPersonal/Aspid.FastTools/blob/main/LICENSE). Release history lives in the [CHANGELOG](https://github.com/VPDPersonal/Aspid.FastTools/blob/main/CHANGELOG.md).
