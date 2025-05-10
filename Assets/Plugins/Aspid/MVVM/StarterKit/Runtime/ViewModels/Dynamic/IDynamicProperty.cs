namespace Aspid.MVVM.StarterKit
{
    public interface IDynamicProperty
    {
        public IBindableMemberEventAdder GetAdder();
    }
}