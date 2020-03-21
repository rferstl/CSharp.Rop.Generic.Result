using JetBrains.Annotations;

namespace CSharp.Rop.GenericResult
{
    [PublicAPI]
    public interface IOption
    {
        bool HasValue { get; }
        bool IsNone { get; }
    }

}
