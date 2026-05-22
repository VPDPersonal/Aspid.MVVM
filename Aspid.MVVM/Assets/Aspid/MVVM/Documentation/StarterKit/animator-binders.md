# Animator Binders

Биндеры для управления параметрами `Animator`.

---

## Общий принцип

Все Animator-биндеры наследуют `AnimatorSetParameterBinder<T>` и:

1. Принимают имя параметра Animator (`ParameterName`) в Inspector
2. Устанавливают параметр через `Animator.SetBool/SetFloat/SetInt`
3. Проверяют `CanExecute` — по умолчанию `Target.gameObject.activeInHierarchy`
4. Оптимизируют: не вызывают Set, если текущее значение уже совпадает

### Обратная привязка (OneWayToSource)

При OneWayToSource Animator-биндеры предоставляют `Action<T>` или `IRelayCommand<T>` обратно во ViewModel, позволяя вызывать анимации из ViewModel.

**Режимы:** OneWay, OneTime, OneWayToSource (TwoWay запрещён).

---

## AnimatorSetBoolBinder

Привязка `Animator.SetBool`.

| Свойство | Описание |
|----------|----------|
| `ParameterName` | Имя bool-параметра в Animator |
| `_isInvert` | Инвертирует значение |

```csharp
[ViewModel]
public partial class CharacterViewModel
{
    [OneWayBind] private bool _isRunning;
    // Привяжите к AnimatorSetBoolBinder с ParameterName = "IsRunning"
}
```

---

## AnimatorSetFloatBinder

Привязка `Animator.SetFloat`.

| Свойство | Описание |
|----------|----------|
| `ParameterName` | Имя float-параметра в Animator |
| Converter | `IConverter<float, float>` (опционально) |

```csharp
[ViewModel]
public partial class CharacterViewModel
{
    [OneWayBind] private float _speed;
    // Привяжите к AnimatorSetFloatBinder с ParameterName = "Speed"
}
```

---

## AnimatorSetIntBinder

Привязка `Animator.SetInteger`.

| Свойство | Описание |
|----------|----------|
| `ParameterName` | Имя int-параметра в Animator |
| Converter | `IConverter<int, int>` (опционально) |

---

## AnimatorSetTriggerBinder

Привязка `Animator.SetTrigger`. Работает иначе — только **OneWayToSource**.

Предоставляет `Action` или `IRelayCommand` во ViewModel для вызова триггера:

```csharp
[ViewModel]
public partial class CharacterViewModel
{
    [OneWayToSourceBind] private IRelayCommand _jumpTrigger;
    // или: [OneWayToSourceBind] private Action _jumpTrigger;

    public void Jump()
    {
        _jumpTrigger?.Execute();  // → Animator.SetTrigger("Jump")
    }
}
```

**Режим:** только OneWayToSource.

---

## См. также

- [Transform Binders](transform-binders.md) — позиция, поворот, масштаб
- [Обзор StarterKit](README.md)
