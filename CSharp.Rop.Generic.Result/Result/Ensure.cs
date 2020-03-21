using System;
using JetBrains.Annotations;

namespace CSharp.Rop.Generic.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        ///     Returns a new failure result if the predicate is false. Otherwise returns the starting result.
        /// </summary>
        public static Result<T> Ensure<T>(this Result<T> result, [NotNull] Func<T, bool> predicate, Error error)
        {
            if (result.IsFailure)
                return result;

            if (result.IsNone || !predicate(result.Value))
                return error;

            return result;
        }

        /// <summary>
        ///     Returns a new failure result if the predicate is false. Otherwise returns the starting result.
        /// </summary>
        public static Result Ensure(this Result result, [NotNull] Func<bool> predicate, Error error)
        {
            if (result.IsFailure)
                return result;

            if (!predicate())
                return error;

            return result;
        }
    }
}
