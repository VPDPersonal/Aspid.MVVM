# Конвертеры

Конвертер преобразует значение по дороге от ViewModel к View, не трогая сам ViewModel. Он позволяет
держать во ViewModel то, что описывает предметную область (`float` здоровья от 0 до 1), а во View —
то, что нужно виджету (`"75%"`, красный цвет, ширина полоски).

## Содержание

- [Обзор](#обзор)
- [Контракт](#контракт)
- [Обратное преобразование](#обратное-преобразование)
- [Каталог](#каталог)
- [Композиция](#композиция)
- [Конвертер как ассет](#конвертер-как-ассет)
- [Отказ данных](#отказ-данных)
- [Свой конвертер](#свой-конвертер)
- [Использование в Inspector](#использование-в-inspector)
- [Специализированные интерфейсы](#специализированные-интерфейсы)

---

## Обзор

Типичные сценарии:

| Из | В | Конвертер |
|----|---|-----------|
| `float` 0..1 | `"75%"` | `PercentStringConverter` |
| `int` 1500 | `"Score: 1500"` | `StringFormatConverter` |
| `float` здоровья | `Color` красный → зелёный | `ThresholdColorConverter` |
| `float` 0..1 | `Vector3` шкалы | `FloatToVector3Converter` |
| `TimeSpan` | `"01:23"` | `TimeSpanFormatConverter` |
| `int` секунд | `"2 hours ago"` | `RelativeTimeConverter` |

Конвертер назначается на биндер — в Inspector через `[SerializeReference]` или через конструктор из
кода.

---

## Контракт

```csharp
public interface IConverter { }                        // маркер, без членов

public interface IConverter<in TFrom, out TTo> : IConverter
{
    TTo Convert(TFrom value);
}
```

Негенерический `IConverter` не объявляет ничего — он нужен, чтобы валидация, пикер и тесты могли
опознать конвертер, не перебирая все закрытия генерика. Реализовывать напрямую его не нужно: он
наследуется автоматически.

> Маркер останется пустым: один тип реализует `IConverter<,>` сколько нужно раз, поэтому член,
> называющий преобразуемые типы, отвечал бы сразу за все реализации.

Конвертеры, которые умеют преобразовывать в обе стороны, реализуют `ITwoWayConverter<TFrom, TTo>` с методом `ConvertBack`: `BoolInvertConverter`, `EnumToIntConverter<TEnum>`, `PassthroughConverter<T>`, `SequenceConverters<T>`.

---

## Обратное преобразование

`IConverter` односторонний. Конвертер, который умеет отменять себя, реализует ещё и `ITwoWayConverter`:

```csharp
public interface ITwoWayConverter<TFrom, TTo> : IConverter<TFrom, TTo>
{
    TFrom ConvertBack(TTo value);
}
```

Биндер в `BindMode.TwoWay` или `BindMode.OneWayToSource` вызывает `ConvertBack`, когда конвертер её
предлагает, и передаёт значение без изменений, когда нет. Прямой конвертер на обратном пути не
применяется никогда: он описывает представление во View, и прогонять его в сторону ViewModel значит
записывать представление обратно в модель.

> Предупреждение в консоль о назначенном одностороннем конвертере пишут только биндеры, наследующие
> `TargetBinder` / `ComponentMonoBinder`. Биндеры со своим полем конвертера — `InputField`, `Slider`,
> `RendererMaterials`, `TwoWayValue` — `ConvertBack` применяют, но молчат: там значение просто
> уезжает непреобразованным.

Ожидание к реализации: `ConvertBack(Convert(x)) == x`. Конвертер, который этого не гарантирует, не
должен реализовывать интерфейс — иначе значение будет дрейфовать на каждом круге.

Двусторонние из коробки:

`ArithmeticNumberConverter`, `AngleToQuaternionConverter`, `AudioDecibelToLinearConverter`,
`AudioLinearToDecibelConverter`, `BoolInvertConverter`, `DegreesToRadiansConverter`,
`EnumToIntConverter`, `EulerToQuaternionConverter`, `InverseLerpConverter`, `LerpNumberConverter`,
`NormalizedToPercentConverter`, `PassthroughConverter`, `QuaternionOffsetConverter`,
`RemapNumberConverter`, `SecondsToTimeSpanConverter`, `SequenceConverters`, `StringToEnumConverter`,
`StringToFloatConverter`, `StringToIntConverter`, `StringToLongConverter`,
`UnixTimestampToDateTimeConverter`, `Vector2ToVector2IntConverter`, `Vector3ToVector3IntConverter`.

---

## Каталог

В пакете 210 конвертеров. В Inspector они разложены по группам — группа видна в выпадающем списке
поля `Converter`.

### Aspid/Bool (8)

`BoolInvertConverter`, `BoolLogicConverter`, `BoolToValueConverter`, `NumberToBoolConverter`,
`ObjectNullToBoolConverter`, `StringEmptyToBoolConverter`, `StringMatchToBoolConverter`,
`UnityObjectNullToBoolConverter`

> `StringEmptyToBoolConverter` полем `StringEmptiness` выбирает, что считать отсутствующей строкой:
> `NullOrEmpty` (по умолчанию), `Null` — пустая строка считается заполненной, `NullOrWhiteSpace` —
> строка из пробелов считается пустой. Последнее и означает «пользователь что-нибудь ввёл?».

### Aspid/Number (21)

`AnimationCurveConverter`, `ArithmeticNumberConverter`, `AudioDecibelToLinearConverter`,
`AudioLinearToDecibelConverter`, `ClampNumberConverter`, `CountdownProgressConverter`,
`EasingConverter`, `InverseLerpConverter`, `LerpNumberConverter`, `ModuloNumberConverter`,
`NormalizedToPercentConverter`, `NumericCastConverter`, `PercentToNormalizedConverter`,
`PowerNumberConverter`, `RemapNumberConverter`, `RoundNumberConverter`, `SmoothStepConverter`,
`SnapToStepConverter`, `SumConstantThenScaleConverter`, `UnaryMathConverter`,
`WrapNumberConverter`

> `NumericCastConverter` — единственный способ сузить число управляемо. Без него `long.MaxValue`,
> попавший в int-биндер, молча уходит в отрицательное; `OverflowMode.Saturate` прижимает к границе,
> `Checked` бросает.

### Aspid/String (46)

`AbbreviatedNumberConverter`, `ByteSizeConverter`, `ConcatStringConverter`, `CurrencyConverter`,
`DecimalFormatConverter`, `DefaultStringConverter`, `GenericToString`, `MaskStringConverter`,
`NumberFormatConverter`, `ObjectToStringConverter`, `OrdinalConverter`, `PadStringConverter`,
`PaddedNumberConverter`, `PercentStringConverter`, `PluralizeConverter`,
`RatioToStringConverter`, `RepeatStringConverter`, `ReplaceStringConverter`,
`ReverseStringConverter`, `RichTextColorConverter`, `RichTextNoParseConverter`,
`RichTextSizeConverter`, `RichTextStyleConverter`, `RomanNumeralConverter`,
`SanitizeRichTextConverter`, `SignedNumberStringConverter`, `SplitJoinStringConverter`,
`StringFormatConverter`, `StringToBoolParseConverter`, `StringToDateTimeConverter`,
`StringToDecimalConverter`, `StringToDoubleConverter`, `StringToEnumConverter`,
`StringToFloatConverter`, `StringToIntConverter`, `StringToLongConverter`,
`StringToTimeSpanConverter`, `StringToVector2Converter`, `StringToVector3Converter`,
`SubstringConverter`, `TextCaseConverter`, `ThousandsSeparatorConverter`,
`ThresholdRichTextColorConverter`, `TimeSpanToStringConverter`, `TrimStringConverter`,
`TruncateStringConverter`

> Для любого текста, который ввёл игрок, нужен `SanitizeRichTextConverter` или
> `RichTextNoParseConverter`. TMP исполняет разметку в любой строке, которую получает: ник
> `<size=400%>` растянет каждый ярлык, где он покажется, на экране каждого другого игрока.
> `RichTextNoParse` заворачивает всё в `<noparse>`; `SanitizeRichText` вырезает или экранирует теги
> выборочно, оставляя белый список.

### Aspid/Time (12)

`DateTimeFormatConverter`, `DateTimeOffsetFormatConverter`, `DateTimeToBoolConverter`,
`DateTimeToUnixTimestampConverter`, `RelativeTimeConverter`, `SecondsToTimeSpanConverter`,
`SecondsToTimeStringConverter`, `TimeSpanArithmeticConverter`, `TimeSpanFormatConverter`,
`TimeSpanToNumberConverter`, `TimeUntilConverter`, `UnixTimestampToDateTimeConverter`

### Aspid/Colour (21)

`Color32ToColorConverter`, `ColorAlphaConverter`, `ColorBlockAlphaConverter`,
`ColorBlockFadeDurationConverter`, `ColorBlockStateConverter`, `ColorBlockTintConverter`,
`ColorChannelConverter`, `ColorGrayscaleConverter`, `ColorHsvConverter`, `ColorLerpConverter`,
`ColorTintConverter`, `ColorToColor32Converter`, `ColorToColorBlockConverter`,
`ColorToHtmlStringConverter`, `ColorToVector4Converter`, `GradientEvaluateConverter`,
`HashToColorConverter`, `HdrIntensityConverter`, `ParseHtmlStringConverter`,
`ThresholdColorConverter`, `Vector4ToColorConverter`

### Aspid/Vector (43)

`BoundsCenterConverter`, `BoundsSizeConverter`, `BoundsToRectConverter`,
`BoxCollider2DOffsetCombineConverter`, `BoxCollider2DSizeCombineConverter`,
`BoxColliderCentreCombineConverter`, `BoxColliderSizeCombineConverter`,
`CapsuleColliderCentreCombineConverter`, `FloatToVector2Converter`, `FloatToVector3Converter`,
`RectToVector4Converter`, `RectTransformAnchoredPosition2DCombineConverter`,
`RectTransformAnchoredPositionCombineConverter`, `RectTransformSizeDeltaCombineConverter`,
`SphereColliderCentreCombineConverter`, `TransformEulerAnglesCombineConverter`,
`TransformPosition2DCombineConverter`, `TransformPositionCombineConverter`,
`TransformScaleCombineConverter`, `Vector2ArithmeticConverter`,
`Vector2ClampComponentsConverter`, `Vector2ClampMagnitudeConverter`,
`Vector2NormalizeConverter`, `Vector2RoundConverter`, `Vector2SubstitutionConverter`,
`Vector2ToFloatConverter`, `Vector2ToVector2IntConverter`, `Vector2ToVector3Converter`,
`Vector3ArithmeticConverter`, `Vector3SubstitutionConverter`, `Vector3ToFloatConverter`,
`Vector3ToVector2Converter`, `Vector3ToVector3IntConverter`, `Vector3ToVector4Converter`,
`Vector4SwizzleConverter`, `Vector4ToRectConverter`, `Vector4ToVector3Converter`,
`VectorClampComponentsConverter`, `VectorClampMagnitudeConverter`, `VectorDistanceConverter`,
`VectorLerpConverter`, `VectorNormalizeConverter`, `VectorRoundConverter`

> Конвертеры `*CombineConverter` берут часть компонент у привязанного вектора, часть — у компонента
> сцены (`Transform`, `RectTransform`, коллайдер). Пары `*2D*` — для двумерных коллайдеров и
> `Vector2`-свойств.

### Aspid/Rotation (15)

`AngleDifferenceConverter`, `AngleToDirectionConverter`, `AngleToQuaternionConverter`,
`AngleWrapConverter`, `DegreesToRadiansConverter`, `DirectionToAngleConverter`,
`EulerToQuaternionConverter`, `LookRotationConverter`, `QuaternionOffsetConverter`,
`QuaternionSlerpConverter`, `QuaternionToAngleConverter`, `QuaternionToEulerConverter`,
`QuaternionToVector4Converter`, `RadiansToDegreesConverter`, `Vector4ToQuaternionConverter`

### Aspid/Collection (11)

`CollectionAggregateConverter`, `CollectionContainsToBoolConverter`, `CollectionCountConverter`,
`CollectionCountToStringConverter`, `CollectionElementAtConverter`,
`CollectionEmptyToBoolConverter`, `CollectionFirstConverter`, `CollectionLastConverter`,
`CollectionTakeConverter`, `DictionaryLookupConverter`, `ListToStringConverter`

### Остальные группы

| Группа | Конвертеры |
|--------|-----------|
| `Aspid/Enum` (8) | `EnumFlagsToStringConverter`, `EnumMaskConverter`, `EnumToBoolConverter`, `EnumToDropdownOptionDataConverter`, `EnumToIntConverter`, `EnumToStringConverter`, `EnumToValueConverter`, `IntToEnumConverter` |
| `Aspid/Object` (3) | `EqualityToBoolConverter`, `IndexToValueConverter`, `NullCoalesceConverter` |
| `Aspid/Texture` (6) | `NormalizedToSpriteConverter`, `ObjectNameConverter`, `SpriteToTextureConverter`, `StringToSpriteConverter`, `Texture2DToSpriteConverter`, `TextureToSpriteRectConverter` |
| `Aspid/Layout` (3) | `IntToRectOffsetConverter`, `RectOffsetScaleConverter`, `Vector4ToRectOffsetConverter` |
| `Aspid/Localization` (4) | `LocaleToStringConverter`, `LocalizedEnumConverter`, `LocalizedNumberConverter`, `LocalizedStringConverter` |
| `Aspid/Asset` (2) | `ConverterAssetReference`, `MaterialInstanceConverter` |

---

## Композиция

Группа `Aspid/Composition` — не преобразования, а обёртки над другими конвертерами.

| Конвертер | Назначение |
|-----------|-----------|
| `ComposeConverter<TFrom, TMid, TTo>` | Два конвертера подряд, с разными типами на стыке |
| `SequenceConverters<T>` | Цепочка любой длины, все звенья `T → T` |
| `CachedConverter<TFrom, TTo>` | Повторяет прошлый результат, пока вход не изменился |
| `SafeConverter<TFrom, TTo>` | Ловит исключение внутреннего конвертера и отдаёт запасное значение |
| `NullGuardConverter<TFrom, TTo>` | Не вызывает внутренний конвертер на `null` |
| `ConditionalConverter<T>` | Выбирает один из двух конвертеров по предикату |
| `PassthroughConverter<T>` | Ничего не делает; заглушка и элемент по умолчанию |

```csharp
// float 0..1 → "75%" с кэшем, чтобы не собирать строку заново на каждом push
var converter = new CachedConverter<float, string>(
    new PercentStringConverter());
```

`CachedConverter` стоит держать в голове: биндер шлёт значение на каждое **уведомление**, а не на
каждое **изменение**, поэтому конвертер, который что-то аллоцирует, вызывается заметно чаще, чем
кажется.

`SafeConverter` полезен потому, что рассылка биндеров — голый multicast: исключение из одного
конвертера обрывает список подписчиков и останавливает соседние, ни в чём не виноватые биндеры.

---

## Конвертер как ассет

`ConverterAsset<TFrom, TTo>` — `ScriptableObject`-обёртка вокруг обычного `[SerializeReference]`
конвертера. Двенадцать стопов градиента или карта на сорок значений enum, вписанные в поле биндера,
принадлежат одному этому полю: их приходится набирать заново в каждом префабе, а исправление —
повторять везде. Ассет настраивается один раз и подключается ссылкой.

Готовые подклассы уже есть в меню **Create → Aspid → MVVM → Converters** (`String`, `Float`, `Int`,
`Bool`, `Color`, `Vector2`, `Vector3`, `Object To String`). Свой тип — пустой запечатанный подкласс:
Unity не умеет создавать ассет открытого генерика, поэтому типы нужно закрыть.

```csharp
[CreateAssetMenu(menuName = "Game/Converters/Health Color", fileName = "HealthColorConverter")]
public sealed class HealthColorConverterAsset : ConverterAsset<float, Color> { }
```

На биндер такой ассет назначается через `ConverterAssetReference` — он есть в обычном пикере
конвертеров, потому что managed reference не может держать `ScriptableObject` напрямую.

---

## Отказ данных

`ConverterFailureMode` — общий словарь для конвертеров, которым могут дать значение, которое нельзя
преобразовать (строка цвета, которая не парсится; число вне диапазона):

| Режим | Поведение |
|-------|-----------|
| `ReturnFallback` | Вернуть настроенное запасное значение, сообщить об ошибке один раз |
| `ReturnInput` | Вернуть вход без изменений, сообщить об ошибке один раз |
| `Throw` | Бросить исключение |

Речь именно о **данных**. Неверно настроенный конвертер сообщает о себе всегда и независимо от
режима.

`Throw` стоит выбирать осознанно: исключение внутри push-а биндера останавливает все биндеры,
стоящие за ним в очереди. Чтобы бросок остался локальным, оберните конвертер в `SafeConverter`.

---

## Свой конвертер

```csharp
using System;
using Aspid.MVVM.StarterKit;
using Aspid.FastTools.Types;

[Serializable]
[TypeSelectorDisplay(Group = "Game/String", Name = "Percent", Tooltip = "0..1 как проценты")]
public sealed class PercentConverter : IConverter<float, string>
{
    public string Convert(float value) => $"{value * 100:F0}%";
}
```

С параметрами из Inspector:

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Game/Number", Name = "Clamp", Tooltip = "Зажимает значение в диапазон")]
public sealed class ClampFloatConverter : IConverter<float, float>
{
    [Tooltip("Нижняя граница.")]
    [SerializeField] private float _min;

    [Tooltip("Верхняя граница.")]
    [SerializeField] private float _max = 1f;

    public float Convert(float value) => Mathf.Clamp(value, _min, _max);
}
```

Чек-лист:

- **`[Serializable]`** — без него класс не появится в списке `[SerializeReference]`.
- **Публичный конструктор без параметров** — пикер создаёт экземпляр именно им. Если его нет,
  пометьте класс `[TypeSelectorDisplay(Hidden = true)]`, иначе он окажется в списке и выбор его
  сломается.
- **`[Tooltip]` на каждом сериализуемом поле** — в Inspector XML-документация не видна, tooltip
  единственное объяснение, которое дойдёт до того, кто настраивает значение.
- **`Group` и `Tooltip` в `[TypeSelectorDisplay]`** — иначе конвертер попадёт в общий плоский список.
- **Никаких аллокаций без кэша** — см. `CachedConverter` выше.

---

## Использование в Inspector

1. На биндере (например, `TextBinder`) найдите поле **Converter**.
2. Нажмите на выпадающий список — откроется пикер `[SerializeReference]` с группами.
3. Выберите конвертер и настройте его поля.

Из кода:

```csharp
// лямбда как конвертер
var converter = new GenericFuncConverter<float, string>(value => $"{value:P0}");
```

---

## Специализированные интерфейсы

Unity до 2023.1 не сериализует `[SerializeReference]`-поле с типом-открытым генериком. Для таких
версий существуют именованные псевдонимы:

```csharp
public interface IConverterFloat : IConverter<float, float> { }
public interface IConverterIntToLong : IConverter<int, long> { }
```

Всего их 40 — числовые пары, `IConverterString`, `IConverterObjectToString`,
`IConverterTimeSpanToString`, `IConverterColor`, `IConverterVector2`/`IConverterVector3` и их
кросс-комбинации. Полный список — в папках `Converters/Specific/` обеих сборок.

> **Все 40 помечены `[Obsolete]` и будут удалены в следующем мажоре.** Минимальная версия
> пакета — Unity 6000.0, где генерик-форма сериализуется напрямую, так что причины, по которой
> они существовали, больше нет. Используйте `IConverter<TFrom, TTo>`.

Вместе с ними устарели 70 обёрток `ToConvert` / `ToConvertSpecific`: они нужны были только затем,
чтобы присвоить лямбду полю с типом-псевдонимом. Замена — генерик-версия, которая остаётся:

```csharp
IConverter<float, float> converter = ((Func<float, float>)(x => x * 2f)).ToConvert();
```

---

## См. также

- [Биндеры](06-binders.md) — как биндер применяет конвертер
- [Режимы биндинга](03-binding-modes.md) — когда вызывается `ConvertBack`
- [StarterKit](StarterKit/README.md) — готовые биндеры с поддержкой конвертеров
