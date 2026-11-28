#if UNITY_EDITOR && !ASPID_MVVM_EDITOR_DISABLED
#nullable disable
using System;
using UnityEngine;
using System.ComponentModel;
using Aspid.MVVM.Validation;
using UnityEditor.SceneManagement;
using System.Diagnostics.CodeAnalysis;
using Component = UnityEngine.Component;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    [SuppressMessage(category: "ReSharper", checkId: "InconsistentNaming")]
    public abstract partial class MonoBinder : IMonoBinderValidatable, IRebindableBinder
    {
        [Tooltip("The View this binder belongs to.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [SerializeField] private Component __view;

        [Tooltip("The last non-empty View, kept to detect a lost reference.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [SerializeField] private MonoBinderPreviousView __previousView;

        [Tooltip("The ID of the View field this binder is bound through.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [SerializeField] private string __id;

        [Tooltip("The last non-empty ID, kept to detect a renamed field.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [SerializeField] private MonoBinderPreviousId __previousId;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [NonSerialized] private LastData? __bindData;

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        bool IMonoBinderValidatable.IsAlive => IsAlive;

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        IView IMonoBinderValidatable.View => __view != null
            ? __view as IView 
            : null;

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        MonoBinderPreviousView IMonoBinderValidatable.PreviousView => __previousView;

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        string IMonoBinderValidatable.Id => __id ?? string.Empty;

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        MonoBinderPreviousId IMonoBinderValidatable.PreviousId => __previousId;

        partial void OnBoundDebug(in BindContext context) =>
            __bindData = new LastData(Mode, in context);

        partial void OnUnboundDebug() =>
            __bindData = null;

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        void IMonoBinderValidatable.SetView(IView view)
        {
            if (view.IsMissing())
                ((IMonoBinderValidatable)this).ResetView();
            else SetViewInternal(view);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private void SetViewInternal(IView view)
        {
            if (!IsAlive) return;

            var componentView = view as Component;
            if (ReferenceEquals(__view, componentView)) return;

            if (componentView != null)
                __previousView = new MonoBinderPreviousView(componentView);
            else if (__view  != null)
                __previousView = new MonoBinderPreviousView(__view);

            __view = componentView;
            SaveBinderDataInEditor();
        }

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        void IMonoBinderValidatable.SetId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                ((IMonoBinderValidatable)this).ResetId();
            }
            else
            {
                SetIdInternal(id);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private void SetIdInternal(string id)
        {
            if (!IsAlive) return;
            if (__id == id) return;

            __previousId = string.IsNullOrWhiteSpace(id)
                ? new MonoBinderPreviousId(__id)
                : new MonoBinderPreviousId(id);

            __id = id;
            SaveBinderDataInEditor();
        }

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        void IMonoBinderValidatable.ResetView(MonoBinderResetMode mode)
        {
            SetViewInternal(null);

            if (mode is MonoBinderResetMode.Hard)
            {
                __previousView = default;
                SaveBinderDataInEditor();
            }
        }

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        void IMonoBinderValidatable.ResetId(MonoBinderResetMode mode)
        {
            SetIdInternal(string.Empty);

            if (mode is MonoBinderResetMode.Hard)
            {
                __previousId = default;
                SaveBinderDataInEditor();
            }
        }

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        void IRebindableBinder.Rebind()
        {
            if (__bindData is not null)
            {
                var cachedData = __bindData.Value;
                var currentMode = Mode;

                _mode = cachedData.Mode;
                try
                {
                    Unbind(new UnbindContext(cachedData.Context.Owner));
                }
                finally
                {
                    if (_state is BinderState.Unbound)
                        _mode = currentMode;
                }

                if (_state is not BinderState.Unbound) return;
                Bind(in cachedData.Context);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private void SaveBinderDataInEditor()
        {
            if (Application.isPlaying) return;
            if (!IsAlive) return;

            UnityEditor.EditorUtility.SetDirty(target: this);
            
            var scene = gameObject.scene;
            if (scene.IsValid())
                EditorSceneManager.MarkSceneDirty(scene);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private readonly struct LastData
        {
            public readonly BindMode Mode;
            public readonly BindContext Context;

            public LastData(
                BindMode mode,
                in BindContext context)
            {
                Mode = mode;
                Context = context;
            }
        }
    }
}
#endif
