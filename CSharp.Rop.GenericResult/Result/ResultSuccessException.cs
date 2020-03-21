using System;

namespace CSharp.Rop.GenericResult
{
    public class ResultSuccessException : Exception
    {
        internal ResultSuccessException()
            : base(Result.Messages.ErrorIsInaccessibleForSuccess)
        {
        }
    }
}
