using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveDataCS : MonoBehaviour
{
    public static PlayerSaveDataCS instance;

    //进货账本存档追加字符
    private readonly string purLeStr = "PLPS_";
    //销售额存档追加字符
    private readonly string xseStr = "XSE_";

    /// <summary>
    /// 货物数据类
    /// </summary>
    public GoodsListDataClass gdsData = new GoodsListDataClass();

    /// <summary>
    /// 货物数据类for游戏，重合数据
    /// </summary>
    public List<List<GoodsDataClass>> gdsDataForGame = new List<List<GoodsDataClass>>();

    /// <summary>
    /// 玩家数据类
    /// </summary>
    public PlayerDataClass pyData = new PlayerDataClass();

    /// <summary>
    /// 各类货物的销售等级
    /// </summary>
    public int[] goodsSalesLevel = new int[4] { 0, 0, 0, 0 };

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

    private void Start()
    {
        InitSalesLevel();
    }

    /// <summary>
    /// 初始化货物重合数据
    /// </summary>
    public void InitGdsDataForGame()
    {
        for (int i = 0; i < LoadJsonFile.gameDataBase.GoodsTable.Count; i++)
        {
            List<GoodsDataClass> goodsDataClasses = new List<GoodsDataClass>();
            gdsDataForGame.Add(goodsDataClasses);
        }

        for (int i = 0; i < gdsData.goodsDataClasses.Count; i++)
        {
            gdsDataForGame[gdsData.goodsDataClasses[i].goodsId].Add(gdsData.goodsDataClasses[i]);
        }
    }

    /// <summary>
    /// 初始化各类货物的销售等级
    /// </summary>
    private void InitSalesLevel()
    {
        int salesVolume, salesLevel;
        for (int i = 0; i < goodsSalesLevel.Length; i++)
        {
            salesLevel = 0;
            salesVolume = GetSalesVolume((GoodsTypeEnum)i);
            while (true)
            {
                string[] arrs = LoadJsonFile.gameDataBase.SalesRatioTable[salesLevel].xse.Split(',');
                if (salesVolume >= int.Parse(arrs[i]))
                {
                    goodsSalesLevel[i] = salesLevel;
                    salesLevel++;
                    if (salesLevel >= LoadJsonFile.gameDataBase.SalesRatioTable.Count)
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 刷新某类货物的销量等级
    /// </summary>
    /// <param name="goodsTypeEnum"></param>
    /// <param name="salesVolume"></param>
    private void UpdateSalesLevel(GoodsTypeEnum goodsTypeEnum, int salesVolume)
    {
        int goodsType = (int)goodsTypeEnum;
        int nextLevelSalesNum = 0;
        while (true)
        {
            if (goodsSalesLevel[goodsType] < LoadJsonFile.gameDataBase.SalesRatioTable.Count - 1)
            {
                nextLevelSalesNum = int.Parse(LoadJsonFile.gameDataBase.SalesRatioTable[goodsSalesLevel[goodsType] + 1].xse.Split(',')[goodsType]);
                if (salesVolume >= nextLevelSalesNum)
                {
                    goodsSalesLevel[goodsType]++;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
    }

    /// <summary>
    /// 游戏初创PlayerPrefs相关初始化
    /// </summary>
    public void InitPlayerPrefsFun()
    {
        string playerPreStr = string.Empty;
        for (int i = 0; i < 4; i++)
        {
            playerPreStr = xseStr + i;
            PlayerPrefs.SetInt(playerPreStr, 0);
        }
        PlayerPrefs.Save();
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

    /// <summary>
    /// 获取某类货物的销量
    /// </summary>
    /// <param name="goodsTypeEnum"></param>
    public int GetSalesVolume(GoodsTypeEnum goodsTypeEnum)
    {
        string playerPreStr = xseStr + goodsTypeEnum.ToString();
        int salesNum = PlayerPrefs.GetInt(playerPreStr);
        return salesNum;
    }

    /// <summary>
    /// 改变某类货物的销量
    /// </summary>
    /// <param name="goodsTypeEnum"></param>
    /// <param name="changeNum"></param>
    public void SetSalesVolume(GoodsTypeEnum goodsTypeEnum, int changeNum)
    {
        string playerPreStr = xseStr + goodsTypeEnum.ToString();
        int salesNum = PlayerPrefs.GetInt(playerPreStr) + changeNum;
        PlayerPrefs.SetInt(playerPreStr, salesNum);
        PlayerPrefs.Save();
        UpdateSalesLevel(goodsTypeEnum, salesNum);
    }
}