---
title: Числовые конвертеры
type: converter
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Numbers/ArithmeticNumberConverter.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Numbers/NumberOperation.cs
tags:
  - converter
  - numbers
  - starterkit
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/converters/Number Converters.md
translated_at: 2026-05-31
---

# Числовые конвертеры

> Применяет арифметическую операцию (`+ - × ÷`) с фиксированным коэффициентом к числовому значению привязки, прозрачно связывая между собой `int`, `long`, `float` и `double`.

## Зачем это нужно

Данные ViewModel и единица отображения во View редко совпадают: исходное значение здоровья типа `int` может потребовать масштабирования для слайдера, а `float` — смещения перед отображением. Вместо того чтобы вручную писать одноразовый [[Converters|конвертер]] под каждый случай, `ArithmeticNumberConverter` декларативно закрывает распространённую потребность «масштаб/смещение». Поскольку он помечен `[Serializable]`, его можно настроить прямо в инспекторе Unity на binder — без изменения кода для перенастройки коэффициента.

## Как это работает

В семействе два типа:

| Тип | Роль |
|------|------|
| `ArithmeticNumberConverter` | Сам конвертер — операция + коэффициент |
| `NumberOperation` | enum: `Plus`, `Minus`, `Division`, `Multiply` |

Конвертер реализует **16 вариантов интерфейса `IConverter<TIn,TOut>`** по комбинациям `int/long/float/double`. Это означает, что один экземпляр способен удовлетворить binder, ожидающий любую из этих числовых пар вход/выход, поэтому [[Runtime Binding Resolution]] может подхватить его независимо от числового типа привязываемого члена.

Реализация намеренно сведена к единой точке: каждый вариант приводит значение к `double`, делегирует единственному методу `IConverter<double, double>.Convert`, а затем приводит результат обратно к целевому типу. Этот основной метод — единственное место, где живёт switch по `NumberOperation`: `value + _coefficient`, `value - _coefficient`, `value * _coefficient` или защищённое деление.

**Деление на ноль безопасно:** если `_coefficient == 0`, метод `Divide` записывает `Debug.LogError` и возвращает входное значение без изменений, вместо того чтобы бросить исключение или выдать `Infinity`.

Два сериализуемых поля, `_operation` и `_coefficient`, обёрнуты в защитные директивы `#if UNITY_2022_1_OR_NEWER` `[SerializeField]`, чтобы тип компилировался и вне Unity. Предусмотрены как конструктор без параметров (для сериализации), так и конструктор `(operation, coefficient)` (для кода).

> Примечание: все методы интерфейса реализованы **явно**, поэтому обращаться к конвертеру нужно через соответствующий интерфейс `IConverter<,>` — методы `Convert` не видны на конкретном типе.

## Ключевые связи

- Используется везде, где binder объявляет слот числового конвертера — см. [[Slider Binders]], [[Scrollbar Binders]], [[Text Binders]] и более широкий [[Binders Catalog]].
- Родственные семейства конвертеров: [[Bool Converters]], [[String Converters]], [[Specific Converters]].
- Связан с привязкой через контракт `IConverter<TIn,TOut>` и однонаправленные потоки [[BindMode]]; судя по устройству, рассчитан на пути отображения значений (чтение).

## Источник

- `ArithmeticNumberConverter.cs`, `NumberOperation.cs` — `StarterKit/Runtime/Converters/Numbers/`
