
public interface ILock
{
    void IncLock();

    void DecLock();

    int GetLockCnt();
}