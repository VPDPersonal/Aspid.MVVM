using System;
using System.ComponentModel;
using UltimateUI.MVVM.ViewModels;
#if UNITY_EDITOR && !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
using Unity.Profiling;
#endif

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM
{
    public class Binding : IBinder, IDisposable
    {
#if UNITY_EDITOR && !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly ProfilerMarker _bindMarker = new("Binding.Bind");
        private static readonly ProfilerMarker _unbindMarker = new("Binding.Unbind");
        
        private static readonly ProfilerMarker _getMarker = new("Binding.GetValue");
        private static readonly ProfilerMarker _setMarker = new("Binding.SetValue");
#endif

        private Action? _setValue;
        private Action? _getValue;
        private INotifyPropertyChanged? _notify;

        public readonly PropertyPath Path;

        public Binding(string path) : this(new PropertyPath(path)) { }
        
        public Binding(PropertyPath path)
        {
            Path = path;
        }   

        public void Bind<T>(in BindData<T> data)
        {
#if UNITY_EDITOR && !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_bindMarker.Auto()) 
#endif     
            {
                // if (data.IsRead && this is ITargetBinding<T> targetBinding)
                // {
                //     var getter = data.Getter!;
                //     _getValue = () => targetBinding.SetValue(getter());
                //     
                //     _notify = data.Notify;
                //     SubscribeNotify();
                // }
                //
                // if (data.IsWrite && this is ISourceBinding<T> sourceBinding)
                // {
                //     var setter = data.Setter!;
                //     _setValue = () => setter(sourceBinding.GetValue());
                // }
                //
                // OnBound();
            }
        }

        protected virtual void OnBound() { }

        public void Unbind()
        {
#if UNITY_EDITOR && !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
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
#if UNITY_EDITOR && !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_setMarker.Auto()) 
#endif
            {
                _setValue?.Invoke();
            }
        }

        protected void SubscribeNotify()
        {
            if (_notify != null)
                _notify.PropertyChanged += OnPropertyChanged;
        }
        
        protected void UnsubscribeNotify()
        {
            if (_notify != null)
                _notify.PropertyChanged -= OnPropertyChanged;
        }
        
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
#if UNITY_EDITOR && !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_getMarker.Auto()) 
#endif
            {
                if (args.PropertyName == Path.Name)
                    _getValue?.Invoke();
            }
        }

        public virtual void Dispose() => Unbind();
    }
}