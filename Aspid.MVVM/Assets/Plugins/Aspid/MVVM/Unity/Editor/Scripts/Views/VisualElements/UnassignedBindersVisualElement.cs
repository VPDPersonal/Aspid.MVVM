#if !ASPID_MVVM_EDITOR_DISABLED
#nullable enable
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Aspid.FastTools;
using Aspid.MVVM.Validation;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Refactor
    /// <summary>
    /// UIElements visual element that displays a warning listing any <see cref="MonoBinder"/> slots
    /// in a <see cref="MonoView"/> that have not been assigned.
    /// </summary>
    /// <typeparam name="TMonoView">The concrete <see cref="MonoView"/> type being inspected.</typeparam>
    /// <typeparam name="TEditor">The corresponding <see cref="MonoViewEditor{TMonoView,TEditor}"/> type.</typeparam>
    public sealed class UnassignedBindersVisualElement<TMonoView, TEditor> : VisualElement
        where TMonoView : MonoView
        where TEditor : MonoViewEditor<TMonoView, TEditor>
    {
        private const string Warning = "It is recommended not to leave unassigned Binders";

        private static readonly StyleSheet _styleSheet = Resources.Load<StyleSheet>("Styles/Aspid-MVVM-UnassignedBinders");

        private readonly TEditor _editor;
        private readonly VisualElement _unassignedBindersContainer;
        private readonly Action<IMonoBinderValidable>? _onBinderClicked;
        private readonly Func<IMonoBinderValidable, bool>? _canAutoAssign;
        private readonly Action<IMonoBinderValidable>? _onAutoAssign;

        private IMonoBinderValidable[]? LastBinders;

        private readonly HashSet<IMonoBinderValidable> _selectedBinders = new();
        private readonly List<ObjectField> _currentFields = new();
        private int _lastClickedIndex = -1;

        public UnassignedBindersVisualElement(TEditor editor, Action<IMonoBinderValidable>? onBinderClicked = null, Func<IMonoBinderValidable, bool>? canAutoAssign = null, Action<IMonoBinderValidable>? onAutoAssign = null)
        {
            _editor = editor;
            _onAutoAssign = onAutoAssign;
            _canAutoAssign = canAutoAssign;
            _onBinderClicked = onBinderClicked;

            // TODO Aspid.MVVM Unity – Rename Name
            _unassignedBindersContainer = new VisualElement().SetName("UnassignedBindersContainer");

            // Prevent any external drag from being dropped onto this element.
            RegisterCallback<DragUpdatedEvent>(evt =>
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Rejected;
                evt.StopPropagation();
            }, TrickleDown.TrickleDown);

            // Deselect when the user clicks anywhere outside this element.
            RegisterCallback<AttachToPanelEvent>(evt =>
                evt.destinationPanel.visualTree.RegisterCallback<MouseDownEvent>(OnPanelMouseDown));
            RegisterCallback<DetachFromPanelEvent>(evt =>
                evt.originPanel.visualTree.UnregisterCallback<MouseDownEvent>(OnPanelMouseDown));

            styleSheets.Add(_styleSheet);
            style.display = DisplayStyle.None;
            Add(Build());
        }

        private VisualElement Build()
        {
            return new AspidContainer()
                .AddChild(new AspidTitle("Unassigned Binders"))
                .AddChild(new AspidHelpBox(Warning, HelpBoxMessageType.Warning)
                    .SetMargin(bottom:5))
                .AddChild(_unassignedBindersContainer);
        }

        public void Invalidate() => LastBinders = null;

        public void Update()
        {
            var binders = _editor.UnassignedBinders.ToArray();
            if (binders.Length == LastBinders?.Length)
            {
                if (LastBinders.SequenceEqual(binders))
                {
                    return;
                }
            }

            LastBinders = binders;
            _selectedBinders.IntersectWith(binders);
            _lastClickedIndex = -1;

            var count = binders.Length;
            _currentFields.Clear();
            _unassignedBindersContainer.Clear();

            foreach (var unassignedBinder in LastBinders)
            {
                var capturedIndex = _currentFields.Count;
                var previousId = unassignedBinder.PreviousId.Id;

                var field = new ObjectField()
                    .SetOpacity(1)
                    .SetValue((Object)unassignedBinder)
                    .SetMargin(top: count > 0 ? 5 : 0, bottom: 0, left: 0, right: 0);

                // Disable the object field's dropdown
                field.Q<VisualElement>(className: "unity-object-field__selector")
                    .SetDisplay(DisplayStyle.None);

                if (!string.IsNullOrWhiteSpace(previousId))
                {
                    var missingLabel = new Label(previousId);
                    missingLabel.AddToClassList("aspid-unassigned-binders-previous-id");

                    var input = field.Q<VisualElement>(className: "unity-base-field__input");
                    input?.Add(missingLabel);
                }

                if (_selectedBinders.Contains(unassignedBinder))
                    SetFieldSelectedStyle(field, true);

                var isDraggingBinder = false;

                // Track the modifier state from MouseDown so that MouseUp can apply
                // the correct deferred action (deselect, toggle, or range-select).
                var hasPendingAction = false;
                var pendingShiftKey = false;
                var pendingActionKey = false;

                field.RegisterCallback<MouseDownEvent>(evt =>
                {
                    if (evt.button is not 0) return;

                    isDraggingBinder = false;
                    hasPendingAction = false;

                    if (!_selectedBinders.Contains(unassignedBinder))
                    {
                        // Unselected item: apply selection logic immediately.
                        if (evt.shiftKey && _lastClickedIndex >= 0)
                        {
                            _selectedBinders.Clear();
                            var min = Math.Min(_lastClickedIndex, capturedIndex);
                            var max = Math.Max(_lastClickedIndex, capturedIndex);
                            for (var i = min; i <= max; i++)
                                _selectedBinders.Add(LastBinders![i]);
                        }
                        else if (evt.actionKey)
                        {
                            _selectedBinders.Add(unassignedBinder);
                            _lastClickedIndex = capturedIndex;
                        }
                        else
                        {
                            _selectedBinders.Clear();
                            _selectedBinders.Add(unassignedBinder);
                            _lastClickedIndex = capturedIndex;
                        }

                        UpdateSelectionStyles();
                    }
                    else
                    {
                        // Selected item: defer the selection change to MouseUp so that a
                        // drag can start without the modifier keys altering the selection.
                        hasPendingAction = true;
                        pendingShiftKey = evt.shiftKey;
                        pendingActionKey = evt.actionKey;
                    }

                    _onBinderClicked?.Invoke(unassignedBinder);
                }, TrickleDown.TrickleDown);

                field.RegisterCallback<MouseUpEvent>(evt =>
                {
                    if (evt.button is not 0 || !hasPendingAction || isDraggingBinder) return;

                    hasPendingAction = false;

                    // Apply the deferred modifier action now that we know no drag occurred.
                    if (pendingShiftKey && _lastClickedIndex >= 0)
                    {
                        _selectedBinders.Clear();
                        var min = Math.Min(_lastClickedIndex, capturedIndex);
                        var max = Math.Max(_lastClickedIndex, capturedIndex);
                        for (var i = min; i <= max; i++)
                            _selectedBinders.Add(LastBinders![i]);
                    }
                    else if (pendingActionKey)
                    {
                        // Ctrl/Cmd+click: deselect this item.
                        _selectedBinders.Remove(unassignedBinder);
                        _lastClickedIndex = capturedIndex;
                    }
                    else
                    {
                        // Plain click: collapse selection to just this item.
                        _selectedBinders.Clear();
                        _selectedBinders.Add(unassignedBinder);
                        _lastClickedIndex = capturedIndex;
                    }

                    UpdateSelectionStyles();
                }, TrickleDown.TrickleDown);

                field.RegisterCallback<MouseMoveEvent>(evt =>
                {
                    if (evt.pressedButtons is not 1 || isDraggingBinder) return;

                    // Drag has started — cancel any pending deferred selection change.
                    hasPendingAction = false;
                    isDraggingBinder = true;
                    DragAndDrop.PrepareStartDrag();

                    if (_selectedBinders.Contains(unassignedBinder) && _selectedBinders.Count > 1)
                    {
                        DragAndDrop.objectReferences = _selectedBinders.Cast<Object>().ToArray();
                        DragAndDrop.StartDrag($"{_selectedBinders.Count} Binders");
                    }
                    else
                    {
                        DragAndDrop.objectReferences = new[] { (Object)unassignedBinder };
                        DragAndDrop.StartDrag(((Object)unassignedBinder).name);
                    }

                    evt.StopPropagation();
                }, TrickleDown.TrickleDown);

                _currentFields.Add(field);

                if (_canAutoAssign != null && _onAutoAssign != null && _canAutoAssign(unassignedBinder))
                {
                    var capturedBinder = unassignedBinder;
                    var autoAssignButton = new Button(() => _onAutoAssign(capturedBinder)) { text = "→" };
                    autoAssignButton.AddToClassList("aspid-unassigned-binders-auto-assign-button");

                    field.SetMargin(top: 0);
                    var row = new VisualElement()
                        .SetFlexGrow(1)
                        .SetAlignItems(Align.Center)
                        .SetFlexDirection(FlexDirection.Row)
                        .SetMargin(top: count > 0 ? 5 : 0)
                        .AddChild(field)
                        .AddChild(autoAssignButton);

                    _unassignedBindersContainer.AddChild(row);
                }
                else
                {
                    _unassignedBindersContainer.AddChild(field);
                }

                count++;
            }

            style.SetDisplay(_unassignedBindersContainer.childCount > 0 ? DisplayStyle.Flex : DisplayStyle.None);
        }

        private void OnPanelMouseDown(MouseDownEvent evt)
        {
            if (evt.button is not 0 || _selectedBinders.Count == 0) return;
            if (worldBound.Contains(evt.mousePosition)) return;
            
            _selectedBinders.Clear();
            UpdateSelectionStyles();
        }

        private void UpdateSelectionStyles()
        {
            if (LastBinders is null) return;
            for (var i = 0; i < _currentFields.Count; i++)
                SetFieldSelectedStyle(_currentFields[i], _selectedBinders.Contains(LastBinders[i]));
        }

        private static void SetFieldSelectedStyle(ObjectField field, bool selected)
        {
            // ObjectField's internal input element has its own opaque background that
            // overrides any color set on the root — target it directly instead.
            var inputElement = field.Q<VisualElement>(className: "unity-base-field__input") ?? field;

            inputElement.SetBackgroundColor(selected
                ? new StyleColor(new Color(0.172f, 0.364f, 0.529f, 1f))
                : new StyleColor(StyleKeyword.Null));
        }
    }
}
#endif