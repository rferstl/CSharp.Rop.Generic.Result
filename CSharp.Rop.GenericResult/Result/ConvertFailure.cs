using System;

namespace CSharp.Rop.GenericResult
{
    public partial struct Result
    {
        public Result<TK> ConvertFailure<TK>()
        {
            if (IsSuccess)
                throw new InvalidOperationException(Messages.ConvertFailureExceptionOnSuccess);

            return Error;
        }
    }

    // ReSharper disable once UnusedTypeParameter
    public partial struct Result<T>
    {
        public Result ConvertFailure()
        {
            if (IsSuccess)
                throw new InvalidOperationException(Result.Messages.ConvertFailureExceptionOnSuccess);

            return Error;
        }

        public Result<TK> ConvertFailure<TK>()
        {
            if (IsSuccess)
                throw new InvalidOperationException(Result.Messages.ConvertFailureExceptionOnSuccess);

            return Error;
        }
    }
}
