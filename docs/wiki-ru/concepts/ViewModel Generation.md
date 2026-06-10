---
title: Генерация ViewModel
type: concept
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Generation/ViewModelAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Generation/BindAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/IViewModel.cs
  - Aspid.MVVM.Generators/Aspid.MVVM.Generators/Aspid.MVVM.Generators/Generators/ViewModels/ViewModelGenerator.cs
tags: [generation, viewmodel, bind]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/concepts/ViewModel Generation.md
translated_at: 2026-05-31
---

# Генерация ViewModel

> `[ViewModel]` превращает написанный вручную `partial`-класс в полноценный [[IViewModel]]; `[Bind]` превращает поле в наблюдаемый привязываемый член. Сгенерированная половина не видна в вашем исходном коде — эта страница как раз её документирует.

## Зачем это нужно

Вы пишете данные (поля) и намерение (атрибуты); генератор пишет механическую обвязку [[IViewModel]]. Никакой рефлексии, никакого ручного уведомления об изменениях.

## Как это работает

- `[ViewModel]` (`ViewModelAttribute`, применим к class/struct) заставляет генератор выпустить реализацию [[IViewModel]] — в частности `FindBindableMember(...)` — для помеченного типа и проанализировать его члены.
- `[Bind]` (`BindAttribute`, на поле) выпускает **привязываемое свойство**, оборачивающее это поле, с уведомлением об изменениях, подключённым к привязанным к нему binder'ам.
- Помеченный тип **обязан быть `partial`** — генератор выпускает второй partial; иначе вы получите ошибки компиляции. → [[Must Be Partial]]

## Режим привязки по умолчанию (стоит запомнить)

`[Bind]` без аргумента выбирает режим по изменяемости поля:

| Поле | Режим по умолчанию |
|---|---|
| изменяемое | `TwoWay` |
| `readonly` | `OneTime` |

При `[Bind(mode)]` на `readonly`-поле принимаются только `OneTime`/`OneWay` (оба ведут себя как `OneTime`); остальные режимы отклоняются. Полное перечисление: [[BindMode]].

## Ключевые связи

- Порождает реализацию [[IViewModel]], потребляемую [[Binders Catalog|binder'ами]].
- Методы `[RelayCommand]` становятся свойствами `IRelayCommand` через тот же конвейер → [[Relay Commands]].
- Сквозной разбор сборки от начала до конца: [[ViewModel to Generated Code]].

## Исходный код

`ViewModelAttribute.cs`, `BindAttribute.cs`, `IViewModel.cs` и `ViewModelGenerator.cs` (в сабмодуле `Aspid.MVVM.Generators` — Unity потребляет **собранную DLL**, а не этот исходный код).
