# Туториал 4. Stats

Разбор сэмпла `Path 4. Stats` — команда с параметром, `CanExecute`, черновик, который попадает в модель только по Confirm.

**Предполагается знание:** [Greeter](../02.%20Greeter/README.ru.md).

---

## Что строим

```
Strength      [-]  3  [+]
Agility       [-]  1  [+]
Intelligence  [-]  2  [+]
Points available: 4
[ Confirm ]  [ Reset ]
```

Кнопки `+`/`-` меняют черновик. `Confirm` отдаёт черновик модели, `Reset` возвращает значения модели. Обе кнопки активны только пока черновик отличается от модели.

Файлы: `Samples~/04. Stats/`.

---

## Модель

```csharp
public sealed class Hero
{
    public event Action Changed;

    public int PointsAvailable { get; private set; }

    public int this[Skill skill] => _skills[skill];

    public void Apply(IReadOnlyDictionary<Skill, int> skills, int pointsAvailable)
    {
        // validation, then:
        PointsAvailable = pointsAvailable;
        Changed?.Invoke();
    }
}
```

Модель ничего не знает о MVVM. Правила (минимум очков, бюджет) живут в ней, а не во ViewModel.

---

## ViewModel

### Команда с параметром

```csharp
[RelayCommand(CanExecute = nameof(CanAdd))]
private void Add(Skill skill)
{
    Set(skill, Get(skill) + 1);
    PointsAvailable--;
}

private bool CanAdd(Skill skill) =>
    PointsAvailable > 0;
```

Генератор создаёт `AddCommand : IRelayCommand<Skill>`. Метод `CanExecute` принимает те же параметры, что и команда — или ни одного, если параметр для решения не нужен.

### CanExecute от свойства

```csharp
[OneWayBind] private bool _isDraft;

[RelayCommand(CanExecute = nameof(IsDraft))]
private void Confirm() { /* ... */ }
```

В `CanExecute` можно указать `bool`-свойство. Но команда не узнаёт об изменении сама — об этом нужно сообщить:

```csharp
partial void OnIsDraftChanged(bool newValue)
{
    ConfirmCommand.NotifyCanExecuteChanged();
    ResetCommand.NotifyCanExecuteChanged();
}
```

`ButtonCommandMonoBinder` слушает это уведомление и переключает `interactable` кнопки.

### Черновик и модель

```csharp
public StatsViewModel(Hero hero)
{
    _hero = hero;
    _hero.Changed += Reset;   // модель изменилась — черновик сбрасывается к ней
    Reset();
}

public void Dispose() =>
    _hero.Changed -= Reset;
```

`Confirm` вызывает `_hero.Apply(...)`, модель поднимает `Changed`, тот вызывает `Reset`, и `IsDraft` становится `false`. ViewModel не дублирует логику «применить», она просит модель.

---

## Биндер для параметра-enum

StarterKit поставляет `ButtonCommandMonoBinder<T>` и закрытые версии для `int`, `float`, `string`, `bool`, `Object`. Для собственного enum закрытие занимает одну строку:

```csharp
[AddComponentMenu("Aspid/MVVM/Binders/Samples/Button Binder – Skill Command")]
public sealed class ButtonCommandSkillMonoBinder : ButtonCommandMonoBinder<Skill> { }
```

Значение `Skill` для каждой кнопки задаётся в Inspector в поле `Param`.

---

## Два ViewModel над одной моделью

```csharp
_editView.Initialize(new StatsViewModel(hero));
_committedView.Initialize(new StatsViewModel(hero));
```

Вторая панель без кнопок показывает только то, что уже в модели: её `StatsViewModel` подписан на `Hero.Changed` и обновляется после `Confirm` в первой.

---

## Резюме

| Концепция | Где |
|---|---|
| `IRelayCommand<T>` | `Add(Skill)`, `Remove(Skill)` |
| `CanExecute` методом с параметром | `CanAdd`, `CanRemove` |
| `CanExecute` свойством + `NotifyCanExecuteChanged` | `IsDraft`, `OnIsDraftChanged` |
| Модель с событием и `IDisposable` во ViewModel | `Hero.Changed`, `Dispose` |
| Закрытие generic-биндера | `ButtonCommandSkillMonoBinder` |

## Следующий шаг

[TodoItem →](../05.%20TodoList/README.ru.md) — дочерний ViewModel над моделью, затем [TodoList](../05.%20TodoList/README.ru.md).
