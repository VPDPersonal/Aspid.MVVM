#if !ASPID_MVVM_EDITOR_DISABLED
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Refactor
    // TODO Aspid.MVVM Unity – Write summary
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoBinder), true)]
    public class MonoBinderEditor : Editor
    {
        public MonoBinder TargetAsMonoBinder => target as MonoBinder;

        public bool HasBinderId => !string.IsNullOrWhiteSpace(IdProperty?.stringValue);
        
        #region Serialized Properties
        public SerializedProperty IdProperty { get; private set; }
        
        public SerializedProperty ViewProperty { get; private set; }
        
        public SerializedProperty ModeProperty { get; private set; }
        
        public SerializedProperty LogsProperty { get; private set; }
        
        public SerializedProperty IsDebugProperty { get; private set; }
        #endregion
        
        protected MonoBinderVisualElement Root { get; private set; }
        
        #region Enable
        private void OnEnable()
        {
            OnEnabling();
            FindProperties();
            Validate();
            OnEnabled();
        }
        
        protected virtual void OnEnabling() { }
        
        protected virtual void OnEnabled() { }
        #endregion

        #region Disable
        private void OnDisable()
        {
            OnDisabling();

            if (TargetAsMonoBinder && IdProperty is not null && ViewProperty is not null)
            {
                var view = ViewProperty.objectReferenceValue;
              
                if (view&& !string.IsNullOrWhiteSpace(IdProperty.stringValue))
                {
                    // TODO Aspid.MVVM – Delete MonoView
                    if (view is not MonoView monoView) throw new NullReferenceException(nameof(monoView));
                    ViewAndMonoBinderSyncValidator.ValidateView(monoView);
                }
            }
            
            OnDisabled();
        }
        
        protected virtual void OnDisabling() { }
        
        protected virtual void OnDisabled() { }
        #endregion

        protected virtual void FindProperties()
        {
            IdProperty = serializedObject.FindProperty("__id");
            ViewProperty = serializedObject.FindProperty("__view");
            ModeProperty = serializedObject.FindProperty("_mode");
             
            LogsProperty = serializedObject.FindProperty("_log");
            IsDebugProperty = serializedObject.FindProperty("_isDebug");
        }

        #region CreateInspectorGUI
        public sealed override VisualElement CreateInspectorGUI()
        {
            var monoBinderVisualElement = BuildVisualElement();
            monoBinderVisualElement.Initialize();

            Root = monoBinderVisualElement;
            OnCreatedInspectorGUI(monoBinderVisualElement);
            
            return monoBinderVisualElement;
        }
        
        protected virtual MonoBinderVisualElement BuildVisualElement() => new(this);

        protected virtual void OnCreatedInspectorGUI(MonoBinderVisualElement root)
        {
            var idDropdown = Root.IdDropdown;
            var viewDropdown = Root.ViewDropdown;
            
            root.RegisterCallback<SerializedPropertyChangeEvent>(e =>
            {
                var dropdownDataId = DropdownData.CreateIdDropdownData(this);
                var dropdownDataView = DropdownData.CreateViewDropdownData(this);

                var idDropdown = root.IdDropdown;
                var viewDropdown = root.ViewDropdown;
                
                if (idDropdown.choices[idDropdown.index] != dropdownDataId.Choices[dropdownDataId.Index]
                    || viewDropdown.choices[viewDropdown.index] != dropdownDataView.Choices[dropdownDataView.Index])
                {
                    idDropdown.choices = dropdownDataId.Choices;
                    viewDropdown.choices = dropdownDataView.Choices;
                    
                    idDropdown.index = dropdownDataId.Index;
                    viewDropdown.index = dropdownDataView.Index;
                }
                
                root.Update();
            });
            
            idDropdown.RegisterValueChangedCallback(value =>
            {
                using (SyncerView.Sync(this))
                {
                    SaveId(value.newValue);
                    root.Update();
                }
            });
            
            viewDropdown.RegisterValueChangedCallback(value =>
            {
                using (SyncerView.Sync(this))
                {
                    SaveView(value.newValue);
        
                    IdProperty.stringValue = string.Empty;
                    var data = DropdownData.CreateIdDropdownData(this);
                    idDropdown.choices = data.Choices;
                    idDropdown.index = data.Index;
        
                    SaveId(idDropdown.value);
                    root.Update();
                }
            });
        }
        #endregion

        private void Validate()
        {
            serializedObject.Update();
            {
                ValidateView();
                ValidateId();
            }
            serializedObject.ApplyModifiedProperties();
            return;

            void ValidateView()
            {
                if (!ViewProperty.objectReferenceValue) return;
		        
                for (var parent = ((Component)target).transform; parent is not null; parent = parent.parent)
                {
                    if (parent.GetComponents<IView>().Any(view => ViewProperty.objectReferenceValue == view as Object)) return;
                }

                ViewProperty.objectReferenceValue = null;
            }

            void ValidateId()
            {
                if (string.IsNullOrWhiteSpace(IdProperty.stringValue)) return;

                // TODO Aspid.MVVM – Fix
                var componentView = ViewProperty.objectReferenceValue;
                if (componentView && componentView is IView view && view.TryGetRequireBinderFieldsById(IdProperty.stringValue, out var field))
                {
                    if (field.FieldType.IsArray)
                    {
                        foreach (var item in (IEnumerable)field.GetValue(field.FieldContainerObj))
                        {
                            if ((MonoBinder)item == TargetAsMonoBinder)
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        if ((MonoBinder)field.GetValue(field.FieldContainerObj) == TargetAsMonoBinder) return;
                    }
                }
                
                IdProperty.stringValue = null;
            }
        }
        
        #region Save Methods
        private void SaveId(string id)
        {
            serializedObject.UpdateIfRequiredOrScript();
            {
                IdProperty.stringValue = id == "No Id" ? string.Empty : id;
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void SaveView(string viewName)
        {
            serializedObject.UpdateIfRequiredOrScript();
            {
                ViewProperty.objectReferenceValue = null;
                var views = BinderEditorUtilities.GetViews(TargetAsMonoBinder)
                    .Where(view => view.name == viewName);
                
                foreach (var view in views)
                {
                    ViewProperty.objectReferenceValue = view.view as Object;
                    break;
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
        #endregion

        protected readonly ref struct SyncerView
        {
            private readonly string _previousId;
            private readonly MonoView _previousView;
            private readonly MonoBinderEditor _editor;

            private SyncerView(MonoBinderEditor editor)
            {
                _editor = editor;
                _previousId = _editor.IdProperty.stringValue;
                _previousView = _editor.ViewProperty.objectReferenceValue as MonoView;
            }

            public static SyncerView Sync(MonoBinderEditor editor) => new(editor);

            public void Dispose()
            {
                var binder = _editor.TargetAsMonoBinder;
                var id = _editor.IdProperty.stringValue;
                var view = _editor.ViewProperty.objectReferenceValue;

                if (_previousView == view && _previousId == id) return;

                if (_previousView && !string.IsNullOrWhiteSpace(_previousId))
                    ViewAndMonoBinderSyncValidator.RemoveBinderIfExistInView(binder, _previousView, _previousId);

                if (view && !string.IsNullOrWhiteSpace(id))
                    ViewAndMonoBinderSyncValidator.SetBinderIfNotExistInView(binder);
            }
        }
    }
}
#endif
