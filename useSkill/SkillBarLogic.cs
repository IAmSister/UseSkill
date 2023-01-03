using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using GCGame.Table;
using UnityEngine;
using System.Collections;
using Games.LogicObj;
using Games.GlobeDefine;
public enum SKILLBAR
{
    MAXSKILLBARNUM = 6,
}
public struct SkillBarInfo
{
    public void CleanUp()
    {
        buttonInfo = null;
        CDPicInfo = null;
        IconInfo = null;
        CDEffect = null;
        SkillIndex = -1;
        IsPress = false;
    }

    public GameObject buttonInfo;
    public UISprite CDPicInfo;
    public UISprite IconInfo;
    public UISpriteAnimation CDEffect;
    public int SkillIndex;
    public bool IsPress;
}
public class SkillBarLogic : MonoBehaviour
{
    public GameObject m_FirstChild;

    public UISprite m_Skill1CDPic;
    public UISprite m_Skill2CDPic;
    public UISprite m_Skill3CDPic;
    public UISprite m_Skill4CDPic;
    public UISprite m_Skill5CDPic;
    public UISprite m_Skill6CDPic;
    public UISprite m_SkillXPCPPic;

    public UISprite m_Skill1IconPic;
    public UISprite m_Skill2IconPic;
    public UISprite m_Skill3IconPic;
    public UISprite m_Skill4IconPic;
    public UISprite m_Skill5IconPic;
    public UISprite m_Skill6IconPic;
    public UISprite m_SkillXPIconPic;

    public GameObject m_Skill1Bt;
    public GameObject m_Skill2Bt;
    public GameObject m_Skill3Bt;
    public GameObject m_Skill4Bt;
    public GameObject m_Skill5Bt;
    public GameObject m_Skill6Bt;
    public GameObject m_SkillAttackBt;
    public GameObject m_SkillXPBt;

    public UISpriteAnimation m_Skill1CDEffect;
    public UISpriteAnimation m_Skill2CDEffect;
    public UISpriteAnimation m_Skill3CDEffect;
    public UISpriteAnimation m_Skill4CDEffect;
    public UISpriteAnimation m_Skill5CDEffect;
    public UISpriteAnimation m_Skill6CDEffect;

    public UISprite m_SkillXPEnergySprite;
    public GameObject m_SkillXPEnergyEffectRotation;

    private static SkillBarLogic m_Instance = null;
    public static SkillBarLogic Instance()
    {
        return m_Instance;
    }

    private SkillBarInfo[] m_MySkillBarInfo;
    public SkillBarInfo[] MySkillBarInfo
    {
        get { return m_MySkillBarInfo; }
        set { m_MySkillBarInfo = value; }
    }
    private bool m_bFirstUpdate = false;
    private bool m_bSetSkillBarSuccess = false;
    // 新手指引
    private int m_NewPlayerGuide_Step = 0;
    public int NewPlayerGuide_Step
    {
        get { return m_NewPlayerGuide_Step; }
        set { m_NewPlayerGuide_Step = value; }
    }


    //特殊效果ID
    private int m_NewSkillEffectID = 60;         //新技能学会特效
    private int m_XPSkillEffectID = 64;         //XP技能学会特效
                                                //private int m_SkillCDZeroEffectID = 86;         //技能CD清零特效

    //public TweenAlpha m_SkillXPTween;
    //public List<TweenAlpha> m_FoldTween;
    void Awake()
    {
        m_Instance = this;
    }
  
    void Start()
    {
        
        m_MySkillBarInfo = new SkillBarInfo[(int)SKILLBAR.MAXSKILLBARNUM];
        for (int i = 0; i < (int)SKILLBAR.MAXSKILLBARNUM; i++)
        {
            m_MySkillBarInfo[i] = new SkillBarInfo();
            m_MySkillBarInfo[i].CleanUp();
        }
        m_SkillXPEnergySprite.fillAmount = 0;
        m_SkillXPEnergyEffectRotation.transform.localRotation = Quaternion.AngleAxis(0, Vector3.forward);
        m_SkillXPEnergyEffectRotation.SetActive(false);
        m_SkillXPIconPic.spriteName = "";
       

        InitMySkillBarInfo(0, m_Skill1Bt, m_Skill1CDPic, m_Skill1IconPic, -1, m_Skill1CDEffect);
        InitMySkillBarInfo(1, m_Skill2Bt, m_Skill2CDPic, m_Skill2IconPic, -1, m_Skill2CDEffect);
        InitMySkillBarInfo(2, m_Skill3Bt, m_Skill3CDPic, m_Skill3IconPic, -1, m_Skill3CDEffect);
        InitMySkillBarInfo(3, m_Skill4Bt, m_Skill4CDPic, m_Skill4IconPic, -1, m_Skill4CDEffect);
        InitMySkillBarInfo(4, m_Skill5Bt, m_Skill5CDPic, m_Skill5IconPic, -1, m_Skill5CDEffect);
        InitMySkillBarInfo(5, m_Skill6Bt, m_Skill6CDPic, m_Skill6IconPic, -1, m_Skill6CDEffect);
        if (m_SkillXPBt.activeInHierarchy)
        {
            EffectLogic effectLogic = m_SkillXPBt.GetComponent<EffectLogic>();
            if (null == effectLogic)
            {
                effectLogic = m_SkillXPBt.AddComponent<EffectLogic>();
                effectLogic.InitEffect(m_SkillXPBt);
            }
            if (null != effectLogic)
            {
                effectLogic.PlayEffect(m_XPSkillEffectID);
            }
        }
        //更新下 技能按钮信息
        UpdateSkillBarInfo();
        //如果玩家数据池里ForthSkillFlag为ture  开始新手教学
       
    }

    void InitMySkillBarInfo(int nIndex, GameObject _button, UISprite _CDPic, UISprite _IconPic, int _SkillIndex, UISpriteAnimation _CDEffect)
    {
        if (nIndex >= 0 && nIndex < (int)SKILLBAR.MAXSKILLBARNUM)
        {
            m_MySkillBarInfo[nIndex].buttonInfo = _button;
            m_MySkillBarInfo[nIndex].CDPicInfo = _CDPic;
            m_MySkillBarInfo[nIndex].IconInfo = _IconPic;
            m_MySkillBarInfo[nIndex].SkillIndex = _SkillIndex;
            m_MySkillBarInfo[nIndex].CDPicInfo.gameObject.SetActive(false);
            m_MySkillBarInfo[nIndex].IconInfo.spriteName = "";
            m_MySkillBarInfo[nIndex].CDEffect = _CDEffect;
            m_MySkillBarInfo[nIndex].CDEffect.gameObject.SetActive(false);
        }
    }
    public void UpdateSkillBarInfo()
    {
      //找玩家 不为空
        m_bFirstUpdate = true;
        //设置XP图标  循环玩家技能信息  根据技能id获取这和技能的详细信息  信息不为空
        //多表联查 或者基本数据 设置XP图标 设置完美尺寸
        //新手引导

        //读取技能栏配置
        // 保存的技能不存在 清掉
        // 保存配置

        //配置读取失败了 给一个默认的配置
        //设置玩家血条技能信息进度条
      
        //保存配置
            
        //更新技能条
       
       
    }
    public void SetSkillBarInfo(int _skillBarIndex, int _skillIndex)
    {
        //找玩家 为技能面板设置值  
        //分找到数据   直接设置
        //没有 默认设置
  
    }
 
    void Update()
    {
        //玩家存在没有死亡
        //更新技能条信息
        //循环所有的技能条信息 如果被按下 释放技能 
        //鼠标没按下 释放 
        //播放冷却动画  循环玩家拥有技能信息数组  获取技能id 和 cd时间  设置
        //先走公共CD
        //技能的总CD时间  m_MySkillBarInfo
        //播放技能cd到0特效
    }

    // xp能量槽挪到玩家头像计算
    public void ChangeXPEnergy(int nValue, int maxXP)
    {
        // 增加怒气 改变精灵的fill amount属性 注意范围是0.3~0.95 按比例映射过去
        //怒气值特效true
        m_SkillXPEnergyEffectRotation.SetActive(true);

        float nFillAmount = (float)nValue / (float)maxXP * 0.65f + 0.3f;
        m_SkillXPEnergySprite.fillAmount = nFillAmount;

        // 更新怒气特效位置 绕Z轴旋转m_SkillXPEnergyEffectRotation 范围:10~-200
        float nAngel = 10 - (float)nValue / (float)maxXP * 210;
        m_SkillXPEnergyEffectRotation.transform.localRotation = Quaternion.AngleAxis(nAngel, Vector3.forward);

        //获取玩家等级 大于13  
        //  m_SkillXPCPPic.alpha = 0;
       // PlayXPActiveEffect(true);
       
        //如果透明度==0  变成1 设置xp特效
        
    }
    void OnEnable()
    {
        if (m_SkillXPBt.activeInHierarchy)
        {
            EffectLogic effectLogic = m_SkillXPBt.GetComponent<EffectLogic>();
            if (null == effectLogic)
            {
                effectLogic = m_SkillXPBt.AddComponent<EffectLogic>();
                effectLogic.InitEffect(m_SkillXPBt);
            }
            if (null != effectLogic)
            {
                effectLogic.PlayEffect(m_XPSkillEffectID);
            }
        }
        //玩家ForthSkillFlag为true
        //新手引导 4 NewPlayerGuide(4);
   
    }
    void OnDisable()
    {
        if (m_MySkillBarInfo != null)
        {
            for (int _skillBarIndex = 0; _skillBarIndex < m_MySkillBarInfo.Length; _skillBarIndex++)
            {
                if (m_MySkillBarInfo[_skillBarIndex].IsPress)
                {
                    ReleaseSkill(m_MySkillBarInfo[_skillBarIndex].buttonInfo);
                }
            }
        }
    }
    void OnDestroy()
    {
        m_Instance = null;
    }

    public void PlayNewSkillEffect(GameObject button)
    {
        if (button.activeInHierarchy == false)
        {
            return;
        }
        EffectLogic effectLogic = button.GetComponent<EffectLogic>();
        if (null == effectLogic)
        {
            effectLogic = button.AddComponent<EffectLogic>();
            effectLogic.InitEffect(button);
        }
        if (null != effectLogic)
        {
            effectLogic.PlayEffect(m_NewSkillEffectID);
        }
    }

    public void PlayXPActiveEffect(bool bShow)
    {
        //获取xpbtn上脚本 
        //没有添加 调用初始化方法
        //有 直接根号有场景id查询场景信息
        //_sceneClassInfo不为空 IsCanUseXp==1 播放特效 m_SkillXPBt父级显示
        //否则定制特效播放  m_SkillXPBt父级隐藏
      
    }

    void PlayCDZeroEffect(UISpriteAnimation CDEffect)
    {
        CDEffect.gameObject.SetActive(true);
        CDEffect.Reset();
    }
    /// <summary>
    /// 由btn传上来的
    /// </summary>
    /// <param name="button"></param>
    public void UseSkill(GameObject button)
    {
        // 正在转动 此时不响应技能
        //主玩家不为空  y return
        //根据不同的按钮，执行不同的方法，但是都会进入UseSkillOpt if进行判断
        //按钮是否有新手引导 关闭新手引导
    }
   /// <summary>
   /// 
   /// </summary>
   /// <param name="button"></param>
    public void PressSkill(GameObject button)
    {
        //先松开 
        if (m_MySkillBarInfo != null)
        {
            for (int _skillBarIndex = 0; _skillBarIndex < m_MySkillBarInfo.Length; _skillBarIndex++)
            {
                if (m_MySkillBarInfo[_skillBarIndex].IsPress)
                {
                    ReleaseSkill(m_MySkillBarInfo[_skillBarIndex].buttonInfo);
                }
            }

        }
        //玩家不为空
        //播放范围特效
        //目标播放 //玩家自己播放
      
        
    }
    public void ReleaseSkill(GameObject button)
    {
        //玩家不为空
       
        //停止范围特效 

        //使用技能
       
    }
    void UseItem(GameObject button)
    {
    }

    // 新手教学
    public void NewPlayerGuide(int nIndex)
    {
        m_NewPlayerGuide_Step = nIndex;
        //根据index 
        switch (nIndex)
        {
            case 1:
                if (m_SkillXPBt && m_SkillXPBt.activeInHierarchy)
                {
                    NewPlayerGuidLogic.OpenWindow(m_SkillXPBt, 110, 110, "", "left", 0, true);
                }
                break;
            case 2:
                if (m_SkillAttackBt && m_SkillAttackBt.activeInHierarchy)
                {
                    //NewPlayerGuidLogic.OpenWindow(m_SkillAttackBt, 134, 134, "点击施放普通攻击", "left", 0, true);
                    NewPlayerGuidLogic.OpenWindow(m_SkillAttackBt, 134, 134, StrDictionary.GetClientDictionaryString("#{2874}"), "left", 0, true);
                }
                break;
            case 3:
                if (m_Skill1Bt && m_Skill1Bt.activeInHierarchy)
                {
                    NewPlayerGuidLogic.OpenWindow(m_Skill2Bt, 110, 110, StrDictionary.GetClientDictionaryString("#{2875}"), "left", 0, true);
                    //NewPlayerGuidLogic.OpenWindow(m_Skill1Bt, 110, 110, "点击施放技能", "left", 0, true);
                }
                break;
            case 4:
                if (m_SkillAttackBt && m_SkillAttackBt.activeInHierarchy)
                {
                    //NewPlayerGuidLogic.OpenWindow(m_SkillAttackBt, 134, 134, "点击施放普通攻击", "left", 0, true);
                    NewPlayerGuidLogic.OpenWindow(m_SkillAttackBt, 200, 200, "", "left", 3);
                }
                break;
            default:
                break;
        }
    }

    //切换目标
    public void SwitchTarget()
    {
        //玩家不为空
        //切换目标
       
    }

    public void PlayTween(bool nDirection)
    {
      
        gameObject.SetActive(!nDirection);
    }

    void XPNewPlayGuid()
    {
        if (PlayerPreferenceData.XPNewPlayerGuideFlag == false)
        {
            NewPlayerGuide(1);
        }
    }
}
