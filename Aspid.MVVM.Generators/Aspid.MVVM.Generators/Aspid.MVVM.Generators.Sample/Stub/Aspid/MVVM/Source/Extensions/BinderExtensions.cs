using System;
using System.Runtime.CompilerServices;

namespace Aspid.MVVM
{
    public static partial class BinderExtensions
    {
        /// <summary>
        /// Attempts to cast a non-generic <see cref="IBinder"/> to a generic <see cref="IBinder{T}"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type to cast the binder to. The method assumes a nullable value of <typeparamref name="T"/>.
        /// </typeparam>
        /// <param name="binder">The non-generic binder instance to cast.</param>
        /// <returns>
        /// The binder cast to <see cref="IBinder{T}"/>, where <c>T</c> is nullable.
        /// </returns>
        /// <exception cref="InvalidCastException">
        /// Thrown if the binder cannot be cast to <see cref="IBinder{T}"/>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IBinder<T?> Cast<T>(this IBinder binder)
        {
            if (binder is not IBinder<T?> specificBinder) 
                throw new InvalidCastException($"Binder must be of type {typeof(IBinder<T?>)}.");

            return specificBinder;
        }
    }
}