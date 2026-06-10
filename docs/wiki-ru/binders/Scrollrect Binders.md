---
title: Биндеры Scrollrect
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Scrollrects/Command/ScrollRectCommandBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Scrollrects/Command/Mono/ScrollRectCommandMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Scrollrects/OneWayToSource/Mono/ScrollRectToSourceMonoBinder.cs
tags: [binder, starterkit, ui, scrollrect, command]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/Scrollrect Binders.md
translated_at: 2026-05-31
---

# Биндеры Scrollrect

> Связывает Unity-компонент `ScrollRect` с ViewModel: вызывает команду при каждом скролле или передаёт живую ссылку на `ScrollRect` обратно в ViewModel.

## Зачем это нужно

В отличие от биндеров значений (Slider, Toggle), которые проталкивают число или булево, у `ScrollRect` нет единственного связываемого «значения», за которым имело бы смысл наблюдать в одностороннем режиме. Обычно нужно *реагировать* на прокрутку. Поэтому это семейство **ориентировано на команды**: оно передаёт текущую позицию скролла в [[IRelayCommand]] на событие `onValueChanged`. Здесь нет вариантов `Switcher`, `Enum` или `EnumGroup` (эти оси имеют смысл только для биндеров, которые *устанавливают* дискретное состояние) — что делает это семейство одним из самых небольших.

## Семейство

| Биндер | Тип связывания | Направление |
|--------|-----------|-----------|
| `ScrollRectCommandBinder` (+ `<T>`, `<T1,T2>`, `<T1,T2,T3>`) | `IRelayCommand<Vector2/Vector3 [,T...]>` | View в ViewModel (команда) |
| `ScrollRectCommandMonoBinder` (+ обобщённые абстрактные варианты) | то же | View в ViewModel (команда) |
| `ScrollRectToSourceMonoBinder` | `ScrollRect` | OneWayToSource |

## Оси вариантов

- **Plain против Mono** — `ScrollRectCommandBinder` помечен `[Serializable]` и наследуется от [[Binder Base Classes|TargetBinder<ScrollRect>]]; он создаётся в коде и вкладывается внутрь хост-[[View]]. `ScrollRectCommandMonoBinder` — это `MonoBehaviour` (`ComponentMonoBinder<ScrollRect>`, см. [[Mono Binders]]), который вешается на тот же GameObject, что и `ScrollRect`, и доступен через `[AddComponentMenu]`. Оба используют идентичную логику команды.
- **Перегрузки по арности** — `<T>`, `<T1,T2>`, `<T1,T2,T3>` добавляют сериализуемые поля `Param`, передаваемые после позиции. Каждый принимает команду либо `Vector2`, либо `Vector3`; побеждает та, что связана, а позиция приводится к нужному типу. Обобщённые арности Mono являются `abstract` — вы наследуетесь от них, чтобы зафиксировать конкретные `T` (ограничение сериализации Unity).
- **OneWayToSource** — `ScrollRectToSourceMonoBinder` — единственный некомандный вариант: запечатанный `ComponentToSourceMonoBinder<ScrollRect>`, который проталкивает саму ссылку на `ScrollRect` в ViewModel ([[BindMode]] OneWayToSource), позволяя бизнес-логике управлять прокруткой.

## Заметное поведение

- При связывании подписывается на `ScrollRect.onValueChanged`; при отвязке удаляет слушателя и отсоединяет команду (вызывает `SetValue(null)`).
- Аргументом команды является `ScrollRect.normalizedPosition` (0..1 по каждой оси), передаваемый как `Vector2` или приводимый к `Vector3`.
- `BindMode.TwoWay` отклоняется при создании (`mode.ThrowExceptionIfTwo()`) — команды скролла по своей природе однонаправлены.
- Необязательный `ICanExecuteView _interactable` отражает `CanExecuteChanged` команды, так что UI может отключаться, когда прокрутка не разрешена.

## Исходники

`StarterKit/Unity/Runtime/Binders/Scrollrects/`. См. [[Binders Catalog]], [[Relay Commands]], [[Scrollbar Binders]].
