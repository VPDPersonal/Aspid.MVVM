using System;
using System.Runtime.CompilerServices;

namespace Aspid.MVVM
{
    public static partial class BinderExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IBinder<T?> Cast<T>(this IBinder binder)
        {
            if (binder is not IBinder<T?> specificBinder) 
                throw new InvalidCastException($"Binder must be of type {typeof(IBinder<T?>)}.");

            return specificBinder;
        }
    }
}