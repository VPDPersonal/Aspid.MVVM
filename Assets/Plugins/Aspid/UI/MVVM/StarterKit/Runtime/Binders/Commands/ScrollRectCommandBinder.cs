using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.Commands;

namespace Aspid.UI.MVVM.StarterKit.Binders.Commands
{
    [Serializable]
    public sealed class ScrollRectCommandBinder : Binder, IBinder<IRelayCommand<Vector2>>
    {
        [SerializeField] private ScrollRect _scrollRect;
        
        private IRelayCommand<Vector2> _command;
        
        public override bool IsBind => _scrollRect is not null;
        
        private ScrollRectCommandBinder() { }
        
        public ScrollRectCommandBinder(ScrollRect scrollRect)
        {
            _scrollRect = scrollRect;
        }
        
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
            if (_command is not null) Unsubscribe();
            _command = null;
        }
    }

    [Serializable]
    public class ScrollRectCommandBinder<T> : Binder, IBinder<IRelayCommand<Vector2, T>>
    {
        [SerializeField] private ScrollRect _scrollRect;
        
        [Header("Parameters")]
        [SerializeField] private T _param;
        
        private IRelayCommand<Vector2, T> _command;
        
        public T Param
        {
            get => _param;
            set => _param = value;
        }
        
        public override bool IsBind => _scrollRect is not null;
        
        private ScrollRectCommandBinder() { }
        
        public ScrollRectCommandBinder(ScrollRect scrollRect, T param)
        {
            _param = param;
            _scrollRect = scrollRect;
        }
        
        public void SetValue(IRelayCommand<Vector2, T> command)
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
            _command?.Execute(value, Param);

        protected override void OnUnbound() => ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command is not null) Unsubscribe();
            _command = null;
        }
    }
    
    [Serializable]
    public class ScrollRectCommandBinder<T1, T2> : Binder, IBinder<IRelayCommand<Vector2, T1, T2>>
    {
        [SerializeField] private ScrollRect _scrollRect;
        
        [Header("Parameters")]
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        
        private IRelayCommand<Vector2, T1, T2> _command;
        
        public T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }
        
        public T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }
        
        public override bool IsBind => _scrollRect is not null;
        
        private ScrollRectCommandBinder() { }
        
        public ScrollRectCommandBinder(ScrollRect scrollRect, T1 param1, T2 param2)
        {
            _param1 = param1;
            _param2 = param2;
            _scrollRect = scrollRect;
        }
        
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
            _command?.Execute(value, Param1, Param2);

        protected override void OnUnbound() => ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command is not null) Unsubscribe();
            _command = null;
        }
    }
    
    [Serializable]
    public class ScrollRectCommandBinder<T1, T2, T3> : Binder, IBinder<IRelayCommand<Vector2, T1, T2, T3>>
    {
        [SerializeField] private ScrollRect _scrollRect;

        [Header("Parameters")]
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        [SerializeField] private T3 _param3;
        
        private IRelayCommand<Vector2, T1, T2, T3> _command;
        
        public T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }
        
        public T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }
        
        public T3 Param3
        {
            get => _param3;
            set => _param3 = value;
        }
        
        public override bool IsBind => _scrollRect is not null;
        
        private ScrollRectCommandBinder() { }
        
        public ScrollRectCommandBinder(ScrollRect scrollRect, T1 param1, T2 param2, T3 param3)
        {
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
            _scrollRect = scrollRect;
        }
        
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
            _command?.Execute(value, Param1, Param2, Param3);

        protected override void OnUnbound() => ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command is not null) Unsubscribe();
            _command = null;
        }
    }
}