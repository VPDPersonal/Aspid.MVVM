# Конвертеры

Конвертеры преобразуют значения при передаче от ViewModel к View, не изменяя сам ViewModel.

## Содержание

- [Обзор](#обзор)
- [IConverter\<TFrom, TTo\>](#iconvertertfrom-tto)
- [Встроенные конвертеры](#встроенные-конвертеры)
- [Цепочка конвертеров](#цепочка-конвертеров)
- [Создание кастомного конвертера](#создание-кастомного-конвертера)
- [Использование в Inspector](#использование-в-inspector)

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

### Логические

| Конвертер | Преобразование | Описание |
|-----------|---------------|----------|
| `NumberToBoolConverter` | числа → `bool` | Сравнение с порогом (>, <, ==, !=, >=, <=) |
| `ObjectNullToBoolConverter` | `object?` → `bool` | `null` check с инверсией |
| `StringEmptyToBoolConverter` | `string?` → `bool` | Пустая строка check |

```csharp
// NumberToBoolConverter:
// Threshold = 0, Comparison = GreaterThan
// 5 → true, 0 → false, -1 → false
```

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

### Функциональные

| Конвертер | Описание |
|-----------|----------|
| `GenericFuncConverter<TFrom, TTo>` | Обёртка над `Func<TFrom?, TTo?>` |
| `SequenceConverters<T>` | Цепочка конвертеров `T → T → ... → T` |

---

## Цепочка конвертеров

`SequenceConverters<T>` позволяет объединить несколько конвертеров:

```csharp
// В Inspector:
// SequenceConverters<float> с двумя конвертерами:
// 1. ArithmeticNumberConverter (Multiply × 100)
// 2. [кастомный] ClampConverter (0, 100)
// Результат: 0.75f → 75f → 75f (clamped)
```

Все конвертеры в цепочке должны иметь одинаковый тип `T`.

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
