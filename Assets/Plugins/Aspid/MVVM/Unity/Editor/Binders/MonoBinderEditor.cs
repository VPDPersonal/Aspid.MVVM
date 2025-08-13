#if !ASPID_MVVM_EDITOR_DISABLED
using System.Linq;
using UnityEditor;
using UnityEngine;
using Aspid.CustomEditors;
using UnityEngine.UIElements;
using System.Collections.Generic;

namespace Aspid.MVVM.Unity
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoBinder), true)]
    public class MonoBinderEditor : BinderEditorBase<MonoBinder>
    {
        private SerializedProperty _id;
        private SerializedProperty _source;
        private SerializedProperty _mode;
        
        private SerializedProperty _log;
        private SerializedProperty _isDebug;
        
        protected VisualElement Root { get; private set; }
        
        protected virtual string[] PropertiesExcluding => new[]
        {
            _id.name,
            _source.name,
            _mode.name,
            "m_Script",
            _log?.name,
            _isDebug?.name,
        };
        
        private bool IsBinderAssignedError => string.IsNullOrEmpty(_id?.stringValue);
        
        private string IconPath => !IsBinderAssignedError ? "Aspid Icon" : "Aspid Icon Red";
        
        private void OnEnable()
        {
	        FindProperties();
	        Validate();
        }

        private void OnDisable()
        {
            if (Binder) return;
            if (_id is null) return;
            if (_source is null) return;
        
            var view = _source.objectReferenceValue as MonoView;

            if (!view) return;
            if (string.IsNullOrWhiteSpace(_id.stringValue)) return;
            
            ViewUtility.ValidateView(view);
        }

        private void Validate()
        {
	        serializedObject.Update();
	        ValidateView();
	        ValidateId();
	        serializedObject.ApplyModifiedProperties();
	        return;

	        void ValidateView()
	        {
		        if (!_source.objectReferenceValue) return;
		        
		        for (var parent = ((Component)target).transform; parent is not null; parent = parent.parent)
                {
                    if (parent.GetComponents<MonoView>().Any(view => _source.objectReferenceValue == view)) return;
                }

		        _source.objectReferenceValue = null;
	        }

	        void ValidateId()
	        {
		        if (string.IsNullOrEmpty(_id.stringValue)) return;

		        var view = _source.objectReferenceValue as MonoView;

                if (view && view.TryGetMonoBinderValidableFieldById(_id.stringValue, out var field))
                {
                    var binderProperty = new SerializedObject(view).FindProperty(field!.Name);

                    if (binderProperty is not null)
                    {
                        if (binderProperty.isArray)
                        {
                            for (var i = 0; i < binderProperty.arraySize; i++)
                            {
                                if (binderProperty.GetArrayElementAtIndex(i).objectReferenceValue == Binder) return;
                            }
                        }
                        else if (binderProperty.objectReferenceValue == Binder) return;
                    }
                }
                
		        _id.stringValue = null;
	        }
        }

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
            _id = serializedObject.FindProperty("__id");
            _source = serializedObject.FindProperty("__source");
            _mode = serializedObject.FindProperty(nameof(_mode));
            
            _log = serializedObject.FindProperty(nameof(_log));
            _isDebug = serializedObject.FindProperty(nameof(_isDebug));
        }

        #region Build
        protected virtual VisualElement Build()
        {
            var root = new VisualElement();
            var header = Elements.CreateHeader(Binder, IconPath);
            header.Q<Image>("HeaderIcon").AddOpenScriptCommand(target);

            var defaultInspector = Elements.CreateContainer(EditorColor.LightContainer)
                .SetName("Parameters")
                .AddTitle(EditorColor.LightText, "Parameters")
                .AddChild(new IMGUIContainer(DrawBaseInspector));
            
            var logContainer = Elements.CreateContainer(EditorColor.LightContainer) 
                .SetName("Log")
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

            var modeContainer = new IMGUIContainer(DrawMode)
                .SetName("Mode")
                .SetAlignItems(Align.Center)
                .SetMargin(left: 3, right: -2);

            return Elements.CreateContainer(EditorColor.DarkContainer)
                .SetFlexDirection(FlexDirection.Column)
                .AddChild(fieldsContainer)
                .AddChild(helpBox)
                .AddChild(modeContainer);

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
            DrawBaseInspectorInternal();

        private void DrawMode()
        {
            serializedObject.UpdateIfRequiredOrScript();
            {
                EditorGUILayout.PropertyField(_mode, new GUIContent());
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawBaseInspectorInternal()
        {
            var hasProperties = false;
            
            serializedObject.UpdateIfRequiredOrScript();
            {
                var enterChildren = true;
                var iterator = serializedObject.GetIterator();
                
                while (iterator.NextVisible(enterChildren))
                {
                    enterChildren = false;
                    if (!PropertiesExcluding.Contains(iterator.name))
                    {
                        hasProperties = true;
                        EditorGUILayout.PropertyField(iterator, true);
                    }
                }
            }
            serializedObject.ApplyModifiedProperties();
            
            Root.Q<VisualElement>("Parameters").style.display = !hasProperties ? DisplayStyle.None : DisplayStyle.Flex;
        }   
        
        private void DrawDebugLog()
        {
            const string isShowLogKey = "IsShowLogKey";
            const string scrollLogPositionYKey = "ScrollLogPositionYKey";
            
            Root.Q<VisualElement>("Log").style.display = _log is null ? DisplayStyle.None : DisplayStyle.Flex;
            
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
                _source.objectReferenceValue = null;
                foreach (var view in GetViewList().Where(view => view.name == viewName))
                {
                    _source.objectReferenceValue = view.view;
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
                var source = editor._source.objectReferenceValue;
            
                var dropdown = !source
                    ? GetDropdown(noneValue) 
                    : GetDropdown(noneValue, id, editor.GetIdList(source as IMonoBinderSource));

                dropdown.name = "IdDropdown";
                return dropdown;
            }

            public static DropdownField GetViewDropdown(MonoBinderEditor editor)
            {
                const string noneValue = "No View";
                
                var views = editor.GetViewList();
                var viewName = GetViewName(editor._source.objectReferenceValue as MonoView);
            
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
                _previousView = _editor._source.objectReferenceValue as MonoView;
            }
            
            public static SyncerView Sync(MonoBinderEditor editor) => new(editor);
            
            public void Dispose()
            {
                var binder = _editor.Binder;
                var id = _editor._id.stringValue;
                var view = _editor._source.objectReferenceValue;
                
                if (_previousView?.GetInstanceID() == view?.GetInstanceID() 
                    && _previousId == id) return;
            
                if (_previousView && !string.IsNullOrWhiteSpace(_previousId))
                    ViewUtility.RemoveBinderIfExist(_previousView, binder, _previousId);
                
                if (view && !string.IsNullOrWhiteSpace(id))
                    ViewUtility.SetBinderIfNotExist(binder);
            }
        }
    }
}
#endif
