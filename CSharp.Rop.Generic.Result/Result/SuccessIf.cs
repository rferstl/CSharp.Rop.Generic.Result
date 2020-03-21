using System;
using JetBrains.Annotations;

namespace CSharp.Rop.Generic.Result
{
    public partial struct Result
    {
        public static Result SuccessIf(bool isSuccess, Error error)
        {
            return isSuccess
                ? Success()
                : Failure(error);
        }

        public static Result SuccessIf([NotNull] Func<bool> predicate, Error error)
        {
            return SuccessIf(predicate(), error);
        }

        public static Result<T> SuccessIf<T>(bool isSuccess, [NotNull] T value, Error error)
        {
            return isSuccess
                ? Success(value)
                : Failure<T>(error);
        }

        public static Result<T> SuccessIf<T>([NotNull] Func<bool> predicate, [NotNull] T value, Error error)
        {
            return SuccessIf(predicate(), value, error);
        }

    }
}
