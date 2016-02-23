public interface ICopiable<T>
{
    void Copy(T value, CopyMode copyMode=CopyMode.ALL);
}

public enum CopyMode
{
    ALL, CONFIG_ONLY, RUNTIME_ONLY
}
