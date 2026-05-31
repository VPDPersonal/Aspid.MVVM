---
title: Закоммиченные DLL
type: risk
status: active
source_paths:
  - CLAUDE.md
  - Aspid.MVVM.Generators/Directory.Build.targets
  - Aspid.MVVM/Assets/Aspid/MVVM/Aspid.MVVM.Generators.dll
  - Aspid.MVVM/Assets/Aspid/MVVM/Aspid.MVVM.Generators.dll.meta
tags: [risk, generators, build, unity, submodule]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/risks/Committed DLLs.md
translated_at: 2026-05-31
---

# Закоммиченные DLL

> DLL генератора/анализатора Roslyn, которые Unity фактически запускает, закоммичены в `Assets/`, а не собираются Unity. Поправите исходники генератора и забудете пересобрать и закоммитить DLL — и ваше изменение незаметно так и не дойдёт до Unity.

## Симптом

Вы меняете код генератора или анализатора (например, то, как генерируются члены [[ViewModel to Generated Code]]), снова открываете Unity, а ничего не меняется — сгенерированный `partial`, диагностика или фикс ведут себя ровно так же, как раньше. Никакая ошибка сборки не указывает на причину. CI или коллега тоже могут видеть устаревшие сгенерированные члены, потому что закоммиченная DLL старше вашей правки исходников.

## Причина

Unity не компилирует проекты генератора/анализатора. Он загружает три заранее собранные DLL, закоммиченные внутри дерева Unity:

- `Aspid.MVVM/Assets/Aspid/MVVM/Aspid.MVVM.Generators.dll`
- `Aspid.MVVM/Assets/Aspid/MVVM/Aspid.MVVM.Analyzers.dll`
- `Aspid.MVVM/Assets/Aspid/MVVM/Unity/Aspid.MVVM.Unity.Generators.dll`

Каждый `.dll.meta` несёт метку `RoslynAnalyzer` — именно так Unity трактует сборку как генератор/анализатор времени компиляции, а не как runtime-плагин. Сами исходники живут в git-сабмодулях ([[Source Generator]], [[Analyzer]], [[Unity Generators]]), которые Unity никогда не читает — см. [[Submodule Init]]. Поэтому DLL и её исходники могут разойтись: во время выполнения источником истины является DLL.

## Решение

Пересоберите изменённое решение и закоммитьте обновлённую DLL:

```bash
dotnet build Aspid.MVVM.Generators/Aspid.MVVM.Generators.sln
```

Вспомогательный механизм сборки делает копирование за вас. `Aspid.MVVM.Generators/Directory.Build.targets` определяет цель `CopyToUnity`, которая выполняется `AfterTargets="Build"` только при `IsRoslynComponent == true`, копируя `$(TargetPath)` в `Assets/Aspid/MVVM/` (с `SkipUnchangedFiles`). Так что успешная сборка автоматически обновляет DLL внутри Assets — но git не проиндексирует её за вас. Вы должны сделать `git add` и закоммитить обновлённый `.dll`. Затем дайте Unity переимпортировать его. (Сабмодули анализатора и Unity-генератора, судя по всему, используют ту же конвенцию `Directory.Build.targets`.)

## Профилактика

- После любого изменения исходников генератора/анализатора считайте «собрать `.sln` и закоммитить DLL» одним неразделимым шагом.
- Перед коммитом убедитесь, что рабочее дерево показывает `.dll` как изменённый; неизменённая DLL означает, что ваша сборка на самом деле её не обновила (не тот проект, ошибка сборки или не задан `IsRoslynComponent`).
- Для сборки вообще требуется [[NET 9 SDK Pin]], а чтобы было что собирать — инициализированные [[Submodule Init|сабмодули]].
- Смотрите на размер бинарного diff, а не на его содержимое: отсутствующая или устаревшая DLL — это и есть режим отказа, поэтому подтвердите, что время изменения/размер сдвинулись.

См. [[Source Generation Pipeline]] о том, что производят эти DLL, и [[Architecture]] об общем потоке сборки.

## Источник

- `Aspid.MVVM.Generators/Directory.Build.targets` — цель авто-копирования `CopyToUnity`
- `Aspid.MVVM/Assets/Aspid/MVVM/*.dll` + `*.dll.meta` — закоммиченные артефакты, метка `RoslynAnalyzer`
- `CLAUDE.md` — нюанс «Generator artifacts are committed»
