using System;
using UnityEngine;
using UnityEngine.UI;
using UltimateUI.MVVM.Commands;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Commands
{
    [Serializable]
    public sealed class ScrollRectCommandProviderVector2 : Binder, IBinder<IRelayCommand<Vector2>>
    {
        [Space]
        [SerializeField] private ScrollRect[] _scrollRects;
        
        private IRelayCommand<Vector2> _command;
        
        public void SetValue(IRelayCommand<Vector2> command)
        {
            ReleaseBinding<IRelayCommand<Vector2>>();
            
            _command = command;
            
            Subscribe();
        }
        
        private void Subscribe()
        {
            foreach (var scrollRect in _scrollRects)
                scrollRect.onValueChanged.AddListener(Execute);
        }

        private void Unsubscribe()
        {
            foreach (var scrollRect in _scrollRects)
                scrollRect.onValueChanged.RemoveListener(Execute);
        }
        
        private void Execute(Vector2 value) =>
            _command?.Execute(value);

        protected override void ReleaseBinding<TBinding>()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
    }

    [Serializable]
    public sealed class ScrollRectCommandProviderVector2<T1> : Binder, IBinder<IRelayCommand<Vector2, T1>>
    {
        [SerializeField] private T1 _parameter1;
        
        [Space]
        [SerializeField] private ScrollRect[] _scrollRects;
        
        private IRelayCommand<Vector2, T1> _command;
        
        public T1 Parameter1
        {
            get => _parameter1;
            set => _parameter1 = value;
        }
        
        public void SetValue(IRelayCommand<Vector2, T1> command)
        {
            ReleaseBinding<IRelayCommand<Vector2, T1>>();
            
            _command = command;
            
            Subscribe();
        }
        
        private void Subscribe()
        {
            foreach (var scrollRect in _scrollRects)
                scrollRect.onValueChanged.AddListener(Execute);
        }

        private void Unsubscribe()
        {
            foreach (var scrollRect in _scrollRects)
                scrollRect.onValueChanged.RemoveListener(Execute);
        }
        
        private void Execute(Vector2 value) =>
            _command?.Execute(value, Parameter1);

        protected override void ReleaseBinding<TBinding>()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
    }
    
    [Serializable]
    public sealed class ScrollRectCommandProviderVector2<T1, T2> : Binder, IBinder<IRelayCommand<Vector2, T1, T2>>
    {
        [SerializeField] private T1 _parameter1;
        [SerializeField] private T2 _parameter2;
        
        [Space]
        [SerializeField] private ScrollRect[] _scrollRects;
        
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
        
        public void SetValue(IRelayCommand<Vector2, T1, T2> command)
        {
            ReleaseBinding<IRelayCommand<Vector2, T1, T2>>();
            
            _command = command;
            
            Subscribe();
        }
        
        private void Subscribe()
        {
            foreach (var scrollRect in _scrollRects)
                scrollRect.onValueChanged.AddListener(Execute);
        }

        private void Unsubscribe()
        {
            foreach (var scrollRect in _scrollRects)
                scrollRect.onValueChanged.RemoveListener(Execute);
        }
        
        private void Execute(Vector2 value) =>
            _command?.Execute(value, Parameter1, Parameter2);

        protected override void ReleaseBinding<TBinding>()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
    }
    
    [Serializable]
    public sealed class ScrollRectCommandProviderVector2<T1, T2, T3> : Binder, IBinder<IRelayCommand<Vector2, T1, T2, T3>>
    {
        [SerializeField] private T1 _parameter1;
        [SerializeField] private T2 _parameter2;
        [SerializeField] private T3 _parameter3;
        
        [Space]
        [SerializeField] private ScrollRect[] _scrollRects;
        
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
        
        public void SetValue(IRelayCommand<Vector2, T1, T2, T3> command)
        {
            ReleaseBinding<IRelayCommand<Vector2, T1, T2, T3>>();
            
            _command = command;
            
            Subscribe();
        }
        
        private void Subscribe()
        {
            foreach (var scrollRect in _scrollRects)
                scrollRect.onValueChanged.AddListener(Execute);
        }

        private void Unsubscribe()
        {
            foreach (var scrollRect in _scrollRects)
                scrollRect.onValueChanged.RemoveListener(Execute);
        }
        
        private void Execute(Vector2 value) =>
            _command?.Execute(value, Parameter1, Parameter2, Parameter3);

        protected override void ReleaseBinding<TBinding>()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
    }
}