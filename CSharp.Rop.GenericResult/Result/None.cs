namespace CSharp.Rop.GenericResult
{
    public struct NoneType
    {
        public static NoneType Inst = new NoneType();
    }

    public partial struct Result
    {
        public static NoneType None => NoneType.Inst;
    }
}
