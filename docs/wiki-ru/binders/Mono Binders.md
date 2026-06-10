---
title: Mono Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Mono/ComponentProperty/ComponentMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Mono/ComponentProperty/ComponentColorMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Mono/Switchers/SwitcherMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Mono/Enums/EnumMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Mono/EnumGroups/EnumGroupMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Mono/ComponentToSourceMonoBinder.cs
tags: [binders, starterkit, mono, abstract-base]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/Mono Binders.md
translated_at: 2026-05-31
---

# Mono Binders

> Переиспользуемый абстрактный каркас, от которого наследуются конкретные binder'ы из StarterKit — они отображают обобщённое привязанное значение на свойство Unity `Component`, так что вам редко приходится писать обвязку для привязки вручную.

Это не «листовое» семейство binder'ов, которое вы вешаете прямо на GameObject; это набор обобщённых базовых классов, который расширяет почти каждый конкретный binder из StarterKit ([[Text Binders]], [[Image Binders]], [[Slider Binders]] и т. д.). Понимание описанных здесь осей объясняет, как устроен весь [[Binders Catalog]].

## Таблица семейства

| База | Привязывает | Что делает |
|------|-------------|------------|
| `ComponentMonoBinder<TComponent>` | (хэндл компонента) | Кэширует целевой `Component`; корень семейства свойств. |
| `ComponentMonoBinder<TComponent, TProperty>` | `TProperty` | Читает/записывает одно свойство компонента через абстрактные get/set `Property`. |
| `ComponentMonoBinder<TComponent, TProperty, TConverter>` | `TProperty` | То же, но с опциональным сериализуемым [[Converters\|конвертером]]. |
| `SwitcherMonoBinder<…>` | `bool` | Выбирает одно из двух сериализуемых значений по привязанному bool. |
| `EnumMonoBinder<…>` | `Enum` | Сопоставляет привязанный enum со значением через таблицу `EnumValues<T>`. |
| `EnumGroupMonoBinder<…>` | `Enum` | Применяет «selected» к совпавшему элементу, «default» — к остальным. |
| `ComponentToSourceMonoBinder<TComponent>` | (обратно) | Передаёт ссылку на компонент обратно во ViewModel. |

## Оси вариаций

- **Простой `MonoBinder` против `ComponentMonoBinder`.** Любая база в конечном счёте наследуется от [[Binder Base Classes\|MonoBinder]] (`MonoBehaviour`, управляющий жизненным циклом [[IBinder]]). Базы с приставкой `Component` дополнительно кэшируют типизированный `Component`, чтобы подклассы работали со свойством; простые перегрузки `MonoBinder` (например, `SwitcherMonoBinder<T>`, `EnumMonoBinder<T>`) нацелены на что угодно, а не на компонент.
- **Property против Switcher против Enum против EnumGroup.** Базы Property просто пробрасывают одно значение насквозь. Switcher преобразует `bool` в одно из двух сериализуемых значений `T`. Enum разрешает `System.Enum` через сериализуемую таблицу соответствий. EnumGroup распределяет enum по множеству элементов, вызывая `SetSelectedValue`/`SetDefaultValue`.
- **Перегрузка с конвертером.** Каждое семейство добавляет дополнительный обобщённый параметр `TConverter : IConverter<T, T>`, который, будучи назначен через выпадающий список `[SerializeReference]`, преобразует значение перед его применением (EnumGroup несёт отдельные конвертеры default/selected).
- **OneWayToSource / обратное направление.** `ComponentMonoBinder<TComponent, TProperty>` также реализует `IReverseBinder<TProperty>`; в режиме [[BindMode]] `OneWayToSource` при привязке он вызывает `ValueChanged`, чтобы инициализировать ViewModel. `ComponentToSourceMonoBinder` работает только в обратном направлении — он отправляет обратно при привязке саму закэшированную ссылку на компонент.

## Заметное поведение

- Конкретные типизированные базы (`ComponentColorMonoBinder<T>`, `ComponentFloat…` и т. д.) просто фиксируют дженерики и добавляют соответствующий интерфейс binder'а (`IColorBinder` и т. д.). Они находятся в `ComponentProperty/`, `Switchers/`, `Enums/`, `EnumGroups/` в вариантах `Bool/Color/Float/Int/String/Vector3`.
- Псевдонимы типов конвертеров переключаются между обобщённым `IConverter<,>` (Unity 2023.1+) и устаревшими типизированными интерфейсами через `#if`.
- `[BindModeOverride]` сужает набор режимов, которые принимает база; `[BinderLog]` на `SetValue` — это инструментирование для профилирования (см. [[Runtime Binding Resolution]]).

## Источник

`Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Mono/` — `ComponentProperty/`, `Switchers/`, `Enums/`, `EnumGroups/`, `ComponentToSourceMonoBinder.cs`. Корень жизненного цикла: [[Binder Base Classes]].
