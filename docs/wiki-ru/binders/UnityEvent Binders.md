---
title: UnityEvent-биндеры
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/UnityEvents/Mono
tags: [binder, starterkit, unityevent, bridge]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/UnityEvent Binders.md
translated_at: 2026-05-31
---

# UnityEvent-биндеры

> Запасной выход: пробрасывает любое связанное значение ViewModel в сериализуемый `UnityEvent` Unity, чтобы дизайнеры могли настраивать эффекты прямо в Inspector без написания собственного биндера.

## Зачем это нужно

Большинство биндеров нацелены на конкретный компонент (Text, Slider, Animator). Когда подходящего готового биндера нет — или дизайнеру просто нужно при изменении значения запустить произвольных слушателей (проиграть звук, плавно скрыть панель, запустить анимацию), — эти биндеры пересылают связанное значение в сериализуемый `UnityEvent`. Они превращают привязку данных в привычную для Unity-разработчиков настройку событий через Inspector.

## Как это работает

Каждый вариант — это [[Mono Binders|MonoBinder]], предоставляющий сериализуемое поле `UnityEvent<T>` (`_set`) и необязательный [[Converters|конвертер]] с атрибутом `[SerializeReference]`. Когда связанное значение разрешается, `SetValue` применяет конвертер (если он задан) и вызывает событие: `_set?.Invoke(_converter?.Convert(value) ?? value)`. Строковый вариант дополнительно принимает `CultureInfoMode` для форматирования чисел. Поскольку это MonoBinder'ы, они участвуют в обычной [[View Initialization|инициализации View]] и связываются в том [[BindMode]], который объявляет их целевой член — практически всегда [[Data Binding|OneWay]].

## Семейство

| Вариант | Цель / событие | Особенности поведения |
|---------|----------------|------------------------|
| Value-биндеры (Bool, Int, Long, Float, Double, String, Color, Vector2, Vector3, Quaternion) | `UnityEvent<T>` | Вызывают событие с преобразованным значением; число→строка через культуру |
| Enum | `UnityEvent` (без аргумента) | Наследует `EnumMonoBinder<UnityEvent>`; вызывает событие, сопоставленное связанному члену enum |
| Switcher | `UnityEvent` (без аргумента) | Наследует `SwitcherMonoBinder<UnityEvent>`; выбирает одно из двух событий по связанному `bool` |
| Number Condition | `UnityEvent<bool>` | Преобразует числовое значение в `bool` и вызывает событие с результатом |
| Number Condition Switcher | два `UnityEvent` | Преобразует число→`bool`, вызывает `_trueSet` или `_falseSet` |
| Bool By Bind | `UnityEvent<bool>` | Игнорирует значение; вызывается при bind/unbind с `IsBound` (с возможной инверсией) |

## Оси вариантов

- **Plain vs Mono** — здесь существуют только варианты MonoBinder; plain-варианта (не-компонента) нет.
- **Switcher / Enum / EnumGroup** — Switcher (bool→одно из двух событий без аргумента) и Enum (enum→сопоставленное событие) переиспользуют общие [[Binder Base Classes|базовые классы биндеров]] `SwitcherMonoBinder<T>` / `EnumMonoBinder<T>`. Варианта EnumGroup в этой папке нет.
- **Condition** — семейство NumberCondition прогоняет числовой ввод через конвертер float→bool, либо выдавая `bool`, либо ветвясь между двумя событиями.
- **OneWayToSource** — отсутствует. Семейство работает только на отправку (ViewModel → UnityEvent) и никогда не пишет обратно. `BoolByBind` ближе всего к хуку жизненного цикла, срабатывая исключительно по состоянию привязки.

Всё семейство находится в `Add General Binder/UnityEvent/...` в меню редактора (через `AddComponentMenu` / `AddBinderContextMenu`), а value-варианты объявляют свой связываемый тип через `AddBinderContextMenuByType`.

## Источник

`Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/UnityEvents/Mono/` — см. также [[Binder Base Classes]], [[Mono Binders]], [[Converters]], [[Binders Catalog]].
