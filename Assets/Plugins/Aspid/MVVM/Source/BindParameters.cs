using System;

namespace Aspid.MVVM
{
    /// <summary>
    /// Represents the parameters required to bind a component to a ViewModel.
    /// </summary>
    public readonly ref struct BindParameters
    {
        /// <summary>
        /// Gets the component ID for binding, which matches the property name in the ViewModel.
        /// </summary>
        public readonly string Id;
        
        /// <summary>
        /// Gets the ViewModel to which the component is bound.
        /// </summary>
        public readonly IViewModel ViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindParameters"/> struct with the specified ViewModel and component ID.
        /// </summary>
        /// <param name="viewModel">The ViewModel to bind to.</param>
        /// <param name="id">The component ID for binding, which matches the property name in the ViewModel.</param>
        public BindParameters(IViewModel viewModel, string id)
        {
            if (viewModel is null) throw new ArgumentNullException(nameof(viewModel));
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
            
            Id = id;
            ViewModel = viewModel;
        }
    }
}