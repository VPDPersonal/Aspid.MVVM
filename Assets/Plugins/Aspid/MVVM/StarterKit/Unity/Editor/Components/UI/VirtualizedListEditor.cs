using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Aspid.CustomEditors;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using UnityEditor.AnimatedValues;
using Aspid.UnityFastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [CustomEditor(typeof(VirtualizedList), true)]
    public class VirtualizedListEditor : Editor
    {
        private const string VerticalError = "For this visibility mode, the Viewport property and the Vertical Scrollbar property both needs to be set to a Rect Transform that is a child to the Scroll Rect.";
        private const string HorizontalError = "For this visibility mode, the Viewport property and the Horizontal Scrollbar property both needs to be set to a Rect Transform that is a child to the Scroll Rect.";
        
        private SerializedProperty _content;
        private SerializedProperty _viewport;
        private SerializedProperty _viewPrefab;
        
        private SerializedProperty _inertia;
        private SerializedProperty _elasticity;
        private SerializedProperty _movementType;
        private SerializedProperty _decelerationRate;
        private SerializedProperty _scrollSensitivity;
        
        private SerializedProperty _horizontal;
        private SerializedProperty _horizontalScrollbar;
        private SerializedProperty _horizontalScrollbarSpacing;
        private SerializedProperty _horizontalScrollbarVisibility;
        
        private SerializedProperty _vertical;
        private SerializedProperty _verticalScrollbar;
        private SerializedProperty _verticalScrollbarSpacing;
        private SerializedProperty _verticalScrollbarVisibility;
        
        private SerializedProperty _onValueChanged;
        
        private AnimBool _showElasticity;
        private AnimBool _showDecelerationRate;

        private bool _viewportIsNotChild;
        private bool _verticalScrollbarIsNotChild;
        private bool _horizontalScrollbarIsNotChild;

        private void OnEnable()
        {
            _content = serializedObject.FindProperty("m_Content");
            _viewport = serializedObject.FindProperty("m_Viewport");
            _viewPrefab = serializedObject.FindProperty("_viewPrefab");
            
            _inertia = serializedObject.FindProperty("m_Inertia");
            _elasticity = serializedObject.FindProperty("m_Elasticity");
            _movementType = serializedObject.FindProperty("m_MovementType");
            _decelerationRate = serializedObject.FindProperty("m_DecelerationRate");
            _scrollSensitivity = serializedObject.FindProperty("m_ScrollSensitivity");
            
            _horizontal = serializedObject.FindProperty("m_Horizontal");
            _horizontalScrollbar = serializedObject.FindProperty("m_HorizontalScrollbar");
            _horizontalScrollbarVisibility = serializedObject.FindProperty("m_HorizontalScrollbarVisibility");
            _horizontalScrollbarSpacing = serializedObject.FindProperty("m_HorizontalScrollbarSpacing");
            
            _vertical = serializedObject.FindProperty("m_Vertical");
            _verticalScrollbar = serializedObject.FindProperty("m_VerticalScrollbar");
            _verticalScrollbarSpacing = serializedObject.FindProperty("m_VerticalScrollbarSpacing");
            _verticalScrollbarVisibility = serializedObject.FindProperty("m_VerticalScrollbarVisibility");
            
            _onValueChanged = serializedObject.FindProperty("m_OnValueChanged");
            
            _showElasticity = new AnimBool(Repaint);
            _showDecelerationRate = new AnimBool(Repaint);
            SetAnimBools(true);
        }

        private void OnDisable()
        {
            _showElasticity.valueChanged.RemoveListener(Repaint);
            _showDecelerationRate.valueChanged.RemoveListener(Repaint);
        }
        
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            var header = new InspectorHeaderPanel(target, "Aspid Icon");
            header.AddOpenScriptCommand(target);
            
            var container = Elements.CreateContainer(EditorColor.DarkContainer)
                .AddChild(new IMGUIContainer(DrawInspector));

            return root
                .AddChild(header
                    .SetMargin(bottom: 10))
                .AddChild(container);
        }

        protected virtual void DrawInspector()
        {
            SetAnimBools(false);

            serializedObject.Update();
            {
                // Once we have a reliable way to know if the object changed, only re-cache in that case.
                CalculateCachedValues();

                EditorGUILayout.PropertyField(_content);
                EditorGUILayout.PropertyField(_viewPrefab);
                
                DrawDirection();
                DrawMovement();
                DrawInertia();

                EditorGUILayout.PropertyField(_scrollSensitivity);
                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(_viewport);

                if (_horizontal.boolValue)
                    DrawHorizontalScrollbar();

                if (_vertical.boolValue)
                    DrawVerticalScrollbar();

                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(_onValueChanged);
            }
            serializedObject.ApplyModifiedProperties();
            return;

            void DrawDirection()
            {
                var index = EditorGUILayout.Popup(
                    EditorGUIUtility.TrTextContent("Direction"),
                    _vertical.boolValue ? 0 : 1,
                    new[] { "Vertical", "Horizontal" });

                if (index is 0)
                {
                    _vertical.boolValue = true;
                    _horizontal.boolValue = false;
                }
                else
                {
                    _vertical.boolValue = false;
                    _horizontal.boolValue = true;
                }
            }

            void DrawMovement()
            {
                EditorGUILayout.PropertyField(_movementType);
                if (EditorGUILayout.BeginFadeGroup(_showElasticity.faded))
                {
                    EditorGUI.indentLevel++;
                    {
                        EditorGUILayout.PropertyField(_elasticity);
                    }
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.EndFadeGroup();
            }

            void DrawInertia()
            {
                EditorGUILayout.PropertyField(_inertia);
                if (EditorGUILayout.BeginFadeGroup(_showDecelerationRate.faded))
                {
                    EditorGUI.indentLevel++;
                    {
                        EditorGUILayout.PropertyField(_decelerationRate);
                    }
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.EndFadeGroup();
            }

            void DrawVerticalScrollbar()
            {
                EditorGUILayout.PropertyField(_verticalScrollbar);
                if (!_verticalScrollbar.objectReferenceValue || _verticalScrollbar.hasMultipleDifferentValues) return;
                
                EditorGUI.indentLevel++;
                {
                    EditorGUILayout.PropertyField(_verticalScrollbarVisibility, EditorGUIUtility.TrTextContent("Visibility"));

                    if ((ScrollRect.ScrollbarVisibility)_verticalScrollbarVisibility.enumValueIndex == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport
                        && !_verticalScrollbarVisibility.hasMultipleDifferentValues)
                    {
                        if (_viewportIsNotChild || _verticalScrollbarIsNotChild)
                            EditorGUILayout.HelpBox(VerticalError, MessageType.Error);
                        
                        EditorGUILayout.PropertyField(_verticalScrollbarSpacing, EditorGUIUtility.TrTextContent("Spacing"));
                    }
                }
                EditorGUI.indentLevel--;
            }
            
            void DrawHorizontalScrollbar()
            {
                EditorGUILayout.PropertyField(_horizontalScrollbar);
                if (!_horizontalScrollbar.objectReferenceValue || _horizontalScrollbar.hasMultipleDifferentValues) return;
                
                EditorGUI.indentLevel++;
                {
                    EditorGUILayout.PropertyField(_horizontalScrollbarVisibility, EditorGUIUtility.TrTextContent("Visibility"));

                    if ((ScrollRect.ScrollbarVisibility)_horizontalScrollbarVisibility.enumValueIndex == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport
                        && !_horizontalScrollbarVisibility.hasMultipleDifferentValues)
                    {
                        if (_viewportIsNotChild || _horizontalScrollbarIsNotChild)
                            EditorGUILayout.HelpBox(HorizontalError, MessageType.Error);
                        
                        EditorGUILayout.PropertyField(_horizontalScrollbarSpacing, EditorGUIUtility.TrTextContent("Spacing"));
                    }
                }
                EditorGUI.indentLevel--;
            }
        }

        private void SetAnimBools(bool instant)
        {
            Set(_showElasticity, !_movementType.hasMultipleDifferentValues && _movementType.enumValueIndex == (int)ScrollRect.MovementType.Elastic, instant);
            Set(_showDecelerationRate, !_inertia.hasMultipleDifferentValues && _inertia.boolValue, instant);
            return;

            static void Set(AnimBool a, bool value, bool instant)
            {
                if (instant) a.value = value;
                else a.target = value;
            }
        }
        
        private void CalculateCachedValues()
        {
            _viewportIsNotChild = false;
            _verticalScrollbarIsNotChild = false;
            _horizontalScrollbarIsNotChild = false;
            
            if (targets.Length == 1)
            {
                var transform = ((ScrollRect)target).transform;
                
                if (!_viewport.objectReferenceValue || ((RectTransform)_viewport.objectReferenceValue).transform.parent != transform)
                    _viewportIsNotChild = true;
                
                if (!_horizontalScrollbar.objectReferenceValue || ((Scrollbar)_horizontalScrollbar.objectReferenceValue).transform.parent != transform)
                    _horizontalScrollbarIsNotChild = true;
                
                if (!_verticalScrollbar.objectReferenceValue || ((Scrollbar)_verticalScrollbar.objectReferenceValue).transform.parent != transform)
                    _verticalScrollbarIsNotChild = true;
            }
        }
    }
}