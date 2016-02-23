using System.Threading;

public class Utils
{

    public static T EnterExisting<T>(T lck) where T : ILock
    {
        return EnterExisting(lck, "");
    }

    public static T EnterExisting<T>(T lck, string info) where T : ILock
    {
	    if (lck != null) {
            EALogger.Log("ZsfGlobals.EnterExisting  lck:" + lck + " info:" + info, EALogger.SEV_DEBUG_1);
            Monitor.Enter(lck);
            lck.IncLock();
            EALogger.Log("ZsfGlobals.EnterExisting  lck:" + lck + " entered, locks:" + lck.GetLockCnt(), EALogger.SEV_DEBUG_1);
        }
        else
        {
            EALogger.Log("ZsfGlobals.EnterExisting : lck object is null !   info:" + info, EALogger.SEV_WARNING);
	    }
	    return lck;
    }


    public static void ExitExisting(ILock lck)
    {
        ExitExisting(lck, "");
    }

    public static void ExitExisting(ILock lck, string info)
    { 
	    if (lck != null) {
            lck.DecLock();
            EALogger.Log("ZsfGlobals.ExitExisting  lck:" + lck + " info:" + info + " locks:" + lck.GetLockCnt(), EALogger.SEV_DEBUG_1);
            Monitor.Exit(lck);
	    } else {
            EALogger.Log("ZsfGlobals.ExitExisting : lck object is null !   info:" + info, EALogger.SEV_WARNING);
	    }
    }

    public static bool Equal(object o1, object o2)
    {
        return null == o1 ? null == o2 : o1.Equals(o2);
    }
}