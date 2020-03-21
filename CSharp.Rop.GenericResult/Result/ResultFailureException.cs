using System;

namespace CSharp.Rop.GenericResult
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
