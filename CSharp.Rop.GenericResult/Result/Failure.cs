using JetBrains.Annotations;

namespace CSharp.Rop.GenericResult
{
    public partial struct Result
    {
        public static Result Failure(Error error)
        {
            return error;
        }

        public static Result Failure([NotNull] string message)
        {
            return new Error(message);
        }

        public static Result<T> Failure<T>(Error error)
        {
            return error;
        }

        public static Result<T> Failure<T>([NotNull] string message)
        {
            return new Error(message);
        }
    }
}
