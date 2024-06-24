// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM
{
    public interface IBinder { }
    
    public interface IBinder<in T> : IBinder
    {
        public void SetValue(T value);
    }
}