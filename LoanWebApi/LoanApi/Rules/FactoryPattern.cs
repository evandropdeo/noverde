namespace LoanApi.Rules
{
    public class FactoryPattern<K, T> where T : class, K, new()
    {
        public static K CreateInstance()
        {
            K objK;

            objK = new T();

            return objK;
        }
    }
}
