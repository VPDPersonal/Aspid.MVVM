namespace Aspid.MVVM
{
    /// <summary>
    /// Represents the result of a binding operation.
    /// </summary>
    public readonly ref struct FindBindableMemberResult
    {
        /// <summary>
        /// Indicates whether the binding operation was successful.
        /// </summary>
        public readonly bool IsFound;
        
        public readonly IViewModelEventAdder? Adder;
        
        public FindBindableMemberResult(bool isFound, IViewModelEventAdder? adder = null)
        {
            Adder = adder;
            IsFound = isFound;
        }
    }
    
    /// <summary>
    /// Represents the result of a binding operation.
    /// </summary>
    public readonly struct FindBindableMemberResult<T>
    {
        /// <summary>
        /// Indicates whether the binding operation was successful.
        /// </summary>
        public readonly bool IsFound;
        
        public readonly BindableMember<T?> Member;
        
        public FindBindableMemberResult(bool isBound)
        {
            Member = default;
            IsFound = isBound;
        }
        
        public FindBindableMemberResult(bool isBound, BindableMember<T?> member)
        {
            Member = member;
            IsFound = isBound;
        }

        public static implicit operator FindBindableMemberResult<T>(BindableMember<T?> bindableMember) =>
            new(true, bindableMember);
    }
}