# Virtualized List

Large collections without a GameObject per item: only visible rows exist, filtered and ordered from the Inspector.

**You learn:** `VirtualizedListItemSourceMonoBinder`, `FilteredList<T>`, `ICollectionFilter`, `ICollectionOrder`, `IComponentInitializable`, `[Access(Get = ...)]`.

**Assumes:** [Todo List](../05.%20TodoList/README.md).

Scenes: `Scenes/Vertical Virtualized List.unity`, `Scenes/Horizontal Virtualized List.unity`.

| File | Role |
|---|---|
| `Scripts/ViewModels/ItemViewModel.cs` | One row. |
| `Scripts/ViewModels/ListViewModel.cs` | `ObservableList` plus a `FilteredList` over it, list operations as commands. |
| `Scripts/Filters/` | `ICollectionFilter` and `ICollectionOrder` implementations configured on the binder. |
| `Scripts/Views/` | `ItemView`, `ListView`. |

## ViewModels

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

`[Access(Get = Access.Public)]` exposes the getters so filters and comparers can read them.

```csharp
[ViewModel]
[Serializable]
public sealed partial class ListViewModel : IComponentInitializable
{
    [SerializeField] [Min(0)] private int _count = 100;

    [OneTimeBind] private readonly FilteredList<ItemViewModel> _isOnTrueItems;
    [OneTimeBind] private readonly ObservableList<ItemViewModel> _items = new();

    public ListViewModel() =>
        _isOnTrueItems = new FilteredList<ItemViewModel>(Items, vm => vm.IsCompleted);

    void IComponentInitializable.Initialize()
    {
        for (var i = 0; i < _count; i++)
            Items.Add(CreateElement());
    }

    [RelayCommand] private void AddViewModel() => Items.Add(CreateElement());
    [RelayCommand] private void InsertViewModel(int index) => Items.Insert(index, CreateElement());
    [RelayCommand] private void Move(int oldIndex, int newIndex) => Items.Move(oldIndex, newIndex);
    [RelayCommand] private void Swap(int index1, int index2) => Items.Swap(index1, index2);
    [RelayCommand] private void Remove(int index) => Items.RemoveAt(index);
    [RelayCommand] private void Replace(int index) => Items[index] = CreateElement();
}
```

- `IComponentInitializable.Initialize` runs after Unity has deserialized the `[Serializable]` ViewModel, so `_count` from the Inspector is already set.
- `FilteredList<T>` is a live subset of `Items`: it follows every change of the source and re-evaluates the predicate.
- Every list operation is a command, so the scene buttons exercise `Add`, `Insert`, `Move`, `Swap`, `RemoveAt` and index replacement.

## Views

```csharp
[View]
public sealed partial class ItemView : MonoView
{
    [RequireBinder(typeof(string))]
    [SerializeField] private MonoBinder[] _number;

    [RequireBinder(typeof(bool))]
    [SerializeField] private MonoBinder[] _isCompleted;
}

[View]
public sealed partial class ListView : MonoView
{
    [RequireBinder(typeof(IReadOnlyList<IViewModel>))]
    [SerializeField] private MonoBinder[] _items;

    [RequireBinder(typeof(IReadOnlyList<IViewModel>))]
    [SerializeField] private MonoBinder[] _isOnTrueItems;
}
```

`TextMonoBinder` accepts the `int` `Number` directly through `INumberBinder`. `ListView` has two collection binders: the full list and the filtered one.

## Filters and orders in the Inspector

```csharp
[Serializable]
public sealed class CompletedCollectionFilter : ICollectionFilter<IViewModel>
{
    [SerializeField] private bool _isCompleted;

    public bool Matches(IViewModel item) =>
        item is ItemViewModel viewModel && viewModel.IsCompleted == _isCompleted;
}

[Serializable]
public sealed class NumberCollectionOrder : ICollectionOrder<IViewModel>
{
    [SerializeField] private bool _isInvert;

    public int Compare(IViewModel x, IViewModel y)
    {
        if (x is not ItemViewModel itemX || y is not ItemViewModel itemY) return 0;

        var result = itemX.Number.CompareTo(itemY.Number);
        return _isInvert ? -result : result;
    }
}
```

Both are assigned on `VirtualizedListItemSourceMonoBinder` through `[SerializeReference]` slots. The ViewModel exposes one list; how it is sliced and sorted is a View decision.

## Virtualization

`VirtualizedListItemSourceBinder` renders only the rows inside the viewport. On scroll:

1. Rows that leave the viewport are deinitialized and returned to the pool.
2. Rows that enter are taken from the pool and initialized with their ViewModel.

Thousands of items cost a handful of GameObjects.

Setup: add `VirtualizedListItemSourceMonoBinder` to the list, assign the item prefab on the `VirtualizedList` component, optionally set a filter and an order on the binder.

## Summary

| Pattern | Where |
|---|---|
| `FilteredList<T>` | live subset in the ViewModel |
| `IComponentInitializable` | setup after Inspector deserialization |
| `ICollectionFilter<IViewModel>` | View-side filter |
| `ICollectionOrder<IViewModel>` | View-side ordering |
| Virtualization | visible rows only |

See also [Collections](../../Documentation/09-collections.md), [Collection Binders](../../Documentation/StarterKit/collection-binders.md), [View Factories](../../Documentation/StarterKit/view-factories.md).

Text uses TextMeshPro (part of `com.unity.ugui`). The sample ships its own font asset in `Fonts/` (Liberation Sans, OFL), so it does not depend on the fonts from TMP Essentials.
