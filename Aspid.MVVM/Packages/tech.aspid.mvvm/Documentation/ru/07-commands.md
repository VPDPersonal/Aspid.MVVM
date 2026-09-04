# Команды

Команды (`IRelayCommand`) инкапсулируют действия с поддержкой `CanExecute`. Атрибут `[RelayCommand]` генерирует команду из обычного метода.

## Содержание

- [Обзор](#обзор)
- [IRelayCommand](#irelaycommand)
- [Атрибут \[RelayCommand\]](#атрибут-relaycommand)
- [CanExecute](#canexecute)
- [Параметризованные команды](#параметризованные-команды)
- [Ручное создание](#ручное-создание)
- [RelayCommand.Empty](#relaycommandempty)

---

## Обзор

Команда — это объект, который:
- Выполняет действие (`Execute`)
- Определяет, доступно ли действие (`CanExecute`)
- Уведомляет об изменении доступности (`CanExecuteChanged`)

В Aspid.MVVM команды привязываются к UI через `ButtonCommandBinder`.

---

## IRelayCommand

```csharp
public interface IRelayCommand
{
    event Action<IRelayCommand>? CanExecuteChanged;
    bool CanExecute();
    void Execute();
    void NotifyCanExecuteChanged();
}
```

Параметризованные варианты — до 4 параметров:

| Интерфейс | Сигнатура Execute |
|-----------|------------------|
| `IRelayCommand` | `void Execute()` |
| `IRelayCommand<T>` | `void Execute(T arg)` |
| `IRelayCommand<T1, T2>` | `void Execute(T1, T2)` |
| `IRelayCommand<T1, T2, T3>` | `void Execute(T1, T2, T3)` |
| `IRelayCommand<T1, T2, T3, T4>` | `void Execute(T1, T2, T3, T4)` |

---

## Атрибут [RelayCommand]

Генерирует `IRelayCommand`-свойство из метода:

```csharp
[ViewModel]
public partial class PlayerViewModel
{
    [RelayCommand]
    private void Attack()
    {
        _player.Attack();
    }
    // → Генерируется: IRelayCommand AttackCommand { get; }

    [RelayCommand]
    private void Heal(int amount)
    {
        _player.Heal(amount);
    }
    // → Генерируется: IRelayCommand<int> HealCommand { get; }
}
```

**Соглашение:** Из метода `DoSomething()` генерируется свойство `DoSomethingCommand`.

---

## CanExecute

Три способа определить условие доступности:

### 1. Метод bool

```csharp
[ViewModel]
public partial class FormViewModel
{
    [Bind] private string _text;

    [RelayCommand(CanExecute = nameof(CanSubmit))]
    private void Submit() { /* ... */ }

    private bool CanSubmit() => !string.IsNullOrEmpty(Text);
}
```

### 2. Метод bool с теми же параметрами

```csharp
[ViewModel]
public partial class MathViewModel
{
    [RelayCommand(CanExecute = nameof(CanDivide))]
    private void Divide(int a, int b)
    {
        Result = a / b;
    }

    private bool CanDivide(int a, int b) => b != 0;
}
```

### 3. Bool-свойство / поле

```csharp
[ViewModel]
public partial class StatsViewModel
{
    [OneWayBind] private bool _isDraft;

    [RelayCommand(CanExecute = nameof(IsDraft))]
    private void Confirm() { /* ... */ }

    [RelayCommand(CanExecute = nameof(IsDraft))]
    private void ResetToDefault() { /* ... */ }

    // При изменении IsDraft — обновляем доступность команд
    partial void OnIsDraftChanged(bool newValue)
    {
        ConfirmCommand.NotifyCanExecuteChanged();
        ResetToDefaultCommand.NotifyCanExecuteChanged();
    }
}
```

> **Важно:** Не забывайте вызывать `NotifyCanExecuteChanged()` при изменении условия. Без этого `ButtonCommandBinder` не обновит состояние кнопки.

---

## Параметризованные команды

Поддерживается до 4 параметров:

```csharp
[ViewModel]
public partial class CommandsExample
{
    // 0 параметров → IRelayCommand
    [RelayCommand]
    private void Do0() { }

    // 1 параметр → IRelayCommand<int>
    [RelayCommand]
    private void Do1(int arg1) { }

    // 2 параметра → IRelayCommand<int, string>
    [RelayCommand]
    private void Do2(int arg1, string arg2) { }

    // 3 параметра → IRelayCommand<int, string, float>
    [RelayCommand]
    private void Do3(int arg1, string arg2, float arg3) { }

    // 4 параметра → IRelayCommand<int, string, float, bool>
    [RelayCommand]
    private void Do4(int arg1, string arg2, float arg3, bool arg4) { }
}
```

Параметры передаются из `ButtonCommandBinder<T>` через сериализованные поля в Inspector.

---

## Ручное создание

Для случаев, когда `[RelayCommand]` не подходит:

```csharp
[ViewModel]
public partial class ManualCommandViewModel
{
    // Создаём вручную, привязываем через [Bind]
    [Bind] private readonly IRelayCommand _saveCommand;
    [Bind] private readonly IRelayCommand _deleteCommand;

    public ManualCommandViewModel(IStorage storage)
    {
        _saveCommand = new RelayCommand(
            execute: () => storage.Save(),
            canExecute: () => storage.HasChanges
        );

        _deleteCommand = new RelayCommand(
            execute: () => storage.Delete(),
            canExecute: () => storage.CanDelete
        );
    }
}
```

> Поле `readonly` с `[Bind]` автоматически получает режим **OneTime**.

---

## RelayCommand.Empty

Статические заглушки для случаев, когда команда не нужна:

```csharp
// Команда, которую нельзя выполнить (CanExecute = false)
IRelayCommand disabled = RelayCommand.Empty;

// Команда, которую можно выполнить, но она ничего не делает
IRelayCommand noop = RelayCommand.EmptyExecution;
```

---

## Связь с ButtonCommandBinder

`ButtonCommandBinder` привязывает `IRelayCommand` к `Button.onClick`:

```csharp
// В ViewModel:
[RelayCommand]
private void Save() { /* ... */ }

// В View:
[SerializeField] private MonoBinder _saveCommand;

// В Inspector: назначьте ButtonCommandBinder на кнопку
// и перетащите его в поле _saveCommand
```

Подробнее: [ButtonCommandBinder](StarterKit/button-command-binders.md).

---

## См. также

- [ViewModel](04-viewmodels.md) — `[RelayCommand]` в контексте ViewModel
- [ButtonCommandBinder](StarterKit/button-command-binders.md) — привязка к кнопке
- [Режимы привязки](03-binding-modes.md) — OneTime для команд
