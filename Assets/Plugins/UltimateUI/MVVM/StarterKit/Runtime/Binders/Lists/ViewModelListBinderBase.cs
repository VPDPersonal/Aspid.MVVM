// using System;
// using UltimateUI.MVVM.Views;
// using UltimateUI.MVVM.ViewModels;
// using System.Collections.Generic;
// using UltimateUI.MVVM.Collections;
// using System.Collections.Specialized;
//
// // ReSharper disable once CheckNamespace
// namespace UltimateUI.MVVM.StarterKit.Binders.Lists
// {
//     public abstract class ViewModelListBinderBase : ListBinderBase<IViewModel>
//     {
//         public abstract int MaxCount { get; }
//         
//         public int Count { get; private set; }
//         
//         protected abstract MonoView GetView(int index);
//         
//         protected override void OnCollectionChanged(INotifyCollectionChangedEventArgs<IViewModel> e)
//         {
//             var oldIndex = e.OldStartingIndex;
//             var newIndex = e.OldStartingIndex;
//             
//             switch (e.Action)
//             {
//                 case NotifyCollectionChangedAction.Add:
//                     if (e.IsSingleItem) OnAdd(newIndex, e.NewItem);
//                     else OnAdd(newIndex, e.NewItems);
//                     break;
//                 
//                 case NotifyCollectionChangedAction.Remove:
//                     if (e.IsSingleItem) OnRemove(oldIndex, e.OldItem);
//                     else OnRemove(oldIndex, e.OldItems);
//                     break;
//                 
//                 case NotifyCollectionChangedAction.Replace:
//                     if (e.IsSingleItem) OnReplace(newIndex, e.OldItem, e.NewItem);
//                     else OnReplace(newIndex, e.OldItems, e.NewItems);
//                     break;
//                 
//                 case NotifyCollectionChangedAction.Move: OnMove(newIndex, oldIndex); break;
//                 
//                 case NotifyCollectionChangedAction.Reset: OnReset(e.OldItems); break;
//                 
//                 default: throw new ArgumentOutOfRangeException();
//             }
//         }
//
//         private void OnAdd(int newIndex, IViewModel newViewModel)
//         {
//             var count = Count + 1;
//             if (count > MaxCount)
//                 throw new IndexOutOfRangeException();
//
//             BindView(newIndex, newViewModel);
//             Count = count;
//         }
//
//         private void OnAdd(int newIndex, IReadOnlyList<IViewModel> newViewModels)
//         {
//             var count = Count + newViewModels.Count;
//             if (count > MaxCount)
//                 throw new IndexOutOfRangeException();
//
//             for (var i = newIndex; i < newIndex + newViewModels.Count; i++)
//                 BindView(i, newViewModels[i - newIndex]);
//
//             Count = count;
//         }
//
//         private void OnRemove(int oldIndex, IViewModel oldViewModel)
//         {
//             var count = Count - 1;
//             if (count < 0)
//                 throw new IndexOutOfRangeException();
//
//             UnbindView(oldIndex, oldViewModel);
//             Count = count;
//         }
//         
//         private void OnRemove(int oldIndex, IReadOnlyList<IViewModel> oldViewModels)
//         {
//             var count = Count - 1;
//             if (count < 0)
//                 throw new IndexOutOfRangeException();
//
//             for (var i = oldIndex; i < oldIndex + oldViewModels.Count; i++)
//                 UnbindView(i, oldViewModels[i - oldIndex]);
//             
//             Count = count;
//         }
//
//         private void OnReplace(int newIndex, IViewModel oldViewModel, IViewModel newViewModel)
//         {
//             ViewBinder.Rebind(GetView(newIndex), oldViewModel, newViewModel);
//         }
//
//         private void OnReplace(int newIndex, IReadOnlyList<IViewModel> oldViewModels, IReadOnlyList<IViewModel> newViewModels)
//         {
//             for (var i = newIndex; i < newIndex + oldViewModels.Count; i++)
//                 ViewBinder.Rebind(GetView(newIndex), oldViewModels[i - newIndex], newViewModels[i - newIndex]);
//         }
//
//         private void OnMove(int newIndex, int oldIndex)
//         {
//             var oldViewModel = List[oldIndex];
//             var newViewModel = List[newIndex];
//                     
//             ViewBinder.Rebind(GetView(oldIndex), newViewModel, oldViewModel);
//             ViewBinder.Rebind(GetView(newIndex), oldViewModel, newViewModel);
//         }
//
//         private void OnReset(IReadOnlyList<IViewModel> viewModels)
//         {
//             for (var i = 0; i < viewModels.Count; i++)
//                 UnbindView(i, viewModels[i]);
//             
//             Count = 0;
//         }
//         
//         protected void BindView(int index, IViewModel viewModel)
//         {
//             var view = GetView(index);
//             ViewBinder.Bind(view, viewModel);
//             view.gameObject.SetActive(true);
//
//             BindViewAfter(view);
//         }
//
//         protected virtual void BindViewAfter(MonoView view) { }
//
//         protected void UnbindView(int index, IViewModel viewModel)
//         {
//             var view = GetView(index);
//             ViewBinder.Unbind(view, viewModel);
//             view.gameObject.SetActive(false);
//             
//             UnbindViewAfter(view);
//         }
//         
//         protected virtual void UnbindViewAfter(MonoView view) { }
//
//         protected override void DisposeList()
//         {
//             for (var i = 0; i < Count; i++)
//                 UnbindView(i, List[i]);
//         }
//     }
// }