namespace Aspid.MVVM
{
    /// <summary>
    /// Interface for a ViewModel that supports data binding.
    /// </summary>
    public interface IViewModel
    {
        public FindBindableMemberResult FindBindableMember(Id id);
        
        public FindBindableMemberResult<T> FindBindableMember<T>(Id id);
    }
}