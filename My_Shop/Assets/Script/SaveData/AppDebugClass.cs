using System;
using UnityEngine;

public class AppDebugClass
{
#if UNITY_EDITOR
    
    private static readonly string logFileUrl = Application.dataPath + "/StreamingAssets/DebugFileV1.0.txt";

    //现有存档
    public static readonly string pyDataString = Application.dataPath + "/StreamingAssets/PyDataSave";
    public static readonly string gdsDataString = Application.dataPath + "/StreamingAssets/GdsDataSave";

    //旧存档
    public static readonly string pyDataString1 = Application.dataPath + "/StreamingAssets/PyDataSave_old";
    public static readonly string gdsDataString1 = Application.dataPath + "/StreamingAssets/GdsDataSave_old";

#elif UNITY_ANDROID  && !UNITY_EDITOR

    private static readonly string logFileUrl = Application.persistentDataPath + "/DebugFileV1.0.txt";

    public static readonly string pyDataString = Application.persistentDataPath + "/PyDataSave";
    public static readonly string gdsDataString = Application.persistentDataPath + "/GdsDataSave";

    public static readonly string pyDataString1 = Application.persistentDataPath + "/PyDataSave_old";
    public static readonly string gdsDataString1 = Application.persistentDataPath + "/GdsDataSave_old";
#endif

    private static bool enabledLogFile = true;

    /// <summary>
    /// UnityLog附加打印方法
    /// </summary>
    /// <param name="logContent">log内容</param>
    /// <param name="logScript">log具体代码位置</param>
    /// <param name="logType">log类型</param>
    public static void LogForUnityLog(string logContent, string logScript, LogType logType)
    {
        string logStr = GetNowTime();
        switch (logType)
        {
            case LogType.Error:
                logStr += "__DebugError__";
                logStr += (logContent + "\n");
                logStr += logScript;
                WriteFLogToFile(logStr);
                break;
            case LogType.Assert:
                logStr += "__DebugAssert__";
                logStr += (logContent + "\n");
                logStr += logScript;
                WriteFLogToFile(logStr);
                break;
            case LogType.Warning:
                //logStr += "__AppDebugWarning__";
                //logStr += (logContent + "\n");
                //logStr += logScript;
                //WriteFLogToFile(logStr);
                break;
            case LogType.Log:
                logStr += "__DebugLog__";
                logStr += (logContent + "\n");
                logStr += logScript;
                WriteFLogToFile(logStr);
                break;
            case LogType.Exception:
                logStr += "__DebugException__";
                logStr += (logContent + "\n");
                logStr += logScript;
                WriteFLogToFile(logStr);
                break;
        }
    }

    private static void WriteFLogToFile(string str)
    {
        if (enabledLogFile)
        {
            enabledLogFile = false;
            string logText = "";
            if (System.IO.File.Exists(logFileUrl))
            {
                logText = System.IO.File.ReadAllText(logFileUrl);
            }
            logText += ("\n" + str);
            System.IO.File.WriteAllText(logFileUrl, logText);
            enabledLogFile = true;
        }
    }

    //获取当前时间字符串
    private static string GetNowTime()
    {
        return DateTime.Now.ToString();
        // DateTime.Now.ToLocalTime().ToString(); // 2008-9-4 20:12:12
    }
}