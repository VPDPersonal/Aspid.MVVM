#nullable enable

namespace Aspid.MVVM.StarterKit.Unity
{
    public interface IViewFactory<T> : IViewFactoryRelease<T>
        where T : IView
    {
        public T Create(IViewModel? viewModel);
    }
    
    public interface IViewFactory<in T, TView> : IViewFactoryRelease<TView>
        where TView : IView
    {
        public TView Create(IViewModel? viewModel, T? param);
    }
    
    public interface IViewFactory<in T1, in T2, TView> : IViewFactoryRelease<TView>
        where TView : IView
    {
        public TView Create(IViewModel? viewModel, T1? param1, T2? param2);
    }
    
    public interface IViewFactory<in T1, in T2, in T3, TView> : IViewFactoryRelease<TView>
        where TView : IView
    {
        public TView Create(IViewModel? viewModel, T1? param1, T2? param2, T3? param3);
    }
}