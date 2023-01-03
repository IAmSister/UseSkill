using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{

	/// <summary>
	/// 释放技能接口
	/// </summary>
	/// <param name="nSkillId"></param>
	/// <param name="skillBtn"></param>
	public void UseSkillOpt(int nSkillId, GameObject skillBtn)
	{

		//获取技能信息  信息为空返回
		//多表联查
	    // 当前有选择目标且不是自己 就不重新选择了
		// 判断选择到目标的状态         
		//需要重新选择目标 

		OnEnterCombat(new GameObject());

	}

    private void OnEnterCombat(object targetObjChar)
    {
		//无技能 默认使用普攻

		//如果开打的时候 当前选择的目标是友好的目标则重新选择一个可以攻击的目标
		//单攻和普攻需要目标
		//向选中目标移动  没在移动状态 能移动 且需要向目标移动则移动
		UseSkillCheck();
		ActiveSkill(1, 1);
	}
	/// <summary>
	/// 使用前对技能所消耗的hp xp进行检查
	/// </summary>
    private void UseSkillCheck()
    {
      
    }

    private void ActiveSkill(object curUseSkillId, object nTargetID)
    {
		//发送技能使用包  by dsy  修改战斗发送消息时序，变为在战斗中发送
		//进行消息发送 
	}
	/// <summary>
	/// 播放特效
	/// </summary>
	/// <param name="skillid"></param>
	public void PlaySkillRangeEffect(int skillid)
	{

		//根据传入技能读一个表 获取当前特效的类型 是圆方 指向
		PlayEffect();//播放
	}
	public void PlayEffect()
	{
		
	}

	void PlayAnimation(int AnimationId)
	{
		//判断任务存在保证技能动作能顺利播放
		// 其他玩家放陷阱技能不播发动作
		
		//动画播放传入id 去动画管理器中播放  声音播放
	}

	//释放技能
	public void UseSkill(int skillId, int senderId, int targetId, string skillname = "")
	{
		//找场景中发送者
		//也没有打断技能
		//通过技能id找到对应技能
		//使用的是旋风则 屏蔽旋转
		//如果有目标 朝向目标
		//===屏蔽前端冲锋动作
		//开始播放动画

		//显示技能名字 ///群雄逐鹿场景下特殊处理下

		//主角的一些的 特殊处理
		// 使用XP技能，如果有新手指引就 关掉
		//吟唱技不在这里加CD 吟唱技能生效后才走CD 服务器同步过来 
		//非连续技 增加公共CD
		//如果是吟唱技则 显示引导条

		//npc和主角自身放技能 可以自带震屏效果
		//摄像机的 操作

	}
	/// <summary>
	/// 寻路
	/// </summary>
	public void EnterAutoCombat()
	{
		//玩家头顶面板值改变
		//控制器 进入战斗
		//不跟随队友
		//自动买药

		//结束组队跟随状态
		//自动卖药
	}
	//自动卖药
	private void UpdateSellItem()
    {
		//判断等级 25
		//身上是否有这个药
		//计算品级及拾取规则     
		//做自动强化物品   
		//发送消息包
	}
	//结束组队跟随状态
	private void LeaveTeamFollow()
    {
       
    }
}
