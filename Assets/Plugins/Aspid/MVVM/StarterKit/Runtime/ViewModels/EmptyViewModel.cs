namespace Aspid.MVVM.StarterKit
{
    public sealed class EmptyViewModel : IViewModel
    {
        public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters) =>
            default;

        public FindBindableMemberResult<T> FindBindableMember<T>(in FindBindableMemberParameters parameters) => 
            default;
    }
}