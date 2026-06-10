---
title: Binder'ы коллекций
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Collections/CollectionBinderBase.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Collections/Lists/ObservableListBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Collections/Dictionaries/ObservableDictionaryBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Collections/ObservableLists/ObservableListMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Collections/Collections/ViewModel/CollectionViewModelBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Collections/ObservableLists/ViewModel/ObservableListViewModelBinder.cs
tags: [binder, collection, observable, starterkit]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/Collection Binders.md
translated_at: 2026-05-31
---

# Binder'ы коллекций

> Семейство binder'ов, которое превращает привязанную коллекцию (observable-список, словарь или коллекцию только для чтения) в живой вывод во View — создавая, переиспользуя и удаляя дочерние View по мере того, как элементы добавляются, удаляются, заменяются, перемещаются или сбрасываются.

## Что он привязывает

Эти binder'ы реализуют [[IBinder]]`<IReadOnlyCollection<T>>` (а также варианты для списков и словарей) вместо одного значения. Привязываемый член — это коллекция на [[ViewModel]]; обычно observable-коллекция из `tech.aspid.collections` (см. [[External Dependencies]]). Binder подписывается на событие `CollectionChanged` этой коллекции и пересылает каждое изменение во View. При отвязке/освобождении он отписывается, чтобы предотвратить утечки обработчиков.

## Общие базовые классы

Все варианты наследуются от небольшого набора абстрактных баз, которые владеют логикой подписки и предоставляют хуки `protected abstract` — `OnAdded`, `OnRemoved`, `OnReplace`, `OnMove`, `OnReset` — для реализации в подклассах (см. [[Binder Base Classes]]):

| База | Привязываемый тип | Особенности поведения |
|---|---|---|
| `CollectionBinderBase<T>` | `IReadOnlyCollection<T>` | `IDisposable`; сброс с последующим переигрыванием при любом изменении; `OnReplace`/`OnMove` переупорядочивают слоты |
| `ObservableListBinder<T>` | `IReadOnlyList` / `IReadOnlyObservableList` / `IReadOnlyFilteredList<T>` | Гранулярные добавление/удаление с учётом индекса; хук `GetFilterList` для обёртки входных данных |
| `ObservableDictionaryBinder<TKey,TValue>` | `IReadOnlyObservableDictionary` | Гранулярные события ключ/значение; `Move` выбрасывает `NotImplementedException` |

## Оси вариантов

- **Обычный Binder против MonoBinder.** Обычные базы наследуются от [[IBinder|Binder]] (создаются в коде, например внутри [[View]]). Варианты MonoBinder (`ObservableListMonoBinder<T>`, `CollectionViewModelMonoBinder`) наследуются от [[Mono Binders|MonoBinder]], чтобы их можно было настраивать в инспекторе и выдавать диагностику `[BinderLog]`.
- **Базовый binder с хуками против конкретного binder'а, распределяющего ViewModel.** Базы выше абстрактны. Поставляемые конкретные binder'ы привязывают коллекцию [[IViewModel]] и отрисовывают по одному дочернему View на элемент:
  - `CollectionViewModelBinder<T>` — распределяет элементы по **фиксированному, заранее созданному массиву `T[]`** из View, активируя/деактивируя слоты; отклоняет [[BindMode|BindMode.TwoWay]].
  - `ObservableListViewModelBinder<T, TViewFactory>` — **создаёт/освобождает** View по требованию через `IViewFactory<T>`, с опциональным фильтром/компаратором (сортировкой). Подклассы для удобства по умолчанию используют `T = MonoView` и стандартную фабрику.
- **Фильтрация/сортировка.** `ObservableListBinder<T>.GetFilterList` позволяет подклассу обернуть источник в `IReadOnlyFilteredList<T>`; binder ViewModel на основе фабрики принимает типы фильтра и компаратора напрямую.

> Оси Switcher / Enum / EnumGroup / OneWayToSource здесь не применяются — они относятся к семействам скалярных binder'ов. Binder'ы коллекций по своей природе односторонние ([[BindMode|OneWay]]), направление от источника к View.

## Особенности поведения

- Начальное состояние **переигрывается**: существующие элементы немедленно пересылаются в `OnAdded` при вызове `SetValue`.
- Негранулярные источники (обычный `IReadOnlyList`, отфильтрованные списки) запускают полный **сброс с повторным добавлением** при каждом изменении; observable-списки/словари используют тонкие события для каждого элемента.
- Для View на каждый элемент жизненный цикл дочернего View использует [[View Initialization]] (`Initialize`/`Deinitialize`); связанная отрисовка по элементам также встречается в [[VirtualizedList Binders]].

## Источник

- Обычные базы: `StarterKit/Runtime/Binders/Collections/`
- Mono- и ViewModel-binder'ы: `StarterKit/Unity/Runtime/Binders/Collections/`

См. также [[StarterKit ViewModels]], [[Binders Catalog]].
