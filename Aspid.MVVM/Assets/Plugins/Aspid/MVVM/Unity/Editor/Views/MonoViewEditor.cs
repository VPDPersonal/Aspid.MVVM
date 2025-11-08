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
    public sealed class MonoViewEditor : MonoViewEditor<MonoView, MonoViewEditor>
    {
        protected override ViewVisualElement<MonoView, MonoViewEditor> BuildVisualElement() =>
            new MonoViewVisualElement(this);
    }
    
    public abstract class MonoViewEditor<TMonoView, TEditor> : ViewEditor<TMonoView, TEditor>
        where TMonoView : MonoView
        where TEditor : MonoViewEditor<TMonoView, TEditor>  
    {
        public IEnumerable<IMonoBinderValidable> UnassignedBinders => TargetAsSpecificView
            .GetComponentsInChildren<IMonoBinderValidable>(true)
            .Where(binder => binder.View is null || string.IsNullOrWhiteSpace(binder.Id));

        protected ValidableBindersById LastBinders { get; private set; }

        #region Enable Methods
        private void OnEnable()
        {
            OnEnabling();
            Validate();
            LastBinders = ValidableBindersById.GetValidableBindersById(TargetAsSpecificView);
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

        protected override void OnSerializedPropertyChange(SerializedPropertyChangeEvent e)
        {
            var binders = ValidableBindersById.GetValidableBindersById(TargetAsSpecificView);
            ViewAndMonoBinderSyncValidator.ValidateViewChanges(TargetAsSpecificView, LastBinders, binders);
            LastBinders = binders;

            base.OnSerializedPropertyChange(e);
        }

        protected virtual void Validate()
        {
            if (TargetAsSpecificView)
                ViewAndMonoBinderSyncValidator.ValidateView(TargetAsSpecificView);
        }
    }
}
#endif