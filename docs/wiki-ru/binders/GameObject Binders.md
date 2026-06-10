---
title: GameObject-биндеры
type: binder-category
status: active
lang: ru
translated_from: binders/GameObject Binders.md
translated_at: 2026-05-31
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/GameObjects
tags: [binders, gameobject]
updated_at: 2026-05-31
---

# GameObject-биндеры

> Биндеры, управляющие самим `GameObject` — его активностью, тегом и инстанцированием — из члена ViewModel.

## Что входит в семейство

| Подцель | Привязывает | Основной binder |
|---|---|---|
| Visible | активность через `GameObject.SetActive` (`activeSelf`) | `GameObjectVisibleBinder` |
| Tag | `GameObject.tag` | `GameObjectTagBinder` |
| Instantiate | инстанцирование addressable в сцену | `GameObjectInstantiateAddressableMonoBinder` |

Все наследуются от `TargetBinder<GameObject>` — базы, которая хранит цель и применяет значения согласно [[BindMode]].

## Оси вариантов

- **Plain vs `Mono`** — обычные binder'ы создаются в коде; `…MonoBinder` основаны на `MonoBehaviour` и настраиваются в инспекторе. → [[Binders Catalog]]
- **`Switcher`** — `GameObjectTagSwitcherBinder` выбирает тег из привязанного значения.
- **`Enum` / `EnumGroup`** — `GameObjectVisibleEnumMonoBinder`, `GameObjectTagEnumMonoBinder` и т.д. управляют целью из enum.

## Примечательное поведение

- `GameObjectVisibleBinder` помечен `[BindModeOverride(OneWay, OneTime, OneWayToSource)]` и **отклоняет `TwoWay`**. Как `IReverseBinder<bool>` он поддерживает [[BindMode|OneWayToSource]]: при привязке отправляет текущее `activeSelf` обратно в ViewModel и вызывает `ValueChanged`.
- У него есть флаг `_isInvert`, инвертирующий привязанный `bool` перед применением — удобно для биндингов «скрыть, когда true».
- Привязываемый член — это обычный [[Bindable Members|привязываемый член]] на [[ViewModel]]; binder его читает, вывода генератора здесь нет.

## Источник

`StarterKit/Unity/Runtime/Binders/GameObjects/` (подпапки `Visible/`, `Tag/`, `Instantiate/`, в каждой есть `Mono/`).
