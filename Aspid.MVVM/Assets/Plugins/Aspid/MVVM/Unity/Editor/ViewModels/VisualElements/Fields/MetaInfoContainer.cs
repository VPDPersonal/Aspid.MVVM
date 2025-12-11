using System;
using System.Linq;
using UnityEngine;
using System.Reflection;
using System.Diagnostics;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class MetaInfoContainer : VisualElement
    {
        private static readonly StyleLength _cellPadding = 2;
        private static readonly StyleFloat _cellBorderWidth = 1;
        private static readonly StyleColor _gridColor = new Color(0.1f, 0.1f, 0.1f, 1f);
        private const BindingFlags BindingFlags = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic;
        
        internal MetaInfoContainer(IFieldContext context, Action closeCallback)
        {
            try
            {
                this.SetDisplay(DisplayStyle.None)
                    .AddChild(new VisualElement()
                        .SetMargin(top: -7, bottom: 2, right: -9)
                        .SetFlexDirection(FlexDirection.Row)
                        .AddChild(new Label("Meta")
                            .SetUnityTextAlign(TextAnchor.MiddleLeft)
                            .SetFlexGrow(1))
                        .AddChild(CreateButton(closeCallback)))
                    .AddChildIfNotNull(CreateIdField(context.Member))
                    .AddChildIfNotNull(CreateTypeField(context))
                    .AddChildIfNotNull(CreateAttributes(context.Member))
                    .AddChildIfNotNull(CreateBindableField(context.Target, context.Member));
            }
            catch (Exception e)
            {
                Clear();
                this.SetDisplay(DisplayStyle.None)
                    .AddChild(new HelpBox(e.Message, HelpBoxMessageType.Error));
            }
        }

        private Button CreateButton(Action closeCallback)
        {
            return new Button(() =>
                {
                    this.SetDisplay(DisplayStyle.None);
                    closeCallback?.Invoke();
                })
                .SetText("▼")
                .SetBorderColor(_gridColor)
                .SetSize(width: 24, height: 20)
                .SetBorderRadius(topRight: 5, bottomRight: 0);
        }

        private static VisualElement CreateIdField(MemberInfo memberInfo)
        {
            const string label = "Id";
            var bindIdAttribute = memberInfo.GetCustomAttribute<BindIdAttribute>();
            
            if (bindIdAttribute is not null) return CreateCells(label, bindIdAttribute.Id);
            
            if (memberInfo is FieldInfo fieldInfo && fieldInfo.IsDefined(typeof(BaseBindAttribute))) 
                return CreateCells(label, fieldInfo.GetGeneratedPropertyName());
            
            if (memberInfo is MethodInfo methodInfo && methodInfo.IsDefined(typeof(RelayCommand))) 
                return CreateCells(label, methodInfo.Name + "Command");

            return null;
        }

        private static VisualElement CreateTypeField(IFieldContext context)
        {
            var memberType = context.MemberType;

            // if (memberInfo is FieldInfo fieldInfo)
            // {
            //     memberType = fieldInfo.FieldType;
            // }
            // if (memberInfo is MethodInfo methodInfo && methodInfo.IsDefined(typeof(RelayCommand)))
            // {
            //     var parameters = methodInfo.GetParameters();
            //     var parameterTypes = parameters.Select(p => p.ParameterType).ToArray();
            //
            //     memberType = parameters.Length switch
            //     {
            //         0 => typeof(RelayCommand),
            //         1 => typeof(RelayCommand<>).MakeGenericType(parameterTypes),
            //         2 => typeof(RelayCommand<,>).MakeGenericType(parameterTypes),
            //         3 => typeof(RelayCommand<,,>).MakeGenericType(parameterTypes),
            //         4 => typeof(RelayCommand<,,,>).MakeGenericType(parameterTypes),
            //         _ => null
            //     };
            // }

            return memberType is null
                ? null
                : CreateCells(label: "Type", memberType.GetTypeDisplayName());
        }

        private static VisualElement CreateAttributes(MemberInfo memberInfo)
        {
            var attributes = memberInfo.GetCustomAttributes(false)
                .Where(attribute => attribute 
                    is not CompilerGeneratedAttribute 
                    and not DebuggerBrowsableAttribute)
                .ToArray();
            
            return attributes.Any() 
                ? CreateCells(label: "Attributes", string.Join(", ", attributes.Select(attribute => attribute.GetType().Name)), TextOverflowPosition.End)
                : null;
        }
        
        private static VisualElement CreateBindableField(object obj, MemberInfo memberInfo)
        {
            string bindableName = null;

            if (memberInfo is FieldInfo fieldInfo)
            {
                if (fieldInfo.IsDefined(typeof(BaseBindAttribute)))
                {
                    var fieldNameWithoutPrefix = fieldInfo.RemovePrefix();
                    var firstChar = char.ToLower(fieldNameWithoutPrefix[0]);
                    bindableName = $"__{firstChar + fieldNameWithoutPrefix[1..]}Bindable";
                }
            }
            else if (memberInfo is MethodInfo methodInfo && methodInfo.IsDefined(typeof(RelayCommand)))
            {
                var firstChar = char.ToLower(methodInfo.Name[0]);
                bindableName = $"__{firstChar + methodInfo.Name[1..]}Bindable";
            }
            
            if (bindableName is null) return null;
            
            var bindableField = obj
                .GetType()
                .GetFieldInfosIncludingBaseClasses(BindingFlags)
                .FirstOrDefault(field => field.IsDefined(typeof(GeneratedCodeAttribute)) && field.Name == bindableName);
            
            var bindableValue = bindableField?.GetValue(obj);
            if (bindableValue is null) return null;
            
            var changedEvent = bindableField.FieldType.GetFieldInfosIncludingBaseClasses(BindingFlags)
                .FirstOrDefault(field => field.Name == "Changed");
            
            if (changedEvent is null) return null;

            var delegateValue = changedEvent.GetValue(bindableValue) as Delegate;
            VisualElement delegateField = delegateValue is null
                ? new DebugNullField("Bindable", changedEvent.FieldType)
                : new AspidDelegateField(label: "Bindable", delegateValue);

            return new VisualElement()
                .SetBorderWidth(1)
                .SetMargin(top: -1)
                .SetBorderColor(_gridColor)
                .AddChild(delegateField);
        }
        
        private static VisualElement CreateCells(string label, string value, TextOverflowPosition textOverflowPosition = TextOverflowPosition.Middle)
        {
            var width = new StyleLength(new Length(50, LengthUnit.Percent));

            return new VisualElement()
                .SetMargin(top: -1)
                .SetFlexDirection(FlexDirection.Row)
                .AddChild(CreateLabel(label, TextOverflowPosition.End))
                .AddChild(CreateLabel(value, textOverflowPosition)
                    .SetFlexGrow(1)
                    .SetMargin(left: -1));

            Label CreateLabel(string text, TextOverflowPosition overflow) => new Label(text)
                .SetSize(width: width)
                .SetPadding(_cellPadding)
                .SetBorderColor(_gridColor)
                .SetOverflow(Overflow.Hidden)
                .SetBorderWidth(_cellBorderWidth)
                .SetUnityTextOverflowPosition(overflow)
                .SetTextOverflow(TextOverflow.Ellipsis);
        }
    }
}
