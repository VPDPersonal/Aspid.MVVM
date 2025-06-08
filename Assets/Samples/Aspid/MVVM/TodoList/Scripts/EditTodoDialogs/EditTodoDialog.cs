using System;
using UnityEngine;
using Aspid.MVVM.Unity;
using Object = UnityEngine.Object;

namespace Aspid.MVVM.TodoList.EditTodoDialogs
{
    public sealed class EditTodoDialog
    {
        private readonly Transform _parent;
        private readonly EditTodoDialogView _prefab;

        public EditTodoDialog(EditTodoDialogView prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
        }

        public void Open(string text, Action<string> renamed, Action cancel = null)
        {
            var view = Object.Instantiate(_prefab, _parent);
            
            cancel += () => Dispose();
            renamed += _ => Dispose();
            
            view.Initialize(new EditTodoDialogViewModel(text, renamed, cancel));
            return;
            
            void Dispose() =>
                view.DestroyView()?.DisposeViewModel();
        }
    }
}