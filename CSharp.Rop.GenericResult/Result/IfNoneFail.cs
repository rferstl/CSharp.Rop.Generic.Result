using System;
using JetBrains.Annotations;

namespace CSharp.Rop.GenericResult
{
    public static partial class ResultExtensions
    {
        /// <summary>
        ///     Return the given errror from func if the calling result is none. otherwise returns the calling result.
        /// </summary>
        public static Result<T> IfNoneFail<T>(this Result<T> result, [NotNull] Func<Error> func)
        {
            if (result.IsFailure)
                return result;

            if (result.IsNone)
                return func();

            return result;
        }

    }
}
