#if UNITY_EDITOR && !ASPID_UI_EDITOR_DISABLED
#nullable disable
using System;
using UnityEngine;
using Aspid.UI.MVVM.Views;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.Mono
{
    public abstract partial class MonoBinder : IMonoBinderValidable
    {
        [SerializeField] private MonoView _view;
        [SerializeField] private string _id;
        
        private IViewModel _viewModel;

        /// <summary>
        /// Is there a component?
        /// </summary>
        public bool IsMonoExist => this;
        
        /// <summary>
        /// The View to which the Binder relates.
        /// (Editor only).
        /// </summary>
        public IView View
        {
            get => _view;
            set
            {
                if (_view == value as MonoView) return;
                
                _view = value switch
                {
                    null => null,
                    MonoView view => view,
                    _ => throw new ArgumentException("View is not a MonoView")
                };

                SaveBinderDataInEditor();
            }
        }
        
        /// <summary>
        /// The ID that must correspond to the name of any ViewModel property.
        /// (Editor only).
        /// </summary>
        public string Id
        {
            get => _id;
            set
            {
                if (_id == value) return;
                
                _id = value;
                SaveBinderDataInEditor();
            }
        }

        partial void OnBindingDebug(IViewModel viewModel, string id)
        {
            if (Id != id) throw new Exception($"Id not match. Binder Id {Id}; Id {id}.");
            if (_viewModel is not null) throw new Exception("Binder has already been bound");
            
            _viewModel = viewModel;
        }

        partial void OnUnbindingDebug() => _viewModel = null;

        private void SaveBinderDataInEditor()
        {
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
        }
    }
}
#endif