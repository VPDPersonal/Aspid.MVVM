#if !ASPID_MVVM_EDITOR_DISABLED
using System;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Object = UnityEngine.Object;

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
        
        public IEnumerable<IMonoBinderValidable> UnassignedBinders => TargetAsView 
            ? TargetAsView.GetComponentsInChildren<IMonoBinderValidable>(includeInactive: true)
                .Where(binder => binder.View is null || string.IsNullOrWhiteSpace(binder.Id)) 
            : Enumerable.Empty<IMonoBinderValidable>();

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
                
                UpdateMetaData();
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
                for (var i = 0; i < BindersList.ArraySize; i++)
                {
                    var hasBinder = false;
                    var property = BindersList.GetArrayElementAtIndex(i);
                    var monoBindersProperty = property.MonoBindersProperty;
                    
                    for (var j = 0; j < monoBindersProperty.arraySize; j++)
                    {
                        hasBinder = monoBindersProperty.GetArrayElementAtIndex(j).objectReferenceValue;
                        if (hasBinder) break;
                    }

                    if (!hasBinder)
                        BindersList.DeleteArrayElementAtIndex(i--);
                }
                
                BindersList.ApplyModifiedProperties();
                return;
            }

            var viewModelMeta = new ViewModelMeta(viewModelType);
            var viewMeta = new ViewMeta(TargetAsView.GetType());

            var bindableProperties = viewModelMeta.BindableProperties
                .Where(bindableProperty => viewMeta.BinderProperties.All(binderProperty => binderProperty.Id != bindableProperty.Id))
                .ToArray();
            
            var fieldsById = new Dictionary<string, Object[]>();

            for (var i = 0; i < BindersList.ArraySize; i++)
            {
                var element = BindersList.GetArrayElementAtIndex(i);
                
                var array = new Object[element.MonoBindersProperty.arraySize];
                for (var j = 0; j < array.Length; j++)
                    array[j] = element.MonoBindersProperty.GetArrayElementAtIndex(j).objectReferenceValue;
                
                fieldsById.Add(element.Id, array);
            }
            
            BindersList.ArraySize = bindableProperties.Length;
                
            for (var i = 0; i < BindersList.ArraySize; i++)
            {
                var id = bindableProperties[i].Id;
                var property = BindersList.GetArrayElementAtIndex(i);
                var monoBindersProperty = property.MonoBindersProperty;

                if (fieldsById.TryGetValue(id, out var array))
                {
                    monoBindersProperty.arraySize = array.Length;

                    for (var j = 0; j < array.Length; j++)
                        monoBindersProperty.GetArrayElementAtIndex(j).objectReferenceValue = array[j];
                    
                    fieldsById.Remove(id);
                }
                else
                {
                    if (!fieldsById.ContainsKey(property.Id))
                    {
                        for (var j = 0; j < monoBindersProperty.arraySize; j++)
                        {
                            if (monoBindersProperty.GetArrayElementAtIndex(j).objectReferenceValue is IMonoBinderValidable monoBinder)
                                monoBinder.Reset();
                        }
                    }
                    
                    monoBindersProperty.arraySize = 0;
                }
                
                property.Id = id;
                property.AssemblyQualifiedName = bindableProperties[i].Type?.AssemblyQualifiedName;
            }
            
            foreach (var value in fieldsById.Values.SelectMany(values => values))
            {
                if (value is IMonoBinderValidable monoBinder)
                    monoBinder.Reset();
            }
            
            BindersList.ApplyModifiedProperties();
        }
    }
}
#endif