using System;
using JetBrains.Annotations;

namespace CSharp.Rop.GenericResult
{
    public partial struct Result
    {
        [NotNull] private static readonly Func<Exception, Error> DefaultTryErrorHandler = exc => new Error(exc?.Message ?? throw new InvalidOperationException());

        public static Result Try([NotNull] Action action, Func<Exception, Error> errorHandler = null)
        {
            errorHandler = errorHandler ?? DefaultTryErrorHandler;

            try
            {
                action();
                return Success();
            }
            catch (Exception exc)
            {
                var error = errorHandler(exc);
                return error;
            }
        }

        public static Result<T> Try<T>([NotNull] Func<T> func, Func<Exception, Error> errorHandler = null)
        {
            errorHandler = errorHandler ?? DefaultTryErrorHandler;

            try
            {
                return func();
            }
            catch (Exception exc)
            {
                var error = errorHandler(exc);
                return error;
            }
        }

    }
}
