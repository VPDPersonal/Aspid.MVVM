---
title: Вики Aspid.MVVM
type: index
status: active
lang: ru
translated_from: index.md
translated_at: 2026-05-31
last_commit: "fd79935291c747db1c6a60b7ee8943fbd4a056a2"
submodule_commits:
  generators: "56d54513be83725300bd35c55aa1a2ec63503d54"
  unity_generators: "d0235ae55fbcab459e79bafdd4981a0083cd5d49"
  analyzers: "ca683bb433811b02227c6180502d297587d0f878"
updated_at: 2026-05-31
---

# Вики Aspid.MVVM

> Навигационная база знаний по фреймворку Aspid.MVVM. **Начните отсюда. Читайте вики до чтения исходников.** Источник истины — английская вики (`docs/wiki/`); это её перевод. Ссылки `[[...]]` ведут на страницы внутри этого же русского vault.

## Ориентация — 8 вопросов

1. **Что это?** MVVM-фреймворк для Unity на основе Source Generator: чистое разделение View / ViewModel / бизнес-логики, ноль рефлексии в биндингах, минимум аллокаций. → [[Architecture]]
2. **С чего начать?** → [[Getting Started]] · примеры [[Samples]] (Counter, Greeter, HelloWorld, Stats, TodoList, VirtualizedList)
3. **Зачем он нужен?** Чтобы убрать MVVM-бойлерплейт и стоимость runtime-рефлексии в Unity за счёт генерации кода на этапе сборки. → [[Architecture]], [[ViewModel Generation]]
4. **Что происходит при сборке и в runtime?** `[ViewModel]`/`[Bind]`/`[RelayCommand]` генерируют вторую половину каждого `partial`-типа на этапе сборки; в runtime [[Binders Catalog|биндеры]] соединяют UI слоя View с членами ViewModel. → [[ViewModel to Generated Code]], [[Runtime Binding Resolution]]
5. **Где живёт состояние?** В полях [[ViewModel]], помеченных `[Bind]`; биндинги распространяются согласно [[BindMode]].
6. **Какие основные части?** Базовые контракты ([[IViewModel]], [[IRelayCommand]], [[IBinder]]), конвейер генерации ([[ViewModel Generation]]), [[Binders Catalog|каталог биндеров]] и [[Converters]].
7. **Что нельзя ломать?** → [[Must Be Partial]], [[Committed DLLs]], [[Submodule Init]], [[NET 9 SDK Pin]]
8. **Куда смотреть в первую очередь?** В этот индекс, затем в нужную страницу `concepts/`, затем в `source_paths` этой страницы.

## Карта

- **overview/** — [[Architecture]] · [[Getting Started]]
- **concepts/** — [[ViewModel Generation]] · [[BindMode]] · [[Data Binding]] · [[Bindable Members]] · [[Relay Commands]] · [[Source Generation Pipeline]] · [[DI Integration]] · [[Binder Base Classes]]
- **entities/** — [[ViewModel]] · [[IViewModel]] · [[IRelayCommand]] · [[IBinder]] · [[View]]
- **flows/** — [[ViewModel to Generated Code]] · [[Runtime Binding Resolution]] · [[View Initialization]]
- **generation/** — [[Source Generator]] · [[Unity Generators]] · [[Analyzer]]
- **converters/** — [[Converters]] · [[Bool Converters]] · [[Number Converters]] · [[String Converters]] · [[Specific Converters]]
- **risks/** — [[Must Be Partial]] · [[Committed DLLs]] · [[Submodule Init]] · [[NET 9 SDK Pin]]
- **reference/** — [[Samples]] · [[Unity Editor Tooling]] · [[External Dependencies]] · [[StarterKit ViewModels]]
- **binders/** — [[Binders Catalog]] (33 категории): [[Text Binders]] · [[Image Binders]] · [[RawImage Binders]] · [[Toggle Binders]] · [[Slider Binders]] · [[Scrollbar Binders]] · [[Scrollrect Binders]] · [[Dropdown Binders]] · [[InputField Binders]] · [[Button Binders]] · [[Selectable Binders]] · [[Transform Binders]] · [[Animator Binders]] · [[AudioSource Binders]] · [[CanvasGroup Binders]] · [[Graphic Binders]] · [[Renderer Binders]] · [[LineRenderer Binders]] · [[Layout Binders]] · [[GameObject Binders]] · [[Object Binders]] · [[Collider Binders]] · [[Behaviour Binders]] · [[EventTrigger Binders]] · [[UnityEvent Binders]] · [[LocalizeStringEvent Binders]] · [[VirtualizedList Binders]] · [[Collection Binders]] · [[Caster Binders]] · [[Generic Binders]] · [[Debug Binders]] · [[Mono Binders]]

## Сопровождение

Создаётся и синхронизируется скиллом **`aspid-wiki`**. Перевод следует за английской вики: при изменении английской страницы её русскую версию нужно перевести заново. См. [[log]].
