using System.Linq;
using Microsoft.CodeAnalysis;
using Aspid.MVVM.Generators.Helpers;
using Aspid.MVVM.Generators.Shared;
using Aspid.Generators.Helper;
using System.Collections.Generic;
using Aspid.MVVM.Generators.Generators.Views.Data;
using Aspid.MVVM.Generators.Generators.Descriptions;
using Aspid.MVVM.Generators.Generators.Views.Helpers;
using Aspid.MVVM.Generators.Generators.ViewModels.Factories;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;
using Aspid.MVVM.Generators.Generators.Views.Body.Extensions;
using static Aspid.Generators.Helper.Classes;
using static Aspid.Generators.Helper.Unity.UnityClasses;
using static Aspid.MVVM.Generators.Generators.Descriptions.Defines;
using static Aspid.MVVM.Generators.Generators.Descriptions.Constants;

namespace Aspid.MVVM.Generators.Generators.Views.Body;

public static class GenericInitializeView
{
    internal static void Generate(
        in ViewDataSpan data,
        AspidTypeDeclaration declaration,
        in SourceProductionContext context)
    {
        foreach (var genericView in data.GenericViews)
        {
            var code = new CodeWriter();

            code.BeginScopedClass(
                    ["Aspid.MVVM", "AspidBinderExtensions = global::Aspid.MVVM.BinderExtensions"],
                    declaration,
                    null)
                .AppendGenericViews(data, genericView)
                .EndScopedClass(declaration);

            context.AddSource(declaration.GetFileName("TypedInitialize." + genericView.Type.ToDisplayString()), code.GetSourceText());
        }
    }

    private static CodeWriter AppendGenericViews(this CodeWriter code, in ViewDataSpan data, GenericViewData genericView)
    {
        code.AppendProfilerMarker(data, genericView.Type)
            .AppendLine()
            .AppendInitialize(data, genericView);

        return code;
    }

    private static CodeWriter AppendInitialize(this CodeWriter code, in ViewDataSpan data, GenericViewData genericView)
    {
        string modifier;
        if (genericView.IsSelf)
            modifier = data.Symbol.IsSealed ? "private" : "protected virtual";
        else
            modifier = "protected override";

        var typeName = genericView.Type.ToDisplayStringGlobal();

        if (genericView.IsSelf)
        {
            code.AppendMultiline(
                $$"""
                  /// <inheritdoc/>
                  /// <exception cref="global::System.ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
                  /// <exception cref="global::System.InvalidOperationException">The View is already initialized.</exception>
                  {{GeneratedCodeViewAttribute}}
                  public void Initialize({{typeName}} viewModel)
                  {
                      if (viewModel is null)
                          throw new {{ArgumentNullException}}(nameof(viewModel));
                      if (ViewModel is not null)
                          throw new {{InvalidOperationException}}("View is already initialized.");

                      ViewModel = viewModel;
                      __isInitializingView = true;
                      try
                      {
                          InitializeInternal(viewModel);
                      }
                      catch
                      {
                          __isDeinitializing = true;
                          try
                          {
                              DeinitializeInternal();
                          }
                          catch
                          {
                              // Do not replace the initialization exception with a cleanup failure.
                          }
                          finally
                          {
                              ViewModel = null;
                              __isDeinitializing = false;
                          }
                          throw;
                      }
                      finally
                      {
                          __isInitializingView = false;
                      }
                  }
                  """);

            code.AppendLine();
        }

        code.AppendMultiline(
            $"""
            {GeneratedCodeViewAttribute}
            {modifier} void InitializeInternal({typeName} viewModel)
            """)
            .BeginBlock()
            .AppendLine($"#if {ASPID_MVVM_PROFILER}")
            .AppendLine($"using (__initialize{AspidTypeDeclaration.Identifier(genericView.Type.ToDisplayString())}Marker.Auto())")
            .AppendLine("#endif")
            .BeginBlock();

        code.AppendLine("if (__isInitializing)")
            .BeginBlock()
            .AppendLineIf(!genericView.IsSelf, "base.InitializeInternal(viewModel);")
            .AppendLine("return;")
            .EndBlock()
            .AppendLine()
            .AppendLine("__isInitializing = true;")
            .AppendLine("try")
            .BeginBlock()
            .AppendLine();

        code.AppendLine("OnInitializingInternal(viewModel);");
        code.AppendLineIf(data.Inheritor is not Inheritor.None, "base.InitializeInternal(viewModel);");

        var isInstantiateBinders = data.MembersByType.PropertyBinders.Length + data.MembersByType.AsBinders.Length > 0;
        code.AppendLineIf(isInstantiateBinders, "InstantiateBinders();");
        code.AppendLine();

        if (genericView.Type.TypeKind is not TypeKind.Interface)
        {
            var bindableMembers = new Dictionary<string, IBindableMemberInfo>();

            for (var viewModelType = genericView.Type; viewModelType is not null; viewModelType = viewModelType.BaseType)
            {
                foreach (var memberPair in
                         BindableMembersFactory.Create(viewModelType, data.Compilation, out _)
                             .ToDictionary(bindable => bindable.Id.SourceValue, bindable => bindable))
                {
                    bindableMembers.Add(memberPair.Key, memberPair.Value);
                }
            }

            foreach (var member in data.Members)
            {
                if (bindableMembers.TryGetValue(member.Id.SourceValue, out var bindableMember))
                {
                    code.AppendBindSafely(member, bindableMember);
                }
                else
                {
                    code.AppendBindSafely(member);
                }
            }

            var virtualFields = VirtualBinderFields.Collect(data);
            foreach (var info in virtualFields.Values)
            {
                if (bindableMembers.TryGetValue(info.Id.SourceValue, out var bindableMember))
                    code.AppendLine(
                        $"AspidBinderExtensions.BindSafely({info.FieldName}, " +
                        $"new global::Aspid.MVVM.BindContext(viewModel.{bindableMember.Bindable.PropertyName}, this));");
            }
        }
        else
        {
            var customViewModelInterfaces = CustomViewModelInterfacesFactory.Create(genericView.Type);

            foreach (var member in data.Members)
            {
                if (customViewModelInterfaces.TryGetValue(member.Id.SourceValue, out var bindableMember))
                {
                    code.AppendBindSafely(member, bindableMember.Property.Name);
                }
                else
                {
                    code.AppendBindSafely(member);
                }
            }
        }

        code.AppendLine()
            .AppendLine("OnInitializedInternal(viewModel);")
            .EndBlock()
            .AppendLine("finally")
            .BeginBlock()
            .AppendLine("__isInitializing = false;")
            .EndBlock();

        code.EndBlock()
            .EndBlock();

        return code;
    }

    private static CodeWriter AppendProfilerMarker(this CodeWriter code, in ViewDataSpan data, ITypeSymbol viewModelType)
    {
        var viewModelTypeName = viewModelType.ToDisplayString();

        return code.AppendMultiline(
            $"""
            #if {ASPID_MVVM_PROFILER}
            {GeneratedCodeViewAttribute}
            {EditorBrowsableAttributeNever}
            private readonly static {ProfilerMarker} __initialize{AspidTypeDeclaration.Identifier(viewModelTypeName)}Marker = new("{data.Declaration.Identifier.Text}.{viewModelTypeName}.Initialize");
            #endif
            """);
    }
}
