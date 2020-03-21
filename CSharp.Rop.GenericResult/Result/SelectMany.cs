using System;
using JetBrains.Annotations;

namespace CSharp.Rop.GenericResult
{
    public static partial class ResultExtensions
    {
        /// <summary>
        ///     This method should be used in linq queries. We recommend using Bind method.
        /// </summary>
        public static Result<TR> SelectMany<T, TK, TR>(
            this Result<T> result,
            [NotNull] Func<T, Result<TK>> func,
            [NotNull] Func<T, TK, TR> project)
        {
            return result
                .Bind(func)
                .Map(x => project(result.Value, x));
        }

    }
}