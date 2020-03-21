using System;
using JetBrains.Annotations;

namespace CSharp.Rop.GenericResult
{
    [PublicAPI]
    [Serializable]
    public partial struct Result : IResult
    {
        private readonly Error _error;
        public bool IsFailure { get; }
        public bool IsSuccess => !IsFailure;
        public Error Error => IsFailure ? _error : throw new ResultSuccessException();

        private Result(bool isFailure, Error error)
        {
            if (isFailure)
            {
                if (string.IsNullOrEmpty(error.Message))
                    throw new ArgumentNullException(nameof(error), Messages.ErrorObjectIsNotProvidedForFailure);
            }
            else
            {
                if (!string.IsNullOrEmpty(error.Message))
                    throw new ArgumentException(Messages.ErrorObjectIsProvidedForSuccess, nameof(error));
            }
            IsFailure = isFailure;
            _error = error;
        }

        public static implicit operator Result(Error error) => new Result(true, error);

    }


}
