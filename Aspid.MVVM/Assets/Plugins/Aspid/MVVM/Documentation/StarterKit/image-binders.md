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

**Режимы:** OneWay, OneTime.

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

**Режимы:** OneWay, OneTime.

---

## ImageFillSwitcherBinder

`bool` → выбор между двумя значениями `fillAmount`.

---

## См. также

- [Graphic Binders](graphic-binders.md) — цвет и материалы
- [Обзор StarterKit](README.md)
