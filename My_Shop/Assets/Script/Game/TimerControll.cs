using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerControll : MonoBehaviour
{
    public Text timer_;
    public Text curTime;

    DateTime gameTime = new DateTime(1997,4,4,6,0,0);

    DateTime beginTime =new DateTime(); //获取的当前时间
    DateTime nextTime = new DateTime(); //下一次登录的时间
    DateTime compareTime = new DateTime(0001,1,1,0,0,0); //比较时间，判断是否第一次登录



    // Start is called before the first frame update
    //Debug.Log(beginTime.CompareTo(gameTime).ToString());//0 两时间比较:早于<0，同时==0，迟于>0[static&&not static]
    //Debug.Log("beginTime:"+beginTime);
    //Debug.Log("gameTime:"+gameTime);
    //Debug.Log(long.Parse(GetTimeStamp(gameTime))-long.Parse(GetTimeStamp(gameTime1)));
    //Debug.Log(GetTime("61"));
    void Start()
    {
        //beginTime = DateTime.Now;//每次登录获取当前时间
        //string getNextTimeStr = PlayerPrefs.GetString("CurrentTime");
        //Debug.Log(getNextTimeStr);
        //curTime.text = getNextTimeStr;//nextTime.ToString();
        ////判断是否第一次登录
        //if (int.Parse(getNextTimeStr) < 0)//nextTime.CompareTo(compareTime).ToString() == "0"
        //{
        //    nextTime = beginTime;
        //    string nextTimeStr = GetTimeStamp(nextTime);//将时间转换为秒数
        //    PlayerPrefs.SetString("CurrentTime", nextTimeStr);//存储每次进来的初始时间
        //    //curTime.text = "bb";//gameTime.ToString();
        //    Debug.Log(nextTime);
        //}
        //else
        //{
        //    //有第二次登录时，进行时间处理
        //    string getBeginTimeStr = PlayerPrefs.GetString("CurrentTime");//获取的是第一次登录的时间转化为的秒数
        //    string curBeginTimeStr = GetTimeStamp(beginTime);//获取的是第二次登录的时间转化为的秒数
        //    long differenceValue = long.Parse(curBeginTimeStr) - long.Parse(getBeginTimeStr);
        //    //将差值加到游戏时间
        //    DateTime finallyTime = GetTime(gameTime,differenceValue.ToString());
        //    //curTime.text = "cc";//finallyTime.ToString();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        timer_.text = DateTime.Now.ToString();
    }
    //当前时间转换为秒数
    private static string GetTimeStamp(DateTime timeStamp)
    {
        TimeSpan ts = DateTime.UtcNow - timeStamp;
        return Convert.ToInt64(ts.TotalSeconds).ToString();
    }
    //秒数转换为时间
    private DateTime GetTime(DateTime dtStart,string timeStamp)
    {
        //DateTime dtStart = new DateTime(1997, 4, 4, 0, 0, 0, 0);  //起始时间
        long lTime = long.Parse(timeStamp + "0000000");//转为long类型  
        TimeSpan toNow = new TimeSpan(lTime); //时间间隔
        return dtStart.Add(toNow); //加上时间间隔得到目标时间
    }

    //存储数据
    public void SaveDataClick()
    {
        string NowTime = DateTime.Now.ToString();
        PlayerPrefs.SetString("_NowTime", NowTime);
    }

    public void GetDataClick()
    {
        curTime.text= PlayerPrefs.GetString("_NowTime");
    }

}
