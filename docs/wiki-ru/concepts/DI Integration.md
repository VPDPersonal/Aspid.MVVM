---
title: Интеграция с DI
type: concept
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Views/Initializers/ViewInitializerBase.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Views/Initializers/ViewInitializer.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Views/Initializers/Components/InitializeComponent.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Views/Initializers/Components/ResolveType.cs
  - Readme.md
tags: [di, zenject, vcontainer, view-initialization, starterkit]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/concepts/DI Integration.md
translated_at: 2026-05-31
---

# Интеграция с DI

> Как [[View]] получает свой ViewModel из контейнера Zenject или VContainer вместо ссылки в Inspector — это спрятано за подключаемыми по выбору символами компиляции, чтобы ядро оставалось независимым от DI.

## Зачем это нужно

View нужен ViewModel, к которому он будет привязываться. StarterKit позволяет настроить это в Inspector, но в проектах на основе DI ViewModel (и его зависимости) живут в контейнере. Вместо жёсткой зависимости от Zenject или VContainer Aspid держит оба за условной компиляцией, поэтому фреймворк собирается даже без установленных библиотек.

## Как это работает

Всё управляется двумя символами define (задаются в Player Settings):

- `ASPID_MVVM_ZENJECT_INTEGRATION`
- `ASPID_MVVM_VCONTAINER_INTEGRATION`

Когда символ определён, в инициализаторах StarterKit активируются три вещи (см. [[View Initialization]]):

1. **Захват контейнера** — `ViewInitializerBase` получает внедряемое поле: `DiContainer` из Zenject (`[Zenject.Inject]`) или `IObjectResolver` из VContainer (`[VContainer.Inject]`). Он передаёт этот контейнер в каждый `InitializeComponent`.
2. **Путь разрешения через DI** — `InitializeComponent<T>` добавляет значение перечисления `ResolveType.Di`. Если оно выбрано в Inspector, `GetComponent()` вызывает `ZenjectContainer?.TryResolve(type)` или `VContainerContainer?.TryResolve(type, out ...)`, где `type` берётся из абстрактного метода `GetTypeForDi()`. Именно так ViewModel извлекается из контейнера вместо сериализованного поля `Mono` / `References` / `ScriptableObject`.
3. **Стадия жизненного цикла DI** — `ViewInitializer` добавляет вариант `InitializeStage.DiConstructor`. Внедряемый метод (`ZenjectConstructor` / `VContainerConstructor`) срабатывает, когда контейнер строит объект, запуская инициализацию в момент внедрения, а не на `Awake` / `OnEnable` / `Start` / `Manual`.

Без любого из символов `ResolveType.Di` и `InitializeStage.DiConstructor` просто не существуют — инициализатор ведёт себя как обычный MonoBehaviour.

## Ключевые связи

- Управляет [[View Initialization]]: DI — это один из `ResolveType` наряду с `Mono`, `References`, `ScriptableObject`.
- Разрешает ViewModel, который потребляет [[View]] / [[IViewModel]].
- Целиком находится в [[StarterKit ViewModels|StarterKit]]; ядро [[Architecture]] не имеет зависимости от DI.
- Zenject / VContainer — это [[External Dependencies]], они не входят в поставку.

## Исходный код

- `StarterKit/.../Views/Initializers/ViewInitializerBase.cs` — внедряемое поле контейнера, его передача.
- `StarterKit/.../Views/Initializers/ViewInitializer.cs` — стадия `DiConstructor`, методы внедрения.
- `StarterKit/.../Initializers/Components/InitializeComponent.cs` — разрешение через `ResolveType.Di`.
- `StarterKit/.../Initializers/Components/ResolveType.cs` — управляемое символом значение перечисления `Di`.

> Примечание: сборка также определяет интеграцию в стиле `Zenject` для фабрик View на основе префабов (например, `PrefabViewFactory`); судя по всему, этот путь следует тому же шаблону с управлением через символы, но здесь он не рассматривается.
