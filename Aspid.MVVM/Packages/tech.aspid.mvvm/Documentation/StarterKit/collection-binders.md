# Collection Binders

Биндеры для отображения коллекций ViewModel в виде списков UI-элементов.

---

## Обзор

| Биндер | Назначение |
|--------|-----------|
| `ViewModelObservableListBinder` | Динамический список с фабрикой View |
| `ViewModelCollectionBinder<T>` | Статическая коллекция (фиксированные View) |
| `ViewModelObservableDictionaryBinder` | Словарь с фабрикой View |
| `VirtualizedListItemSourceBinder` | Источник данных для VirtualizedList |

---

## ViewModelObservableListBinder

Основной биндер для отображения `ObservableList<IViewModel>`. При добавлении/удалении элементов автоматически создаёт/уничтожает View.

### Принцип работы

```
ObservableList<IViewModel> (ViewModel)
    → ViewModelObservableListBinder
        → IViewFactory.Create(viewModel) при Add
        → IViewFactory.Release(view) при Remove
```

### Inspector-свойства

| Свойство | Тип | Описание |
|----------|-----|----------|
| `Factory` | `IViewFactory<MonoView>` | Фабрика для создания View (PrefabViewFactory или PrefabViewPool) |
| `Filter` | `ICollectionFilter<IViewModel>` | Опциональный фильтр |
| `Order` | `ICollectionOrder<IViewModel>` | Опциональная сортировка |

### Режим

**OneWay** и **OneTime**.

### Пример ViewModel

```csharp
[ViewModel]
public partial class TodoListViewModel
{
    [OneTimeBind]
    private IReadOnlyObservableListSync<TodoItemViewModel> _items;

    public TodoListViewModel(TodoStorage storage)
    {
        _items = storage.Todos.CreateSync(
            todo => new TodoItemViewModel(todo)
        );
    }
}
```

MonoBinder-вариант: `ViewModelObservableListMonoBinder`.

---

## ViewModelCollectionBinder\<T\>

Для статических коллекций с предварительно созданными View:

```csharp
// В Inspector: массив _views заполнен заранее
// При привязке: View[0].Initialize(collection[0]), View[1].Initialize(collection[1])...
// Лишние View скрываются через SetActive(false)
```

Подходит для фиксированного количества элементов (например, 5 слотов инвентаря).

---

## VirtualizedListItemSourceBinder

Устанавливает `ItemsSource` для `VirtualizedList`:

```csharp
[ViewModel]
public partial class ListViewModel
{
    [OneTimeBind]
    private FilteredList<ItemViewModel> _filteredItems;

    [OneTimeBind]
    private ObservableList<ItemViewModel> _items;

    public ListViewModel()
    {
        _items = new ObservableList<ItemViewModel>();
        _filteredItems = new FilteredList<ItemViewModel>(_items);
    }
}
```

### Inspector-свойства

| Свойство | Описание |
|----------|----------|
| `Filter` | `ICollectionFilter<IViewModel>` |
| `Order` | `ICollectionOrder<IViewModel>` |

Создаёт внутренний `FilteredList<IViewModel>` при наличии фильтра/сортировки.

**Режим:** **OneTime**.

---

## View Factories

Фабрики используются `ViewModelObservableListBinder` для создания View:

### PrefabViewFactory

Создаёт View через `Object.Instantiate`:

```
Create(viewModel) → Instantiate(prefab) → SetSibling → Initialize(viewModel)
Release(view) → DestroyViewAndGameObject()
```

### PrefabViewPool

Переиспользует View через `ObjectPool`:

```
Create(viewModel) → Pool.Get() → Initialize(viewModel) → SetActive(true)
Release(view) → Deinitialize() → SetActive(false) → Pool.Release()
```

**Свойства:**
- `_initialCount` — начальный размер пула
- `_maxCount` — максимальный размер

> **Рекомендация:** Используйте `PrefabViewPool` для часто обновляемых списков (чат, лента).

Подробнее: [View Factories](view-factories.md).

---

## Фильтрация и сортировка

### ICollectionFilter\<T\>

```csharp
public interface ICollectionFilter<in T>
{
    bool Matches(T item);
}
```

Реализуйте для создания кастомного фильтра:

```csharp
[Serializable]
public class CompletedFilter : ICollectionFilter<IViewModel>
{
    public bool Matches(IViewModel item) =>
        item is ItemViewModel viewModel && viewModel.IsCompleted;
}
```

### ICollectionOrder\<T\>

```csharp
public interface ICollectionOrder<in T> : IComparer<T> { }
```

Реализуйте `Compare`, как у обычного `IComparer<T>`. Пустой слот в Inspector сохраняет порядок исходной коллекции.

### Готовые фильтры

| Фильтр | Поведение |
|--------|-----------|
| `AndCollectionFilter<T>` | Проходит элемент, который пропускают все вложенные фильтры |
| `OrCollectionFilter<T>` | Проходит элемент, который пропускает хотя бы один вложенный фильтр |
| `NotCollectionFilter<T>` | Инвертирует вложенный фильтр |
| `ConditionalCollectionFilter<T>` | Применяет вложенный фильтр только при включённом `IsEnabled` |
| `ConverterCollectionFilter<T>` | Пропускает элемент, для которого `IConverter<T, bool>` вернул `true` |
| `PredicateCollectionFilter<T>` | Обёртка над `Predicate<T>` для фильтров из кода |

Пустой вложенный слот пропускает всё.

### Готовые сортировки

| Сортировка | Поведение |
|------------|-----------|
| `SequenceCollectionOrder<T>` | Применяет вложенные порядки по очереди: решает первый, различивший элементы |
| `InverseCollectionOrder<T>` | Обращает вложенный порядок |
| `ComparisonCollectionOrder<T>` | Обёртка над `IComparer<T>` или `Comparison<T>` для сортировок из кода |

---

## См. также

- [Коллекции](../09-collections.md) — ObservableList, FilteredList, синхронизация
- [View Factories](view-factories.md) — PrefabViewFactory, PrefabViewPool
- [VirtualizedList Tutorial](../Tutorials/virtualized-list.md) — пример виртуализации
