using System;
using System.Collections;
using System.Net;
using UnityEngine;

public class TimerControll : MonoBehaviour
{
    public static TimerControll instance;

    //获取网络时间戳网址
    private readonly string timeWebPath = "http://www.hko.gov.hk/cgi-bin/gts/time5a.pr?a=1";
    private readonly string timeWebPath0 = "http://api.m.taobao.com/rest/api3.do?api=mtop.common.getTimestamp";

    /// <summary>
    /// 是否获取到网络时间
    /// </summary>
    public static bool isGetNetworkTime;
    /// <summary>
    /// 当前网络时间戳long
    /// </summary>
    public static long nowTimeLong;

    private static DateTime startTime;

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
        //DontDestroyOnLoad(gameObject);

        isGetNetworkTime = false;
        startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

        StartCoroutine(GetTime());
    }

    /// <summary>
    /// 实时更新网络时间
    /// </summary>
    /// <returns></returns>
    IEnumerator GetTime()
    {
        string timeStr = string.Empty;

        while (true)
        {
            WWW www = new WWW(timeWebPath0);
            yield return www;

            if (www.text == "" || www.text.Trim() == "")//如果断网
            {
                isGetNetworkTime = false;

                print("请联网");
            }
            else//成功获取网络时间
            {
                try
                {
                    //0=1600529802400
                    //timeStr = www.text.Substring(2); //获取网络准确时间戳
                    timeStr = www.text.Substring(81, 13); //获取网络准确时间戳

                    nowTimeLong = long.Parse(timeStr);

                    isGetNetworkTime = true;

                    print("当前时间" + GetStrBackTime());
                }
                catch (Exception e)
                {
                    isGetNetworkTime = false;
                    print("请联网");
                }

            }
            yield return new WaitForSeconds(5f);
        }
    }

    /// <summary>
    /// 根据秒数返回时间格式
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static string BackToTimeShow(int seconds)
    {
        string str = string.Empty;
        if (seconds <= 0)
        {
            str = "";
        }
        else
        {
            if (seconds < 3600)
            {
                str = seconds / 60 + "分" + seconds % 60 + "秒";
            }
            else
            {
                if (seconds < 86400)
                {
                    str = seconds / 3600 + "时" + (seconds % 3600) / 60 + "分";
                }
                else
                {
                    str = seconds / 86400 + "天" + (seconds % 86400) / 3600 + "时";
                }
            }
        }
        return str;
    }

    /// <summary>
    /// 时间戳转换为时间格式
    /// </summary>
    /// <returns></returns>
    public static DateTime GetStrBackTime(long timeLong = 0)
    {
        if (timeLong == 0)
        {
            timeLong = nowTimeLong;
        }
        //return nowTimeLong;
        return startTime.AddMilliseconds(Convert.ToDouble(timeLong));
    }

    //////////////////////////未用到//////////////////////////////////////////

    /// <summary>
    /// 获取百度时间
    /// </summary>
    /// <returns></returns>
    private string GetNetDateTime()
    {
        WebRequest request = null;
        WebResponse response = null;
        WebHeaderCollection headerCollection = null;
        string datetime = string.Empty;
        try
        {
            request = WebRequest.Create("https://www.baidu.com");
            request.Timeout = 3000;
            request.Credentials = CredentialCache.DefaultCredentials;
            response = (WebResponse)request.GetResponse();
            headerCollection = response.Headers;
            foreach (var h in headerCollection.AllKeys)
            { if (h == "Date") { datetime = headerCollection[h]; } }
            return datetime;
        }
        catch (Exception) { return datetime; }
        finally
        {
            if (request != null)
            { request.Abort(); }
            if (response != null)
            { response.Close(); }
            if (headerCollection != null)
            { headerCollection.Clear(); }
        }
    }

    /// <summary>
    /// 获取网址内容
    /// </summary>
    /// <param name="getUrl"></param>
    /// <returns></returns>
    private static string GetWebRequest(string getUrl)
    {
        string responseContent = "";

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getUrl);
        request.ContentType = "application/json";
        request.Method = "GET";

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //在这里对接收到的页面内容进行处理
        using (System.IO.Stream resStream = response.GetResponseStream())
        {
            using (System.IO.StreamReader reader = new System.IO.StreamReader(resStream, System.Text.Encoding.UTF8))
            {
                responseContent = reader.ReadToEnd().ToString();
            }
        }
        return responseContent;
    }
}