#nullable enable

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public interface IViewFactory<TView> : IViewFactoryWithKey<TView>
        where TView : IView
    {
        TView IViewFactoryWithKey<TView>.Create<TKey>(IViewModel? viewModel, TKey key) =>
            Create(viewModel);
        
        public TView Create(IViewModel? viewModel);
    }
    
    public interface IViewFactory<in T, TView> : IViewFactoryWithKey<T, TView>
        where TView : IView
    {
        TView IViewFactoryWithKey<T, TView>.Create<TKey>(IViewModel? viewModel, TKey key, T param) =>
            Create(viewModel, param);
        
        public TView Create(IViewModel? viewModel, T? param);
    }
    
    public interface IViewFactory<in T1, in T2, TView> : IViewFactoryWithKey<T1, T2, TView>
        where TView : IView
    {
        TView IViewFactoryWithKey<T1, T2, TView>.Create<TKey>(IViewModel? viewModel, TKey key, T1 param1, T2 param2) =>
            Create(viewModel, param1, param2);
        
        public TView Create(IViewModel? viewModel, T1? param1, T2? param2);
    }
    
    public interface IViewFactory<in T1, in T2, in T3, TView> : IViewFactoryWithKey<T1, T2, T3, TView>
        where TView : IView
    {
        TView IViewFactoryWithKey<T1, T2, T3, TView>.Create<TKey>(IViewModel? viewModel, TKey key, T1 param1, T2 param2, T3 param3) =>
            Create(viewModel, param1, param2, param3);
        
        public TView Create(IViewModel? viewModel, T1? param1, T2? param2, T3? param3);
    }
}