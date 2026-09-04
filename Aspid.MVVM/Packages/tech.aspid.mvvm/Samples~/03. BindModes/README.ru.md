# Туториал 3. Bind Modes

Разбор сэмпла `Path 3. Bind Modes` — все четыре режима биндинга на одном экране и конвертер в слоте биндера.

**Предполагается знание:** [Greeter](../02.%20Greeter/README.ru.md).

---

## Что строим

```
OneTime          1.0.0
OneWay           Normal
TwoWay           [────●────] 0.5     [x] Muted
OneWayToSource   [ Profile name...  ]
                 [ Reset ]
```

Файлы: `Samples~/03. BindModes/`.

---

## Четыре режима — четыре поля

```csharp
[ViewModel]
[Serializable]
public sealed partial class AudioSettingsViewModel
{
    [OneTimeBind] private readonly string _version = "1.0.0";

    [BindAlso(nameof(VolumeLabel))]
    [TwoWayBind] [SerializeField] [Range(0f, 1f)] private float _volume = 0.5f;

    [TwoWayBind] [SerializeField] private bool _isMuted;

    [OneWayToSourceBind] private string _profileName;

    private string VolumeLabel => Volume switch { 0f => "Silent", < 0.34f => "Quiet", < 0.67f => "Normal", _ => "Loud" };
}
```

| Режим | Кто пишет | Типичный биндер в сцене |
|---|---|---|
| `OneTime` | никто после первого чтения | `TextMonoBinder` для версии |
| `OneWay` | ViewModel → View | `TextMonoBinder` для `VolumeLabel` |
| `TwoWay` | оба | `SliderValueMonoBinder`, `ToggleIsOnMonoBinder` |
| `OneWayToSource` | View → ViewModel | `InputFieldMonoBinder` для `ProfileName` |

Режим на члене ViewModel — верхняя граница. Биндер в сцене выбирает свой режим в поле **Mode**, но не выше разрешённого: `TwoWay`-слайдер можно повесить на `TwoWay`-свойство, `OneWay`-текст — тоже, а вот `TwoWay`-биндер на `OneWay`-свойство редактор не пропустит.

`readonly`-поле становится `OneTime` само; атрибут `[OneTimeBind]` здесь оставлен для наглядности.

---

## Один член — несколько биндеров

`Volume` привязан дважды: слайдер (`TwoWay`) и текст (`OneWay`). `IsMuted` — тоже дважды: тумблер (`TwoWay`) и `interactable` слайдера (`OneWay`) через `SelectableInteractableMonoBinder` с `BoolInvertConverter` в слоте: пока Muted включён, слайдер выключен.

Так и должно быть: ViewModel описывает состояние один раз, а сколько элементов UI на него смотрят — дело сцены.

---

## Свой конвертер

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

- `[Serializable]` — чтобы конвертер мог лежать в `[SerializeReference]`-слоте биндера.
- `[TypeSelectorDisplay]` — чтобы он появился в выпадающем списке **Converter** в Inspector.
- `ITwoWayConverter` обязателен для `TwoWay`-биндера: без `ConvertBack` значение слайдера вернулось бы во ViewModel неконвертированным, и биндер предупредит об этом в консоли.

Конвертер стоит в слоте `SliderValueMonoBinder`: линейная громкость `0.25` показывается как позиция `0.5`, и обратно.

---

## Резюме

| Концепция | Где |
|---|---|
| Четыре `BindMode` | по одному полю на режим |
| Режим биндера ≤ режима члена | поле **Mode** у каждого биндера в сцене |
| Несколько биндеров на один член | `Volume`, `IsMuted` |
| Встроенный конвертер | `BoolInvertConverter` на `interactable` |
| Свой `ITwoWayConverter` | `PerceptualVolumeConverter` |

Подробнее о режимах — в [Режимах биндинга](../../Documentation/ru/03-binding-modes.md), о конвертерах — в [Конвертерах](../../Documentation/ru/08-converters.md).

## Следующий шаг

[Stats →](../04.%20Stats/README.ru.md) — команды с параметром и `CanExecute`.
