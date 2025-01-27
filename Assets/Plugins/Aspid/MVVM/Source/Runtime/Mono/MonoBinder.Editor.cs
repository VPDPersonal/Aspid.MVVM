#if UNITY_EDITOR && !ASPID_MVVM_EDITOR_DISABLED
#nullable disable
using System;
using UnityEngine;
using System.ComponentModel;

namespace Aspid.MVVM.Mono
{
    public abstract partial class MonoBinder : IMonoBinderValidable
    {
        [SerializeField] private MonoView _view;
        [SerializeField] private string _id;
        
        /// <summary>
        /// Is there a component?
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool IsMonoExist => this;
        
        /// <summary>
        /// The View to which the Binder relates.
        /// (Editor only).
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public IView View
        {
            get => _view;
            set
            {
                if (!IsMonoExist) return;

                if (value is null)
                {
                    _view = null;
                    return;
                }
                
                if (_view == value as MonoView) return;
                
                _view = value switch
                {
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
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string Id
        {
            get => _id;
            set
            {
                if (!IsMonoExist) return;
                if (_id == value) return;
                
                _id = value;
                SaveBinderDataInEditor();
            }
        }

        partial void OnBindingDebug(in BindParameters parameters)
        {
            if (Id != parameters.Id) 
                throw new Exception($"Id not match. Binder Id {Id}; Id {parameters.Id}.");
            
            if (parameters.ViewModel != View?.ViewModel) 
                throw new Exception($"ViewModel {parameters.ViewModel} not match. Binder ViewModel {View?.ViewModel}; Id {Id}.");
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private void SaveBinderDataInEditor()
        {
	        if (!IsMonoExist) return;
	        
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
        }
    }
}
#endif