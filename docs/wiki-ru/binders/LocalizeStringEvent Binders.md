---
title: Биндеры LocalizeStringEvent
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LocalizeStringEvents/Entry/LocalizeStringEventEntryBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LocalizeStringEvents/Entry/LocalizeStringEventEntrySwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LocalizeStringEvents/Entry/Mono/LocalizeStringEventEntryMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LocalizeStringEvents/Entry/Mono/LocalizeStringEventEntryEnumGroupMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LocalizeStringEvents/Variable/LocalizeStringEventVariableBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LocalizeStringEvents/OneWayToSource/Mono/LocalizeStringEventToSourceMonoBinder.cs
tags: [binder, starterkit, localization, unity]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/LocalizeStringEvent Binders.md
translated_at: 2026-05-31
---

# Биндеры LocalizeStringEvent

> Биндеры StarterKit, которые управляют компонентом Unity `LocalizeStringEvent` из ViewModel — переключают локализованную запись таблицы или передают переменные Smart String — так что локализованный текст реагирует на ваши данные.

## Зачем это нужно

Пакет локализации Unity отображает переведённый текст через компонент `LocalizeStringEvent`, чей `StringReference` хранит и ключ записи (какая строка в таблице), и именованные *переменные Smart String* (подстановки во время выполнения). Это семейство позволяет ViewModel управлять любым из них без ручной развязки, так что отображаемая запись или её плейсхолдеры `{variable}` обновляются при изменении значения. Вся папка обёрнута условием `#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION`, поэтому компилируется только при наличии пакета локализации.

## Семейство

| Группа | Целевое свойство | Привязываемый тип | Варианты |
|-------|-----------------|------------|----------|
| Entry | `StringReference.TableEntryReference` | `string` | Plain, Mono, Switcher (plain + Mono), Enum, EnumGroup |
| Variable | именованный `IVariable` Smart String | числа, `bool`, `string`, `Object` | Plain, Mono |
| OneWayToSource | привязанное свойство | — | только Mono |

## Оси вариантов

- **Entry против Variable** — Entry переписывает то, какая строка перевода показывается; Variable обновляет именованную подстановку `Smart String` (например, `IntVariable`, `StringVariable`, `ObjectVariable`), а затем вызывает `RefreshString()`. Биндер Variable реализует [[IBinder]]`<bool>`/`<string>`/`<Object>` плюс `INumberBinder`, лениво создавая переменную, если она отсутствует.
- **Plain против Mono** — Plain-биндеры (например, `LocalizeStringEventEntryBinder`, наследующий `TargetStringBinder`/`TargetBinder`) помечены `[Serializable]` и создаются в коде. Mono-биндеры (наследующие MonoBehaviour'ы `ComponentStringMonoBinder`/`TargetBinder`) — это инспекторные компоненты, несущие `[AddComponentMenu]` / `[AddBinderContextMenu]`. См. [[Mono Binders]].
- **Switcher** — выбирает между ключом записи `trueValue` и `falseValue` на основе привязанного `bool` (`SwitcherStringBinder`).
- **Enum / EnumGroup** — Enum сопоставляет значение перечисления с одной записью; EnumGroup задаёт запись для *каждого элемента* в настроенной группе `LocalizeStringEvent`.
- **OneWayToSource** — `LocalizeStringEventToSourceMonoBinder` (это `ComponentToSourceMonoBinder`) передаёт текущее значение компонента обратно в ViewModel при установлении привязки.

## Заметное поведение

- Биндеры Entry/Variable отвергают [[BindMode|TwoWay]] в своих конструкторах (`ThrowExceptionIfMatches`/`ThrowExceptionIfTwo`); допустимые режимы — OneWay/OneTime, при этом Mono-вариант Entry также поддерживает OneWayToSource (передаёт текущий `TableEntryReference` при привязке).
- Биндер Variable выполнит `Add` новой переменной под настроенным именем, если у `StringReference` её нет, и выбросит `InvalidCastException`, если тип существующей переменной не совпадает.

## Исходники

`Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LocalizeStringEvents/` — `Entry/`, `Variable/`, `OneWayToSource/`. Общие базовые классы находятся в [[Binder Base Classes]]; см. также [[Text Binders]], [[String Converters]], [[Binders Catalog]].
