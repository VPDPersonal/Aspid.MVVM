namespace Aspid.MVVM;

/// <summary>
/// Represents the parameters used to search for a bindable member in a ViewModel.
/// </summary>
public readonly ref struct FindBindableMemberParameters
{
    /// <summary>
    /// The identifier of the bindable member to search for.
    /// </summary>
    public readonly string Id;

    /// <summary>
    /// Initializes a new instance of the <see cref="FindBindableMemberParameters"/> struct with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the bindable member.</param>
    public FindBindableMemberParameters(string id)
    {
        Id = id;
    }
}