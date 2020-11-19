using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

public class LoadJsonFile : MonoBehaviour
{
    public static LoadJsonFile instance;

    //Resources文件夹下
    public static readonly string topFolder = "Jsons/";
    //存放json数据名
    private static readonly string tableNameStrs = "ShopTable;OTCTable;AwardTable;GoodsTable;SalesRatioTable";

    /// <summary>
    /// 游戏数据库
    /// </summary>
    public static Roots gameDataBase = new Roots();

    /// <summary>
    /// 加载json文件获取数据至链表中
    /// </summary>
    private void JsonDataToSheets(string[] tableNames)
    {
        //Json数据控制类
        Roots root = new Roots();
        //存放json数据
        string jsonData = string.Empty;
        //记录读取到第几个表
        int indexTable = 0;

        // 加载数据:ShopTable
        {
            //读取json文件数据
            jsonData = LoadJsonByName(tableNames[indexTable]);
            //解析数据存放至Root中
            root = JsonConvert.DeserializeObject<Roots>(jsonData);
            //实例化数据存储链表
            gameDataBase.ShopTable = root.ShopTable;
            indexTable++;
        }
        // 加载数据:OTCTable
        {
            jsonData = LoadJsonByName(tableNames[indexTable]);
            root = JsonConvert.DeserializeObject<Roots>(jsonData);
            gameDataBase.OTCTable = root.OTCTable;
            indexTable++;
        }
        // 加载数据:AwardTable
        {
            jsonData = LoadJsonByName(tableNames[indexTable]);
            root = JsonConvert.DeserializeObject<Roots>(jsonData);
            gameDataBase.AwardTable = root.AwardTable;
            indexTable++;
        }
        // 加载数据:GoodsTable
        {
            jsonData = LoadJsonByName(tableNames[indexTable]);
            root = JsonConvert.DeserializeObject<Roots>(jsonData);
            gameDataBase.GoodsTable = root.GoodsTable;
            indexTable++;
        }
        // 加载数据:SalesRatioTable
        {
            jsonData = LoadJsonByName(tableNames[indexTable]);
            root = JsonConvert.DeserializeObject<Roots>(jsonData);
            gameDataBase.SalesRatioTable = root.SalesRatioTable;
            indexTable++;
        }


        if (indexTable >= tableNames.Length)
        {
            Debug.Log("所有Json数据加载成功。");
        }
        else
        {
            //Debug.Log("还有Json数据未进行加载。");
        }
    }

    /// <summary>
    /// 深拷贝List等
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="List"></param>
    /// <returns></returns>
    public static List<T> DeepClone<T>(object List)
    {
        using (System.IO.Stream objectStream = new System.IO.MemoryStream())
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(objectStream, List);
            objectStream.Seek(0, System.IO.SeekOrigin.Begin);
            return formatter.Deserialize(objectStream) as List<T>;
        }
    }

    /// <summary>
    /// 根据id获取文本内容
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    //public static string GetStringText(int id)
    //{
    //    return stringTextTableDatas[id][1];
    //}

    /// <summary>
    /// 根据id获取游戏数值内容
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    //public static int GetGameValue(int id)
    //{
    //    return int.Parse(numParametersTableDatas[id][1]);
    //}


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

        string[] arrStr = tableNameStrs.Split(';');
        if (arrStr.Length > 0)
        {
            JsonDataToSheets(arrStr);  //传递Json文件名进行加载
        }
        else
        {
            //Debug.Log("////请检查Json表名");
        }
        DontDestroyOnLoad(gameObject);//跳转场景等不销毁
    }


    /// <summary>
    /// 通过json文件名获取json数据
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string LoadJsonByName(string fileName)
    {
        string path = string.Empty;
        string data = string.Empty;
        if (Application.isPlaying)
        {
            path = System.IO.Path.Combine(topFolder, fileName);  //合并文件路径
            var asset = Resources.Load<TextAsset>(path);
            //Debug.Log("Loading..." + fileName + "\nFrom:" + path);
            if (asset == null)
            {
                Debug.LogError("No text asset could be found at resource path: " + path);
                return null;
            }
            data = asset.text;
            Resources.UnloadAsset(asset);
        }
        else
        {
#if UNITY_EDITOR
            path = Application.dataPath + "/Resources/" + topFolder + "/" + fileName + ".json";
            //Debug.Log("Loading JsonFile " + fileName + " from: " + path);
            var asset1 = System.IO.File.ReadAllText(path);
            data = asset1;
#endif
        }
        return data;
    }

    /// <summary>
    /// 通过StreamReader读取json,json存在StreamingAssets文件夹下
    /// </summary>
    /// using LitJson;
    //public JsonReader LoadJsonUseStreamReader(string fileName)
    //{
    //    System.IO.StreamReader streamreader = new System.IO.StreamReader(Application.dataPath + "/StreamingAssets/Jsons/" + fileName + ".json");  //读取数据，转换成数据流
    //    JsonReader js = new JsonReader(streamreader);   //再转换成json数据
    //    //Root r = JsonConvert.DeserializeObject<Root>(js);     //读取
    //    //for (int i = 0; i < r.LevelTable.Count; i++)  //遍历获取数据
    //    //{
    //    //    textone.text += r.LevelTable[i].experience + "   ";
    //    //}
    //    streamreader.Close();
    //    return js;
    //}

    /// <summary>
    /// 通过WWW方法读取Json数据，json存在StreamingAssets文件夹下
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns></returns>
    public string LoadJsonUseWWW(string fileName)
    {
        string localPath = string.Empty;
        if (Application.platform == RuntimePlatform.Android)
        {
            //localPath = Application.streamingAssetsPath + "/" + path;
            localPath = "jar:file://" + Application.dataPath + "!/assets/" + fileName + ".json";
        }
        else
        {
            localPath = "file:///" + Application.streamingAssetsPath + "/" + fileName + ".json";
        }
        WWW www = new WWW(localPath);     //格式必须是"ANSI"，不能是"UTF-8"
        if (www.error != null)
        {
            Debug.LogError("error : " + localPath);
            return null;          //读取文件出错
        }
        return www.text;
    }
}