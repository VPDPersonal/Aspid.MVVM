# View Factories

Фабрики для создания и уничтожения View из префабов. Используются коллекционными биндерами.

---

## IViewFactory\<T\>

Интерфейс фабрики:

```csharp
public interface IViewFactory<T> where T : IView
{
    T Create(IViewModel viewModel);
    void Release(T view);
}
```

---

## PrefabViewFactory

Создаёт View из префаба через `Object.Instantiate`. При `Release` — уничтожает объект.

### Inspector-свойства

| Свойство | Описание |
|----------|----------|
| `_prefab` | Префаб MonoView |
| `_container` | Родительский Transform для инстанцированных объектов |
| `_overrideSibling` | Переопределить порядок в иерархии |
| `_siblingIndex` | Индекс при `_overrideSibling = true` |

### Принцип работы

1. `Create(viewModel)` — `Instantiate(prefab, container)` → `view.Initialize(viewModel)`
2. `Release(view)` — `view.DestroyViewAndGameObject()`

```csharp
// Из кода:
var factory = new PrefabViewFactory(itemPrefab, container);

// Или типизированный:
var factory = new PrefabViewFactory<ItemView>(itemPrefab, container);
```

---

## PrefabViewPool

Наследует `PrefabViewFactory`, но использует `ObjectPool<T>` вместо создания/уничтожения.

### Inspector-свойства

| Свойство | Описание |
|----------|----------|
| `_initialCount` | Начальный размер пула (pre-warm) |
| `_maxCount` | Максимальный размер пула |

### Принцип работы

1. `Create(viewModel)` — берёт View из пула (или создаёт новый) → `SetActive(true)` → `Initialize(viewModel)`
2. `Release(view)` — `Deinitialize()` → `SetActive(false)` → возвращает в пул

```csharp
// Из кода:
var pool = new PrefabViewPool(itemPrefab, container, new PoolSettings(initialCount: 10, maxCount: 100));
```

### Преимущества над PrefabViewFactory

- Нет аллокаций при повторном использовании
- Нет вызовов `Instantiate`/`Destroy`
- Подходит для списков с частым добавлением/удалением элементов

---

## Использование с коллекционными биндерами

View Factories используются в `ViewModelObservableListBinder` и `VirtualizedListItemSourceBinder`:

```csharp
// В Inspector:
// ViewModelObservableListBinder → ViewFactory → PrefabViewPool
//                               → Prefab: ItemView
//                               → Container: ScrollContent
//                               → Initial Count: 20
```

---

## См. также

- [Collection Binders](collection-binders.md) — использование фабрик
- [Коллекции](../09-collections.md) — ObservableList
- [Обзор StarterKit](README.md)
