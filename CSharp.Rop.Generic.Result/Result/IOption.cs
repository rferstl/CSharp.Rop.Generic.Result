using JetBrains.Annotations;

namespace CSharp.Rop.Generic.Result
{
    [PublicAPI]
    public interface IOption
    {
        bool HasValue { get; }
        bool IsNone { get; }
    }

}
