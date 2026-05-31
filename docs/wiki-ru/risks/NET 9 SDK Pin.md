---
title: Привязка к .NET 9 SDK
type: risk
status: active
source_paths:
  - CLAUDE.md
  - Aspid.MVVM.Generators/global.json
tags:
  - risk
  - build
  - source-generator
  - toolchain
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/risks/NET 9 SDK Pin.md
translated_at: 2026-05-31
---

# Привязка к .NET 9 SDK

> Проектам Roslyn-генератора/анализатора требуется .NET 9 SDK, зафиксированный через `global.json`. Соберите их более старым SDK — и сборка упадёт ещё до того, как начнётся компиляция кода.

## Симптом

`dotnet build`/`dotnet test` для решений генератора или анализатора немедленно падает с ошибкой разрешения SDK (например, «A compatible .NET SDK was not found» / запрошенная версия `9.0.0`), даже если сам C# в порядке. В результате [[Committed DLLs]] так и не обновляются, и Unity продолжает использовать устаревшие [[Source Generator]] / [[Analyzer]].

## Причина

`Aspid.MVVM.Generators/global.json` фиксирует набор инструментов:

```json
{ "sdk": { "version": "9.0.0", "rollForward": "latestMajor", "allowPrerelease": true } }
```

`rollForward: latestMajor` означает, что принимается любой SDK **9.0.0 или новее**; всё, что старше, отклоняется сразу. Это самостоятельные проекты .NET (а не C#, компилируемый Unity), поэтому им нужен настоящий SDK в `PATH`. `allowPrerelease` позволяет удовлетворить привязку и предварительным версиям SDK.

Примечание: в этой выгрузке только сабмодуль Generators реально несёт `global.json` в своём корне. CLAUDE.md говорит «в корне каждого генератора/анализатора», но корни сабмодулей Analyzers и [[Unity Generators]] его не содержат — они, вероятно, откатываются к глобальному SDK / общему набору инструментов. Привязка, которую необходимо удовлетворить, — это привязка Generators.

## Исправление

1. Установите .NET 9 SDK (или новее): `dotnet --list-sdks` должна показывать запись `9.x`.
2. Перезапустите сборку, например `dotnet build Aspid.MVVM.Generators/Aspid.MVVM.Generators.sln`.
3. Закоммитьте пересобранную DLL — см. [[Committed DLLs]], чтобы понять, какие артефакты нужно обновить.

Если сабмодули пусты (сборка вообще не может найти проект), это уже другая проблема — см. [[Submodule Init]].

## Профилактика

- Держите 9.x SDK установленным на каждой машине и CI-раннере, которые касаются генераторов.
- Рассматривайте требование к SDK как часть настройки сборки наряду с [[Submodule Init]] и шагом коммита DLL из [[Committed DLLs]].
- Не правьте `global.json`, чтобы ослабить нижнюю планку, без веских причин; [[Source Generation Pipeline]] собран против известного SDK, и его понижение может незаметно изменить генерируемый вывод.

Несвязанная ловушка: это касается только времени сборки. На рантайм-код Unity это не влияет — он компилируется против .NET Standard 2.0 и этого SDK никогда не видит.

## Источник

- `Aspid.MVVM.Generators/global.json` — собственно привязка.
- `CLAUDE.md` (Key Technologies, Gotchas) — указывает, что `.NET 9.0 SDK` требуется для проектов генератора/анализатора.
