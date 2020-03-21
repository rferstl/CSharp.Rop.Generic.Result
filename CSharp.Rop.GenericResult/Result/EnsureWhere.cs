using System;
using JetBrains.Annotations;

namespace CSharp.Rop.GenericResult
{
    public static partial class ResultExtensions
    {

        /// <summary>
        ///     Creates a new result from the return value of a given function. If the calling Result is a failure, a new failure result is returned instead.
        /// </summary>
        public static Result<T> EnsureWhere<T>(this Result<T> result, [NotNull] Func<T, bool> predicate, Error error)
        {
            if (result.IsFailure)
                return result;
            //TODO: is this the best way or also return no value
            if (result.IsNone || !predicate(result.Value))
                return error;

            return result;
        }

    }
}
