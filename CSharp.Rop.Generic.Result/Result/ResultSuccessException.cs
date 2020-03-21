using System;

namespace CSharp.Rop.Generic.Result
{
    public class ResultSuccessException : Exception
    {
        internal ResultSuccessException()
            : base(Result.Messages.ErrorIsInaccessibleForSuccess)
        {
        }
    }
}
