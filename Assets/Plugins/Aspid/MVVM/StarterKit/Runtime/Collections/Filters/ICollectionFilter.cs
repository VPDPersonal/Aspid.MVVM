using System;

namespace Aspid.MVVM.StarterKit
{
    public interface ICollectionFilter<in T>
    {
        public Predicate<T>? Get();
    }
}