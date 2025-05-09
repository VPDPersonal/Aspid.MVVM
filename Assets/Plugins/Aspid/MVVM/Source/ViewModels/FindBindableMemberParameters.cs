namespace Aspid.MVVM
{
    public readonly ref struct FindBindableMemberParameters
    {
        public readonly string Id;

        public FindBindableMemberParameters(string id)
        {
            Id = id;
        }
    }
}