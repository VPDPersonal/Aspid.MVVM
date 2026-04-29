# VisualElement Extensions — full reference

Fluent extension methods for building UIToolkit trees in code. All methods return `T` (the element itself) for chaining.

```csharp
using Aspid.FastTools.UIElements;         // runtime extensions
using Aspid.FastTools.UIElements.Editors; // editor-only extensions (e.g. AddOpenScriptCommand)
```

## Core element operations

```csharp
element
    .SetName("MyElement")
    .SetVisible(true)
    .SetTooltip("Tooltip text")
    .AddChild(new Label("Hello"))
    .AddChildren(child1, child2, child3);
```

| Method | Description |
|--------|-------------|
| `SetName(string)` | Sets `element.name` |
| `SetVisible(bool)` | Sets `element.visible` |
| `SetTooltip(string)` | Sets `element.tooltip` |
| `SetUserData(object)` | Sets `element.userData` |
| `SetEnabledSelf(bool)` | Sets `element.enabledSelf` |
| `SetPickingMode(PickingMode)` | Sets `element.pickingMode` |
| `SetUsageHints(UsageHints)` | Sets `element.usageHints` |
| `SetViewDataKey(string)` | Sets `element.viewDataKey` |
| `SetLanguageDirection(LanguageDirection)` | Sets `element.languageDirection` |
| `SetDisablePlayModeTint(bool)` | Sets `element.disablePlayModeTint` |
| `SetDataSource(object)` | Sets `element.dataSource` |
| `SetDataSourceType(Type)` | Sets `element.dataSourceType` |
| `SetDataSourcePath(PropertyPath)` | Sets `element.dataSourcePath` |
| `AddChild(VisualElement)` | Appends a child, returns the parent |
| `AddChildren(params VisualElement[])` | Appends multiple children |
| `AddChildren(IEnumerable<VisualElement>)` | Appends from a sequence |
| `AddChildren(List<VisualElement>)` | Appends from a list |
| `AddChildren(Span<VisualElement>)` | Appends from a span |
| `AddChildren(ReadOnlySpan<VisualElement>)` | Appends from a read-only span |
| `InsertChild(int, VisualElement)` | Inserts a child at the specified index |
| `InsertChildren(int, params VisualElement[])` | Inserts multiple children starting at an index |
| `InsertChildren(int, IEnumerable<VisualElement>)` | Inserts from a sequence |
| `InsertChildren(int, List<VisualElement>)` | Inserts from a list |
| `InsertChildren(int, Span<VisualElement>)` | Inserts from a span |
| `InsertChildren(int, ReadOnlySpan<VisualElement>)` | Inserts from a read-only span |

> `RegisterCallbackOnce<TEventType>` and `RegisterCallbackOnce<TEventType, TUserArgsType>` are available on all Unity versions (polyfill included for versions prior to 2023.1).

## Focusable

| Method | Description |
|--------|-------------|
| `SetFocus()` | Attempts to give focus to the element |
| `SetBlur()` | Tells the element to release focus |
| `IsFocus()` | Returns whether the element currently has keyboard focus |
| `SetTabIndex(int)` | Sets `element.tabIndex` |
| `SetFocusable(bool)` | Sets `element.focusable` |
| `SetDelegatesFocus(bool)` | Sets `element.delegatesFocus` |

## USS & class operations

| Method | Description |
|--------|-------------|
| `AddClass(string)` | Adds a USS class |
| `RemoveClass(string)` | Removes a USS class |
| `ClearClasses()` | Removes all USS classes |
| `ToggleInClass(string)` | Toggles a USS class on/off |
| `EnableInClass(string, bool)` | Adds or removes a USS class based on a condition |
| `AddStyleSheets(StyleSheet)` | Adds a `StyleSheet` |
| `RemoveStyleSheets(StyleSheet)` | Removes a `StyleSheet` |
| `AddStyleSheetsFromResource(string)` | Adds a stylesheet loaded via `Resources.Load` |
| `RemoveStyleSheetsFromResource(string)` | Removes a stylesheet loaded via `Resources.Load` |

## Style extensions — by category

All style methods are also available on `IStyle` directly (same method names, operate on the style object).

### Layout

| Method | Style property |
|--------|---------------|
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

| Method | Description |
|--------|-------------|
| `SetSize(StyleLength)` | Sets both width and height |
| `SetSize(width?, height?)` | Sets width and/or height independently |
| `SetMinSize(StyleLength)` | Sets both minWidth and minHeight |
| `SetMinSize(width?, height?)` | |
| `SetMaxSize(StyleLength)` | Sets both maxWidth and maxHeight |
| `SetMaxSize(width?, height?)` | |
| `SetWidth(StyleLength)` | `width` |
| `SetMinWidth(StyleLength)` | `minWidth` |
| `SetMaxWidth(StyleLength)` | `maxWidth` |
| `SetHeight(StyleLength)` | `height` |
| `SetMinHeight(StyleLength)` | `minHeight` |
| `SetMaxHeight(StyleLength)` | `maxHeight` |

### Spacing

All spacing methods have a uniform-value overload, a per-side overload (`top`, `right`, `bottom`, `left`), single-side setters, and X/Y-axis pair setters.

| Method | Style properties |
|--------|------------------|
| `SetMargin(…)` / `SetPadding(…)` / `SetDistance(…)` | `Top/Right/Bottom/Left` (uniform or per-side) |
| `SetMarginX/Y` · `SetPaddingX/Y` · `SetDistanceX/Y` | Sets the horizontal (X = `Left`+`Right`) or vertical (Y = `Top`+`Bottom`) pair |
| `SetMarginTop/Right/Bottom/Left` | Single-side margin |
| `SetPaddingTop/Right/Bottom/Left` | Single-side padding |
| `SetDistanceTop/Right/Bottom/Left` *(via `SetTop` / `SetRight` / `SetBottom` / `SetLeft`)* | Single-side absolute offset (`top` / `right` / `bottom` / `left` style properties) |

> `SetDistance` is the wrapper for the four `top`/`right`/`bottom`/`left` style properties used by absolute positioning. `SetTop`, `SetRight`, `SetBottom`, `SetLeft` are direct single-property aliases.

### Font

| Method | Style property |
|--------|---------------|
| `SetUnityFont(StyleFont)` | `unityFont` |
| `SetFontSize(StyleLength)` | `fontSize` |
| `SetUnityFontDefinition(StyleFontDefinition)` | `unityFontDefinition` |
| `SetUnityFontStyleAndWeight(StyleEnum<FontStyle>)` | `unityFontStyleAndWeight` |

### Font style presets

Convenience methods for toggling bold / italic without overwriting the other flag:

| Method | Description |
|--------|-------------|
| `SetNormalUnityFontStyleAndWeight()` | Resets to `FontStyle.Normal` |
| `AddBoldUnityFontStyleAndWeight()` | Adds bold, preserving italic |
| `RemoveBoldUnityFontStyleAndWeight()` | Removes bold, preserving italic |
| `AddItalicUnityFontStyleAndWeight()` | Adds italic, preserving bold |
| `RemoveItalicUnityFontStyleAndWeight()` | Removes italic, preserving bold |

### Text

| Method | Style property | Notes |
|--------|---------------|-------|
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

| Method | Style property |
|--------|---------------|
| `SetColor(StyleColor)` | `color` |
| `SetColor(string)` | `color` parsed from an HTML string (`"#RRGGBB"` or a named color) |
| `SetOpacity(StyleFloat)` | `opacity` |

### Border

| Method | Description |
|--------|-------------|
| `SetBorderColor(StyleColor)` | All sides |
| `SetBorderColor(top?, right?, bottom?, left?)` | Per side |
| `SetBorderColorX(StyleColor)` · `SetBorderColorY(StyleColor)` | Horizontal (left + right) or vertical (top + bottom) pair |
| `SetBorderColorTop/Right/Bottom/Left(StyleColor)` | Single side |
| `SetBorderRadius(StyleLength)` | All corners |
| `SetBorderRadius(topLeft?, topRight?, bottomLeft?, bottomRight?)` | Per corner |
| `SetBorderRadiusTop(StyleLength)` · `SetBorderRadiusBottom(StyleLength)` | Top or bottom corner pair |
| `SetBorderRadiusTopLeft/TopRight/BottomLeft/BottomRight(StyleLength)` | Single corner |
| `SetBorderWidth(StyleFloat)` | All sides |
| `SetBorderWidth(top?, right?, bottom?, left?)` | Per side |
| `SetBorderWidthX(StyleFloat)` · `SetBorderWidthY(StyleFloat)` | Horizontal or vertical pair |
| `SetBorderWidthTop/Right/Bottom/Left(StyleFloat)` | Single side |

### Background

| Method | Style property |
|--------|---------------|
| `SetBackgroundColor(StyleColor)` | `backgroundColor` |
| `SetBackgroundColor(string)` | `backgroundColor` parsed from an HTML string (`"#RRGGBB"` or a named color) |
| `SetBackgroundImage(StyleBackground)` | `backgroundImage` |
| `SetBackgroundImageFromResource(string)` | Loads a `Texture2D` via `Resources.Load` and assigns it to `backgroundImage` |
| `SetBackgroundSize(StyleBackgroundSize)` | `backgroundSize` |
| `SetBackgroundRepeat(StyleBackgroundRepeat)` | `backgroundRepeat` |
| `SetBackgroundPosition(StyleBackgroundPosition)` | Both X and Y |
| `SetBackgroundPosition(x?, y?)` | Independently |
| `SetBackgroundPositionX(StyleBackgroundPosition)` | `backgroundPositionX` |
| `SetBackgroundPositionY(StyleBackgroundPosition)` | `backgroundPositionY` |
| `SetUnityBackgroundImageTintColor(StyleColor)` | `unityBackgroundImageTintColor` |

### Transform

| Method | Style property |
|--------|---------------|
| `SetScale(StyleScale)` | `scale` |
| `SetRotate(StyleRotate)` | `rotate` |
| `SetTranslate(StyleTranslate)` | `translate` |
| `SetTransformOrigin(StyleTransformOrigin)` | `transformOrigin` |

### Aspect, Filter & Material

Available on Unity 6000.3+.

| Method | Style property |
|--------|---------------|
| `SetAspectRation(StyleRatio)` | `aspectRatio` *(method name preserves the source typo)* |
| `SetFilter(StyleList<FilterFunction>)` | `filter` |
| `SetUnityMaterial(StyleMaterialDefinition)` | `unityMaterial` |

### Transition

| Method | Style property |
|--------|---------------|
| `SetTransitionDelay(StyleList<TimeValue>)` | `transitionDelay` |
| `SetTransitionDuration(StyleList<TimeValue>)` | `transitionDuration` |
| `SetTransitionProperty(StyleList<StylePropertyName>)` | `transitionProperty` |
| `SetTransitionTimingFunction(StyleList<EasingFunction>)` | `transitionTimingFunction` |

### Overflow & Visibility

| Method | Style property |
|--------|---------------|
| `SetOverflow(StyleEnum<Overflow>)` | `overflow` |
| `SetUnityOverflowClipBox(StyleEnum<OverflowClipBox>)` | `unityOverflowClipBox` |
| `SetVisibility(StyleEnum<Visibility>)` | `visibility` |
| `SetDisplay(DisplayStyle)` | `display` |

### Unity Slice

| Method | Description |
|--------|-------------|
| `SetUnitySlice(StyleInt)` | All sides |
| `SetUnitySlice(top?, right?, bottom?, left?)` | Per side |
| `SetUnitySliceX(StyleInt)` · `SetUnitySliceY(StyleInt)` | Horizontal (left + right) or vertical (top + bottom) pair |
| `SetUnitySliceTop/Right/Bottom/Left(StyleInt)` | Single side |
| `SetUnitySliceScale(StyleFloat)` | `unitySliceScale` |
| `SetUnitySliceType(StyleEnum<SliceType>)` | Unity 6+ |

### Cursor

| Method | Style property |
|--------|---------------|
| `SetCursor(StyleCursor)` | `cursor` |

## Specialized element extensions

### TextElement

```csharp
label
    .SetText("Hello World")
    .SetEnableRichText(true)
    .SetParseEscapeSequences(true);
```

| Method | Description |
|--------|-------------|
| `SetText(string)` | Sets the displayed text |
| `SetEnableRichText(bool)` | Enables rich-text tag parsing |
| `SetEmojiFallbackSupport(bool)` | Enables emoji fallback rendering |
| `SetParseEscapeSequences(bool)` | Whether escape sequences (e.g. `\n`) are parsed |
| `SetDisplayTooltipWhenElided(bool)` | Shows the elided text in a tooltip on hover |

### ITextEdition (TextField, IntegerField, …)

```csharp
textField
    .SetPlaceholder("Search…")
    .SetMaxLength(64)
    .SetIsDelayed(true);
```

| Method | Description |
|--------|-------------|
| `SetMaxLength(int)` | Maximum number of characters |
| `SetMaskChar(char)` | Character used to mask password input |
| `SetIsDelayed(bool)` | Defers value change until focus loss / Enter |
| `SetIsReadOnly(bool)` | Disables editing |
| `SetIsPassword(bool)` | Toggles password mode (uses mask char) |
| `SetPlaceholder(string)` | Placeholder text shown when empty |
| `SetAutoCorrection(bool)` | Enables auto-correction (mobile) |
| `SetHideMobileInput(bool)` | Hides the mobile soft input |
| `SetHideSoftKeyboard(bool)` | Hides the on-screen soft keyboard |
| `SetHidePlaceholderOnFocus(bool)` | Removes the placeholder on focus |
| `SetKeyboardType(TouchScreenKeyboardType)` | Sets the touch-screen keyboard type |

### ITextSelection

```csharp
textField
    .SetIsSelectable(true)
    .SetSelectAllOnFocus(true)
    .AddOnCursorIndexChange(() => Debug.Log(textField.cursorIndex));
```

| Method | Description |
|--------|-------------|
| `AddOnCursorIndexChange(Action)` / `RemoveOnCursorIndexChange(Action)` | Cursor-index change subscription |
| `AddOnSelectIndexChange(Action)` / `RemoveOnSelectIndexChange(Action)` | Selection-index change subscription |
| `SetCursorIndex(int)` | Sets the current cursor index |
| `SetSelectIndex(int)` | Sets the current selection anchor |
| `SetIsSelectable(bool)` | Whether text can be selected |
| `SetSelectAllOnFocus(bool)` | Selects all text on focus |
| `SetSelectAllOnMouseUp(bool)` | Selects all text on mouse release |
| `SetDoubleClickSelectsWord(bool)` | Double-click selects the word under cursor |
| `SetTripleClickSelectsLine(bool)` | Triple-click selects the line under cursor |

### BaseField\<TValueType\>

```csharp
field.SetLabel("My Field");
field.SetValue(42);
```

### BaseBoolField (Toggle)

```csharp
toggle
    .SetLabel("Enabled")
    .SetText("Show advanced settings")
    .SetToggleOnLabelClick(true);
```

| Method | Description |
|--------|-------------|
| `SetText(string)` | Sets the label next to the toggle box |
| `SetLabel(string)` | Sets the field-level label |
| `SetToggleOnLabelClick(bool)` | Whether clicking the label toggles the value |

### INotifyValueChanged\<T\>

```csharp
field.SetValue(42, notify: false); // sets value without raising ChangeEvent
field.AddValueChanged(evt => Debug.Log(evt.newValue));
field.RemoveValueChanged(myCallback);
```

Typed overloads are provided for `int`, `uint`, `nint`, `nuint`, `long`, `ulong`, `short`, `ushort`, `byte`, `sbyte`, `float`, `double`, `decimal`, `char`, `string`, `bool`, `Color`, `Vector2/3/4`, `Vector2Int/3Int`, `Rect/RectInt`, `Bounds/BoundsInt`, `Hash128`, `GUID`, `Quaternion`, `Matrix4x4`, `Gradient`, `AnimationCurve`, `Delegate`, `Enum`, `Object`, `object`, plus a generic `SetValue<T, TValue>` fallback.

> When the `com.unity.mathematics` package is installed, the `ASPID_FASTTOOLS_UNITY_MATHEMATICS_INTEGRATION` define is set automatically and adds `SetValue` / `AddValueChanged` / `RemoveValueChanged` overloads for `int2/3/4` (and `intMxN`), `float2/3/4` (and `floatMxN`), `bool2/3/4` (and `boolMxN`), and `quaternion`.

### IMixedValueSupport

```csharp
field.SetShowMixedValue(true); // shows the mixed-value indicator
```

### Button

```csharp
button
    .AddClicked(() => Debug.Log("Clicked"))
    .SetClickable(new Clickable(() => { }))
    .SetIconImage(myBackground);
```

| Method | Description |
|--------|-------------|
| `AddClicked(Action)` | Subscribes to `Button.clicked` |
| `RemoveClicked(Action)` | Unsubscribes from `Button.clicked` |
| `SetClickable(Clickable)` | Sets `Button.clickable` |
| `SetIconImage(Background)` | Sets `Button.iconImage` |

### Slider / BaseSlider\<TValue\>

```csharp
slider
    .SetLowValue(0f)
    .SetHighValue(100f)
    .SetShowInputField<SliderFloat, float>(true);
```

| Method | Description |
|--------|-------------|
| `SetLowValue(TValue)` | Sets the minimum slider value |
| `SetHighValue(TValue)` | Sets the maximum slider value |
| `SetFill(bool)` | Whether the track is filled up to the current value |
| `SetInverted(bool)` | Reverses the slider direction |
| `SetPageSize(float)` | Controls how much the value changes per page step |
| `SetShowInputField(bool)` | Shows a numeric input field alongside the slider |
| `SetDirection(SliderDirection)` | Sets the slider orientation |

### ProgressBar

```csharp
progressBar.SetTitle("Loading...").SetLowValue(0f).SetHighValue(100f);
```

| Method | Description |
|--------|-------------|
| `SetTitle(string)` | Sets the title displayed in the center |
| `SetLowValue(float)` | Sets the minimum value |
| `SetHighValue(float)` | Sets the maximum value |

### HelpBox

```csharp
helpBox
    .SetText("Something went wrong")
    .SetMessageType(HelpBoxMessageType.Warning);
```

| Method | Description |
|--------|-------------|
| `SetText(string)` | Sets the help-box message text |
| `SetMessageType(HelpBoxMessageType)` | Sets the icon / severity (`None` / `Info` / `Warning` / `Error`) |

### Foldout

```csharp
foldout
    .SetText("Section Title")
    .SetToggleOnLabelClick(true)
    .SetValue(true);
```

| Method | Description |
|--------|-------------|
| `SetText(string)` | Sets the foldout title |
| `SetToggleOnLabelClick(bool)` | Whether clicking the title toggles expansion |

### Image

```csharp
image
    .SetImage(myTexture)
    .SetTintColor(Color.white)
    .SetScaleMode(ScaleMode.ScaleToFit);
```

| Method | Description |
|--------|-------------|
| `SetImage(Texture)` | Sets `Image.image` |
| `SetImageFromResource(string)` | Loads a texture via `Resources.Load<Texture2D>` |
| `SetSprite(Sprite)` | Sets `Image.sprite` |
| `SetSpriteFromResource(string)` | Loads a sprite via `Resources.Load<Sprite>` |
| `SetVectorImage(VectorImage)` | Sets `Image.vectorImage` |
| `SetVectorImageFromResource(string)` | Loads a vector image via `Resources.Load<VectorImage>` |
| `SetUv(Rect)` | Sets the UV rect |
| `SetSourceRect(Rect)` | Sets the source rect |
| `SetTintColor(Color)` | Sets the image tint |
| `SetScaleMode(ScaleMode)` | Sets the scale mode |

### IMGUIContainer

```csharp
container
    .SetOnGUIHandler(() => GUILayout.Label("IMGUI"))
    .SeCullingEnabled(true);
```

| Method | Description |
|--------|-------------|
| `SetOnGUIHandler(Action)` | Replaces the `onGUIHandler` callback |
| `AddOnGUIHandler(Action)` | Subscribes to `onGUIHandler` |
| `RemoveOnGUIHandler(Action)` | Unsubscribes from `onGUIHandler` |
| `SeCullingEnabled(bool)` | Skips `onGUIHandler` when the element is offscreen *(method name preserves the source typo: `Se`, not `Set`)* |
| `SetContextType(ContextType)` | Sets the IMGUI context type |

### Collection views (ListView, TreeView, MultiColumn variants)

Common methods are spread across multiple targeted extensions:

- `BaseVerticalCollectionViewExtensions` — applies to **all** collection views (ListView, TreeView, MultiColumn variants).
- `BaseListViewExtensions` — applies to ListView and MultiColumnListView.
- `BaseTreeViewExtensions` — applies to TreeView and MultiColumnTreeView.
- `ListViewExtensions` / `TreeViewExtensions` — `MakeItem`/`BindItem`/`UnbindItem`/`DestroyItem` factories per view.
- `MultiColumnListViewExtensions` / `MultiColumnTreeViewExtensions` — multi-column-specific helpers.

```csharp
listView
    .SetItemsSource(items)
    .SetMakeItem(() => new Label())
    .SetBindItem((el, i) => ((Label)el).SetText(items[i]))
    .SetSelectionType(SelectionType.Single)
    .AddSelectionChanged(selected => Debug.Log(selected));
```

#### Source, layout and behavior — `BaseVerticalCollectionView`

| Method | Description | Notes |
|--------|-------------|-------|
| `SetItemsSource(IList)` | Underlying data source | |
| `SetReorderable(bool)` | Enables drag-to-reorder | |
| `SetSelectedIndex(int)` | Selects a specific index | |
| `SetSelectionType(SelectionType)` | None / Single / Multiple | |
| `SetFixedItemHeight(float)` | Fixed item height (for `FixedHeight` virtualization) | |
| `SetVirtualizationMethod(CollectionVirtualizationMethod)` | `FixedHeight` or `DynamicHeight` | |
| `SetHorizontalScrollingEnabled(bool)` | Enables horizontal scrolling | |
| `SetShowAlternatingRowBackgrounds(AlternatingRowBackground)` | Zebra striping mode | |
| `SetMakeFooter(Func<VisualElement>)` · `AddMakeFooter` · `RemoveMakeFooter` | Footer factory | Unity 6+ |
| `SetMakeHeader(Func<VisualElement>)` · `AddMakeHeader` · `RemoveMakeHeader` | Header factory | Unity 6+ |
| `SetMakeNoneElement(Func<VisualElement>)` · `AddMakeNoneElement` · `RemoveMakeNoneElement` | Empty-state factory | Unity 6+ |

#### Events — `BaseVerticalCollectionView`

| Method | Description |
|--------|-------------|
| `AddItemsChosen(Action<IEnumerable<object>>)` / `RemoveItemsChosen` | Items confirmed (e.g. double-click / Enter) |
| `AddSelectionChanged(Action<IEnumerable<object>>)` / `RemoveSelectionChanged` | Selection changed (objects) |
| `AddSelectedIndicesChanged(Action<IEnumerable<int>>)` / `RemoveSelectedIndicesChanged` | Selection changed (indices) |
| `AddItemIndexChanged(Action<int, int>)` / `RemoveItemIndexChanged` | Item moved (drag-reorder) |
| `AddItemsSourceChanged(Action)` / `RemoveItemsSourceChanged` | `itemsSource` reference changed |
| `AddCanStartDrag(Func<CanStartDragArgs, bool>)` / `RemoveCanStartDrag` | Custom drag-start gating |
| `AddSetupDragAndDrop(Func<SetupDragAndDropArgs, StartDragArgs>)` / `RemoveSetupDragAndDrop` | Drag-and-drop preparation |
| `AddSetupDragAndDrop(Func<HandleDragAndDropArgs, DragVisualMode>)` / `RemoveSetupDragAndDrop` | Drag-and-drop visual mode |
| `AddHandleDrop(Func<HandleDragAndDropArgs, DragVisualMode>)` / `RemoveHandleDrop` | Drop handling |

#### `BaseListView`-specific

| Method | Description |
|--------|-------------|
| `SetAllowAdd(bool)` · `SetAllowRemove(bool)` | Toggles built-in add/remove buttons |
| `SetHeaderTitle(string)` | Title shown when foldout header is on |
| `SetShowFoldoutHeader(bool)` | Wraps the list in a `Foldout` |
| `SetShowAddRemoveFooter(bool)` | Toggles the add/remove footer |
| `SetShowBoundCollectionSize(bool)` | Shows the collection-size field |
| `SetReorderMode(ListViewReorderMode)` | `Simple` or `Animated` |
| `SetBindingSourceSelectionMode(BindingSourceSelectionMode)` | Auto-assign / manual |
| `SetOnAdd(Action<BaseListView>)` · `AddOnAdd` · `RemoveOnAdd` | Custom add-button callback |
| `SetOnRemove(Action<BaseListView>)` · `AddOnRemove` · `RemoveOnRemove` | Custom remove-button callback |
| `SetOverridingAddButtonBehavior(Action<BaseListView, Button>)` · `AddOverridingAddButtonBehavior` · `RemoveOverridingAddButtonBehavior` | Replace default add-button click |
| `AddItemsAdded(Action<IEnumerable<int>>)` / `RemoveItemsAdded` | Items added by index |
| `AddItemsRemoved(Action<IEnumerable<int>>)` / `RemoveItemsRemoved` | Items removed by index |

#### `BaseTreeView`-specific

| Method | Description |
|--------|-------------|
| `SetAutoExpand(bool)` | Auto-expand new nodes |
| `AddItemExpandedChanged(Action<TreeViewExpansionChangedArgs>)` / `RemoveItemExpandedChanged` | Subscription to expansion changes |

#### `ListView` / `TreeView` item factories

These methods are duplicated across `ListViewExtensions` and `TreeViewExtensions` (each operating on its own view type).

| Method | Description |
|--------|-------------|
| `SetMakeItem(Func<VisualElement>)` · `AddMakeItem` · `RemoveMakeItem` | Item factory |
| `SetBindItem(Action<VisualElement, int>)` · `AddBindItem` · `RemoveBindItem` | Item binding |
| `SetUnbindItem(Action<VisualElement, int>)` · `AddUnbindItem` · `RemoveUnbindItem` | Item unbinding |
| `SetDestroyItem(Action<VisualElement>)` · `AddDestroyItem` · `RemoveDestroyItem` | Item teardown |
| `SetItemTemplate(VisualTreeAsset)` | UXML template used to build items |

#### `MultiColumnListView` / `MultiColumnTreeView`

| Method | Description |
|--------|-------------|
| `SetSortingMode(ColumnSortingMode)` | Built-in sorting mode for the column header |

## Editor commands (editor-only)

```csharp
using Aspid.FastTools.UIElements.Editors;

image.AddOpenScriptCommand(target);
// Double-clicking the element opens the script for 'target' in the IDE
```

| Method | Target | Description |
|--------|--------|-------------|
| `AddOpenScriptCommand(Object)` | `VisualElement` | Registers a double-click handler that opens the source script for the given `MonoBehaviour` / `ScriptableObject` in the IDE. |
| `BindTo(SerializedObject)` | `VisualElement` | Calls `BindingExtensions.Bind` on the element. |
| `BindTo(SerializedObject, string propertyPath)` | `IBindable` | Sets `bindingPath` and binds to the given `SerializedObject`. |
| `BindPropertyTo(SerializedProperty)` | `IBindable` | Calls `BindingExtensions.BindProperty` with the supplied property. |
| `Initialize(Enum defaultValue, bool includeObsoleteValues = false)` | `EnumField` / `EnumFlagsField` | Initializes the field to the supplied default enum value. |
| `AddValueChanged(EventCallback<SerializedPropertyChangeEvent>)` / `RemoveValueChanged(...)` | `PropertyField` | Subscribes / unsubscribes to property change notifications. |

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

| Method | Description |
|--------|-------------|
| `ICustomStyle.TryGetByEnum<T>(CustomStyleProperty<string>, out T)` | Resolves a string-typed USS custom property and parses it case-insensitively as the enum `T`. Used by every `*Style` struct that exposes a USS-driven enum (`ThemeStyle`, `StatusStyle`, `AspidLabelSizeStyle`, etc.). |
