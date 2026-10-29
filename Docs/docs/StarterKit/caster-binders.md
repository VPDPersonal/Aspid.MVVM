# Caster Binders

Converting binders that accept one data type and turn it into another.

---

## Overview

Caster binders perform type casts. Unlike converters on regular binders, a caster binder implements the interface of the **source** data type.

---

## AnyToStringCasterBinder

Accepts **any** data type and converts it to `string`:

```
object? → IConverter<object?, string?>? → string → (a target TextBinder or other)
```

Implements `IAnyBinder`, so the Source Generator can attach it to any ViewModel property.

### Inspector properties

| Property | Description |
|----------|----------|
| `Converter` | Optional `IConverter<object?, string?>` (default `ObjectToStringConverter`) |

**Modes:** OneWay, OneTime.

---

## ToStringCasterBinder\<T\>

The typed version: accepts a concrete `T` and converts it to `string`:

```
T? → IConverter<T?, string?>? → string
```

Implements `IBinder<T>`.

---

## Example

```csharp
[ViewModel]
public partial class DebugViewModel
{
    [OneWayBind] private Vector3 _position;  // Vector3 to be shown as text
}
```

In the Inspector:
1. Add `AnyToStringCasterBinder`
2. Bind it to `Position`
3. Result: the Vector3 is converted to a string through `ToString()`

---

## See also

- [Converters](../08-converters.md), converters on the binder level
- [StarterKit overview](README.md)
