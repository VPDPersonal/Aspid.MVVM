using System.Reflection;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal static class FieldContextFactory
    {
        public static IFieldContext Create(object target, MemberInfo memberInfo, bool isAlternativeColor = false)
        {
            if (memberInfo.IsBindField(target))
            {
                return new BindFieldContext(target, memberInfo as FieldInfo, isAlternativeColor);
            }
            
            return new FieldContext(target, memberInfo, isAlternativeColor);
        }

        private static bool IsBindField(this MemberInfo memberInfo, object obj) =>
            memberInfo is FieldInfo 
            && memberInfo.IsDefined(typeof(BaseBindAttribute))
            && obj.GetType().IsDefined(typeof(ViewModelAttribute))
            && obj is IViewModel;
    }
}
