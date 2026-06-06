# ViewInitializer

ViewInitializer позволяет инициализировать View через Inspector без написания bootstrap-кода.

## Содержание

- [Обзор](#обзор)
- [ViewInitializer](#viewinitializer-1)
- [InitializeStage](#initializestage)
- [ViewInitializerManual](#viewinitializermanual)
- [InitializeComponent](#initializecomponent)
- [Настройка в Inspector](#настройка-в-inspector)

---

## Обзор

Вместо написания Bootstrap-скрипта:

```csharp
// Без ViewInitializer — нужен скрипт:
private void Awake()
{
    var viewModel = new PlayerViewModel();
    _view.Initialize(viewModel);
}
```

Используйте компонент ViewInitializer — вся настройка через Inspector:

1. Добавьте `ViewInitializer` на GameObject
2. Укажите View (компонент `MonoView`)
3. Укажите ViewModel (компонент, ScriptableObject или DI)
4. Выберите этап инициализации (Awake, Start, OnEnable, Manual, Di)

---

## ViewInitializer

Полностью автоматическая инициализация:

```csharp
public class ViewInitializer : ViewInitializerBase
{
    // Сериализованные поля — настраиваются в Inspector:
    // - View компонент(ы)
    // - ViewModel источник(и)
    // - InitializeStage
    // - _isDisposeViewOnDestroy
    // - _isDisposeViewModelOnDestroy
}
```

---

## InitializeStage

Определяет момент инициализации:

| Этап | Когда | Деинициализация |
|------|-------|-----------------|
| `Awake` | В `Awake()` | В `OnDestroy()` |
| `Start` | В `Start()` | В `OnDestroy()` |
| `OnEnable` | В `OnEnable()` | В `OnDisable()` |
| `Manual` | При вызове `Initialize()` | При вызове `Deinitialize()` |
| `DiConstructor` | При inject из DI-контейнера | В `OnDestroy()` |

### Awake (по умолчанию)

Инициализация в `Awake`. Подходит для большинства случаев.

### OnEnable / OnDisable

Полезно для переключаемых экранов — View инициализируется при активации и деинициализируется при деактивации:

```
GameObject.SetActive(true)  → OnEnable → Initialize
GameObject.SetActive(false) → OnDisable → Deinitialize
```

### Manual

Инициализация управляется из кода:

```csharp
[SerializeField] private ViewInitializer _initializer;

public void Show(IViewModel viewModel)
{
    _initializer.Initialize(); // Использует ViewModel из Inspector-настроек
}

public void Hide()
{
    _initializer.Deinitialize();
}
```

### DiConstructor

ViewModel разрешается через DI-контейнер. См. [Интеграция с DI](12-di-integration.md).

---

## ViewInitializerManual

Упрощённый вариант, требующий явного вызова из кода:

```csharp
[SerializeField] private ViewInitializerManual _initializer;

public void Show(IViewModel viewModel)
{
    _initializer.Initialize(viewModel);
}

public void Hide()
{
    _initializer.Deinitialize();
}
```

**Отличия от ViewInitializer:**
- Нет `InitializeStage` — только ручной вызов
- ViewModel передаётся в `Initialize(IViewModel)` напрямую
- Нельзя инициализировать дважды без деинициализации

---

## InitializeComponent

Компонент настройки View/ViewModel источника в ViewInitializer:

### ResolveType

| Тип | Описание |
|-----|----------|
| `Mono` | Ссылка на MonoBehaviour в Inspector |
| `References` | `[SerializeReference]` — для POCO-объектов |
| `ScriptableObject` | Ссылка на ScriptableObject |
| `Di` | Разрешение через DI-контейнер (Zenject/VContainer) |

### ViewModelInitializeComponent

Специализация для ViewModel:
- `ResolveType.Mono` — указывает на `MonoViewModel` компонент
- `ResolveType.ScriptableObject` — указывает на `ScriptableViewModel`
- `ResolveType.Di` — `TypeSelector` для выбора типа ViewModel из контейнера

---

## Настройка в Inspector

### Шаг 1: Добавьте ViewInitializer

Добавьте компонент `ViewInitializer` на GameObject.

### Шаг 2: Настройте Stage

Выберите `InitializeStage` (по умолчанию Awake).

### Шаг 3: Настройте View

В секции Views добавьте View-компоненты (`MonoView`).

### Шаг 4: Настройте ViewModel

В секции ViewModel выберите `ResolveType` и укажите источник:
- **Mono** — перетащите `MonoViewModel` из Inspector
- **ScriptableObject** — перетащите `ScriptableViewModel`
- **Di** — выберите тип для DI-разрешения

### Шаг 5: Опции автоочистки

- `_isDisposeViewOnDestroy` — автоматически деинициализировать View при OnDestroy
- `_isDisposeViewModelOnDestroy` — автоматически вызвать Dispose на ViewModel

---

## См. также

- [View](05-views.md) — инициализация из кода
- [DI интеграция](12-di-integration.md) — DiConstructor с Zenject/VContainer
