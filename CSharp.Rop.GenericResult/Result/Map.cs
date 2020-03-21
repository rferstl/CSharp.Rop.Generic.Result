using System;
using JetBrains.Annotations;

namespace CSharp.Rop.GenericResult
{
    public static partial class ResultExtensions
    {

        /// <summary>
        ///     Creates a new result from the return value of a given function. If the calling Result is a failure, a new failure result is returned instead.
        /// </summary>
        public static Result<TK> Map<T, TK>(this Result<T> result, [NotNull] Func<T, TK> func)
        {
            if (result.IsFailure)
                return result.Error;

            if (result.IsNone)
                return Result.None;

            return func(result.Value);
        }

        /// <summary>
        ///     Creates a new result from the return value of a given function. If the calling Result is a failure, a new failure result is returned instead.
        /// </summary>
        public static Result<TK> Map<TK>(this Result result, [NotNull] Func<TK> func)
        {
            if (result.IsFailure)
                return result.Error;

            return func();
        }
    }
}
