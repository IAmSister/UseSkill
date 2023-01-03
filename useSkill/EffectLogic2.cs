using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLogic2 : MonoBehaviour
{
    /// <summary>
    /// 播放特效,只接受type= 0的特效
    /// </summary>
    /// <param name="effectID">t特效id</param>
    /// <param name="delPlayEffect">延迟</param>
    /// <param name="param">参数</param>
    public void PlayEffect()
    {
        //UNITY_ANDROID 
        //只在低配机下运行; 如果NPC超过30时 同时其他玩家超过1个时，默认关闭NPC特效


        //设置了 取消技能特效

        //如果做了屏幕内特效优化，则也不显示
        //查表

        //加个数量限制
        //如果表格配置为-1则不限制，此时不用判断数量
        // UNITY_ANDROID
        //只在低配机下运行

        //加载特效
        LoadEffect();
    }
    
    void LoadEffect()
    {
        //判断类型 不同类型走不同逻辑
        //找通用节点
        //如果挂点不为空则直接查找该值赋值 不再添加 //播放武器特效
        //去缓存里找特效 没有就生成Add 有直接用
    }
}
