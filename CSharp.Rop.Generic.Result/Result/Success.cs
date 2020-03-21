using JetBrains.Annotations;

namespace CSharp.Rop.Generic.Result
{
    public partial struct Result
    {
        public static Result Success()
        {
            return new Result(false, default(Error));
        }

        public static Result<T> Success<T>([NotNull] T value)
        {
            return value;
        }
    }
}
