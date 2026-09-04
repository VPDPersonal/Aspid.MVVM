# Туториал 6. Custom Binder

Разбор сэмпла `Path 6. Custom Binder` — биндер для компонента, о котором StarterKit не знает.

**Предполагается знание:** [TodoList](../05.%20TodoList/README.ru.md).

---

## Что строим

```
Hero
[████████████░░░░░░░░]  70%
[ Hit ]   [ Heal ]
```

`HealthBar` — обычный UI-компонент проекта: `Image` с заливкой, подпись и градиент цвета. Задача — привязать его `Value` к `Health` во ViewModel.

Файлы: `Samples~/06. CustomBinder/`.

---

## Компонент без MVVM

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

Компонент ничего не знает о фреймворке. Это важно: биндер — адаптер снаружи, а не зависимость внутри.

---

## Биндер

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

Всё, что нужно написать, — свойство `Property`. Остальное даёт база и атрибуты:

| Элемент | Что даёт |
|---|---|
| `ComponentFloatMonoBinder<T>` | `IBinder<float>` плюс `int`, `long`, `double`, слот конвертера, кеш компонента, `OneWayToSource` |
| `[GenerateSerializableBinder]` | генератор создаёт `HealthBarValueBinder` — сериализуемый двойник для полей `[View]`-классов без компонента |
| `[AddBinderContextMenu(typeof(HealthBar))]` | пункт «Add Binder» в контекстном меню компонента `HealthBar` |
| `[AddComponentMenu]` | место в меню Add Component |
| `this.SafeClamp01(value)` | значение вне `0..1` не проглатывается молча: в консоль уходит ошибка, применяется ближайшее допустимое |

Класс не `sealed`: сгенерированный двойник и проектные наследники должны иметь возможность расширить его.

### Какую базу выбрать

| Тип свойства | База |
|---|---|
| `float` / `int` | `ComponentFloatMonoBinder<T>` / `ComponentIntMonoBinder<T>` |
| `UnityEngine.Object` (`Sprite`, `Material`, …) | `ComponentObjectMonoBinder<T, TObject>` |
| всё остальное | `ComponentMonoBinder<T, TValue>` |
| значение нужно преобразовать и передать дальше | `CasterMonoBinder<TFrom, TTo>` |

---

## Сцена

В сцене нет `[View]`-класса: обычный `MonoView` со списком биндеров по именам (`Health`, `HitCommand`, `HealCommand`) и `ViewInitializer` с `[Serializable]` `HeroViewModel`, который редактируется прямо в Inspector. Так один экран не требует ни одного скрипта View.

---

## Резюме

| Концепция | Где |
|---|---|
| Биндер = `Property` над `CachedComponent` | `HealthBarValueMonoBinder` |
| Сериализуемый двойник из генератора | `[GenerateSerializableBinder]` |
| Контекстное меню компонента | `[AddBinderContextMenu]` |
| Валидация без молчания | `SafeClamp01` |
| `MonoView` + `ViewInitializer` без кода | сцена `Custom Binder` |

Полные правила написания биндеров — в [Биндерах](../../Documentation/ru/06-binders.md).
