using UnityEngine;
using UltimateUI.MVVM.Unity.Views;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.Lists
{
    [AddComponentMenu("UI/Binders/Lists/Static List Binder - View Model")]
    public sealed class ViewModelStaticListBinder : ViewModelListBinderBase
    {
        [SerializeField] private MonoView[] _views;

        public override int MaxCount => _views.Length;

        protected override MonoView GetView(int index) => _views[index];

        protected override void Initialize()
        {
            for (var i = 0; i < List.Count; i++)
                BindView(i, List[i]);

            for (var i = List.Count; i < _views.Length; i++)
                _views[i].gameObject.SetActive(false);
        }
    }
}