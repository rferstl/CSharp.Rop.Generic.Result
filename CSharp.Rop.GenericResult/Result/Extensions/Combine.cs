using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace CSharp.Rop.GenericResult
{
    public static partial class ResultExtensions
    {

        public static Result Combine([NotNull] this IEnumerable<Result> results, string errorMessageSeparator = null)
            => Result.Combine(results, errorMessageSeparator);

        public static Result<IEnumerable<T>> Combine<T>([NotNull] this IEnumerable<Result<T>> results, string errorMessageSeparator = null)
        {
            CombineInternal(results, out var values, out var errorMessages, out var nones);

            if (errorMessages.Count > 0)
                return new Error(string.Join(errorMessageSeparator ?? Result.ErrorMessagesSeparator, errorMessages));

            if (values.Count == 0 && nones > 0)
                return Result.None;

            return Result.Success((IEnumerable<T>)values);
        }

        public static Result<TK> Combine<T, TK>([NotNull] this IEnumerable<Result<T>> results, [NotNull] Func<IEnumerable<T>, TK> composer,
            string errorMessageSeparator = null)
        {
            CombineInternal(results, out var values, out var errorMessages, out var nones);

            if (errorMessages.Count > 0)
                return new Error(string.Join(errorMessageSeparator ?? Result.ErrorMessagesSeparator, errorMessages));
 
            if (values.Count == 0 && nones > 0)
                return Result.None;

            return composer(values);
        }

        private static void CombineInternal<T>([NotNull] IEnumerable<Result<T>> results, [NotNull] out List<T> values, [NotNull] out List<string> errorMessages, out int nones)
        {
            errorMessages = new List<string>();
            values = new List<T>();
            nones = 0;
            foreach (var result in results)
            {
                if (result.IsFailure)
                    errorMessages.Add(result.Error.Message);
                else if (result.HasValue)
                    values.Add(result.Value);
                else
                    nones++;
            }
        }
    }
}
