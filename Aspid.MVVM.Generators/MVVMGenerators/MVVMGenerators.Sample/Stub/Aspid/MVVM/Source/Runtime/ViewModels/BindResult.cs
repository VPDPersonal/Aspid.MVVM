namespace Aspid.MVVM
{
    /// <summary>
    /// Represents the result of a binding operation.
    /// </summary>
    public readonly ref struct BindResult
    {
        /// <summary>
        /// Indicates whether the binding operation was successful.
        /// </summary>
        public readonly bool IsBound;
        
        /// <summary>
        /// Provides an interface for removing the binder from the ViewModel, if the binding was successful.
        /// </summary>
        public readonly IRemoveBinderFromViewModel? BinderRemover;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindResult"/> struct with the specified binding status.
        /// </summary>
        /// <param name="isBound">
        /// <c>true</c> if the binding operation was successful; otherwise, <c>false</c>.
        /// </param>
        /// <remarks>
        /// This constructor is used when the binding operation fails or when no binder remover is needed.
        /// </remarks>
        public BindResult(bool isBound)
        {
            IsBound = isBound;
            BinderRemover = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindResult"/> struct with a binder remover.
        /// </summary>
        /// <param name="binderRemover">
        /// The interface for removing the binder from the ViewModel, if the binding was successful.
        /// </param>
        /// <remarks>
        /// This constructor is used when the binding operation is successful and a binder remover is provided.
        /// </remarks>
        public BindResult(IRemoveBinderFromViewModel? binderRemover)
        {
            IsBound = true;
            BinderRemover = binderRemover;
        }
    }
}