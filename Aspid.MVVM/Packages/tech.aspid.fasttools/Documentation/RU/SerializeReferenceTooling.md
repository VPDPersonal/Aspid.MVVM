# SerializeReference Tooling

[Селектор в Инспекторе](SerializeReferences.md) чинит ссылки по одному полю; этот
документ — про проектный масштаб: вкладки окна FastTools для аудита и массовой починки
managed-ссылок, страница Project Settings с гейтом на сборку плеера и та же проверка
в headless-CI. Гейт покрывает и незаданные поля `[TypeSelector(Required = true)]` —
см. свойство `Required` в [TypeSelectorAttribute](Types.md#typeselectorattribute).

**Разделы справочника:**

* [`Bulk repair tabs`](#bulk-repair-tabs) — вкладки **Asset References** и
  **Project References** для аудита и массовой починки по всему проекту;
* [`Project settings & the build/CI gate`](#project-settings--the-buildci-gate) —
  настройки в Project Settings, их scope и гейт на сборку плеера;
* [`Headless CI`](#headless-ci) — `SerializeReferenceCiGate.RunCheck` для batchmode-пайплайнов.

**Краткая версия с теми же примерами — в** [README](README.md#serializereference-selector).

## Bulk repair tabs

[Чинить ссылки по одной](SerializeReferences.md#repairing-broken-references) необязательно:
аудит и массовая починка вынесены в отдельные вкладки окна FastTools.

| Вкладка | Назначение |
|---|---|
| **Asset References** (`Tools → Aspid 🐍 → FastTools → Asset References`) | Строит весь граф managed-ссылок ассета прямо из YAML — дерево по компонентам с путями полей, общими и осиротевшими ссылками, значками `MISSING` / `SHARED` и инлайн-выбором типа на каждой карточке. Достаёт потерянные ссылки, которые инспектор не показывает. |
| **Project References** (`Tools → Aspid 🐍 → FastTools → Project References`) | `Scan Project` обходит каждый `.prefab` / `.asset` / `.unity` под `Assets/`, группирует сломанные ссылки по сохранённому типу и чинит всю группу одним `Fix all` (плюс Smart Fix). Группа, чей сохранённый тип совпадает с объявленным переименованием `[MovedFrom]`, читается как ожидающая миграция, а не поломка — один клик **Migrate all** запекает переименование в файлы, после чего атрибут можно удалить из кода. |

Вкладка **Asset References** раскладывает граф managed-ссылок одного ассета по карточкам
со значками `MISSING` / `SHARED` и инлайн-починкой:

![Вкладка Asset References: граф ссылок ассета с карточкой Fix Missing](../Images/aspid_fasttools_serialize_reference_asset_references.png)

Вкладка **Project References** группирует находки всего проекта по сохранённому типу —
одна группа чинится целиком одним `Fix all`:

![Вкладка Project References: группа сломанных ссылок с Fix all и Smart Fix](../Images/aspid_fasttools_serialize_reference_project_references.png)

## Project settings & the build/CI gate

**`Project Settings → Aspid FastTools → SerializeReference`** содержит:

| Настройка | Scope | Что делает |
|---|---|---|
| **Breakage detection** | per-user | Проактивный тост + предупреждение в Console, когда ссылки заново становятся потерянными после рекомпиляции / импорта. |
| **Auto de-alias duplicated list elements** | коммитимая | Дублированный элемент списка получает собственный экземпляр вместо совместного использования id оригинала. |
| **Build / CI gate** | коммитимая | `Off` / `Warn` / `Fail`: при сборке плеера логировать или прерывать сборку на потерянных (а для CI — и на незаданных обязательных) managed-ссылках. |
| **Excluded scan folders** | коммитимая | Пути, пропускаемые при всех проектных сканах. |

- Коммитимые значения хранятся в `ProjectSettings/SerializeReferenceSharedSettings.asset` — закоммитьте его, чтобы команда и CI вели себя одинаково; breakage detection остаётся per-machine (`EditorPrefs`).
- Rid colours — не настройка: общая ссылка всегда раскрашивается по id — совпадающий цвет и показывает, какие поля делят один экземпляр.

Те же опции продублированы во вкладке **Settings** окна (`Tools → Aspid 🐍 → FastTools → Settings`) и на странице **`Preferences → Aspid FastTools`**, рядом с индивидуальными настройками пикера:

- **Favorites** — переключатель секции.
- **Recent items** — слайдер ёмкости (0–20; 0 скрывает секцию и приостанавливает запись, не стирая историю).
- **Saved lists** — очищает сохранённые Favorites / Recent.
- **Welcome** — переключатель автопоказа.

Каждая строка помечена полоской scope (зелёная — коммитимые, синяя — индивидуальные); закреплённый футер предлагает **Reset to defaults** отдельно для каждого scope (сохранённые списки Favorites / Recent сброс переживают). Все поверхности зеркалят друг друга живьём.

## Headless CI

Для headless-CI та же проверка запускается методом `SerializeReferenceCiGate.RunCheck`:
он сканирует проект, пишет отчёт, логирует каждое нарушение и учитывает коммитимую
строгость гейта — `Off` пропускает проверку, `Warn` логирует, но завершается с кодом 0,
`Fail` завершается с кодом 1 при нарушениях (код 2 — внутренняя ошибка самой проверки).

```bash
Unity -batchmode -quit -projectPath . \
  -executeMethod Aspid.FastTools.SerializeReferences.Editors.SerializeReferenceCiGate.RunCheck \
  -srGateReport SerializeReferenceGateReport.txt -srGateRequired
```

| Флаг | Описание |
|---|---|
| `-srGateReport <path>` | Путь файла отчёта; по умолчанию `SerializeReferenceGateReport.txt` в корне проекта. Каждое нарушение — машиночитаемая строка с типом нарушения, путём ассета и путём поля. |
| `-srGateRequired` | Дополнительно проверяет незаданные поля `[TypeSelector(Required = true)]` в префабах, ScriptableObject и сценах (required-поля верхнего уровня, чистый YAML-проход). |
| `-srGateWarnOnly` | Переопределяет коммитимую строгость на `Warn` для этого запуска: нарушения логируются, но код выхода 0. Выигрывает у `-srGateFail`, если переданы оба. |
| `-srGateFail` | Переопределяет коммитимую строгость на `Fail` для этого запуска: код выхода 1 при нарушениях. |
