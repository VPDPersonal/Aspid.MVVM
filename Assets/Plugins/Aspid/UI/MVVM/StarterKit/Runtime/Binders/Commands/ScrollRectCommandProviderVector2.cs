using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.Commands;
using Aspid.UI.MVVM.Mono.Generation;

namespace Aspid.UI.MVVM.StarterKit.Binders.Commands
{
    [Serializable]
    public sealed partial class ScrollRectCommandProviderVector2 : Binder, IBinder<IRelayCommand<Vector2>>
    {
        [SerializeField] private ScrollRect _scrollRect;
        
        private IRelayCommand<Vector2> _command;
        
        public override bool IsBind => _scrollRect != null;
        
        [BinderLog]
        public void SetValue(IRelayCommand<Vector2> command)
        {
            ReleaseCommand();
            _command = command;
            
            Subscribe();
        }
        
        private void Subscribe() =>
            _scrollRect.onValueChanged.AddListener(Execute);

        private void Unsubscribe() =>
            _scrollRect.onValueChanged.RemoveListener(Execute);
        
        private void Execute(Vector2 value) =>
            _command?.Execute(value);

        protected override void OnUnbound() => ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
    }

    [Serializable]
    public sealed partial class ScrollRectCommandProviderVector2<T1> : Binder, IBinder<IRelayCommand<Vector2, T1>>
    {
        [SerializeField] private ScrollRect _scrollRect;
        
        [Header("Parameters")]
        [SerializeField] private T1 _parameter1;
        
        private IRelayCommand<Vector2, T1> _command;
        
        public T1 Parameter1
        {
            get => _parameter1;
            set => _parameter1 = value;
        }
        
        public override bool IsBind => _scrollRect != null;
        
        [BinderLog]
        public void SetValue(IRelayCommand<Vector2, T1> command)
        {
            ReleaseCommand();            
            _command = command;
            
            Subscribe();
        }
        
        private void Subscribe() =>
            _scrollRect.onValueChanged.AddListener(Execute);

        private void Unsubscribe() =>
            _scrollRect.onValueChanged.RemoveListener(Execute);
        
        private void Execute(Vector2 value) =>
            _command?.Execute(value, Parameter1);

        protected override void OnUnbound() => ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
    }
    
    [Serializable]
    public sealed partial class ScrollRectCommandProviderVector2<T1, T2> : Binder, IBinder<IRelayCommand<Vector2, T1, T2>>
    {
        [SerializeField] private ScrollRect _scrollRect;
        
        [Header("Parameters")]
        [SerializeField] private T1 _parameter1;
        [SerializeField] private T2 _parameter2;
        
        private IRelayCommand<Vector2, T1, T2> _command;
        
        public T1 Parameter1
        {
            get => _parameter1;
            set => _parameter1 = value;
        }
        
        public T2 Parameter2
        {
            get => _parameter2;
            set => _parameter2 = value;
        }
        
        public override bool IsBind => _scrollRect != null;
        
        [BinderLog]
        public void SetValue(IRelayCommand<Vector2, T1, T2> command)
        {
            ReleaseCommand();            
            _command = command;
            
            Subscribe();
        }
        
        private void Subscribe() =>
            _scrollRect.onValueChanged.AddListener(Execute);

        private void Unsubscribe() =>
            _scrollRect.onValueChanged.RemoveListener(Execute);
        
        private void Execute(Vector2 value) =>
            _command?.Execute(value, Parameter1, Parameter2);

        protected override void OnUnbound() => ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
    }
    
    [Serializable]
    public sealed partial class ScrollRectCommandProviderVector2<T1, T2, T3> : Binder, IBinder<IRelayCommand<Vector2, T1, T2, T3>>
    {
        [SerializeField] private ScrollRect _scrollRect;

        [Header("Parameters")]
        [SerializeField] private T1 _parameter1;
        [SerializeField] private T2 _parameter2;
        [SerializeField] private T3 _parameter3;
        
        private IRelayCommand<Vector2, T1, T2, T3> _command;
        
        public T1 Parameter1
        {
            get => _parameter1;
            set => _parameter1 = value;
        }
        
        public T2 Parameter2
        {
            get => _parameter2;
            set => _parameter2 = value;
        }
        
        public T3 Parameter3
        {
            get => _parameter3;
            set => _parameter3 = value;
        }
        
        public override bool IsBind => _scrollRect != null;
        
        [BinderLog]
        public void SetValue(IRelayCommand<Vector2, T1, T2, T3> command)
        {
            ReleaseCommand();            
            _command = command;
            
            Subscribe();
        }
        
        private void Subscribe() =>
            _scrollRect.onValueChanged.AddListener(Execute);

        private void Unsubscribe() =>
            _scrollRect.onValueChanged.RemoveListener(Execute);
        
        private void Execute(Vector2 value) =>
            _command?.Execute(value, Parameter1, Parameter2, Parameter3);

        protected override void OnUnbound() => ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
    }
}