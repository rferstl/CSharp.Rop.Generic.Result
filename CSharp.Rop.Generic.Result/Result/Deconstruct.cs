namespace CSharp.Rop.Generic.Result
{
    public static partial class ResultExtensions
    {
        public static void Deconstruct(this Result result, out bool isSuccess, out Error error)
        {
            isSuccess = result.IsSuccess;
            error = result.IsFailure ? result.Error : default(Error);
        }

        public static void Deconstruct<T>(this Result<T> result, out bool isSuccess, out bool hasValue)
        {
            (isSuccess, hasValue, _) = result;
        }

        public static void Deconstruct<T>(this Result<T> result, out bool isSuccess, out bool hasValue, out T value)
        {
            (isSuccess, hasValue, value, _) = result;
        }

        public static void Deconstruct<T>(this Result<T> result, out bool isSuccess, out bool hasValue, out T value, out Error error)
        {
            isSuccess = result.IsSuccess;
            hasValue = result.HasValue;
            value = result.IsSuccess && result.HasValue ? result.Value : default(T);
            error = result.IsFailure ? result.Error : default(Error);
        }

    }
}
