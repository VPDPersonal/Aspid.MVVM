using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Represents the query passed to <see cref="IViewModel.FindBindableMember"/>.
    /// </summary>
    /// <remarks>The default instance is invalid. Built-in ViewModels validate the query before lookup.</remarks>
    public readonly ref struct FindBindableMemberParameters
    {
        /// <summary>
        /// The identifier of the bindable member, as generated or set by <see cref="BindIdAttribute"/>.
        /// </summary>
        public readonly string Id;

        /// <summary>
        /// Creates a query for the member with identifier <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The identifier of the bindable member.</param>
        /// <exception cref="ArgumentNullException"><paramref name="id"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="id"/> is empty or whitespace.</exception>
        public FindBindableMemberParameters(string id)
        {
            ValidateId(id);
            Id = id;
        }

        /// <summary>
        /// Ensures the query has a non-empty identifier.
        /// </summary>
        /// <exception cref="ArgumentNullException">The identifier is <see langword="null"/>, including a default query.</exception>
        /// <exception cref="ArgumentException">The identifier is empty or whitespace.</exception>
        public void EnsureValid() =>
            ValidateId(Id);

        private static void ValidateId(string id)
        {
            if (id is null)
                throw new ArgumentNullException(nameof(id));
            
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("A bindable member ID cannot be empty or whitespace.", nameof(id));
        }
    }
}
