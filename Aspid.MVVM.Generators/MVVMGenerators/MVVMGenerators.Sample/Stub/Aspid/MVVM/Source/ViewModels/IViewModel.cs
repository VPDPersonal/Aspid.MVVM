namespace Aspid.MVVM
{
    /// <summary>
    /// Interface for a ViewModel that supports data binding.
    /// </summary>
    public interface IViewModel
    {
        public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters);
        
        public FindBindableMemberResult<T> FindBindableMember<T>(in FindBindableMemberParameters parameters);
    }
}