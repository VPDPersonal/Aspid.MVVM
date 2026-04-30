# Туториал: VirtualizedList

Разбор Sample-проекта VirtualizedList — виртуализация, FilteredList, фильтры и компараторы.

---

## Что мы разбираем

Список из 100+ элементов с виртуализацией (только видимые элементы рендерятся), фильтрацией (чётные/нечётные, выполненные/невыполненные) и сортировкой.

Файлы: `Samples/VirtualizedList/`

---

## ViewModel

### ItemViewModel — элемент списка

```csharp
[ViewModel]
public partial class ItemViewModel
{
    [Access(Get = Access.Public)]
    [OneWayBind] private int _number;

    [Access(Get = Access.Public)]
    [OneWayBind] private bool _isCompleted;

    public ItemViewModel(int number, bool isCompleted)
    {
        _number = number;
        _isCompleted = isCompleted;
    }
}
```

`[Access(Get = Access.Public)]` — свойства `Number` и `IsCompleted` доступны для чтения из фильтров и компараторов.

### ListViewModel — управление списком

```csharp
[ViewModel]
[Serializable]
public sealed partial class ListViewModel : IComponentInitializable
{
    [SerializeField] [Min(0)] private int _count = 100;

    [OneTimeBind] private readonly FilteredList<ItemViewModel> _isOnTrueItems;
    [OneTimeBind] private readonly ObservableList<ItemViewModel> _items = new();

    public ListViewModel()
    {
        _items = new ObservableList<ItemViewModel>(_items);
        _isOnTrueItems = new FilteredList<ItemViewModel>(Items, vm => vm.IsCompleted);
    }

    void IComponentInitializable.Initialize()
    {
        for (var i = 0; i < _count; i++)
            Items.Add(CreateElement());
    }

    [RelayCommand]
    private void AddViewModel() => Items.Add(CreateElement());

    [RelayCommand]
    private void InsertViewModel(int index) => Items.Insert(index, CreateElement());

    [RelayCommand]
    private void Move(int oldIndex, int newIndex) => Items.Move(oldIndex, newIndex);

    [RelayCommand]
    private void Swap(int index1, int index2) => Items.Swap(index1, index2);

    [RelayCommand]
    private void Remove(int index) => Items.RemoveAt(index);

    [RelayCommand]
    private void Replace(int index) => Items[index] = CreateElement();

    private ItemViewModel CreateElement()
    {
        var isCompleted = Random.Range(0, 2) is 0;
        return new ItemViewModel(_number++, isCompleted);
    }
}
```

**Ключевые паттерны:**

1. **`IComponentInitializable`** — интерфейс для инициализации из Unity (вызывается `ViewModelInitializeComponent`)
2. **`[Serializable]` + `[SerializeField]`** — настройка из Inspector (например, `_count`)
3. **`FilteredList<T>`** — подмножество `_items`, автоматически фильтрующее по `vm.IsCompleted`
4. **Операции списка** — `Add`, `Insert`, `Move`, `Swap`, `RemoveAt`, замена по индексу

---

## FilteredList

```csharp
_isOnTrueItems = new FilteredList<ItemViewModel>(Items, vm => vm.IsCompleted);
```

`FilteredList<T>` автоматически отслеживает изменения в исходном списке и обновляет отфильтрованный список. Биндер получает только элементы, прошедшие фильтр.

---

## Views

### ItemView

```csharp
[View]
public sealed partial class ItemView : MonoView
{
    [BindId("Number")]
    [RequireBinder(typeof(string))]
    [SerializeField] private MonoBinder[] _name;

    [RequireBinder(typeof(bool))]
    [SerializeField] private MonoBinder[] _isCompleted;
}
```

`[BindId("Number")]` — поле `_name` в View привязывается к свойству `Number` в ViewModel.

### ListView

```csharp
[View]
public sealed partial class ListView : MonoView
{
    [RequireBinder(typeof(IReadOnlyList<IViewModel>))]
    [SerializeField] private MonoBinder[] _items;

    [RequireBinder(typeof(IReadOnlyList<IViewModel>))]
    [SerializeField] private MonoBinder[] _isOnTrueItems;
}
```

Два коллекционных биндера: `_items` для полного списка, `_isOnTrueItems` для отфильтрованного.

---

## Фильтры и компараторы

### IViewModelCollectionFilter

Фильтр для коллекционных биндеров, задаётся в Inspector:

```csharp
[Serializable]
public sealed class CompletedCollectionFilter : IViewModelCollectionFilter
{
    [SerializeField] private bool _isCompleted;

    public Predicate<IViewModel> Get() => viewModel =>
    {
        if (viewModel is ItemViewModel itemViewModel)
            return itemViewModel.IsCompleted == _isCompleted;
        return false;
    };
}
```

```csharp
[Serializable]
public sealed class EvenCollectionFilter : IViewModelCollectionFilter
{
    [SerializeField] private bool _isInvert;

    public Predicate<IViewModel> Get() => viewModel =>
    {
        if (viewModel is ItemViewModel itemViewModel)
            return (itemViewModel.Number % 2 is 0) != _isInvert;
        return false;
    };
}
```

### IViewModelCollectionComparer

Сортировка для коллекционных биндеров:

```csharp
[Serializable]
public sealed class NumberCollectionComparer : IViewModelCollectionComparer
{
    [SerializeField] private bool _isInvert;

    public IComparer<IViewModel> Get() => new Comparer(_isInvert);

    private class Comparer : IComparer<IViewModel>
    {
        private readonly bool _isInvert;
        public Comparer(bool isInvert) { _isInvert = isInvert; }

        public int Compare(IViewModel x, IViewModel y)
        {
            if (x is not ItemViewModel itemX || y is not ItemViewModel itemY) return 0;
            var result = itemX.Number.CompareTo(itemY.Number);
            return _isInvert ? -result : result;
        }
    }
}
```

Фильтры и компараторы задаются в Inspector на `VirtualizedListItemSourceMonoBinder` через `[SerializeReference]`.

---

## Виртуализация

`VirtualizedListItemSourceBinder` рендерит только видимые элементы. При прокрутке:
1. Элементы за пределами видимости — деинициализируются и возвращаются в пул
2. Новые видимые элементы — берутся из пула и инициализируются ViewModel

Это позволяет отображать тысячи элементов без проседания производительности.

### Настройка в Inspector

1. Добавьте `VirtualizedListItemSourceMonoBinder` на ListView
2. Укажите `ViewFactory` (рекомендуется `PrefabViewPool` для виртуализации)
3. Опционально добавьте фильтр и/или компаратор

---

## Ключевые паттерны

| Паттерн | Описание |
|---------|----------|
| `FilteredList<T>` | Автоматическая фильтрация коллекции |
| `IComponentInitializable` | Инициализация ViewModel из Unity-компонента |
| `[Serializable]` ViewModel | Настройка ViewModel из Inspector |
| `IViewModelCollectionFilter` | Фильтр на стороне View (Inspector) |
| `IViewModelCollectionComparer` | Сортировка на стороне View (Inspector) |
| Виртуализация | Рендеринг только видимых элементов |

---

## См. также

- [Коллекции](../09-collections.md) — FilteredList, ObservableList
- [Collection Binders](../StarterKit/collection-binders.md) — VirtualizedListItemSourceBinder
- [View Factories](../StarterKit/view-factories.md) — PrefabViewPool
- [Туториал TodoList](todo-list.md) — CreateSync, программные биндеры
