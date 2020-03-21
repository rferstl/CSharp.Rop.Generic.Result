using JetBrains.Annotations;

namespace CSharp.Rop.GenericResult
{
    public partial struct Result
    {

        public static Result<T> From<T>([CanBeNull] T value)
        {
            if (ReferenceEquals(value, null))
                return None;
            return value;
        }

    }
}
