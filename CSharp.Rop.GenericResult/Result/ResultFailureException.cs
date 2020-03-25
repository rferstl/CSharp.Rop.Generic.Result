using System;

namespace CSharp.Rop.GenericResult
{
    public class ResultFailureException : Exception
    {
        public string Error { get; }

        internal ResultFailureException(string error)
            : base(Result.Messages.ValueIsInaccessibleForFailure, new ErrorException(error))
        {
            Error = error;
        }
    }

    public class ErrorException : Exception
    {
        public ErrorException(string error) : base(error)
        {
        }
    }
}
