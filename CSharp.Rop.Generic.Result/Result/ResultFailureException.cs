using System;

namespace CSharp.Rop.Generic.Result
{
    public class ResultFailureException : Exception
    {
        public string Error { get; }

        internal ResultFailureException(string error)
            : base(Result.Messages.ValueIsInaccessibleForFailure)
        {
            Error = error;
        }
    }
}
