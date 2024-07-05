// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM
{
    public interface IAnyBinder : IBinder
    {
        public void SetValue<T>(T value);
    }
}