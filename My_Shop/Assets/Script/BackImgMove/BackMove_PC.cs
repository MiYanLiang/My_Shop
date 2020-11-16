using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMove_PC : MonoBehaviour
{
    private Vector2 first = Vector2.zero;//鼠标第一次下落点
    private Vector2 second = Vector2.zero;//鼠标第二次位置（拖拽位置）
    private Vector3 vecPos = Vector3.zero;
    private bool IsNeedMove = false;//是否需要移动

    void Start()
    {
        //初始化第一次下落点
        first.x = transform.position.x;
        first.y = transform.position.y;
        //获取背景图片的尺寸
        string imageWidth = gameObject.GetComponent<RectTransform>().rect.width.ToString();
        string imageHeight = gameObject.GetComponent<RectTransform>().rect.height.ToString();
    }
    public void OnGUI()
    {
        if (Event.current.type == EventType.MouseDown)
        {
            //记录鼠标按下的位置 　　
            first = Event.current.mousePosition;
        }
        if (Event.current.type == EventType.MouseDrag)
        {
            //记录鼠标拖动的位置 　　
            second = Event.current.mousePosition;
            Vector3 fir = new Vector3(first.x, 0, 0);
            Vector3 sec = new Vector3(second.x, 0, 0);
            vecPos = sec - fir;//需要移动的 向量
            first = second;
            IsNeedMove = true;
        }
        else
        {
            IsNeedMove = false;
        }
    }

    void Update()
    {
        if (UIControllerCS.instance.isOpenBackPack)
            return;

        if (!IsNeedMove)
            return;

        var x = transform.position.x;
        x = x + vecPos.x;//向量偏移
        x = Mathf.Clamp(x, -420, 1500);
        //x = Mathf.Clamp(x, -58, 58);

        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}
