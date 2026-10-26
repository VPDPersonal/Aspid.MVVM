# Image Binders

Binders for the Unity UI `Image` component.

---

## ImageSpriteBinder

Binds a sprite to `Image.sprite`.

| Interface | Data type |
|-----------|-----------|
| `IBinder<Sprite?>` | Sets the sprite directly |
| `IBinder<Texture2D?>` | Converts a `Texture2D` to a `Sprite` through `Sprite.Create` |

### Inspector properties

| Property | Description |
|----------|----------|
| `_disabledWhenNull` | Disables the `Image` component when the sprite is `null` |

A sprite created from a `Texture2D` belongs to the binder and is destroyed on unbind.

**Modes:** OneWay, OneTime, OneWayToSource.

```csharp
[ViewModel]
public partial class PlayerViewModel
{
    [OneWayBind] private Sprite _avatar;
    [OneWayBind] private Texture2D _downloadedImage;  // Converted to a Sprite automatically
}
```

---

## ImageSpriteSwitcherBinder

`bool` → one of two sprites.

| Property | Description |
|----------|----------|
| True Value | Sprite for `true` |
| False Value | Sprite for `false` |

**Modes:** OneWay, OneTime.

---

## ImageFillBinder

Binds `Image.fillAmount` (0-1):

```csharp
[ViewModel]
public partial class HealthBarViewModel
{
    [OneWayBind] private float _healthRatio;  // 0.0 - 1.0
}
```

The value is clamped to [0, 1]. Implements `INumberBinder`: accepts `int`, `float`, `long`, `double`.

**Modes:** OneWay, OneTime, OneWayToSource.

---

## ImageFillSwitcherBinder

`bool` → one of two `fillAmount` values.

---

## ImageSpriteAddressableMonoBinder

Loads a `Sprite` by an Addressables key (`string` or `IKeyEvaluator`) and puts it into `Image.sprite`. Available only with `ASPID_MVVM_ADDRESSABLES_INTEGRATION`.

| Property | Description |
|----------|----------|
| `_defaultSprite` | Shown while loading and on error |
| `_disabledWhenNull` | Disables the `Image` when the sprite is `null` |
| `_seamlessSwap` | Keep the previous sprite until loading completes |

---

## Other Image properties

| Binder | Property | Type |
|--------|----------|-----|
| `ImageTypeBinder` | `Image.type` | `Image.Type` |
| `ImagePreserveAspectBinder` | `preserveAspect` | `bool` |
| `ImageFillOriginBinder` | `fillOrigin` | `int`, an index into the enum of the current `fillMethod` |
| `ImageFillClockwiseBinder` | `fillClockwise` | `bool` |

**Modes:** OneWay, OneTime, OneWayToSource.

---

## See also

- [Graphic Binders](graphic-binders.md), color and materials
- [StarterKit overview](README.md)
