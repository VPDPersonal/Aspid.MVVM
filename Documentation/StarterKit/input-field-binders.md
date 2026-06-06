# InputField Binders

Биндеры для `TMP_InputField` с поддержкой двусторонней привязки, числовых типов и различных событий.

---

## InputFieldBinder

Основной биндер для ввода текста.

| Интерфейс | Описание |
|-----------|----------|
| `IBinder<string?>` | Получает текст от ViewModel |
| `IReverseBinder<string>` | Отправляет изменения обратно |
| `INumberBinder` | Принимает числовые типы |
| `INumberReverseBinder` | Отправляет числа обратно |

### Inspector-свойства

| Свойство | Описание |
|----------|----------|
| UpdateEvent | Событие обновления: `OnValueChanged`, `OnEndEdit`, `OnSubmit`, `OnSelect`, `OnDeselect` |
| Converter | `IConverter<string?, string?>` (опционально) |

### Защита от циклов

Флаг `_isNotifyValueChanged` предотвращает бесконечную рекурсию при TwoWay-привязке: когда ViewModel обновляет InputField, обратное событие блокируется.

### Числовые режимы

При ContentType `IntegerNumber` или `DecimalNumber` — парсит строку в число и отправляет через `INumberReverseBinder`.

**Режимы:** OneWay, TwoWay, OneTime, OneWayToSource.

```csharp
[ViewModel]
public partial class FormViewModel
{
    [TwoWayBind] private string _userName;
}
```

---

## Дополнительные биндеры

| Биндер | Привязывает | Тип |
|--------|------------|-----|
| `InputFieldCharacterValidationBinder` | `characterValidation` | `CharacterValidation` |
| `InputFieldContentTypeBinder` | `contentType` | `ContentType` |
| `InputFieldInputTypeBinder` | `inputType` | `InputType` |
| `InputFieldLineTypeBinder` | `lineType` | `LineType` |

Каждый имеет Switcher-вариант (`bool` → выбор между двумя значениями).

---

## См. также

- [Text Binders](text-binders.md)
- [Режимы привязки](../03-binding-modes.md) — TwoWay
- [Обзор StarterKit](README.md)
