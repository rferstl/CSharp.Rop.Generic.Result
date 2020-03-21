using System;
using JetBrains.Annotations;

namespace CSharp.Rop.GenericResult
{
    public static partial class ResultExtensions
    {
        public static TK Match<TK, T>(this Result<T> result, [NotNull] Func<T, TK> onSuccessHasValue, [NotNull] Func<TK> onSuccessIsNone, [NotNull] Func<Error, TK> onFailure)
        {
            return result.IsFailure ? onFailure(result.Error) 
                : result.HasValue ? onSuccessHasValue(result.Value)
                : onSuccessIsNone();
        }

        public static T Match<T>(this Result result, [NotNull] Func<T> onSuccess, [NotNull] Func<Error, T> onFailure)
        {
            return result.IsSuccess
                ? onSuccess()
                : onFailure(result.Error);
        }

        public static void Match<T>(this Result<T> result, [NotNull] Action<T> onSuccessHasValue, [NotNull] Action onSuccessIsNone, [NotNull] Action<Error> onFailure)
        {
            if (result.IsFailure)
                onFailure(result.Error);
            else if (result.HasValue)
                onSuccessHasValue(result.Value);
            else
                onSuccessIsNone();
        }

        public static void Match(this Result result, [NotNull] Action onSuccess, [NotNull] Action<Error> onFailure)
        {
            if (result.IsSuccess)
                onSuccess();
            else
                onFailure(result.Error);
        }
    }
}
