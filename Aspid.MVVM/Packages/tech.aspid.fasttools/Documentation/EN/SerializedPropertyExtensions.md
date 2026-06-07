# SerializedProperty Extensions — full reference

Chainable extension methods on `SerializedProperty` for synchronizing the owning `SerializedObject`, setting values, and reflecting on the underlying field.

```csharp
using Aspid.FastTools.Editors;
```

All extensions are generic over `T : SerializedProperty` and return the same property instance, so calls can be chained freely.

## Update / Apply

Thin wrappers around the matching `SerializedObject` methods on `property.serializedObject`.

```csharp
property
    .Update()
    .SetInt(42)
    .ApplyModifiedProperties();
```

| Method | Description |
|--------|-------------|
| `Update()` | Calls `serializedObject.Update()` |
| `UpdateIfRequiredOrScript()` | Calls `serializedObject.UpdateIfRequiredOrScript()` |
| `ApplyModifiedProperties()` | Calls `serializedObject.ApplyModifiedProperties()` |

## SetValue / SetXxx — typed setters

For each supported type four variants exist:

| Variant | Behavior |
|---------|----------|
| `SetValue(value)` | Generic dispatch — picks the right typed setter based on the value's runtime type, returns `property` |
| `SetValueAndApply(value)` | `SetValue(value)` followed by `ApplyModifiedProperties()` |
| `SetXxx(value)` | Typed setter (e.g. `SetInt`) that writes to the matching `SerializedProperty.xxxValue` field |
| `SetXxxAndApply(value)` | `SetXxx(value)` followed by `ApplyModifiedProperties()` |

### Supported types

| Method family | Unity type | Notes |
|---------------|------------|-------|
| `SetInt` | `int` | |
| `SetUint` | `uint` | |
| `SetLong` | `long` | |
| `SetUlong` | `ulong` | |
| `SetFloat` | `float` | |
| `SetDouble` | `double` | |
| `SetBool` | `bool` | |
| `SetString` | `string` | |
| `SetColor` | `Color` | |
| `SetGradient` | `Gradient` | |
| `SetHash128` | `Hash128` | |
| `SetRect` / `SetRectInt` | `Rect` / `RectInt` | |
| `SetBounds` / `SetBoundsInt` | `Bounds` / `BoundsInt` | |
| `SetVector2` / `SetVector2Int` | `Vector2` / `Vector2Int` | |
| `SetVector3` / `SetVector3Int` | `Vector3` / `Vector3Int` | |
| `SetVector4` | `Vector4` | |
| `SetQuaternion` | `Quaternion` | |
| `SetAnimationCurve` | `AnimationCurve` | |
| `SetEntityId` | `Unity.Entities.EntityId` | Unity 6.2+. The apply-variant is named `SetEntityIdApply` *(method name preserves the source typo: missing `And`)* |

### Enum setters

Enum values do not flow through `SetValue` — use the explicit pair below depending on whether the field is a `[Flags]` enum:

| Method | Description |
|--------|-------------|
| `SetEnumFlag(int)` / `SetEnumFlagAndApply(int)` | Writes to `enumValueFlag` |
| `SetEnumIndex(int)` / `SetEnumIndexAndApply(int)` | Writes to `enumValueIndex` |

### Example

```csharp
SerializedProperty property = GetProperty();

// Equivalent forms
property.SetValue(10).ApplyModifiedProperties();
property.SetValueAndApply(10);
property.SetInt(10).ApplyModifiedProperties();
property.SetIntAndApply(10);

// Chain multiple setters
property
    .SetVector3(Vector3.up)
    .SetBool(true)
    .ApplyModifiedProperties();
```

## Array operations

| Method | Description |
|--------|-------------|
| `SetArraySize(int)` / `SetArraySizeAndApply(int)` | Sets `property.arraySize` |
| `AddArraySize(int = 1)` / `AddArraySizeAndApply(int = 1)` | Increases `arraySize` by the given amount (default `1`) |
| `RemoveArraySize(int = 1)` / `RemoveArraySizeAndApply(int = 1)` | Decreases `arraySize` by the given amount (default `1`) |

## Reference setters

| Method | Description | Notes |
|--------|-------------|-------|
| `SetManagedReference(object)` / `SetManagedReferenceAndApply(object)` | Writes to `managedReferenceValue` (target must be a `[SerializeReference]` field) | |
| `SetObjectReference(Object)` / `SetObjectReferenceAndApply(Object)` | Writes to `objectReferenceValue` | |
| `SetExposedReference(Object)` / `SetExposedReferenceAndApply(Object)` | Writes to `exposedReferenceValue` | |
| `SetBoxed(object)` / `SetBoxedAndApply(object)` | Writes to `boxedValue` | Unity 6+ |

## Reflection helpers

For drawer / inspector code that needs to inspect the runtime type or instance behind a property:

| Method | Returns | Description |
|--------|---------|-------------|
| `GetPropertyType()` | `Type` or `null` | Returns the `FieldType` / `PropertyType` of the C# member that backs the property. `null` if the member can't be resolved. |
| `GetMemberInfo()` | `MemberInfo` or `null` | Locates the field/property on the owning class whose name matches `SerializedProperty.name`. Walks base classes via `TypeExtensions.GetMembersInfosIncludingBaseClasses`. |
| `GetClassInstance()` | `object` | Walks `propertyPath` from the root `targetObject` and returns the runtime instance that directly contains this property. Supports nested objects, arrays, and `List<T>` fields. |

```csharp
public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
{
    var declaringType = property.GetPropertyType();
    var owner = property.GetClassInstance();
    // …
}
```
