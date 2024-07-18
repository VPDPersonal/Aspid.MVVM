// using UltimateUI.MVVM.Samples.ProductSample.Economy;
// using UltimateUI.MVVM.Samples.ProductSample.Economy.Data;
// using UltimateUI.MVVM.ViewModels;
// using UltimateUI.MVVM.Views;
// using UnityEngine;
// using UnityEngine.Serialization;
//
// // ReSharper disable once CheckNamespace
// namespace UltimateUI.MVVM.Samples.ProductSample
// {
//     public class ProductSampleBootstrap : MonoBehaviour
//     {
//         [FormerlySerializedAs("_coinsView")] [SerializeField] private CurrencyView _coinsViewValidate;
//         [FormerlySerializedAs("_emeraldView")] [SerializeField] private CurrencyView _emeraldViewValidate;
//         [SerializeField] private CurrencyViewDataCollection _currencyViewDataCollection;
//         
//         [Space]
//         [SerializeField] [Min(0)] private int _coinsCount = 100;
//         [SerializeField] [Min(0)] private int _emeraldCount = 100;
//         
//         private void Awake()
//         {
//             // var wallet = new Wallet((CurrencyType.Coins, _coinsCount), (CurrencyType.Emeralds, _emeraldCount));
//             
//             // var coinsViewModel = new CurrencyViewModel(wallet, _currencyViewDataCollection, CurrencyType.Coins);
//             // var emeraldsViewModel = new CurrencyViewModel(wallet, _currencyViewDataCollection, CurrencyType.Emeralds);
//             //
//             // BindViewAndViewModel(_coinsView, coinsViewModel);
//             // BindViewAndViewModel(_emeraldView, emeraldsViewModel);
//         }
//         
//         private static void BindViewAndViewModel(IView view, IViewModel viewModel)
//         {
//             var binds = viewModel.GetBindMethods();
//             var binders = view.GetBinders();
//             
//             foreach (var binder in binders)
//             {
//                 if (binds.TryGetValue(binder.Key, out var bind))
//                     bind?.Invoke(binder.Value);
//             }
//         }
//     }
// }
