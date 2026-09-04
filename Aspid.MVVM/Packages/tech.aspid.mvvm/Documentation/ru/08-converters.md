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

---

## Обзор

Типичные сценарии:

| Из | В | Конвертер |
|----|---|-----------|
| `float` 0..1 | `"75%"` | `NumberFormatConverter` с форматом `P0` |
| `int` 1500 | `"Score: 1500"` | `StringFormatConverter` |
| `float` здоровья | `Color` красный → зелёный | `ThresholdColorConverter` |
| `float` 0..1 | `Vector3` шкалы | `FloatToVectorConverter` |
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

Конвертеры, которые умеют преобразовывать в обе стороны, реализуют `ITwoWayConverter<TFrom, TTo>` с методом `ConvertBack`: `BoolInvertConverter`, `EnumToNumberConverter<TEnum>`, `PassthroughConverter<T>`, `SequenceConverter<T>`.

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
> `RendererMaterials`, `ValueTwoWayBinder` — `ConvertBack` применяют, но молчат: там значение просто
> уезжает непреобразованным.

Ожидание к реализации: `ConvertBack(Convert(x)) == x`. Конвертер, который этого не гарантирует, не
должен реализовывать интерфейс — иначе значение будет дрейфовать на каждом круге.

Конвертер, у которого есть **обратный интерфейс** — вторая реализация, принимающая результат и
возвращающая исходное (`IConverter<B, A>` или `ITwoWayConverter<B, A>` рядом с прямой парой, либо
`ITwoWayConverter<A, A>`, где стороны совпадают) — называется **без `To`**:
`Vector2Vector3Converter`, `ColorColor32Converter`, `DegreesRadiansConverter`. Один
`ITwoWayConverter<A, B>` направления не добавляет — `ConvertBack` живёт внутри той же пары, — поэтому
`StringToIntConverter` и `BoolToValueConverter` остаются с `To`. Так по имени видно, можно ли
привязать конвертер с любой из двух сторон. Исключение одно — `SnapToStepConverter`: «to» там входит
в саму операцию, а не соединяет пару типов.

Двусторонние из коробки:

`AngleToQuaternionConverter`, `ArithmeticNumberConverter`,
`AudioLinearDecibelConverter`, `BoolInvertConverter`, `BoolLogicConverter`,
`BoolToValueConverter`, `CachedConverter`, `ColorColor32Converter`,
`ColorToHtmlStringConverter`, `ColorVector4Converter`, `DateTimeToUnixTimestampConverter`,
`DegreesRadiansConverter`, `EnumToNumberConverter`, `EulerToQuaternionConverter`,
`InverseConverter`, `InverseLerpConverter`, `LerpNumberConverter`,
`NormalizedPercentConverter`, `OffsetThenScaleConverter`, `PassthroughConverter`,
`PowerNumberConverter`, `QuaternionOffsetConverter`,
`RectVector4Converter`, `RemapNumberConverter`, `SafeConverter`,
`SecondsToTimeSpanConverter`, `SequenceConverter`, `StringToBoolConverter`,
`StringToDateTimeConverter`,
`StringToDecimalConverter`,
`StringToDoubleConverter`, `StringToEnumConverter`, `StringToFloatConverter`,
`StringToIntConverter`, `StringToLongConverter`, `StringToTimeSpanConverter`,
`StringToVector2Converter`, `StringToVector3Converter`, `UnixTimestampToDateTimeConverter`,
`Vector2Vector3Converter`, `VectorToVectorIntConverter`.

---

## Каталог

В пакете 192 конвертера. Правило раскладки одно: **группа — тип значения, которое лежит во
ViewModel; подгруппа `To <тип>` — то, во что оно превращается**. Ищете конвертер — начинайте с
того, что у вас есть: `float` — в `Aspid/Number`, строка — в `Aspid/String`; нужен другой тип на
выходе — откройте подгруппу `To ...`. Исключения три: `Aspid/Composition` — обёртки над другими
конвертерами, `Aspid/Localization` — вся локализация в одном месте, `Aspid/Asset` — инфраструктура
конвертеров-ассетов.

То же правило действует и в исходниках: папка повторяет группу
(`Converters/Strings/ToNumber/` ↔ `Aspid/String/To Number`).

### Aspid/Bool (3)

`BoolInvertConverter`, `BoolLogicConverter`; **To Value**: `BoolToValueConverter`.

> `BoolToValueConverter` двусторонний: обратный путь сравнивает пришедшее значение с двумя
> заданными и возвращает соответствующий bool. Значение, не совпавшее ни с одним, отдаёт fallback;
> одинаковые значения в обеих ветках делают обратный путь невозможным и пишутся в консоль ошибкой.

### Aspid/Number (51)

Число → число: `AngleDifferenceConverter`, `AngleWrapConverter`, `AnimationCurveConverter`,
`ArithmeticNumberConverter`, `AudioLinearDecibelConverter`,
`ClampNumberConverter`, `CountdownProgressConverter`, `DegreesRadiansConverter`,
`EasingConverter`, `InverseLerpConverter`, `LerpNumberConverter`, `ModuloNumberConverter`,
`NormalizedPercentConverter`, `NumericCastConverter`,
`PowerNumberConverter`, `RemapNumberConverter`,
`RoundNumberConverter`, `SmoothStepConverter`, `SnapToStepConverter`,
`OffsetThenScaleConverter`, `UnaryMathConverter`, `WrapNumberConverter`

| Подгруппа | Конвертеры |
|-----------|-----------|
| To Bool | `NumberCompareConverter` |
| To Color | `ColorLerpConverter`, `GradientEvaluateConverter`, `ThresholdColorConverter` |
| To Enum | `NumberToEnumConverter` |
| To Quaternion | `AngleToQuaternionConverter`, `QuaternionSlerpConverter` |
| To Rect Offset | `IntToRectOffsetConverter` |
| To Sprite | `NormalizedToSpriteConverter` |
| To String | `AbbreviatedNumberConverter`, `ByteSizeConverter`, `CurrencyConverter`, `NumberFormatConverter`, `OrdinalConverter`, `PaddedNumberConverter`, `PluralizeConverter`, `RatioToStringConverter`, `RepeatStringConverter`, `RomanNumeralConverter`, `SecondsToTimeStringConverter`, `SignedNumberStringConverter`, `ThousandsSeparatorConverter`, `ThresholdRichTextColorConverter` |
| To Time | `SecondsToTimeSpanConverter`, `UnixTimestampToDateTimeConverter` |
| To Value | `IndexToValueConverter` |
| To Vector | `FloatToVectorConverter`, `VectorLerpConverter` |

> `NumericCastConverter` — единственный способ сузить число управляемо. Без него `long.MaxValue`,
> попавший в int-биндер, молча уходит в отрицательное; `OverflowMode.Saturate` прижимает к границе,
> `Checked` бросает.

> `SecondsToTimeSpanConverter` принимает `int`, `long`, `float` и `double`. В целочисленных
> перегрузках обратный путь отбрасывает дробную часть секунды, а измерение, не помещающееся
> в `int` или `long`, прижимается к границе типа.

### Aspid/String (33)

Строка → строка: `ConcatStringConverter`, `DefaultStringConverter`, `MaskStringConverter`,
`PadStringConverter`, `ReplaceStringConverter`, `ReverseStringConverter`,
`SplitJoinStringConverter`, `StringFormatConverter`, `SubstringConverter`, `TextCaseConverter`,
`TrimStringConverter`, `TruncateStringConverter`

| Подгруппа | Конвертеры |
|-----------|-----------|
| Rich Text | `RichTextColorConverter`, `RichTextNoParseConverter`, `RichTextSizeConverter`, `RichTextStyleConverter`, `RichTextSanitizeConverter` |
| To Bool | `StringEmptyToBoolConverter`, `StringMatchToBoolConverter`, `StringToBoolConverter` |
| To Color | `HashToColorConverter`, `ParseHtmlStringConverter` |
| To Enum | `StringToEnumConverter` |
| To Number | `StringToDecimalConverter`, `StringToDoubleConverter`, `StringToFloatConverter`, `StringToIntConverter`, `StringToLongConverter` |
| To Sprite | `StringToSpriteConverter` |
| To Time | `StringToDateTimeConverter`, `StringToTimeSpanConverter` |
| To Vector | `StringToVector2Converter`, `StringToVector3Converter` |

Разбирающие конвертеры (`String To *`) в пикере называются `Parse *`: они парсят с учётом культуры
(`CultureInfoMode`), а текст, который не читается, отдают запасным значением.

> Для любого текста, который ввёл игрок, нужен `RichTextSanitizeConverter` или
> `RichTextNoParseConverter`. TMP исполняет разметку в любой строке, которую получает: ник
> `<size=400%>` растянет каждый ярлык, где он покажется, на экране каждого другого игрока.
> `RichTextNoParse` заворачивает всё в `<noparse>`; `SanitizeRichText` вырезает или экранирует теги
> выборочно, оставляя белый список.

> `StringEmptyToBoolConverter` полем `StringEmptiness` выбирает, что считать отсутствующей строкой:
> `NullOrEmpty` (по умолчанию), `Null` — пустая строка считается заполненной, `NullOrWhiteSpace` —
> строка из пробелов считается пустой. Последнее и означает «пользователь что-нибудь ввёл?».

### Aspid/Time (9)

`TimeSpanArithmeticConverter`, `TimeUntilConverter`

| Подгруппа | Конвертеры |
|-----------|-----------|
| To Bool | `DateTimeCompareConverter` |
| To Number | `DateTimeToUnixTimestampConverter`, `TimeSpanToNumberConverter` |
| To String | `DateTimeFormatConverter`, `DateTimeOffsetFormatConverter`, `RelativeTimeConverter`, `TimeSpanFormatConverter` |

### Aspid/Enum (7)

`EnumMaskConverter`; **To Bool**: `EnumMatchConverter`; **To Collection**:
`EnumToDropdownOptionDataConverter`; **To Number**: `EnumToNumberConverter`; **To String**:
`EnumFlagsToStringConverter`, `EnumToStringConverter`; **To Value**: `EnumToValueConverter`.

### Aspid/Collection (11)

`CollectionTakeConverter`, `DictionaryLookupConverter`

| Подгруппа | Конвертеры |
|-----------|-----------|
| To Bool | `CollectionContainsToBoolConverter`, `CollectionEmptyToBoolConverter` |
| To Number | `CollectionAggregateConverter`, `CollectionCountConverter` |
| To String | `CollectionCountToStringConverter`, `CollectionJoinToStringConverter` |
| To Value | `CollectionElementAtConverter`, `CollectionFirstConverter`, `CollectionLastConverter` |

> Все конвертеры группы, кроме `CollectionElementAtConverter`, принимают любой `IEnumerable<T>` — итератор
> или LINQ-запрос тоже. Счётчики берут `Count`, если он есть, и обходят последовательность только когда
> его нет; `CollectionEmptyToBoolConverter` в этом случае дёргает один элемент.
> `CollectionElementAtConverter` остаётся на `IReadOnlyList<T>`: чтобы выбрать элемент с конца, нужна
> длина.

### Aspid/Object (4)

`NullCoalesceConverter`; **To Bool**: `EqualityToBoolConverter`;
**To String**: `ValueToStringConverter`, `ObjectNameConverter`, `ObjectToStringConverter`.

> `EqualityToBoolConverter` с пустым операндом работает как проверка «отсутствует ли объект» и
> считает уничтоженный `UnityEngine.Object` отсутствующим: null-сторона сравнивается через
> перегруженный Unity оператор `==`, потому что `is null` для уничтоженного объекта даёт `false`.
> Reference equality сравнивает экземпляры как есть — уничтоженный объект пустому операнду там не равен.

### Aspid/Vector (28)

Вектор → вектор: `Vector2Vector3Converter`, `Vector3Vector4Converter`,
`VectorArithmeticConverter`, `VectorClampComponentsConverter`,
`VectorClampMagnitudeConverter`, `VectorNormalizeConverter`, `VectorRoundConverter`,
`VectorSwizzleConverter`, `VectorToVectorIntConverter`

> `VectorArithmeticConverter`, `VectorClampComponentsConverter`, `VectorClampMagnitudeConverter`,
> `VectorNormalizeConverter`, `VectorRoundConverter`, `VectorSwizzleConverter`, `VectorToFloatConverter`
> и `FloatToVectorConverter` обслуживают `Vector2`, `Vector3` и `Vector4` одним классом,
> `VectorToVectorIntConverter` — `Vector2` и `Vector3`, а `Vector2Vector3Converter` ходит в обе стороны. Настройки-векторы (`_operand`, `_min`, `_max`) хранятся как `Vector4`, и читаются
> только те компоненты, которые есть у привязанного вектора.

| Подгруппа | Конвертеры |
|-----------|-----------|
| Combine | `BoxCollider2DOffsetCombineConverter`, `BoxCollider2DSizeCombineConverter`, `BoxColliderCenterCombineConverter`, `BoxColliderSizeCombineConverter`, `CapsuleColliderCenterCombineConverter`, `RectTransformAnchoredPosition2DCombineConverter`, `RectTransformAnchoredPositionCombineConverter`, `RectTransformSizeDeltaCombineConverter`, `SphereColliderCenterCombineConverter`, `TransformEulerAnglesCombineConverter`, `TransformPosition2DCombineConverter`, `TransformPositionCombineConverter`, `TransformScaleCombineConverter` |
| To Number | `DirectionAngleConverter`, `VectorDistanceConverter`, `VectorToFloatConverter` |
| To Quaternion | `EulerToQuaternionConverter`, `LookRotationConverter` |
| To Rect Offset | `Vector4ToRectOffsetConverter` |

> Конвертеры подгруппы `Combine` берут часть компонент у привязанного вектора, часть — у компонента
> сцены (`Transform`, `RectTransform`, коллайдер). Пары `*2D*` — для двумерных коллайдеров и
> `Vector2`-свойств.

### Aspid/Color (14)

Цвет → цвет: `ColorAlphaConverter`, `ColorBlockAlphaConverter`,
`ColorBlockFadeDurationConverter`, `ColorBlockStateConverter`, `ColorBlockTintConverter`,
`ColorChannelConverter`, `ColorGrayscaleConverter`, `ColorHsvConverter`, `ColorTintConverter`,
`ColorColor32Converter`, `ColorToColorBlockConverter`, `HdrIntensityConverter`;
**To String**: `ColorToHtmlStringConverter`; **To Vector**: `ColorVector4Converter`.

### Остальные группы

| Группа | Конвертеры |
|--------|-----------|
| `Aspid/Quaternion` (4) | `QuaternionOffsetConverter`; **To Number**: `QuaternionToAngleConverter`; **To Vector**: `QuaternionToEulerConverter`, `QuaternionVector4Converter` |
| `Aspid/Bounds` (2) | **To Rect**: `BoundsToRectConverter`; **To Vector**: `BoundsToVectorConverter` |
| `Aspid/Rect` (1) | **To Vector**: `RectVector4Converter` |
| `Aspid/Rect Offset` (1) | `RectOffsetScaleConverter` |
| `Aspid/Texture` (3) | `SpriteToTextureConverter`, `Texture2DToSpriteConverter`; **To Rect**: `TextureToSpriteRectConverter` |
| `Aspid/Localization` (4) | `LocaleToStringConverter`, `LocalizedEnumConverter`, `LocalizedNumberConverter`, `LocalizedStringConverter` |
| `Aspid/Material` (1) | `MaterialInstanceConverter` |
| `Aspid/Asset` (1) | `ConverterAssetReference` |

Группа `Aspid/Composition` (8) описана в разделе [Композиция](#композиция).

---

## Общие перечисления

Три енума настраивают конвертеры из разных групп, поэтому их стоит знать до каталога.

| Енум | Где встречается | Что решает |
|------|-----------------|-----------|
| `ComparisonMode` | `NumberCompareConverter`, `DateTimeCompareConverter` | `Equal`, `NotEqual`, `LessThan`, `GreaterThan`, `LessThanOrEqual`, `GreaterThanOrEqual`. Читается как `привязанное <оп> настроенное`. У `NumberCompareConverter` допуск общий для всех шести сравнений и зависит от типа: `int`/`long` — точно, `float` — 1e-6 от величины, `double` — 1e-12 |
| `CultureInfoMode` | все строковые и разбирающие конвертеры, а также биндеры полей ввода и текста | Какой культурой форматировать и разбирать. Текст, который видит игрок, — `CurrentCulture`; текст, который уезжает в сейв, в сеть или в `PlayerPrefs`, — `InvariantCulture` |
| `ConverterFailureMode` | `BoolLogicConverter`, `EnumMaskConverter` | Что делать со значением, которое не преобразуется: `ReturnFallback` или `ReturnInput`. Поле есть только у конвертеров, у которых вход и выход одного типа, — вернуть вход больше негде |

> Разделитель дробной части — запятая в половине Европы. Число, записанное одной культурой и
> разобранное другой, теряет дробную часть, а не падает: `1,5` под `InvariantCulture` читается как
> `15`. Для всего, что ходит туда-обратно, ставьте `InvariantCulture`.

`CultureInfoMode` резолвится в `CultureInfo` расширением `ToCultureInfo()` из
`ToCultureStringExtensions` — там же лежат перегрузки `ToCultureString(число, режим)`. Оба типа
лежат вне папки конвертеров, в `StarterKit/Runtime/Globalization`: биндеры полей ввода и текста
держат такое же сериализованное поле.

---

## Плюрализация

`PluralizeConverter` (`Aspid/Number/To String`) пишет число со словом в нужной форме. Грамматики в нём
нет: конвертер держит только формат фразы, а слова и правило их выбора лежат в `PluralRule`, который
выбирается в инспекторе — группа `Aspid/Plural Rule`.

`PluralRule` — абстрактный класс, реализующий `IConverter<long, string>`: число (по модулю) → слово.
Наследник объявляет только те слова, которые нужны его языку, поэтому в инспекторе не бывает поля, до
которого выбранная грамматика не дотянется.

| Правило | Языки | Поля |
|---------|-------|------|
| `SingleFormPluralRule` | китайский, японский, корейский, тайский, вьетнамский, турецкий | `word` |
| `EnglishPluralRule` | английский, немецкий, нидерландский, испанский, итальянский, шведский | `one`, `other` |
| `FrenchPluralRule` | французский, бразильский португальский, хинди | `one` (0 и 1), `other` |
| `EastSlavicPluralRule` | русский, украинский, белорусский | `one`, `few`, `many` |
| `PolishPluralRule` | польский | `one` (ровно 1), `few`, `many` |
| `CzechPluralRule` | чешский, словацкий | `one`, `few`, `other` |
| `ArabicPluralRule` | арабский | `one`, `two`, `few`, `many`, `other` |

Общее у всех — поле `zero` из базового класса: необязательное слово, которое забирает ноль независимо
от грамматики. В английском отдельной формы для нуля нет, а «Нет предметов» нужно всем. Слово, до
которого грамматика дотянулась, но которое не заполнено, логируется на каждый push — молча подставлять
соседнюю форму конвертер не станет.

Языка нет в списке — наследник `PluralRule` в проекте: объявить свои поля и переопределить
`Word(long)`. Ноль и отчёт о незаполненном слове достаются от базы, а в picker'е правило встаёт в ту
же группу рядом со встроенными.

`CollectionCountToStringConverter` эту логику не дублирует: он считает элементы, отдаёт число
`PluralizeConverter` и оставляет себе только текст для пустой коллекции — фразу, которая пишется без
числа впереди.

---

## Композиция

Группа `Aspid/Composition` — не преобразования, а обёртки над другими конвертерами.

| Конвертер | Назначение |
|-----------|-----------|
| `ComposeConverter<TFrom, TMid, TTo>` | Два конвертера подряд, с разными типами на стыке |
| `SequenceConverter<T>` | Цепочка любой длины, все звенья `T → T` |
| `CachedConverter<TFrom, TTo>` | Повторяет прошлый результат, пока вход не изменился; каждое направление кэширует отдельно |
| `SafeConverter<TFrom, TTo>` | Ловит исключение внутреннего конвертера и отдаёт запасное значение — в обе стороны |
| `NullGuardConverter<TFrom, TTo>` | Не вызывает внутренний конвертер на `null` |
| `ConditionalConverter<T>` | Выбирает один из двух конвертеров по предикату |
| `PassthroughConverter<T>` | Ничего не делает; заглушка и элемент по умолчанию |
| `InverseConverter<TFrom, TTo>` | Гоняет двусторонний конвертер в обратную сторону |

```csharp
// float → "1,500" с кэшем, чтобы не собирать строку заново на каждом push
var converter = new CachedConverter<float, string>(
    new NumberFormatConverter());
```

`CachedConverter` стоит держать в голове: биндер шлёт значение на каждое **уведомление**, а не на
каждое **изменение**, поэтому конвертер, который что-то аллоцирует, вызывается заметно чаще, чем
кажется.

`SafeConverter` полезен потому, что рассылка биндеров — голый multicast: исключение из одного
конвертера обрывает список подписчиков и останавливает соседние, ни в чём не виноватые биндеры.

Обёртка без того, что она оборачивает, бессмысленна, поэтому конструкторы обёрток бросают
`ArgumentNullException` на пустое звено: `inner` у `Cached`, `Safe` и `NullGuard`, оба звена у
`Compose`, конвертер у `Inverse`, предикат у `Conditional`, ассет у `ConverterAssetReference`. Полупустое состояние —
инспекторное: там обёртка собирается по полю за раз, поэтому пустое звено не падает, а сообщает
об ошибке на каждом преобразовании и отдаёт запасное значение. Ветви `then` / `else`
у `Conditional` и звенья `SequenceConverter` пустыми быть могут — там `null` означает «пропустить
этот шаг».

---

## Конвертер как ассет

`ConverterAsset<TFrom, TTo>` — `ScriptableObject`-обёртка вокруг обычного `[SerializeReference]`
конвертера. Двенадцать стопов градиента или карта на сорок значений enum, вписанные в поле биндера,
принадлежат одному этому полю: их приходится набирать заново в каждом префабе, а исправление —
повторять везде. Ассет настраивается один раз и подключается ссылкой.

Готовые подклассы уже есть в меню **Create → Aspid → MVVM → Converters**, сгруппированные по типу
входа (`Numbers`, `String`, `Vector`, `Color`, `Time` и т.д.): одноимённые для преобразований
«в себя» (`Float Converter`, `Vector3 Converter`) и `X To Y Converter` для смены типа
(`Vector3 To Vector2 Converter`, `String To Int Converter`). Покрыты не все пары каталога —
числовые касты между `int`, `long`, `float` и `double` ассетов не имеют, их настраивают
`[SerializeReference]`-полем. Недостающая пара — это пустой запечатанный подкласс на одну строку:
Unity не умеет создавать ассет открытого генерика, поэтому типы нужно закрыть. Enum-семейство поставляется и открытыми базами
(`EnumConverterAsset<T>` и т.д.) — закройте их своим enum'ом.

```csharp
[CreateAssetMenu(menuName = "Game/Converters/Health Color", fileName = "HealthColorConverter")]
public sealed class HealthColorConverterAsset : ConverterAsset<float, Color> { }
```

На биндер такой ассет назначается через `ConverterAssetReference` — он есть в обычном пикере
конвертеров, потому что managed reference не может держать `ScriptableObject` напрямую.

---

## Отказ данных

Конвертер, которому дали значение, которое нельзя преобразовать (строка цвета, которая не парсится;
число вне диапазона), сообщает об этом ошибкой и возвращает настроенное запасное значение — поле
`Fallback` в инспекторе.

Об ошибке сообщается на каждом провале, а не один раз: значение, которое перестало преобразовываться
посреди сессии, — как раз тот случай, который правило «логировать однажды» скрывает.

Запасное значение отвечает на **любой** провал — и на данные, которые не преобразуются, и на
конфигурацию, через которую не преобразуется ничто (обе ветки `BoolToValueConverter` равны, список
true-написаний пуст).

`ConverterFailureMode` добавляет к запасному значению второй вариант — вернуть вход без изменений
(`ReturnInput`) — и стоит поэтому только у `BoolLogicConverter` и `EnumMaskConverter`: вернуть вход
можно лишь там, где вход и выход одного типа. У остальных конвертеров возвращать нечего, поэтому
поля `On Failure` у них нет.

Все сообщения — и об отказе данных, и о неверной настройке — проходят через один хелпер
`ConverterLogger`: отказ данных печатается через `LogError` как
`[Aspid.MVVM] Конвертер: expected X but got "Y". Using the fallback.` (или
`Returning the input unchanged.` при `ReturnInput`), всё остальное — как
`[Aspid.MVVM] Конвертер: проблема. Что возвращается вместо результата.` По префиксу `[Aspid.MVVM]`
ошибки пакета ищутся в консоли; `null` печатается словом `null`, строка — в кавычках, остальные
значения — как есть; generic-имя конвертера печатается закрытым (`BoolToValueConverter<Sprite>`).
Само это оформление типов и значений вынесено в `LogMessageText`: имя типа пишет
`LogMessageText.GetTypeName`, а значение — extension `value.Describe()`, который
интерполируется прямо в текст сообщения. Так текст внутри сообщения выглядит одинаково и в логе,
и в исключении. Для сообщений, которые не являются ошибками, у хелпера есть обычный `Log` в том же
формате. `Debug.Log`/`Debug.LogError` в коде конвертеров живут только внутри `ConverterLogger`.

Конвертер логирует о себе extension-методами на `IConverter` — `this.LogError(problem, consequence)`,
`this.LogError(exception, consequence)`, `this.Log(message)` — тип берётся из `this`, а конвертер,
который сам является объектом Unity (например, `ConverterAsset`), автоматически становится и
`context` — объектом, который Unity подсветит по клику на лог; для остальных `context` передаётся
опциональным параметром. Перегрузки с `Type` остаются для хелперов, которые сообщают от чужого
имени.

В коде запасное значение отдаётся одним вызовом — extension
`this.UseFallback(_fallback, value.Expected("a whole number"))`: он логирует отказ и возвращает
`_fallback`. Формулировку проблемы вызов приносит готовой; канонную — «expected X but got Y» —
строит extension `value.Expected("a whole number")` из `LogMessageText`, провал конфигурации
пишет её сам.

У двух конвертеров, у которых есть режим, он лежит вместе со значением в одном поле
`ConverterFallback<T>` (в инспекторе — две строки: значение и `On Failure`), а вызов выглядит как
`_fallback.Fail(this, value, problem)` — он тоже логирует отказ, но возвращает то, что велит режим,
поэтому принимает и само значение, на котором случился провал.

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

Имя класса выбирается по одному правилу. `XToYConverter` — зарезервированное имя канонического
преобразования пары типов, того единственного, которое ожидается по умолчанию
(`Vector2Vector3Converter`, `StringToIntConverter` — парсинг). Любой другой конвертер той же пары
называется по операции (`OrdinalConverter`, `StringLengthConverter`), а вариант канонического
преобразования — это настройка существующего класса, не новый класс. В пикере `Name` несёт
операцию, типы уже сказаны группой и подгруппой.

Чек-лист:

- **`[Serializable]`** — без него класс не появится в списке `[SerializeReference]`.
- **Конструктор без параметров** — пикер создаёт экземпляр именно им, через
  `Activator.CreateInstance(type, nonPublic: true)`, так что публичным он быть не обязан: многие
  конвертеры прячут его как `private`, оставляя публичным только конструктор с параметрами. Если
  такого конструктора нет вовсе, пометьте класс `[TypeSelectorDisplay(Hidden = true)]`, иначе он
  окажется в списке и выбор его сломается.
- **`[Tooltip]` на каждом сериализуемом поле** — в Inspector XML-документация не видна, tooltip
  единственное объяснение, которое дойдёт до того, кто настраивает значение.
- **`Group` и `Tooltip` в `[TypeSelectorDisplay]`** — иначе конвертер попадёт в общий плоский список.

> `TypeSelectorDisplayAttribute` помечен `[Conditional("UNITY_EDITOR")]` и `Inherited = false`. Это
> значит две вещи. Все аннотации исчезают из метаданных, если собрать сборку вне Unity — DLL,
> собранная обычным `dotnet build`, придёт в пикер без групп, имён и скрытий. И атрибут не
> наследуется: подкласс не получает разметку базового класса, её нужно повторить.
- **Никаких аллокаций без кэша** — см. `CachedConverter` выше.

---

## Использование в Inspector

1. На биндере (например, `TextBinder`) найдите поле **Converter**.
2. Нажмите на выпадающий список — откроется пикер `[SerializeReference]` с группами.
3. Выберите конвертер и настройте его поля.

Из кода:

```csharp
// лямбда как конвертер
var converter = new FuncConverter<float, string>(value => $"{value:P0}");

// то же через ToConverter
IConverter<float, float> doubler = ((Func<float, float>)(x => x * 2f)).ToConverter();
```

---

## См. также

- [Биндеры](06-binders.md) — как биндер применяет конвертер
- [Режимы биндинга](03-binding-modes.md) — когда вызывается `ConvertBack`
- [StarterKit](StarterKit/README.md) — готовые биндеры с поддержкой конвертеров
