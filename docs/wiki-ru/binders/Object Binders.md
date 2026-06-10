---
title: Object-биндеры
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Objects/ObjectNameBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Objects/Mono/ObjectNameMonoBinder.cs
tags: [binder, starterkit, unity-object, name]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/Object Binders.md
translated_at: 2026-05-31
---

# Object-биндеры

> Биндеры, которые управляют свойством `UnityEngine.Object.name` на основе `string` из ViewModel — полезны для отладки, подписей в иерархии и удобного для инспектора именования объектов.

Это семейство нацелено на единственное свойство, общее для каждого `UnityEngine.Object`: `name`. Это самая маленькая группа биндеров уровня члена в StarterKit — здесь есть только одна привязываемая цель, поэтому семейство существует в основном для того, чтобы продемонстрировать разделение Plain/Mono и обратную передачу `OneWayToSource`, а не чтобы покрыть множество свойств.

## Таблица семейства

| Вариант | Базовый класс | Цель | Направление |
|---------|-----------|--------|-----------|
| `ObjectNameBinder` | [[Binder Base Classes\|TargetBinder<Object>]] | `Object.name` | `IBinder<string>` + `IReverseBinder<string>` |
| `ObjectNameMonoBinder` | [[Mono Binders\|MonoBinder]] | `Object.name` | `IBinder<string>` + `IReverseBinder<string>` |

## Оси вариантов

- **Plain против Mono** — `ObjectNameBinder` помечен `[Serializable]`, живёт внутри [[View]] как сериализуемая ссылка и получает свою цель через конструктор. `ObjectNameMonoBinder` — это компонент `MonoBehaviour` (`partial`, поэтому генератор выпускает его связующий код привязки) с сериализуемым полем `Object _object`; его `OnValidate` по умолчанию назначает целью объект-хозяин `gameObject`.
- **Switcher / Enum / EnumGroup** — здесь их нет. Эти оси применимы к более богатым семействам (например, [[GameObject Binders]]); `name` — это обычная строка, поэтому переключать или перечислять нечего.
- **OneWayToSource** — оба варианта реализуют `IReverseBinder<string>` и объявляют `[BindModeOverride(OneWay, OneTime, OneWayToSource)]`. `TwoWay` отклоняется при конструировании (`ThrowExceptionIfMatches`). В режиме `OneWayToSource` метод `OnBound` отправляет текущее значение `name` обратно в ViewModel сразу после установления привязки — значение из инспектора служит начальным значением для модели.

## Заметное поведение

- Необязательный `IConverter<string,string>` (интерфейс [[Converters|конвертера]]) может преобразовать значение перед тем, как оно будет применено или отправлено обратно. `GetConvertedValue` подменяет результат на `string.Empty`, поэтому значение null никогда не вызывает исключения.
- Два варианта различаются типизацией nullability конвертера: под `UNITY_2023_1_OR_NEWER` Plain-биндер использует `IConverter<string?,string?>`, тогда как Mono-биндер использует `IConverter<string,string>` — небольшая несогласованность, скорее всего случайная.
- Mono-биндер применяет `[BinderLog]` к `SetValue`; этот атрибут, по-видимому, управляет сгенерированным отладочным логированием (см. [[Debug Binders]]).
- Оба привязываются к `string`, поэтому сгенерированный `[[Bindable Members|Bind]]` член типа `string` на [[ViewModel]] является естественным источником. Семантику режимов см. в [[BindMode]].

## Источник

`Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Objects/` — `ObjectNameBinder.cs` и `Mono/ObjectNameMonoBinder.cs`. Полный индекс биндеров см. в [[Binders Catalog]], а базовый контракт — в [[Data Binding]].
