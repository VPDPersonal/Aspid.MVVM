#if !ASPID_UI_EDITOR_DISABLED
using System.Linq;
using UnityEditor;
using UnityEngine;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Views;
using Aspid.CustomEditors;
using UnityEngine.UIElements;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.Unity.Views;
using System.Collections.Generic;
using Aspid.CustomEditors.Configs;
using Aspid.CustomEditors.Components;
using Aspid.CustomEditors.Components.Extensions;
using Aspid.CustomEditors.Extensions.VisualElements;

namespace Aspid.UI.MVVM.Unity
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoBinder), true)]
    public class MonoBinderEditor : BinderEditorBase<MonoBinder>
    {
        private SerializedProperty _id;
        private SerializedProperty _view;
        
        private SerializedProperty _log;
        private SerializedProperty _isDebug;
        
        protected VisualElement Root { get; private set; }
        
        protected virtual string[] PropertiesExcluding => new[]
        {
            _id.name,
            _view.name,
            "m_Script",
            _log?.name,
            _isDebug?.name,
        };
        
        private bool IsBinderAssignedError => string.IsNullOrEmpty(_id?.stringValue);
        
        private string IconPath => !IsBinderAssignedError ? "Aspid Icon" : "Aspid Icon Red";

        private void OnEnable() => FindProperties();

        public sealed override VisualElement CreateInspectorGUI()
        {
            OnCreatingInspectorGUI();
            Root = Build();
            OnCreatedInspectorGUI();

            return Root;
        }
        
        protected virtual void OnCreatingInspectorGUI() { }

        protected virtual void OnCreatedInspectorGUI()
        {
            var header = Root.Q<VisualElement>("Header");
            
            var idDropdown = Root.Q<DropdownField>("IdDropdown");
            idDropdown.RegisterValueChangedCallback(OnIdChanged);
            
            var viewDropdown = Root.Q<DropdownField>("ViewDropdown");
            viewDropdown.RegisterValueChangedCallback(OnViewChanged);
            return;

            void OnIdChanged(ChangeEvent<string> value)
            {
                using (SyncerView.Sync(this))
                {
                    SaveId(value.newValue);
                    UpdateErrorStatusView();
                }
            }
            
            void OnViewChanged(ChangeEvent<string> value)
            {
                using (SyncerView.Sync(this))
                {
                    SaveView(value.newValue);
                    
                    _id.stringValue = string.Empty;
                    var newIdDropdown = DropdownFields.GetIdDropdown(this);
                    idDropdown.choices = newIdDropdown.choices.ToList();
                    idDropdown.value = newIdDropdown.value;
                
                    SaveId(idDropdown.value);
                    UpdateErrorStatusView();
                }
            }
            
            void UpdateErrorStatusView()
            {
                Root.Q<HelpBox>().SetDisplay(IsBinderAssignedError ? DisplayStyle.Flex : DisplayStyle.None);
                header.Q<Image>().SetImageFromResource(IconPath);
            }
        }

        protected virtual void FindProperties()
        {
            _id = serializedObject.FindProperty(nameof(_id));
            _view = serializedObject.FindProperty(nameof(_view));
            
            _log = serializedObject.FindProperty(nameof(_log));
            _isDebug = serializedObject.FindProperty(nameof(_isDebug));
        }

        #region Build
        protected virtual VisualElement Build()
        {
            var root = new VisualElement();
            var header = Elements.CreateHeader(Binder, IconPath);

            var defaultInspector = Elements.CreateContainer(EditorColor.LightContainer)
                .AddTitle(EditorColor.LightText, "Parameters")
                .AddChild(new IMGUIContainer(DrawBaseInspector));
            
            var logContainer = Elements.CreateContainer(EditorColor.LightContainer) 
                .AddTitle(EditorColor.LightText, "Logs") 
                .AddChild(new IMGUIContainer(DrawDebugLog));

            root.AddChild(header)
                .AddChild(BuildBinderId()
                    .SetMargin(top: 10))
                .AddChild(defaultInspector
                    .SetMargin(top: 10))
                .AddChild(logContainer
                    .SetMargin(top: 10));

            return root;
        }
        
        private VisualElement BuildBinderId()
        {
            var idDropdown = DropdownFields.GetIdDropdown(this);
            var viewDropdown = DropdownFields.GetViewDropdown(this);

            var helpBox = Elements.CreateHelpBox("View and ID must be assigned", HelpBoxMessageType.Error)
                .SetHelpBoxFontSize(14)
                .SetDisplay(IsBinderAssignedError ? DisplayStyle.Flex : DisplayStyle.None);

            var fieldsContainer = new VisualElement()
                .SetAlignItems(Align.Center)
                .SetFlexDirection(FlexDirection.Row)
                .AddChild(CreateField("View", viewDropdown))
                .AddChild(CreateField("ID", idDropdown));

            return Elements.CreateContainer(EditorColor.DarkContainer)
                .SetFlexDirection(FlexDirection.Column)
                .AddChild(fieldsContainer)
                .AddChild(helpBox);

            VisualElement CreateField(string text, DropdownField dropdown)
            {
                return new VisualElement()
                    .SetFlexGrow(1)
                    .SetFlexDirection(FlexDirection.Column)
                    .SetSize(width: new StyleLength(new Length(50, LengthUnit.Percent)))
                    .AddChild(new Label(text)
                        .SetFontSize(13)
                        .SetPadding(left: 5)
                        .SetAlignSelf(Align.FlexStart)
                        .SetColor(EditorColor.LightText)
                        .SetUnityFontStyleAndWeight(FontStyle.Bold))
                    .AddChild(dropdown
                        .SetFlexGrow(1));
            }
        }
        #endregion
        
        #region Draw
        protected virtual void DrawBaseInspector() =>
            DrawBaseInspectorIternal();

        private void DrawBaseInspectorIternal()
        {
            serializedObject.UpdateIfRequiredOrScript();
            {
                DrawPropertiesExcluding(serializedObject, PropertiesExcluding);
            }
            serializedObject.ApplyModifiedProperties();
        }   
        
        private void DrawDebugLog()
        {
            const string isShowLogKey = "IsShowLogKey";
            const string scrollLogPositionYKey = "ScrollLogPositionYKey";

            if (_isDebug is null || _log is null) return;
        
            serializedObject.UpdateIfRequiredOrScript();
            {
                EditorGUILayout.PropertyField(_isDebug, new GUIContent("Is Debug Log"));
            }
            serializedObject.ApplyModifiedProperties();
            
            if (!Application.isPlaying || !_isDebug.boolValue) return;
            var isShow = EditorPrefs.GetBool(isShowLogKey);
            
            serializedObject.UpdateIfRequiredOrScript();
            {
                isShow = EditorGUILayout.Foldout(isShow, new GUIContent("Is Show Log"));
            }
            serializedObject.ApplyModifiedProperties();
            
            EditorPrefs.SetBool(isShowLogKey, isShow);
            if (!isShow) return;

            var count = _log.arraySize;
            var visibleLines = Mathf.Min(count, 5);
            var height = EditorGUIUtility.singleLineHeight * (visibleLines * 3);
            var scrollPosition = new Vector2(0, EditorPrefs.GetFloat(scrollLogPositionYKey));

            using (AspidEditorGUILayout.BeginScrollView(ref scrollPosition, GUILayout.Height(height)))
            {
                for (var i = 0; i < count; i++)
                {
                    using (AspidEditorGUILayout.BeginHorizontal(GUI.skin.box))
                        EditorGUILayout.HelpBox(_log.GetArrayElementAtIndex(i).stringValue, MessageType.Info);
                }
            }

            EditorPrefs.SetFloat(scrollLogPositionYKey, scrollPosition.y);
        }
        #endregion

        #region Save
        private void SaveId(string id)
        {
            serializedObject.UpdateIfRequiredOrScript();
            {
                _id.stringValue = id == "No Id" ? string.Empty : id;
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void SaveView(string viewName)
        {
            serializedObject.UpdateIfRequiredOrScript();
            {
                _view.objectReferenceValue = null;
                foreach (var view in GetViewList().Where(view => view.name == viewName))
                {
                    _view.objectReferenceValue = view;
                    break;
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
        #endregion
        
        protected class DropdownFields
        {
            public static DropdownField GetIdDropdown(MonoBinderEditor editor)
            {
                const string noneValue = "No Id";
                
                var id = editor._id.stringValue;
                var view = editor._view.objectReferenceValue;
            
                var dropdown = view == null
                    ? GetDropdown(noneValue) 
                    : GetDropdown(noneValue, id, editor.GetIdList(view as IView));

                dropdown.name = "IdDropdown";
                return dropdown;
            }

            public static DropdownField GetViewDropdown(MonoBinderEditor editor)
            {
                const string noneValue = "No View";
                
                var views = editor.GetViewList();
                var viewName = editor._view.objectReferenceValue?.name;
            
                var dropdown = views.Count == 0
                    ? GetDropdown(noneValue) 
                    : GetDropdown(noneValue, viewName, views.Select(view => view.name).ToList());
            
                dropdown.name = "ViewDropdown";
                return dropdown;
            }
            
            public static DropdownField GetDropdown(string noneValue, string defaultValue = null, List<string> choices = null)
            {
                if (choices is null || choices.Count == 0) 
                    return new DropdownField(new List<string> { noneValue }, 0);
            
                choices = new List<string>(choices);
                choices.Insert(0, null);
                choices.Insert(0, noneValue);
            
                return string.IsNullOrEmpty(defaultValue) 
                    ? new DropdownField(choices, 0) 
                    : new DropdownField(choices, defaultValue);
            }
        }
        
        protected readonly ref struct SyncerView
        {
            private readonly string _previousId;
            private readonly MonoView _previousView;
            private readonly MonoBinderEditor _editor;
            
            private SyncerView(MonoBinderEditor editor)
            {
                _editor = editor;
                _previousId = _editor._id.stringValue;
                _previousView = _editor._view.objectReferenceValue as MonoView;
            }
            
            public static SyncerView Sync(MonoBinderEditor editor) => new(editor);
            
            public void Dispose()
            {
                var binder = _editor.Binder;
                var id = _editor._id.stringValue;
                var view = _editor._view.objectReferenceValue;
                
                if (_previousView?.GetInstanceID() == view?.GetInstanceID() 
                    && _previousId == id) return;
            
                if (_previousView && !string.IsNullOrEmpty(_previousId))
                    ViewUtility.RemoveMonoBinderIfSet(_previousView, binder, _previousId);
                
                if (view && !string.IsNullOrEmpty(id))
                    ViewUtility.SetMonoBinderIfNotSet((MonoView)view, binder, id);
            }
        }
    }
}
#endif
