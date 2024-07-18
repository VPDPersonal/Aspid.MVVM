// using UltimateUI.MVVM.Collections;
//
// // ReSharper disable once CheckNamespace
// namespace UltimateUI.MVVM.StarterKit.Binders.Lists
// {
//     public abstract partial class ListBinderBase<T> : MonoBinder, IBinder<IReadOnlyObservableList<T>>
//     {
//         protected IReadOnlyObservableList<T> List { get; private set; }
//         
// #if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
//         [BindingLog]
// #endif
//         public void SetValue(IReadOnlyObservableList<T> list)
//         {
//             if (List != null)
//             {
//                 Unsubscribe();
//                 DisposeList();
//             }
//             
//             List = list;
//             
//             Subscribe();
//             Initialize();
//         }
//         
//         protected virtual void Initialize() { }
//         
//         private void Subscribe()
//         {
//             List.CollectionChanged += OnCollectionChanged;
//         }
//
//         private void Unsubscribe()
//         {
//             List.CollectionChanged -= OnCollectionChanged;
//         }
//
//         protected virtual void DisposeList() { }
//
//         protected abstract void OnCollectionChanged(INotifyCollectionChangedEventArgs<T> e);
//     }
// }