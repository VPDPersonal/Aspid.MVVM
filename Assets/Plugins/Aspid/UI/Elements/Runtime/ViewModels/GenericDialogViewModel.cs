using System;
using UnityEngine;
using Aspid.MVVM.Generation;
using Aspid.MVVM.ViewModels;
using Aspid.UI.Elements.Models;
using System.Collections.Generic;
using Aspid.MVVM.ViewModels.Generation;

namespace Aspid.UI.Elements.ViewModels
{
    [ViewModel]
    [CreateFrom(typeof(GenericDialog))]
    public partial class GenericDialogViewModel
    {
        [ReadOnlyBind] private readonly Sprite _icon;
        [ReadOnlyBind] private readonly string _title;
        [ReadOnlyBind] private readonly string _message;
        [ReadOnlyBind] private readonly GenericDialogButtonViewModel[] _genericDialogButtons;

        public GenericDialogViewModel(GenericDialog genericDialog)
        {
            _icon = genericDialog.Icon;
            _title = genericDialog.Title;
            _message = genericDialog.Message;
            _genericDialogButtons = genericDialog.Buttons.ToGenericDialogButtonViewModel();
        }
    }

    public static class ViewModelExtension
    {
        public static TViewModel ToViewModel<T, TViewModel>(this T model, Func<T, TViewModel> convert)
            where TViewModel : IViewModel
        {
            return convert(model);
        }
        
        public static TViewModel[] ToViewModel<T, TViewModel>(this T[] models, Func<T, TViewModel> convert)
            where TViewModel : IViewModel
        {
            var viewModels = new TViewModel[models.Length];

            for (var i = 0; i < models.Length; i++)
                viewModels[i] = models[i].ToViewModel(convert);

            return viewModels;
        }
        
        public static List<TViewModel> ToViewModelAsList<T, TViewModel>(this T[] models, Func<T, TViewModel> convert)
            where TViewModel : IViewModel
        {
            var viewModels = new List<TViewModel>(models.Length);

            foreach (var model in models)
                viewModels.Add(model.ToViewModel(convert));

            return viewModels;
        }
        
        public static List<TViewModel> ToViewModel<T, TViewModel>(this List<T> models, Func<T, TViewModel> convert)
            where TViewModel : IViewModel
        {
            var viewModels = new List<TViewModel>(models.Count);

            foreach (var model in models)
                viewModels.Add(model.ToViewModel(convert));

            return viewModels;
        }
        
        public static TViewModel[] ToViewModelAsArray<T, TViewModel>(this List<T> models, Func<T, TViewModel> convert)
            where TViewModel : IViewModel
        {
            var viewModels = new TViewModel[models.Count];

            for (var i = 0; i < models.Count; i++)
                viewModels[i] = models[i].ToViewModel(convert);

            return viewModels;
        }
    }
}