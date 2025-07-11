namespace Aspid.MVVM
{
    /// <summary>
    /// Represents the result of a binding operation, indicating whether a bindable member was successfully located.
    /// </summary>
    public readonly struct FindBindableMemberResult
    {
        /// <summary>
        /// Indicates whether the bindable member was successfully found.
        /// </summary>
        public readonly bool IsFound;
        
        /// <summary>
        /// The event adder for the bindable member, if found.
        /// </summary>
        public readonly IBinderAdder? Adder;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindBindableMemberResult"/> struct.
        /// </summary>
        /// <param name="adder">The event adder for the bindable member, or <c>null</c> if not found.</param>
        public FindBindableMemberResult(IBinderAdder? adder = null)
        {
            Adder = adder;
            IsFound = adder is not null;
        }
    }

}