// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// A converter from <see cref="double"/> to <see cref="bool"/>.
    /// </summary>
    /// <remarks>
    /// A named alias for <see cref="IConverter{TFrom, TTo}"/> closed over these two types.
    /// It exists because Unity before 2023.1 cannot serialize a <c>[SerializeReference]</c>
    /// field typed as an open generic, so a binder declares the field as this instead. From
    /// 2023.1 the generic form is used directly and this is a compatibility shim.
    /// </remarks>
    public interface IConverterDoubleToBool : IConverter<double, bool> { }
}