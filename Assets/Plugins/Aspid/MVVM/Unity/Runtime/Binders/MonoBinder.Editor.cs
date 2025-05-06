#if UNITY_EDITOR && !ASPID_MVVM_EDITOR_DISABLED
#nullable disable
using System;
using UnityEngine;
using System.ComponentModel;

namespace Aspid.MVVM.Unity
{
    public abstract partial class MonoBinder : IMonoBinderValidable
    {
        // ReSharper disable once InconsistentNaming
        [EditorBrowsable(EditorBrowsableState.Never)]
        [SerializeField] private MonoView __view;
        
        // ReSharper disable once InconsistentNaming
        [EditorBrowsable(EditorBrowsableState.Never)]
        [SerializeField] private string __id;
        
        /// <summary>
        /// Is there a component?
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        bool IMonoBinderValidable.IsMonoExist => this;
        
        /// <summary>
        /// The View to which the Binder relates.
        /// (Editor only).
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        IView IMonoBinderValidable.View
        {
            get => __view;
            set
            {
                if (!((IMonoBinderValidable)this).IsMonoExist) return;

                if (value is null)
                {
                    __view = null;
                    return;
                }
                
                if (__view == value as MonoView) return;
                
                __view = value switch
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
        string IMonoBinderValidable.Id
        {
            get => __id;
            set
            {
                if (!((IMonoBinderValidable)this).IsMonoExist) return;
                if (__id == value) return;
                
                __id = value;
                SaveBinderDataInEditor();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private void SaveBinderDataInEditor()
        {
	        if (!((IMonoBinderValidable)this).IsMonoExist) return;
	        
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
        }
    }
}
#endif