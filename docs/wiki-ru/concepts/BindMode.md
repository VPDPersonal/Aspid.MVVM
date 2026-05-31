---
title: BindMode
type: concept
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Mode/BindMode.cs
tags: [binding, bind-mode]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/concepts/BindMode.md
translated_at: 2026-05-31
---

# BindMode

> `BindMode` задаёт **направление потока данных** между членом ViewModel и элементом View для отдельной привязки.

## Режимы

| Режим | Значение | Направление |
|---|---|---|
| `None` | 0 | Привязка отсутствует (значение enum по умолчанию). |
| `OneWay` | 1 | ViewModel → View. Правки во View **не** доходят до ViewModel. |
| `TwoWay` | 2 | Оба направления. |
| `OneTime` | 3 | ViewModel → View **однократно**; последующие изменения ViewModel игнорируются. |
| `OneWayToSource` | 4 | Только View → ViewModel. Изменения ViewModel **не** доходят до View. |

## Почему это важно

Направление выбирается для каждой привязки отдельно, а не глобально. Метка обычно `OneWay`; поле ввода, редактирующее состояние, — `TwoWay` или `OneWayToSource`; значение, заданное при инициализации и более не меняющееся, — `OneTime` (самый дешёвый вариант — без постоянной подписки).

## Как он выбирается

- `[Bind]` без аргумента: `TwoWay` для изменяемых полей, `OneTime` для полей `readonly`. См. [[ViewModel Generation]].
- Многие [[Binders Catalog|binders]] ограничивают набор принимаемых режимов — например, [[Text Binders|TextBinder]] отклоняет `TwoWay` (метке нечего записывать обратно). Такие binders бросают исключение при создании, если режим не поддерживается.

## Источник

`Source/Mode/BindMode.cs` (enum); `Source/Mode/Extensions/` (вспомогательные методы валидации).
