using System;
using AspidUI.MVVM.ViewModels;
using AspidUI.MVVM.Collections;
using AspidUI.MVVM.Unity.Views;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace AspidUI.MVVM.StarterKit.Binders.Mono.Lists
{
    public abstract class ViewModelListBinderBase : ListBinderBase<IViewModel>
    {
        public abstract int MaxCount { get; }
        
        public int Count { get; private set; }
        
        protected abstract MonoView GetView(int index);
        
        protected override void OnCollectionChanged(INotifyCollectionChangedEventArgs<IViewModel> e)
        {
            var oldIndex = e.OldStartingIndex;
            var newIndex = e.OldStartingIndex;
            
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.IsSingleItem) OnAdd(newIndex, e.NewItem);
                    else OnAdd(newIndex, e.NewItems);
                    break;
                
                case NotifyCollectionChangedAction.Remove:
                    if (e.IsSingleItem) OnRemove(oldIndex, e.OldItem);
                    else OnRemove(oldIndex, e.OldItems);
                    break;
                
                case NotifyCollectionChangedAction.Replace:
                    if (e.IsSingleItem) OnReplace(newIndex, e.OldItem, e.NewItem);
                    else OnReplace(newIndex, e.OldItems, e.NewItems);
                    break;
                
                case NotifyCollectionChangedAction.Move: OnMove(newIndex, oldIndex); break;
                
                case NotifyCollectionChangedAction.Reset: OnReset(e.OldItems); break;
                
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void OnAdd(int newIndex, IViewModel newViewModel)
        {
            var count = Count + 1;
            if (count > MaxCount)
                throw new IndexOutOfRangeException();

            BindView(newIndex, newViewModel);
            Count = count;
        }

        private void OnAdd(int newIndex, IReadOnlyList<IViewModel> newViewModels)
        {
            var count = Count + newViewModels.Count;
            if (count > MaxCount)
                throw new IndexOutOfRangeException();

            for (var i = newIndex; i < newIndex + newViewModels.Count; i++)
                BindView(i, newViewModels[i - newIndex]);

            Count = count;
        }

        private void OnRemove(int oldIndex, IViewModel oldViewModel)
        {
            var count = Count - 1;
            if (count < 0)
                throw new IndexOutOfRangeException();

            UnbindView(oldIndex, oldViewModel);
            Count = count;
        }
        
        private void OnRemove(int oldIndex, IReadOnlyList<IViewModel> oldViewModels)
        {
            var count = Count - 1;
            if (count < 0)
                throw new IndexOutOfRangeException();

            for (var i = oldIndex; i < oldIndex + oldViewModels.Count; i++)
                UnbindView(i, oldViewModels[i - oldIndex]);
            
            Count = count;
        }

        private void OnReplace(int newIndex, IViewModel _, IViewModel newViewModel)
        {
            GetView(newIndex).Initialize(newViewModel);
        }

        private void OnReplace(int newIndex, IReadOnlyList<IViewModel> oldViewModels, IReadOnlyList<IViewModel> newViewModels)
        {
            for (var i = newIndex; i < newIndex + oldViewModels.Count; i++)
                GetView(newIndex).Initialize(newViewModels[i - newIndex]);
        }

        private void OnMove(int newIndex, int oldIndex)
        {
            var oldViewModel = List[oldIndex];
            var newViewModel = List[newIndex];
                    
            GetView(oldIndex).Initialize(oldViewModel);
            GetView(newIndex).Initialize(newViewModel);
        }

        private void OnReset(IReadOnlyList<IViewModel> viewModels)
        {
            for (var i = 0; i < viewModels.Count; i++)
                UnbindView(i, viewModels[i]);
            
            Count = 0;
        }
        
        protected void BindView(int index, IViewModel viewModel)
        {
            var view = GetView(index);
            view.Initialize(viewModel);
            view.gameObject.SetActive(true);

            OnViewBounded(view);
        }

        protected virtual void OnViewBounded(MonoView view) { }

        protected void UnbindView(int index, IViewModel viewModel)
        {
            var view = GetView(index);
            // TODO Unbind?
            view.gameObject.SetActive(false);
            
            OnViewUnbounded(view);
        }
        
        protected virtual void OnViewUnbounded(MonoView view) { }

        protected override void DisposeList()
        {
            for (var i = 0; i < Count; i++)
                UnbindView(i, List[i]);
        }
    }
}