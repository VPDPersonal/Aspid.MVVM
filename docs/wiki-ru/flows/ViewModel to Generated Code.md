---
title: От ViewModel к сгенерированному коду
type: flow
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/IViewModel.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/FindBindableMemberParameters.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/FindBindableMemberResult.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Generation/ViewModelAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Generation/BindAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Generation/BindIdAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Generation/IgnoreBindAttribute.cs
  - Aspid.MVVM.Generators/Aspid.MVVM.Generators/Aspid.MVVM.Generators/Generators/ViewModels/ViewModelGenerator.cs
  - Aspid.MVVM.Generators/Aspid.MVVM.Generators/Aspid.MVVM.Generators/Generators/ViewModels/Body/FindBindableMembersBody.cs
tags: [flow, viewmodel, source-generation, binding]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/flows/ViewModel to Generated Code.md
translated_at: 2026-05-31
---

# От ViewModel к сгенерированному коду

> Как аннотированный `partial`-класс превращается в работающий во время выполнения [[IViewModel]] с привязываемыми членами — путь, который никто не пишет руками, потому что за вас его пишет [[Source Generator]].

Суть этого потока: вы описываете *намерение* (поля + атрибуты), а генератор на этапе сборки производит шаблонную обвязку `IViewModel`. Во время выполнения нет рефлексии, поэтому поиск привязок остаётся быстрым и без аллокаций.

1. **Вы аннотируете `partial`-класс.** Поставьте `[ViewModel]` на класс и `[Bind]` на поля, которые хотите открыть для привязки. `[ViewModelAttribute]` применяется к классу или структуре, не наследуется и является чисто маркером. Модификатор `partial` обязателен — генератор испускает *вторую* половину partial-класса (см. [[Must Be Partial]]).

2. **Генератор обнаруживает тип.** `ViewModelGenerator` — это `IIncrementalGenerator`. Он использует `ForAttributeWithMetadataName` для поиска кандидатов, после чего `SyntacticPredicate` отфильтровывает их до нестатических объявлений `partial class` с хотя бы одним атрибутом. Статические классы пока не поддерживаются (согласно `// TODO` в исходниках).

3. **Генератор моделирует тип.** `FindViewModels` читает `INamedTypeSymbol`, определяет, несёт ли уже базовый тип `[ViewModel]` (устанавливает `Inheritor.Inheritor` или `Inheritor.None`), собирает привязываемые члены через фабрику, группирует их по длине ID и собирает пользовательские интерфейсы ViewModel в `ViewModelData`.

4. **Генератор испускает члены.** `GenerateCode` запускает семь эмиттеров, среди которых `BindableMembers` (свойство на каждое поле с `[Bind]` — см. [[Bindable Members]]), `RelayCommandBody` (для методов с `[RelayCommand]` — см. [[Relay Commands]]), тела `GeneratedProperties`/`PropertyNotification` и `FindBindableMembersBody`.

5. **Генерируется таблица поиска привязок.** `FindBindableMembersBody` пишет `IViewModel.FindBindableMember`. Он вкладывает `switch` по `parameters.Id.Length`, а затем `switch` по строке ID, возвращая `new FindBindableMemberResult(<PropertyName>)`. Эта диспетчеризация «сначала по длине, потом по строке» даёт почти O(1) поиск без рефлексии. Когда тип наследуется от другой ViewModel, испускается `public override` с откатом на `base.FindBindableMember(parameters)`; в противном случае возвращается `default`. Тело оборачивается в `ProfilerMarker`, если только не отключён define профайлера.

6. **ID разрешаются.** По умолчанию ID члена — это его имя; `[BindId("custom")]` переопределяет его (поле/свойство/метод), а `[IgnoreBind]` исключает член из привязки со стороны View. [[IViewModel|FindBindableMemberParameters]] несёт запрашиваемый `Id`; [[IViewModel|FindBindableMemberResult]] сообщает `IsFound` плюс `IBinderAdder`.

7. **Время выполнения использует это.** Во время выполнения [[View]] передаёт ID в `FindBindableMember`; сгенерированный switch возвращает adder, который связывает [[IBinder]] со свойством согласно его [[BindMode]]. См. [[Runtime Binding Resolution]] и [[View Initialization]].

> Примечание: код генератора находится в submodule; Unity потребляет закоммиченный DLL ([[Committed DLLs]]), а не исходники. Полный конвейер описан в [[Source Generation Pipeline]] и [[ViewModel Generation]].
