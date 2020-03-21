using System;
using JetBrains.Annotations;

namespace CSharp.Rop.Generic.Result
{
    public static partial class ResultExtensions
    {
        public static Result<T> OnFailureCompensate<T>(this Result<T> result, [NotNull] Func<Result<T>> func)
        {
            if (result.IsFailure)
                return func();

            return result;
        }

        public static Result OnFailureCompensate(this Result result, [NotNull] Func<Result> func)
        {
            if (result.IsFailure)
                return func();

            return result;
        }

        public static Result<T> OnFailureCompensate<T>(this Result<T> result, [NotNull] Func<Error, Result<T>> func)
        {
            if (result.IsFailure)
                return func(result.Error);

            return result;
        }

        public static Result OnFailureCompensate(this Result result, [NotNull] Func<Error, Result> func)
        {
            if (result.IsFailure)
                return func(result.Error);

            return result;
        }
    }
}
