---
title: Внешние зависимости
type: reference
status: active
source_paths:
  - Aspid.MVVM/Packages/manifest.json
  - CLAUDE.md
  - Readme.md
tags:
  - reference
  - dependencies
  - upm
  - integration
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/reference/External Dependencies.md
translated_at: 2026-05-31
---

# Внешние зависимости

> Сторонние пакеты, на которые опирается Aspid.MVVM, способ подключения каждого из них и те, что отсутствуют в этом чекауте — прочитайте это, прежде чем гоняться за ошибкой компиляции «missing type».

Aspid.MVVM держит своё ядро (ту часть, что потребляет [[Committed DLLs]]) лёгким, но компоненты [[Samples]] и [[StarterKit ViewModels|StarterKit]] зависят от нескольких внешних пакетов, объявленных в `Aspid.MVVM/Packages/manifest.json`. Большинство из них подтягиваются через UPM git URL, а не через сам ассет; если вы скопируете в другой проект только `Assets/Aspid/`, вам придётся переподключить их самостоятельно.

## Ключевые зависимости

| Пакет | UPM id | Источник | Для чего используется |
|---|---|---|---|
| Aspid.Collections | `tech.aspid.collections` | git `#upm` | Наблюдаемые коллекции (`ObservableList<T>` и т. п.), [[Collection Binders]], [[VirtualizedList Binders]] |
| UniTask | `com.cysharp.unitask` | git `#2.1.0` | Утилиты async/await без аллокаций |
| VContainer | `jp.hadashikick.vcontainer` | git `#1.16.0` | [[DI Integration]] для [[View Initialization]] |
| Zenject | (отсутствует в manifest) | внешний | Альтернативная цель для [[DI Integration]] |
| TextMeshPro | через `com.unity.ugui` 2.0.0 | реестр Unity | Text/[[InputField Binders]], [[Dropdown Binders]] |

## Aspid.Collections — это НЕ субмодуль

Это самое частое заблуждение. В отличие от репозиториев генератора/анализатора (см. [[Submodule Init]]), Aspid.Collections **не** является git-субмодулем — это UPM git-пакет (`tech.aspid.collections`). Поэтому он **отсутствует в этом рабочем дереве**: в данном чекауте нет папки `Assets/Aspid/Collections/`. Unity скачивает его в кэш пакетов при открытии проекта. В карте проекта в CLAUDE.md показан элемент `Collections/`, но он отражает раскладку разрешённого пакета, а не файлы, закоммиченные здесь.

Если вы выполните `grep` по исходникам `ObservableList<T>` и ничего не найдёте — это ожидаемо: он живёт во внешнем пакете, а не в этом репозитории.

## DI подключается по выбору

Zenject и VContainer — это *цели* интеграции, а не жёсткие требования ядра. VContainer прописан в manifest; Zenject упоминается в ссылках сборок StarterKit и поддерживается, но здесь не объявлен, поэтому при использовании его, скорее всего, придётся добавлять вручную. Инициализаторы View из [[StarterKit ViewModels|StarterKit]] ветвятся в зависимости от того, какой контейнер присутствует. См. [[DI Integration]].

## UniTask и TextMeshPro

UniTask (зафиксирован на `2.1.0`) обеспечивает асинхронные потоки. TextMeshPro поставляется внутри `com.unity.ugui` 2.0.0 (современная упаковка Unity), поэтому [[Text Binders]] и биндеры полей, нацеленные на `TMP_Text`, разрешаются без отдельной строки зависимости.

## Источник

- `Aspid.MVVM/Packages/manifest.json` — авторитетный список зависимостей
- `CLAUDE.md`, `Readme.md` — обзор и заметки по интеграции

См. также: [[Submodule Init]], [[Committed DLLs]], [[Getting Started]].
