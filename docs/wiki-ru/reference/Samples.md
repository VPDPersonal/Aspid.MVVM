---
title: Примеры
type: reference
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Samples/01. Counter/Scripts
  - Aspid.MVVM/Assets/Aspid/MVVM/Samples/02. Greeter/Scripts
  - Aspid.MVVM/Assets/Aspid/MVVM/Samples/HelloWorld/MVVM/Scripts
  - Aspid.MVVM/Assets/Aspid/MVVM/Samples/Stats/Scripts/ViewModels/StatsViewModel.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Samples/TodoList/Scripts/Todos/Storages/TodoStorageViewModel.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Samples/VirtualizedList/Scripts/ViewModels/ListViewModel.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/package.json
tags: [samples, learning, examples]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/reference/Samples.md
translated_at: 2026-05-31
---

# Примеры

> Шесть запускаемых сцен, упорядоченных от тривиальных к продвинутым, которые обучают фреймворку по одной функции за раз — прочитайте их перед изучением исходного кода.

Поставляемые примеры расположены в `Assets/Aspid/MVVM/Samples/`. Четыре из них (HelloWorld, Stats, TodoList, VirtualizedList) зарегистрированы как импортируемые UPM-примеры в `package.json`; пронумерованные папки (`01. Counter` … `06. VirtualizedList`) представляют собой переструктурирование в порядке обучения (добавлены недавно — отслеживаются в git, но ещё не включены в манифест UPM, поэтому, вероятно, являются незавершённой работой версии 1.1.0).

## Чему учит каждый из них

| Пример | Демонстрирует |
|--------|--------------|
| **Counter** | Минимально возможный цикл: `[Bind] int _count` плюс три метода `[RelayCommand]` (`Increment`/`Decrement`/`Reset`). [[View]] объявляет слоты `MonoBinder[]` с `[RequireBinder(typeof(int))]`. |
| **Greeter** | `MonoViewModel` (ViewModel, живущий на MonoBehaviour), двусторонний текстовый ввод, хук изменения `partial void OnNameChanged`, пересчитывающий `Greeting`, и пользовательский `IConverterString` (`PaintNameConverter`). |
| **HelloWorld** | Одна и та же задача, реализованная тремя способами — `General` (обычный), `MVP` и `MVVM` — чтобы можно было сравнить архитектуры. MVVM-класс `SpeakerViewModel` подробно прокомментирован и служит каноническим эталоном «что генерирует генератор». |
| **Stats** | Множество полей `[OneWayBind]`, ограничение через `[RelayCommand(CanExecute = …)]` и вызов `NotifyCanExecuteChanged()`, управляемый хуками изменений. Доступные только для чтения и редактируемые представления одного [[IViewModel]]. |
| **TodoList** | Наблюдаемая коллекция, синхронизированная с дочерними ViewModel через `CreateSync`, члены `[OneTimeBind]`/`[TwoWayBind]`, фильтрация по поиску и переиспользуемый диалог редактирования. |
| **VirtualizedList** | Отрисовка больших списков с пулингом с использованием `ObservableList` + `FilteredList`, `IComponentInitializable` и `IViewModelCollectionFilter` для фильтрации/сортировки во время выполнения. |

## Как они связаны между собой

Каждый пример следует одной и той же структуре: MonoBehaviour `Bootstrap` создаёт ViewModel и вызывает `view.Initialize(viewModel)`, а затем `DeinitializeView()?.DisposeViewModel()` при уничтожении. View — это `[View] partial : MonoView`, предоставляющий сериализованные слоты `MonoBinder`; ViewModel — это `[ViewModel] partial`, чьи члены `[Bind]`/`[RelayCommand]` генерируют свойства, методы `Set…`, события `…Changed` и свойства `…Command`, которые binder'ы потребляют во время выполнения (см. [[ViewModel to Generated Code]]).

Последовательность отражает путь обучения: Counter (привязки + команды) → Greeter (MonoViewModel + конвертеры + хуки) → HelloWorld (сравнение архитектур) → Stats (`CanExecute`) → TodoList → VirtualizedList (коллекции). HelloWorld рекомендуется читать первым.

Примеры с коллекциями зависят от внешнего UPM-пакета [[External Dependencies|tech.aspid.collections]] (`Aspid.Collections.Observable.*`), а не от исходного кода репозитория.

## Источник

- `Assets/Aspid/MVVM/Samples/` — все папки с примерами
- `Assets/Aspid/MVVM/package.json` — манифест `samples` для UPM

## Смотрите также

[[Getting Started]] · [[Data Binding]] · [[Relay Commands]] · [[Converters]] · [[VirtualizedList Binders]] · [[Collection Binders]] · [[BindMode]]
