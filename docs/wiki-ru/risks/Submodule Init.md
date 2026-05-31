---
title: Инициализация сабмодулей
type: risk
status: active
source_paths:
  - .gitmodules
  - CLAUDE.md
tags:
  - risk
  - git
  - setup
  - submodules
  - build
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/risks/Submodule Init.md
translated_at: 2026-05-31
---

# Инициализация сабмодулей

> Клонирование без сабмодулей оставляет репозитории генератора/анализатора пустыми, поэтому Unity нечего потреблять, и проект не компилируется. Всегда клонируйте с рекурсивным извлечением сабмодулей.

## Симптом

После свежего `git clone` Unity сообщает об ошибках компиляции: атрибуты `[ViewModel]`, `[Bind]` и `[RelayCommand]` распознаются, но никаких сгенерированных членов не появляется, а типы из проектов генератора/анализатора отсутствуют. Каталоги `Aspid.MVVM.Generators/`, `Aspid.MVVM.Analyzers/` и `Aspid.MVVM.Unity.Generators/` существуют, но пусты.

## Причина

Проект подключает три git-сабмодуля (см. факты по `.gitmodules` ниже), объявленные в `.gitmodules`:

- `Aspid.MVVM.Generators`
- `Aspid.MVVM.Analyzers`
- `Aspid.MVVM.Unity.Generators`

Обычный `git clone` записывает указатели на сабмодули, но **не** загружает их содержимое. Пока они не извлечены, исходный код [[Source Generator|генератора исходного кода]], [[Analyzer|анализатора]] и [[Unity Generators|Unity-генераторов]] отсутствует.

Примечание: это касается *исходной* стороны. Сам Unity потребляет [[Committed DLLs|закоммиченные DLL]], добавленные в `Aspid.MVVM/Assets/Aspid/MVVM/`, а не исходники сабмодулей — поэтому пустой сабмодуль не всегда сразу ломает редактор. Однако он ломает пересборку генераторов (см. [[Source Generation Pipeline|конвейер генерации исходного кода]]) и любую работу над кодом генератора/анализатора. Формулировка «Unity не скомпилируется» в CLAUDE.md — это наихудший исход; надёжно воспроизводимый сбой заключается в том, что вы не сможете перегенерировать DLL.

[[External Dependencies|Aspid.Collections]] **не** является сабмодулем — он потребляется как UPM git-пакет (`tech.aspid.collections`), поэтому эта проблема его не затрагивает.

## Решение

Если вы ещё не клонировали:

```bash
git clone --recurse-submodules <repo-url>
```

Если вы уже клонировали без сабмодулей:

```bash
git submodule update --init --recursive
```

Затем пересоберите решение генератора и заново закоммитьте обновлённую DLL, если вы меняли код генератора (см. [[Source Generator]] и [[Committed DLLs]]).

## Профилактика

- Сделайте `--recurse-submodules` своей привычкой клонирования по умолчанию; также можно задать `git config --global submodule.recurse true`, чтобы `pull`/`checkout` автоматически работали с сабмодулями.
- После переключения веток, которые сдвигают указатели сабмодулей, повторно выполните `git submodule update --init --recursive`.
- См. также [[NET 9 SDK Pin]] и [[Getting Started]] для остальной части первоначальной настройки, а также [[Architecture]] — чтобы понять, как сабмодули встраиваются в сборку.
