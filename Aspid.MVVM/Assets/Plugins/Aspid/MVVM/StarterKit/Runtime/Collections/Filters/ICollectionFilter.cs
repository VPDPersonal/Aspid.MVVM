using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public interface ICollectionFilter<in T>
    {
        public Predicate<T>? Get();
    }
}