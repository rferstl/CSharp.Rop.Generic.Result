using JetBrains.Annotations;

namespace CSharp.Rop.Generic.Result.Tests
{
    public abstract class TestBase
    {
        protected class T
        {
            [NotNull] public static readonly T Value = new T();
        }

        protected class E
        {
            [NotNull] public static readonly E Value = new E();
        }

        protected class Discard
        {
            [NotNull] public static readonly Discard Value = new Discard();
        }
    }
}
