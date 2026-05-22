# Caster Binders

Конвертирующие биндеры, которые принимают один тип данных и преобразуют его в другой.

---

## Обзор

Caster-биндеры — это специальные биндеры, которые выполняют приведение типов. В отличие от конвертеров на обычных биндерах, caster-биндеры реализуют интерфейс для **исходного** типа данных.

---

## AnyToStringCasterBinder

Принимает **любой** тип данных и преобразует в `string`:

```
object? → IConverter<object?, string?>? → string → (целевой TextBinder или другой)
```

Реализует `IAnyBinder` — Source Generator может подключить его к любому свойству ViewModel.

### Inspector-свойства

| Свойство | Описание |
|----------|----------|
| `Converter` | Опциональный `IConverter<object?, string?>` (по умолчанию `ObjectToStringConverter`) |

**Режимы:** OneWay, OneTime.

---

## GenericToStringCasterBinder\<T\>

Типизированная версия — принимает конкретный тип `T` и преобразует в `string`:

```
T? → IConverter<T?, string?>? → string
```

Реализует `IBinder<T>`.

---

## StringToBoolCasterBinder

Преобразует `string` в `bool`:

```
string → bool (через bool.TryParse)
```

Реализует `IBinder<string>`.

---

## Пример использования

```csharp
[ViewModel]
public partial class DebugViewModel
{
    [OneWayBind] private Vector3 _position;  // Vector3 → нужно отобразить как текст
}
```

В Inspector:
1. Добавьте `AnyToStringCasterBinder`
2. Привяжите к `Position`
3. Результат: Vector3 преобразуется в строку через `ToString()`

---

## См. также

- [Конвертеры](../08-converters.md) — конвертеры на уровне биндера
- [Обзор StarterKit](README.md)
