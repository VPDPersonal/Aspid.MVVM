# Интеграция с DI

Aspid.MVVM поддерживает Zenject и VContainer для автоматического разрешения ViewModel через Dependency Injection.

## Содержание

- [Обзор](#обзор)
- [Zenject](#zenject)
- [VContainer](#vcontainer)
- [DiConstructor](#diconstructor)

---

## Обзор

Интеграция с DI позволяет:
- Разрешать ViewModel из DI-контейнера
- Инжектировать зависимости в ViewModel
- Использовать `ViewInitializer` с `InitializeStage.DiConstructor`

Поддерживаются два DI-фреймворка:
- **Zenject** (Extenject)
- **VContainer**

---

## Zenject

### Шаг 1: Определите символ компиляции

В `Project Settings → Player → Scripting Define Symbols` добавьте:

```
ASPID_MVVM_ZENJECT_INTEGRATION
```

### Шаг 2: Зарегистрируйте ViewModel в контейнере

```csharp
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerViewModel>().AsSingle();
        Container.Bind<InventoryViewModel>().AsSingle();
    }
}
```

### Шаг 3: Настройте ViewInitializer

1. Добавьте `ViewInitializer` на GameObject
2. Установите `InitializeStage` → **DiConstructor**
3. В секции ViewModel выберите `ResolveType` → **Di**
4. В `TypeSelector` укажите тип ViewModel (например, `PlayerViewModel`)

Zenject автоматически инжектирует контейнер в `ViewInitializerBase` через `[Inject]`.

### Пример ViewModel с Zenject

```csharp
[ViewModel]
public partial class PlayerViewModel
{
    [OneWayBind] private string _name;
    [OneWayBind] private int _health;

    private readonly IPlayerService _playerService;

    // Zenject инжектирует IPlayerService автоматически
    public PlayerViewModel(IPlayerService playerService)
    {
        _playerService = playerService;
        _name = playerService.Name;
        _health = playerService.Health;
    }
}
```

---

## VContainer

### Шаг 1: Определите символ компиляции

```
ASPID_MVVM_VCONTAINER_INTEGRATION
```

### Шаг 2: Зарегистрируйте ViewModel

```csharp
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<PlayerViewModel>(Lifetime.Scoped);
        builder.Register<InventoryViewModel>(Lifetime.Scoped);
    }
}
```

### Шаг 3: Настройте ViewInitializer

Аналогично Zenject — через Inspector с `InitializeStage.DiConstructor`.

---

## DiConstructor

`InitializeStage.DiConstructor` — специальный этап, при котором:

1. DI-контейнер инжектирует себя в `ViewInitializerBase`
2. При инициализации `ViewModelInitializeComponent` с `ResolveType.Di` обращается к контейнеру
3. Контейнер создаёт ViewModel с разрешением всех зависимостей
4. View инициализируется полученным ViewModel

### ViewModelInitializeComponent с Di

В Inspector:
- `ResolveType` → **Di**
- `TypeSelector` → выберите конкретный тип ViewModel

`TypeSelector` отображает имя типа (строка), по которому контейнер находит нужную регистрацию.

---

## Без DI — ручная инициализация

Если DI не используется, применяйте:
- Bootstrap-паттерн с `view.Initialize(viewModel)` — см. [Быстрый старт](01-getting-started.md)
- `ViewInitializer` с `ResolveType.Mono` или `ResolveType.ScriptableObject`
- `ViewInitializerManual` с передачей ViewModel из кода

---

## См. также

- [ViewInitializer](11-view-initializers.md) — подробности об инициализации
- [ViewModel](04-viewmodels.md) — создание ViewModel
- [Быстрый старт](01-getting-started.md) — инициализация без DI
