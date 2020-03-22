namespace CSharp.Rop.GenericResult
{
    public partial struct Result<T>
    {
        /// <summary>
        ///     Return the value if IsSuccess and HasValue or default.
        /// </summary>
        public T ValueOrDefault()
        {
            if (IsFailure || IsNone)
                return default(T);

            return _value;
        }

    }
}
