namespace DotaApiViewer
{
    public struct QueryParameter
    {
        public readonly string Key;
        public readonly string Value;

        public QueryParameter(string key, string value)
        {
            Key = key;
            Value = value;
        }
        
        public static QueryParameter Create<T>(string key, T value)
        {
            return new QueryParameter(key, value.ToString());
        }
    }
}
