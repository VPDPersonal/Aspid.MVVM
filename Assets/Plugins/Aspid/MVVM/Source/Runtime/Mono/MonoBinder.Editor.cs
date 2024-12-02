#if UNITY_EDITOR && !ASPID_MVVM_EDITOR_DISABLED
#nullable disable
using System;
using UnityEngine;
using Aspid.MVVM.Views;
using Aspid.MVVM.Mono.Views;
using Aspid.MVVM.ViewModels;

namespace Aspid.MVVM.Mono
{
    public abstract partial class MonoBinder : IMonoBinderValidable
    {
        [SerializeField] private MonoView _view;
        [SerializeField] private string _id;
        
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
            if (Id != id) 
                throw new Exception($"Id not match. Binder Id {Id}; Id {id}.");
            
            if (viewModel != View?.ViewModel) 
                throw new Exception($"ViewModel {viewModel} not match. Binder ViewModel {View?.ViewModel}; Id {Id}.");
        }

        private void SaveBinderDataInEditor()
        {
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
        }
    }
}
#endif