using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public interface IBinderAdderEnumerable
    {
        public IEnumerable<IBinderAdder> GetBinderAdders();
    }
}