using System;

namespace CSharp.Rop.GenericResult
{
    public class ResultNoneException : Exception
    {
        internal ResultNoneException()
            : base(Result.Messages.ValueIsInaccessibleForNone)
        {
        }
    }
}