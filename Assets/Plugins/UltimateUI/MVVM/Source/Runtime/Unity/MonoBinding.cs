#nullable disable
using System;
using UnityEngine;
using System.ComponentModel;
using UltimateUI.MVVM.Unity.Views;
using UltimateUI.MVVM.Views;
using UnityEngine.Profiling;
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
using Unity.Profiling;
#endif

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Unity
{
    public abstract class MonoBinding : MonoBehaviour, IBinder
    {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly ProfilerMarker _bindMarker = new("MonoBinding.Bind");
        private static readonly ProfilerMarker _unbindMarker = new("MonoBinding.Unbind");
        
        private static readonly ProfilerMarker _getMarker = new("MonoBinding.GetValue");
        private static readonly ProfilerMarker _setMarker = new("MonoBinding.SetValue");
#endif
        
        [Header("Binding")]
        [SerializeField] private PropertyPath _path;
#if UNITY_EDITOR
        [SerializeField] private MonoView _view;
#endif
        
        private Action _setValue;
        private Action _getValue;
        private INotifyPropertyChanged _notify;

        protected virtual void OnDestroy() => Unbind();

        public void Bind<T>(in BindData<T> data)
        {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_bindMarker.Auto()) 
#endif     
            {
                if (data.IsRead && this is ITargetBinding<T> targetBinding)
                {
                    var getter = data.Getter!;
                    
                    _getValue = () => targetBinding.SetValue(getter());
                    _getValue.Invoke();
                    
                    _notify = data.Notify;
                    SubscribeNotify();
                }
                
                if (data.IsWrite && this is ISourceBinding<T> sourceBinding)
                {
                    var setter = data.Setter!;
                    _setValue = () => setter(sourceBinding.GetValue());
                }
                
                OnBound();
            }
        }

        protected virtual void OnBound() { }

        public void Unbind()
        {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_unbindMarker.Auto()) 
#endif
            {
                _getValue = null;
                _setValue = null;
                
                UnsubscribeNotify();
                _notify = null;

                OnUnbound();
            }
        }
        
        protected virtual void OnUnbound() { }

        // TODO Rename?
        protected void UpdateValue() 
        {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_setMarker.Auto()) 
#endif
            {
                _setValue?.Invoke();
            }
        }

        private void SubscribeNotify()
        {
            if (_notify != null)
                _notify.PropertyChanged += OnPropertyChanged;
        }
        
        private void UnsubscribeNotify()
        {
            if (_notify != null)
                _notify.PropertyChanged -= OnPropertyChanged;
        }
        
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_getMarker.Auto()) 
#endif
            {
                if (args.PropertyName == _path.Name)
                    _getValue?.Invoke();
            }
        }
    }
}