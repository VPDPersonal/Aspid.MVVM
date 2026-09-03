using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Serializable sort order for collection binders.
    /// </summary>
    /// <typeparam name="T">The element type being ordered.</typeparam>
    public interface ICollectionOrder<in T> : IComparer<T> { }
}
