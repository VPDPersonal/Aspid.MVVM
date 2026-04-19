# Aspid.FastTools

**Aspid.FastTools** — набор инструментов, предназначенных для минимизации рутинного написания кода в Unity.

## Исходный код

[[Aspid.FastTools](https://github.com/VPDPersonal/Aspid.FastTools)]


---

## Интеграция

Установите Aspid.FastTools одним из следующих способов:

- **Скачать .unitypackage** — Перейдите на [страницу релизов GitHub](https://github.com/VPDPersonal/Aspid.FastTools/releases) и скачайте последнюю версию `Aspid.FastTools.X.X.X.unitypackage`. Импортируйте его в проект.
- **Через UPM** (Unity Package Manager) подключите следующие пакеты:
  - `https://github.com/VPDPersonal/Aspid.Internal.Unity.git`
  - `https://github.com/VPDPersonal/Aspid.FastTools.git?path=Aspid.FastTools/Assets/Plugins/Aspid/FastTools`

---

## Пространства имён

| Пространство имён | Описание |
|-------------------|----------|
| `Aspid.FastTools` | Runtime API — типы, расширения VisualElement |
| `Aspid.FastTools.Editors` | Editor-only API — property drawers, IMGUI-области, расширения редактора |

---

## ProfilerMarker

Предоставляет регистрацию `ProfilerMarker` через source generation. Генератор создаёт статический маркер для каждого места вызова, идентифицируемый по вызывающему методу и номеру строки.

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
        // Некоторый код
    }

    private void DoSomething2()
    {
        using (this.Marker())
        {
            // Некоторый код
            using var _ = this.Marker().WithName("Calculate");
            // Некоторый код
        }
    }
}
```

### Сгенерированный код

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

### Результат

![Aspid.FastTools.ProfilerMarkers.png](Images/Aspid.FastTools.ProfilerMarkers.png)

---

## Система сериализуемых типов

Позволяет сериализовать ссылку на `System.Type` в Unity Inspector. Выбранный тип хранится как assembly-qualified name и разрешается лениво при первом обращении.

### SerializableType

Доступны два варианта:

- **`SerializableType`** — хранит любой тип (базовый тип — `object`)
- **`SerializableType<T>`** — хранит тип, ограниченный `T` или его подклассами

Оба поддерживают неявное преобразование в `System.Type`.

```csharp
using UnityEngine;
using Aspid.FastTools;

public class MyBehaviour : MonoBehaviour
{
    [SerializeField] private SerializableType _anyType;
    [SerializeField] private SerializableType<MonoBehaviour> _behaviourType;

    private void Start()
    {
        Type type1 = _anyType;             // неявный оператор
        Type type2 = _behaviourType.Type;  // явное свойство

        var instance = (MonoBehaviour)gameObject.AddComponent(type2);
    }
}
```
![Aspid.FastTools.SerializableType.png](Images/Aspid.FastTools.SerializableType.png)
### ComponentTypeSelector

Сериализуемая структура, добавляющая в Inspector выпадающий список для смены типа объекта. Добавьте её как поле в базовый класс — при выборе подтипа редактор перезаписывает `m_Script` на `SerializedObject`, фактически превращая компонент или ScriptableObject в выбранный подтип.

Список автоматически ограничивается подтипами класса, в котором объявлено поле. Дополнительная настройка не требуется.

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

Атрибут `PropertyAttribute`, доступный только в редакторе, ограничивающий всплывающее окно выбора типа конкретными базовыми типами. Применяется к полям `string`, хранящим assembly-qualified имена типов.

```csharp
[Conditional("UNITY_EDITOR")]
public sealed class TypeSelectorAttribute : PropertyAttribute
{
    public TypeSelectorAttribute() // базовый тип: object
    public TypeSelectorAttribute(Type type)
    public TypeSelectorAttribute(params Type[] types)
    public TypeSelectorAttribute(string assemblyQualifiedName)
    public TypeSelectorAttribute(params string[] assemblyQualifiedNames)

    public bool AllowAbstractTypes { get; set; } // по умолчанию: false
    public bool AllowInterfaces    { get; set; } // по умолчанию: false
}
```

| Свойство | Описание |
|----------|----------|
| `AllowAbstractTypes` | Включает абстрактные классы в список выбора. По умолчанию: `false` |
| `AllowInterfaces` | Включает интерфейсы в список выбора. По умолчанию: `false` |

```csharp
using UnityEngine;
using Aspid.FastTools;

public class MyBehaviour : MonoBehaviour
{
    [TypeSelector(typeof(IMyInterface))]
    [SerializeField] private string _typeName;

    // Включить абстрактные типы и интерфейсы в список выбора
    [TypeSelector(typeof(object), AllowAbstractTypes = true, AllowInterfaces = true)]
    [SerializeField] private string _anyType;
}
```

### Окно выбора типа

В Inspector отображается кнопка, открывающая всплывающее окно с поиском, которое включает:

- Иерархическую организацию по пространствам имён
- Текстовый поиск с фильтрацией
- Навигацию с клавиатуры (стрелки, Enter, Escape)
- Историю навигации (кнопка «назад»)
- Разрешение неоднозначности для типов с одинаковыми именами из разных сборок

![Aspid.FastTools.TypeSelectorWindow.png](Images/Aspid.FastTools.TypeSelectorWindow.png)
---

## Система перечислений

Предоставляет сериализуемые отображения enum → значение, настраиваемые через Inspector.

### EnumValues\<TValue\>

Сериализуемая коллекция записей `EnumValue<TValue>` с настраиваемым значением по умолчанию. Реализует `IEnumerable<KeyValuePair<Enum, TValue>>`.

```csharp
[Serializable]
public sealed class EnumValues<TValue> : IEnumerable<KeyValuePair<Enum, TValue>>
```

| Член | Описание |
|------|----------|
| `TValue GetValue(Enum enumValue)` | Возвращает сопоставленное значение или `_defaultValue`, если не найдено |
| `bool Equals(Enum, Enum)` | Проверка равенства с поддержкой `[Flags]` |

Поддерживает `[Flags]`-перечисления: `Equals` использует `HasFlag` и корректно обрабатывает члены со значением `0`.

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

В Inspector выберите тип перечисления в заголовке `EnumValues`, затем назначьте значение для каждого члена перечисления. Нажмите правой кнопкой мыши по свойству, чтобы открыть контекстное меню с пунктом **Populate Missing Enum Members** — он добавит записи для всех отсутствующих членов перечисления, используя текущее Default Value как начальное значение.

---

## Система строковых ID

Отображает строковые имена в стабильные целочисленные ID. Предназначена для data-driven проектов, где ассетам нужен надёжный, назначаемый из редактора идентификатор, безопасный для использования в `switch` и `Dictionary`.

### Использование

**1.** Объявите `partial struct`, реализующий `IId`. Генератор исходников автоматически добавит необходимые поля и свойство:

```csharp
using Aspid.FastTools;

public partial struct EnemyId : IId { }
```

Сгенерированный код:

```csharp
public partial struct EnemyId
{
    [SerializeField] private string __stringId;
    [SerializeField] private int _id;

    public int Id => _id;
}
```

**2.** Создайте ассет `IdRegistry` через меню *Assets → Create → Aspid → FastTools → String Id Registry* и привяжите его к вашему типу структуры в Inspector.

**3.** Используйте структуру как сериализуемое поле. В Inspector отображается выпадающий список зарегистрированных имён с кнопкой **Create** для добавления новых записей прямо там:

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
        int id = _targetEnemy.Id; // стабильный integer, безопасен для switch / Dictionary
    }
}
```

### UniqueIdAttribute

Помечает поле как требующее уникального значения среди всех ассетов объявляющего типа. Inspector показывает предупреждение, если два ассета используют одинаковый ID.

```csharp
[Conditional("UNITY_EDITOR")]
public sealed class UniqueIdAttribute : PropertyAttribute { }
```

### IdRegistry

`ScriptableObject`, хранящий пары имя ↔ целое число. Каждому имени назначается стабильный, автоинкрементный ID, который не изменяется даже при добавлении или удалении других записей.

| Член | Описание |
|------|----------|
| `bool Contains(string name)` | Возвращает, зарегистрировано ли имя |
| `int Add(string name)` | Регистрирует имя и возвращает ID; если имя уже есть — возвращает существующий ID |
| `void Remove(string name)` | Удаляет запись по имени |
| `void Rename(string oldName, string newName)` | Переименовывает запись |
| `int GetId(string name)` | Возвращает ID для имени или `0`, если не найдено |
| `string? GetName(int id)` | Возвращает имя для ID или `null`, если не найдено |

---

## Расширения SerializedProperty

Цепочечные методы расширения для `SerializedProperty`, позволяющие задавать значения и применять изменения.

```csharp
using Aspid.FastTools.Editors;
```

### Update и Apply

```csharp
property.Update();
property.UpdateIfRequiredOrScript();
property.ApplyModifiedProperties();
```

Все методы возвращают `SerializedProperty`, что позволяет строить цепочки вызовов.

### SetValue / SetXxx

Для каждого поддерживаемого типа доступны два метода:

- `SetXxx(value)` — устанавливает значение, возвращает `property` для цепочки
- `SetXxxAndApply(value)` — устанавливает значение и сразу вызывает `ApplyModifiedProperties()`

`SetValue(value)` автоматически направляет вызов к соответствующему типизированному сеттеру.

| Семейство методов | Тип Unity |
|-------------------|-----------|
| `SetInt` / `SetUint` / `SetLong` / `SetUlong` | Целочисленные типы |
| `SetFloat` / `SetDouble` | Вещественные типы |
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
| `SetEnumFlag` / `SetEnumIndex` | Флаг/индекс перечисления |
| `SetArraySize` | Размер массива |
| `SetManagedReference` | Управляемая ссылка |
| `SetObjectReference` | `UnityEngine.Object` |
| `SetExposedReference` | Exposed reference |
| `SetBoxed` | Упакованное значение *(Unity 6+)* |
| `SetEntityId` | Entity ID *(Unity 6.2+)* |

```csharp
SerializedProperty property = GetProperty();

// Простое применение
property.ApplyModifiedProperties();

// Установка и применение — эквивалентные формы
property.SetValue(10).ApplyModifiedProperties();
property.SetValueAndApply(10);
property.SetInt(10).ApplyModifiedProperties();
property.SetIntAndApply(10);

// Цепочка нескольких сеттеров
property.SetVector3(Vector3.up).SetBool(true).ApplyModifiedProperties();
```

---

## IMGUI-области разметки

```csharp
using Aspid.FastTools.Editors;
```

Доступны три типа областей: `VerticalScope`, `HorizontalScope`, `ScrollViewScope`. Каждая предоставляет свойство `Rect` и вызывает соответствующий метод `EditorGUILayout.End*` в `Dispose`.

### Использование через AspidEditorGUILayout

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

### Использование через структуры областей напрямую

```csharp
using (VerticalScope.Begin()) { /* ... */ }
using (HorizontalScope.Begin()) { /* ... */ }
using (ScrollViewScope.Begin(ref scrollPos)) { /* ... */ }
```

Все перегрузки `Begin` соответствуют сигнатурам `EditorGUILayout.Begin*` (с опциональными `GUIStyle`, `GUILayoutOption[]`, параметрами scroll view и т.д.).

---

## Расширения VisualElement

Fluent-методы расширения для построения UIToolkit-деревьев в коде. Все методы возвращают `T` (сам элемент) для цепочки вызовов.

```csharp
using Aspid.FastTools;         // runtime-расширения
using Aspid.FastTools.Editors; // editor-only расширения
```

### Основные операции с элементами

```csharp
element
    .SetName("MyElement")
    .SetVisible(true)
    .SetTooltip("Текст подсказки")
    .AddChild(new Label("Hello"))
    .AddChildren(child1, child2, child3);
```

| Метод | Описание |
|-------|----------|
| `SetName(string)` | Устанавливает `element.name` |
| `SetVisible(bool)` | Устанавливает `element.visible` |
| `SetTooltip(string)` | Устанавливает `element.tooltip` |
| `SetUserData(object)` | Устанавливает `element.userData` |
| `SetEnabledSelf(bool)` | Устанавливает `element.enabledSelf` |
| `SetPickingMode(PickingMode)` | Устанавливает `element.pickingMode` |
| `SetUsageHints(UsageHints)` | Устанавливает `element.usageHints` |
| `SetViewDataKey(string)` | Устанавливает `element.viewDataKey` |
| `SetLanguageDirection(LanguageDirection)` | Устанавливает `element.languageDirection` |
| `SetDisablePlayModeTint(bool)` | Устанавливает `element.disablePlayModeTint` |
| `SetDataSource(object)` | Устанавливает `element.dataSource` |
| `SetDataSourceType(Type)` | Устанавливает `element.dataSourceType` |
| `SetDataSourcePath(PropertyPath)` | Устанавливает `element.dataSourcePath` |
| `AddChild(VisualElement)` | Добавляет дочерний элемент, возвращает родителя |
| `AddChildren(params VisualElement[])` | Добавляет несколько дочерних элементов |
| `AddChildren(IEnumerable<VisualElement>)` | Добавляет из последовательности |
| `AddChildren(List<VisualElement>)` | Добавляет из списка |
| `AddChildren(Span<VisualElement>)` | Добавляет из span |
| `AddChildren(ReadOnlySpan<VisualElement>)` | Добавляет из read-only span |

> `RegisterCallbackOnce<TEventType>` и `RegisterCallbackOnce<TEventType, TUserArgsType>` доступны на всех версиях Unity (пакет содержит polyfill для версий до 2023.1).

### Focusable

| Метод | Описание |
|-------|----------|
| `SetFocus()` | Устанавливает фокус на элемент |
| `SetBlur()` | Снимает фокус с элемента |
| `IsFocus()` | Возвращает, находится ли элемент в фокусе |
| `SetTabIndex(int)` | Устанавливает `element.tabIndex` |
| `SetFocusable(bool)` | Устанавливает `element.focusable` |
| `SetDelegatesFocus(bool)` | Устанавливает `element.delegatesFocus` |

### USS и операции с классами

| Метод | Описание |
|-------|----------|
| `AddClass(string)` | Добавляет USS-класс |
| `RemoveClass(string)` | Удаляет USS-класс |
| `ClearClasses()` | Удаляет все USS-классы |
| `ToggleInClass(string)` | Переключает USS-класс вкл/выкл |
| `EnableInClass(string, bool)` | Добавляет или удаляет USS-класс по условию |
| `AddStyleSheets(StyleSheet)` | Добавляет `StyleSheet` |
| `RemoveStyleSheets(StyleSheet)` | Удаляет `StyleSheet` |
| `AddStyleSheetsFromResource(string)` | Добавляет таблицу стилей через `Resources.Load` |
| `RemoveStyleSheetsFromResource(string)` | Удаляет таблицу стилей, загруженную через `Resources.Load` |

### Расширения стилей — по категориям

Все методы стилей также доступны напрямую на `IStyle` (те же имена методов, работают с объектом стиля).

#### Разметка

| Метод | Свойство стиля |
|-------|----------------|
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

#### Размер

| Метод | Описание |
|-------|----------|
| `SetSize(StyleLength)` | Устанавливает ширину и высоту одновременно |
| `SetSize(width?, height?)` | Устанавливает ширину и/или высоту независимо |
| `SetMinSize(StyleLength)` | Устанавливает minWidth и minHeight одновременно |
| `SetMinSize(width?, height?)` | |
| `SetMaxSize(StyleLength)` | Устанавливает maxWidth и maxHeight одновременно |
| `SetMaxSize(width?, height?)` | |

#### Отступы

Все методы отступов имеют перегрузку с единым значением и перегрузку по сторонам (`top`, `bottom`, `left`, `right`).

| Метод | Свойства стиля |
|-------|----------------|
| `SetMargin(…)` | `Top/Bottom/Left/Right` |
| `SetPadding(…)` | `Top/Bottom/Left/Right` |
| `SetDistance(…)` | `Top/Bottom/Left/Right` (смещение для абсолютного позиционирования) |

#### Шрифт

| Метод | Свойство стиля |
|-------|----------------|
| `SetUnityFont(StyleFont)` | `unityFont` |
| `SetFontSize(StyleLength)` | `fontSize` |
| `SetUnityFontDefinition(StyleFontDefinition)` | `unityFontDefinition` |
| `SetUnityFontStyleAndWeight(StyleEnum<FontStyle>)` | `unityFontStyleAndWeight` |

#### Пресеты стиля шрифта

Удобные методы для переключения bold / italic без перезаписи другого флага:

| Метод | Описание |
|-------|----------|
| `SetNormalUnityFontStyleAndWeight()` | Сбрасывает в `FontStyle.Normal` |
| `AddBoldUnityFontStyleAndWeight()` | Добавляет bold, сохраняя italic |
| `RemoveBoldUnityFontStyleAndWeight()` | Убирает bold, сохраняя italic |
| `AddItalicUnityFontStyleAndWeight()` | Добавляет italic, сохраняя bold |
| `RemoveItalicUnityFontStyleAndWeight()` | Убирает italic, сохраняя bold |

#### Текст

| Метод | Свойство стиля | Примечания |
|-------|---------------|------------|
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

#### Цвет и прозрачность

| Метод | Свойство стиля |
|-------|----------------|
| `SetColor(StyleColor)` | `color` |
| `SetOpacity(StyleFloat)` | `opacity` |

#### Рамка

| Метод | Описание |
|-------|----------|
| `SetBorderColor(StyleColor)` | Все стороны |
| `SetBorderColor(top?, bottom?, left?, right?)` | По стороне |
| `SetBorderRadius(StyleLength)` | Все углы |
| `SetBorderRadius(topLeft?, topRight?, bottomLeft?, bottomRight?)` | По углу |
| `SetBorderWidth(StyleFloat)` | Все стороны |
| `SetBorderWidth(top?, bottom?, left?, right?)` | По стороне |

#### Фон

| Метод | Свойство стиля |
|-------|----------------|
| `SetBackgroundColor(StyleColor)` | `backgroundColor` |
| `SetBackgroundImage(StyleBackground)` | `backgroundImage` |
| `SetBackgroundSize(StyleBackgroundSize)` | `backgroundSize` |
| `SetBackgroundRepeat(StyleBackgroundRepeat)` | `backgroundRepeat` |
| `SetBackgroundPosition(StyleBackgroundPosition)` | X и Y одновременно |
| `SetBackgroundPosition(x?, y?)` | Независимо |
| `SetUnityBackgroundImageTintColor(StyleColor)` | `unityBackgroundImageTintColor` |

#### Трансформации

| Метод | Свойство стиля |
|-------|----------------|
| `SetScale(StyleScale)` | `scale` |
| `SetRotate(StyleRotate)` | `rotate` |
| `SetTranslate(StyleTranslate)` | `translate` |
| `SetTransformOrigin(StyleTransformOrigin)` | `transformOrigin` |

#### Анимации переходов

| Метод | Свойство стиля |
|-------|----------------|
| `SetTransitionDelay(StyleList<TimeValue>)` | `transitionDelay` |
| `SetTransitionDuration(StyleList<TimeValue>)` | `transitionDuration` |
| `SetTransitionProperty(StyleList<StylePropertyName>)` | `transitionProperty` |
| `SetTransitionTimingFunction(StyleList<EasingFunction>)` | `transitionTimingFunction` |

#### Переполнение и видимость

| Метод | Свойство стиля |
|-------|----------------|
| `SetOverflow(StyleEnum<Overflow>)` | `overflow` |
| `SetUnityOverflowClipBox(StyleEnum<OverflowClipBox>)` | `unityOverflowClipBox` |
| `SetVisibility(StyleEnum<Visibility>)` | `visibility` |
| `SetDisplay(DisplayStyle)` | `display` |

#### Unity Slice

| Метод | Описание |
|-------|----------|
| `SetUnitySlice(StyleInt)` | Все стороны |
| `SetUnitySlice(top?, bottom?, left?, right?)` | По стороне |
| `SetUnitySliceType(StyleEnum<SliceType>)` | Unity 6+ |

#### Курсор

| Метод | Свойство стиля |
|-------|----------------|
| `SetCursor(StyleCursor)` | `cursor` |

### Расширения для специализированных элементов

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
field.SetValue(42, notify: false); // устанавливает значение без генерации ChangeEvent
field.AddValueChanged(evt => Debug.Log(evt.newValue));
field.RemoveValueChanged(myCallback);
```

Типизированные перегрузки доступны для `int`, `uint`, `long`, `ulong`, `short`, `ushort`, `byte`, `sbyte`, `float`, `double`, `string`, `bool`, `Color`, `Vector2/3/4`, `Vector2Int/3Int`, `Rect/RectInt`, `Bounds/BoundsInt`, `Hash128`, `Enum`, `Object` и других типов.

#### IMixedValueSupport

```csharp
field.SetShowMixedValue(true); // показывает индикатор смешанного значения
```

#### Button

```csharp
button
    .AddClicked(() => Debug.Log("Clicked"))
    .SetClickable(new Clickable(() => { }))
    .SetIconImage(myBackground);
```

| Метод | Описание |
|-------|----------|
| `AddClicked(Action)` | Подписка на `Button.clicked` |
| `RemoveClicked(Action)` | Отписка от `Button.clicked` |
| `SetClickable(Clickable)` | Устанавливает `Button.clickable` |
| `SetIconImage(Background)` | Устанавливает `Button.iconImage` |

#### Slider / BaseSlider\<TValue\>

```csharp
slider
    .SetLowValue(0f)
    .SetHighValue(100f)
    .SetShowInputField<SliderFloat, float>(true);
```

| Метод | Описание |
|-------|----------|
| `SetLowValue(TValue)` | Устанавливает минимальное значение слайдера |
| `SetHighValue(TValue)` | Устанавливает максимальное значение слайдера |
| `SetFill(bool)` | Заполнение трека до текущего значения |
| `SetInverted(bool)` | Инвертирует направление слайдера |
| `SetPageSize(float)` | Шаг изменения значения при постраничной навигации |
| `SetShowInputField(bool)` | Показывает числовое поле ввода рядом со слайдером |
| `SetDirection(SliderDirection)` | Устанавливает ориентацию слайдера |

#### ProgressBar

```csharp
progressBar.SetTitle("Загрузка...").SetLowValue(0f).SetHighValue(100f);
```

| Метод | Описание |
|-------|----------|
| `SetTitle(string)` | Устанавливает заголовок, отображаемый в центре |
| `SetLowValue(float)` | Устанавливает минимальное значение |
| `SetHighValue(float)` | Устанавливает максимальное значение |

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
image.SetImageFromResource("Editor/MyIcon"); // загрузка через Resources.Load
```

#### IMGUIContainer

```csharp
container
    .SetOnGUIHandler(() => GUILayout.Label("IMGUI"))
    .SetCullingEnabled(true);
```

| Метод | Описание |
|-------|----------|
| `SetOnGUIHandler(Action)` | Заменяет коллбэк `onGUIHandler` |
| `AddOnGUIHandler(Action)` | Подписка на `onGUIHandler` |
| `RemoveOnGUIHandler(Action)` | Отписка от `onGUIHandler` |
| `SetCullingEnabled(bool)` | Пропускает `onGUIHandler`, когда элемент за пределами экрана |
| `SetContextType(ContextType)` | Устанавливает тип контекста IMGUI |

#### ListView / CollectionView

| Метод | Описание | Примечания |
|-------|----------|------------|
| `SetBindItem(Action<VisualElement, int>)` | Коллбэк привязки элемента | |
| `SetMakeItem(Func<VisualElement>)` | Фабрика элементов | |
| `SetMakeFooter(Func<VisualElement>)` | Фабрика подвала | Unity 6+ |
| `SetMakeHeader(Func<VisualElement>)` | Фабрика заголовка | Unity 6+ |
| `SetMakeNoneElement(Func<VisualElement>)` | Фабрика элемента пустого состояния | Unity 6+ |

### Команды редактора (только для редактора)

```csharp
using Aspid.FastTools.Editors;

image.AddOpenScriptCommand(target);
// Двойной клик на элемент открывает скрипт 'target' в IDE
```

### Полный пример

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

### Результат

![Aspid.FastTools.VisualElement.png](Images/Aspid.FastTools.VisualElement.png)

---

## Вспомогательные расширения для редактора

Утилитарные методы для получения отображаемых имён объектов Unity в пользовательских редакторах.

```csharp
using Aspid.FastTools.Editors;
```

```csharp
public static string GetScriptName(this Object obj)
```

Возвращает отображаемое имя объекта Unity:
- Если тип имеет `[AddComponentMenu]`, возвращает `ObjectNames.GetInspectorTitle(obj)`
- В противном случае возвращает `ObjectNames.NicifyVariableName(typeName)`

```csharp
public static string GetScriptNameWithIndex(this Component targetComponent)
```

Возвращает отображаемое имя с числовым суффиксом, если на одном GameObject присутствует несколько компонентов одного типа. Например, если прикреплены два компонента `AudioSource`, второй вернёт `"Audio Source (2)"`.

```csharp
[CustomEditor(typeof(MyBehaviour))]
public class MyBehaviourEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        // "My Behaviour" — или "Custom Name", если присутствует [AddComponentMenu("Custom Name")]
        var name = target.GetScriptName();

        // "My Behaviour (2)" при наличии второго компонента того же типа
        var nameWithIndex = ((Component)target).GetScriptNameWithIndex();

        return new Label(name);
    }
}
```
