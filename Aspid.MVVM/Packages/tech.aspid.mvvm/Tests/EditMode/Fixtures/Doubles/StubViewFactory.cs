using Aspid.MVVM.StarterKit;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Creates a <see cref="StubView"/> parented under a fixed transform, for tests of view-spawning binders.
    /// </summary>
    internal sealed class StubViewFactory : IViewFactory<StubView>
    {
        private readonly Transform _parent;

        public StubViewFactory(Transform parent) => _parent = parent;

        public StubView Create(IViewModel viewModel)
        {
            var view = new GameObject("StubView").AddComponent<StubView>();

            view.transform.SetParent(_parent, worldPositionStays: false);
            view.Initialize(viewModel);

            return view;
        }

        public StubView Create<TKey>(IViewModel viewModel, TKey key) => Create(viewModel);

        public void Release(StubView view) => Object.DestroyImmediate(view.gameObject);
    }
}
