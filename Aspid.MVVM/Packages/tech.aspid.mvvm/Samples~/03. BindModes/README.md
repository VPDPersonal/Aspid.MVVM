# Bind Modes

Every `BindMode` on one screen, plus a converter of your own in a binder slot.

**You learn:** `OneTime`, `OneWay`, `TwoWay`, `OneWayToSource`; several binders on one member; `ITwoWayConverter`.

**Assumes:** [Greeter](../02.%20Greeter/README.md).

Scene: `Scenes/Bind Modes.unity`. Scripts: `Scripts/ViewModels/AudioSettingsViewModel.cs`, `Scripts/Converters/PerceptualVolumeConverter.cs`.

## What we build

```
OneTime          1.0.0
OneWay           Normal
TwoWay           [────●────] 0.5     [x] Muted
OneWayToSource   [ Profile name...  ]
                 [ Reset ]
```

## Four modes, four fields

```csharp
[ViewModel]
[Serializable]
public sealed partial class AudioSettingsViewModel
{
    [OneTimeBind] private readonly string _version = "1.0.0";

    [BindAlso(nameof(VolumeLabel))]
    [TwoWayBind]
    [SerializeField] [Range(0f, 1f)] private float _volume = 0.5f;

    [TwoWayBind]
    [SerializeField] private bool _isMuted;

    [OneWayToSourceBind] private string _profileName;

    [RelayCommand]
    private void Reset()
    {
        Volume = 0.5f;
        IsMuted = false;
    }

    private string VolumeLabel => Volume switch
    {
        0f => "Silent",
        < 0.34f => "Quiet",
        < 0.67f => "Normal",
        _ => "Loud",
    };

    partial void OnProfileNameChanged(string newValue) =>
        Debug.Log($"Profile name is now \"{newValue}\"");
}
```

| Member | Mode | Who writes | Binder in the scene |
|---|---|---|---|
| `Version` | `OneTime` | nobody after the first read | Text |
| `VolumeLabel` | `OneWay` | ViewModel → View | Text |
| `Volume` | `TwoWay` | both | Slider, Text |
| `IsMuted` | `TwoWay` | both | Toggle, slider `interactable` |
| `ProfileName` | `OneWayToSource` | View → ViewModel | Input field |

The mode declared on the ViewModel member is the upper bound. Each binder picks its own mode in the **Mode** field, but never above what the member allows: a `OneWay` text on a `TwoWay` property is fine, a `TwoWay` slider on a `OneWay` property is rejected by the editor.

A `readonly` field becomes `OneTime` on its own. `[OneTimeBind]` is spelled out here for clarity.

## One member, several binders

`Volume` is bound twice: the slider (`TwoWay`) and a label (`OneWay`). `IsMuted` is bound twice as well: the toggle (`TwoWay`) and the slider's `interactable` through `SelectableInteractableMonoBinder` with a `BoolInvertConverter` in its slot. While muted, the slider is disabled.

The ViewModel describes state once. How many UI elements look at it is up to the scene.

## Your own converter

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Samples/Bind Modes", Name = "Perceptual Volume")]
public sealed class PerceptualVolumeConverter : ITwoWayConverter<float, float>
{
    [SerializeField] [Min(0.1f)] private float _exponent = 2f;

    public float Convert(float value) => Mathf.Pow(Mathf.Clamp01(value), 1f / _exponent);
    public float ConvertBack(float value) => Mathf.Pow(Mathf.Clamp01(value), _exponent);
}
```

- `[Serializable]` lets the converter live in a binder's `[SerializeReference]` slot.
- `[TypeSelectorDisplay]` puts it into the **Converter** dropdown in the Inspector.
- `ITwoWayConverter` is required for a `TwoWay` binder. Without `ConvertBack` the slider value would reach the ViewModel unconverted, and the binder warns about it in the console.

It sits in the `SliderValueMonoBinder` slot: linear volume `0.25` shows as slider position `0.5` and back.

## Summary

| Concept | Where |
|---|---|
| Four `BindMode`s | one field per mode |
| Binder mode ≤ member mode | the **Mode** field of every binder |
| Several binders per member | `Volume`, `IsMuted` |
| Built-in converter | `BoolInvertConverter` on `interactable` |
| Own `ITwoWayConverter` | `PerceptualVolumeConverter` |

More in [Binding Modes](../../Documentation/03-binding-modes.md) and [Converters](../../Documentation/08-converters.md).

Next: [Stats](../04.%20Stats/README.md), commands with a parameter and `CanExecute`.

Text uses TextMeshPro (part of `com.unity.ugui`). The sample ships its own font asset in `Fonts/` (Liberation Sans, OFL), so it does not depend on the fonts from TMP Essentials.
