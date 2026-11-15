#nullable enable
using System;
using UnityEngine;
using System.Threading;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public partial class MonoView
    {
        private static T InitializeInstance<T>(T view, IViewModel viewModel)
            where T : Object, IView
        {
            try
            {
                view.Initialize(viewModel);
                return view;
            }
            catch
            {
                RollbackInstance(view);
                throw;
            }
        }

        private static void InitializeOnCompletion<T>(
            AsyncInstantiateOperation<T> operation,
            IViewModel viewModel,
            CancellationToken cancellationToken = default)
            where T : Object, IView
        {
            operation.completed += _ =>
            {
                var views = operation.Result;
                
                if (views is null || views.Length is 0) return;
                if (cancellationToken.IsCancellationRequested)
                {
                    RollbackInstances(views);
                    return;
                }

                try
                {
                    foreach (var view in views)
                    {
                        if (!view)
                            throw new InvalidOperationException("An instantiated view was destroyed before initialization.");

                        view.Initialize(viewModel);
                    }
                }
                catch
                {
                    RollbackInstances(views);
                    throw;
                }
            };
        }

        private static void RollbackInstance<T>(T view)
            where T : Object, IView
        {
            try
            {
                view.DestroyViewAndGameObject();
            }
            catch
            {
                // Preserve the initialization failure and continue rolling back the group.
            }
        }

        private static void RollbackInstances<T>(T[] views)
            where T : Object, IView
        {
            foreach (var view in views)
                RollbackInstance(view);
        }
    }
}
