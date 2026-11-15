#if (UNITY_EDITOR || DEBUG) && !ASPID_MVVM_EDITOR_DISABLED
using System;
using System.ComponentModel;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public abstract partial class Binder : IRebindableBinder
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        [NonSerialized] private LastData? __bindData;

        partial void OnBoundDebug(in BindContext context) =>
            __bindData = new LastData(_mode, in context);

        partial void OnUnboundDebug() =>
            __bindData = null;

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
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
