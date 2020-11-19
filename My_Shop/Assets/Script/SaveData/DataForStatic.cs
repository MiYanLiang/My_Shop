using UnityEngine;

/// <summary>
/// 颜色数据类
/// </summary>
public static class ColorDataStatic
{
    /// <summary>
    /// 灰色
    /// </summary>
    public static readonly Color name_gray = new Color(109f / 255f, 109f / 255f, 109f / 255f, 1);

    /// <summary>
    /// 绿色
    /// </summary>
    public static readonly Color name_green = new Color(27f / 255f, 105f / 255f, 0f / 255f, 1);

    /// <summary>
    /// 蓝色
    /// </summary>
    public static readonly Color name_blue = new Color(0f / 255f, 44f / 255f, 207f / 255f, 1);
}

/// <summary>
/// 货物类型
/// </summary>
public enum GoodsTypeEnum
{
    /// <summary>
    /// 零食
    /// </summary>
    LINGSHI = 0,
    /// <summary>
    /// 饮料
    /// </summary>
    YINLIAO = 1,
    /// <summary>
    /// 雪糕
    /// </summary>
    XUEGAO = 2,
    /// <summary>
    /// 百货
    /// </summary>
    BAIHUO = 3
}