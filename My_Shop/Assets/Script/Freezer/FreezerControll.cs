using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezerControll : MonoBehaviour
{
    public GameObject freezer; //冰箱
    public GameObject back;//背景



    private void Update()
    {
        IsDragBack();
    }


    ///<summary>
    ///点击冰箱
    ///</summary>
    public void Freezer_Click()
    {
        freezer.SetActive(true);
    }

    ///<summary>
    ///背景是否可拖动:各类背包打开时，拖动背景功能取消
    ///</summary>
    private void IsDragBack()
    {
        if (freezer.activeSelf)
        {
            back.GetComponent<BackMove_Phone>().enabled = false;
            back.GetComponent<BackMove_PC>().enabled = false;
        }
        else
        {
            back.GetComponent<BackMove_Phone>().enabled = true;
            back.GetComponent<BackMove_PC>().enabled = true;
        }
    }
}
