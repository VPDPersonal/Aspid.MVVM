# Расширения SerializedProperty — полный справочник

Цепочные методы расширения над `SerializedProperty` для синхронизации владеющего `SerializedObject`, установки значений и рефлексии над полем-источником.

```csharp
using Aspid.FastTools.Editors;
```

Все расширения обобщены по `T : SerializedProperty` и возвращают тот же экземпляр, поэтому вызовы можно свободно объединять в цепочки.

## Update / Apply

Тонкие обёртки над одноимёнными методами `SerializedObject` у `property.serializedObject`.

```csharp
property
    .Update()
    .SetInt(42)
    .ApplyModifiedProperties();
```

| Метод | Описание |
|-------|----------|
| `Update()` | Вызывает `serializedObject.Update()` |
| `UpdateIfRequiredOrScript()` | Вызывает `serializedObject.UpdateIfRequiredOrScript()` |
| `ApplyModifiedProperties()` | Вызывает `serializedObject.ApplyModifiedProperties()` |

## SetValue / SetXxx — типизированные сеттеры

Для каждого поддерживаемого типа существуют четыре варианта:

| Вариант | Поведение |
|---------|-----------|
| `SetValue(value)` | Обобщённый диспетчер — выбирает нужный типизированный сеттер по runtime-типу значения, возвращает `property` |
| `SetValueAndApply(value)` | `SetValue(value)` плюс `ApplyModifiedProperties()` |
| `SetXxx(value)` | Типизированный сеттер (например, `SetInt`), пишущий в соответствующее поле `SerializedProperty.xxxValue` |
| `SetXxxAndApply(value)` | `SetXxx(value)` плюс `ApplyModifiedProperties()` |

### Поддерживаемые типы

| Семейство методов | Unity-тип | Примечания |
|-------------------|-----------|------------|
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
| `SetEntityId` | `Unity.Entities.EntityId` | Unity 6.2+. Apply-вариант называется `SetEntityIdApply` *(имя метода сохраняет опечатку из исходника: пропущено `And`)* |

### Enum-сеттеры

Значения enum не идут через `SetValue` — используйте явную пару ниже в зависимости от того, является ли поле `[Flags]`-перечислением:

| Метод | Описание |
|-------|----------|
| `SetEnumFlag(int)` / `SetEnumFlagAndApply(int)` | Пишет в `enumValueFlag` |
| `SetEnumIndex(int)` / `SetEnumIndexAndApply(int)` | Пишет в `enumValueIndex` |

### Пример

```csharp
SerializedProperty property = GetProperty();

// Эквивалентные формы
property.SetValue(10).ApplyModifiedProperties();
property.SetValueAndApply(10);
property.SetInt(10).ApplyModifiedProperties();
property.SetIntAndApply(10);

// Цепочка из нескольких сеттеров
property
    .SetVector3(Vector3.up)
    .SetBool(true)
    .ApplyModifiedProperties();
```

## Операции с массивами

| Метод | Описание |
|-------|----------|
| `SetArraySize(int)` / `SetArraySizeAndApply(int)` | Устанавливает `property.arraySize` |
| `AddArraySize(int = 1)` / `AddArraySizeAndApply(int = 1)` | Увеличивает `arraySize` на указанное количество (по умолчанию `1`) |
| `RemoveArraySize(int = 1)` / `RemoveArraySizeAndApply(int = 1)` | Уменьшает `arraySize` на указанное количество (по умолчанию `1`) |

## Сеттеры ссылок

| Метод | Описание | Примечания |
|-------|----------|------------|
| `SetManagedReference(object)` / `SetManagedReferenceAndApply(object)` | Пишет в `managedReferenceValue` (поле должно быть помечено `[SerializeReference]`) | |
| `SetObjectReference(Object)` / `SetObjectReferenceAndApply(Object)` | Пишет в `objectReferenceValue` | |
| `SetExposedReference(Object)` / `SetExposedReferenceAndApply(Object)` | Пишет в `exposedReferenceValue` | |
| `SetBoxed(object)` / `SetBoxedAndApply(object)` | Пишет в `boxedValue` | Unity 6+ |

## Рефлексионные хелперы

Для drawer-/inspector-кода, которому нужно получить runtime-тип или экземпляр, стоящий за property:

| Метод | Возвращает | Описание |
|-------|------------|----------|
| `GetPropertyType()` | `Type` или `null` | Возвращает `FieldType` / `PropertyType` C#-члена, стоящего за property. `null`, если член не удаётся разрешить. |
| `GetMemberInfo()` | `MemberInfo` или `null` | Находит field/property на классе-владельце, имя которого совпадает с `SerializedProperty.name`. Обходит базовые классы через `TypeExtensions.GetMembersInfosIncludingBaseClasses`. |
| `GetClassInstance()` | `object` | Идёт по `propertyPath` от корневого `targetObject` и возвращает runtime-экземпляр, который непосредственно содержит это property. Поддерживает вложенные объекты, массивы и `List<T>`-поля. |

```csharp
public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
{
    var declaringType = property.GetPropertyType();
    var owner = property.GetClassInstance();
    // …
}
```
