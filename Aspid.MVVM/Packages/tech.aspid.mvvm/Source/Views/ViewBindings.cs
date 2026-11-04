#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.ExceptionServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Tracks binders acquired by a generated View, leaving preexisting bindings with their owner.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class ViewBindings
    {
        private readonly List<IBinder> _binders = new();

        /// <summary>
        /// Binds one binder through the View's ownership tracker.
        /// </summary>
        /// <param name="binder">The binder to bind, or <see langword="null"/>.</param>
        /// <param name="adder">The member to bind to.</param>
        /// <param name="owner">The View used as the diagnostic context.</param>
        /// <param name="memberName">The binder member name.</param>
        public void BindSafely(IBinder? binder, IBinderAdder adder, object owner, string memberName)
        {
            if (binder is null) return;
            binder.BindSafely(new TrackingAdder(adder, _binders), owner, memberName);
        }

        /// <summary>
        /// Binds a collection through the View's ownership tracker, preserving collection failure isolation.
        /// </summary>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to bind, or <see langword="null"/>.</param>
        /// <param name="adder">The member to bind to.</param>
        /// <param name="owner">The View used as the diagnostic context.</param>
        /// <param name="memberName">The binder member name.</param>
        public void BindSafely<T>(IEnumerable<T>? binders, IBinderAdder adder, object owner, string memberName)
            where T : IBinder
        {
            if (binders is null) return;
            binders.BindSafely(new TrackingAdder(adder, _binders), owner, memberName);
        }

        /// <summary>
        /// Binds one binder when the member lookup succeeds.
        /// </summary>
        /// <param name="binder">The binder to bind, or <see langword="null"/>.</param>
        /// <param name="result">The member lookup result.</param>
        /// <param name="owner">The View used as the diagnostic context.</param>
        /// <param name="memberName">The binder member name.</param>
        public void BindSafely(IBinder? binder, in FindBindableMemberResult result, object owner, string memberName)
        {
            if (result.IsFound)
                BindSafely(binder, result.Adder!, owner, memberName);
        }

        /// <summary>
        /// Binds a collection when the member lookup succeeds.
        /// </summary>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to bind, or <see langword="null"/>.</param>
        /// <param name="result">The member lookup result.</param>
        /// <param name="owner">The View used as the diagnostic context.</param>
        /// <param name="memberName">The binder member name.</param>
        public void BindSafely<T>(IEnumerable<T>? binders, in FindBindableMemberResult result, object owner, string memberName)
            where T : IBinder
        {
            if (result.IsFound)
                BindSafely(binders, result.Adder!, owner, memberName);
        }

        /// <summary>
        /// Releases every acquired binder and rethrows the first cleanup failure after finishing the rest.
        /// </summary>
        public void Unbind()
        {
            Exception? failure = null;
            foreach (var binder in _binders)
            {
                try { binder.Unbind(); }
                catch (Exception exception) { failure ??= exception; }
            }

            _binders.Clear();
            if (failure is not null)
                ExceptionDispatchInfo.Capture(failure).Throw();
        }

        private sealed class TrackingAdder : IBinderAdder
        {
            private readonly IBinderAdder _adder;
            private readonly List<IBinder> _binders;

            public TrackingAdder(
                IBinderAdder adder,
                List<IBinder> binders)
            {
                _adder = adder;
                _binders = binders;
            }

            public BindMode Mode => _adder.Mode;

            public IBinderRemover? Add(IBinder binder)
            {
                var remover = _adder.Add(binder);
                _binders.Add(binder);
                return remover;
            }
        }
    }
}
