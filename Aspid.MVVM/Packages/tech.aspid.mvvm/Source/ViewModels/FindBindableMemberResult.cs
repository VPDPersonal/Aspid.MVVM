// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Represents the result of <see cref="IViewModel.FindBindableMember"/>.
    /// </summary>
    public readonly struct FindBindableMemberResult
    {
        /// <summary>
        /// Indicates whether the bindable member was found.
        /// </summary>
        public readonly bool IsFound;
        
        /// <summary>
        /// The found bindable member; <see langword="null"/> when none was found.
        /// </summary>
        public readonly IBinderAdder? Adder;

        /// <summary>
        /// Creates a result for <paramref name="adder"/>.
        /// </summary>
        /// <param name="adder">The found bindable member; <see langword="null"/> when none was found.</param>
        public FindBindableMemberResult(IBinderAdder? adder = null)
        {
            Adder = adder;
            IsFound = adder is not null;
        }
    }
}
