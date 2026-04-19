# Туториал: Stats

Разбор Sample-проекта Stats — параметризованные команды, CanExecute, черновик/подтверждение.

---

## Что мы разбираем

Экран распределения очков навыков героя. Кнопки +/- для каждого навыка, CanExecute для контроля доступности, черновик с подтверждением/сбросом.

Файлы: `Samples/Stats/`

---

## Модель

### Skill (enum)

```csharp
public enum Skill
{
    Power, Intelligence, Reflexes, TechnicalAbility, Cool
}
```

### Hero

Модель героя с пулом очков и валидацией:

```csharp
public class Hero
{
    public event Action<Skill> SkillChanged;
    public int SkillPointsAvailable { get; private set; }

    public void SetSkillPointTo(Skill skill, int value) { /* ... */ }
    public int GetNumberSkillPointFrom(Skill skill) => _skills[skill];
}
```

---

## ViewModel

### Привязка навыков

```csharp
[ViewModel]
public sealed partial class StatsViewModel : IDisposable
{
    [OneWayBind] private int _cool;
    [OneWayBind] private int _power;
    [OneWayBind] private int _reflexes;
    [OneWayBind] private int _intelligence;
    [OneWayBind] private int _technicalAbility;
    [OneWayBind] private int _skillPointsAvailable;
    [OneWayBind] private bool _isDraft;

    private readonly Hero _hero;
    // ...
}
```

### Команды с CanExecute

#### Confirm / ResetToDefault — проверка по свойству

```csharp
[RelayCommand(CanExecute = nameof(IsDraft))]
private void Confirm() { /* ... */ }

[RelayCommand(CanExecute = nameof(IsDraft))]
private void ResetToDefault() { /* ... */ }
```

`CanExecute = nameof(IsDraft)` — кнопка активна только если `IsDraft == true`.

#### Параметризованная команда AddSkillPointTo — проверка по методу

```csharp
[RelayCommand(CanExecute = nameof(CanAddSkillPointTo))]
private void AddSkillPointTo(Skill skill)
{
    if (SkillPointsAvailable == 0) return;
    SetSkillPointsTo(skill, GetNumberSkillPointFrom(skill) + 1);
    SkillPointsAvailable--;
}

private bool CanAddSkillPointTo() =>
    SkillPointsAvailable > 0;
```

Генерируется `IRelayCommand<Skill> AddSkillPointToCommand`.

#### RemoveSkillPointTo — CanExecute зависит от параметра

```csharp
[RelayCommand(CanExecute = nameof(CanRemoveSkillPointTo))]
private void RemoveSkillPointTo(Skill skill) { /* ... */ }

private bool CanRemoveSkillPointTo(Skill skill) =>
    GetNumberSkillPointFrom(skill) != _hero.GetNumberSkillPointFrom(skill);
```

`CanRemoveSkillPointTo(Skill)` — принимает тот же параметр, что и команда.

### Уведомление об изменении CanExecute

```csharp
partial void OnIsDraftChanged(bool newValue)
{
    ConfirmCommand.NotifyCanExecuteChanged();
    ResetToDefaultCommand.NotifyCanExecuteChanged();
}

partial void OnSkillPointsAvailableChanged(int newValue)
{
    IsDraft = newValue != _hero.SkillPointsAvailable;
    AddSkillPointToCommand.NotifyCanExecuteChanged();
    RemoveSkillPointToCommand.NotifyCanExecuteChanged();
}
```

Вызовите `NotifyCanExecuteChanged()` когда условия CanExecute могут измениться.

---

## Views

### ReadOnlyStatsView — только отображение

```csharp
[View]
public partial class ReadOnlyStatsView : MonoView, IView<StatsViewModel>
{
    [RequireBinder(typeof(int))]
    [SerializeField] private MonoBinder[] _cool;
    // ... остальные навыки
    [SerializeField] private MonoBinder[] _skillPointsAvailable;
}
```

`IView<StatsViewModel>` — типизированный интерфейс, ограничивает привязку к конкретному типу ViewModel.

### EditStatsView — с командами

```csharp
[View]
public sealed partial class EditStatsView : ReadOnlyStatsView
{
    [SerializeField] private ButtonCommandBinder[] _confirmCommand;
    [SerializeField] private ButtonCommandBinder[] _resetToDefaultCommand;
    [SerializeField] private ButtonCommandBinder<Skill>[] _addSkillPointToCommand;
    [SerializeField] private ButtonCommandBinder<Skill>[] _removeSkillPointToCommand;
}
```

- `ButtonCommandBinder` — для команд без параметров
- `ButtonCommandBinder<Skill>` — для параметризованных команд (параметр задаётся в Inspector)
- Наследование View: `EditStatsView` наследует привязки `ReadOnlyStatsView`

### Bootstrap

```csharp
private void Awake()
{
    var hero = new Hero(_skillPointsAvailable);
    _editStatsView.Initialize(new StatsViewModel(hero));
    _readOnlyStatsView.Initialize(new StatsViewModel(hero));
}
```

Два View привязаны к разным ViewModel, но к одному Hero.

---

## Ключевые паттерны

### 1. CanExecute с тремя подходами

| Подход | Пример | Когда использовать |
|--------|--------|-------------------|
| Свойство `bool` | `CanExecute = nameof(IsDraft)` | Простое условие-флаг |
| Метод без параметров | `CanAddSkillPointTo()` | Условие не зависит от параметра |
| Метод с параметром | `CanRemoveSkillPointTo(Skill)` | Условие зависит от параметра команды |

### 2. NotifyCanExecuteChanged

Всегда вызывайте `Command.NotifyCanExecuteChanged()` когда условия CanExecute могут измениться. Удобно делать это в `OnXxxChanged` обработчиках.

### 3. Наследование View

`EditStatsView : ReadOnlyStatsView` — можно переиспользовать привязки базового View.

### 4. InteractableMode

В Inspector `ButtonCommandBinder` поддерживает `InteractableMode`:
- `Interactable` — кнопка серая когда `CanExecute = false`
- `Visible` — кнопка скрыта когда `CanExecute = false`

---

## См. также

- [Команды](../07-commands.md) — полная документация IRelayCommand
- [Button Command Binders](../StarterKit/button-command-binders.md) — InteractableMode
- [Туториал TodoList](todo-list.md) — коллекции и фильтрация
