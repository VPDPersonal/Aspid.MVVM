namespace Aspid.MVVM
{
    public readonly ref struct BindResult
    {
        public readonly bool IsBound;
        public readonly IRemoveBinderFromViewModel? BinderRemover;

        public BindResult(bool isBound)
        {
            IsBound = isBound;
            BinderRemover = null;
        }

        public BindResult(IRemoveBinderFromViewModel? binderRemover)
        {
            IsBound = true;
            BinderRemover = binderRemover;
        }
    }
}
