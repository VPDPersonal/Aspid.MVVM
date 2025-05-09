namespace Aspid.MVVM
{
    /// <summary>
    /// Represents the result of a binding operation.
    /// </summary>
    public readonly partial struct FindBindableMemberResult
    {
        /// <summary>
        /// Indicates whether the binding operation was successful.
        /// </summary>
        public readonly bool IsFound;
        
        public readonly IViewModelEventAdder? Adder;
        
        public FindBindableMemberResult(IViewModelEventAdder? adder = null)
        {
            Adder = adder;
            IsFound = true;
        }
    }
    
    /// <summary>
    /// Represents the result of a binding operation.
    /// </summary>
    public readonly partial struct FindBindableMemberResult<T>
    {
        /// <summary>
        /// Indicates whether the binding operation was successful.
        /// </summary>
        public readonly bool IsFound;
        public readonly BindableMember<T?> Member;
        
        public FindBindableMemberResult(BindableMember<T?> member)
        {
            Member = member;
            IsFound = true;
        }

        public static implicit operator FindBindableMemberResult(FindBindableMemberResult<T> result) =>
            new(result.Member);
        
        public static implicit operator FindBindableMemberResult<T>(in BindableMember<T?> bindableMember) =>
            new(bindableMember);
    }
}