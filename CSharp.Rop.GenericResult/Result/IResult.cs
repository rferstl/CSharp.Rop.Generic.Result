using JetBrains.Annotations;

namespace CSharp.Rop.GenericResult
{
    [PublicAPI]
    public interface IResult
    {
        bool IsFailure { get; }
        bool IsSuccess { get; }
    }

    [PublicAPI]
    public interface IValue<out T>
    {
        T Value { get; }
    }
}
