using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;


public class EALogger
{
    public const int SEV_PRINT = 0;
    public const int SEV_ERROR = 1;
    public const int SEV_WARNING = 2;
    public const int SEV_INFO = 3;
    public const int SEV_DEBUG_1 = 4;
    public const int SEV_DEBUG_2 = 5;
    public const int SEV_DEBUG_3 = 6;
    public const int SEV_DEBUG_4 = 7;
    public const int SEV_DEBUG_5 = 8;

    public delegate void DEALogger(String s, int severity, string filePath, string lineNumber, string function, string functionSign);

    public static event DEALogger DLog;

    // .NET 4.5
    //public static void Log(String s, int severity = SEV_PRINT, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, string function = "", string functionSign = "")

    // .NET 4.0 ->
    public static void Log(String s, int severity = SEV_PRINT, string filePath = null, string lineNumber = null, string function = null, string functionSign = null)
    {
#if (DEBUG)
        StackFrame frame = new StackTrace(true).GetFrame(1);
        if (filePath==null)
            filePath = frame.GetFileName();
        if (lineNumber == null)
            lineNumber = "" + frame.GetFileLineNumber() + ":" + frame.GetFileColumnNumber();
        if (function == null)
            function = frame.GetMethod().DeclaringType.Name + "." + frame.GetMethod().Name;
        if (functionSign == null)
            functionSign = "";
#endif
    // <- .NET 4.0

        DLog(s, severity, filePath, lineNumber, function, functionSign);
    }
}
