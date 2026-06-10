---
title: Обобщённые binder'ы
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Generics
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Generics
tags:
  - binders
  - starterkit
  - generics
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/Generic Binders.md
translated_at: 2026-05-31
---

# Обобщённые binder'ы

> Переиспользуемые, не привязанные к конкретной цели реализации [[IBinder]], которые соединяют значение ViewModel типа `T` с любым делегатом-сеттером — запасной вариант, когда ни один специализированный binder (Text, Image и т. д.) не подходит.

## Зачем это нужно

Большинство binder'ов из StarterKit оборачивают один компонент Unity. Обобщённые binder'ы переворачивают этот подход: вместо жёсткой привязки к цели они принимают сеттер (и при необходимости геттер/событие) при создании. Это позволяет привязкам, создаваемым из кода — или новым кастомным binder'ам — связать [[Bindable Members|bindable-член]] с произвольным свойством без написания отдельного класса. Они являются строительным блоком, на который опираются другие binder'ы и семейство [[Caster Binders]].

## Таблица семейства

| Вариант | Реализует | Направление |
|---------|-----------|-------------|
| `GenericOneWayBinder<T>` | `IBinder<T>` | ViewModel → View |
| `GenericOneTimeBinder<T>` | (подкласс OneWay) | один раз, затем останавливается |
| `GenericTwoWayBinder<T>` | `IBinder<T>`, `IReverseBinder<T>` | в обе стороны |
| `GenericOneWayToSourceBinder<T>` | `IReverseBinder<T>` | View → ViewModel |
| `GenericCasterBinder<TFrom,TTo>` | `IBinder<TFrom>` | ViewModel → View, с преобразованием |

Каждый из них находится в `Aspid.MVVM.StarterKit` и наследуется от [[Binder Base Classes|Binder]].

## Оси вариантов

- **Plain против Unity.** Существуют два параллельных набора. Plain-набор (`GenericOneWayBinder<T>`, …) принимает сеттер `System.Action<T?>`. Unity-набор (`UnityGenericOneWayBinder<T>`, …) идентичен, но принимает `UnityEngine.Events.UnityAction<T?>`, что удобно для колбэков в стиле событий Unity. Конструкторы Unity-вариантов OneWay/OneTime являются `protected` (рассчитаны на наследование), а plain-варианты — `public`.
- **Single против привязки к цели.** Каждый тип поставляется в двух арностях: `<T>` захватывает сеттер напрямую, а `<TTarget, T>` хранит `TTarget target` и передаёт его первым аргументом сеттера. Форма с привязкой к цели позволяет избежать выделения замыкания при использовании сеттеров на основе групп методов компонента — это осознанная оптимизация по выделению памяти (согласно XML-документации).
- **Арности caster'ов.** `GenericCasterBinder` добавляет аргумент [[Converters|IConverter]] и получает перегрузку `<TTarget, TFrom, TTo>` наряду с `<TFrom, TTo>`.

## Особенности поведения

- **TwoWay бросает исключение при неправильном использовании.** OneWay/OneTime/Caster вызывают `mode.ThrowExceptionIfTwo()` — передача [[BindMode|BindMode.TwoWay]] приводит к исключению. `GenericOneTimeBinder` просто жёстко задаёт `BindMode.OneTime`.
- **Обратный поток через `initialize`.** `GenericTwoWayBinder` и `GenericOneWayToSourceBinder` принимают `Action<Action<T>> initialize`; они передают свой внутренний колбэк `OnValueChanged` в это действие, чтобы вызывающая сторона зарегистрировала его на событии со стороны View. Они вызывают `ValueChanged`, чтобы передать значения обратно.
- **Хуки bound/unbound.** Оба binder'а, поддерживающие обратное направление, принимают опциональные фабрики `onBoundValueChanged` / `onUnboundValueChanged`; их результаты передаются во ViewModel при `OnBound` / `OnUnbinding`. Конструктор `OneWayToSource` без параметров требует хотя бы одну из них.

Это binder'ы уровня кода/времени выполнения (не компоненты MonoBehaviour); их эквиваленты, доступные в инспекторе, описаны на страницах отдельных компонентов в [[Binders Catalog]]. Оси «Switcher / Enum / EnumGroup / MonoBinder» из брифа в этой папке **отсутствуют** — эти паттерны встречаются в других семействах binder'ов.

## Источник

- `StarterKit/Runtime/Binders/Generics/` — plain-варианты (`System.Action`).
- `StarterKit/Unity/Runtime/Binders/Generics/` — варианты на `UnityAction`.
- `XmlExampleDoc-Generics-1.1.0.xml`, `XmlExampleDoc-UnityGenerics-1.1.0.xml` — включения `<example>`.

См. также [[Data Binding]], [[Runtime Binding Resolution]], [[Caster Binders]], [[Mono Binders]].
