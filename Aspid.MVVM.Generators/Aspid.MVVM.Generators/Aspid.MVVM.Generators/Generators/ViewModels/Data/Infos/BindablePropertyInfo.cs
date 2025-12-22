using System.Text;
using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Generators.Ids.Data;
using static Aspid.Generators.Helper.Classes;
using static Aspid.MVVM.Generators.Generators.Descriptions.General;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;

public sealed class BindablePropertyInfo : IBindableMemberInfo
{
    public ISymbol Member { get; }
    
    public string Type { get; }
    
    public string Name { get; }
    
    public IdData Id { get; }
    
    public BindMode Mode { get; }
    
    public GeneratedBindableMembers Bindable { get; }
    
    public string Declaration { get; }

    public BindablePropertyInfo(IPropertySymbol propertySymbol, BindMode mode, bool isSetMethodUsed)
    {
        Mode = mode;
        Member = propertySymbol;
        Name = propertySymbol.Name;
        Id = new IdData(propertySymbol);
        Type = propertySymbol.Type.ToDisplayStringGlobal();
        Bindable = GeneratedBindableMembers.CreateForProperty(propertySymbol);

        var setMethodName = $"Set{Name}Field";
        StringBuilder declaration = new();
        
        if (Mode is not BindMode.OneTime && Mode is not BindMode.None)
        {
            var onPropertyChangedMethod = $"On{Name}PropertyChanged";
            
            if (propertySymbol is { IsReadOnly: false, IsWriteOnly: false })
            {
                declaration.AppendLine($"#region {Name}");
                
                if (isSetMethodUsed)
                {
                    var onChangedMethod = $"On{Name}Changed";
                    var onChangingMethod = $"On{Name}Changing";

                    declaration.AppendLine(
                        $$"""
                        {{GeneratedCodeViewModelAttribute}} 
                        private bool {{setMethodName}}(ref {{Type}} field, {{Type}} value)
                        {
                            if ({{EqualityComparer_1}}<{{Type}}>.Default.Equals(field, value)) return false;
                            
                            {{Type}} oldValue = field;
                            
                            {{onChangingMethod}}(value);
                            {{onChangingMethod}}(oldValue, value);
                            {
                                field = value;
                                {{onPropertyChangedMethod}}();
                            }
                            {{onChangedMethod}}(value);
                            {{onChangedMethod}}(oldValue, value);
                            
                            return true;
                        }

                        [{{EditorBrowsableAttribute}}({{EditorBrowsableState}}.Never)]
                        {{GeneratedCodeViewModelAttribute}}
                        partial void {{onChangingMethod}}({{Type}} newValue);

                        [{{EditorBrowsableAttribute}}({{EditorBrowsableState}}.Never)]
                        {{GeneratedCodeViewModelAttribute}}
                        partial void {{onChangingMethod}}({{Type}} oldValue, {{Type}} newValue);

                        [{{EditorBrowsableAttribute}}({{EditorBrowsableState}}.Never)]
                        {{GeneratedCodeViewModelAttribute}}
                        partial void {{onChangedMethod}}({{Type}} newValue);

                        [{{EditorBrowsableAttribute}}({{EditorBrowsableState}}.Never)]
                        {{GeneratedCodeViewModelAttribute}}
                        partial void {{onChangedMethod}}({{Type}} oldValue, {{Type}} newValue);
                        """);
                }
                else
                {
                    declaration.AppendLine(
                        $"""
                        {GeneratedCodeViewModelAttribute} 
                        private bool {setMethodName}(ref {Type} field, {Type} value) =>
                            throw new {Classes.NotImplementedException}("Generator Error: SetMethod is not used.");
                        """);
                }
                
                declaration.AppendLine("#endregion");
            }
        }
        
        Declaration = declaration.ToString();
    }
}

