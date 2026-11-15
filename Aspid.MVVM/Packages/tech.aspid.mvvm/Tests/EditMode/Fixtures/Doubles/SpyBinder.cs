using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal sealed class SpyBinder : IBinder
    {
        public bool IsBound { get; set; }

        public bool ThrowOnBind { get; set; }

        public bool ThrowOnUnbind { get; set; }

        public BindMode Mode => BindMode.OneWay;

        public void Bind(in BindContext context)
        {
            if (ThrowOnBind) throw new InvalidOperationException("broken binder");
            IsBound = true;
        }

        public void Unbind(in UnbindContext context)
        {
            if (ThrowOnUnbind) throw new InvalidOperationException("broken binder");
            IsBound = false;
        }
    }
}
