using System;
using JetBrains.Annotations;

namespace CSharp.Rop.GenericResult
{
    public static partial class ResultExtensions
    {
        /// <summary>
        ///     If result has value check for predicate. Otherwise return false.
        /// </summary>
        public static bool Check<T>(this Result<T> result, [NotNull] Func<T, bool> predicate)
        {
            if (result.IsFailure)
                return false;

            if (result.IsNone)
                return false;

            return predicate(result.Value);
        }

    }
}
