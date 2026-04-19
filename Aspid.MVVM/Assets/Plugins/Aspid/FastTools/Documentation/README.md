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
| `Aspid.FastTools` | Runtime API — types, VisualElement extensions |
| `Aspid.FastTools.Editors` | Editor-only API — property drawers, IMGUI scopes, editor extensions |

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
using Aspid.FastTools;

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
using Aspid.FastTools;

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
using Aspid.FastTools;

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
using Aspid.FastTools;

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

## String ID System

Maps string names to stable integer IDs. Designed for data-driven setups where assets need a reliable, editor-assignable identifier that can be safely used in switch statements and dictionaries.

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
    [SerializeField] private string __stringId;
    [SerializeField] private int _id;

    public int Id => _id;
}
```

**2.** Create an `IdRegistry` asset via *Assets → Create → Aspid → FastTools → String Id Registry* and bind it to the struct type in the Inspector.

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

### IdRegistry

A `ScriptableObject` that stores name ↔ integer pairs. Each name is assigned a stable, auto-incrementing ID that never changes even when other entries are added or removed.

| Member | Description |
|--------|-------------|
| `bool Contains(string name)` | Returns whether a name is registered |
| `int Add(string name)` | Registers a name and returns its ID; returns the existing ID if already registered |
| `void Remove(string name)` | Removes an entry by name |
| `void Rename(string oldName, string newName)` | Renames an entry |
| `int GetId(string name)` | Returns the ID for a name, or `0` if not found |
| `string? GetName(int id)` | Returns the name for an ID, or `null` if not found |

---

## SerializedProperty Extensions

Chainable extension methods on `SerializedProperty` for setting values and applying changes.

```csharp
using Aspid.FastTools.Editors;
```

### Update and Apply

```csharp
property.Update();
property.UpdateIfRequiredOrScript();
property.ApplyModifiedProperties();
```

All methods return `SerializedProperty`, enabling chaining.

### SetValue / SetXxx

For each supported type there are two methods:

- `SetXxx(value)` — sets the value, returns `property` for chaining
- `SetXxxAndApply(value)` — sets the value and immediately calls `ApplyModifiedProperties()`

`SetValue(value)` dispatches to the appropriate typed setter automatically.

| Method family | Unity type |
|---------------|-----------|
| `SetInt` / `SetUint` / `SetLong` / `SetUlong` | Integer types |
| `SetFloat` / `SetDouble` | Float types |
| `SetBool` | `bool` |
| `SetString` | `string` |
| `SetColor` | `Color` |
| `SetRect` / `SetRectInt` | `Rect` / `RectInt` |
| `SetBounds` / `SetBoundsInt` | `Bounds` / `BoundsInt` |
| `SetVector2` / `SetVector2Int` | `Vector2` / `Vector2Int` |
| `SetVector3` / `SetVector3Int` | `Vector3` / `Vector3Int` |
| `SetVector4` | `Vector4` |
| `SetQuaternion` | `Quaternion` |
| `SetGradient` | `Gradient` |
| `SetHash128` | `Hash128` |
| `SetAnimationCurveValue` | `AnimationCurve` |
| `SetEnumFlag` / `SetEnumIndex` | Enum flag/index |
| `SetArraySize` | Array size |
| `SetManagedReference` | Managed reference |
| `SetObjectReference` | `UnityEngine.Object` |
| `SetExposedReference` | Exposed reference |
| `SetBoxed` | Boxed value *(Unity 6+)* |
| `SetEntityId` | Entity ID *(Unity 6.2+)* |

```csharp
SerializedProperty property = GetProperty();

// Simple apply
property.ApplyModifiedProperties();

// Set and apply — equivalent forms
property.SetValue(10).ApplyModifiedProperties();
property.SetValueAndApply(10);
property.SetInt(10).ApplyModifiedProperties();
property.SetIntAndApply(10);

// Chain multiple setters
property.SetVector3(Vector3.up).SetBool(true).ApplyModifiedProperties();
```

---

## IMGUI Layout Scopes

```csharp
using Aspid.FastTools.Editors;
```

Three scope types are available: `VerticalScope`, `HorizontalScope`, `ScrollViewScope`. Each exposes a `Rect` property and calls the matching `EditorGUILayout.End*` method on `Dispose`.

### Usage via AspidEditorGUILayout

```csharp
using (AspidEditorGUILayout.BeginVertical())
{
    EditorGUILayout.LabelField("Item 1");
    EditorGUILayout.LabelField("Item 2");
}

using (AspidEditorGUILayout.BeginHorizontal())
{
    EditorGUILayout.LabelField("Left");
    EditorGUILayout.LabelField("Right");
}

var scrollPos = Vector2.zero;
using (AspidEditorGUILayout.BeginScrollView(ref scrollPos))
{
    EditorGUILayout.LabelField("Scrollable content");
}
```

### Usage via scope structs directly

```csharp
using (VerticalScope.Begin()) { /* ... */ }
using (HorizontalScope.Begin()) { /* ... */ }
using (ScrollViewScope.Begin(ref scrollPos)) { /* ... */ }
```

All `Begin` overloads match the corresponding `EditorGUILayout.Begin*` signatures (with optional `GUIStyle`, `GUILayoutOption[]`, scroll view options, etc.).

---

## VisualElement Extensions

Fluent extension methods for building UIToolkit trees in code. All methods return `T` (the element itself) for chaining.

```csharp
using Aspid.FastTools;         // runtime extensions
using Aspid.FastTools.Editors; // editor-only extensions
```

### Core element operations

```csharp
element
    .SetName("MyElement")
    .SetVisible(true)
    .SetTooltip("Tooltip text")
    .AddChild(new Label("Hello"))
    .AddChildren(child1, child2, child3);
```

| Method | Description |
|--------|-------------|
| `SetName(string)` | Sets `element.name` |
| `SetVisible(bool)` | Sets `element.visible` |
| `SetTooltip(string)` | Sets `element.tooltip` |
| `SetUserData(object)` | Sets `element.userData` |
| `SetEnabledSelf(bool)` | Sets `element.enabledSelf` |
| `SetPickingMode(PickingMode)` | Sets `element.pickingMode` |
| `SetUsageHints(UsageHints)` | Sets `element.usageHints` |
| `SetViewDataKey(string)` | Sets `element.viewDataKey` |
| `SetLanguageDirection(LanguageDirection)` | Sets `element.languageDirection` |
| `SetDisablePlayModeTint(bool)` | Sets `element.disablePlayModeTint` |
| `SetDataSource(object)` | Sets `element.dataSource` |
| `SetDataSourceType(Type)` | Sets `element.dataSourceType` |
| `SetDataSourcePath(PropertyPath)` | Sets `element.dataSourcePath` |
| `AddChild(VisualElement)` | Appends a child, returns the parent |
| `AddChildren(params VisualElement[])` | Appends multiple children |
| `AddChildren(IEnumerable<VisualElement>)` | Appends from a sequence |
| `AddChildren(List<VisualElement>)` | Appends from a list |
| `AddChildren(Span<VisualElement>)` | Appends from a span |
| `AddChildren(ReadOnlySpan<VisualElement>)` | Appends from a read-only span |

> `RegisterCallbackOnce<TEventType>` and `RegisterCallbackOnce<TEventType, TUserArgsType>` are available on all Unity versions (polyfill included for versions prior to 2023.1).

### Focusable

| Method | Description |
|--------|-------------|
| `SetFocus()` | Attempts to give focus to the element |
| `SetBlur()` | Tells the element to release focus |
| `IsFocus()` | Returns whether the element currently has keyboard focus |
| `SetTabIndex(int)` | Sets `element.tabIndex` |
| `SetFocusable(bool)` | Sets `element.focusable` |
| `SetDelegatesFocus(bool)` | Sets `element.delegatesFocus` |

### USS & class operations

| Method | Description |
|--------|-------------|
| `AddClass(string)` | Adds a USS class |
| `RemoveClass(string)` | Removes a USS class |
| `ClearClasses()` | Removes all USS classes |
| `ToggleInClass(string)` | Toggles a USS class on/off |
| `EnableInClass(string, bool)` | Adds or removes a USS class based on a condition |
| `AddStyleSheets(StyleSheet)` | Adds a `StyleSheet` |
| `RemoveStyleSheets(StyleSheet)` | Removes a `StyleSheet` |
| `AddStyleSheetsFromResource(string)` | Adds a stylesheet loaded via `Resources.Load` |
| `RemoveStyleSheetsFromResource(string)` | Removes a stylesheet loaded via `Resources.Load` |

### Style extensions — by category

All style methods are also available on `IStyle` directly (same method names, operate on the style object).

#### Layout

| Method | Style property |
|--------|---------------|
| `SetFlexBasis(StyleLength)` | `flexBasis` |
| `SetFlexGrow(StyleFloat)` | `flexGrow` |
| `SetFlexShrink(StyleFloat)` | `flexShrink` |
| `SetFlexWrap(StyleEnum<Wrap>)` | `flexWrap` |
| `SetFlexDirection(FlexDirection)` | `flexDirection` |
| `SetAlignSelf(StyleEnum<Align>)` | `alignSelf` |
| `SetAlignItems(StyleEnum<Align>)` | `alignItems` |
| `SetAlignContent(StyleEnum<Align>)` | `alignContent` |
| `SetJustifyContent(StyleEnum<Justify>)` | `justifyContent` |
| `SetPosition(StyleEnum<Position>)` | `position` |

#### Size

| Method | Description |
|--------|-------------|
| `SetSize(StyleLength)` | Sets both width and height |
| `SetSize(width?, height?)` | Sets width and/or height independently |
| `SetMinSize(StyleLength)` | Sets both minWidth and minHeight |
| `SetMinSize(width?, height?)` | |
| `SetMaxSize(StyleLength)` | Sets both maxWidth and maxHeight |
| `SetMaxSize(width?, height?)` | |

#### Spacing

All spacing methods have a uniform-value overload and a per-side overload (`top`, `bottom`, `left`, `right`).

| Method | Style properties                                   |
|--------|----------------------------------------------------|
| `SetMargin(…)` | `Top/Bottom/Left/Right`                            |
| `SetPadding(…)` | `Top/Bottom/Left/Right`                            |
| `SetDistance(…)` | `Top/Bottom/Left/Right` (absolute position offset) |

#### Font

| Method | Style property |
|--------|---------------|
| `SetUnityFont(StyleFont)` | `unityFont` |
| `SetFontSize(StyleLength)` | `fontSize` |
| `SetUnityFontDefinition(StyleFontDefinition)` | `unityFontDefinition` |
| `SetUnityFontStyleAndWeight(StyleEnum<FontStyle>)` | `unityFontStyleAndWeight` |

#### Font style presets

Convenience methods for toggling bold / italic without overwriting the other flag:

| Method | Description |
|--------|-------------|
| `SetNormalUnityFontStyleAndWeight()` | Resets to `FontStyle.Normal` |
| `AddBoldUnityFontStyleAndWeight()` | Adds bold, preserving italic |
| `RemoveBoldUnityFontStyleAndWeight()` | Removes bold, preserving italic |
| `AddItalicUnityFontStyleAndWeight()` | Adds italic, preserving bold |
| `RemoveItalicUnityFontStyleAndWeight()` | Removes italic, preserving bold |

#### Text

| Method | Style property | Notes |
|--------|---------------|-------|
| `SetWorldSpacing(StyleLength)` | `wordSpacing` | |
| `SetLetterSpacing(StyleLength)` | `letterSpacing` | |
| `SetUnityTextAlign(TextAnchor)` | `unityTextAlign` | |
| `SetTextShadow(StyleTextShadow)` | `textShadow` | |
| `SetUnityTextOutlineColor(StyleColor)` | `unityTextOutlineColor` | |
| `SetUnityTextOutlineWidth(StyleFloat)` | `unityTextOutlineWidth` | |
| `SetUnityParagraphSpacing(StyleLength)` | `unityParagraphSpacing` | |
| `SetTextOverflow(StyleEnum<TextOverflow>)` | `textOverflow` | |
| `SetUnityTextOverflowPosition(TextOverflowPosition)` | `unityTextOverflowPosition` | |
| `SetUnityTextGenerator(TextGeneratorType)` | `unityTextGenerator` | Unity 6+ |
| `SetUnityEditorTextRenderingMode(EditorTextRenderingMode)` | `unityEditorTextRenderingMode` | Unity 6+ |
| `SetUnityTextAutoSize(StyleTextAutoSize)` | `unityTextAutoSize` | Unity 6.2+ |
| `SetWhiteSpace(StyleEnum<WhiteSpace>)` | `whiteSpace` | |

#### Color & Opacity

| Method | Style property |
|--------|---------------|
| `SetColor(StyleColor)` | `color` |
| `SetOpacity(StyleFloat)` | `opacity` |

#### Border

| Method | Description |
|--------|-------------|
| `SetBorderColor(StyleColor)` | All sides |
| `SetBorderColor(top?, bottom?, left?, right?)` | Per side |
| `SetBorderRadius(StyleLength)` | All corners |
| `SetBorderRadius(topLeft?, topRight?, bottomLeft?, bottomRight?)` | Per corner |
| `SetBorderWidth(StyleFloat)` | All sides |
| `SetBorderWidth(top?, bottom?, left?, right?)` | Per side |

#### Background

| Method | Style property |
|--------|---------------|
| `SetBackgroundColor(StyleColor)` | `backgroundColor` |
| `SetBackgroundImage(StyleBackground)` | `backgroundImage` |
| `SetBackgroundSize(StyleBackgroundSize)` | `backgroundSize` |
| `SetBackgroundRepeat(StyleBackgroundRepeat)` | `backgroundRepeat` |
| `SetBackgroundPosition(StyleBackgroundPosition)` | Both X and Y |
| `SetBackgroundPosition(x?, y?)` | Independently |
| `SetUnityBackgroundImageTintColor(StyleColor)` | `unityBackgroundImageTintColor` |

#### Transform

| Method | Style property |
|--------|---------------|
| `SetScale(StyleScale)` | `scale` |
| `SetRotate(StyleRotate)` | `rotate` |
| `SetTranslate(StyleTranslate)` | `translate` |
| `SetTransformOrigin(StyleTransformOrigin)` | `transformOrigin` |

#### Transition

| Method | Style property |
|--------|---------------|
| `SetTransitionDelay(StyleList<TimeValue>)` | `transitionDelay` |
| `SetTransitionDuration(StyleList<TimeValue>)` | `transitionDuration` |
| `SetTransitionProperty(StyleList<StylePropertyName>)` | `transitionProperty` |
| `SetTransitionTimingFunction(StyleList<EasingFunction>)` | `transitionTimingFunction` |

#### Overflow & Visibility

| Method | Style property |
|--------|---------------|
| `SetOverflow(StyleEnum<Overflow>)` | `overflow` |
| `SetUnityOverflowClipBox(StyleEnum<OverflowClipBox>)` | `unityOverflowClipBox` |
| `SetVisibility(StyleEnum<Visibility>)` | `visibility` |
| `SetDisplay(DisplayStyle)` | `display` |

#### Unity Slice

| Method | Description |
|--------|-------------|
| `SetUnitySlice(StyleInt)` | All sides |
| `SetUnitySlice(top?, bottom?, left?, right?)` | Per side |
| `SetUnitySliceType(StyleEnum<SliceType>)` | Unity 6+ |

#### Cursor

| Method | Style property |
|--------|---------------|
| `SetCursor(StyleCursor)` | `cursor` |

### Specialized element extensions

#### TextElement

```csharp
label.SetText("Hello World");
```

#### BaseField\<TValueType\>

```csharp
field.SetLabel("My Field");
field.SetValue(42);
```

#### INotifyValueChanged\<T\>

```csharp
field.SetValue(42, notify: false); // sets value without raising ChangeEvent
field.AddValueChanged(evt => Debug.Log(evt.newValue));
field.RemoveValueChanged(myCallback);
```

Typed overloads are provided for `int`, `uint`, `long`, `ulong`, `short`, `ushort`, `byte`, `sbyte`, `float`, `double`, `string`, `bool`, `Color`, `Vector2/3/4`, `Vector2Int/3Int`, `Rect/RectInt`, `Bounds/BoundsInt`, `Hash128`, `Enum`, `Object`, and more.

#### IMixedValueSupport

```csharp
field.SetShowMixedValue(true); // shows the mixed-value indicator
```

#### Button

```csharp
button
    .AddClicked(() => Debug.Log("Clicked"))
    .SetClickable(new Clickable(() => { }))
    .SetIconImage(myBackground);
```

| Method | Description |
|--------|-------------|
| `AddClicked(Action)` | Subscribes to `Button.clicked` |
| `RemoveClicked(Action)` | Unsubscribes from `Button.clicked` |
| `SetClickable(Clickable)` | Sets `Button.clickable` |
| `SetIconImage(Background)` | Sets `Button.iconImage` |

#### Slider / BaseSlider\<TValue\>

```csharp
slider
    .SetLowValue(0f)
    .SetHighValue(100f)
    .SetShowInputField<SliderFloat, float>(true);
```

| Method | Description |
|--------|-------------|
| `SetLowValue(TValue)` | Sets the minimum slider value |
| `SetHighValue(TValue)` | Sets the maximum slider value |
| `SetFill(bool)` | Whether the track is filled up to the current value |
| `SetInverted(bool)` | Reverses the slider direction |
| `SetPageSize(float)` | Controls how much the value changes per page step |
| `SetShowInputField(bool)` | Shows a numeric input field alongside the slider |
| `SetDirection(SliderDirection)` | Sets the slider orientation |

#### ProgressBar

```csharp
progressBar.SetTitle("Loading...").SetLowValue(0f).SetHighValue(100f);
```

| Method | Description |
|--------|-------------|
| `SetTitle(string)` | Sets the title displayed in the center |
| `SetLowValue(float)` | Sets the minimum value |
| `SetHighValue(float)` | Sets the maximum value |

#### HelpBox

```csharp
helpBox.SetHelpBoxFontSize(14);
helpBox.SetMessageType(14, HelpBoxMessageType.Warning);
```

#### Foldout

```csharp
foldout.SetText("Section Title");
foldout.SetValue(true);
```

#### Image

```csharp
image.SetImage(myTexture);
image.SetImageFromResource("Editor/MyIcon"); // loads via Resources.Load
```

#### IMGUIContainer

```csharp
container
    .SetOnGUIHandler(() => GUILayout.Label("IMGUI"))
    .SetCullingEnabled(true);
```

| Method | Description |
|--------|-------------|
| `SetOnGUIHandler(Action)` | Replaces the `onGUIHandler` callback |
| `AddOnGUIHandler(Action)` | Subscribes to `onGUIHandler` |
| `RemoveOnGUIHandler(Action)` | Unsubscribes from `onGUIHandler` |
| `SetCullingEnabled(bool)` | Skips `onGUIHandler` when the element is offscreen |
| `SetContextType(ContextType)` | Sets the IMGUI context type |

#### ListView / CollectionView

| Method | Description | Notes |
|--------|-------------|-------|
| `SetBindItem(Action<VisualElement, int>)` | Item binding callback | |
| `SetMakeItem(Func<VisualElement>)` | Item factory | |
| `SetMakeFooter(Func<VisualElement>)` | Footer factory | Unity 6+ |
| `SetMakeHeader(Func<VisualElement>)` | Header factory | Unity 6+ |
| `SetMakeNoneElement(Func<VisualElement>)` | Empty state element factory | Unity 6+ |

### Editor commands (editor-only)

```csharp
using Aspid.FastTools.Editors;

image.AddOpenScriptCommand(target);
// Double-clicking the element opens the script for 'target' in the IDE
```

### Full example

```csharp
using UnityEditor;
using UnityEngine;
using Aspid.FastTools;
using Aspid.FastTools.Editors;
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
