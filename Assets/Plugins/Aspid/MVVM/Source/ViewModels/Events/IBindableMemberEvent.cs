namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a bindable member event that supports both adding and removing event bindings.
    /// </summary>
    public interface IBindableMemberEvent : IBindableMemberEventAdder, IBindableMemberEventRemover { }
}