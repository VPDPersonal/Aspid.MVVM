// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM
{
    public interface ISourceBinding { }
    
    public interface ISourceBinding<out T> : ISourceBinding
    {
        public T GetValue();
    }
}