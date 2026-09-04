using System;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable CheckNamespace
namespace Aspid.MVVM.Samples.TodoList
{
    // Spawns a dialog View with its own ViewModel and destroys both when the dialog closes.
    public sealed class EditTextDialog
    {
        private readonly Transform _parent;
        private readonly EditTextDialogView _prefab;

        public EditTextDialog(EditTextDialogView prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
        }

        public void Open(string text, Action<string> renamed)
        {
            var view = Object.Instantiate(_prefab, _parent);

            var viewModel = new EditTextDialogViewModel(
                text,
                renamed: newText =>
                {
                    renamed(newText);
                    Close();
                },
                cancelled: Close);

            view.Initialize(viewModel);
            return;

            void Close() =>
                view.DestroyViewAndGameObject()?.DisposeViewModel();
        }
    }
}
