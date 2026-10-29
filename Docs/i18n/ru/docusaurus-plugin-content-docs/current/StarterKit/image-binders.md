# Image Binders

Биндеры для `Image` компонента Unity UI.

---

## ImageSpriteBinder

Привязка спрайта к `Image.sprite`.

| Интерфейс | Тип данных |
|-----------|-----------|
| `IBinder<Sprite?>` | Устанавливает спрайт напрямую |
| `IBinder<Texture2D?>` | Конвертирует `Texture2D` в `Sprite` через `Sprite.Create` |

### Inspector-свойства

| Свойство | Описание |
|----------|----------|
| `_disabledWhenNull` | Отключает `Image` компонент, когда спрайт `null` |

Спрайт, созданный из `Texture2D`, принадлежит биндеру и уничтожается при отвязке.

**Режимы:** OneWay, OneTime, OneWayToSource.

```csharp
[ViewModel]
public partial class PlayerViewModel
{
    [OneWayBind] private Sprite _avatar;
    [OneWayBind] private Texture2D _downloadedImage;  // Автоконвертация в Sprite
}
```

---

## ImageSpriteSwitcherBinder

`bool` → выбор между двумя спрайтами.

| Свойство | Описание |
|----------|----------|
| True Value | Спрайт при `true` |
| False Value | Спрайт при `false` |

**Режимы:** OneWay, OneTime.

---

## ImageFillBinder

Привязка `Image.fillAmount` (0-1):

```csharp
[ViewModel]
public partial class HealthBarViewModel
{
    [OneWayBind] private float _healthRatio;  // 0.0 - 1.0
}
```

Значение clamp-ится в диапазоне [0, 1]. Реализует `INumberBinder` — принимает `int`, `float`, `long`, `double`.

**Режимы:** OneWay, OneTime, OneWayToSource.

---

## ImageFillSwitcherBinder

`bool` → выбор между двумя значениями `fillAmount`.

---

## ImageSpriteAddressableMonoBinder

Загружает `Sprite` по адресу Addressables (`string` или `IKeyEvaluator`) и ставит его в `Image.sprite`. Доступен только с `ASPID_MVVM_ADDRESSABLES_INTEGRATION`.

| Свойство | Описание |
|----------|----------|
| `_defaultSprite` | Показывается во время загрузки и при ошибке |
| `_disabledWhenNull` | Отключает `Image`, когда спрайт `null` |
| `_seamlessSwap` | Держать прежний спрайт до окончания загрузки |

---

## Прочие свойства Image

| Биндер | Свойство | Тип |
|--------|----------|-----|
| `ImageTypeBinder` | `Image.type` | `Image.Type` |
| `ImagePreserveAspectBinder` | `preserveAspect` | `bool` |
| `ImageFillOriginBinder` | `fillOrigin` | `int`, индекс в enum текущего `fillMethod` |
| `ImageFillClockwiseBinder` | `fillClockwise` | `bool` |

**Режимы:** OneWay, OneTime, OneWayToSource.

---

## См. также

- [Graphic Binders](graphic-binders.md) — цвет и материалы
- [Обзор StarterKit](README.md)
