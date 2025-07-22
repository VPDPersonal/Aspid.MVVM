using System.Collections.Generic;

namespace Aspid.MVVM.StarterKit
{
    public interface ICollectionComparer<in T>
    {
        public IComparer<T>? Get();
    }
}