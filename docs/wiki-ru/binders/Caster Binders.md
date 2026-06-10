---
title: Биндеры-преобразователи (Caster)
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Casters
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Casters
tags:
  - binder
  - caster
  - converter
  - starterkit
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/Caster Binders.md
translated_at: 2026-05-31
---

# Биндеры-преобразователи (Caster)

> Биндеры-мосты, которые преобразуют значение ViewModel одного типа в другой тип перед тем, как передать его в сеттер или [[UnityEvent Binders|UnityEvent]].

## Зачем они нужны
Биндируемый член предоставляет один тип, а потребителю нужен другой (`TimeSpan`, отображаемый как `string`, `Vector2`, управляющий слотом `Vector3`, `string`, переключающий `bool`). Вместо того чтобы переделывать ViewModel, биндер-преобразователь становится посередине: он получает исходный тип, запускает [[Converters|IConverter]] и выдаёт целевой тип. Это сохраняет ViewModel чистой по типам и переносит адаптацию типов на слой View.

## Как они работают
Каждый преобразователь содержит `IConverter<TFrom, TTo>` плюс приёмник (делегат или `UnityEvent<TTo>`). `SetValue` вызывает `_converter.Convert(value)` и передаёт результат дальше. Режим ограничен: конструкторы вызывают `mode.ThrowExceptionIfTwo()`, поэтому преобразователи работают только в стиле [[BindMode|OneWay]] — они не возвращают значения в источник. Если конвертер не задан, подставляется конвертер по умолчанию (например, `GenericToString<object>`, `StringEmptyToBoolConverter`, `Vector2ToVector3Converter`).

## Семейство

| Биндер | Из → В | Конвертер по умолчанию |
|--------|-----------|-------------------|
| `AnyToStringCasterBinder` ([[IBinder]] через `IAnyBinder`) | любой → string | `GenericToString<object>` |
| `GenericToStringCasterBinder` | T → string | (обобщённый) |
| `StringToBoolCasterBinder` | string → bool | `StringEmptyToBoolConverter` |
| `GenericToStringCasterMonoBinder<T>` (абстрактная база) | T → string | задаётся в каждом подклассе |
| `TimeSpanToStringCasterMonoBinder` | TimeSpan → string | `TimeSpanToStringConverter` |
| `Vector2ToVector3CasterMonoBinder` / `Vector3ToVector2CasterMonoBinder` | Vector2 ↔ Vector3 | `Vector2ToVector3Converter` |
| `AnyToStringCasterMonoBinder`, `StringToBoolCasterMonoBinder` | как выше | назначается в инспекторе |

## Оси вариантов
- **Plain против Mono** — Plain-преобразователи наследуются от [[Binder Base Classes|Binder]] и принимают `Action` `setValue` в конструкторе (управляются из кода). Mono-преобразователи наследуются от `MonoBinder` (см. [[Mono Binders]]), предоставляют сериализуемый `IConverter` (`[SerializeReference]` + выпадающий список) и приёмник `UnityEvent<TTo>`, настраиваемый в инспекторе. Они регистрируются под `Aspid/MVVM/Binders/Casters` через атрибуты add-menu.
- **Обобщённая база + конкретный подкласс** — `GenericToStringCasterMonoBinder<T>` имеет модификаторы `abstract partial`; конкретные типы (`TimeSpanToStringCasterMonoBinder`) просто закрывают обобщённый тип. База ветвится по `UNITY_2023_1_OR_NEWER`: более новый Unity сериализует обобщённый `IConverter<T,string>` напрямую, а старый Unity требует необобщённого интерфейса плюс переопределения абстрактного свойства `Converter`.
- *(Предположение)* В этой папке нет вариантов Switcher / Enum / EnumGroup / OneWayToSource — эти оси находятся в других категориях биндеров, а не в Casters.

## Заметное поведение
- Mono-преобразователи защищаются от отсутствующего конвертера: они вызывают `Debug.LogError` и возвращаются без вызова события; `[BinderLog]` декорирует `SetValue`.
- `OnValidate` повторно назначает конвертер по умолчанию, когда поле в инспекторе очищено.

## Исходный код
- `StarterKit/Runtime/Binders/Casters/` — преобразователи на базе обычного `Binder`.
- `StarterKit/Unity/Runtime/Binders/Casters/Mono/` — преобразователи на базе `MonoBinder`.
- Конвертеры находятся в [[Converters]]; см. также [[Binders Catalog]].
