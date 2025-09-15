using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public interface ICollectionComparer<in T>
    {
        public IComparer<T>? Get();
    }
}