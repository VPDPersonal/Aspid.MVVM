#if !ASPID_MVVM_EDITOR_DISABLED
using System;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoView), editorForChildClasses: true)]
    public sealed class MonoViewEditor : MonoViewEditor<MonoView, MonoViewEditor>
    {
        protected override ViewVisualElement<MonoView, MonoViewEditor> BuildVisualElement() =>
            new MonoViewVisualElement(this);
    }
    
    public abstract class MonoViewEditor<TMonoView, TEditor> : ViewEditor<TMonoView, TEditor>
        where TMonoView : MonoView
        where TEditor : MonoViewEditor<TMonoView, TEditor>  
    {
        public BinderListProperty BindersList { get; private set; }
        
        public SerializedProperty DesignViewModel { get; private set; }
        
        protected ValidableBindersById LastBinders { get; private set; }
        
        public IEnumerable<IMonoBinderValidable> UnassignedBinders => TargetAsView?
            .GetComponentsInChildren<IMonoBinderValidable>(includeInactive: true)
            .Where(binder => binder.View is null || string.IsNullOrWhiteSpace(binder.Id))
            ?? Enumerable.Empty<IMonoBinderValidable>();

        #region Enable Methods
        protected sealed override void OnEnable()
        {
            base.OnEnable();
            
            OnEnabling();
            {
                ValidateView();
                
                BindersList = new BinderListProperty(serializedObject);
                DesignViewModel = serializedObject.FindProperty("_designViewModel");
                LastBinders = ValidableBindersById.GetValidableBindersById(TargetAsView);
            }
            OnEnabled();
        }

        protected virtual void OnEnabling() { }

        protected virtual void OnEnabled() { }
        #endregion

        #region Disable Methods
        protected sealed override void OnDisable()
        {
            base.OnDisable();
            
            OnDisabling();
            {
                ValidateView();
            }
            OnDisabled();
        }

        protected virtual void OnDisabling() { }

        protected virtual void OnDisabled() { }
        #endregion
        
        protected override void Update()
        {
            base.Update();
            
            // TODO Aspid.MVVM Refactor
            // This is a temp solution.
            {
                var binders = ValidableBindersById.GetValidableBindersById(TargetAsView);
                
                if (LastBinders is null)
                {
                    LastBinders = binders;
                    return;
                }

                var areEqual = binders.Count == LastBinders.Count && binders.All(pair =>
                        LastBinders.ContainsKey(pair.Key) &&
                        pair.Value.SequenceEqual(LastBinders[pair.Key]));

                if (!areEqual)
                {
                    LastBinders = binders;
                }
            }
        }
        
        protected override void OnCreatingInspectorGUI() =>
            UpdateMetaData();
        
        protected override void OnGeometryChangedEventOnce(GeometryChangedEvent e) =>
            ((MonoViewVisualElement<TMonoView, TEditor>)Root).UpdateGeneralBinders();

        protected override void OnSerializedPropertyChanged(SerializedPropertyChangeEvent e)
        {
            ValidateChangedInView();
            
            if (e.changedProperty.propertyPath == DesignViewModel.propertyPath)
            {
                UpdateMetaData();
                ((MonoViewVisualElement<TMonoView, TEditor>)Root).UpdateGeneralBinders();
            }
        }

        protected virtual void ValidateView()
        {
            if (TargetAsView)
                ViewAndMonoBinderSyncValidator.ValidateView(TargetAsView);
        }

        protected void ValidateChangedInView()
        {
            var binders = ValidableBindersById.GetValidableBindersById(TargetAsView);
            ViewAndMonoBinderSyncValidator.ValidateViewChanges(TargetAsView, LastBinders, binders);
            LastBinders = binders;
        }
        
        private void UpdateMetaData()
        {
            var viewModelType = string.IsNullOrWhiteSpace(DesignViewModel.stringValue) 
                ? null
                : Type.GetType(DesignViewModel.stringValue);
            
            if (viewModelType is null)
            {
                BindersList.ArraySize = 0;
                return;
            }

            var viewModelMeta = new ViewModelMeta(viewModelType);
            var viewMeta = new ViewMeta(TargetAsView.GetType());

            var bindableProperties = viewModelMeta.BindableProperties
                .Where(bindableProperty => viewMeta.BinderProperties.All(binderProperty => binderProperty.Id != bindableProperty.Id))
                .ToArray();

            BindersList.ArraySize = bindableProperties.Length;
                
            for (var i = 0; i < bindableProperties.Length; i++)
            {
                var element = BindersList.GetArrayElementAtIndex(i);
                    
                element.Id = bindableProperties[i].Id;
                element.AssemblyQualifiedName = bindableProperties[i].Type?.AssemblyQualifiedName;
            }
        }
    }
}
#endif