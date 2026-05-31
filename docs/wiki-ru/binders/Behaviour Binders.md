---
title: Binder-ы Behaviour
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Behaviours/Enabled/BehaviourEnabledBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Behaviours/Enabled/Mono/BehaviourEnabledMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Behaviours/Enabled/Mono/BehaviourEnabledByBindMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Behaviours/Enabled/Mono/BehaviourEnabledEnumMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Behaviours/Enabled/Mono/BehaviourEnabledEnumGroupMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Behaviours/OneWayToSource/Mono/BehaviourToSourceMonoBinder.cs
tags: [binder, starterkit, unity, behaviour, bool, enum]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/Behaviour Binders.md
translated_at: 2026-05-31
---

# Binder-ы Behaviour

> Binder-ы из StarterKit, которые переключают флаг `Behaviour.enabled` любого Unity-компонента по значению из ViewModel — самый простой способ включить или выключить скрипт, коллайдер или компонент через MVVM.

## Что биндится

Каждый вариант в конечном счёте управляет `Behaviour.enabled` (значением типа `bool`). Поскольку `Behaviour` является базовым типом почти для всех Unity-компонентов (скрипты, `Collider`, подтипы `Renderer` и т.д.), это семейство служит универсальным переключателем вкл/выкл. Один обратный вариант, наоборот, отправляет ссылку на `Behaviour` *обратно* в ViewModel. Целевое свойство всегда такое:

```csharp
get => Target.enabled;
set => Target.enabled = value;
```

## Таблица семейства

| Binder | Базовый класс | Направление / значение |
|--------|-----------|-------------------|
| `BehaviourEnabledBinder` | `TargetBoolBinder<Behaviour>` | bool на вход (Plain, сериализуемый, без MonoBehaviour) |
| `BehaviourEnabledMonoBinder` | `ComponentBoolMonoBinder<Behaviour>` | bool на вход (на основе компонента) |
| `BehaviourEnabledByBindMonoBinder` | [[Mono Binders\|MonoBinder]], `IAnyBinder` | OneTime; включает сам себя по *наличию привязки* |
| `BehaviourEnabledEnumMonoBinder` | `EnumMonoBinder<Behaviour, bool>` | enum → bool |
| `BehaviourEnabledEnumGroupMonoBinder` | `EnumGroupMonoBinder<Behaviour, bool>` | enum → bool для группы |
| `BehaviourToSourceMonoBinder` | `ComponentToSourceMonoBinder<Behaviour>` | OneWayToSource (отправляет компонент наружу) |

## Оси вариантов

- **Plain vs Mono** — `BehaviourEnabledBinder` — это `[Serializable]` plain-binder, который встраивается как поле; форма `MonoBinder` — это компонент, добавляемый на объект (`AddComponentMenu` / `AddBinderContextMenu`). См. [[Binder Base Classes]] и [[Mono Binders]].
- **Инверсия bool** — обе bool-формы принимают `isInvert`, инвертируя значение перед его применением.
- **EnabledByBind** — *не* передаёт значение; он устанавливает собственный `enabled` в зависимости от того, существует ли подходящая привязка на ViewModel (`IsBound`), с возможной инверсией. `SetValue<T>` ничего не делает. Полезен для отображения/скрытия узла, когда ViewModel действительно предоставляет член.
- **Enum / EnumGroup** — вычисляют `bool` из привязанного enum-значения (см. настройку [[BindMode]] / конвертеров); форма *Group* применяет результат к каждому `Behaviour` из сериализованного списка.
- **OneWayToSource** — `BehaviourToSourceMonoBinder` разворачивает поток, записывая закэшированный компонент в ViewModel.

В этой папке нет варианта «Switcher» (в отличие от некоторых других семейств); ближайший аналог — EnabledByBind.

## Особенности поведения

- `BehaviourEnabledBinder` и `BehaviourEnabledMonoBinder` отвергают `BindMode.TwoWay` (binder выбрасывает исключение / переопределяет режимы); Mono-форма дополнительно поддерживает `OneWayToSource`, отправляя текущее значение `enabled` обратно при привязке.
- `BehaviourEnabledByBindMonoBinder` помечен `[BindModeOverride(BindMode.OneTime)]` и пересчитывает значение при `OnEnable`, `OnBound` и `OnUnbound`.

## Исходный код

`StarterKit/Unity/Runtime/Binders/Behaviours/` — `Enabled/` (bool, enum, enum-group, by-bind) и `OneWayToSource/`. Базовые классы описаны в [[Binder Base Classes]]; поток привязки — в [[Data Binding]] и [[Runtime Binding Resolution]]. Все семейства можно просмотреть через [[Binders Catalog]].
