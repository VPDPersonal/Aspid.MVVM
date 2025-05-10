namespace Aspid.MVVM.StarterKit
{
    public sealed class EmptyViewModel : IViewModel
    {
        public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters) =>
            default;
    }
}