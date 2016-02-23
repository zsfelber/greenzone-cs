public class RootLock : ILock
{
    private int locks = 0;

    public void IncLock()
    {
        lock (this)
        {
            locks++;
        }
    }

    public void DecLock()
    {
        lock (this)
        {
            locks--;
        }
    }

    public int GetLockCnt()
    {
        lock (this)
        {
            return locks;
        }
    }
}
