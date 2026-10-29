# View Factories

Factories that create and destroy Views from prefabs. Used by the collection binders.

---

## IViewFactory\<T\>

The factory interface:

```csharp
public interface IViewFactory<T> where T : IView
{
    T Create(IViewModel viewModel);
    void Release(T view);
}
```

---

## PrefabViewFactory

Creates a View from a prefab through `Object.Instantiate`. `Release` destroys the object.

### Inspector properties

| Property | Description |
|----------|----------|
| `_prefab` | The MonoView prefab |
| `_container` | Parent Transform for instantiated objects |
| `_overrideSibling` | Override the order in the hierarchy |
| `_siblingIndex` | Index used when `_overrideSibling = true` |

### How it works

1. `Create(viewModel)`: `Instantiate(prefab, container)` → `view.Initialize(viewModel)`
2. `Release(view)`: `view.DestroyViewAndGameObject()`

```csharp
// From code:
var factory = new PrefabViewFactory(itemPrefab, container);

// Or typed:
var factory = new PrefabViewFactory<ItemView>(itemPrefab, container);
```

---

## PrefabViewPool

Inherits `PrefabViewFactory` but uses an `ObjectPool<T>` instead of create/destroy.

### Inspector properties

| Property | Description |
|----------|----------|
| `_initialCount` | Initial pool size (pre-warm) |
| `_maxCount` | Maximum pool size |

### How it works

1. `Create(viewModel)`: takes a View from the pool (or creates one) → `SetActive(true)` → `Initialize(viewModel)`
2. `Release(view)`: `Deinitialize()` → `SetActive(false)` → returns it to the pool

```csharp
// From code:
var pool = new PrefabViewPool(itemPrefab, container, new PoolSettings(initialCount: 10, maxCount: 100));
```

### Advantages over PrefabViewFactory

- No allocations on reuse
- No `Instantiate`/`Destroy` calls
- Fits lists with frequent add/remove

---

## With collection binders

View factories are used by `ViewModelObservableListBinder` and `VirtualizedListItemSourceBinder`:

```csharp
// Inspector:
// ViewModelObservableListBinder → ViewFactory → PrefabViewPool
//                               → Prefab: ItemView
//                               → Container: ScrollContent
//                               → Initial Count: 20
```

---

## See also

- [Collection Binders](collection-binders.md), using the factories
- [Collections](../09-collections.md), ObservableList
- [StarterKit overview](README.md)
