# Stats

Commands with a parameter, `CanExecute`, and a draft that reaches the model only on Confirm.

**You learn:** `IRelayCommand<T>`, `CanExecute` by method and by property, `NotifyCanExecuteChanged`, a model with an event, `IDisposable` ViewModel, closing a generic binder.

**Assumes:** [Greeter](../02.%20Greeter/README.md).

Scene: `Scenes/Stats.unity`.

| File | Role |
|---|---|
| `Scripts/Models/Hero.cs` | The model owns the rules and raises `Changed`. |
| `Scripts/ViewModels/StatsViewModel.cs` | Draft over the model, commands, `CanExecute`. |
| `Scripts/Binders/ButtonCommandSkillMonoBinder.cs` | `ButtonCommandMonoBinder<T>` closed over the project enum. |
| `Scripts/Bootstrap.cs` | Two ViewModels over one model. |

## What we build

```
Strength      [-]  3  [+]
Agility       [-]  1  [+]
Intelligence  [-]  2  [+]
Points available: 4
[ Confirm ]  [ Reset ]
```

`+`/`-` edit a draft. `Confirm` hands the draft to the model, `Reset` restores the model's values. Both are enabled only while the draft differs from the model.

## Model

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

The model knows nothing about MVVM. Rules (minimum per skill, budget) live here, not in the ViewModel.

## ViewModel

### Command with a parameter

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

The generator emits `AddCommand : IRelayCommand<Skill>`. The `CanExecute` method takes the same parameters as the command, or none when the decision does not need them.

### `CanExecute` from a property

```csharp
[OneWayBind] private bool _isDraft;

[RelayCommand(CanExecute = nameof(IsDraft))]
private void Confirm() { /* ... */ }

partial void OnIsDraftChanged(bool newValue)
{
    ConfirmCommand.NotifyCanExecuteChanged();
    ResetCommand.NotifyCanExecuteChanged();
}
```

A `bool` property works as `CanExecute`, but the command does not watch it. Call `NotifyCanExecuteChanged()` when it changes; `ButtonCommandMonoBinder` listens and toggles the button's `interactable`.

### Draft and model

```csharp
public StatsViewModel(Hero hero)
{
    _hero = hero;
    _hero.Changed += Reset;   // model changed → draft snaps back to it
    Reset();
}

public void Dispose() =>
    _hero.Changed -= Reset;
```

`Confirm` calls `_hero.Apply(...)`, the model raises `Changed`, which runs `Reset`, and `IsDraft` becomes `false`. The ViewModel does not duplicate "apply" logic. It asks the model.

The subscription is why the ViewModel is `IDisposable`. `DeinitializeView()?.DisposeViewModel()` in `Bootstrap` unsubscribes it.

## A binder for an enum parameter

StarterKit ships `ButtonCommandMonoBinder<T>` and closed versions for `int`, `float`, `string`, `bool` and `Object`. Closing it over your own enum is one line:

```csharp
[AddComponentMenu("Aspid/MVVM/Binders/Samples/Button Binder – Skill Command")]
public sealed class ButtonCommandSkillMonoBinder : ButtonCommandMonoBinder<Skill> { }
```

Each button sets its `Skill` in the **Param** field in the Inspector.

## Two ViewModels over one model

```csharp
_editView.Initialize(new StatsViewModel(hero));
_committedView.Initialize(new StatsViewModel(hero));
```

The second panel has no buttons and shows only what is already in the model. Its `StatsViewModel` is subscribed to `Hero.Changed`, so it refreshes after `Confirm` in the first one.

## Summary

| Concept | Where |
|---|---|
| `IRelayCommand<T>` | `Add(Skill)`, `Remove(Skill)` |
| `CanExecute` by method with a parameter | `CanAdd`, `CanRemove` |
| `CanExecute` by property + `NotifyCanExecuteChanged` | `IsDraft`, `OnIsDraftChanged` |
| Model event + `IDisposable` ViewModel | `Hero.Changed`, `Dispose` |
| Closing a generic binder | `ButtonCommandSkillMonoBinder` |

Next: [Todo List](../05.%20TodoList/README.md), collections and child ViewModels.

Text uses TextMeshPro (part of `com.unity.ugui`). The sample ships its own font asset in `Fonts/` (Liberation Sans, OFL), so it does not depend on the fonts from TMP Essentials.
