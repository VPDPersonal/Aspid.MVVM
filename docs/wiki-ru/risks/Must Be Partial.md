---
title: "Риск: типы должны быть partial"
type: risk
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Generation/ViewModelAttribute.cs
  - Aspid.MVVM.Analyzers
tags: [risk, generation, partial]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/risks/Must Be Partial.md
translated_at: 2026-05-31
---

# Риск: типы должны быть `partial`

> **Симптом:** ошибки компиляции (отсутствующие члены или «no implementation for IViewModel») в классе, использующем `[ViewModel]`, `[Bind]` или `[RelayCommand]`.

## Причина

Эти атрибуты запускают генератор исходного кода, который порождает **второе объявление `partial`** для типа (реализацию [[IViewModel]], bindable-свойства, свойства команд). Если тип не объявлен как `partial`, сгенерированной половине некуда «приземлиться», поэтому ссылки на сгенерированные члены не разрешаются. → [[ViewModel Generation]]

## Решение

Объявите тип как `partial`:

```csharp
[ViewModel]
public partial class CounterViewModel   // <-- partial
{
    [Bind] private int _count;
}
```

## Профилактика

Сабмодуль `Aspid.MVVM.Analyzers` поставляет Roslyn-диагностику, которая помечает `[ViewModel]`/`[Bind]`/`[RelayCommand]` на не-`partial` типах, поэтому проблема всплывает как предупреждение/ошибка анализатора в IDE ещё до полной сборки. Не подавляйте её.

## Связанное

- [[Committed DLLs]] — анализатор/генератор запускаются из **закоммиченных DLL**; если анализатор не срабатывает, DLL может быть устаревшей или сабмодуль не инициализирован ([[Submodule Init]]).
