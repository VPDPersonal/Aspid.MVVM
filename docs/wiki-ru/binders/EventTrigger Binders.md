---
title: Биндеры EventTrigger
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/EventTriggers/Command/EventTriggerCommandBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/EventTriggers/Command/Mono/EventTriggerCommandMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/EventTriggers/OneWayToSource/Mono/EventTriggerToSourceMonoBinder.cs
tags: [binder, starterkit, eventtrigger, command]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/EventTrigger Binders.md
translated_at: 2026-05-31
---

# Биндеры EventTrigger

> Связывают события указателя/UI компонента Unity `EventTrigger` (PointerDown, Drag, Scroll и т. д.) с [[IRelayCommand|relay-командами]] ViewModel — позволяя View реагировать на сырые данные события без написания обработчиков событий в MonoBehaviour.

## Зачем это нужно

`EventTrigger` в Unity предоставляет множество событий `EventSystem` (`PointerEnter`, `Drag`, `Submit`...), которые не выставляются наружу кнопками/тогглами. Эти биндеры позволяют единственному настроенному `EventTriggerType` маршрутизироваться в [[Relay Commands|relay-команду]], так что обработка ввода живёт в ViewModel, а не в связующем коде. См. [[Relay Commands]] и [[Bindable Members]].

## Семейство

| Вариант | Базовый класс | Что связывает |
|---|---|---|
| `EventTriggerCommandBinder` | `TargetBinder<EventTrigger>` (Plain) | выполняет команду при срабатывании события |
| `EventTriggerCommandMonoBinder` | `ComponentMonoBinder<EventTrigger>` (Mono) | то же самое, в виде сериализуемого MonoBehaviour |
| `EventTriggerCommandBinder<T1..T4>` / `...MonoBinder<T1..T4>` | абстрактный Plain / Mono | добавляет 1–4 дополнительных передаваемых параметра |
| `EventTriggerToSourceMonoBinder` | `ComponentToSourceMonoBinder<EventTrigger>` | OneWayToSource: проталкивает компонент в VM |

## Оси вариантов

- **Plain vs Mono** — Plain (`[Serializable]`) живёт внутри [[View]] как сериализуемое поле; Mono — это самостоятельный компонент (`[AddComponentMenu]` "Aspid/MVVM/Binders/.../EventTrigger"). Оба используют идентичную логику событий/команд; см. [[Binder Base Classes]] и [[Mono Binders]].
- **Command** — основное семейство. Каждый неуниверсальный биндер реализует три перегрузки [[IBinder]]: `IRelayCommand` (без аргументов), `IRelayCommand<BaseEventData>` (получает сработавшее событие) и `IRelayCommand<EventTriggerType>` (получает настроенный тип). Универсальные варианты `<T1..T4>` являются **абстрактными** — необходимо создать подкласс с конкретным sealed-типом — и добавляют сериализованные `Param1..Param4` в начало вызова команды. Все три подходящих вида команд срабатывают вместе при триггере.
- **OneWayToSource** — `EventTriggerToSourceMonoBinder` пустой; он наследует базовое поведение по отправке закэшированного `EventTrigger` в ViewModel. Используйте его, когда VM нужен сам компонент, а не реакция на события.
- **Вариантов Switcher / Enum / EnumGroup в этой папке нет** (в отличие от некоторых других семейств биндеров).

## Примечательное поведение

- При привязке (`OnBound`) биндер создаёт `EventTrigger.Entry` с `eventID = _event`, подключает его коллбэк и добавляет его в `Target.triggers`. При отвязке он удаляет запись и передаёт `null` в каждую перегрузку `SetValue`, чтобы отсоединить команды.
- Необязательный `ICanExecuteView _customInteractable` (выпадающий список `[SerializeReference]`) отражает состояние `CanExecute` — отключая UI, когда команда не может выполниться. Он подписывается на `CanExecuteChanged`.
- `TwoWay` отвергается: конструктор вызывает `mode.ThrowExceptionIfTwo()`. См. [[BindMode]].
- Mono-варианты заново вычисляют `CanExecute` в `OnValidate`, поэтому изменения в редакторе обновляют состояние интерактивности вживую.

## Исходный код

- `.../Binders/EventTriggers/Command/EventTriggerCommandBinder.cs`
- `.../Command/Mono/EventTriggerCommandMonoBinder.cs`
- `.../OneWayToSource/Mono/EventTriggerToSourceMonoBinder.cs`

Связанное: [[Button Binders]], [[UnityEvent Binders]], [[Binders Catalog]].
