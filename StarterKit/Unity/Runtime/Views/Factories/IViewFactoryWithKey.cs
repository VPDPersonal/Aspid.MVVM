#nullable enable

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public interface IViewFactoryWithKey<TView> : IViewFactoryRelease<TView>
        where TView : IView
    {
        public TView Create<TKey>(IViewModel? viewModel, TKey key);
    }
    
    public interface IViewFactoryWithKey<in T, TView> : IViewFactoryRelease<TView>
        where TView : IView
    {
        public TView Create<TKey>(IViewModel? viewModel, TKey key, T param);
    }
    
    public interface IViewFactoryWithKey<in T1, in T2, TView> : IViewFactoryRelease<TView>
        where TView : IView
    {
        public TView Create<TKey>(IViewModel? viewModel, TKey key, T1 param1, T2 param2);
    }
    
    public interface IViewFactoryWithKey<in T1, in T2, in T3, TView> : IViewFactoryRelease<TView>
        where TView : IView
    {
        public TView Create<TKey>(IViewModel? viewModel, TKey key, T1 param1, T2 param2, T3 param3);
    }
}