using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    [ViewModel]
    internal partial class NotificationTestViewModel
    {
        private int _implicit, _direct, _namedA, _namedB, _manual, _sourceOnly;

        public List<string> Trace { get; } = new List<string>();
    }
}
