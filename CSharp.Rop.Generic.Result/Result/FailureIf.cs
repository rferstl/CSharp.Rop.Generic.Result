using System;
using JetBrains.Annotations;

namespace CSharp.Rop.Generic.Result
{
    public partial struct Result
    {
        public static Result FailureIf(bool isFailure, Error error)
            => SuccessIf(!isFailure, error);

        public static Result FailureIf([NotNull] Func<bool> failurePredicate, Error error)
            => SuccessIf(!failurePredicate(), error);

        public static Result<T> FailureIf<T>(bool isFailure, [NotNull] T value, Error error)
            => SuccessIf(!isFailure, value, error);

        public static Result<T> FailureIf<T>([NotNull] Func<bool> failurePredicate, [NotNull] T value, Error error)
            => SuccessIf(!failurePredicate(), value, error);

    }
}
