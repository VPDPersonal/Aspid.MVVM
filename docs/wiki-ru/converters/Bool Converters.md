---
title: Bool-конвертеры
type: converter
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Bools/NumberToBoolConverter.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Bools/ObjectNullToBoolConverter.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Bools/StringEmptyToBoolConverter.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Bools/Comparisons.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Specific/Bool/IConverterObjectToBool.cs
tags: [converter, bool, starterkit]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/converters/Bool Converters.md
translated_at: 2026-05-31
---

# Bool-конвертеры

> Конвертеры из StarterKit, которые превращают исходное значение, не являющееся `bool` (число, ссылку на объект или строку), в `bool`, чтобы bindable-член мог управлять binder'ом, ожидающим булево значение (например, состояние enabled/visible/interactable).

## Зачем это нужно

ViewModel часто предоставляет число (`Health`), ссылку (`SelectedItem`) или строку (`SearchText`), но целевому binder'у нужен `bool` («интерактивна ли кнопка?», «показать ли эту панель?»). Вместо того чтобы добавлять в ViewModel отдельное bool-свойство под каждое правило UI, вы вешаете на binder переиспользуемый сериализуемый конвертер. Каждый конвертер помечен `[Serializable]` с конфигурацией через `[SerializeField]`, поэтому его порог/инверсию можно редактировать в Unity Inspector. Обзор семейства смотрите в [[Converters]].

## Как это работает

Каждый класс реализует один или несколько типизированных интерфейсов конвертеров из набора [[Specific Converters|Specific Converter]] (`IConverter<TFrom, bool>`):

| Конвертер | Реализует | Правило |
|-----------|------------|------|
| `NumberToBoolConverter` | `IConverter{Float,Double,Int,Long}ToBool` | `value` против настроенного `_value` через операцию `Comparisons` |
| `ObjectNullToBoolConverter` | `IConverterObjectToBool` | `value is null` (при необходимости с инверсией) |
| `StringEmptyToBoolConverter` | `IConverterStringToBool` | `string.IsNullOrEmpty(value)` (при необходимости с инверсией) |

`NumberToBoolConverter` — самый функциональный: он принимает четыре числовых типа, но выполняет все сравнения в `double` (`Compare(double)`), так что большие величины `long`/`double` сравниваются точно, без округления float. Операция берётся из перечисления `Comparisons`: `Equal`, `Inequality`, `LessThan`, `GreaterThan`, `LessThanOrEqual`, `GreaterThanOrEqual`. `Equal`/`Inequality` используют допуск `Approximately` (относительный эпсилон), а не точное `==`, что позволяет избежать ложноотрицательных результатов из-за дрейфа float.

Два конвертера на основе булева флага имеют одинаковую структуру: единственное поле `_isInvert` с `[SerializeField]`, конструктор по умолчанию (`isInvert: false`) и конструктор с одним аргументом. Результат — `_isInvert ? !x : x`, поэтому `ObjectNullToBoolConverter(isInvert: true)` читается как «true, когда установлено», а `StringEmptyToBoolConverter(isInvert: true)` — как «true, когда не пусто».

## Ключевые связи

- Используется binder'ами, которые принимают источник `bool` (состояние Toggle/CanvasGroup/активность GameObject), подключёнными через [[IBinder]] и [[Data Binding]].
- Родственные семейства: [[Number Converters]], [[String Converters]], [[Specific Converters]].
- Перечисление `Comparisons` локально для этой папки, но концептуально переиспользуется другими конвертерами сравнения (по-видимому, это канонический набор операций сравнения).

## Источник

`StarterKit/Runtime/Converters/Bools/` — `NumberToBoolConverter.cs`, `ObjectNullToBoolConverter.cs`, `StringEmptyToBoolConverter.cs`, `Comparisons.cs`. Интерфейсы в `Converters/Specific/Bool/`.
