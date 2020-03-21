using JetBrains.Annotations;

namespace CSharp.Rop.Generic.Result
{
    public partial struct Result
    {
        /// <summary>
        /// Returns first failure in the list of <paramref name="results"/>. If there is no failure returns success.
        /// </summary>
        /// <param name="results">List of results.</param>
        public static Result FirstFailureOrSuccess([NotNull] params Result[] results)
        {
            foreach (var result in results)
            {
                if (result.IsFailure)
                    return Failure(result.Error);
            }

            return Success();
        }
    }
}
