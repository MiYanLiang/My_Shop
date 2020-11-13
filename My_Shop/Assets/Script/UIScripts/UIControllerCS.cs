using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerCS : MonoBehaviour
{
    public static UIControllerCS instance;

    [HideInInspector]
    public bool isOpenBackPack; //是否开启背包界面
    [SerializeField]
    GameObject backPackObj; //背包界面
    [SerializeField]
    Text backPackTitleText;

    string[] backPackTitleNameStr = new string[3] {
        "冰箱",
        "柜台",
        "冰柜"
    };

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

        InitUIForGameProp();
    }

    //初始化相关内容
    private void InitUIForGameProp()
    {
        isOpenBackPack = false;

        for (int i = 0; i < marketTitleBtnsObj.transform.childCount; i++)
        {
            int index = i;
            marketTitleBtnsObj.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(delegate() {
                OpenMarketToShow(index);
            });
        }
    }

    /// <summary>
    /// 打开背包
    /// </summary>
    /// <param name="indexType">0，冰箱；1，；2，；</param>
    public void OpenBackPackOnClick(int indexType)
    {
        backPackTitleText.text = backPackTitleNameStr[indexType];

        backPackObj.SetActive(true);
    }

    [SerializeField]
    GameObject marketWinObj;    //市场界面
    [SerializeField]
    GameObject marketPackObj;   //市场背包
    [SerializeField]
    GameObject woodObj; //货物obj
    [SerializeField]
    Text woodInfoText;  //货物介绍
    [SerializeField]
    GameObject marketTitleBtnsObj;  //市场顶部按钮obj

    /// <summary>
    /// 打开市场
    /// </summary>
    public void OpenMarketToShow(int indexType)
    {
        for (int i = 0; i < marketTitleBtnsObj.transform.childCount; i++)
        {
            if (indexType == i)
            {
                marketTitleBtnsObj.transform.GetChild(i).transform.localScale = new Vector3(2f, 2f);
            }
            else
            {
                marketTitleBtnsObj.transform.GetChild(i).transform.localScale = new Vector3(1f, 1f);
            }
        }

        for (int i = 0; i < marketPackObj.transform.childCount; i++)
        {
            Destroy(marketPackObj.transform.GetChild(i).gameObject);
        }

        switch ((MarketWoodsType)indexType)
        {
            case MarketWoodsType.lingShi:
                for (int i = 0; i < LoadJsonFile.gameDataBase.SnacksTable.Count; i++)
                {
                    ShowOneWoodForMarket(LoadJsonFile.gameDataBase.SnacksTable[i].SnacksName, LoadJsonFile.gameDataBase.SnacksTable[i].Image_id);
                }
                break;
            case MarketWoodsType.yinLiao:
                for (int i = 0; i < LoadJsonFile.gameDataBase.DrinksTable.Count; i++)
                {
                    ShowOneWoodForMarket(LoadJsonFile.gameDataBase.DrinksTable[i].DrinksName, LoadJsonFile.gameDataBase.DrinksTable[i].Image_id);
                }
                break;
            case MarketWoodsType.xueGao:
                for (int i = 0; i < LoadJsonFile.gameDataBase.IceTable.Count; i++)
                {
                    ShowOneWoodForMarket(LoadJsonFile.gameDataBase.IceTable[i].IceName, LoadJsonFile.gameDataBase.IceTable[i].Image_id);
                }
                break;
            case MarketWoodsType.baiHuo:
                for (int i = 0; i < LoadJsonFile.gameDataBase.SogoTable.Count; i++)
                {
                    ShowOneWoodForMarket(LoadJsonFile.gameDataBase.SogoTable[i].SogoName, LoadJsonFile.gameDataBase.SogoTable[i].Image_id);
                }
                break;
            case MarketWoodsType.jiangChi:
                for (int i = 0; i < LoadJsonFile.gameDataBase.AwardTable.Count; i++)
                {
                    ShowOneWoodForMarket(LoadJsonFile.gameDataBase.AwardTable[i].AwardName, LoadJsonFile.gameDataBase.AwardTable[i].Image_id);
                }
                break;
            default:
                break;
        }
        marketWinObj.SetActive(true);
    }

    //展示设置一个物品的内容
    private void ShowOneWoodForMarket(string woodName, int woodImgId)
    {
        GameObject obj = Instantiate(woodObj, marketPackObj.transform);
        obj.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Image/InventoryImg/" + woodImgId, typeof(Sprite)) as Sprite;
        obj.GetComponentInChildren<Button>().onClick.AddListener(delegate ()
        {
            woodInfoText.text = woodName;
        });
    }
}


/// <summary>
/// 货物类型
/// </summary>
public enum MarketWoodsType
{
    lingShi,
    yinLiao,
    xueGao,
    baiHuo,
    jiangChi
}