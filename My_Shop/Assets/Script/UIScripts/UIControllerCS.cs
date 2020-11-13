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
    GameObject backPackObj;

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
}
