---
title: Биндеры VirtualizedList
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/VirtualizedLists/ItemSource/VirtualizedListItemSourceBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/VirtualizedLists/ItemSource/Mono/VirtualizedListItemSourceMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/VirtualizedLists/OneWayToSource/Mono/VirtualizedListToSourceMonoBinder.cs
tags: [binders, starterkit, virtualizedlist, collections]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/VirtualizedList Binders.md
translated_at: 2026-05-31
---

# Биндеры VirtualizedList

> Биндеры из StarterKit, которые передают компоненту `VirtualizedList` наблюдаемый список дочерних `IViewModel` с опциональной фильтрацией и сортировкой.

## Зачем это нужно

`VirtualizedList` отрисовывает только видимый срез большой коллекции, переиспользуя представления элементов по мере прокрутки пользователем. Эти биндеры связывают данный компонент со свойством ViewModel типа `IReadOnlyList<IViewModel>`, так что каждая дочерняя модель представления управляет одним элементом списка. Это сохраняет дешевизну больших списков и позволяет слою View оставаться декларативным. Об их невиртуализированных аналогах см. [[Collection Binders]].

## Таблица семейства

| Биндер | Базовый класс | Цель / привязывает | Режим |
| --- | --- | --- | --- |
| `VirtualizedListItemSourceBinder` | `TargetBinder<VirtualizedList>` | `IReadOnlyList<IViewModel>` -> `VirtualizedList.ItemsSource` | OneWay / OneTime (не TwoWay) |
| `VirtualizedListItemSourceMonoBinder` | `ComponentMonoBinder<VirtualizedList>` | то же, в виде MonoBehaviour | OneWay / OneTime |
| `VirtualizedListToSourceMonoBinder` | `ComponentToSourceMonoBinder<VirtualizedList>` | возвращает текущее значение обратно в ViewModel | OneWayToSource |

## Оси вариантов

- **Plain против Mono.** Обычный `VirtualizedListItemSourceBinder` — это `[Serializable]` биндер уровня поля, создаваемый в коде; вариант `MonoBinder` — это `MonoBehaviour` ([[Mono Binders]]), настраиваемый в инспекторе, с записями `[AddComponentMenu]` / `[AddBinderContextMenu]` в разделе `Aspid/MVVM/Binders/UI/VirtualizedList/`. Оба реализуют `IBinder<IReadOnlyList<IViewModel>>` и используют идентичную логику `SetValue`.
- **OneWayToSource.** `VirtualizedListToSourceMonoBinder` — это пустой подкласс `ComponentToSourceMonoBinder<VirtualizedList>`; поведение привязки к источнику целиком находится в этом базовом классе ([[Binder Base Classes]]).
- В этом семействе **нет** вариантов Switcher / Enum / EnumGroup (в отличие от большинства других категорий биндеров).

## Примечательное поведение

- **Фильтр + компаратор.** Оба биндера ItemSource предоставляют `[SerializeReference]` поля `_filter` и `_comparer`. Когда задано хотя бы одно из них, `SetValue` оборачивает входящий список в `FilteredList<IViewModel>` (из внешнего пакета `Aspid.Collections.Observable.Filtered` — см. [[External Dependencies]]) перед присвоением `ItemsSource`. Обёртка освобождается при перепривязке и в `OnUnbound`, который также обнуляет `ItemsSource`.
- **Псевдонимы для версий Unity.** `Filter` / `Comparer` разрешаются в обобщённые `ICollectionFilter<IViewModel>` / `ICollectionComparer<IViewModel>` на Unity 2023.1+, откатываясь к необобщённым интерфейсам на более старых версиях.
- Метод `SetValue` у MonoBinder несёт атрибут `[BinderLog]`, поэтому его присваивания отображаются в [[Debug Binders|логировании биндеров]] (выведено из имени атрибута).
- `TwoWay` отклоняется при создании (`mode.ThrowExceptionIfTwo()`); см. [[BindMode]].

## Исходный код

`StarterKit/Unity/Runtime/Binders/VirtualizedLists/` — `ItemSource/` (plain + `Mono/`) и `OneWayToSource/Mono/`. Скомпилировано в [[Committed DLLs|закоммиченную сборку StarterKit]]. Связанное: [[StarterKit ViewModels]], [[IBinder]], [[IViewModel]].
