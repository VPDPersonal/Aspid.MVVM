# Best Practices

Recommendations for using Aspid.MVVM and the mistakes people make most often.

## Contents

- [Project structure](#project-structure)
- [ViewModel](#viewmodel)
- [Bindings](#bindings)
- [Commands](#commands)
- [Collections](#collections)
- [Performance](#performance)
- [Common mistakes](#common-mistakes)
- [Testing](#testing)

---

## Project structure

Recommended layout:

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
    Binders/          (custom ones, if any)
      HealthBarBinder.cs
```

Group by feature, not by type (all ViewModels in one folder).

---

## ViewModel

### Prefer POCO ViewModels

```csharp
// ✅ Good: plain C# without Unity dependencies
[ViewModel]
public partial class PlayerViewModel
{
    [OneWayBind] private int _health;
}

// ⚠️ Only when Inspector editing is needed
[ViewModel]
public partial class SettingsViewModel : MonoViewModel
{
    [SerializeField] [OneWayBind] private float _volume;
}
```

### Keep the ViewModel clean

A ViewModel should not hold Unity-specific logic:

```csharp
// ❌ Bad: a Unity dependency in the ViewModel
[ViewModel]
public partial class BadViewModel
{
    [OneWayBind] private string _text;

    public void Update() => Text = Time.deltaTime.ToString(); // Do NOT do this
}

// ✅ Good: business logic in the Model
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

### Use `partial void OnXxxChanged` instead of external subscriptions

```csharp
// ✅ Good: react to a change through the handler
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

## Bindings

### Pick the least powerful mode that works

```csharp
// ✅ OneWay for display (no feedback needed)
[OneWayBind] private int _score;

// ✅ OneTime for static data and commands
[OneTimeBind] private IRelayCommand _save;
[OneTimeBind] private string _title;

// ✅ TwoWay only for interactive elements
[TwoWayBind] private string _inputText;
```

### Use converters instead of changing the ViewModel

```csharp
// ❌ Bad: formatting in the ViewModel
[OneWayBind] private string _healthText;
Health = 75;
HealthText = $"HP: {Health}/100";

// ✅ Good: a converter on the binder
[OneWayBind] private int _health;
// Inspector: TextBinder + StringFormatConverter (Format = "HP: {0}/100")
```

---

## Commands

### Use `[RelayCommand]` instead of manual creation

```csharp
// ✅ Good
[RelayCommand(CanExecute = nameof(CanSave))]
private void Save() => _storage.Save();
private bool CanSave() => _storage.HasChanges;

// ⚠️ Manual creation only for special cases
[Bind] private readonly IRelayCommand _legacyCommand = new RelayCommand(...);
```

### Do not forget `NotifyCanExecuteChanged`

```csharp
partial void OnIsDirtyChanged(bool newValue)
{
    // ✅ Required, otherwise the button does not update
    SaveCommand.NotifyCanExecuteChanged();
}
```

---

## Collections

### `CreateSync` for Model → ViewModel

```csharp
// ✅ Good: automatic synchronization
_viewModels = _models.CreateSync(model => new ItemViewModel(model));

// ❌ Bad: manual synchronization
_models.CollectionChanged += (_, args) => {
    // Do not do this: long and error-prone
};
```

### FilteredList instead of LINQ

```csharp
// ✅ Good: reactive filtering
var filtered = new FilteredList<ItemViewModel>(_items)
{
    Filter = item => item.IsVisible
};

// ❌ Bad: non-reactive LINQ
var filtered = _items.Where(x => x.IsVisible).ToList();
```

### Do not forget Dispose

```csharp
// ✅ A FilteredList must be disposed
public void Dispose()
{
    _filteredList.Dispose();
}
```

---

## Performance

### `NotifyAll()` for bulk updates

```csharp
// ✅ Good: one notification instead of N
_health = data.Health;
_name = data.Name;
_level = data.Level;
NotifyAll(); // Once for everything
```

### `PrefabViewPool` for frequent create/destroy

```csharp
// ✅ Good: the pool reuses objects
// Inspector: ViewModelObservableListBinder → Factory = PrefabViewPool

// ⚠️ PrefabViewFactory creates/destroys every time
```

### `VirtualizedList` for large lists

```csharp
// ✅ Hundreds/thousands of items: VirtualizedList
// Renders only the visible items

// ❌ ViewModelObservableListBinder for 1000+ items
// Creates 1000 GameObjects
```

---

## Common mistakes

### 1. Missing `partial`

```csharp
// ❌ The Source Generator does not run
[ViewModel]
public class PlayerViewModel { ... }

// ✅ Fix
[ViewModel]
public partial class PlayerViewModel { ... }
```

**Symptom:** no generated properties, compile errors.

### 2. Wrong BindMode

```csharp
// ❌ TwoWay on readonly data: the View cannot update it
[TwoWayBind] private readonly string _title;

// ✅ Use OneTime for readonly
[OneTimeBind] private readonly string _title;
```

### 3. Leaks: Deinitialize was not called

```csharp
// ❌ Leak: binders stay subscribed
Destroy(viewGameObject);

// ✅ Deinitialize first
_view.DeinitializeView()?.DisposeViewModel();
Destroy(viewGameObject);
```

### 4. Git submodule not initialized

```bash
# Compile errors after cloning
git submodule update --init --recursive
```

### 5. View field name does not match the ViewModel

```csharp
// ViewModel:
[OneWayBind] private string _playerName;  // → property PlayerName

// View:
[SerializeField] private MonoBinder _name;  // ❌ Looks for "Name", will not find "PlayerName"
[SerializeField] private MonoBinder _playerName;  // ✅
```

### 6. TwoWay update loops

StarterKit binders have loop protection. When writing a custom TwoWay binder:

```csharp
// ✅ Add a flag
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

## Testing

### A ViewModel is tested without Unity

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

### DynamicViewModel for View tests

```csharp
[Test]
public void View_ShouldBind_WhenInitialized()
{
    var vm = new DynamicViewModel
    {
        { "Name", "Test" }
    };

    _view.Initialize(vm);
    Assert.IsNotNull(_view.ViewModel);
}
```

---

## See also

- [Architecture](02-architecture.md), the overall structure
- [ViewModels](04-viewmodels.md), every attribute
- [Analyzers](13-analyzers.md), automatic code checks
