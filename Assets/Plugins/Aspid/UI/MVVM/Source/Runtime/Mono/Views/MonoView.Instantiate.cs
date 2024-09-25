using UnityEngine;
using Aspid.UI.MVVM.Views;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.Mono.Views
{
    public abstract partial class MonoView
    {
        public static T Instantiate<T>(
            T original, 
            IViewModel viewModel)
            where T : Object, IView
        {
            var view = Object.Instantiate(original); 
            view.Initialize(viewModel);

            return view;
        }

        public static T Instantiate<T>(
            T original, 
            Transform parent,
            IViewModel viewModel) 
            where T : Object, IView
        {
            return Instantiate(original, parent, false, viewModel);
        }
        
        public static T Instantiate<T>(
            T original,
            Transform parent,
            bool worldPositionStays,
            IViewModel viewModel) 
            where T : Object, IView
        {
            var view = Object.Instantiate(original, parent, worldPositionStays);
            view.Initialize(viewModel);

            return view;
        }

        public static T Instantiate<T>(
            T original,
            Vector3 position,
            Quaternion rotation, 
            IViewModel viewModel)
            where T : Object, IView
        {
            var view = Object.Instantiate(original, position, rotation);
            view.Initialize(viewModel);

            return view;
        }
        
        public static T Instantiate<T>(
            T original,
            Vector3 position, 
            Quaternion rotation,
            Transform parent,
            IViewModel viewModel)
            where T : Object, IView
        {
            var view = Object.Instantiate(original, position, rotation, parent);
            view.Initialize(viewModel);

            return view;
        }
    }
}