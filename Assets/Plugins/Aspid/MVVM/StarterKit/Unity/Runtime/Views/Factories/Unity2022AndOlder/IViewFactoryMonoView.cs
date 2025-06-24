using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    public interface IViewFactoryMonoView : IViewFactory<MonoView>, IViewFactory<Transform, MonoView> { }
}