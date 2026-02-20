#if UNITY_EDITOR && !ASPID_MVVM_EDITOR_DISABLED
#nullable disable
using System;
using UnityEngine;
using System.ComponentModel;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public abstract partial class MonoBinder : IMonoBinderValidable, IRebindableBinder
    {
        // ReSharper disable once InconsistentNaming
        [EditorBrowsable(EditorBrowsableState.Never)]
        [SerializeField] private MonoView __view;
        
        // ReSharper disable once InconsistentNaming
        [EditorBrowsable(EditorBrowsableState.Never)]
        [SerializeField] private string __id;
        
        // ReSharper disable once InconsistentNaming
        [EditorBrowsable(EditorBrowsableState.Never)]
        [NonSerialized] private LastData? __bindData;
        
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

        partial void OnBoundDebug(IBinderAdder binderAdder) =>
            __bindData = new LastData(_mode, binderAdder);

        partial void OnUnboundDebug() =>
            __bindData = null;
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        void IRebindableBinder.Rebind()
        {
            if (__bindData is not null)
            {
                var cachedData = __bindData.Value;
                var currentMode = Mode;

                _mode = cachedData.Mode;
                Unbind();

                _mode = currentMode;
                Bind(cachedData.Adder);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private void SaveBinderDataInEditor()
        {
	        if (!((IMonoBinderValidable)this).IsMonoExist) return;
	        
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
        }
        
        private readonly struct LastData
        {
            public readonly BindMode Mode;
            public readonly IBinderAdder Adder;

            public LastData(BindMode mode, IBinderAdder adder)
            {
                Mode = mode;
                Adder = adder;
            }
        }
    }
}
#endif