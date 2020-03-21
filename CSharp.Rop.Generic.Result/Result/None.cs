namespace CSharp.Rop.Generic.Result
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
