using System;
using JetBrains.Annotations;

namespace CSharp.Rop.GenericResult
{
    [PublicAPI]
    public static partial class ResultExtensions
    {
        /// <summary>
        ///     Executes the given action if the calling result is a failure. Returns the calling result.
        /// </summary>
        public static Result<T> OnFailure<T>(this Result<T> result, [NotNull] Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        /// <summary>
        ///     Executes the given action if the calling result is a failure. Returns the calling result.
        /// </summary>
        public static Result OnFailure(this Result result, [NotNull] Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        /// <summary>
        ///     Executes the given action if the calling result is a failure. Returns the calling result.
        /// </summary>
        public static Result<T> OnFailure<T>(this Result<T> result, [NotNull] Action<Error> action)
        {
            if (result.IsFailure)
            {
                action(result.Error);
            }

            return result;
        }

        /// <summary>
        ///     Executes the given action if the calling result is a failure. Returns the calling result.
        /// </summary>
        public static Result OnFailure(this Result result, [NotNull] Action<Error> action)
        {
            if (result.IsFailure)
            {
                action(result.Error);
            }

            return result;
        }
    }
}
