using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Identifies the owner releasing a binder.
    /// </summary>
    public readonly struct UnbindContext
    {
        /// <summary>
        /// Gets the owner identified by reference equality.
        /// </summary>
        public object Owner { get; }

        /// <summary>
        /// Creates a cleanup context for the given owner.
        /// </summary>
        /// <param name="owner">The same owner instance that acquired the binding.</param>
        /// <exception cref="ArgumentNullException"><paramref name="owner"/> is <see langword="null"/>.</exception>
        public UnbindContext(object owner)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
        }

        [SuppressMessage(category: "ReSharper", checkId: "NotResolvedInText")]
        internal void EnsureValid()
        {
            if (Owner is null)
                throw new ArgumentException("An unbinding context requires an owner.", paramName: "context");
        }
    }
}
