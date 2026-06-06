// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public interface IViewFactoryRelease<in T>
        where T : IView
    {
        public void Release(T view);
    }
}