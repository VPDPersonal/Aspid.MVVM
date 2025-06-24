#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.Unity;
using Object = UnityEngine.Object;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public sealed class PrefabViewFactory : PrefabViewFactory<MonoView>, IViewFactoryMonoView
    {
        public PrefabViewFactory() { }
        
        public PrefabViewFactory(MonoView prefab)
            : base(prefab) { }
    }
    
    [Serializable]
    public class PrefabViewFactory<T> : IViewFactory<T>, IViewFactory<Transform, T>
        where T : MonoBehaviour, IView
    {
        [SerializeField] private T _prefab;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public PrefabViewFactory() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        
        public PrefabViewFactory(T prefab)
        {
            _prefab = prefab;
        }

        public T Create(IViewModel? viewModel) =>
            Create(viewModel, null);

        public T Create(IViewModel? viewModel, Transform? container)
        {
            var view = Object.Instantiate(_prefab, container);
            
            if (viewModel is not null)
                view.Initialize(viewModel);

            return view;
        }

        public void Release(T view) =>
            view.DestroyView();
    }
}