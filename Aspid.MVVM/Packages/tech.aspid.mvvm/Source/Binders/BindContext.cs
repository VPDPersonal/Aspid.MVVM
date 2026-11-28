using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Binding request containing a bindable member and its owner.
    /// </summary>
    public readonly struct BindContext
    {
        /// <summary>
        /// Gets the bindable member to attach to.
        /// </summary>
        public IBinderAdder Adder { get; }

        /// <summary>
        /// Gets the owner identified by reference equality.
        /// </summary>
        public object Owner { get; }

        /// <summary>
        /// Creates a binding context for the given member and owner.
        /// </summary>
        /// <param name="adder">The bindable member to attach to.</param>
        /// <param name="owner">The owner instance that will release the binding.</param>
        /// <exception cref="ArgumentNullException"><paramref name="adder"/> or <paramref name="owner"/> is <see langword="null"/>.</exception>
        public BindContext(IBinderAdder adder, object owner)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            Adder = adder ?? throw new ArgumentNullException(nameof(adder));
        }

        /// <summary>
        /// Verifies that the context has a bindable member and an owner.
        /// </summary>
        /// <exception cref="ArgumentException"><see cref="Adder"/> or <see cref="Owner"/> is <see langword="null"/>.</exception>
        [SuppressMessage(category: "ReSharper", checkId: "NotResolvedInText")]
        public void EnsureValid()
        {
            if (Adder is null || Owner is null)
                throw new ArgumentException("A binding context requires a member and an owner.", paramName: "context");
        }
    }
}
