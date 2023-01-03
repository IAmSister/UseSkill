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
    // ����ָ��
    private int m_NewPlayerGuide_Step = 0;
    public int NewPlayerGuide_Step
    {
        get { return m_NewPlayerGuide_Step; }
        set { m_NewPlayerGuide_Step = value; }
    }


    //����Ч��ID
    private int m_NewSkillEffectID = 60;         //�¼���ѧ����Ч
    private int m_XPSkillEffectID = 64;         //XP����ѧ����Ч
                                                //private int m_SkillCDZeroEffectID = 86;         //����CD������Ч

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
        //������ ���ܰ�ť��Ϣ
        UpdateSkillBarInfo();
        //���������ݳ���ForthSkillFlagΪture  ��ʼ���ֽ�ѧ
       
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
      //����� ��Ϊ��
        m_bFirstUpdate = true;
        //����XPͼ��  ѭ����Ҽ�����Ϣ  ���ݼ���id��ȡ��ͼ��ܵ���ϸ��Ϣ  ��Ϣ��Ϊ��
        //������� ���߻������� ����XPͼ�� ���������ߴ�
        //��������

        //��ȡ����������
        // ����ļ��ܲ����� ���
        // ��������

        //���ö�ȡʧ���� ��һ��Ĭ�ϵ�����
        //�������Ѫ��������Ϣ������
      
        //��������
            
        //���¼�����
       
       
    }
    public void SetSkillBarInfo(int _skillBarIndex, int _skillIndex)
    {
        //����� Ϊ�����������ֵ  
        //���ҵ�����   ֱ������
        //û�� Ĭ������
  
    }
 
    void Update()
    {
        //��Ҵ���û������
        //���¼�������Ϣ
        //ѭ�����еļ�������Ϣ ��������� �ͷż��� 
        //���û���� �ͷ� 
        //������ȴ����  ѭ�����ӵ�м�����Ϣ����  ��ȡ����id �� cdʱ��  ����
        //���߹���CD
        //���ܵ���CDʱ��  m_MySkillBarInfo
        //���ż���cd��0��Ч
    }

    // xp������Ų�����ͷ�����
    public void ChangeXPEnergy(int nValue, int maxXP)
    {
        // ����ŭ�� �ı侫���fill amount���� ע�ⷶΧ��0.3~0.95 ������ӳ���ȥ
        //ŭ��ֵ��Чtrue
        m_SkillXPEnergyEffectRotation.SetActive(true);

        float nFillAmount = (float)nValue / (float)maxXP * 0.65f + 0.3f;
        m_SkillXPEnergySprite.fillAmount = nFillAmount;

        // ����ŭ����Чλ�� ��Z����תm_SkillXPEnergyEffectRotation ��Χ:10~-200
        float nAngel = 10 - (float)nValue / (float)maxXP * 210;
        m_SkillXPEnergyEffectRotation.transform.localRotation = Quaternion.AngleAxis(nAngel, Vector3.forward);

        //��ȡ��ҵȼ� ����13  
        //  m_SkillXPCPPic.alpha = 0;
       // PlayXPActiveEffect(true);
       
        //���͸����==0  ���1 ����xp��Ч
        
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
        //���ForthSkillFlagΪtrue
        //�������� 4 NewPlayerGuide(4);
   
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
        //��ȡxpbtn�Ͻű� 
        //û����� ���ó�ʼ������
        //�� ֱ�Ӹ����г���id��ѯ������Ϣ
        //_sceneClassInfo��Ϊ�� IsCanUseXp==1 ������Ч m_SkillXPBt������ʾ
        //��������Ч����  m_SkillXPBt��������
      
    }

    void PlayCDZeroEffect(UISpriteAnimation CDEffect)
    {
        CDEffect.gameObject.SetActive(true);
        CDEffect.Reset();
    }
    /// <summary>
    /// ��btn��������
    /// </summary>
    /// <param name="button"></param>
    public void UseSkill(GameObject button)
    {
        // ����ת�� ��ʱ����Ӧ����
        //����Ҳ�Ϊ��  y return
        //���ݲ�ͬ�İ�ť��ִ�в�ͬ�ķ��������Ƕ������UseSkillOpt if�����ж�
        //��ť�Ƿ����������� �ر���������
    }
   /// <summary>
   /// 
   /// </summary>
   /// <param name="button"></param>
    public void PressSkill(GameObject button)
    {
        //���ɿ� 
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
        //��Ҳ�Ϊ��
        //���ŷ�Χ��Ч
        //Ŀ�겥�� //����Լ�����
      
        
    }
    public void ReleaseSkill(GameObject button)
    {
        //��Ҳ�Ϊ��
       
        //ֹͣ��Χ��Ч 

        //ʹ�ü���
       
    }
    void UseItem(GameObject button)
    {
    }

    // ���ֽ�ѧ
    public void NewPlayerGuide(int nIndex)
    {
        m_NewPlayerGuide_Step = nIndex;
        //����index 
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
                    //NewPlayerGuidLogic.OpenWindow(m_SkillAttackBt, 134, 134, "���ʩ����ͨ����", "left", 0, true);
                    NewPlayerGuidLogic.OpenWindow(m_SkillAttackBt, 134, 134, StrDictionary.GetClientDictionaryString("#{2874}"), "left", 0, true);
                }
                break;
            case 3:
                if (m_Skill1Bt && m_Skill1Bt.activeInHierarchy)
                {
                    NewPlayerGuidLogic.OpenWindow(m_Skill2Bt, 110, 110, StrDictionary.GetClientDictionaryString("#{2875}"), "left", 0, true);
                    //NewPlayerGuidLogic.OpenWindow(m_Skill1Bt, 110, 110, "���ʩ�ż���", "left", 0, true);
                }
                break;
            case 4:
                if (m_SkillAttackBt && m_SkillAttackBt.activeInHierarchy)
                {
                    //NewPlayerGuidLogic.OpenWindow(m_SkillAttackBt, 134, 134, "���ʩ����ͨ����", "left", 0, true);
                    NewPlayerGuidLogic.OpenWindow(m_SkillAttackBt, 200, 200, "", "left", 3);
                }
                break;
            default:
                break;
        }
    }

    //�л�Ŀ��
    public void SwitchTarget()
    {
        //��Ҳ�Ϊ��
        //�л�Ŀ��
       
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
