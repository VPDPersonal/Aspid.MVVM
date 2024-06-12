using System;
using UnityEngine;
using System.Collections.Generic;
using UltimateUI.MVVM.ViewModels;
using UltimateUI.MVVM.Samples.Products.Models;
using UltimateUI.MVVM.Samples.Economic.Currency.Data;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.Products
{
    // public partial class ProductViewModel : IViewModel
    // {
    //     [Bind] private int _price;
    //     [Bind] private string _description;
    //     [Bind] private Sprite _productIcon;
    //     [Bind] private Sprite _currencyIcon;
    //     
    //     public ProductViewModel(IProduct product, CurrencyViewData viewData)
    //     {
    //         _price = product.Price;
    //         _productIcon = product.Icon;
    //         _description = product.Description;
    //         _currencyIcon = viewData.GetIcon(_price);
    //     }
    // }
    //
    // public partial class ProductViewModel
    // {
    //     private event Action<int> PriceChanged;
    //     private event Action<string> DescriptionChanged;
    //     private event Action<Sprite> ProductIconChanged;
    //     private event Action<Sprite> CurrencyIconChanged;
    //     
    //     private int Price
    //     {
    //         get => _price;
    //         set => ViewModelUtility.SetValue(ref _price, value, PriceChanged);
    //     }
    //     
    //     private string Description
    //     {
    //         get => _description;
    //         set => ViewModelUtility.SetValue(ref _description, value, DescriptionChanged);
    //     }
    //     
    //     private Sprite ProductIcon
    //     {
    //         get => _productIcon;
    //         set => ViewModelUtility.SetValue(ref _productIcon, value, ProductIconChanged);
    //     }
    //     
    //     private Sprite CurrencyIcon
    //     {
    //         get => _currencyIcon;
    //         set => ViewModelUtility.SetValue(ref _currencyIcon, value, CurrencyIconChanged);
    //     }
    //     
    //     void IViewModel.Bind(IReadOnlyDictionary<string, IReadOnlyList<IBinder>> bindersById)
    //     {
    //         var binds = GetBindFieldsForBind();
    //         
    //         foreach (var pair in bindersById)
    //             binds[pair.Key].Invoke(pair.Value);
    //     }
    //     
    //     void IViewModel.Unbind(IReadOnlyDictionary<string, IReadOnlyList<IBinder>> bindersById)
    //     {
    //         var unbinds = GetBindFieldsForUnbind();
    //         
    //         foreach (var pair in bindersById)
    //             unbinds[pair.Key].Invoke(pair.Value);
    //     }
    //     
    //     protected virtual IReadOnlyDictionary<string, Action<IReadOnlyList<IBinder>>> GetBindFieldsForBind()
    //     {
    //         return new Dictionary<string, Action<IReadOnlyList<IBinder>>>
    //         {
    //             { nameof(Price), binders => ViewModelUtility.Bind(Price, ref PriceChanged, binders) },
    //             { nameof(Description), binders => ViewModelUtility.Bind(Description, ref DescriptionChanged, binders) },
    //             { nameof(ProductIcon), binders => ViewModelUtility.Bind(ProductIcon, ref ProductIconChanged, binders) },
    //             { nameof(CurrencyIcon), binders => ViewModelUtility.Bind(CurrencyIcon, ref CurrencyIconChanged, binders) },
    //         };
    //     }
    //     
    //     protected virtual IReadOnlyDictionary<string, Action<IReadOnlyList<IBinder>>> GetBindFieldsForUnbind()
    //     {
    //         return new Dictionary<string, Action<IReadOnlyList<IBinder>>>
    //         {
    //             { nameof(Price), binders => ViewModelUtility.Unbind(ref PriceChanged, binders) },
    //             { nameof(Description), binders => ViewModelUtility.Unbind(ref DescriptionChanged, binders) },
    //             { nameof(ProductIcon), binders => ViewModelUtility.Unbind(ref ProductIconChanged, binders) },
    //             { nameof(CurrencyIcon), binders => ViewModelUtility.Unbind(ref CurrencyIconChanged, binders) },
    //         };
    //     }
    // }
}