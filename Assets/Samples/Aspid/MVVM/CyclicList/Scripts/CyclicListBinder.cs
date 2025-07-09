using System;
using System.Collections.Generic;
using Aspid.MVVM;
using Aspid.MVVM.StarterKit.Unity;
using Aspid.MVVM.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Aspid.MVVM.CyclicList
{
    public class CyclicListBinder : ObservableListMonoBinder<IViewModel>
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private MonoView _prefab;
        
        private int _firstVisibleIndex = -1;
        private int _visibleItemCount;

        private bool _childControlWidth;
        private bool _childControlHeight;
        
        private RectTransform Content => _scrollRect.content;
        
        private RectTransform Viewport => _scrollRect.viewport;
        
        private int previousTopIndex = -1;
        private List<MonoView> views = new();

        protected override void OnBound()
        {
            Calculate();
        }
        
        private void Calculate()
        {
            CalculateVisibleItemCount();
            CreateOrReuseViews();
        
            _scrollRect.onValueChanged.AddListener(OnScroll);
            UpdateVisibleItems();
        }
        
        void CalculateVisibleItemCount()
        {
            var itemHeight = 80;
            var viewHeight = _scrollRect.viewport.rect.height;
            _visibleItemCount = Mathf.CeilToInt(viewHeight / itemHeight) + 1;
            Content.sizeDelta = new Vector2(Content.sizeDelta.x, itemHeight * List.Count);
        }
        
        void CreateOrReuseViews()
        {
            for (int i = 0; i < _visibleItemCount; i++)
            {
                var go = Instantiate(_prefab, Content);
                views.Add(go);
            }
        }
        
        private void OnScroll(Vector2 _)
        {
            UpdateVisibleItems();
        }
        
        private void UpdateVisibleItems()
        {
            const int itemHeight = 80;
            float scrollY = Content.anchoredPosition.y;
            int newTopIndex = Mathf.FloorToInt(scrollY / itemHeight);

            if (newTopIndex == previousTopIndex) return;

            int direction = newTopIndex - previousTopIndex;
            previousTopIndex = newTopIndex;

            for (int i = 0; i < views.Count; i++)
            {
                int viewIndex = (i + direction) % views.Count;
                if (viewIndex < 0) viewIndex += views.Count;

                int modelIndex = newTopIndex + i;
                if (modelIndex < 0 || modelIndex >= List.Count)
                {
                    views[viewIndex].gameObject.SetActive(false);
                    continue;
                }

                views[viewIndex].gameObject.SetActive(true);
                views[viewIndex].transform.localPosition = new Vector3(0, -modelIndex * itemHeight, 0);
                views[viewIndex].Initialize(List[modelIndex]);
            }
            
            // const int itemHeight = 80;
            // float scrollY = Content.anchoredPosition.y;
            // int newFirstVisibleIndex = Mathf.FloorToInt(scrollY / itemHeight);
            //
            // if (_firstVisibleIndex == newFirstVisibleIndex)
            //     return;
            //
            // int direction = newFirstVisibleIndex - _firstVisibleIndex;
            // _firstVisibleIndex = newFirstVisibleIndex;
            //
            // for (int i = 0; i < views.Count; i++)
            // {
            //     int modelIndex = newFirstVisibleIndex + i;
            //
            //     if (modelIndex < 0 || modelIndex >= List.Count)
            //     {
            //         views[i].gameObject.SetActive(false);
            //         continue;
            //     }
            //
            //     views[i].gameObject.SetActive(true);
            //     views[i].transform.localPosition = new Vector3(0, -modelIndex * itemHeight, 0);
            //     views[i].Deinitialize();
            //     views[i].Initialize(List[modelIndex]); // ⚠️ Убери Deinitialize, если нет необходимости
            // }
            
            // var itemHeight = 80;
            // var scrollY = Content.anchoredPosition.y;
            // var topIndex = Mathf.FloorToInt(scrollY / itemHeight);
            //
            // if (topIndex == previousTopIndex) return;
            // previousTopIndex = topIndex;
            //
            // for (var i = 0; i < views.Count; i++)
            // {
            //     int modelIndex = topIndex + i;
            //     
            //     if (modelIndex < 0 || modelIndex >= List.Count)
            //     {
            //         views[i].gameObject.SetActive(false);
            //         continue;
            //     }
            //
            //     views[i].gameObject.SetActive(true);
            //     views[i].transform.localPosition = new Vector3(0, -modelIndex * itemHeight, 0);
            //     
            //     views[i].Deinitialize();
            //     views[i].Initialize(List[modelIndex]);
            // }
        }

        protected override void OnAdded(IViewModel newItem, int newStartingIndex)
        {
            // throw new System.NotImplementedException();
        }

        protected override void OnAdded(IReadOnlyList<IViewModel> newItems, int newStartingIndex)
        {
            // throw new System.NotImplementedException();
        }

        protected override void OnRemoved(IViewModel oldItem, int oldStartingIndex)
        {
            // throw new System.NotImplementedException();
        }

        protected override void OnRemoved(IReadOnlyList<IViewModel> oldItems, int oldStartingIndex)
        {
            // throw new System.NotImplementedException();
        }

        protected override void OnReplace(IViewModel oldItem, IViewModel newItem, int newStartingIndex)
        {
            // throw new System.NotImplementedException();
        }

        protected override void OnMove(IViewModel oldItem, IViewModel newItem, int oldStartingIndex, int newStartingIndex)
        {
            // throw new System.NotImplementedException();
        }

        protected override void OnReset()
        {
            // throw new System.NotImplementedException();
        }
    }
}