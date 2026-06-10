---
title: Инструментарий редактора Unity
type: reference
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Unity/Editor/Scripts/Binders/MonoBinderEditor.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Unity/Editor/Scripts/Views/ViewEditor.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Unity/Editor/Scripts/Views/ViewAndMonoBinderSyncValidator.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Unity/Editor/Scripts/Binders/AddBinderContextMenu.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Unity/Editor/Scripts/ViewModels/Debugs/DebugViewModelPanel.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Unity/Editor/Scripts/Settings/AspidMvvmSettings.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Unity/Editor/Scripts/Settings/AspidMvvmSettingsWindow.cs
tags: [reference, unity, editor, inspector, debug, uitoolkit]
updated_at: 2026-05-31
lang: ru
translated_from: reference/Unity Editor Tooling.md
translated_at: 2026-05-31
---

# Инструментарий редактора Unity

> Слой, работающий только в редакторе (~108 файлов в `Unity/Editor/`), который превращает «сырые» сериализуемые поля в управляемые инспекторы биндеров/представлений, панель отладки ViewModel во время выполнения и окно настроек.

Весь код редактора обёрнут в `#if !ASPID_MVVM_EDITOR_DISABLED` и расположен в `Aspid.MVVM.Unity.Editor.asmdef`. Инспекторы построены на UI Toolkit (UIElements); стилизация берётся из таблиц `.uss` в `Editor/Resources/Styles/`. Это инструментарий для редактирования компонентов [[View]]/[[Binder Base Classes|MonoBinder]] и просмотра сгенерированного состояния [[ViewModel]] — он не выполняется в сборках.

## Пользовательские инспекторы (`CustomEditor`)

- **Инспектор биндера** — `MonoBinderEditor` (применяется ко всем подклассам [[Binder Base Classes|MonoBinder]] через `editorForChildClasses`). Отрисовывает выпадающие списки **ID** и **View** вместо обычных строковых/объектных полей, перекрашивает их, когда значение откатывается к «предыдущей» записи, и поддерживает корректность выбранного идентификатора [[Bindable Members|связываемого члена]] относительно разрешённого [[View]].
- **Инспекторы представления** — обобщённая база `ViewEditor<T,TEditor>` с конкретными редакторами `MonoViewEditor` / `ScriptableViewEditor`; управляют интерфейсом списка биндеров и выявляют неназначенные биндеры.
- **Инспекторы ViewModel** — `MonoViewModelEditor` / `ScriptableViewModelEditor` поверх базы `ViewModelEditor`.

## Пользовательские отрисовщики свойств

`BindModeDrawer` отрисовывает перечисление [[BindMode]]; `AspidSlider`/`AspidSliderInt`/`AspidToggle`/`AspidDelegateField` — стилизованные варианты полей UIElements, переиспользуемые в инспекторах.

## Синхронизация представления и биндера

`ViewAndMonoBinderSyncValidator` поддерживает синхронизацию обязательных слотов биндеров у [[View]] с компонентами [[Binder Base Classes|MonoBinder]] в его области видимости на сцене — добавляя, удаляя или восстанавливая ссылки по мере изменений в инспекторе. `ValidableBindersById` и `ViewAndMonoBinderSyncValidator` обеспечивают [[Runtime Binding Resolution|разрешение на основе идентификаторов]], на которое опирается связывание.

## Контекстное меню

`AddBinderContextMenu` регистрирует пункт правого клика «Add Binder» на сериализуемых свойствах, сканируя все сборки на предмет совместимых типов биндеров (управляется через `AddBinderContextMenuAttribute` / `RequireBinderAttribute`). _(Предположение: формируется на основе сканирования атрибутов при перезагрузке скриптов.)_

## Панель отладки ViewModel

`DebugViewModelPanel` рефлексивно анализирует живой [[IViewModel]] во время воспроизведения и отрисовывает по одному полю отладки на каждый член: сгенерированные [[Bindable Members|связываемые]] члены, [[Relay Commands|relay-команды]] и обычные поля распределяются по отдельным вкладкам, с фильтром поиска по префиксу `t:`. Семейство `Debug*Field` (~45 файлов: `DebugIntegerField`, `DebugVector3Field`, `DebugEnumField`, `DebugRelayCommandField`, ...) покрывает каждый тип значения; резервные поля сгенерированных членов скрыты.

## Настройки

`AspidMvvmSettings` переключает три символа определения скриптов — профайлер, лог биндеров и переключатель проверок редактора `ASPID_MVVM_EDITOR_DISABLED`; `AspidMvvmSettingsWindow` — это `EditorWindow`, который их предоставляет.

## Исходный код

- `Editor/Scripts/Binders/` — `MonoBinderEditor`, отрисовщики, данные выпадающих списков
- `Editor/Scripts/Views/` — редакторы представлений + валидатор синхронизации
- `Editor/Scripts/ViewModels/Debugs/` — панель отладки + семейство `Debug*Field`
- `Editor/Scripts/Settings/` — окно настроек

См. [[View Initialization]], [[ViewModel to Generated Code]], [[Committed DLLs]].
