namespace Aspid.MVVM.StarterKit
{
    public sealed class EmptyViewModel : IViewModel
    {
        public FindBindableMemberResult FindBindableMember(Id id) =>
            default;

        public FindBindableMemberResult<T> FindBindableMember<T>(Id id) => 
            default;
    }
}