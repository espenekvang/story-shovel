namespace StoryShovel.VsTs
{
    internal class VsTsWorkObject
    {
        public string Op { get; }
        public string Path { get; }
        public string Value { get; }
        public VsTsWorkObject(string op, string path, string value)
        {
            Op = op;
            Path = path;
            Value = value;
        }
    }
}