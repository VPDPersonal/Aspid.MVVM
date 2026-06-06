# Switcher Binders

Паттерн Switcher: `bool` → выбор между двумя предустановленными значениями.

---

## Обзор

Switcher-биндер принимает `bool` и возвращает одно из двух значений:
- `true` → `_trueValue`
- `false` → `_falseValue`

Это альтернатива конвертерам для простых случаев "если/иначе".

---

## SwitcherBinder\<T\>

Базовый класс (не MonoBehaviour):

```csharp
public abstract class SwitcherBinder<T> : Binder, IBinder<bool>
{
    // Сериализуются в Inspector:
    protected T _trueValue;
    protected T _falseValue;

    public void SetValue(bool value)
    {
        SetValue(value ? _trueValue : _falseValue);
    }

    protected abstract void SetValue(T value);
}
```

**Режимы:** OneWay, OneTime. TwoWay и OneWayToSource **не поддерживаются**.

---

## Готовые Switcher-биндеры

Почти каждый стандартный биндер имеет Switcher-вариант:

| Switcher-биндер | Выбирает между | Пример |
|----------------|----------------|--------|
| `TextSwitcherBinder` | двумя строками | "Активен" / "Неактивен" |
| `TextFontSwitcherBinder` | двумя шрифтами | Bold / Regular |
| `TextFontSizeSwitcherBinder` | двумя размерами | 24 / 16 |
| `TextAlignmentSwitcherBinder` | двумя выравниваниями | Center / Left |
| `ImageSpriteSwitcherBinder` | двумя спрайтами | CheckOn / CheckOff |
| `ImageFillSwitcherBinder` | двумя fillAmount | 1.0 / 0.0 |
| `SliderMinMaxSwitcherBinder` | двумя min/max | (0,100) / (0,10) |
| `GraphicColorSwitcherBinder` | двумя цветами | Green / Red |
| `CanvasGroupAlphaSwitcherBinder` | двумя alpha | 1.0 / 0.3 |
| `RendererMaterialColorSwitcherBinder` | двумя цветами | Lit / Dark |
| `SelectableColorBlockSwitcherBinder` | двумя ColorBlock | Normal / Disabled |

---

## SwitcherMonoBinder

MonoBehaviour-вариант для использования в Inspector:

```csharp
// Три generic-перегрузки:
SwitcherMonoBinder<T>                    // без конвертера
SwitcherMonoBinder<T, TTarget>           // с целевым компонентом
SwitcherMonoBinder<T, TTarget, TConv>    // с конвертером
```

---

## Пример использования

### ViewModel

```csharp
[ViewModel]
public partial class TaskViewModel
{
    [OneWayBind] private bool _isCompleted;
}
```

### В Inspector

1. Добавьте `TextSwitcherBinder` на объект с TextMeshPro
2. Установите:
   - `True Value` = "Выполнено"
   - `False Value` = "В процессе"
3. Привяжите к `IsCompleted`

Результат: текст автоматически переключается при изменении `IsCompleted`.

### Цвета статуса

1. Добавьте `GraphicColorSwitcherBinder`
2. `True Value` = зелёный
3. `False Value` = красный

---

## Когда использовать Switcher vs Конвертер

| Switcher | Конвертер |
|----------|-----------|
| Два фиксированных значения | Произвольное преобразование |
| Настройка в Inspector | Логика в коде |
| Только `bool` вход | Любой тип входа |
| Быстрая настройка | Переиспользуемая логика |

---

## См. также

- [Конвертеры](../08-converters.md) — произвольные преобразования
- [Обзор StarterKit](README.md) — таблица всех компонентов
