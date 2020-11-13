using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMove_Phone : MonoBehaviour
{
    private Touch oldTouch1;  //上次触摸点1(手指1)  
    private Touch oldTouch2;  //上次触摸点2(手指2) 
    private bool isPortraitScreen;  //是否为竖屏
    private bool isLockScreen;  //是否锁定缩放
    private static Vector3 objectScale;  //记录对象物体的初始大小
    private Vector2 first = Vector2.zero;//鼠标第一次下落点
    private Vector2 second = Vector2.zero;//鼠标第二次位置（拖拽位置）
    private Vector3 vecPos = Vector3.zero;
    private bool isUpdateTouch = true; //是否更新touch 
    private bool IsNeedMove = false;//是否需要移动


    void Awake()
    {
        //初始化屏幕方向
        isPortraitScreen = true;
        Screen.orientation = ScreenOrientation.Portrait;
        //Screen.SetResolution(720, 1280, false);//设置屏幕分辨率
        isLockScreen = false;
        objectScale = transform.localScale;

        //初始化第一次下落点
        first.x = transform.position.x;
        first.y = transform.position.y;
    }
    void Update()
    {
        if (UIControllerCS.instance.isOpenBackPack)
            return;

        if (!IsNeedMove)
            return;

        MoveFunction();
        var x = transform.position.x;
        x = x + vecPos.x;//向量偏移
        x = Mathf.Clamp(x, -420, 1500);
        //x = Mathf.Clamp(x, -58, 58);

        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }


    ///<summary>
    ///移动物体
    ///</summary>
    public void MoveFunction()
    {
        //没有触摸  
        if (Input.touchCount <= 0)
        {
            return;
        }
        if (!isUpdateTouch)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began || Input.GetTouch(i).phase == TouchPhase.Canceled || Input.GetTouch(i).phase == TouchPhase.Ended)
                {
                    isUpdateTouch = true;
                    break;
                }
            }
        }
        if (Input.touchCount == 1)
        {
            if (isUpdateTouch)
            {
                first = Input.GetTouch(0).position;
                isUpdateTouch = false;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                second = Input.GetTouch(0).position;
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
    }


    ///<summary>
    ///缩放物体
    ///</summary>
    public void ZoomFunction()
    {
        //没有触摸  
        if (Input.touchCount <= 0)
        {
            return;
        }
        if (isLockScreen == false)
        {
            //多点触摸, 放大缩小  
            Touch newTouch1 = Input.GetTouch(0);
            Touch newTouch2 = Input.GetTouch(1);

            //第2点刚开始接触屏幕, 只记录，不做处理  
            if (newTouch2.phase == TouchPhase.Began)
            {
                oldTouch2 = newTouch2;
                oldTouch1 = newTouch1;
                return;
            }

            //计算老的两点距离和新的两点间距离，变大要放大模型，变小要缩放模型  
            float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
            float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);

            //两个距离之差，为正表示放大手势， 为负表示缩小手势  
            float offset = newDistance - oldDistance;

            //放大因子， 一个像素按 0.01倍来算(100可调整)  
            float scaleFactor = offset / 100f;
            Vector3 localScale = transform.localScale;
            Vector3 scale = new Vector3(localScale.x + scaleFactor,
                                        localScale.y + scaleFactor,
                                        localScale.z + scaleFactor);

            //最小缩放到 0.3 倍  
            if (scale.x > 0.3f && scale.y > 0.3f && scale.z > 0.3f)
            {
                transform.localScale = scale;
            }

            //记住最新的触摸点，下次使用  
            oldTouch1 = newTouch1;
            oldTouch2 = newTouch2;
        }
    }

    ///<summary>
    ///旋转物体
    ///</summary>
    //单点触摸， 水平上下旋转  
    public void RotateFunction()
    {
        //没有触摸  
        if (Input.touchCount <= 0)
        {
            return;
        }
        if (1 == Input.touchCount)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 deltaPos = touch.deltaPosition;
            transform.Rotate(Vector3.down * deltaPos.x, Space.World);
            //transform.Rotate(Vector3.right * deltaPos.y, Space.World);
        }
    }

    ///<summary>
    ///设置屏幕方向
    ///</summary>
    public void ScreenDirection()
    {
        //设置为竖屏
        if (isPortraitScreen == false)
        {
            Debug.Log("竖屏显示");
            isPortraitScreen = true;
            Screen.orientation = ScreenOrientation.Portrait;
            //Screen.SetResolution(720, 1280, false);//设置屏幕分辨率
        }
        else if (isPortraitScreen == true)
        {
            //设置为横屏
            Debug.Log("横屏显示");
            isPortraitScreen = false;
            Screen.orientation = ScreenOrientation.Landscape;
            //Screen.SetResolution(1280, 720, false);
        }
    }

    ///<summary>
    ///设置是否可以缩放
    ///</summary>
    public void ScreenZoom()
    {
        if (isLockScreen == false)
        {
            isLockScreen = true;
            transform.localScale = objectScale;
        }
        else if (isLockScreen == true)
        {
            isLockScreen = false;
        }
    }

}
