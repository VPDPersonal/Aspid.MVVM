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
предлагает, и передаёт значение без изменений, когда нет. Такой биндер пишет предупреждение в
консоль, если ему назначен односторонний конвертер, — иначе значение молча уезжало бы во ViewModel
непреобразованным.

Ожидание к реализации: `ConvertBack(Convert(x)) == x`. Конвертер, который этого не гарантирует, не
должен реализовывать интерфейс — иначе значение будет дрейфовать на каждом круге.

Двусторонние из коробки:

`ArithmeticNumberConverter`, `AngleToQuaternionConverter`, `AudioDecibelToLinearConverter`,
`AudioLinearToDecibelConverter`, `BoolInvertConverter`, `DegreesToRadiansConverter`,
`EnumToIntConverter`, `EulerToQuaternionConverter`, `InverseLerpConverter`, `LerpNumberConverter`,
`NormalizedToPercentConverter`, `PassthroughConverter`, `QuaternionOffsetConverter`,
`RemapNumberConverter`, `SecondsToTimeSpanConverter`, `SequenceConverter`, `StringToEnumConverter`,
`StringToFloatConverter`, `StringToIntConverter`, `StringToLongConverter`,
`UnixTimestampToDateTimeConverter`, `Vector2ToVector2IntConverter`, `Vector3ToVector3IntConverter`.

---

## Каталог

В пакете 147 конвертеров. В Inspector они разложены по группам — группа видна в выпадающем списке
поля `Converter`.

### Aspid/Bool (8)

`BoolInvertConverter`, `BoolLogicConverter`, `BoolToValueConverter`, `NumberToBoolConverter`,
`ObjectNullToBoolConverter`, `StringEmptyToBoolConverter`, `StringMatchToBoolConverter`,
`UnityObjectNullToBoolConverter`

`UnityObjectNullToBoolConverter` — не то же самое, что `ObjectNullToBoolConverter`: он использует
перегруженный `==` Unity и потому видит уничтоженный объект, для которого `is null` вернёт `false`.

`StringEmptyToBoolConverter` полем `StringEmptiness` выбирает, что считать отсутствующей строкой:
`NullOrEmpty` (по умолчанию), `Null` — пустая строка считается заполненной, `NullOrWhiteSpace` —
строка из пробелов считается пустой. Последнее и означает «пользователь что-нибудь ввёл?».

### Aspid/Number (15)

`AnimationCurveConverter`, `ArithmeticNumberConverter`, `AudioDecibelToLinearConverter`,
`AudioLinearToDecibelConverter`, `ClampNumberConverter`, `CountdownProgressConverter`,
`InverseLerpConverter`, `LerpNumberConverter`, `NormalizedToPercentConverter`,
`RemapNumberConverter`, `RoundNumberConverter`, `SmoothStepConverter`, `SnapToStepConverter`,
`UnaryMathConverter`, `WrapNumberConverter`

### Aspid/String (36)

Форматирование чисел: `AbbreviatedNumberConverter` (`1234` → `1.2K`), `ByteSizeConverter`,
`CurrencyConverter`, `NumberFormatConverter`, `OrdinalConverter` (`3` → `3rd`),
`PaddedNumberConverter`, `PercentStringConverter`, `RatioToStringConverter`, `RomanNumeralConverter`,
`SignedNumberStringConverter`.

Манипуляции со строкой: `ConcatStringConverter`, `DefaultStringConverter`, `MaskStringConverter`,
`PadStringConverter`, `PluralizeConverter`, `RepeatStringConverter`, `ReplaceStringConverter`,
`SubstringConverter`, `TextCaseConverter`, `TrimStringConverter`, `TruncateStringConverter`.

Rich text (TextMeshPro): `RichTextColorConverter`, `RichTextNoParseConverter`,
`RichTextSizeConverter`, `RichTextStyleConverter`, `ThresholdRichTextColorConverter`.

> `RichTextNoParseConverter` — для любого текста, который ввёл игрок. TMP исполняет разметку в любой
> строке, которую получает: ник `<size=400%>` растянет каждый ярлык, где он покажется, на экране
> каждого другого игрока.

Разбор строки: `StringToBoolParseConverter`, `StringToDateTimeConverter`, `StringToEnumConverter`,
`StringToFloatConverter`, `StringToIntConverter`, `StringToLongConverter`.

Общее: `GenericToStringConverter`, `ObjectToStringConverter`, `StringFormatConverter`,
`TimeSpanToStringConverter`.

### Aspid/Time (8)

`DateTimeFormatConverter`, `DateTimeToBoolConverter`, `RelativeTimeConverter`,
`SecondsToTimeSpanConverter`, `SecondsToTimeStringConverter`, `TimeSpanFormatConverter`,
`TimeSpanToNumberConverter`, `UnixTimestampToDateTimeConverter`

### Aspid/Colour (14)

`ColorAlphaConverter`, `ColorBlockAlphaConverter`, `ColorBlockFadeDurationConverter`,
`ColorBlockTintConverter`, `ColorGrayscaleConverter`, `ColorHsvConverter`, `ColorLerpConverter`,
`ColorTintConverter`, `ColorToColorBlockConverter`, `ColorToHtmlStringConverter`,
`GradientEvaluateConverter`, `HashToColorConverter`, `ParseHtmlStringConverter`,
`ThresholdColorConverter`

### Aspid/Vector (23)

Арифметика и форма: `FloatToVector2Converter`, `FloatToVector3Converter`,
`Vector2SubstitutionConverter`, `Vector2ToVector2IntConverter`, `Vector2ToVector3Converter`,
`Vector3ArithmeticConverter`, `Vector3SubstitutionConverter`, `Vector3ToFloatConverter`,
`Vector3ToVector2Converter`, `Vector3ToVector3IntConverter`, `VectorClampMagnitudeConverter`,
`VectorLerpConverter`, `VectorNormalizeConverter`, `VectorRoundConverter`.

Комбинирование со сценой — берут часть компонент у привязанного вектора, часть у компонента сцены:
`Vector2CombineConverter`, `BoxColliderCentreCombineConverter`, `BoxColliderSizeCombineConverter`,
`CapsuleColliderCentreCombineConverter`, `RectTransformAnchoredPositionCombineConverter`,
`SphereColliderCentreCombineConverter`, `TransformEulerAnglesCombineConverter`,
`TransformPositionCombineConverter`, `TransformScaleCombineConverter`.

### Aspid/Rotation (9)

`AngleToDirectionConverter`, `AngleToQuaternionConverter`, `AngleWrapConverter`,
`DegreesToRadiansConverter`, `DirectionToAngleConverter`, `EulerToQuaternionConverter`,
`LookRotationConverter`, `QuaternionOffsetConverter`, `QuaternionToEulerConverter`

### Aspid/Collection (6)

`CollectionAggregateConverter`, `CollectionContainsToBoolConverter`, `CollectionCountConverter`,
`CollectionElementAtConverter`, `CollectionEmptyToBoolConverter`, `ListToStringConverter`

### Остальные группы

| Группа | Конвертеры |
|--------|-----------|
| `Aspid/Enum` (5) | `EnumToBoolConverter`, `EnumToDropdownOptionDataConverter`, `EnumToIntConverter`, `EnumToStringConverter`, `EnumToValueConverter` |
| `Aspid/Object` (3) | `EqualityToBoolConverter`, `IndexToValueConverter`, `NullCoalesceConverter` |
| `Aspid/Texture` (4) | `NormalizedToSpriteConverter`, `ObjectNameConverter`, `SpriteToTextureConverter`, `Texture2DToSpriteConverter` |
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
