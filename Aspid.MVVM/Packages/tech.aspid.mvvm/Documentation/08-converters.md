# Конвертеры

Конвертеры преобразуют значения при передаче от ViewModel к View, не изменяя сам ViewModel.

## Содержание

- [Обзор](#обзор)
- [IConverter\<TFrom, TTo\>](#iconvertertfrom-tto)
- [Встроенные конвертеры](#встроенные-конвертеры)
- [Композиция конвертеров](#композиция-конвертеров)
- [Обработка ошибок](#обработка-ошибок)
- [Конвертеры-ассеты](#конвертеры-ассеты)
- [Создание кастомного конвертера](#создание-кастомного-конвертера)
- [Использование в Inspector](#использование-в-inspector)
- [Специализированные интерфейсы](#специализированные-интерфейсы)

---

## Обзор

Типичные сценарии:
- `float` (0-1) → `string` ("75%")
- `int` (score) → `string` ("Score: 1500")
- `float` (health ratio) → `bool` (alive/dead)
- Арифметические преобразования (умножение, деление)

Конвертер назначается на биндер — через Inspector (`[SerializeReference]`) или через generic-параметр.

---

## IConverter\<TFrom, TTo\>

```csharp
public interface IConverter<in TFrom, out TTo>
{
    TTo Convert(TFrom value);
}
```

Однонаправленный — преобразует значение при передаче ViewModel → View. Обратного преобразования нет (для TwoWay значение из View передаётся без конвертации).

Конвертеры, которые умеют преобразовывать в обе стороны, реализуют `ITwoWayConverter<TFrom, TTo>` с методом `ConvertBack`: `BoolInvertConverter`, `EnumToIntConverter<TEnum>`, `PassthroughConverter<T>`, `SequenceConverters<T>`.

---

## Встроенные конвертеры

### Строковые

| Конвертер | Преобразование | Описание |
|-----------|---------------|----------|
| `ObjectToStringConverter` | `object?` → `string?` | Вызывает `ToString()` |
| `GenericToString<TFrom>` | `T?` → `string?` | С поддержкой `string.Format` |
| `StringFormatConverter` | `string?` → `string?` | Оборачивает в `string.Format(_format, value)` |
| `TimeSpanToStringConverter` | `TimeSpan` → `string?` | Форматирование времени |

```csharp
// GenericToString с форматом:
// _format = "Score: {0}"
// 1500 → "Score: 1500"
```

Культура задаётся полем `CultureInfoMode` (по умолчанию `CurrentCulture` — локаль устройства): `CurrentCulture`, `CurrentUICulture`, `InvariantCulture`, `InstalledUICulture`, `DefaultThreadCurrentCulture`, `DefaultThreadCurrentUICulture`. Для чисел, которые уходят в сохранения или логи, берите `InvariantCulture` — иначе разделитель дробной части зависит от устройства.

`StringFormatConverter` дополнительно решает, форматировать ли пустое значение: по умолчанию `null` и строка из пробелов возвращаются без шаблона, а флаг `Format Empty Values` пропускает через шаблон и их (вместо `null` подставляется пустая строка).

### Логические

| Конвертер | Преобразование | Описание |
|-----------|---------------|----------|
| `NumberToBoolConverter` | числа → `bool` | Сравнение с порогом (`Comparisons`) |
| `ObjectNullToBoolConverter` | `object?` → `bool` | `null` check с инверсией |
| `UnityObjectNullToBoolConverter` | `Object?` → `bool` | То же для `UnityEngine.Object` — ловит и уничтоженный объект, который `is null` не видит |
| `StringEmptyToBoolConverter` | `string?` → `bool` | Пустая строка check (`Null` / `NullOrEmpty` / `NullOrWhiteSpace`) |
| `StringMatchToBoolConverter` | `string?` → `bool` | Сравнение с заданным текстом (`StringMatch`) |
| `EqualityToBoolConverter<T>` | `T` → `bool` | Сравнение с заданным значением через `EqualityComparer<T>.Default` |
| `BoolInvertConverter` | `bool` → `bool` | Инверсия, двунаправленная |
| `BoolLogicConverter` | `bool` → `bool` | Логическая операция с заданным операндом (`LogicOperation`) |

Сравнения (`Comparisons`): `Equal`, `Inequality`, `LessThan`, `GreaterThan`, `LessThanOrEqual`, `GreaterThanOrEqual`.

Пустота строки (`StringEmptiness`): `NullOrEmpty` (по умолчанию), `Null` — пустая строка считается заполненной, `NullOrWhiteSpace` — строка из пробелов считается пустой. Последнее и означает «пользователь что-нибудь ввёл?».

Способ сравнения строк (`StringMatch`): `Equals`, `Contains`, `StartsWith`, `EndsWith`. Регистр по умолчанию игнорируется (`IgnoreCase = true`), `null` не совпадает ни с чем.

Логические операции (`LogicOperation`): `And`, `Or`, `Xor`, `Nand`, `Nor`, `Xnor`.

Проверки на `null`, пустоту, совпадение строки и равенство значений имеют флаг инверсии (`IsInvert`) — отдельный конвертер-инвертор для них не нужен.

```csharp
// NumberToBoolConverter:
// Comparison = GreaterThan, Value = 0
// 5 → true, 0 → false, -1 → false

// StringMatchToBoolConverter:
// Match = StartsWith, Text = "boss_", IgnoreCase = true
// "Boss_Golem" → true, "mob_rat" → false

// BoolLogicConverter:
// Operation = And, Operand = true
// true → true, false → false
```

Для перечислений есть отдельный `EnumToBoolConverter<TEnum>` — см. [Перечисления](#перечисления).

### Выбор значения

Конвертеры этой группы держат авторские значения (спрайты, цвета, строки) на стороне View — ViewModel остаётся с одним `bool`, `int` или enum.

| Конвертер | Преобразование | Описание |
|-----------|---------------|----------|
| `BoolToValueConverter<T>` | `bool` → `T` | Два значения: для `true` и для `false` |
| `IndexToValueConverter<T>` | `int` → `T` | Массив значений, поведение за границами задаёт `IndexMode` |
| `NullCoalesceConverter<T>` | `T?` → `T` | Подставляет заданное значение вместо `null` |

Выход за границы массива (`IndexMode`): `Clamp` (по умолчанию) — ближайший край, `Wrap` — по кругу, `Fallback` — авторское значение. Пустой массив всегда даёт `Fallback`.

```csharp
// IndexToValueConverter<Sprite>:
// Values = [bronze, silver, gold], Mode = Clamp
// 0 → bronze, 2 → gold, 7 → gold, -1 → bronze

// BoolToValueConverter<Color>:
// TrueValue = Color.green, FalseValue = Color.red
// true → green, false → red
```

Значение по перечислению — `EnumToValueConverter<TEnum, T>`, см. [Перечисления](#перечисления).

### Перечисления

| Конвертер | Преобразование | Описание |
|-----------|---------------|----------|
| `EnumToBoolConverter<TEnum>` | `TEnum` → `bool` | Проверка значения или флагов (`EnumMatch`) |
| `EnumToIntConverter<TEnum>` | `TEnum` → `int` | Двунаправленный — для `Dropdown.value` |
| `EnumToStringConverter<TEnum>` | `TEnum` → `string` | Подпись значения (`EnumNameSource`) |
| `EnumToValueConverter<TEnum, T>` | `TEnum` → `T` | Таблица `Entry { Key, Value }` + значение по умолчанию |
| `EnumToDropdownOptionDataConverter` | `Enum?` → `IEnumerable<TMP_Dropdown.OptionData>` | Подпись и спрайт для каждого значения |

Способ проверки (`EnumMatch`): `Equals`, `NotEquals`, `HasAllFlags`, `HasAnyFlag`. Флаговые режимы рассчитаны на `[Flags]`-перечисления.

Источник подписи (`EnumNameSource`): `Name` — имя члена как в коде, `InspectorName` — значение атрибута `[InspectorName]` с откатом на имя члена. Значение, которого нет в перечислении, даёт авторский `Fallback`.

`EnumToIntConverter` в обратную сторону не проверяет, что число соответствует объявленному члену: индекс, которого нет в перечислении, вернётся как значение этого типа.

```csharp
// EnumToBoolConverter<GameState>:
// Target = GameState.Loading, Match = Equals
// Loading → true, Ready → false

// Флаги:
// Target = Damage.Fire | Damage.Ice, Match = HasAnyFlag
// Damage.Fire → true, Damage.None → false

// EnumToValueConverter<Weather, Color>:
// Map = [Clear → yellow, Rain → blue], Fallback = gray
// Clear → yellow, Snow → gray
```

Карта `EnumToValueConverter` — данные, а не подкласс биндера, поэтому одну и ту же таблицу можно переиспользовать (иконка состояния и его цвет) и вынести в [ассет](#конвертеры-ассеты).

### Арифметические

| Конвертер | Преобразование | Описание |
|-----------|---------------|----------|
| `ArithmeticNumberConverter` | числа → числа | Арифметика с коэффициентом |

Операции (`NumberOperation`):
- `Plus` — value + coefficient
- `Minus` — value - coefficient
- `Multiply` — value × coefficient
- `Division` — value / coefficient

```csharp
// ArithmeticNumberConverter:
// Operation = Multiply, Coefficient = 100
// 0.75f → 75f (проценты)
```

### Векторные

| Конвертер | Преобразование | Описание |
|-----------|---------------|----------|
| `Vector2ToVector3Converter` | `Vector2` → `Vector3` | Куда положить компоненты: `XY`, `XZ`, `YZ` |
| `Vector3ToVector2Converter` | `Vector3` → `Vector2` | Какие компоненты взять: `XY`, `XZ`, `YX`, `YZ`, `ZX`, `ZY` |
| `Vector2SubstitutionConverter` | `Vector2` → `Vector2` | Перестановка и дублирование компонент: `XY`, `YX`, `YY`, `XX` |
| `Vector3SubstitutionConverter` | `Vector3` → `Vector3` | То же для трёх компонент — все перестановки и повторы (`XZY`, `XXY`, `ZYY`, …) |
| `Vector3CombineConverter` | `Vector2`/`Vector3` → `Vector3` | Часть компонент из привязанного вектора, остальные — из опорного. Абстрактный: опорный вектор задаёт наследник |
| `Vector2CombineConverter` | `Vector2` + `Vector2` → `Vector2` | То же для двух компонент. Не `IConverter` — оба вектора передаёт биндер |

Режим `Mode` у Combine-конвертеров перечисляет, какие компоненты берутся из привязанного вектора (`X`, `Y`, `Z`, `XY`, `XZ`, `YZ`, `XYZ`); остальные приходят из опорного. Дополнительно можно указать конвертеры до и после сборки (`PreConverter` / `PostConverter`).

Наследники `Vector3CombineConverter` отличаются только источником опорного вектора: `TransformPositionCombineConverter`, `TransformEulerAnglesCombineConverter`, `TransformScaleCombineConverter`, `RectTransformAnchoredPositionCombineConverter`, `BoxColliderCentreCombineConverter`, `BoxColliderSizeCombineConverter`, `SphereColliderCentreCombineConverter`, `CapsuleColliderCentreCombineConverter`.

```csharp
// TransformPositionCombineConverter:
// Mode = XZ, опорный вектор — transform.position
// привязанный (5, 9, 7) + позиция (0, 3, 0) → (5, 3, 7)
```

### Цвета

| Конвертер | Преобразование | Описание |
|-----------|---------------|----------|
| `ParseHtmlStringConverter` | `string?` → `Color` | Разбирает `#RRGGBB`, `#RRGGBBAA` и имена цветов |

Поведение при неразобранной строке задаёт [`ConverterFailureMode`](#обработка-ошибок); `ReturnInput` здесь недоступен — на входе строка, на выходе цвет — и работает как `ReturnFallback`.

### Функциональные

| Конвертер | Описание |
|-----------|----------|
| `GenericFuncConverter<TFrom, TTo>` | Обёртка над `Func<TFrom?, TTo?>`; создаётся и расширением `func.ToConvert()` |

Конвертеры, которые собирают другие конвертеры — цепочка, ветвление, кэш, — собраны в [Композиции](#композиция-конвертеров).

---

## Композиция конвертеров

| Конвертер | Описание |
|-----------|----------|
| `SequenceConverters<T>` | Последовательность конвертеров одного типа |
| `ComposeConverter<TFrom, TMid, TTo>` | Два конвертера через промежуточный тип |
| `ConditionalConverter<T>` | Ветвление по предикату `IConverter<T, bool>` |
| `SafeConverter<TFrom, TTo>` | Ловит исключение вложенного конвертера и подставляет авторское значение |
| `NullGuardConverter<TFrom, TTo>` | Фиксированный результат для `null` вместо передачи его дальше |
| `CachedConverter<TFrom, TTo>` | Запоминает последний результат, пока значение не изменилось |
| `PassthroughConverter<T>` | Явный no-op — читается как осознанная ветка, а не как незаполненное поле |

`SequenceConverters<T>` объединяет несколько конвертеров одного типа:

```csharp
// В Inspector:
// SequenceConverters<float> с двумя конвертерами:
// 1. ArithmeticNumberConverter (Multiply × 100)
// 2. [кастомный] ClampConverter (0, 100)
// Результат: 0.75f → 75f → 75f (clamped)
```

Все конвертеры в цепочке должны иметь одинаковый тип `T`. Когда типы по краям различаются, берите `ComposeConverter<TFrom, TMid, TTo>` — здесь обе части обязательны: типы не совпадают, и при пустой части возвращать нечего.

```csharp
// ComposeConverter<float, bool, Color>:
// first  = NumberToBoolConverter (GreaterThan 0.3)
// second = BoolToValueConverter<Color> (green / red)
// 0.75f → true → green
```

У `ConditionalConverter<T>` необязательны все три части, и пустая означает «оставить значение как есть», так что частично настроенный конвертер вырождается в `Passthrough`, а не в ошибку.

`CachedConverter<TFrom, TTo>` рассчитан на чистые конвертеры: биндеры пушат значение на каждое уведомление, а не на каждое изменение, поэтому аллоцирующий конвертер аллоцирует и на неизменившемся значении. Конвертер, который читает что-то помимо входа (например, текущую позицию компонента в сцене), в обёртке будет возвращать значение на момент последнего изменения входа.

> **Зачем `SafeConverter`.** Рассылка значений биндерам — обычный multicast: исключение внутри конвертера обрывает список подписчиков, и все биндеры, стоящие в очереди после текущего, значения не получают. Обёртка удерживает поломку внутри одного конвертера.

---

## Обработка ошибок

`ConverterFailureMode` описывает, что конвертер делает со значением, которое не может преобразовать — не с собственной неверной настройкой (о ней конвертер сообщает всегда):

| Режим | Поведение |
|-------|-----------|
| `ReturnFallback` | Вернуть авторское значение и сообщить об ошибке |
| `ReturnInput` | Вернуть входное значение без изменений и сообщить об ошибке. Конвертеры с разными типами входа и выхода так не могут и ведут себя как `ReturnFallback` |
| `Throw` | Бросить исключение |

Об ошибке сообщается при каждом появлении, а не один раз: конвертер, которому изредка приходит битая строка, иначе выглядел бы исправным.

---

## Конвертеры-ассеты

`ConverterAsset<TFrom, TTo>` — `ScriptableObject` с одним конвертером внутри; `ConverterAssetReference<TFrom, TTo>` — поле биндера, ссылающееся на такой ассет. Вместе они позволяют настроить преобразование один раз и переиспользовать его между сценами и префабами.

Готовые ассеты создаются через **Assets → Create → Aspid/MVVM/Converters**:

| Ассет | Преобразование |
|-------|---------------|
| `BoolConverterAsset` | `bool` → `bool` |
| `ColorConverterAsset` | `Color` → `Color` |
| `FloatConverterAsset` | `float` → `float` |
| `IntConverterAsset` | `int` → `int` |
| `StringConverterAsset` | `string?` → `string?` |
| `ObjectToStringConverterAsset` | `object?` → `string?` |
| `Vector2ConverterAsset` | `Vector2` → `Vector2` |
| `Vector3ConverterAsset` | `Vector3` → `Vector3` |

Пустой ассет (или ссылка без ассета) возвращает значение по умолчанию своего типа.

---

## Создание кастомного конвертера

```csharp
using System;
using Aspid.MVVM.StarterKit;

[Serializable]
public sealed class PercentConverter : IConverter<float, string>
{
    public string Convert(float value)
    {
        return $"{value * 100:F0}%";
    }
}
```

```csharp
// Конвертер с параметрами (сериализуемыми в Inspector):
[Serializable]
public sealed class ClampFloatConverter : IConverter<float, float>
{
    [SerializeField] private float _min;
    [SerializeField] private float _max = 1f;

    public float Convert(float value)
    {
        return Mathf.Clamp(value, _min, _max);
    }
}
```

> **Важно:** Класс должен быть `[Serializable]` для отображения в Inspector через `[SerializeReference]`.

---

## Использование в Inspector

1. На биндере (например, `TextBinder`) найдите поле **Converter**
2. Нажмите на выпадающий список (`[SerializeReference]`)
3. Выберите нужный конвертер (например, `ArithmeticNumberConverter`)
4. Настройте параметры конвертера

### Из кода

```csharp
// GenericFuncConverter — для привязки из кода
var converter = new GenericFuncConverter<float, string>(
    value => $"{value:P0}"
);

// То же короче — через расширение для Func
Func<float, string> format = value => $"{value:P0}";
var sameConverter = format.ToConvert();
```

---

## Специализированные интерфейсы

Для совместимости с Unity до 2023 (без поддержки generic SerializeReference) существуют типизированные интерфейсы:

- `IConverterFloat` = `IConverter<float, float>`
- `IConverterInt` = `IConverter<int, int>`
- `IConverterFloatToString` = `IConverter<float, string>`
- и другие кросс-типовые комбинации

В Unity 2023+ можно использовать `IConverter<T, T>` напрямую.

---

## См. также

- [Биндеры](06-binders.md) — использование конвертеров в биндерах
- [StarterKit](StarterKit/README.md) — готовые биндеры с поддержкой конвертеров
