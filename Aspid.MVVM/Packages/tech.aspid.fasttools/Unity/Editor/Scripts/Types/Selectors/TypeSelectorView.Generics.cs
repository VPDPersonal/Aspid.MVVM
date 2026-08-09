using System;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    // The open-generic argument-resolution flow: one pushed page per type parameter, each reusing the ordinary
    // search/keyboard/navigation, accumulating arguments until the closed type is constructed and emitted. The
    // page stack (_pages) is owned here through Push/Pop, with PickerPage describing a single page's context.
    internal sealed partial class TypeSelectorView
    {
        private void BeginResolveGeneric(Type openDefinition, Type primaryFieldType, Type[] validationFieldTypes, Action<Type> onClosed)
        {
            // A closed-generic field already fixes the arguments — skip the picker and construct directly. Inference
            // only checks the primary field type, so re-validate the result against every field type (as the manual
            // PickParam -> TryConstruct path does); if it is not assignable to all of them, fall through to the picker
            // rather than emitting a value Unity would drop.
            if (GenericTypeResolver.TryInferFromFieldType(primaryFieldType, openDefinition, out var inferred) &&
                GenericTypeResolver.IsAssignableToFieldTypes(inferred, validationFieldTypes))
            {
                onClosed(inferred);
                return;
            }

            PickParam(openDefinition, validationFieldTypes, Array.Empty<Type>(), _pages.Count, onClosed);
        }

        private void PickParam(Type openDefinition, Type[] validationFieldTypes, Type[] argsSoFar, int startDepth, Action<Type> onClosed)
        {
            var parameters = openDefinition.GetGenericArguments();

            if (argsSoFar.Length == parameters.Length)
            {
                if (GenericTypeResolver.TryConstruct(openDefinition, argsSoFar, validationFieldTypes, out var closed, out var error))
                {
                    PopToDepth(startDepth);
                    onClosed(closed);
                }
                else
                {
                    ShowError(error);
                }

                return;
            }

            var index = argsSoFar.Length;
            var parameter = parameters[index];

            var page = BuildParamPage(openDefinition, argsSoFar, index, parameter, picked =>
                PickParam(openDefinition, validationFieldTypes, Append(argsSoFar, picked), startDepth, onClosed));

            PushPage(page);
        }

        private PickerPage BuildParamPage(Type openDefinition, Type[] argsSoFar, int index, Type parameter, Action<Type> onPicked)
        {
            var baseTypes = GenericTypeResolver.GetConstraintBaseTypes(parameter);
            var constraintType = baseTypes.Length == 1 ? baseTypes[0] : typeof(object);

            // Offer open generic definitions as arguments too, so the user can nest generics (e.g. choose
            // Modifier<T> for T) — picking one resolves its own arguments before it is used here. Pass every
            // constraint base type (not just the collapsed single one) so a multi-constraint parameter narrows
            // the nested definitions by all of them up front, instead of offering defs that fail every later pick.
            var nested = GenericTypeResolver.GetAssignableGenericDefinitions(baseTypes[0], baseTypes);
            var hierarchy = HierarchyBuilder.Build(baseTypes, TypeAllow.None, (Func<Type, bool>)Filter, nested, includeNoneOption: false);

            return new PickerPage
            {
                Navigation = new NavigationController(hierarchy),
                TitlePrefix = $"{FormatBuilding(openDefinition, argsSoFar, index)}  ▸  {parameter.Name}",
                ConstraintType = constraintType,
                OnPicked = onPicked,
                IsBase = false,
            };

            bool Filter(Type candidate) =>
                GenericTypeResolver.SatisfiesSpecialConstraints(parameter, candidate)
                && (_argumentFilter?.Invoke(candidate) ?? true);
        }

        private void PushPage(PickerPage page)
        {
            _pages.Add(page);
            ResetSearchField();
            UpdateSearchChrome();
            RefreshView();
            SelectFirstItem();
            FocusPicker();
        }

        private void PopPage()
        {
            if (_pages.Count <= 1) return;

            _pages.RemoveAt(_pages.Count - 1);
            HideError();
            ResetSearchField();
            UpdateSearchChrome();
            RefreshView();
            SelectFirstItem();
            FocusPicker();
        }

        private void PopToDepth(int depth)
        {
            while (_pages.Count > depth)
                _pages.RemoveAt(_pages.Count - 1);
        }

        private void ResetSearchField()
        {
            _searchField.SetValueWithoutNotify(string.Empty);
            Nav.ApplySearch(string.Empty);
        }

        private void SelectFirstItem()
        {
            _listView.selectedIndex = Nav.CurrentItems.Count > 0 ? 0 : -1;
            _listView.ScrollToItem(0);
        }

        private static string FormatBuilding(Type openDefinition, Type[] argsSoFar, int currentIndex)
        {
            var parameters = openDefinition.GetGenericArguments();

            // A [TypeSelectorDisplay(Name)] override carries its "<T, …>" suffix (see GetCustomDisplayName); the
            // building header re-spells the argument list itself, so only the base part before '<' is wanted here.
            var custom = TypeSelectorHelpers.GetCustomDisplayName(openDefinition);
            var angle = custom?.IndexOf('<') ?? -1;
            var baseName = custom is null
                ? TypeUtility.StripArity(openDefinition.Name)
                : angle < 0 ? custom : custom[..angle];

            var parts = new string[parameters.Length];
            for (var k = 0; k < parameters.Length; k++)
            {
                if (k < argsSoFar.Length) parts[k] = TypeSelectorHelpers.GetTypeSelectorTitle(argsSoFar[k]);
                else if (k == currentIndex) parts[k] = "?";
                else parts[k] = parameters[k].Name;
            }

            return $"{baseName}<{string.Join(", ", parts)}>";
        }

        private static Type[] Append(Type[] array, Type value)
        {
            var result = new Type[array.Length + 1];
            Array.Copy(array, result, array.Length);
            result[^1] = value;
            return result;
        }

        private sealed class PickerPage
        {
            public NavigationController Navigation;
            public string TitlePrefix;
            public Type ConstraintType;
            public Action<Type> OnPicked;
            public bool IsBase;
        }
    }
}
