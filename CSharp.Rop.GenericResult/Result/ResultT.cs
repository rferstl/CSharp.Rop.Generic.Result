using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace CSharp.Rop.GenericResult
{
    [PublicAPI]
    [Serializable]
    public partial struct Result<T> : IResult, IOption, IValue<T>, IEquatable<Result<T>>
    {

        private readonly Error _error;
        public bool IsFailure { get; }
        public bool IsSuccess => !IsFailure;
        public Error Error => IsFailure ? _error : throw new ResultSuccessException();

        private readonly T _value;
        public T Value => IsSuccess && HasValue ? _value : throw new ResultFailureException(Error.Message);

        public static Result<T> None => new Result<T>(false, default(Error), false, default(T));

        public bool HasValue { get; }
        public bool IsNone => !HasValue;

        internal Result(bool isFailure, Error error, bool hasValue, T value)
        {
            if (isFailure)
            {
                if (string.IsNullOrEmpty(error.Message))
                    throw new ArgumentNullException(nameof(error), Result.Messages.ErrorObjectIsNotProvidedForFailure);
                if (hasValue)
                    throw new ArgumentNullException(Result.Messages.HasValueIsProvidedForFailure, nameof(hasValue));
            }
            else
            {
                if(hasValue && ReferenceEquals(value, null))
                    throw new ArgumentNullException(Result.Messages.ValidValueObjectCannotBeNull, nameof(value));
                if (!string.IsNullOrEmpty(error.Message))
                    throw new ArgumentException(Result.Messages.ErrorObjectIsProvidedForSuccess, nameof(error));
            }
            IsFailure = isFailure;
            _error = isFailure ? error : default(Error);
            HasValue = hasValue;
            _value = hasValue ? value : default(T);
        }

        #region implicit casts

        public static implicit operator Result(Result<T> result)
        {
            if (result.IsSuccess)
                return Result.Success();
            return Result.Failure(result.Error);
        }

        public static implicit operator Result<T>(NoneType value) => new Result<T>(false, default(Error), false, default(T));
        public static implicit operator Result<T>(Error error) => new Result<T>(true, error, false, default(T));
        public static implicit operator Result<T>(T value) => new Result<T>(false, default(Error), true, value);

        #endregion

        #region equality members

        public bool Equals(Result<T> other)
        {
            if (IsFailure && other.IsFailure)
                return _error.Equals(other._error);
            if(IsNone && other.IsNone)
                return true;
            if (HasValue && other.HasValue)
                return EqualityComparer<T>.Default.Equals(_value, other._value);
            return false;
        }

        public override bool Equals(object obj)
        {
            return obj is Result<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _error.GetHashCode();
                hashCode = (hashCode * 397) ^ EqualityComparer<T>.Default.GetHashCode(_value);
                hashCode = (hashCode * 397) ^ IsFailure.GetHashCode();
                hashCode = (hashCode * 397) ^ IsNone.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Result<T> left, Result<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Result<T> left, Result<T> right)
        {
            return !left.Equals(right);
        }

        #endregion

    }

}
