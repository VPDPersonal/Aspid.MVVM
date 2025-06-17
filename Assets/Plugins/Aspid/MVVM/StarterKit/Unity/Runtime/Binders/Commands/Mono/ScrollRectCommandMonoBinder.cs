using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [RequireComponent(typeof(ScrollRect))]
    [AddPropertyContextMenu(typeof(ScrollRect), "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Commands/ScrollRect Binder - Command")]
    [AddComponentContextMenu(typeof(ScrollRect),"Add ScrollRect Binder/ScrollRect Binder - Command")]
    public sealed partial class ScrollRectCommandMonoBinder : ComponentMonoBinder<ScrollRect>, IBinder<IRelayCommand<Vector2>>, IBinder<IRelayCommand<Vector3>>
    {
        private IRelayCommand<Vector2> _vector2command;
        private IRelayCommand<Vector3> _vector3command;
        
        private void OnEnable() =>
            CachedComponent.onValueChanged.AddListener(Execute);

        private void OnDisable() =>
            CachedComponent.onValueChanged.RemoveListener(Execute);

        [BinderLog]
        public void SetValue(IRelayCommand<Vector2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector2command, value);

        [BinderLog]
        public void SetValue(IRelayCommand<Vector3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector3command, value);
        
        protected override void OnUnbound()
        {
            SetValue((IRelayCommand<Vector2>)null);
            SetValue((IRelayCommand<Vector3>)null);
        }
        
        private void Execute(Vector2 value)
        {
            if (_vector2command is not null) _vector2command.Execute(value);
            else _vector3command?.Execute(value);
        }
    }
}