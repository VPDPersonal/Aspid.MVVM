using System;
using System.Collections.Generic;
// using System.Linq;
using AspidUI.MVVM.Commands;
using AspidUI.MVVM.ViewModels.Generation;
using Plugins.AspidUI.Elements.Runtime.Models;

namespace Plugins.AspidUI.Elements.Runtime.ViewModels
{
    [ViewModel]
    [ModelToViewModel(typeof(GenericDialogButton))]
    public partial class GenericDialogButtonViewModel
    {
        [ReadOnlyBind] private readonly string _text;
        [ReadOnlyBind] private readonly IRelayCommand _clickCommand;

        private GenericDialogButton _button;

        public GenericDialogButtonViewModel(GenericDialogButton button)
        {
            _text = button.Text;
            _clickCommand = new RelayCommand(button.CallBack);
        }
    }
    
    public static class GenericDialogButtonToGenericDialogButtonViewModel
    {
        public static GenericDialogButtonViewModel ToGenericDialogButtonViewModel<T>(this T model)
            where T : GenericDialogButton
        {
            return new GenericDialogButtonViewModel(model);
        }
        
        public static GenericDialogButtonViewModel[] ToGenericDialogButtonViewModel<T>(this T[] models)
            where T : GenericDialogButton
        {
            var viewModels = new GenericDialogButtonViewModel[models.Length];

            for (var i = 0; i < models.Length; i++)
                viewModels[i] = new GenericDialogButtonViewModel(models[i]);

            return viewModels;
        }
        
        public static GenericDialogButtonViewModel[] ToGenericDialogButtonViewModelAsArray<T>(this List<T> models)
            where T : GenericDialogButton
        {
            var viewModels = new GenericDialogButtonViewModel[models.Count];

            for (var i = 0; i < models.Count; i++)
                viewModels[i] = new GenericDialogButtonViewModel(models[i]);

            return viewModels;
        }
        
        public static Span<GenericDialogButtonViewModel> ToGenericDialogButtonViewModelAsSpan<T>(this List<T> models)
            where T : GenericDialogButton
        {
            var viewModels = new GenericDialogButtonViewModel[models.Count];

            for (var i = 0; i < models.Count; i++)
                viewModels[i] = new GenericDialogButtonViewModel(models[i]);

            return viewModels;
        }
        
        public static List<GenericDialogButtonViewModel> ToGenericDialogButtonViewModelAsList<T>(this T[] models)
            where T : GenericDialogButton
        {
            var viewModels = new List<GenericDialogButtonViewModel>(models.Length);

            foreach (var model in models)
                viewModels.Add(new GenericDialogButtonViewModel(model));

            return viewModels;
        }
        
        public static List<GenericDialogButtonViewModel> ToGenericDialogButtonViewModel<T>(this List<T> models)
            where T : GenericDialogButton
        {
            var viewModels = new List<GenericDialogButtonViewModel>(models.Count);

            foreach (var model in models)
                viewModels.Add(new GenericDialogButtonViewModel(model));

            return viewModels;
        }
        
        public static IEnumerable<GenericDialogButtonViewModel> ToGenericDialogButtonViewModel<T>(this IEnumerable<T> models)
            where T : GenericDialogButton
        {
            return models.Select(model => new GenericDialogButtonViewModel(model));
        }
    }
}