# Animator Binders

Binders that drive `Animator` parameters.

---

## Common behaviour

Every Animator binder inherits `AnimatorSetParameterBinder<T>` and:

1. Takes the Animator parameter name (`ParameterName`) in the Inspector
2. Sets the parameter through `Animator.SetBool/SetFloat/SetInt`
3. Checks `CanExecute`, by default `Target.gameObject.activeInHierarchy`
4. Skips the Set call when the current value already matches

### Reverse binding (OneWayToSource)

In OneWayToSource the Animator binders hand an `Action<T>` or `IRelayCommand<T>` back to the ViewModel, so animations can be triggered from the ViewModel.

**Modes:** OneWay, OneTime, OneWayToSource (TwoWay is not allowed).

---

## AnimatorSetBoolBinder

Binds `Animator.SetBool`.

| Property | Description |
|----------|----------|
| `ParameterName` | Name of the bool parameter in the Animator |
| `_converter` | Optional value converter (for example `BoolInvertConverter`) |

```csharp
[ViewModel]
public partial class CharacterViewModel
{
    [OneWayBind] private bool _isRunning;
    // Bind to AnimatorSetBoolBinder with ParameterName = "IsRunning"
}
```

---

## AnimatorSetFloatBinder

Binds `Animator.SetFloat`.

| Property | Description |
|----------|----------|
| `ParameterName` | Name of the float parameter in the Animator |
| Converter | `IConverter<float, float>` (optional) |

```csharp
[ViewModel]
public partial class CharacterViewModel
{
    [OneWayBind] private float _speed;
    // Bind to AnimatorSetFloatBinder with ParameterName = "Speed"
}
```

---

## AnimatorSetIntBinder

Binds `Animator.SetInteger`.

| Property | Description |
|----------|----------|
| `ParameterName` | Name of the int parameter in the Animator |
| Converter | `IConverter<int, int>` (optional) |

---

## AnimatorSetTriggerBinder

Binds `Animator.SetTrigger`. Works differently: **OneWayToSource** only. Its pair, `AnimatorResetTriggerBinder`, calls `ResetTrigger`: a trigger that was set but never consumed otherwise stays active.

Hands an `Action` or `IRelayCommand` to the ViewModel for firing the trigger:

```csharp
[ViewModel]
public partial class CharacterViewModel
{
    [OneWayToSourceBind] private IRelayCommand _jumpTrigger;
    // or: [OneWayToSourceBind] private Action _jumpTrigger;

    public void Jump()
    {
        _jumpTrigger?.Execute();  // → Animator.SetTrigger("Jump")
    }
}
```

**Mode:** OneWayToSource only.

---

## See also

- [Transform Binders](transform-binders.md), position, rotation, scale
- [StarterKit overview](README.md)
