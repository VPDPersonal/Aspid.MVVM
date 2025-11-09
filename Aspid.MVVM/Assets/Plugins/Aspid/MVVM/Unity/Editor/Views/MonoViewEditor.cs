#if !ASPID_MVVM_EDITOR_DISABLED
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Write summary
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoView), editorForChildClasses: true)]
    public sealed class MonoViewEditor : MonoViewEditor<MonoView, MonoViewEditor>
    {
        protected override ViewVisualElement<MonoView, MonoViewEditor> BuildVisualElement() =>
            new MonoViewVisualElement(this);
    }
    
    // TODO Aspid.MVVM Unity – Write summary
    public abstract class MonoViewEditor<TMonoView, TEditor> : ViewEditor<TMonoView, TEditor>
        where TMonoView : MonoView
        where TEditor : MonoViewEditor<TMonoView, TEditor>  
    {
        public IEnumerable<IMonoBinderValidable> UnassignedBinders => TargetAsView
            .GetComponentsInChildren<IMonoBinderValidable>(true)
            .Where(binder => binder.View is null || string.IsNullOrWhiteSpace(binder.Id));

        protected ValidableBindersById LastBinders { get; private set; }

        #region Enable Methods
        private void OnEnable()
        {
            OnEnabling();
            {
                ValidateView();
                LastBinders = ValidableBindersById.GetValidableBindersById(TargetAsView);
            }
            OnEnabled();
        }

        protected virtual void OnEnabling() { }

        protected virtual void OnEnabled() { }
        #endregion

        #region Disable Methods
        private void OnDisable()
        {
            OnDisabling();
            {
                ValidateView();
            }
            OnDisabled();
        }

        protected virtual void OnDisabling() { }

        protected virtual void OnDisabled() { }
        #endregion

        protected override void OnSerializedPropertyChanged(SerializedPropertyChangeEvent e)
        {
            ValidateChangedInView();
            base.OnSerializedPropertyChanged(e);
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
    }
}
#endif