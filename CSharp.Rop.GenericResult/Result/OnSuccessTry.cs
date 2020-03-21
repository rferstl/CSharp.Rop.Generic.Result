using System;
using JetBrains.Annotations;

namespace CSharp.Rop.GenericResult
{
    public static partial class ResultExtensions
    {
        public static Result OnSuccessTry(this Result result, [NotNull] Action action, Func<Exception, Error> errorHandler = null)
        {
            return result.IsFailure
                ? result
                : Result.Try(action, errorHandler);
        }

        public static Result<T> OnSuccessTry<T>(this Result result, [NotNull] Func<T> func, Func<Exception, Error> errorHandler = null)
        {
            return result.IsFailure
                ? Result.Failure<T>(result.Error)
                : Result.Try(func, errorHandler);
        }

        public static Result OnSuccessTry<T>(this Result<T> result, [NotNull] Action<T> action, Func<Exception, Error> errorHandler = null)
        {
            return result.IsFailure
                ? Result.Failure(result.Error)
                : Result.Try(() => action(result.Value), errorHandler);
        }

        public static Result<TK> OnSuccessTry<T, TK>(this Result<T> result, [NotNull] Func<T, TK> func, Func<Exception, Error> errorHandler = null)
        {
            return result.IsFailure
                ? Result.Failure<TK>(result.Error)
                : Result.Try(() => func(result.Value), errorHandler);
        }
    }
}
