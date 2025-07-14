using System.Collections.Generic;
using Aspid.Collections.Observable.Filtered;

namespace Aspid.MVVM.StarterKit
{
    public interface IFilterFactory<T>
    {
        public IReadOnlyFilteredList<T>? Create(IReadOnlyList<T>? list) =>
            list?.CreateFiltered();

        public void Release();
    }
}