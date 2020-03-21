using System;
using JetBrains.Annotations;

namespace CSharp.Rop.GenericResult
{
    public static partial class ResultExtensions
    {
        /// <summary>
        ///     Executes the given action if the calling result is a success and condition is true. Returns the calling result.
        /// </summary>
        public static Result TapIf(this Result result, bool condition, [NotNull] Action action)
        {
            if (condition)
                return result.Tap(action);
            return result;
        }

        /// <summary>
        ///     Executes the given action if the calling result is a success and condition is true. Returns the calling result.
        /// </summary>
        public static Result<T> TapIf<T>(this Result<T> result, bool condition, [NotNull] Action action)
        {
            if (condition)
                return result.Tap(action);
            return result;
        }

        /// <summary>
        ///     Executes the given action if the calling result is a success and condition is true. Returns the calling result.
        /// </summary>
        public static Result<T> TapIf<T>(this Result<T> result, bool condition, [NotNull] Action<T> action)
        {
            if (condition)
                return result.Tap(action);
            return result;
        }

        /// <summary>
        ///     Executes the given action if the calling result is a success and condition is true. Returns the calling result.
        /// </summary>
        public static Result TapIf<T>(this Result result, bool condition, [NotNull] Func<T> func)
        {
            if (condition)
                return result.Tap(func);
            return result;
        }

        /// <summary>
        ///     Executes the given action if the calling result is a success and condition is true. Returns the calling result.
        /// </summary>
        public static Result<T> TapIf<T, TK>(this Result<T> result, bool condition, [NotNull] Func<TK> func)
        {
            if (condition)
                return result.Tap(func);
            return result;
        }

        /// <summary>
        ///     Executes the given action if the calling result is a success and condition is true. Returns the calling result.
        /// </summary>
        public static Result<T> TapIf<T, TK>(this Result<T> result, bool condition, [NotNull] Func<T, TK> func)
        {
            if (condition)
                return result.Tap(func);
            return result;
        }

    }
}
