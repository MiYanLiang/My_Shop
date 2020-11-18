using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveDataCS : MonoBehaviour
{
    public static PlayerSaveDataCS instance;

    /// <summary>
    /// 货物数据类
    /// </summary>
    public GoodsListDataClass gdsData = new GoodsListDataClass();

    /// <summary>
    /// 玩家数据类
    /// </summary>
    public PlayerDataClass pyData = new PlayerDataClass();

    //进货账本存档追加字符
    private readonly string purLeStr = "PLPS_";

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
    }

    /// <summary>
    /// 根据年月获取那月进货账本信息
    /// </summary>
    /// <param name="yearNumber"></param>
    /// <param name="monthNumber"></param>
    /// <returns></returns>
    public string GetPurchaseLedgerForMonth(int yearNumber, int monthNumber)
    {
        string content = "";
        string playerPreStr = purLeStr + yearNumber + monthNumber;
        if (PlayerPrefs.HasKey(playerPreStr))
        {
            content = PlayerPrefs.GetString(playerPreStr);
        }
        return content;
    }

    /// <summary>
    /// 根据年月存放进货信息到当月账本
    /// </summary>
    /// <param name="yearNumber"></param>
    /// <param name="monthNumber"></param>
    /// <param name="content"></param>
    public void SetPurchaseLedgerForMonth(int yearNumber, int monthNumber, string content)
    {
        string playerPreStr = purLeStr + yearNumber + monthNumber;
        if (PlayerPrefs.HasKey(playerPreStr))
        {
            PlayerPrefs.SetString(playerPreStr, PlayerPrefs.GetString(playerPreStr) + content);
        }
        else
        {
            PlayerPrefs.SetString(playerPreStr, content);
        }
        PlayerPrefs.Save();
    }
}
