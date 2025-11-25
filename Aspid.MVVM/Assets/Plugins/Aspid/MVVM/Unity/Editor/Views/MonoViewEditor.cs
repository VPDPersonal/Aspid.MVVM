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
        protected sealed override void OnEnable()
        {
            base.OnEnable();
            
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

        protected override void OnSerializedPropertyChanged(SerializedPropertyChangeEvent e) =>
            ValidateChangedInView();

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