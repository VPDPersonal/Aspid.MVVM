using UnityEngine;
using Aspid.UI.TodoList.Views;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.ViewModels;
using Aspid.UI.TodoList.Models;
using Aspid.UI.TodoList.ViewModels;
using Aspid.UI.MVVM.Mono.Views.Extensions;
using Aspid.UI.MVVM.ViewModels.Extensions;

namespace Aspid.UI.TodoList.Factories
{
    public sealed class EditTodoPopUpViewFactory
    {
        private readonly Transform _parent;
        private readonly EditTodoPopUpView _prefab;

        public EditTodoPopUpViewFactory(EditTodoPopUpView prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
        }

        public EditTodoPopUpView Create(EditTodoPopUp editTodoPopUp)
        {
            IViewModel viewModel = editTodoPopUp.ToEditTodoPopUpViewModel();
            return MonoView.Instantiate(_prefab, _parent, viewModel);
        }

        public static void Release(EditTodoPopUpView view) =>
            view.DestroyView()?.DisposeViewModel();
    }
}