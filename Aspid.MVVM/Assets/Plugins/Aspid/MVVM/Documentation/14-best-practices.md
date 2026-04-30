# Лучшие практики

Рекомендации по использованию Aspid.MVVM и типичные ошибки.

## Содержание

- [Структура проекта](#структура-проекта)
- [ViewModel](#viewmodel)
- [Привязки](#привязки)
- [Команды](#команды)
- [Коллекции](#коллекции)
- [Производительность](#производительность)
- [Типичные ошибки](#типичные-ошибки)
- [Тестирование](#тестирование)

---

## Структура проекта

Рекомендуемая организация:

```
Features/
  PlayerStats/
    Models/
      Player.cs
    ViewModels/
      PlayerViewModel.cs
    Views/
      PlayerView.cs
      PlayerView.prefab
    Binders/          (кастомные, если есть)
      HealthBarBinder.cs
```

Группируйте по функциональности (feature), не по типу (все ViewModel в одной папке).

---

## ViewModel

### Предпочитайте POCO ViewModel

```csharp
// ✅ Хорошо — чистый C# без Unity-зависимостей
[ViewModel]
public partial class PlayerViewModel
{
    [OneWayBind] private int _health;
}

// ⚠️ Только если нужна редактируемость в Inspector
[ViewModel]
public partial class SettingsViewModel : MonoViewModel
{
    [SerializeField] [OneWayBind] private float _volume;
}
```

### Держите ViewModel чистым

ViewModel не должен содержать Unity-специфичную логику:

```csharp
// ❌ Плохо — Unity-зависимость в ViewModel
[ViewModel]
public partial class BadViewModel
{
    [OneWayBind] private string _text;

    public void Update() => Text = Time.deltaTime.ToString(); // НЕ делайте так
}

// ✅ Хорошо — бизнес-логика в Model
[ViewModel]
public partial class GoodViewModel
{
    [OneWayBind] private string _text;

    public GoodViewModel(ITimer timer)
    {
        timer.Tick += elapsed => Text = elapsed.ToString();
    }
}
```

### Используйте `partial void OnXxxChanged` вместо сторонних подписок

```csharp
// ✅ Хорошо — реакция на изменение через обработчик
[ViewModel]
public partial class SearchViewModel
{
    [TwoWayBind] private string _query;

    partial void OnQueryChanged(string newValue)
    {
        _searchService.Search(newValue);
    }
}
```

---

## Привязки

### Выбирайте минимально необходимый режим

```csharp
// ✅ OneWay для отображения (не нужна обратная связь)
[OneWayBind] private int _score;

// ✅ OneTime для статических данных и команд
[OneTimeBind] private IRelayCommand _save;
[OneTimeBind] private string _title;

// ✅ TwoWay только для интерактивных элементов
[TwoWayBind] private string _inputText;
```

### Используйте конвертеры вместо изменения ViewModel

```csharp
// ❌ Плохо — форматирование в ViewModel
[OneWayBind] private string _healthText;
Health = 75;
HealthText = $"HP: {Health}/100";

// ✅ Хорошо — конвертер на биндере
[OneWayBind] private int _health;
// В Inspector: TextBinder + StringFormatConverter (Format = "HP: {0}/100")
```

---

## Команды

### Используйте `[RelayCommand]` вместо ручного создания

```csharp
// ✅ Хорошо
[RelayCommand(CanExecute = nameof(CanSave))]
private void Save() => _storage.Save();
private bool CanSave() => _storage.HasChanges;

// ⚠️ Ручное создание — только для особых случаев
[Bind] private readonly IRelayCommand _legacyCommand = new RelayCommand(...);
```

### Не забывайте `NotifyCanExecuteChanged`

```csharp
partial void OnIsDirtyChanged(bool newValue)
{
    // ✅ Обязательно — иначе кнопка не обновится
    SaveCommand.NotifyCanExecuteChanged();
}
```

---

## Коллекции

### `CreateSync` для Model → ViewModel

```csharp
// ✅ Хорошо — автоматическая синхронизация
_viewModels = _models.CreateSync(model => new ItemViewModel(model));

// ❌ Плохо — ручная синхронизация
_models.CollectionChanged += (_, args) => {
    // Не делайте так — это долго и подвержено ошибкам
};
```

### FilteredList вместо LINQ

```csharp
// ✅ Хорошо — реактивная фильтрация
var filtered = new FilteredList<ItemViewModel>(_items)
{
    Filter = item => item.IsVisible
};

// ❌ Плохо — нереактивный LINQ
var filtered = _items.Where(x => x.IsVisible).ToList();
```

### Не забывайте Dispose

```csharp
// ✅ Обязательно освободить FilteredList
public void Dispose()
{
    _filteredList.Dispose();
}
```

---

## Производительность

### `NotifyAll()` при массовых обновлениях

```csharp
// ✅ Хорошо — одно уведомление вместо N
_health = data.Health;
_name = data.Name;
_level = data.Level;
NotifyAll(); // Один раз для всех
```

### `PrefabViewPool` для частого создания/удаления

```csharp
// ✅ Хорошо — пул переиспользует объекты
// В Inspector: ViewModelObservableListBinder → Factory = PrefabViewPool

// ⚠️ PrefabViewFactory — создаёт/уничтожает каждый раз
```

### `VirtualizedList` для больших списков

```csharp
// ✅ Для сотен/тысяч элементов — VirtualizedList
// Рендерит только видимые элементы

// ❌ ViewModelObservableListBinder для 1000+ элементов
// Создаст 1000 GameObjects
```

---

## Типичные ошибки

### 1. Забыли `partial`

```csharp
// ❌ Source Generator не работает
[ViewModel]
public class PlayerViewModel { ... }

// ✅ Исправление
[ViewModel]
public partial class PlayerViewModel { ... }
```

**Симптом:** Нет сгенерированных свойств, ошибки компиляции.

### 2. Неправильный BindMode

```csharp
// ❌ TwoWay на readonly данных — View не сможет обновить
[TwoWayBind] private readonly string _title;

// ✅ Используйте OneTime для readonly
[OneTimeBind] private readonly string _title;
```

### 3. Утечки — не вызван Deinitialize

```csharp
// ❌ Утечка — биндеры остаются подписаны
Destroy(viewGameObject);

// ✅ Сначала деинициализация
_view.DeinitializeView()?.DisposeViewModel();
Destroy(viewGameObject);
```

### 4. Не инициализирован git submodule

```bash
# Ошибки компиляции после клонирования
git submodule update --init --recursive
```

### 5. Имя поля View не совпадает с ViewModel

```csharp
// ViewModel:
[OneWayBind] private string _playerName;  // → свойство PlayerName

// View:
[SerializeField] private MonoBinder _name;  // ❌ Ищет "Name", не найдёт "PlayerName"
[SerializeField] private MonoBinder _playerName;  // ✅
```

### 6. Циклические обновления TwoWay

Биндеры из StarterKit содержат защиту от циклов. При создании кастомного TwoWay-биндера:

```csharp
// ✅ Добавьте флаг
private bool _isUpdating;

public void SetValue(string value)
{
    _isUpdating = true;
    _inputField.text = value;
    _isUpdating = false;
}

private void OnValueChanged(string value)
{
    if (_isUpdating) return;
    ValueChanged?.Invoke(value);
}
```

---

## Тестирование

### ViewModel тестируется без Unity

```csharp
[Test]
public void Health_ShouldDecrease_WhenDamaged()
{
    var player = new Player(health: 100);
    var vm = new PlayerViewModel(player);

    player.TakeDamage(30);

    Assert.AreEqual(70, vm.Health);
}
```

### DynamicViewModel для тестов View

```csharp
[Test]
public void View_ShouldBind_WhenInitialized()
{
    var vm = DynamicViewModel.Create(
        new DynamicPropertyData<string>("Name", "Test", BindMode.OneWay)
    );

    _view.Initialize(vm);
    Assert.IsNotNull(_view.ViewModel);
}
```

---

## См. также

- [Архитектура](02-architecture.md) — общая структура
- [ViewModel](04-viewmodels.md) — все атрибуты
- [Анализаторы](13-analyzers.md) — автоматические проверки кода
