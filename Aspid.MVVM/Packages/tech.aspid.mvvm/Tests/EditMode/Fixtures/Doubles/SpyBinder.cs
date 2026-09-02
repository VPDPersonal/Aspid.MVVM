using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// A binder that records whether it was bound and can be told to throw from either half of the lifecycle.
    /// </summary>
    internal sealed class SpyBinder : IBinder
    {
        public bool IsBound { get; set; }

        public bool ThrowOnBind { get; set; }

        public bool ThrowOnUnbind { get; set; }

        public BindMode Mode => BindMode.OneWay;

        public void Bind(IBinderAdder binderAdder)
        {
            if (ThrowOnBind) throw new InvalidOperationException("broken binder");
            IsBound = true;
        }

        public void Unbind()
        {
            if (ThrowOnUnbind) throw new InvalidOperationException("broken binder");
            IsBound = false;
        }
    }
}
