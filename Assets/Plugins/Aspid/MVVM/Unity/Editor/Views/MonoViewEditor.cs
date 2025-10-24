#if !ASPID_MVVM_EDITOR_DISABLED
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoView), editorForChildClasses: true)]
    public class MonoViewEditor : ViewEditor<MonoView, MonoViewEditor>
    {
        public IEnumerable<IMonoBinderValidable> UnassignedBinders => TargetAsSpecific
            .GetComponentsInChildren<IMonoBinderValidable>(true)
                .Where(binder => binder.View is null || string.IsNullOrWhiteSpace(binder.Id));
        
        protected ValidableBindersById LastBinders { get; private set; }

        #region Enable Methods
        private void OnEnable()
        {
            OnEnabling();
            Validate();
            LastBinders = ValidableBindersById.GetValidableBindersById(TargetAsSpecific);
            OnEnabled();
        }
        
        protected virtual void OnEnabling() { }
        
        protected virtual void OnEnabled() { }
        #endregion

        #region Disable Methods
        private void OnDisable()
        {
            OnDisabling();
            Validate();
            OnDisabled();
        }
        
        protected virtual void OnDisabling() { }
        
        protected virtual void OnDisabled() { }
        #endregion
        
        #region CreateInspectorGUI
        protected override ViewVisualElement<MonoView, MonoViewEditor> BuildVisualElement() =>
            new MonoViewVisualElement(this);
       
        protected override void OnCreatedInspectorGUI(ViewVisualElement<MonoView, MonoViewEditor> root)
        {
            base.OnCreatedInspectorGUI(root);
            
            root.RegisterCallback<SerializedPropertyChangeEvent>(_ =>
            {
                var binders = ValidableBindersById.GetValidableBindersById(TargetAsSpecific);
                ViewUtility.ValidateViewChanges(TargetAsSpecific, LastBinders, binders);
                LastBinders = binders;
                
                root.Update();
            });
        }
        #endregion
        
        protected virtual void Validate()
        {
            if (TargetAsSpecific)
                ViewUtility.ValidateView(TargetAsSpecific);
        }
    }
}
#endif