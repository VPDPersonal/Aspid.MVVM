namespace Aspid.MVVM
{
    /// <summary>
    /// Represents the result of a binding operation.
    /// </summary>
    public readonly struct FindBindableMemberResult
    {
        /// <summary>
        /// Indicates whether the binding operation was successful.
        /// </summary>
        public readonly bool IsFound;
        
        public readonly IBindableMemberEventAdder? Adder;
        
        public FindBindableMemberResult(IBindableMemberEventAdder? adder = null)
        {
            Adder = adder;
            IsFound = adder is not null;
        }
    }
}