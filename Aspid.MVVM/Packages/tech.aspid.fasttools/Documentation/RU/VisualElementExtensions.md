# VisualElement Extensions — full reference

Fluent-методы расширения для построения UIToolkit-деревьев в коде. Все методы возвращают `T` (сам элемент) для цепочки вызовов.

```csharp
using Aspid.FastTools.UIElements;         // runtime-расширения
using Aspid.FastTools.UIElements.Editors; // editor-only расширения (например, AddOpenScriptCommand)
```

## Core element operations

```csharp
element
    .SetName("MyElement")
    .SetVisible(true)
    .SetTooltip("Текст подсказки")
    .AddChild(new Label("Hello"))
    .AddChildren(child1, child2, child3);
```

| Метод | Описание |
|-------|----------|
| `SetName(string)` | Устанавливает `element.name` |
| `SetVisible(bool)` | Устанавливает `element.visible` |
| `SetTooltip(string)` | Устанавливает `element.tooltip` |
| `SetUserData(object)` | Устанавливает `element.userData` |
| `SetEnabledSelf(bool)` | Устанавливает `element.enabledSelf` |
| `SetPickingMode(PickingMode)` | Устанавливает `element.pickingMode` |
| `SetUsageHints(UsageHints)` | Устанавливает `element.usageHints` |
| `SetViewDataKey(string)` | Устанавливает `element.viewDataKey` |
| `SetLanguageDirection(LanguageDirection)` | Устанавливает `element.languageDirection` |
| `SetDisablePlayModeTint(bool)` | Устанавливает `element.disablePlayModeTint` |
| `SetDataSource(object)` | Устанавливает `element.dataSource` |
| `SetDataSourceType(Type)` | Устанавливает `element.dataSourceType` |
| `SetDataSourcePath(PropertyPath)` | Устанавливает `element.dataSourcePath` |
| `AddChild(VisualElement)` | Добавляет дочерний элемент, возвращает родителя |
| `AddChildren(params VisualElement[])` | Добавляет несколько дочерних элементов |
| `AddChildren(IEnumerable<VisualElement>)` | Добавляет из последовательности |
| `AddChildren(List<VisualElement>)` | Добавляет из списка |
| `AddChildren(Span<VisualElement>)` | Добавляет из span |
| `AddChildren(ReadOnlySpan<VisualElement>)` | Добавляет из read-only span |
| `InsertChild(int, VisualElement)` | Вставляет дочерний элемент по указанному индексу |
| `InsertChildren(int, params VisualElement[])` | Вставляет несколько дочерних элементов начиная с индекса |
| `InsertChildren(int, IEnumerable<VisualElement>)` | Вставляет из последовательности |
| `InsertChildren(int, List<VisualElement>)` | Вставляет из списка |
| `InsertChildren(int, Span<VisualElement>)` | Вставляет из span |
| `InsertChildren(int, ReadOnlySpan<VisualElement>)` | Вставляет из read-only span |

> `RegisterCallbackOnce<TEventType>` и `RegisterCallbackOnce<TEventType, TUserArgsType>` доступны на всех версиях Unity (пакет содержит polyfill для версий до 2023.1).

## Focusable

| Метод | Описание |
|-------|----------|
| `SetFocus()` | Устанавливает фокус на элемент |
| `SetBlur()` | Снимает фокус с элемента |
| `IsFocus()` | Возвращает, находится ли элемент в фокусе |
| `SetTabIndex(int)` | Устанавливает `element.tabIndex` |
| `SetFocusable(bool)` | Устанавливает `element.focusable` |
| `SetDelegatesFocus(bool)` | Устанавливает `element.delegatesFocus` |

## USS & class operations

| Метод | Описание |
|-------|----------|
| `AddClass(string)` | Добавляет USS-класс |
| `RemoveClass(string)` | Удаляет USS-класс |
| `ClearClasses()` | Удаляет все USS-классы |
| `ToggleInClass(string)` | Переключает USS-класс вкл/выкл |
| `EnableInClass(string, bool)` | Добавляет или удаляет USS-класс по условию |
| `AddStyleSheets(StyleSheet)` | Добавляет `StyleSheet` |
| `RemoveStyleSheets(StyleSheet)` | Удаляет `StyleSheet` |
| `AddStyleSheetsFromResource(string)` | Добавляет таблицу стилей через `Resources.Load` |
| `RemoveStyleSheetsFromResource(string)` | Удаляет таблицу стилей, загруженную через `Resources.Load` |

## Style extensions — by category

Все методы стилей также доступны напрямую на `IStyle` (те же имена методов, работают с объектом стиля).

### Layout

| Метод | Свойство стиля |
|-------|----------------|
| `SetFlexBasis(StyleLength)` | `flexBasis` |
| `SetFlexGrow(StyleFloat)` | `flexGrow` |
| `SetFlexShrink(StyleFloat)` | `flexShrink` |
| `SetFlexWrap(StyleEnum<Wrap>)` | `flexWrap` |
| `SetFlexDirection(FlexDirection)` | `flexDirection` |
| `SetAlignSelf(StyleEnum<Align>)` | `alignSelf` |
| `SetAlignItems(StyleEnum<Align>)` | `alignItems` |
| `SetAlignContent(StyleEnum<Align>)` | `alignContent` |
| `SetJustifyContent(StyleEnum<Justify>)` | `justifyContent` |
| `SetPosition(StyleEnum<Position>)` | `position` |

### Size

| Метод | Описание |
|-------|----------|
| `SetSize(StyleLength)` | Устанавливает ширину и высоту одновременно |
| `SetSize(width?, height?)` | Устанавливает ширину и/или высоту независимо |
| `SetMinSize(StyleLength)` | Устанавливает minWidth и minHeight одновременно |
| `SetMinSize(width?, height?)` | |
| `SetMaxSize(StyleLength)` | Устанавливает maxWidth и maxHeight одновременно |
| `SetMaxSize(width?, height?)` | |
| `SetWidth(StyleLength)` | `width` |
| `SetMinWidth(StyleLength)` | `minWidth` |
| `SetMaxWidth(StyleLength)` | `maxWidth` |
| `SetHeight(StyleLength)` | `height` |
| `SetMinHeight(StyleLength)` | `minHeight` |
| `SetMaxHeight(StyleLength)` | `maxHeight` |

### Spacing

Все методы отступов имеют перегрузку с единым значением, перегрузку по сторонам (`top`, `right`, `bottom`, `left`), сеттеры по одной стороне и сеттеры по парам осей X/Y.

| Метод | Свойства стиля |
|-------|----------------|
| `SetMargin(…)` / `SetPadding(…)` / `SetDistance(…)` | `Top/Right/Bottom/Left` (общее значение или per-side) |
| `SetMarginX/Y` · `SetPaddingX/Y` · `SetDistanceX/Y` | Устанавливает горизонтальную (X = `Left`+`Right`) или вертикальную (Y = `Top`+`Bottom`) пару |
| `SetMarginTop/Right/Bottom/Left` | Margin одной стороны |
| `SetPaddingTop/Right/Bottom/Left` | Padding одной стороны |
| `SetDistanceTop/Right/Bottom/Left` *(через `SetTop` / `SetRight` / `SetBottom` / `SetLeft`)* | Смещение одной стороны для абсолютного позиционирования (свойства `top` / `right` / `bottom` / `left`) |

> `SetDistance` — обёртка для четырёх свойств `top`/`right`/`bottom`/`left`, используемых при абсолютном позиционировании. `SetTop`, `SetRight`, `SetBottom`, `SetLeft` — это прямые алиасы для одного свойства.

### Font

| Метод | Свойство стиля |
|-------|----------------|
| `SetUnityFont(StyleFont)` | `unityFont` |
| `SetFontSize(StyleLength)` | `fontSize` |
| `SetUnityFontDefinition(StyleFontDefinition)` | `unityFontDefinition` |
| `SetUnityFontStyleAndWeight(StyleEnum<FontStyle>)` | `unityFontStyleAndWeight` |

### Font style presets

Удобные методы для переключения bold / italic без перезаписи другого флага:

| Метод | Описание |
|-------|----------|
| `SetNormalUnityFontStyleAndWeight()` | Сбрасывает в `FontStyle.Normal` |
| `AddBoldUnityFontStyleAndWeight()` | Добавляет bold, сохраняя italic |
| `RemoveBoldUnityFontStyleAndWeight()` | Убирает bold, сохраняя italic |
| `AddItalicUnityFontStyleAndWeight()` | Добавляет italic, сохраняя bold |
| `RemoveItalicUnityFontStyleAndWeight()` | Убирает italic, сохраняя bold |

### Text

| Метод | Свойство стиля | Примечания |
|-------|---------------|------------|
| `SetWorldSpacing(StyleLength)` | `wordSpacing` | |
| `SetLetterSpacing(StyleLength)` | `letterSpacing` | |
| `SetUnityTextAlign(TextAnchor)` | `unityTextAlign` | |
| `SetTextShadow(StyleTextShadow)` | `textShadow` | |
| `SetUnityTextOutlineColor(StyleColor)` | `unityTextOutlineColor` | |
| `SetUnityTextOutlineWidth(StyleFloat)` | `unityTextOutlineWidth` | |
| `SetUnityParagraphSpacing(StyleLength)` | `unityParagraphSpacing` | |
| `SetTextOverflow(StyleEnum<TextOverflow>)` | `textOverflow` | |
| `SetUnityTextOverflowPosition(TextOverflowPosition)` | `unityTextOverflowPosition` | |
| `SetUnityTextGenerator(TextGeneratorType)` | `unityTextGenerator` | Unity 6+ |
| `SetUnityEditorTextRenderingMode(EditorTextRenderingMode)` | `unityEditorTextRenderingMode` | Unity 6+ |
| `SetUnityTextAutoSize(StyleTextAutoSize)` | `unityTextAutoSize` | Unity 6.2+ |
| `SetWhiteSpace(StyleEnum<WhiteSpace>)` | `whiteSpace` | |

### Color & Opacity

| Метод | Свойство стиля |
|-------|----------------|
| `SetColor(StyleColor)` | `color` |
| `SetColor(string)` | `color`, разобранный из HTML-строки (`"#RRGGBB"` или именованный цвет) |
| `SetOpacity(StyleFloat)` | `opacity` |

### Border

| Метод | Описание |
|-------|----------|
| `SetBorderColor(StyleColor)` | Все стороны |
| `SetBorderColor(top?, right?, bottom?, left?)` | По стороне |
| `SetBorderColorX(StyleColor)` · `SetBorderColorY(StyleColor)` | Горизонтальная (left + right) или вертикальная (top + bottom) пара |
| `SetBorderColorTop/Right/Bottom/Left(StyleColor)` | Одна сторона |
| `SetBorderRadius(StyleLength)` | Все углы |
| `SetBorderRadius(topLeft?, topRight?, bottomLeft?, bottomRight?)` | По углу |
| `SetBorderRadiusTop(StyleLength)` · `SetBorderRadiusBottom(StyleLength)` | Пара верхних или нижних углов |
| `SetBorderRadiusTopLeft/TopRight/BottomLeft/BottomRight(StyleLength)` | Один угол |
| `SetBorderWidth(StyleFloat)` | Все стороны |
| `SetBorderWidth(top?, right?, bottom?, left?)` | По стороне |
| `SetBorderWidthX(StyleFloat)` · `SetBorderWidthY(StyleFloat)` | Горизонтальная или вертикальная пара |
| `SetBorderWidthTop/Right/Bottom/Left(StyleFloat)` | Одна сторона |

### Background

| Метод | Свойство стиля |
|-------|----------------|
| `SetBackgroundColor(StyleColor)` | `backgroundColor` |
| `SetBackgroundColor(string)` | `backgroundColor`, разобранный из HTML-строки (`"#RRGGBB"` или именованный цвет) |
| `SetBackgroundImage(StyleBackground)` | `backgroundImage` |
| `SetBackgroundImageFromResource(string)` | Загружает `Texture2D` через `Resources.Load` и присваивает его в `backgroundImage` |
| `SetBackgroundSize(StyleBackgroundSize)` | `backgroundSize` |
| `SetBackgroundRepeat(StyleBackgroundRepeat)` | `backgroundRepeat` |
| `SetBackgroundPosition(StyleBackgroundPosition)` | X и Y одновременно |
| `SetBackgroundPosition(x?, y?)` | Независимо |
| `SetBackgroundPositionX(StyleBackgroundPosition)` | `backgroundPositionX` |
| `SetBackgroundPositionY(StyleBackgroundPosition)` | `backgroundPositionY` |
| `SetUnityBackgroundImageTintColor(StyleColor)` | `unityBackgroundImageTintColor` |

### Transform

| Метод | Свойство стиля |
|-------|----------------|
| `SetScale(StyleScale)` | `scale` |
| `SetRotate(StyleRotate)` | `rotate` |
| `SetTranslate(StyleTranslate)` | `translate` |
| `SetTransformOrigin(StyleTransformOrigin)` | `transformOrigin` |

### Aspect, Filter & Material

Доступно начиная с Unity 6000.3+.

| Метод | Свойство стиля |
|-------|----------------|
| `SetAspectRation(StyleRatio)` | `aspectRatio` *(имя метода сохраняет опечатку из исходника)* |
| `SetFilter(StyleList<FilterFunction>)` | `filter` |
| `SetUnityMaterial(StyleMaterialDefinition)` | `unityMaterial` |

### Transition

| Метод | Свойство стиля |
|-------|----------------|
| `SetTransitionDelay(StyleList<TimeValue>)` | `transitionDelay` |
| `SetTransitionDuration(StyleList<TimeValue>)` | `transitionDuration` |
| `SetTransitionProperty(StyleList<StylePropertyName>)` | `transitionProperty` |
| `SetTransitionTimingFunction(StyleList<EasingFunction>)` | `transitionTimingFunction` |

### Overflow & Visibility

| Метод | Свойство стиля |
|-------|----------------|
| `SetOverflow(StyleEnum<Overflow>)` | `overflow` |
| `SetUnityOverflowClipBox(StyleEnum<OverflowClipBox>)` | `unityOverflowClipBox` |
| `SetVisibility(StyleEnum<Visibility>)` | `visibility` |
| `SetDisplay(DisplayStyle)` | `display` |

### Unity Slice

| Метод | Описание |
|-------|----------|
| `SetUnitySlice(StyleInt)` | Все стороны |
| `SetUnitySlice(top?, right?, bottom?, left?)` | По стороне |
| `SetUnitySliceX(StyleInt)` · `SetUnitySliceY(StyleInt)` | Горизонтальная (left + right) или вертикальная (top + bottom) пара |
| `SetUnitySliceTop/Right/Bottom/Left(StyleInt)` | Одна сторона |
| `SetUnitySliceScale(StyleFloat)` | `unitySliceScale` |
| `SetUnitySliceType(StyleEnum<SliceType>)` | Unity 6+ |

### Cursor

| Метод | Свойство стиля |
|-------|----------------|
| `SetCursor(StyleCursor)` | `cursor` |

## Specialized element extensions

### TextElement

```csharp
label
    .SetText("Hello World")
    .SetEnableRichText(true)
    .SetParseEscapeSequences(true);
```

| Метод | Описание |
|-------|----------|
| `SetText(string)` | Устанавливает отображаемый текст |
| `SetEnableRichText(bool)` | Включает разбор тегов rich-text |
| `SetEmojiFallbackSupport(bool)` | Включает emoji-fallback при рендеринге |
| `SetParseEscapeSequences(bool)` | Обрабатывать ли escape-последовательности (например, `\n`) |
| `SetDisplayTooltipWhenElided(bool)` | Показывать обрезанный текст в подсказке при наведении |

### ITextEdition (TextField, IntegerField, …)

```csharp
textField
    .SetPlaceholder("Поиск…")
    .SetMaxLength(64)
    .SetIsDelayed(true);
```

| Метод | Описание |
|-------|----------|
| `SetMaxLength(int)` | Максимальное число символов |
| `SetMaskChar(char)` | Символ для маскировки пароля |
| `SetIsDelayed(bool)` | Откладывает изменение значения до потери фокуса / Enter |
| `SetIsReadOnly(bool)` | Запрещает редактирование |
| `SetIsPassword(bool)` | Включает password-режим (использует mask char) |
| `SetPlaceholder(string)` | Текст-плейсхолдер для пустого поля |
| `SetAutoCorrection(bool)` | Включает автокоррекцию (mobile) |
| `SetHideMobileInput(bool)` | Скрывает мобильный soft input |
| `SetHideSoftKeyboard(bool)` | Скрывает экранную клавиатуру |
| `SetHidePlaceholderOnFocus(bool)` | Убирает плейсхолдер при фокусе |
| `SetKeyboardType(TouchScreenKeyboardType)` | Тип touch-screen клавиатуры |

### ITextSelection

```csharp
textField
    .SetIsSelectable(true)
    .SetSelectAllOnFocus(true)
    .AddOnCursorIndexChange(() => Debug.Log(textField.cursorIndex));
```

| Метод | Описание |
|-------|----------|
| `AddOnCursorIndexChange(Action)` / `RemoveOnCursorIndexChange(Action)` | Подписка на изменение позиции курсора |
| `AddOnSelectIndexChange(Action)` / `RemoveOnSelectIndexChange(Action)` | Подписка на изменение якоря выделения |
| `SetCursorIndex(int)` | Текущая позиция курсора |
| `SetSelectIndex(int)` | Текущий якорь выделения |
| `SetIsSelectable(bool)` | Можно ли выделять текст |
| `SetSelectAllOnFocus(bool)` | Выделять весь текст при фокусе |
| `SetSelectAllOnMouseUp(bool)` | Выделять весь текст при отпускании мыши |
| `SetDoubleClickSelectsWord(bool)` | Двойной клик выделяет слово |
| `SetTripleClickSelectsLine(bool)` | Тройной клик выделяет строку |

### BaseField\<TValueType\>

```csharp
field.SetLabel("My Field");
field.SetValue(42);
```

### BaseBoolField (Toggle)

```csharp
toggle
    .SetLabel("Включено")
    .SetText("Показать расширенные настройки")
    .SetToggleOnLabelClick(true);
```

| Метод | Описание |
|-------|----------|
| `SetText(string)` | Устанавливает текст рядом с чекбоксом |
| `SetLabel(string)` | Устанавливает label поля |
| `SetToggleOnLabelClick(bool)` | Переключать ли значение по клику на label |

### INotifyValueChanged\<T\>

```csharp
field.SetValue(42, notify: false); // устанавливает значение без генерации ChangeEvent
field.AddValueChanged(evt => Debug.Log(evt.newValue));
field.RemoveValueChanged(myCallback);
```

Типизированные перегрузки доступны для `int`, `uint`, `nint`, `nuint`, `long`, `ulong`, `short`, `ushort`, `byte`, `sbyte`, `float`, `double`, `decimal`, `char`, `string`, `bool`, `Color`, `Vector2/3/4`, `Vector2Int/3Int`, `Rect/RectInt`, `Bounds/BoundsInt`, `Hash128`, `GUID`, `Quaternion`, `Matrix4x4`, `Gradient`, `AnimationCurve`, `Delegate`, `Enum`, `Object`, `object`, плюс обобщённый fallback `SetValue<T, TValue>`.

> При установленном пакете `com.unity.mathematics` автоматически выставляется define `ASPID_FASTTOOLS_UNITY_MATHEMATICS_INTEGRATION` и добавляются перегрузки `SetValue` / `AddValueChanged` / `RemoveValueChanged` для `int2/3/4` (и `intMxN`), `float2/3/4` (и `floatMxN`), `bool2/3/4` (и `boolMxN`), а также `quaternion`.

### IMixedValueSupport

```csharp
field.SetShowMixedValue(true); // показывает индикатор смешанного значения
```

### Button

```csharp
button
    .AddClicked(() => Debug.Log("Clicked"))
    .SetClickable(new Clickable(() => { }))
    .SetIconImage(myBackground);
```

| Метод | Описание |
|-------|----------|
| `AddClicked(Action)` | Подписка на `Button.clicked` |
| `RemoveClicked(Action)` | Отписка от `Button.clicked` |
| `SetClickable(Clickable)` | Устанавливает `Button.clickable` |
| `SetIconImage(Background)` | Устанавливает `Button.iconImage` |

### Slider / BaseSlider\<TValue\>

```csharp
slider
    .SetLowValue(0f)
    .SetHighValue(100f)
    .SetShowInputField<SliderFloat, float>(true);
```

| Метод | Описание |
|-------|----------|
| `SetLowValue(TValue)` | Устанавливает минимальное значение слайдера |
| `SetHighValue(TValue)` | Устанавливает максимальное значение слайдера |
| `SetFill(bool)` | Заполнение трека до текущего значения |
| `SetInverted(bool)` | Инвертирует направление слайдера |
| `SetPageSize(float)` | Шаг изменения значения при постраничной навигации |
| `SetShowInputField(bool)` | Показывает числовое поле ввода рядом со слайдером |
| `SetDirection(SliderDirection)` | Устанавливает ориентацию слайдера |

### ProgressBar

```csharp
progressBar.SetTitle("Загрузка...").SetLowValue(0f).SetHighValue(100f);
```

| Метод | Описание |
|-------|----------|
| `SetTitle(string)` | Устанавливает заголовок, отображаемый в центре |
| `SetLowValue(float)` | Устанавливает минимальное значение |
| `SetHighValue(float)` | Устанавливает максимальное значение |

### HelpBox

```csharp
helpBox
    .SetText("Что-то пошло не так")
    .SetMessageType(HelpBoxMessageType.Warning);
```

| Метод | Описание |
|-------|----------|
| `SetText(string)` | Текст сообщения help-box |
| `SetMessageType(HelpBoxMessageType)` | Иконка / уровень (`None` / `Info` / `Warning` / `Error`) |

### Foldout

```csharp
foldout
    .SetText("Section Title")
    .SetToggleOnLabelClick(true)
    .SetValue(true);
```

| Метод | Описание |
|-------|----------|
| `SetText(string)` | Заголовок foldout |
| `SetToggleOnLabelClick(bool)` | Переключать ли раскрытие по клику на заголовок |

### Image

```csharp
image
    .SetImage(myTexture)
    .SetTintColor(Color.white)
    .SetScaleMode(ScaleMode.ScaleToFit);
```

| Метод | Описание |
|-------|----------|
| `SetImage(Texture)` | Устанавливает `Image.image` |
| `SetImageFromResource(string)` | Загружает текстуру через `Resources.Load<Texture2D>` |
| `SetSprite(Sprite)` | Устанавливает `Image.sprite` |
| `SetSpriteFromResource(string)` | Загружает sprite через `Resources.Load<Sprite>` |
| `SetVectorImage(VectorImage)` | Устанавливает `Image.vectorImage` |
| `SetVectorImageFromResource(string)` | Загружает vector image через `Resources.Load<VectorImage>` |
| `SetUv(Rect)` | Устанавливает UV-rect |
| `SetSourceRect(Rect)` | Устанавливает source rect |
| `SetTintColor(Color)` | Цветовой tint изображения |
| `SetScaleMode(ScaleMode)` | Режим масштабирования |

### IMGUIContainer

```csharp
container
    .SetOnGUIHandler(() => GUILayout.Label("IMGUI"))
    .SetCullingEnabled(true);
```

| Метод | Описание |
|-------|----------|
| `SetOnGUIHandler(Action)` | Заменяет коллбэк `onGUIHandler` |
| `AddOnGUIHandler(Action)` | Подписка на `onGUIHandler` |
| `RemoveOnGUIHandler(Action)` | Отписка от `onGUIHandler` |
| `SetCullingEnabled(bool)` | Пропускает `onGUIHandler`, когда элемент за пределами экрана |
| `SetContextType(ContextType)` | Устанавливает тип контекста IMGUI |

### Collection views (ListView, TreeView, MultiColumn variants)

Общие методы распределены по нескольким специализированным расширениям:

- `BaseVerticalCollectionViewExtensions` — применяется ко **всем** collection-views (ListView, TreeView, MultiColumn-варианты).
- `BaseListViewExtensions` — применяется к ListView и MultiColumnListView.
- `BaseTreeViewExtensions` — применяется к TreeView и MultiColumnTreeView.
- `ListViewExtensions` / `TreeViewExtensions` — фабрики `MakeItem`/`BindItem`/`UnbindItem`/`DestroyItem` для своего вью.
- `MultiColumnListViewExtensions` / `MultiColumnTreeViewExtensions` — хелперы для multi-column-вариантов.

```csharp
listView
    .SetItemsSource(items)
    .SetMakeItem(() => new Label())
    .SetBindItem((el, i) => ((Label)el).SetText(items[i]))
    .SetSelectionType(SelectionType.Single)
    .AddSelectionChanged(selected => Debug.Log(selected));
```

#### Source, layout and behavior — `BaseVerticalCollectionView`

| Метод | Описание | Примечания |
|-------|----------|------------|
| `SetItemsSource(IList)` | Источник данных | |
| `SetReorderable(bool)` | Включает drag-reorder | |
| `SetSelectedIndex(int)` | Выбирает элемент по индексу | |
| `SetSelectionType(SelectionType)` | None / Single / Multiple | |
| `SetFixedItemHeight(float)` | Фиксированная высота элемента (для виртуализации `FixedHeight`) | |
| `SetVirtualizationMethod(CollectionVirtualizationMethod)` | `FixedHeight` или `DynamicHeight` | |
| `SetHorizontalScrollingEnabled(bool)` | Включает горизонтальную прокрутку | |
| `SetShowAlternatingRowBackgrounds(AlternatingRowBackground)` | Режим зебра-полос | |
| `SetMakeFooter(Func<VisualElement>)` · `AddMakeFooter` · `RemoveMakeFooter` | Фабрика подвала | Unity 6+ |
| `SetMakeHeader(Func<VisualElement>)` · `AddMakeHeader` · `RemoveMakeHeader` | Фабрика заголовка | Unity 6+ |
| `SetMakeNoneElement(Func<VisualElement>)` · `AddMakeNoneElement` · `RemoveMakeNoneElement` | Фабрика empty-state | Unity 6+ |

#### Events — `BaseVerticalCollectionView`

| Метод | Описание |
|-------|----------|
| `AddItemsChosen(Action<IEnumerable<object>>)` / `RemoveItemsChosen` | Подтверждение элементов (двойной клик / Enter) |
| `AddSelectionChanged(Action<IEnumerable<object>>)` / `RemoveSelectionChanged` | Изменение выделения (объекты) |
| `AddSelectedIndicesChanged(Action<IEnumerable<int>>)` / `RemoveSelectedIndicesChanged` | Изменение выделения (индексы) |
| `AddItemIndexChanged(Action<int, int>)` / `RemoveItemIndexChanged` | Перемещение элемента (drag-reorder) |
| `AddItemsSourceChanged(Action)` / `RemoveItemsSourceChanged` | Смена ссылки `itemsSource` |
| `AddCanStartDrag(Func<CanStartDragArgs, bool>)` / `RemoveCanStartDrag` | Кастомный gating старта drag |
| `AddSetupDragAndDrop(Func<SetupDragAndDropArgs, StartDragArgs>)` / `RemoveSetupDragAndDrop` | Подготовка drag-and-drop |
| `AddSetupDragAndDrop(Func<HandleDragAndDropArgs, DragVisualMode>)` / `RemoveSetupDragAndDrop` | Визуальный режим drag-and-drop |
| `AddHandleDrop(Func<HandleDragAndDropArgs, DragVisualMode>)` / `RemoveHandleDrop` | Обработка drop |

#### `BaseListView`-specific

| Метод | Описание |
|-------|----------|
| `SetAllowAdd(bool)` · `SetAllowRemove(bool)` | Включают встроенные кнопки add/remove |
| `SetHeaderTitle(string)` | Заголовок при включённом foldout-header |
| `SetShowFoldoutHeader(bool)` | Оборачивает список в `Foldout` |
| `SetShowAddRemoveFooter(bool)` | Показывает footer с add/remove |
| `SetShowBoundCollectionSize(bool)` | Поле размера коллекции |
| `SetReorderMode(ListViewReorderMode)` | `Simple` или `Animated` |
| `SetBindingSourceSelectionMode(BindingSourceSelectionMode)` | Auto-assign / manual |
| `SetOnAdd(Action<BaseListView>)` · `AddOnAdd` · `RemoveOnAdd` | Кастомный коллбэк add-кнопки |
| `SetOnRemove(Action<BaseListView>)` · `AddOnRemove` · `RemoveOnRemove` | Кастомный коллбэк remove-кнопки |
| `SetOverridingAddButtonBehavior(Action<BaseListView, Button>)` · `AddOverridingAddButtonBehavior` · `RemoveOverridingAddButtonBehavior` | Подменяет дефолтное поведение add |
| `AddItemsAdded(Action<IEnumerable<int>>)` / `RemoveItemsAdded` | Добавление элементов по индексам |
| `AddItemsRemoved(Action<IEnumerable<int>>)` / `RemoveItemsRemoved` | Удаление элементов по индексам |

#### `BaseTreeView`-specific

| Метод | Описание |
|-------|----------|
| `SetAutoExpand(bool)` | Авто-разворачивание новых узлов |
| `AddItemExpandedChanged(Action<TreeViewExpansionChangedArgs>)` / `RemoveItemExpandedChanged` | Подписка на изменение раскрытия |

#### `ListView` / `TreeView` item factories

Эти методы дублируются в `ListViewExtensions` и `TreeViewExtensions` (каждое работает со своим типом view).

| Метод | Описание |
|-------|----------|
| `SetMakeItem(Func<VisualElement>)` · `AddMakeItem` · `RemoveMakeItem` | Фабрика элементов |
| `SetBindItem(Action<VisualElement, int>)` · `AddBindItem` · `RemoveBindItem` | Привязка элемента |
| `SetUnbindItem(Action<VisualElement, int>)` · `AddUnbindItem` · `RemoveUnbindItem` | Отвязка элемента |
| `SetDestroyItem(Action<VisualElement>)` · `AddDestroyItem` · `RemoveDestroyItem` | Уничтожение элемента |
| `SetItemTemplate(VisualTreeAsset)` | UXML-шаблон, по которому строятся элементы |

#### `MultiColumnListView` / `MultiColumnTreeView`

| Метод | Описание |
|-------|----------|
| `SetSortingMode(ColumnSortingMode)` | Встроенный режим сортировки заголовка колонки |

## Editor commands (editor-only)

```csharp
using Aspid.FastTools.UIElements.Editors;

image.AddOpenScriptCommand(target);
// Двойной клик на элемент открывает скрипт 'target' в IDE
```

| Метод | Цель | Описание |
|-------|------|----------|
| `AddOpenScriptCommand(Object)` | `VisualElement` | Регистрирует обработчик двойного клика, открывающий исходный скрипт `MonoBehaviour` / `ScriptableObject` в IDE. |
| `BindTo(SerializedObject)` | `VisualElement` | Вызывает `BindingExtensions.Bind` на элементе. |
| `BindTo(SerializedObject, string propertyPath)` | `IBindable` | Устанавливает `bindingPath` и привязывается к указанному `SerializedObject`. |
| `BindPropertyTo(SerializedProperty)` | `IBindable` | Вызывает `BindingExtensions.BindProperty` для переданного property. |
| `Initialize(Enum defaultValue, bool includeObsoleteValues = false)` | `EnumField` / `EnumFlagsField` | Инициализирует поле указанным значением enum по умолчанию. |
| `AddValueChanged(EventCallback<SerializedPropertyChangeEvent>)` / `RemoveValueChanged(...)` | `PropertyField` | Подписка / отписка от уведомлений об изменении свойства. |

## USS custom-style helpers (`ICustomStyle`)

```csharp
using Aspid.FastTools.UIElements;

private static readonly CustomStyleProperty<string> ThemeProperty = new("--aspid-fasttools-prop-theme");

void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
{
    if (evt.customStyle.TryGetByEnum(ThemeProperty, out ThemeStyle.Type theme))
        ApplyTheme(theme);
}
```

| Метод | Описание |
|-------|----------|
| `ICustomStyle.TryGetByEnum<T>(CustomStyleProperty<string>, out T)` | Резолвит USS custom-property со строковым значением и парсит её регистронезависимо как enum `T`. Используется во всех `*Style`-структурах с USS-driven enum (`ThemeStyle`, `StatusStyle`, `AspidLabelSizeStyle` и т. д.). |
