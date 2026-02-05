using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collections/Collection Binder – ViewModel")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Collections/Collection Binder – ViewModel")]
    public class ViewModelCollectionMonoBinder : ViewModelCollectionMonoBinder<MonoView> { }
    
    public abstract class ViewModelCollectionMonoBinder<T> : CollectionMonoBinder<IViewModel>
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
            
            for (var i = index; i < _views.Length; i++)
                _views[i].gameObject.SetActive(false);
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