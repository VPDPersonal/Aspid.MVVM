#if !ASPID_MVVM_EDITOR_DISABLED
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using System.Collections.Generic;
using UnityEngine;

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
        
        protected override void OnCreatedInspectorGUI(ViewVisualElement<TMonoView, TEditor> root)
        {
            base.OnCreatedInspectorGUI(root);

            root.RegisterCallback<SerializedPropertyChangeEvent>(_ =>
            {
                var binders = ValidableBindersById.GetValidableBindersById(TargetAsSpecific);
                ViewAndMonoBinderSyncValidator.ValidateViewChanges(TargetAsSpecific, LastBinders, binders);
                LastBinders = binders;

                root.Update();
            });
        }

        protected virtual void Validate()
        {
            if (TargetAsSpecific)
                ViewAndMonoBinderSyncValidator.ValidateView(TargetAsSpecific);
        }
    }
}
#endif