#if (UNITY_EDITOR || DEBUG) && !ASPID_MVVM_EDITOR_DISABLED
using UnityEngine;
using System.ComponentModel;

namespace Aspid.MVVM
{
    public abstract partial class Binder : IRebindableBinder
    {
        // ReSharper disable once InconsistentNaming
        [EditorBrowsable(EditorBrowsableState.Never)]
        private LastData? __bindData;

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