---
title: IViewModel
type: entity
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/IViewModel.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/FindBindableMemberParameters.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/FindBindableMemberResult.cs
tags: [contract, viewmodel]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/entities/IViewModel.md
translated_at: 2026-05-31
---

# IViewModel

> Базовый контракт, который реализует каждый ViewModel. Вы почти никогда не пишете его вручную — его генерирует `[ViewModel]`. См. [[ViewModel Generation]].

## Поверхность

```csharp
public interface IViewModel
{
    FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters);
}
```

Единственный метод: по параметрам, идентифицирующим член, возвращает результат, описывающий найденный bindable-член. Это тот поиск, который используют [[Binders Catalog|байндеры]], чтобы определить, к какому члену ViewModel они подключаются.

## Почему он устроен именно так

`in FindBindableMemberParameters` передаётся по readonly-ссылке, чтобы избежать копирования структуры параметров при каждом поиске — это соответствует цели фреймворка работать без рефлексии и с минимумом аллокаций. Сгенерированная реализация разрешает члены без рефлексии во время выполнения.

## Ключевые связи

- Генерируется через `[ViewModel]` → [[ViewModel Generation]].
- Используется байндерами при инициализации View ([[View Initialization]]) и во время [[Runtime Binding Resolution]].
- Связанные контракты: [[IRelayCommand]], [[IBinder]].

## Исходный код

`Source/ViewModels/IViewModel.cs`, а также структуры параметров/результата в той же папке.
