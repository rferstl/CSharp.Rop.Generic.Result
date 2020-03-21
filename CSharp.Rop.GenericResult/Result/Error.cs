using System;
using JetBrains.Annotations;

namespace CSharp.Rop.GenericResult
{
    [PublicAPI]
    [Serializable]
    public struct Error : IEquatable<Error>
    {

        public static readonly Error Empty = new Error(string.Empty);

        [NotNull] public string Message { get;  }
        public int Code { get; }

        public Error([NotNull] string message) : this(message, 0)
        {
        }

        public Error(int code) : this($"ErrorCode: {code}", code)
        {
        }

        public Error([NotNull] string message, int code)
        {
            Message = message;
            Code = code;
        }

        #region equality members

        public bool Equals(Error other)
        {
            return Message == other.Message && Code == other.Code;
        }

        public override bool Equals(object obj)
        {
            return obj is Error other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Message.GetHashCode() * 397) ^ Code;
            }
        }

        public static bool operator ==(Error left, Error right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Error left, Error right)
        {
            return !left.Equals(right);
        }

        #endregion
    }
}
