#if UNITY_EDITOR && !ASPID_MVVM_EDITOR_DISABLED
#nullable disable
using UnityEngine;
using System.ComponentModel;
using UnityEngine.Serialization;
using Component = UnityEngine.Component;

namespace Aspid.MVVM.Unity
{
    public abstract partial class MonoBinder : IMonoBinderValidable, IRebindableBinder
    {
        // ReSharper disable once InconsistentNaming
        [FormerlySerializedAs("__view")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [SerializeField] private Component __source;
        
        // ReSharper disable once InconsistentNaming
        [EditorBrowsable(EditorBrowsableState.Never)]
        [SerializeField] private string __id;
        
        // ReSharper disable once InconsistentNaming
        [EditorBrowsable(EditorBrowsableState.Never)]
        private LastData? __bindData;
        
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
        IMonoBinderSource IMonoBinderValidable.Source
        {
            get => __source as IMonoBinderSource;
            set
            {
                if (!((IMonoBinderValidable)this).IsMonoExist) return;

                if (value is null)
                {
                    __source = null;
                    return;
                }
                
                var component = value as Component;
                if (__source == component) return;
                
                __source = component;
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