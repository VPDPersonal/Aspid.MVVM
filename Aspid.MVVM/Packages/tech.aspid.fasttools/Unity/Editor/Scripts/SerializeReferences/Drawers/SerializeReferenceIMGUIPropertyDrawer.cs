using System;
using UnityEditor;
using UnityEngine;
using Aspid.FastTools.Editors;
using Aspid.FastTools.Types.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// IMGUI rendering for the <c>[TypeSelector]</c> drawer on a <c>[SerializeReference]</c> field: a
    /// foldout-and-dropdown header row, an optional missing-type warning, and the nested properties of the
    /// assigned instance. The optional base types narrow the candidate list below the declared field type.
    /// </summary>
    internal static class SerializeReferenceIMGUIPropertyDrawer
    {
        public static float GetHeight(SerializedProperty property)
        {
            var spacing = EditorGUIUtility.standardVerticalSpacing;
            var height = EditorGUIUtility.singleLineHeight;

            // Mixed types across the selection: the per-instance child fields cannot be merged, so only the dropdown
            // and a single-line "different types" hint are drawn — never the children or the per-asset notices.
            if (SerializeReferenceHelpers.HasMixedTypes(property))
                return height + spacing + EditorGUIUtility.singleLineHeight;

            // Per-asset notices are suppressed under a multi-object selection (each reads/writes a single backing asset).
            if (SerializeReferenceHelpers.NoticesApply(property))
            {
                if (SerializeReferenceHelpers.IsMissingType(property))
                    height += spacing + EditorGUIUtility.singleLineHeight;

                if (SerializeReferenceHelpers.HasSharedReference(property))
                    height += spacing + EditorGUIUtility.singleLineHeight;

                if (SerializeReferenceRequiredGate.IsViolation(property))
                    height += spacing + EditorGUIUtility.singleLineHeight;
            }

            if (property.managedReferenceValue is not null && property.isExpanded)
                height += GetChildrenHeight(property, spacing);

            return height;
        }

        public static void Draw(Rect position, GUIContent label, SerializedProperty property, params Type[] baseTypes)
        {
            // Auto-de-alias a freshly duplicated list element: on a rid collision within the array, the guard queues a
            // swap to an independent clone on the next editor tick — never mutating the SerializedObject mid-draw.
            // Cheap on the unchanged path, so safe to call from every IMGUI repaint.
            SerializeReferenceDuplicateGuard.Observe(property);

            var spacing = EditorGUIUtility.standardVerticalSpacing;
            var mixedTypes = SerializeReferenceHelpers.HasMixedTypes(property);
            var currentType = SerializeReferenceHelpers.GetCurrentType(property);
            var hasValue = currentType is not null && !mixedTypes;
            var fieldType = SerializeReferenceHelpers.GetFieldType(property);

            // Per-asset notices read/write a single backing asset, so they are suppressed under a multi-object
            // selection. Computed up front: showing any of them decides whether the field reserves the stripe gutter.
            var noticesApply = !mixedTypes && SerializeReferenceHelpers.NoticesApply(property);
            var showMissing = noticesApply && SerializeReferenceHelpers.IsMissingType(property);
            var showShared = noticesApply && SerializeReferenceHelpers.HasSharedReference(property);
            var showRequired = noticesApply && SerializeReferenceRequiredGate.IsViolation(property);

            // The shared group's 1-based badge number (0 when not shared) drives BOTH the stripe colour and the notice,
            // so a badge's colour tracks its number instead of a rid hash that could alias two groups onto one hue.
            var sharedIndex = showShared ? SerializeReferenceHelpers.GetSharedReferenceIndex(property) : 0;

            // Reserve a left gutter (StripeGutter) for the status stripe ONLY on a field that shows one, mirroring the
            // UIToolkit field's padding-left. Missing / required fields keep the gutter but pull their arrow-less
            // label + notice left by FoldoutArrowIndent, onto the foldout-arrow spot.
            var flat = showMissing || showRequired;
            var gutter = showMissing || showShared || showRequired ? StripeGutter : 0f;
            var body = new Rect(position.x + gutter, position.y, position.width - gutter, position.height);

            var line = new Rect(body.x, body.y, body.width, EditorGUIUtility.singleLineHeight);

            var contextEvent = Event.current;
            if (contextEvent.type == EventType.ContextClick && line.Contains(contextEvent.mousePosition))
            {
                ShowContextMenu(property, fieldType, baseTypes);
                contextEvent.Use();
            }

            // Dropping a MonoScript on the header row assigns an instance of its class (when assignable).
            if ((contextEvent.type == EventType.DragUpdated || contextEvent.type == EventType.DragPerform) &&
                line.Contains(contextEvent.mousePosition))
            {
                if (SerializeReferenceDropHandler.TryResolveDroppedType(fieldType, baseTypes, out var droppedType))
                {
                    DragAndDrop.visualMode = DragAndDropVisualMode.Link;
                    if (contextEvent.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();
                        SerializeReferenceDropHandler.Assign(property, droppedType);
                        contextEvent.Use();
                        return; // re-layout on the next repaint with the new value
                    }
                }
                else
                {
                    DragAndDrop.visualMode = DragAndDropVisualMode.Rejected;
                }
            }

            var labelRect = new Rect(line.x, line.y, EditorGUIUtility.labelWidth, line.height);
            if (hasValue)
            {
                property.isExpanded = EditorGUI.Foldout(labelRect, property.isExpanded, label, toggleOnLabelClick: true);
            }
            else
            {
                // A flat (missing / required) field has no foldout arrow — pull its label left onto the arrow's spot.
                var labelPull = flat ? FoldoutArrowIndent : 0f;
                EditorGUI.LabelField(new Rect(labelRect.x - labelPull, labelRect.y,
                    labelRect.width + labelPull, labelRect.height), label);
            }

            var dropdownRect = new Rect(
                line.x + EditorGUIUtility.labelWidth + 2f,
                line.y,
                line.width - EditorGUIUtility.labelWidth - 2f,
                line.height);

            var openRect = Rect.zero;
            if (hasValue)
            {
                var openSize = line.height;
                openRect = new Rect(dropdownRect.xMax - openSize, dropdownRect.y, openSize, openSize);
                dropdownRect.width -= openSize + 1f;
            }

            // Mixed types: the "—" caption renders the dash itself (DropdownButton has no mixed-value styling), but
            // EditorGUI.showMixedValue is still set/restored so the flag propagates to any nested IMGUI control.
            string missingTooltip = null;
            var caption = mixedTypes ? "—" : GetCaption(property, currentType, out missingTooltip);
            var previousMixed = EditorGUI.showMixedValue;
            EditorGUI.showMixedValue = mixedTypes;

            // Missing stored type: mirror the UIToolkit dropdown's --missing treatment — amber caption, trimmed from
            // the LEFT (IMGUI clips at the right edge, which would cut the informative class-name tail).
            var captionStyle = EditorStyles.miniPullDown;
            if (missingTooltip is not null)
            {
                captionStyle = GetMissingCaptionStyle();
                caption = FitCaptionFromLeft(captionStyle, caption, dropdownRect.width);
            }

            // A resolved type hovers its full Namespace.Class, Assembly identity (the caption shows only the short
            // name); a missing one, the stored identity it can no longer load.
            var captionTooltip = mixedTypes
                ? "Mixed — the selected objects hold different types."
                : missingTooltip ?? TypeSelectorHelpers.GetTypeSelectorTooltip(currentType);

            if (EditorGUI.DropdownButton(dropdownRect, new GUIContent(caption, captionTooltip),
                    FocusType.Passive, captionStyle))
            {
                // Under mixed types there is no single "current" type to pre-highlight — open the picker unselected.
                ShowSelector(property, fieldType, baseTypes, mixedTypes ? null : currentType, dropdownRect);
            }

            EditorGUI.showMixedValue = previousMixed;

            if (hasValue)
                TypeIMGUIPropertyDrawer.DrawOpenScriptButton(openRect, currentType);

            var y = line.yMax + spacing;

            // Mixed types: stand in for the per-instance child fields (which cannot be merged) with a single dim info
            // line, and skip the per-asset notices entirely.
            if (mixedTypes)
            {
                var hintRect = new Rect(body.x, y, body.width, EditorGUIUtility.singleLineHeight);
                DrawInfoNotice(
                    hintRect,
                    "Different types selected",
                    "The selected objects hold different managed-reference types, so their fields cannot be shown " +
                    "together.\nPick a type from the dropdown to set it on all of them, or select a single object " +
                    "to edit its own fields.");
                return;
            }

            // Anchor the notices and the stripe to the indented rect so their offset from the foldout arrow is the
            // same at every nesting depth; GUI.Label/DrawRect ignore indentLevel, so it is applied explicitly.
            var content = EditorGUI.IndentedRect(body);

            // Left status stripe spanning the whole field, mirroring the UIToolkit field's full-height __stripe: the
            // badge's per-index colour for a shared reference, else the warning amber. Offset into the left gutter
            // (StripeOffset) and inset vertically (StripeInsetY) so adjacent stripes stay apart.
            {
                Color? stripeColor = null;
                if (showShared && sharedIndex > 0)
                    stripeColor = SerializeReferenceRidColor.ForIndex(sharedIndex);
                else if (showMissing || showRequired)
                    stripeColor = NoticeColor;

                if (stripeColor.HasValue)
                    EditorGUI.DrawRect(
                        new Rect(content.x - StripeOffset, position.y + StripeInsetY,
                            StripeWidth, position.height - 2f * StripeInsetY),
                        stripeColor.Value);
            }

            if (showMissing)
            {
                // Flat field (no arrow): pull the notice left onto the arrow's spot so it lines up with the label above.
                var noticeRect = new Rect(content.x - FoldoutArrowIndent, y,
                    content.width + FoldoutArrowIndent, EditorGUIUtility.singleLineHeight);
                var typeName = SerializeReferenceHelpers.GetMissingTypeDisplayName(property);
                var canFix = SerializeReferenceHelpers.TryGetRepairLocation(property, out _, out _, out _);

                // Smart Fix suggestion ("· → Pistol"). The ranking is cached per (asset, rid), so this stays cheap
                // across IMGUI's per-frame repaints; the candidate is pre-declared so it stays definitely assigned
                // when the short-circuit skips the probe.
                SerializeReferenceRepairSuggestions.RepairCandidate suggestion = default;
                var hasSuggestion = canFix &&
                    SerializeReferenceHelpers.TryGetRepairSuggestion(property, baseTypes, out suggestion);

                DrawNotice(
                    noticeRect,
                    "Missing type",
                    canFix ? "Fix" : null,
                    canFix
                        ? $"Missing type: {typeName}.\nClick Fix to re-point this reference to an existing type, keeping its data."
                        : $"Missing type: {typeName}.\nOpen this asset from the Project window to repair it.",
                    canFix
                        ? () =>
                        {
                            // Anchor from the notice's top (yMin): ShowAsDropDown opens below the anchor rect, so a
                            // top-anchored one-line rect ends flush at the notice's bottom (yMax drops it a line lower).
                            var screenPosition = GUIUtility.GUIToScreenPoint(new Vector2(noticeRect.x, noticeRect.y));
                            var screenRect = new Rect(screenPosition.x, screenPosition.y, noticeRect.width, EditorGUIUtility.singleLineHeight);
                            SerializeReferenceHelpers.ShowFixTypeSelector(property.Persistent(), screenRect, null, baseTypes);
                        }
                        : null,
                    hasSuggestion ? SerializeReferenceHelpers.GetSuggestionLabel(suggestion) : null,
                    hasSuggestion ? SerializeReferenceHelpers.GetSuggestionDetail(suggestion) : null,
                    hasSuggestion
                        ? () => SerializeReferenceHelpers.TryFixMissingType(property.Persistent(), suggestion.Type)
                        : null);

                y += EditorGUIUtility.singleLineHeight + spacing;
            }

            // A required-but-empty reference shows a non-actionable notice; the header dropdown above is the fix.
            if (showRequired)
            {
                // Flat field (no arrow): pull the notice left onto the arrow's spot so it lines up with the label above.
                var noticeRect = new Rect(content.x - FoldoutArrowIndent, y,
                    content.width + FoldoutArrowIndent, EditorGUIUtility.singleLineHeight);
                var message = "Required reference is not set";

                DrawRequiredNotice(noticeRect, message,
                    "This [SerializeReference] field is marked required but has no value.");
                y += EditorGUIUtility.singleLineHeight + spacing;
            }

            if (hasValue && property.isExpanded)
            {
                EditorGUI.indentLevel++;
                DrawChildren(property, body.x, body.width, spacing, ref y);
                EditorGUI.indentLevel--;
            }

            // Shared-reference notice sits under the nested properties, mirroring the UIToolkit field. It is the only
            // notice that coexists with children (missing / required render no value), so only it moves down here.
            if (showShared)
            {
                // The badge's per-index colour tints the whole notice and the stripe, so aliased fields read as one
                // colour (mirrors the UIToolkit --shared notice: no warning icon — attention, not an error).
                Color? indexColor = sharedIndex > 0 ? SerializeReferenceRidColor.ForIndex(sharedIndex) : null;

                // When this member is the one a sibling's message click just revealed, scroll the inspector to it.
                SerializeReferenceSharedNavigation.RevealIfPending(property, position);

                // Pull the notice left by the foldout arrow's reserved width so its leading swatch lines up under the
                // header's arrow (the value is always a foldout here); widen to match so "Make unique" stays right-pinned.
                var noticeRect = new Rect(content.x - FoldoutArrowIndent, y,
                    content.width + FoldoutArrowIndent, EditorGUIUtility.singleLineHeight);
                var persistent = property.Persistent();

                // Navigation gets the LIVE property, not the persistent copy: the click callback runs synchronously
                // inside this Draw, and its ancestor isExpanded writes must go through the inspector's own
                // SerializedObject — expansion state is cached per instance, so a fresh copy's write never reaches it.
                DrawNotice(
                    noticeRect,
                    sharedIndex > 0 ? $"Shared reference #{sharedIndex}" : "Shared reference",
                    "Make unique",
                    SerializeReferenceHelpers.BuildSharedReferenceDetail(property),
                    () => SerializeReferenceHelpers.MakeReferenceUnique(persistent),
                    ridColor: indexColor,
                    onMessageClick: () => SerializeReferenceSharedNavigation.NavigateFrom(property));

                y += EditorGUIUtility.singleLineHeight + spacing;

                // Group-navigation pulse (mirrors the UIToolkit __flash overlay): painted from the status stripe's
                // line so pulse and stripe read as one band. Right edge: the inspector's edge for a root-level field,
                // the list's box border for a row inside a SerializeReferenceIMGUIList. A field whose path crosses
                // an array element without a pushed limit is a row of Unity's OWN ReorderableList — its rect is
                // inset by Defaults.padding from the box's inner edge, so adding it back lands the band on the box
                // frame instead of spilling past it to the inspector's edge.
                if (SerializeReferenceSharedNavigation.TryGetFlashAlpha(property, out var flashAlpha) &&
                    indexColor.HasValue)
                {
                    var flashColor = indexColor.Value;
                    flashColor.a = flashAlpha;
                    var flashX = content.x - StripeOffset;
                    var rowLimit = SerializeReferenceIMGUIList.CurrentElementRightLimit;
                    if (float.IsNaN(rowLimit) && property.propertyPath.Contains(".Array.data["))
                        rowLimit = position.xMax + UnityEditorInternal.ReorderableList.Defaults.padding;
                    var flashXMax = float.IsNaN(rowLimit)
                        ? Mathf.Max(position.xMax, EditorGUIUtility.currentViewWidth)
                        : rowLimit;
                    EditorGUI.DrawRect(
                        new Rect(flashX, position.y, flashXMax - flashX, position.height), flashColor);
                }
            }
        }

        private static void DrawChildren(SerializedProperty property, float x, float width, float spacing, ref float y)
        {
            var iterator = property.Copy();
            var end = property.GetEndProperty();
            var enterChildren = true;

            while (iterator.NextVisible(enterChildren) && !SerializedProperty.EqualContents(iterator, end))
            {
                enterChildren = false;

                var height = EditorGUI.GetPropertyHeight(iterator, includeChildren: true);
                EditorGUI.PropertyField(new Rect(x, y, width, height), iterator, includeChildren: true);
                y += height + spacing;
            }
        }

        private static float GetChildrenHeight(SerializedProperty property, float spacing)
        {
            var height = 0f;
            var iterator = property.Copy();
            var end = property.GetEndProperty();
            var enterChildren = true;

            while (iterator.NextVisible(enterChildren) && !SerializedProperty.EqualContents(iterator, end))
            {
                enterChildren = false;
                height += EditorGUI.GetPropertyHeight(iterator, includeChildren: true) + spacing;
            }

            return height;
        }

        private static void ShowSelector(SerializedProperty property, Type fieldType, Type[] baseTypes, Type currentType, Rect dropdownRect)
        {
            var persistent = property.Persistent();
            var screenPosition = GUIUtility.GUIToScreenPoint(new Vector2(dropdownRect.x, dropdownRect.y));
            var screenRect = new Rect(screenPosition.x, screenPosition.y, dropdownRect.width, dropdownRect.height);

            TypeSelectorWindow.Show(
                screenRect: screenRect,
                filter: new TypeSelectorFilter
                {
                    Types = new[] { fieldType },
                    Predicate = SerializeReferenceHelpers.BuildAssignableFilter(baseTypes),
                    AdditionalTypes = GenericTypeResolver.GetAssignableGenericDefinitions(fieldType, baseTypes),
                    ArgumentFilter = SerializeReferenceHelpers.IsValidGenericArgument,
                },
                currentAqn: currentType?.AssemblyQualifiedName ?? string.Empty,
                onSelected: assemblyQualifiedName => Apply(string.IsNullOrEmpty(assemblyQualifiedName)
                    ? null
                    : Type.GetType(assemblyQualifiedName, throwOnError: false)));

            return;

            void Apply(Type type)
            {
                // Multi-object: each target gets its OWN instance, created from that target's previous value, so the
                // managed reference is never aliased across objects; <None> clears all. One Undo step covers them all.
                if (SerializeReferenceHelpers.IsEditingMultipleObjects(persistent))
                {
                    SerializeReferenceHelpers.ApplyManagedReferencePerTarget(
                        persistent,
                        previous => SerializeReferenceHelpers.CreateInstancePreservingData(type, previous));

                    // All targets now share the new type, so the live foldout drives expansion; set it on the
                    // persistent property (the per-target writes went through disposed SerializedObjects).
                    persistent.isExpanded = type is not null;
                    return;
                }

                var single = persistent.managedReferenceValue;
                persistent.SetManagedReferenceAndApply(SerializeReferenceHelpers.CreateInstancePreservingData(type, single));
                persistent.isExpanded = type is not null;
            }
        }

        private static void ShowContextMenu(SerializedProperty property, Type fieldType, Type[] baseTypes)
        {
            var persistent = property.Persistent();
            var filter = SerializeReferenceHelpers.BuildAssignableFilter(baseTypes);
            var menu = new GenericMenu();

            // Copy reads the first target's value (Unity's convention for a multi-selection menu). Paste then applies an
            // independent instance PER target, so the pasted reference is never aliased across objects.
            menu.AddItem(new GUIContent("Copy Serialize Reference"), false,
                () => SerializeReferenceClipboard.Copy(persistent.managedReferenceValue));

            var pasteLabel = new GUIContent("Paste Serialize Reference");
            if (SerializeReferenceClipboard.CanPasteInto(fieldType, filter))
                menu.AddItem(pasteLabel, false, () => Paste(persistent));
            else
                menu.AddDisabledItem(pasteLabel);

            // Make-unique is a single-asset cross-reference operation; only offered (and only correct) for a single
            // target — under a multi-object selection the shared-reference notice is already suppressed.
            if (SerializeReferenceHelpers.NoticesApply(property) &&
                SerializeReferenceHelpers.HasSharedReference(property))
                menu.AddItem(new GUIContent("Make Unique Reference"), false,
                    () => SerializeReferenceHelpers.MakeReferenceUnique(persistent));

            // Find every asset/field using the current type, via the sr: Quick Search provider.
            var usagesType = SerializeReferenceHelpers.GetCurrentType(property);
            if (usagesType != null)
            {
                menu.AddItem(new GUIContent($"Find Usages of {usagesType.Name}"), false,
                    () => SerializeReferenceUsageSearchProvider.OpenSearch(usagesType));
            }

            // Link this field to an existing instance of the same object (inverse of Make Unique), single-target only.
            if (SerializeReferenceHelpers.NoticesApply(property))
            {
                foreach (var candidate in SerializeReferenceLinker.CollectLinkCandidates(property))
                {
                    var path = candidate.Path;
                    menu.AddItem(new GUIContent($"Link to Existing/{candidate.Type.Name}  ({path})"), false,
                        () => SerializeReferenceLinker.LinkTo(persistent, path));
                }
            }

            // Generate a new subclass of the field's type and assign it once it compiles.
            if (fieldType != null)
            {
                menu.AddItem(new GUIContent("Create New Script…"), false, () =>
                {
                    if (!SerializeReferenceScriptCreator.TryCreateSubclassStub(fieldType, out _, out var fullTypeName)) return;

                    // Multi-object: enqueue one pending assignment PER target — targetObject alone would leave objects
                    // 2..N untouched. Read from the persistent property: the transient `property` may be disposed by
                    // the time this deferred context-menu callback runs.
                    foreach (var target in persistent.serializedObject.targetObjects)
                        SerializeReferencePendingAssignment.Enqueue(target, persistent.propertyPath, fullTypeName);
                });
            }

            if (usagesType != null)
            {
                var value = persistent.managedReferenceValue;
                menu.AddItem(new GUIContent("Save as Template…"), false,
                    () => SerializeReferenceNamePrompt.Show("Save Template",
                        SerializeReferenceTemplates.SuggestName(usagesType),
                        name => SerializeReferenceTemplates.SaveConfirmed(name, value)));
            }

            foreach (var template in SerializeReferenceTemplates.LoadResolved())
            {
                if (fieldType != null && !fieldType.IsAssignableFrom(template.Type)) continue;
                if (!filter(template.Type)) continue;
                var name = template.Name;
                menu.AddItem(new GUIContent($"Paste Template/{name}"), false, () => ApplyTemplate(persistent, name));
            }

            menu.ShowAsContext();
            return;

            void Paste(SerializedProperty target)
            {
                if (SerializeReferenceHelpers.IsEditingMultipleObjects(target))
                {
                    SerializeReferenceHelpers.ApplyManagedReferencePerTarget(
                        target,
                        _ => SerializeReferenceClipboard.CreateInstance());

                    // All targets now share the pasted type, so the live foldout drives expansion; set it on the
                    // persistent property (the per-target writes went through disposed SerializedObjects). A null
                    // clipboard type is an empty-reference paste, which collapses — matching the single-object branch.
                    target.isExpanded = SerializeReferenceClipboard.Type is not null;
                    return;
                }

                var value = SerializeReferenceClipboard.CreateInstance();
                target.SetManagedReferenceAndApply(value);
                target.isExpanded = value is not null;
            }
        }

        // Applies a saved template to the property (an independent instance per target on a multi-object selection).
        private static void ApplyTemplate(SerializedProperty property, string name)
        {
            var persistent = property.Persistent();

            if (SerializeReferenceHelpers.IsEditingMultipleObjects(persistent))
            {
                SerializeReferenceHelpers.ApplyManagedReferencePerTarget(persistent, _ => SerializeReferenceTemplates.CreateInstance(name));
                persistent.isExpanded = true;
                return;
            }

            var instance = SerializeReferenceTemplates.CreateInstance(name);
            if (instance is null) return;

            persistent.SetManagedReferenceAndApply(instance);
            persistent.isExpanded = true;
        }


        // A missing stored type reports its full identity through missingTooltip (null otherwise), which feeds the
        // dropdown's hover tooltip and flags the caption for the amber missing treatment.
        private static string GetCaption(SerializedProperty property, Type currentType, out string missingTooltip)
        {
            missingTooltip = null;

            if (currentType is not null)
                return TypeSelectorHelpers.GetTypeSelectorTitle(currentType);

            var missingType = SerializeReferenceHelpers.IsMissingType(property)
                ? SerializeReferenceHelpers.GetMissingTypeName(property)
                : default;

            if (!missingType.IsEmpty)
                missingTooltip = $"Missing type: {missingType.FullName}";

            return TypeSelectorHelpers.GetTypeSelectorTitle(null, missingType.DisplayName);
        }

        // Amber caption for a missing stored type, mirroring the UIToolkit dropdown's --missing tint. The colour is
        // (re)assigned on every call so the cached style survives editor-theme changes.
        private static GUIStyle _missingCaptionStyle;

        private static GUIStyle GetMissingCaptionStyle()
        {
            _missingCaptionStyle ??= new GUIStyle(EditorStyles.miniPullDown);
            _missingCaptionStyle.normal.textColor = NoticeColor;
            _missingCaptionStyle.hover.textColor = NoticeColor;
            _missingCaptionStyle.active.textColor = NoticeColor;
            _missingCaptionStyle.focused.textColor = NoticeColor;
            return _missingCaptionStyle;
        }

        // IMGUI clips a too-long caption at its RIGHT edge, which would cut the informative class-name tail — so
        // mirror the UIToolkit start-ellipsis by hand: binary-search how many leading characters to drop behind "…".
        private static readonly GUIContent _measureContent = new();

        private static string FitCaptionFromLeft(GUIStyle style, string text, float width)
        {
            _measureContent.text = text;
            if (style.CalcSize(_measureContent).x <= width) return text;

            // low..high — candidate counts of dropped leading characters; find the smallest that fits.
            int low = 1, high = text.Length;
            while (low < high)
            {
                var mid = (low + high) / 2;
                _measureContent.text = "…" + text.Substring(mid);

                if (style.CalcSize(_measureContent).x <= width) high = mid;
                else low = mid + 1;
            }

            return "…" + text.Substring(low);
        }

        // Warning yellow mirrors the UIToolkit notice palette:
        // --aspid-colors-status-warning-text-light / -lightness.
        private static readonly Color NoticeColor = new(245f / 255f, 185f / 255f, 85f / 255f);
        private static readonly Color NoticeColorHover = new(255f / 255f, 235f / 255f, 175f / 255f);

        // How far the shared action's rid colour lightens toward white on hover — mirrors the UIToolkit notice's
        // ActionHoverLighten, the hover feedback in place of a static USS brighten (the rid colour is dynamic).
        private const float ActionHoverLighten = 0.35f;

        // Leading rid swatch size on the shared-reference notice — mirrors the UIToolkit __dot (8px).
        private const float DotSize = 8f;

        // Space the foldout arrow reserves left of the value's label; notices pull back by it so their leading edge
        // lines up under the arrow rather than the label.
        private const float FoldoutArrowIndent = 11f;

        // Left status stripe. StripeGutter shifts the field body right to clear a gutter for the bar (mirrors the
        // UIToolkit padding-left). StripeOffset places the bar inside it, measured left from the indented content so
        // its gap from the arrow is depth-independent. StripeInsetY keeps adjacent full-height stripes from merging.
        private const float StripeGutter = 5f;
        private const float StripeWidth = 2f;
        private const float StripeOffset = 16f;
        private const float StripeInsetY = 2f;

        // Dim grey for the non-actionable mixed-types info hint, mirroring the UIToolkit info notice's --aspid-colors-text-dark.
        private static readonly Color _infoNoticeColor = new(150f / 255f, 150f / 255f, 150f / 255f);

        private static GUIStyle _messageStyle;
        private static GUIStyle _actionStyle;
        private static GUIStyle _infoMessageStyle;

        /// <summary>
        /// Draws a compact single-row, non-actionable info hint: a small info icon and a terse dim message. Used for the
        /// multi-object "different types" notice that stands in for the suppressed child fields, mirroring the UIToolkit
        /// <see cref="SerializeReferenceNotice"/> info variant. The full <paramref name="detail"/> rides the hover tooltip.
        /// </summary>
        private static void DrawInfoNotice(Rect rect, string message, string detail)
        {
            _infoMessageStyle ??= new GUIStyle(EditorStyles.label) { wordWrap = false };
            _infoMessageStyle.normal.textColor = _infoNoticeColor;

            const float iconSize = 16f;
            var iconRect = new Rect(rect.x, rect.y + (rect.height - iconSize) * 0.5f, iconSize, iconSize);
            GUI.Label(iconRect, EditorGUIUtility.IconContent("console.infoicon"));

            var messageContent = new GUIContent(message, detail);
            var messageRect = new Rect(iconRect.xMax + 4f, rect.y, rect.xMax - iconRect.xMax - 4f, rect.height);
            GUI.Label(messageRect, messageContent, _infoMessageStyle);
        }

        /// <summary>
        /// Draws a compact single-row notice, mirroring the UIToolkit <see cref="SerializeReferenceNotice"/>: a terse
        /// message, then a bold, right-pinned clickable action word (underlined; it lightens on hover) and — for a
        /// missing-type notice with a Smart Fix candidate — an optional trailing suggestion word ("· → Pistol")
        /// clustered just after it at the right edge. The full <paramref name="detail"/> rides each segment's hover
        /// tooltip. Without <paramref name="ridColor"/> the row is a warning: a yellow triangle icon and the warning
        /// amber palette (missing-type / required). With <paramref name="ridColor"/> it is the shared-reference variant:
        /// no icon, a leading rid-coloured swatch, and the message plus action both tinted that per-rid colour — so
        /// aliased fields read as one colour and match at a glance. <paramref name="onMessageClick"/>, when given,
        /// makes the message itself clickable (link cursor + hover lighten, no underline — the action words keep that
        /// affordance) — the shared notice's "show me the other members of this group" segment.
        /// </summary>
        private static void DrawNotice(Rect rect, string message, string actionText, string detail, Action onClick,
            string suggestionText = null, string suggestionDetail = null, Action onSuggestion = null,
            Color? ridColor = null, Action onMessageClick = null)
        {
            var shared = ridColor.HasValue;
            var baseColor = shared ? ridColor.Value : NoticeColor;
            var hoverColor = shared ? Color.Lerp(baseColor, Color.white, ActionHoverLighten) : NoticeColorHover;

            _messageStyle ??= new GUIStyle(EditorStyles.label) { wordWrap = false };
            _actionStyle ??= new GUIStyle(EditorStyles.label) { fontStyle = FontStyle.Bold };
            _messageStyle.normal.textColor = baseColor;

            float messageX;
            if (shared)
            {
                // Shared-reference variant: no warning icon; lead the row with the rid-coloured swatch instead.
                DrawDot(rect.x, rect, baseColor);
                messageX = rect.x + DotSize + 6f;
            }
            else
            {
                const float iconSize = 16f;
                var iconRect = new Rect(rect.x, rect.y + (rect.height - iconSize) * 0.5f, iconSize, iconSize);
                GUI.Label(iconRect, EditorGUIUtility.IconContent("console.warnicon"));
                messageX = iconRect.xMax + 4f;
            }

            var messageContent = new GUIContent(message, detail);
            var messageWidth = _messageStyle.CalcSize(messageContent).x;
            var messageRect = new Rect(messageX, rect.y, messageWidth, rect.height);
            if (onMessageClick is not null)
            {
                // Clickable message (the shared notice's group navigation): link cursor and the same hover lighten as
                // the action beside it, mirroring the UIToolkit __message--navigable treatment.
                var messageHover = messageRect.Contains(Event.current.mousePosition);
                var messageColor = messageHover ? hoverColor : baseColor;
                _messageStyle.normal.textColor = messageColor;
                _messageStyle.hover.textColor = messageColor;

                EditorGUIUtility.AddCursorRect(messageRect, MouseCursor.Link);
                if (GUI.Button(messageRect, messageContent, _messageStyle)) onMessageClick();
            }
            else
            {
                // The style is shared across notices — reset the hover tint a clickable message may have left behind.
                _messageStyle.hover.textColor = baseColor;
                GUI.Label(messageRect, messageContent, _messageStyle);
            }

            if (string.IsNullOrEmpty(actionText) || onClick is null) return;

            // Right-align the action cluster (mirrors the UIToolkit margin-left:auto): measure the action and any
            // trailing Smart Fix suggestion, then pin them flush to the row's right edge — never overlapping the message.
            var actionContent = new GUIContent(actionText, detail);
            var actionWidth = _actionStyle.CalcSize(actionContent).x;

            var hasSuggestion = !string.IsNullOrEmpty(suggestionText) && onSuggestion is not null;
            var suggestionContent = hasSuggestion ? new GUIContent(suggestionText, suggestionDetail) : null;
            var suggestionWidth = hasSuggestion ? _actionStyle.CalcSize(suggestionContent).x : 0f;
            const float suggestionGap = 6f;

            // The "·" between Fix and the suggestion is decoration, not an action — drawn as a plain label (no
            // underline, no link cursor, no hover), mirroring the UIToolkit notice's separator element.
            var separatorContent = hasSuggestion ? new GUIContent("·") : null;
            var separatorWidth = hasSuggestion ? _actionStyle.CalcSize(separatorContent).x : 0f;

            var clusterWidth = actionWidth +
                (hasSuggestion ? suggestionGap + separatorWidth + suggestionGap + suggestionWidth : 0f);
            var actionX = Mathf.Max(messageRect.xMax + 6f, rect.xMax - clusterWidth);

            DrawLink(new Rect(actionX, rect.y, actionWidth, rect.height), actionContent, baseColor, hoverColor, onClick);

            if (hasSuggestion)
            {
                _actionStyle.normal.textColor = baseColor;
                _actionStyle.hover.textColor = baseColor;
                GUI.Label(new Rect(actionX + actionWidth + suggestionGap, rect.y, separatorWidth, rect.height),
                    separatorContent, _actionStyle);

                DrawLink(new Rect(actionX + actionWidth + suggestionGap + separatorWidth + suggestionGap, rect.y,
                    suggestionWidth, rect.height), suggestionContent, baseColor, hoverColor, onSuggestion);
            }
        }

        /// <summary>
        /// Draws the shared non-actionable "required" warning row (warning icon + yellow message). Reused by the string
        /// <see cref="Aspid.FastTools.Types.Editors.TypeIMGUIPropertyDrawer"/> path so its required notice matches the
        /// managed-reference one exactly.
        /// </summary>
        public static void DrawRequiredNotice(Rect rect, string message, string detail) =>
            DrawNotice(rect, message, actionText: string.Empty, detail: detail, onClick: null);

        // One bold, clickable link word — underlined to match the UIToolkit notice's <u> action treatment — that
        // lightens on hover. Shared by Fix, the Smart Fix suggestion and Make-unique; the caller supplies the colours.
        private static void DrawLink(Rect linkRect, GUIContent content, Color color, Color hoverColor, Action onClick)
        {
            var hover = linkRect.Contains(Event.current.mousePosition);
            var drawColor = hover ? hoverColor : color;
            _actionStyle.normal.textColor = drawColor;
            _actionStyle.hover.textColor = drawColor;

            EditorGUIUtility.AddCursorRect(linkRect, MouseCursor.Link);

            // IMGUI rich text has no <u>, so the underline is a hand-drawn 1px line under the word.
            EditorGUI.DrawRect(new Rect(linkRect.x + 1f, linkRect.yMax - 3f, linkRect.width - 2f, 1f), drawColor);

            if (GUI.Button(linkRect, content, _actionStyle)) onClick();
        }

        // The rid-coloured swatch leading the shared-reference notice, mirroring the UIToolkit rounded __dot: IMGUI
        // has no circle primitive, so the 1×1 white texture is tinted and fully rounded via GUI.DrawTexture.
        private static void DrawDot(float x, Rect rect, Color color)
        {
            var dotRect = new Rect(x, rect.y + (rect.height - DotSize) * 0.5f, DotSize, DotSize);
            GUI.DrawTexture(dotRect, Texture2D.whiteTexture, ScaleMode.StretchToFill,
                alphaBlend: true, imageAspect: 0f, color: color, borderWidth: 0f, borderRadius: DotSize * 0.5f);
        }
    }
}
