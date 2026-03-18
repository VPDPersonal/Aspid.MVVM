#if UNITY_EDITOR && !ASPID_MVVM_EDITOR_DISABLED
#nullable disable
using System;
using UnityEngine;
using System.ComponentModel;
using Aspid.MVVM.Validation;
using Component = UnityEngine.Component;

// ReSharper disable InconsistentNaming
// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public abstract partial class MonoBinder : IMonoBinderValidable, IRebindableBinder
    {
        #region View Fields
        [EditorBrowsable(EditorBrowsableState.Never)]
        [SerializeField] private Component __view;
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        [SerializeField] private MonoBinderPreviousView __previousView;
        #endregion

        #region Id Fields
        [EditorBrowsable(EditorBrowsableState.Never)]
        [SerializeField] private string __id;
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        [SerializeField] private MonoBinderPreviousId __previousId;
        #endregion
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        [NonSerialized] private LastData? __bindData;
        
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        bool IMonoBinderValidable.IsMonoExist => this;

        #region View Properties
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        IView IMonoBinderValidable.View => __view as IView;
        
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        MonoBinderPreviousView IMonoBinderValidable.PreviousView => __previousView;
        #endregion

        #region Id Properties
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        string IMonoBinderValidable.Id => __id;
        
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        MonoBinderPreviousId IMonoBinderValidable.PreviousId => __previousId;
        #endregion

        #region Bound Handlers
        partial void OnBoundDebug(IBinderAdder binderAdder) =>
            __bindData = new LastData(_mode, binderAdder);

        partial void OnUnboundDebug() =>
            __bindData = null;
        #endregion

        #region Set Methods
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        void IMonoBinderValidable.SetView(IView view)
        {
            if (view is null)
            {
                ((IMonoBinderValidable)this).ResetView();
            }
            else
            {
                SetViewInternal(view);   
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private void SetViewInternal(IView view)
        {
            if (!((IMonoBinderValidable)this).IsMonoExist) return;
            
            var componentView = view as Component;
            if (__view == componentView) return;
            
            __previousView = componentView 
                ? new MonoBinderPreviousView(componentView) 
                : new MonoBinderPreviousView(__view);
            
            __view = componentView;
            SaveBinderDataInEditor();
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        void IMonoBinderValidable.SetId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                ((IMonoBinderValidable)this).ResetId();
            }
            else
            {
                SetIdInternal(id);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private void SetIdInternal(string id)
        {
            if (!((IMonoBinderValidable)this).IsMonoExist) return;
            if (__id == id) return;
            
            __previousId = string.IsNullOrWhiteSpace(id) 
                ? new MonoBinderPreviousId(__id) 
                : new MonoBinderPreviousId(id);
            
            __id = id;
            SaveBinderDataInEditor();
        }
        #endregion

        #region Reset Methods
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        void IMonoBinderValidable.ResetView(MonoBinderResetMode mode)
        {
            SetViewInternal(null);
            
            if (mode is MonoBinderResetMode.Hard)
                __previousView = default;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        void IMonoBinderValidable.ResetId(MonoBinderResetMode mode)
        {
            SetIdInternal(string.Empty);
            
            if (mode is MonoBinderResetMode.Hard)
                __previousId = default;
        }
        #endregion
        
        [EditorBrowsable(EditorBrowsableState.Advanced)]
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
            if (Application.isPlaying) return;
            if (!((IMonoBinderValidable)this).IsMonoExist) return;
	        
            UnityEditor.EditorUtility.SetDirty(target: this);
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
        }
        
        [EditorBrowsable(EditorBrowsableState.Never)]
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