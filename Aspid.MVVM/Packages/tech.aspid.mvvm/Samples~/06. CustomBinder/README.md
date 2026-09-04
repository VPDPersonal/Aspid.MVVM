# Custom Binder

How to bind a component the StarterKit does not know about.

**You learn:** `ComponentFloatMonoBinder<T>`, `[GenerateSerializableBinder]`, `[AddBinderContextMenu]`, non-silent validation, a scene without a `[View]` class.

**Assumes:** [Todo List](../05.%20TodoList/README.md).

Scene: `Scenes/Custom Binder.unity`.

| File | Role |
|---|---|
| `Scripts/Components/HealthBar.cs` | A plain UI component with a `Value` property. |
| `Scripts/Binders/HealthBarValueMonoBinder.cs` | The binder. |
| `Scripts/ViewModels/HeroViewModel.cs` | The ViewModel the scene binds to, edited inside `ViewInitializer`. |

## What we build

```
Hero
[████████████░░░░░░░░]  70%
[ Hit ]   [ Heal ]
```

`HealthBar` is an ordinary project component: a filled `Image`, a label and a color gradient. The task is to bind its `Value` to `Health` on the ViewModel.

## A component without MVVM

```csharp
public sealed class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _fill;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private Gradient _gradient = new();

    public float Value
    {
        get => _value;
        set
        {
            _value = Mathf.Clamp01(value);
            _fill.fillAmount = _value;
            _fill.color = _gradient.Evaluate(_value);
            _label.text = $"{Mathf.RoundToInt(_value * 100f)}%";
        }
    }
}
```

The component knows nothing about the framework. The binder is an adapter on the outside, not a dependency on the inside.

## The binder

```csharp
[GenerateSerializableBinder]
[AddBinderContextMenu(typeof(HealthBar))]
[AddComponentMenu("Aspid/MVVM/Binders/Samples/Health Bar Binder – Value")]
public class HealthBarValueMonoBinder : ComponentFloatMonoBinder<HealthBar>
{
    protected sealed override float Property
    {
        get => CachedComponent.Value;
        set => CachedComponent.Value = this.SafeClamp01(value);
    }
}
```

All you write is `Property`. The rest comes from the base and the attributes:

| Element | What it gives |
|---|---|
| `ComponentFloatMonoBinder<T>` | `IBinder<float>` plus `int`, `long`, `double`, a converter slot, a cached component, `OneWayToSource` support |
| `[GenerateSerializableBinder]` | the generator emits `HealthBarValueBinder`, a serializable twin for fields of `[View]` classes |
| `[AddBinderContextMenu(typeof(HealthBar))]` | an "Add Binder" entry in the `HealthBar` component's context menu |
| `[AddComponentMenu]` | the place in the Add Component menu |
| `this.SafeClamp01(value)` | a value outside `0..1` is not swallowed: an error is logged and the nearest valid value applied |

The class is not `sealed`: the generated twin and project subclasses must be able to extend it.

### Which base to pick

| Property type | Base |
|---|---|
| `float` / `int` | `ComponentFloatMonoBinder<T>` / `ComponentIntMonoBinder<T>` |
| `UnityEngine.Object` (`Sprite`, `Material`, …) | `ComponentObjectMonoBinder<T, TObject>` |
| anything else | `ComponentMonoBinder<T, TValue>` |
| convert and pass on | `CasterMonoBinder<TFrom, TTo>` |

## Scene

There is no `[View]` class. A plain `MonoView` lists binders by id (`Health`, `HitCommand`, `HealCommand`), and `ViewInitializer` holds a `[Serializable]` `HeroViewModel` edited right in the Inspector. One screen, zero View scripts.

```csharp
[ViewModel]
[Serializable]
public sealed partial class HeroViewModel
{
    [OneWayBind]
    [SerializeField] [Range(0f, 1f)] private float _health = 1f;

    [RelayCommand]
    private void Hit() => Health = Mathf.Max(0f, Health - 0.15f);

    [RelayCommand]
    private void Heal() => Health = Mathf.Min(1f, Health + 0.25f);
}
```

## Summary

| Concept | Where |
|---|---|
| Binder = `Property` over `CachedComponent` | `HealthBarValueMonoBinder` |
| Serializable twin from the generator | `[GenerateSerializableBinder]` |
| Component context menu | `[AddBinderContextMenu]` |
| Validation that speaks up | `SafeClamp01` |
| `MonoView` + `ViewInitializer` without code | scene `Custom Binder` |

Full binder rules are in [Binders](../../Documentation/06-binders.md).

Text uses TextMeshPro (part of `com.unity.ugui`). The sample ships its own font asset in `Fonts/` (Liberation Sans, OFL), so it does not depend on the fonts from TMP Essentials.
