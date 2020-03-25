using System;
using JetBrains.Annotations;

namespace CSharp.Rop.GenericResult
{
    public static partial class ResultExtensions
    {

        /// <summary>
        ///     Selects result from the return value of a given function. If the calling Result is a failure, a new failure result is returned instead.
        /// </summary>
        public static Result<TK> Bind<T, TK>(this Result<T> result, [NotNull] Func<T, Result<TK>> func)
        {
            if (result.IsFailure)
                return result.Error;

            if (result.IsNone)
                return Result.None;

            return func(result.Value);
        }

        /// <summary>
        ///     Selects result from the return value of a given function. If the calling Result is a failure, a new failure result is returned instead.
        /// </summary>
        public static Result<TK> Bind<TK>(this Result result, [NotNull] Func<Result<TK>> func)
        {
            if (result.IsFailure)
                return result.Error;

            return func();
        }

        /// <summary>
        ///     Selects result from the return value of a given function. If the calling Result is a failure, a new failure result is returned instead.
        /// </summary>
        public static Result Bind<T>(this Result<T> result, [NotNull] Func<T, Result> func)
        {
            if (result.IsFailure)
                return result.Error;

            return func(result.Value);
        }

        /// <summary>
        ///     Selects result from the return value of a given function. If the calling Result is a failure, a new failure result is returned instead.
        /// </summary>
        public static Result Bind(this Result result, [NotNull] Func<Result> func)
        {
            if (result.IsFailure)
                return result;

            return func();
        }

    }
}
