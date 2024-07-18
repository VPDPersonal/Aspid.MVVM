// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM
{
    public interface ITargetBinding { }
    
    public interface ITargetBinding<in T> : ITargetBinding
    {
        public void SetValue(T value);
    }
}