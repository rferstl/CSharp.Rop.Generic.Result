using System;
using JetBrains.Annotations;

namespace CSharp.Rop.GenericResult
{
    public static partial class ResultExtensions
    {
        public static Result<T> Elvis<T>(this Result<T> result, [NotNull] Func<Result<T>> func)
        {
            if (result.IsNone)
                return func();

            return result;
        }

        public static Result<T> Coalesce<T>(this Result<T> result, [NotNull] Func<Result<T>> func)
        {
            if (result.IsNone)
                return func();

            return result;
        }

    }
}
