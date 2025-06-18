using UnityEngine;
using Aspid.MVVM.Unity;
using System.Collections.Generic;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collections/Static Collection - ViewModel")]
    [AddComponentContextMenu(typeof(Component), "Add General Binder/Collection/Static Collection - ViewModel")]
    public class StaticViewModelMonoCollection : StaticViewModelMonoCollection<MonoView> { }
    
    public abstract class StaticViewModelMonoCollection<T> : CollectionMonoBinderBase<IViewModel>
        where T : MonoBehaviour, IView
    {
        [SerializeField] private T[] _views;

        protected override void OnAdded(IReadOnlyCollection<IViewModel> values)
        {
            var index = 0;
            
            foreach (var value in values)
            {
                _views[index].gameObject.SetActive(true);
                _views[index].Initialize(value);

                index++;
            }
        }

        protected override void OnReset()
        {
            foreach (var view in _views)
            {
                view.Deinitialize();
                view.gameObject.SetActive(false);
            }
        }
    }
}