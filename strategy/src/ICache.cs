namespace strategy
{
    public interface ICache
    {
        string Get(string key);
        bool Set(string key);
        bool Exit(string key);
    }
}