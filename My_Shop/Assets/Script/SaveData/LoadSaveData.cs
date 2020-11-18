using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class LoadSaveData : MonoBehaviour
{
    public static LoadSaveData instance;

    [HideInInspector]
    public bool isLoadingSaveData;  //标记是否在加载存档

    [HideInInspector]
    public bool isHadSaveData;  //是否有存档

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        isLoadingSaveData = false;
        isHadSaveData = IsEventSaveData();

        //打印log附加代码
        Application.logMessageReceived += AppDebugClass.LogForUnityLog;
    }

    /// <summary>
    /// 是否已有完整存档
    /// </summary>
    /// <returns></returns>
    private bool IsEventSaveData()
    {
        string filePath0 = AppDebugClass.pyDataString;
        string filePath1 = AppDebugClass.wdDataString;

        if (File.Exists(filePath0) && File.Exists(filePath1))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 删除所有存档
    /// </summary>
    public void DeleteAllSaveData()
    {
        Debug.Log("删除存档");

        string filePath = AppDebugClass.pyDataString;
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        filePath = AppDebugClass.wdDataString;
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        SceneManager.LoadScene(0);
        Destroy(LoadJsonFile.instance.gameObject);
        Destroy(LoadSaveData.instance.gameObject);
        Destroy(PlayerSaveDataCS.instance.gameObject);
        PlayerPrefs.DeleteAll();
    }

    /// <summary>
    /// 存档json
    /// </summary>
    private void SaveByJson(PlayerDataClass save)
    {
        isLoadingSaveData = true;
        try
        {
            File.WriteAllText(AppDebugClass.pyDataString, JsonConvert.SerializeObject(save));
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.ToString());
        }
        isLoadingSaveData = false;
    }
    private void SaveByJson(GoodsListDataClass save)
    {
        isLoadingSaveData = true;
        try
        {
            File.WriteAllText(AppDebugClass.wdDataString, JsonConvert.SerializeObject(save));
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.ToString());
        }
        isLoadingSaveData = false;
    }
    /// <summary>
    /// 存储游戏
    /// </summary>
    /// <param name="indexFun">默认0：全部存储，1：存储pyData，2：存储wdData</param>
    public void SaveGameData(int indexFun = 0)
    {
        switch (indexFun)
        {
            case 0:
                SaveByJson(PlayerSaveDataCS.instance.pyData);
                SaveByJson(PlayerSaveDataCS.instance.gdsData);
                break;
            case 1:
                SaveByJson(PlayerSaveDataCS.instance.pyData);
                break;
            case 2:
                SaveByJson(PlayerSaveDataCS.instance.gdsData);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 备份存档,传入json字符
    /// </summary>
    /// <param name="pyDataStr"></param>
    /// <param name="wdDataStr"></param>
    private void BackupArchiveForGame(string pyDataStr, string wdDataStr)
    {
        string filePath0 = AppDebugClass.pyDataString1;
        string filePath1 = AppDebugClass.wdDataString1;

        try
        {
            File.WriteAllText(filePath0, pyDataStr);
            File.WriteAllText(filePath1, wdDataStr);

            Debug.Log("存档备份成功");
        }
        catch (System.Exception e)
        {
            Debug.LogError("存档备份失败 " + e.ToString());
        }
    }

    /// <summary>
    /// 读档json
    /// </summary>
    public void LoadByJson()
    {
        isLoadingSaveData = true;

        if (isHadSaveData)
        {
            string filePath0 = AppDebugClass.pyDataString;
            string filePath1 = AppDebugClass.wdDataString;

            PlayerDataClass save0 = new PlayerDataClass();
            GoodsListDataClass save1 = new GoodsListDataClass();

            string jsonStr0 = string.Empty;
            string jsonStr1 = string.Empty;

            try
            {
                //读取文件
                jsonStr0 = File.ReadAllText(filePath0);
                //解析json
                save0 = JsonConvert.DeserializeObject<PlayerDataClass>(jsonStr0);

                jsonStr1 = File.ReadAllText(filePath1);
                save1 = JsonConvert.DeserializeObject<GoodsListDataClass>(jsonStr1);

                Debug.Log("读档成功");

                //备份存档
                BackupArchiveForGame(jsonStr0, jsonStr1);
            }
            catch (System.Exception e)
            {
                Debug.LogError("读档失败 " + e.ToString());

                filePath0 = AppDebugClass.pyDataString1;
                filePath1 = AppDebugClass.wdDataString1;

                //尝试获取备份存档
                try
                {
                    if (File.Exists(filePath0) && File.Exists(filePath1))
                    {
                        jsonStr0 = File.ReadAllText(filePath0);
                        save0 = JsonConvert.DeserializeObject<PlayerDataClass>(jsonStr0);

                        jsonStr1 = File.ReadAllText(filePath1);
                        save1 = JsonConvert.DeserializeObject<GoodsListDataClass>(jsonStr1);
                        Debug.Log("读取备份存档成功");
                    }
                    else
                    {
                        Debug.Log("无备份存档");
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("备份存档损坏 " + ex.ToString());
                }
            }

            //存档数据提取到游戏中
            SetGamePlayerBasicData(save0, save1);
        }
        else
        {
            CreateDataSave();

            //LoadByJson();
        }
        isLoadingSaveData = false;
    }

    /// <summary>
    /// 游戏数据初次存档
    /// </summary>
    /// <returns></returns>
    private void CreateDataSave()
    {
        string filePath0 = AppDebugClass.pyDataString;
        string filePath1 = AppDebugClass.wdDataString;

        PlayerDataClass save0 = new PlayerDataClass();
        GoodsListDataClass save1 = new GoodsListDataClass();

        //save0.playerName = "空";
        //save0.shopName = "空";
        save0.level = 1;
        save0.money = 88888;

        save1.goodsDataClasses = new System.Collections.Generic.List<GoodsDataClass>();

        for (int i = 0; i < 10; i++)
        {
            GoodsDataClass goodsDataClass = new GoodsDataClass();
            goodsDataClass.goodsId = i;
            goodsDataClass.goodsNum = 10;
            goodsDataClass.goodsPrice = 30;
            goodsDataClass.isExpired = false;
            goodsDataClass.purchaseTime = TimerControll.nowTimeLong.ToString();
            goodsDataClass.isArrivaled = true;
            goodsDataClass.arrivalTime = TimerControll.nowTimeLong.ToString();
            save1.goodsDataClasses.Add(goodsDataClass);
        }

        SetGamePlayerBasicData(save0, save1);

        SaveGameData();

        isHadSaveData = true;
        Debug.Log("初创存档成功");

    }

    /// <summary>
    /// 提取存档数据到游戏中
    /// </summary>
    private void SetGamePlayerBasicData(PlayerDataClass save, GoodsListDataClass save1)
    {
        PlayerSaveDataCS.instance.pyData = save;
        PlayerSaveDataCS.instance.gdsData = save1;
    }

    /// <summary>
    /// 游戏切除和再次进入的回调
    /// </summary>
    /// <param name="focus"></param>
    private void OnApplicationPause(bool focus)
    {
        if (focus)
        {
            //SaveAllGameData();
            //Debug.Log("切出游戏，OnApplicationPause(true)");
        }
        else
        {
            //Debug.Log("切出游戏再次进入，OnApplicationPause(false)");
        }
    }

    /// <summary>
    /// 退出游戏的回调
    /// </summary>
    private void OnApplicationQuit()
    {
        //SaveAllGameData();
        //Debug.Log("正常退出游戏,OnApplicationQuit()");
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            //Debug.Log("暂停状态后进入游戏内，OnApplicationFocus(true)");
        }
        else
        {
            //Debug.Log("切出游戏画面进入暂停状态，OnApplicationFocus(false)");
        }
    }
}