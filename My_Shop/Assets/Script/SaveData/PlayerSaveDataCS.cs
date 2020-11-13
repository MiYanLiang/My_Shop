using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveDataCS : MonoBehaviour
{
    public static PlayerSaveDataCS instance;

    /// <summary>
    /// 货物数据类
    /// </summary>
    public WoodsListDataClass wdData = new WoodsListDataClass();

    /// <summary>
    /// 玩家数据类
    /// </summary>
    public PlayerDataClass pyData = new PlayerDataClass();

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
}
