using System;
using UnityEngine;
using System.Threading;
using Aspid.UI.MVVM.Views;
using Aspid.UI.MVVM.ViewModels;
using Object = UnityEngine.Object;

namespace Aspid.UI.MVVM.Mono.Views
{
    public abstract partial class MonoView
    {
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original);
            operation.completed += _ => operation.Result[0].Initialize(viewModel);
            return operation;
        }

        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            Transform parent,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, parent);
            operation.completed += _ => operation.Result[0].Initialize(viewModel);
            return operation;
        }

        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            Vector3 position,
            Quaternion rotation,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, position, rotation);
            operation.completed += _ => operation.Result[0].Initialize(viewModel);
            return operation;
        }

        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            Transform parent,
            Vector3 position,
            Quaternion rotation,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, parent, position, rotation);
            operation.completed += _ => operation.Result[0].Initialize(viewModel);
            return operation;
        }

        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            IViewModel viewModel) 
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, count);
            
            operation.completed += _ =>
            {
                foreach (var result in operation.Result)
                    result.Initialize(viewModel);
            };
            
            return operation;
        }

        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Transform parent,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, count, parent);
            
            operation.completed += _ =>
            {
                foreach (var result in operation.Result)
                    result.Initialize(viewModel);
            };
            
            return operation;
        }

        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Vector3 position,
            Quaternion rotation,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, count, position, rotation);
            
            operation.completed += _ =>
            {
                foreach (var result in operation.Result)
                    result.Initialize(viewModel);
            };
            
            return operation;
        }

        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            ReadOnlySpan<Vector3> positions,
            ReadOnlySpan<Quaternion> rotations,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, count, positions, rotations);
            
            operation.completed += _ =>
            {
                foreach (var result in operation.Result)
                    result.Initialize(viewModel);
            };
            
            return operation;
        }

        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Transform parent,
            Vector3 position,
            Quaternion rotation,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, count, parent, position, rotation);
            
            operation.completed += _ =>
            {
                foreach (var result in operation.Result)
                    result.Initialize(viewModel);
            };
            
            return operation;
        }

        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Transform parent,
            Vector3 position,
            Quaternion rotation,
            IViewModel viewModel,
            CancellationToken cancellationToken)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, count, parent, position, rotation, cancellationToken);
            
            operation.completed += _ =>
            {
                foreach (var result in operation.Result)
                    result.Initialize(viewModel);
            };
            
            return operation;
        }

        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Transform parent,
            ReadOnlySpan<Vector3> positions,
            ReadOnlySpan<Quaternion> rotations,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, count, parent, positions, rotations);
            
            operation.completed += _ =>
            {
                foreach (var result in operation.Result)
                    result.Initialize(viewModel);
            };
            
            return operation;
        }

        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Transform parent,
            ReadOnlySpan<Vector3> positions,
            ReadOnlySpan<Quaternion> rotations,
            IViewModel viewModel,
            CancellationToken cancellationToken)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, count, parent, positions, rotations, cancellationToken);
            
            operation.completed += _ =>
            {
                foreach (var result in operation.Result)
                    result.Initialize(viewModel);
            };
            
            return operation;
        }
    }
}