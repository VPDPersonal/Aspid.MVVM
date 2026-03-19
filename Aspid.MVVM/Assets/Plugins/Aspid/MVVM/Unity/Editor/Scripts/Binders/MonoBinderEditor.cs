#if !ASPID_MVVM_EDITOR_DISABLED
using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Custom Unity Editor for <see cref="MonoBinder"/> components.
    /// Builds a UIElements-based inspector with ID and View dropdown selectors.
    /// Can be extended by derived editors for specialized binder types.
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoBinder), editorForChildClasses: true)]
    public class MonoBinderEditor : Editor
    {
        private string _lastId;
        private IView _lastView;
        
        public MonoBinder TargetAsMonoBinder => target as MonoBinder;

        public bool HasBinderId => 
            ViewProperty.Value is not null
            && !string.IsNullOrWhiteSpace(IdProperty.Value);
        
        #region Serialized Properties
        public MonoBinderIdProperty IdProperty { get; private set; }
        
        public MonoBinderViewProperty ViewProperty { get; private set; }
        
        public SerializedProperty ModeProperty { get; private set; }
        
        public SerializedProperty LogsProperty { get; private set; }
        
        public SerializedProperty IsDebugProperty { get; private set; }
        #endregion
        
        protected MonoBinderVisualElement Root { get; private set; }
        
        #region Enable
        private void OnEnable()
        {
            OnEnabling();
            {
                FindProperties();
                Validate();
                
                EditorApplication.update -= Update;
                EditorApplication.update += Update;
            }
            OnEnabled();
        }
        
        protected virtual void OnEnabling() { }
        
        protected virtual void OnEnabled() { }
        #endregion

        #region Disable
        private void OnDisable()
        {
            OnDisabling();
            {
                EditorApplication.update -= Update;
                
                if (IdProperty is not null && ViewProperty is not null)
                {
                    var view = ViewProperty.Value;

                    if (view is not null && string.IsNullOrEmpty(IdProperty.Value))
                    {
                        if (view is not MonoView monoView) throw new NullReferenceException(nameof(monoView));
                        ViewAndMonoBinderSyncValidator.ValidateView(monoView);
                    }
                }
            }
            OnDisabled();
        }
        
        protected virtual void OnDisabling() { }
        
        protected virtual void OnDisabled() { }
        #endregion
        
        protected virtual void Update() => 
            Root?.Update();

        protected virtual void FindProperties()
        {
            IdProperty = new MonoBinderIdProperty(serializedObject);
            ViewProperty = new MonoBinderViewProperty(serializedObject);
            ModeProperty = serializedObject.FindProperty("_mode");
             
            LogsProperty = serializedObject.FindProperty("_log");
            IsDebugProperty = serializedObject.FindProperty("_isDebug");
        }

        #region CreateInspectorGUI
        public sealed override VisualElement CreateInspectorGUI()
        {
            var root = BuildVisualElement();
            root.Initialize();
            
            OnCreatingInspectorGUI(root);
            {
                Root = root;
                
                var idDropdown = root.IdDropdown;
                var viewDropdown = root.ViewDropdown;
                
                root.RegisterCallback<SerializedPropertyChangeEvent>(e =>
                {
                    if (e.changedProperty.propertyPath != IdProperty.ValueProperty.propertyPath 
                        && e.changedProperty.propertyPath != ViewProperty.ValueProperty.propertyPath) return;
                
                    var dropdownDataId = BinderDropdownData.CreateIdDropdownData(this);
                    var dropdownDataView = BinderDropdownData.CreateViewDropdownData(this);
                
                    root.IdDropdown.choices = dropdownDataId.Choices;
                    root.ViewDropdown.choices = dropdownDataView.Choices;

                    var currentView = ViewProperty.Value;
                    var currentId = IdProperty.Value;

                    if (currentId != _lastId || currentView != _lastView)
                    {
                        if (_lastView is not null && !string.IsNullOrWhiteSpace(_lastId))
                        {
                            var ids = BinderEditorUtilities
                                .GetIds(TargetAsMonoBinder, _lastView)
                                .Select(data => data.Id);
                            
                            if (ids.Contains(_lastId)) 
                                ViewAndMonoBinderSyncValidator.RemoveBinderIfExistInView(TargetAsMonoBinder, _lastView, _lastId);
                        }
                    
                        if (currentView is not null && !string.IsNullOrWhiteSpace(currentId))
                        {
                            if (dropdownDataId.Contains(currentId))
                                ViewAndMonoBinderSyncValidator.SetBinderIfNotExistInView(TargetAsMonoBinder);
                        }
                    }
                
                    root.IdDropdown.SetValueWithoutNotify(dropdownDataId.Choices[dropdownDataId.Index]);
                    root.ViewDropdown.SetValueWithoutNotify(dropdownDataView.Choices[dropdownDataView.Index]);

                    root.UpdateDropdownColor(root.IdDropdown, dropdownDataId.HasPrevious);
                    root.UpdateDropdownColor(root.ViewDropdown, dropdownDataView.HasPrevious);

                    _lastId = IdProperty.Value;
                    _lastView = ViewProperty.Value as MonoView;
                
                    root.Update();
                });
                
                idDropdown.RegisterValueChangedCallback(value =>
                {
                    SaveId(value.newValue);

                    var data = BinderDropdownData.CreateIdDropdownData(this);
                    idDropdown.SetValueWithoutNotify(data.Choices[data.Index]);
                    root.UpdateDropdownColor(idDropdown, data.HasPrevious);

                    root.Update();
                });

                viewDropdown.RegisterValueChangedCallback(value =>
                {
                    SaveView(value.newValue);

                    var idData = BinderDropdownData.CreateIdDropdownData(this);
                    idDropdown.choices = idData.Choices;
                    idDropdown.SetValueWithoutNotify(idData.Choices[idData.Index]);

                    var viewData = BinderDropdownData.CreateViewDropdownData(this);
                    root.UpdateDropdownColor(viewDropdown, viewData.HasPrevious);
                    root.UpdateDropdownColor(idDropdown, idData.HasPrevious);

                    root.Update();
                });

                idDropdown.RegisterCallback<PointerDownEvent>(_ =>
                {
                    if (!string.IsNullOrWhiteSpace(IdProperty.PreviousValue)
                        && string.IsNullOrWhiteSpace(IdProperty.Value))
                    {
                        idDropdown.SetValueWithoutNotify(string.Empty);
                    }
                }, TrickleDown.TrickleDown);

                viewDropdown.RegisterCallback<PointerDownEvent>(_ =>
                {
                    var previousView = ViewProperty.PreviousName;
                    if (!string.IsNullOrWhiteSpace(previousView) && ViewProperty.Value is null)
                    {
                        viewDropdown.SetValueWithoutNotify(string.Empty);
                    }
                }, TrickleDown.TrickleDown);
            }
            OnCreatedInspectorGUI(root);
            
            return root;
        }
        
        protected virtual MonoBinderVisualElement BuildVisualElement() =>
            new(editor: this);

        protected virtual void OnCreatingInspectorGUI(MonoBinderVisualElement root) { }

        protected virtual void OnCreatedInspectorGUI(MonoBinderVisualElement root) { }
        #endregion

        #region Restore Methods
        public bool CanRestoreView()
        {
            if (ViewProperty.Value is not null) return false;
            
            var previousView = ViewProperty.PreviousValue;
            return previousView is not null && previousView.IsBinderInViewScope(TargetAsMonoBinder);
        }
        
        public void RestoreView()
        {
            if (!CanRestoreView()) return;

            var viewName = BinderViewData.GetViewName(ViewProperty.PreviousValue as Component);
            if (string.IsNullOrWhiteSpace(viewName)) return;
            
            SaveView(viewName);
        }
        
        public bool CanRestoreId()
        {
            if (!string.IsNullOrWhiteSpace(IdProperty.Value)) return false;
            
            var view = ViewProperty.Value;
            if (view is null) return false;

            var previousId = IdProperty.PreviousValue;
            var ids = BinderEditorUtilities.GetIds(TargetAsMonoBinder, view);
            
            return ids.Any(id => id.Id == previousId);
        }
        
        public void RestoreId()
        {
            if (!CanRestoreId()) return;
            SaveId(IdProperty.PreviousValue);
        }
        #endregion

        #region Save Methods
        private void SaveId(string id)
        {
            serializedObject.UpdateIfRequiredOrScript();
            {
                IdProperty.Value = id is "No Id" ? string.Empty : id;
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void SaveView(string viewName)
        {
            serializedObject.UpdateIfRequiredOrScript();
            {
                if (viewName is "No View")
                {
                    ViewProperty.Value = null;
                }
                else
                {
                    var view = BinderEditorUtilities
                        .GetViews(TargetAsMonoBinder)
                        .FirstOrDefault(view => view.Name == viewName);

                    ViewProperty.Value = view.View;
                }
            }
            serializedObject.ApplyModifiedProperties();

            Validate();
        }
        #endregion
        
        private void Validate()
        {
            serializedObject.Update();
            {
                ViewProperty.Validate();
                IdProperty.Validate(ViewProperty);

                if (ViewProperty?.Value is MonoView monoView)
                {
                    // Create a temporary editor for MonoView to trigger its OnEnable,
                    // which will update BindersList based on DesignViewModel
                    var viewEditor = CreateEditor(monoView);
                    if (viewEditor) DestroyImmediate(viewEditor);
                    
                    var ids = BinderEditorUtilities.GetIds(TargetAsMonoBinder, monoView)
                        .Select(data => data.Id);
                    
                    if (!string.IsNullOrWhiteSpace(IdProperty.Value) && !ids.Contains(IdProperty.Value))
                        IdProperty.Value = string.Empty;
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif