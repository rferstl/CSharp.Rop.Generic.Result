using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace CSharp.Rop.GenericResult
{
    public partial struct Result
    {
        public static Result Combine([NotNull] IEnumerable<Result> results, string errorMessagesSeparator = null)
        {
            var failedResults = results.Where(x => x.IsFailure).ToList();

            if (failedResults.Count == 0)
                return Success();

            var errorMessage = string.Join(errorMessagesSeparator ?? ErrorMessagesSeparator, failedResults.Select(x => x.Error.Message));
            return new Error(errorMessage);
        }

        public static Result Combine<T>([NotNull] IEnumerable<Result<T>> results, string errorMessagesSeparator = null)
        {
            var failedResults = results.Where(x => x.IsFailure).ToList();

            if (failedResults.Count == 0)
                return Success();

            var errorMessage = string.Join(errorMessagesSeparator ?? ErrorMessagesSeparator, failedResults.Select(x => x.Error.Message));
            return new Error(errorMessage);
        }

        /// <summary>
        /// Combines several results (and any error messages) into a single result.
        /// The returned result will be a failure if any of the input <paramref name="results"/> are failures.
        /// Error messages are concatenated with the default <see cref="Result.ErrorMessagesSeparator" /> between each message.
        /// </summary>
        /// <param name="results">The Results to be combined.</param>
        /// <returns>A Result that is a success when all the input <paramref name="results"/> are also successes.</returns>
        public static Result Combine([NotNull] params Result[] results)
            => Combine(results, ErrorMessagesSeparator);

        /// <summary>
        /// Combines several results (and any error messages) into a single result.
        /// The returned result will be a failure if any of the input <paramref name="results"/> are failures.
        /// Error messages are concatenated with the specified <paramref name="errorMessagesSeparator"/> between each message.
        /// </summary>
        /// <param name="errorMessagesSeparator">The string to use as a separator. If omitted, the default <see cref="Result.ErrorMessagesSeparator" /> is used instead.</param>
        /// <param name="results">The Results to be combined.</param>
        /// <returns>A Result that is a success when all the input <paramref name="results"/> are also successes.</returns>
        public static Result Combine(string errorMessagesSeparator, [NotNull] params Result[] results)
            => Combine(results, errorMessagesSeparator);

        public static Result Combine<T>([NotNull] params Result<T>[] results)
            => Combine(results, ErrorMessagesSeparator);

        public static Result Combine<T>(string errorMessagesSeparator, [NotNull] params Result<T>[] results)
            => Combine(results, errorMessagesSeparator);
    }
}
