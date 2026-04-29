# Aspid.FastTools

**Aspid.FastTools** is a set of tools designed to minimize routine code writing in Unity.

## Source Code

[[Aspid.FastTools](https://github.com/VPDPersonal/Aspid.FastTools)]


---

## Integration

Install Aspid.FastTools using one of the following methods:

- **Download .unitypackage** — Visit the [Release page on GitHub](https://github.com/VPDPersonal/Aspid.FastTools/releases) and download the latest version, `Aspid.FastTools.X.X.X.unitypackage`. Import it into your project.
- **Via UPM** (Unity Package Manager) integrate the following packages:
  - `https://github.com/VPDPersonal/Aspid.Internal.Unity.git`
  - `https://github.com/VPDPersonal/Aspid.FastTools.git?path=Aspid.FastTools/Assets/Plugins/Aspid/FastTools`

---

## Namespaces

| Namespace | Description |
|-----------|-------------|
| `Aspid.FastTools` | `IId`, `UniqueIdAttribute`, `StringIdRegistry` |
| `Aspid.FastTools.Types` | `SerializableType`, `SerializableType<T>`, `ComponentTypeSelector`, `TypeSelectorAttribute` |
| `Aspid.FastTools.Enums` | `EnumValues<T>` |
| `Aspid.FastTools.Ids` | `IdRegistry` (int-only at runtime) |
| `Aspid.FastTools.UIElements` | Runtime `VisualElement` fluent extensions |
| `Aspid.FastTools.Editors` | Editor helpers — `SerializedProperty` extensions, IMGUI scopes, `GetScriptName` |
| `Aspid.FastTools.Types.Editors` · `.Enums.Editors` · `.Ids.Editors` · `.UIElements.Editors` | Per-feature editor code (property drawers, registry inspector, editor-only `VisualElement` extensions) |

---

## ProfilerMarker

Provides source-generated `ProfilerMarker` registration. The generator creates a static marker per call-site, identified by the calling method and line number.

```csharp
using UnityEngine;
using Aspid.FastTools;

public class MyBehaviour : MonoBehaviour
{
    private void Update()
    {
        DoSomething1();
        DoSomething2();
    }

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

### Generated code

```csharp
using System;
using Unity.Profiling;
using System.Runtime.CompilerServices;

internal static class __MyBehaviourProfilerMarkerExtensions
{
    private static readonly ProfilerMarker DoSomething1_line_13 = new("MyBehaviour.DoSomething1 (13)");
    private static readonly ProfilerMarker DoSomething2_line_19 = new("MyBehaviour.DoSomething2 (19)");
    private static readonly ProfilerMarker DoSomething2_line_22 = new("MyBehaviour.Calculate (22)");

    public static ProfilerMarker.AutoScope Marker(this MyBehaviour _, [CallerLineNumberAttribute] int line = -1)
    {
        if (line is 13) return DoSomething1_line_13.Auto();
        if (line is 19) return DoSomething2_line_19.Auto();
        if (line is 22) return DoSomething2_line_22.Auto();

        throw new Exception();
    }
}
```

### Result

![Aspid.FastTools.ProfilerMarkers.png](Images/Aspid.FastTools.ProfilerMarkers.png)

---

## Serializable Type System

Allows serializing a `System.Type` reference in the Unity Inspector. The selected type is stored as an assembly-qualified name and resolved lazily on first access.

### SerializableType

Two variants are available:

- **`SerializableType`** — stores any type (base type is `object`)
- **`SerializableType<T>`** — stores a type constrained to `T` or its subclasses

Both support implicit conversion to `System.Type`.

```csharp
using UnityEngine;
using Aspid.FastTools.Types;

public class MyBehaviour : MonoBehaviour
{
    [SerializeField] private SerializableType _anyType;
    [SerializeField] private SerializableType<MonoBehaviour> _behaviourType;

    private void Start()
    {
        Type type1 = _anyType;             // implicit operator
        Type type2 = _behaviourType.Type;  // explicit property

        var instance = (MonoBehaviour)gameObject.AddComponent(type2);
    }
}
```
![Aspid.FastTools.SerializableType.png](Images/Aspid.FastTools.SerializableType.png)
### ComponentTypeSelector

A serializable struct that renders a type-switching dropdown in the Inspector. Add it as a field to a base class — picking a subtype rewrites `m_Script` on the `SerializedObject`, effectively changing the component or ScriptableObject to the chosen subtype.

The dropdown is automatically constrained to subtypes of the class that declares the field. No additional configuration is required.

```csharp
using UnityEngine;
using Aspid.FastTools.Types;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] private ComponentTypeSelector _typeSelector;
}

public class FastEnemy : BaseEnemy { }
public class TankEnemy : BaseEnemy { }
```

---

### TypeSelectorAttribute

An editor-only `PropertyAttribute` that restricts the type selection popup to specific base types. Applied to `string` fields that store assembly-qualified type names.

```csharp
[Conditional("UNITY_EDITOR")]
public sealed class TypeSelectorAttribute : PropertyAttribute
{
    public TypeSelectorAttribute() // base type: object
    public TypeSelectorAttribute(Type type)
    public TypeSelectorAttribute(params Type[] types)
    public TypeSelectorAttribute(string assemblyQualifiedName)
    public TypeSelectorAttribute(params string[] assemblyQualifiedNames)

    public bool AllowAbstractTypes { get; set; } // default: false
    public bool AllowInterfaces    { get; set; } // default: false
}
```

| Property | Description |
|----------|-------------|
| `AllowAbstractTypes` | Includes abstract classes in the picker. Default: `false` |
| `AllowInterfaces` | Includes interface types in the picker. Default: `false` |

```csharp
using UnityEngine;
using Aspid.FastTools.Types;

public class MyBehaviour : MonoBehaviour
{
    [TypeSelector(typeof(IMyInterface))]
    [SerializeField] private string _typeName;

    // Include abstract types and interfaces in the picker
    [TypeSelector(typeof(object), AllowAbstractTypes = true, AllowInterfaces = true)]
    [SerializeField] private string _anyType;
}
```

### Type Selector Window

The Inspector shows a button that opens a searchable popup window with:

- Hierarchical namespace organization
- Text search with filtering
- Keyboard navigation (Arrow keys, Enter, Escape)
- Navigation history (back button)
- Assembly disambiguation for types with identical names

![Aspid.FastTools.TypeSelectorWindow.png](Images/Aspid.FastTools.TypeSelectorWindow.png)
---

## Enum System

Provides serializable enum-to-value mappings configurable from the Inspector.

### EnumValues\<TValue\>

A serializable collection of `EnumValue<TValue>` entries with a configurable default value. Implements `IEnumerable<KeyValuePair<Enum, TValue>>`.

```csharp
[Serializable]
public sealed class EnumValues<TValue> : IEnumerable<KeyValuePair<Enum, TValue>>
```

| Member | Description |
|--------|-------------|
| `TValue GetValue(Enum enumValue)` | Returns the mapped value, or `_defaultValue` if not found |
| `bool Equals(Enum, Enum)` | Equality check with proper `[Flags]` support |

Supports `[Flags]` enums: `Equals` uses `HasFlag` and treats `0`-valued members correctly.

```csharp
using UnityEngine;
using Aspid.FastTools.Enums;

public enum Direction { Left, Right, Up, Down }

public class MyBehaviour : MonoBehaviour
{
    [SerializeField] private EnumValues<Sprite> _directionSprites;

    private void SetIcon(Direction dir)
    {
        var sprite = _directionSprites.GetValue(dir);
        _image.sprite = sprite;
    }
}
```

In the Inspector, select the enum type in the `EnumValues` header, then assign a value for each enum member. Right-click the property to open a context menu with **Populate Missing Enum Members** — it appends an entry for every enum member not yet in the list, seeded with the current Default Value.

---

## ID System

Maps an asset-assignable name to a stable integer ID. Use the resulting `int` in `switch` statements and `Dictionary` keys without paying for string lookups at runtime.

Two registry flavours are available — pick one per `IId`-struct:

| Registry | Runtime contract | When to use |
|---|---|---|
| `StringIdRegistry` (`Aspid.FastTools`) | Full `int ↔ string` map at runtime | Name lookups are needed in player builds (logs, save files, debug) |
| `IdRegistry` (`Aspid.FastTools.Ids`) | int-only at runtime; names live in an editor-only partial and are stripped from player builds | Names are only needed in the editor |

Each `IId`-struct is bound to exactly one registry of either kind — uniqueness is enforced at lookup time by `IdRegistryResolver`, which searches both types.

### Setup

**1.** Declare a `partial struct` implementing `IId`. The source generator adds the required fields and property automatically:

```csharp
using Aspid.FastTools;

public partial struct EnemyId : IId { }
```

Generated code:

```csharp
public partial struct EnemyId
{
    [SerializeField] private string __stringId; // editor-only field, stripped from player builds
    [SerializeField] private int _id;

    public int Id => _id;
}
```

**2.** Create the registry asset and bind it to the struct type in its Inspector:
- `Assets → Create → Aspid → FastTools → String Id Registry` — for `StringIdRegistry`;
- `Assets → Create → Aspid → FastTools → Id Registry` — for int-only `IdRegistry`.

**3.** Use the struct as a serialized field. The Inspector shows a dropdown of registered names with a **Create** button to add new entries inline:

```csharp
using UnityEngine;
using Aspid.FastTools;

[CreateAssetMenu]
public class EnemyDefinition : ScriptableObject
{
    [UniqueId] [SerializeField] private EnemyId _id;
}
```

```csharp
using UnityEngine;
using Aspid.FastTools;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyId _targetEnemy;

    private void Spawn()
    {
        int id = _targetEnemy.Id; // stable integer, safe for switch / Dictionary
    }
}
```

### UniqueIdAttribute

Marks a field as requiring a unique value across all assets of the declaring type. The Inspector shows a warning if two assets share the same ID.

```csharp
[Conditional("UNITY_EDITOR")]
public sealed class UniqueIdAttribute : PropertyAttribute { }
```

### StringIdRegistry

`ScriptableObject` in `Aspid.FastTools` that stores `(int, string)` entries and keeps the lookup tables available at runtime. Each name is assigned a stable, auto-incrementing ID that never changes when other entries are added or removed.

| Member | Description |
|--------|-------------|
| `int GetId(string name)` | Returns the ID for a name, or `-1` if not found |
| `string? GetNameId(int id)` | Returns the name for an ID, or `null` if not found |
| `bool Contains(int id)` | Whether an ID is registered |
| `bool Contains(string name)` | Whether a name is registered |
| `IEnumerable<int> Ids` · `IEnumerable<string> IdNames` | Enumerate registered IDs / names |
| `IEnumerator<KeyValuePair<int, string>> GetEnumerator()` | Iterate `(id, name)` pairs |

### IdRegistry

`ScriptableObject` in `Aspid.FastTools.Ids` that stores integer IDs only at runtime — names exist solely as editor metadata and are dropped in player builds.

| Member | Description |
|--------|-------------|
| `int Count` | Number of registered IDs |
| `bool Contains(int id)` | Whether an ID is registered |
| `IEnumerator<int> GetEnumerator()` | Iterate registered IDs |

Both registries expose a generic counterpart (`StringIdRegistry<T>` / `IdRegistry<T>` with `T : struct, IId`) that adds a strongly-typed `Contains(T)` overload. Edits — adding, renaming, removing entries — happen through the registry inspector and `RegistryEditorCore`, not via a public runtime API.

---

## SerializedProperty Extensions

Chainable extensions on `SerializedProperty` for synchronizing the owning `SerializedObject`, writing typed values, and reflecting on the underlying field.

```csharp
using Aspid.FastTools.Editors;
```

```csharp
property
    .Update()
    .SetVector3(Vector3.up)
    .SetBool(true)
    .ApplyModifiedProperties();
```

The package covers:

- **Update / Apply** — `Update`, `UpdateIfRequiredOrScript`, `ApplyModifiedProperties`.
- **Typed setters** — `SetValue` (generic dispatch) and `SetXxx` for `int`/`uint`/`long`/`ulong`/`float`/`double`/`bool`/`string`/`Color`/`Gradient`/`Hash128`/`Rect`/`RectInt`/`Bounds`/`BoundsInt`/`Vector2..4` (and `Vector2/3Int`)/`Quaternion`/`AnimationCurve`/`EntityId` (Unity 6.2+). Each comes with a paired `SetXxxAndApply` variant.
- **Enum setters** — `SetEnumFlag` and `SetEnumIndex` (each + `AndApply`).
- **Arrays** — `SetArraySize`, `AddArraySize`, `RemoveArraySize` (each + `AndApply`).
- **References** — `SetManagedReference`, `SetObjectReference`, `SetExposedReference`, and `SetBoxed` (Unity 6+).
- **Reflection helpers** — `GetPropertyType`, `GetMemberInfo`, `GetClassInstance` for resolving the C# member and runtime instance behind a property.

> Full method-by-method reference: [SerializedPropertyExtensions.md](SerializedPropertyExtensions.md)

---

## IMGUI Layout Scopes

```csharp
using Aspid.FastTools.Editors;
```

Three `ref struct` scopes — `VerticalScope`, `HorizontalScope`, `ScrollViewScope` — wrap `EditorGUILayout.Begin*` / `End*`. Each exposes a `Rect` property and calls the matching `End*` method on `Dispose`:

```csharp
using (VerticalScope.Begin())
{
    EditorGUILayout.LabelField("Item 1");
    EditorGUILayout.LabelField("Item 2");
}

using (HorizontalScope.Begin())
{
    EditorGUILayout.LabelField("Left");
    EditorGUILayout.LabelField("Right");
}

var scrollPos = Vector2.zero;
using (ScrollViewScope.Begin(ref scrollPos))
{
    EditorGUILayout.LabelField("Scrollable content");
}
```

Capture the group rect with the `out`-overload when needed:

```csharp
using (VerticalScope.Begin(out var rect, GUI.skin.box))
{
    EditorGUI.DrawRect(rect, new Color(0, 0, 0, 0.1f));
    EditorGUILayout.LabelField("Boxed content");
}
```

All `Begin` overloads match the corresponding `EditorGUILayout.Begin*` signatures (optional `GUIStyle`, `GUILayoutOption[]`, scroll view options, etc.).

---

## VisualElement Extensions

Fluent extension methods for building UIToolkit trees in code. All methods return `T` (the element itself) for chaining.

```csharp
using Aspid.FastTools.UIElements;         // runtime extensions
using Aspid.FastTools.UIElements.Editors; // editor-only extensions (e.g. AddOpenScriptCommand)
```

### Quick reference

The package covers:

- **Core element operations** — name, visibility, tooltip, user data, picking mode, data source, and `AddChild`/`InsertChild` helpers.
- **Focus** — `SetFocus`, `SetBlur`, `SetTabIndex`, `SetFocusable`.
- **USS** — `AddClass`/`RemoveClass`/`ToggleInClass`/`EnableInClass`, `AddStyleSheets[FromResource]`.
- **Styles** — every `IStyle` property: layout, size, spacing, font, text, color, border, background, transform (incl. Unity 6.3+ aspect/filter/material), transition, overflow, slice, cursor.
- **Specialized elements** — `TextElement`, `ITextEdition`, `ITextSelection`, `BaseField`, `BaseBoolField` (Toggle), `INotifyValueChanged` (with optional `Unity.Mathematics` types), `IMixedValueSupport`, `Button`, `Slider`/`BaseSlider`, `ProgressBar`, `HelpBox`, `Foldout`, `Image`, `IMGUIContainer`, plus the full `ListView`/`TreeView`/`MultiColumn*` surface.
- **Editor-only commands** — `AddOpenScriptCommand`, `BindTo`/`BindPropertyTo`, `EnumField`/`EnumFlagsField` `Initialize`, and `PropertyField` value-change subscriptions.
- **USS custom-style helpers** — `ICustomStyle.TryGetByEnum` for parsing string USS properties as enums.

> Full method-by-method reference: [VisualElementExtensions.md](VisualElementExtensions.md)

### Full example

```csharp
using UnityEditor;
using UnityEngine;
using Aspid.FastTools.Editors;          // GetScriptName
using Aspid.FastTools.UIElements;       // runtime VisualElement extensions
using Aspid.FastTools.UIElements.Editors; // AddOpenScriptCommand
using UnityEngine.UIElements;

[CustomEditor(typeof(MyBehaviour))]
public class MyBehaviourEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        const string iconPath = "Editor/MyIcon";

        var scriptName = target.GetScriptName();
        var dark  = new Color(0.15f, 0.15f, 0.15f);
        var light = new Color(0.75f, 0.75f, 0.75f);

        return new VisualElement()
            .SetName("Header")
            .SetBackgroundColor(dark)
            .SetFlexDirection(FlexDirection.Row)
            .SetPadding(top: 5, bottom: 5, left: 10, right: 10)
            .SetBorderRadius(topLeft: 10, topRight: 10, bottomLeft: 10, bottomRight: 10)
            .AddChild(new Image()
                .SetName("Icon")
                .AddOpenScriptCommand(target)
                .SetImageFromResource(iconPath)
                .SetSize(width: 40, height: 40))
            .AddChild(new Label(scriptName)
                .SetName("Title")
                .SetFlexGrow(1)
                .SetFontSize(16)
                .SetMargin(left: 10)
                .SetColor(light)
                .SetAlignSelf(Align.Center)
                .SetOverflow(Overflow.Hidden)
                .SetWhiteSpace(WhiteSpace.NoWrap)
                .SetTextOverflow(TextOverflow.Ellipsis)
                .SetUnityFontStyleAndWeight(FontStyle.Bold));
    }
}
```

### Result

![Aspid.FastTools.VisualElement.png](Images/Aspid.FastTools.VisualElement.png)

---

## Editor Helper Extensions

Utility methods for getting display names of Unity objects in custom editors.

```csharp
using Aspid.FastTools.Editors;
```

```csharp
public static string GetScriptName(this Object obj)
```

Returns the display name of a Unity object:
- If the type has `[AddComponentMenu]`, returns `ObjectNames.GetInspectorTitle(obj)`
- Otherwise returns `ObjectNames.NicifyVariableName(typeName)`

```csharp
public static string GetScriptNameWithIndex(this Component targetComponent)
```

Returns the display name with a count suffix when multiple components of the same type exist on the same GameObject. For example, if two `AudioSource` components are attached, the second returns `"Audio Source (2)"`.

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
