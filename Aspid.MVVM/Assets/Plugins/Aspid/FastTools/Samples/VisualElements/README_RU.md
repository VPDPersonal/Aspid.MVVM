# Пример VisualElements

Кастомный UIToolkit Inspector для `ScriptableObject` `AbilityConfig`, демонстрирующий внутренние редакторные VisualElement-компоненты Aspid и fluent-API расширений `VisualElement`.

Смотрите:

- `Scripts/Editor/AbilityConfigEditor.cs:18` — `AspidInspectorHeader` с динамическим акцентом `StatusStyle` (Warning, когда способность бесплатная, иначе Success).
- `Scripts/Editor/AbilityConfigEditor.cs:24` — контейнер `AspidBox`, собранный fluent-вызовами `.SetPadding(...)` и цепочкой `.AddChild(...)`, оборачивающий редакторы `PropertyField`.
- `Scripts/Editor/AbilityConfigEditor.cs:33` — условный `AspidHelpBox` с предупреждением, добавляемый только когда `manaCost == 0`.

Чтобы попробовать:

1. Выберите `ScriptableObjects/AbilityConfigAsset.asset` в окне Project — кастомный inspector появится в панели Inspector.
2. Отредактируйте поля. Установите `Mana Cost` в `0`, чтобы увидеть, как акцент статуса переключится на Warning, и появится встроенный help box.
3. Или создайте свой через `Assets > Create > Aspid > FastTools > Samples > Ability Config`.
