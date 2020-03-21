using System;
using JetBrains.Annotations;

namespace CSharp.Rop.Generic.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        ///     Executes the given action if the calling result is a success. Returns the calling result.
        /// </summary>
        public static Result Tap(this Result result, [NotNull] Action action)
        {
            if (result.IsSuccess)
                action();

            return result;
        }

        /// <summary>
        ///     Executes the given action if the calling result is a success. Returns the calling result.
        /// </summary>
        public static Result<T> Tap<T>(this Result<T> result, [NotNull] Action action)
        {
            if (result.IsSuccess && result.HasValue)
                action();

            return result;
        }

        /// <summary>
        ///     Executes the given action if the calling result is a success. Returns the calling result.
        /// </summary>
        public static Result<T> Tap<T>(this Result<T> result, [NotNull] Action<T> action)
        {
            if (result.IsSuccess && result.HasValue)
                action(result.Value);

            return result;
        }

        /// <summary>
        ///     Executes the given action if the calling result is a success. Returns the calling result.
        /// </summary>
        public static Result Tap<T>(this Result result, [NotNull] Func<T> func)
        {
            if (result.IsSuccess)
                func();

            return result;
        }

        /// <summary>
        ///     Executes the given action if the calling result is a success. Returns the calling result.
        /// </summary>
        public static Result<T> Tap<T, TK>(this Result<T> result, [NotNull] Func<TK> func)
        {
            if (result.IsSuccess && result.HasValue)
                func();

            return result;
        }

        /// <summary>
        ///     Executes the given action if the calling result is a success. Returns the calling result.
        /// </summary>
        public static Result<T> Tap<T, TK>(this Result<T> result, [NotNull] Func<T, TK> func)
        {
            if (result.IsSuccess && result.HasValue)
                func(result.Value);

            return result;
        }
    }
}
