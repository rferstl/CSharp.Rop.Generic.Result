using System;

namespace CSharp.Rop.Generic.Result
{
    public partial struct Result
    {
        public Result<TK> ConvertFailure<TK>()
        {
            if (IsSuccess)
                throw new InvalidOperationException(Messages.ConvertFailureExceptionOnSuccess);

            return Failure<TK>(Error);
        }
    }

    // ReSharper disable once UnusedTypeParameter
    public partial struct Result<T>
    {
        public Result ConvertFailure()
        {
            if (IsSuccess)
                throw new InvalidOperationException(Result.Messages.ConvertFailureExceptionOnSuccess);

            return Result.Failure(Error);
        }

        public Result<TK> ConvertFailure<TK>()
        {
            if (IsSuccess)
                throw new InvalidOperationException(Result.Messages.ConvertFailureExceptionOnSuccess);

            return Result.Failure<TK>(Error);
        }
    }
}
