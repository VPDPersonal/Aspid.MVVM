using UnityEngine;
using Aspid.MVVM.Mono.Views;
using Aspid.MVVM.ViewModels;
using Aspid.MVVM.TodoList.Views;
using Aspid.MVVM.TodoList.Models;
using Aspid.MVVM.TodoList.ViewModels;
using Aspid.MVVM.ViewModels.Extensions;
using Aspid.MVVM.Mono.Views.Extensions;

namespace Aspid.MVVM.TodoList.Factories
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