---
title: Текстовые биндеры
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Texts
tags: [binders, text, tmp]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/Text Binders.md
translated_at: 2026-05-31
---

# Текстовые биндеры

> Биндеры, связывающие члены ViewModel со свойствами TextMeshPro (`TMP_Text`) — самим текстом, шрифтом, размером, выравниванием и локализацией.

## Почему это одна страница, а не 28

Папка `Texts/` содержит около 28 файлов, но это **одни и те же несколько идей**, повторённые для разных подцелей и вариантов. Описать *паттерн* один раз гораздо полезнее, чем создавать 28 почти идентичных заготовок. (Правило гранулярности: [[Binders Catalog]].)

## Что входит в семейство

| Подцель | Что связывает | Основной binder |
|---|---|---|
| Text | строковое содержимое (`TMP_Text.text`) | `TextBinder` |
| Font | `TMP_FontAsset` | `TextFontBinder` |
| FontSize | размер шрифта | `TextFontSizeBinder` |
| Alignment | выравнивание текста | `TextAlignmentBinder` |
| Localizations | запись локализации | `TextLocalizationEntryBinder` |

## Оси вариантов (применяются ко всему семейству)

- **Обычный против `Mono`** — обычные биндеры создаются в коде; `…MonoBinder` основаны на `MonoBehaviour`, поэтому настраиваются в инспекторе Unity. → [[Binders Catalog]]
- **`Switcher`** — выбирает между значениями на основе связанного значения.
- **`Enum` / `EnumGroup`** — управляют свойством из enum (например, `TextEnumMonoBinder`).
- **`OneWayToSource`** — например, `TextToSourceMonoBinder`, передаёт View → ViewModel.

## Заметное поведение

- `TextBinder` задаёт `TMP_Text.text` и также реализует `INumberBinder`, поэтому числовые значения форматируются с учётом культуры (через `CultureInfoMode`) и связываются как текст.
- Он **отклоняет `TwoWay`** (`mode.ThrowExceptionIfMatches(BindMode.TwoWay)`) — метке нечего записывать обратно. См. [[BindMode]].
- Всё семейство ограничено условием `UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION` (зависимость от TextMeshPro).

## Источник

`StarterKit/Unity/Runtime/Binders/Texts/` (подпапки: `Text/`, `Font/`, `FontSize/`, `Alignment/`, `Localizations/`, каждая со вложенной папкой `Mono/`).
