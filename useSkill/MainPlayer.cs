using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{

	/// <summary>
	/// �ͷż��ܽӿ�
	/// </summary>
	/// <param name="nSkillId"></param>
	/// <param name="skillBtn"></param>
	public void UseSkillOpt(int nSkillId, GameObject skillBtn)
	{

		//��ȡ������Ϣ  ��ϢΪ�շ���
		//�������
	    // ��ǰ��ѡ��Ŀ���Ҳ����Լ� �Ͳ�����ѡ����
		// �ж�ѡ��Ŀ���״̬         
		//��Ҫ����ѡ��Ŀ�� 

		OnEnterCombat(new GameObject());

	}

    private void OnEnterCombat(object targetObjChar)
    {
		//�޼��� Ĭ��ʹ���չ�

		//��������ʱ�� ��ǰѡ���Ŀ�����Ѻõ�Ŀ��������ѡ��һ�����Թ�����Ŀ��
		//�������չ���ҪĿ��
		//��ѡ��Ŀ���ƶ�  û���ƶ�״̬ ���ƶ� ����Ҫ��Ŀ���ƶ����ƶ�
		UseSkillCheck();
		ActiveSkill(1, 1);
	}
	/// <summary>
	/// ʹ��ǰ�Լ��������ĵ�hp xp���м��
	/// </summary>
    private void UseSkillCheck()
    {
      
    }

    private void ActiveSkill(object curUseSkillId, object nTargetID)
    {
		//���ͼ���ʹ�ð�  by dsy  �޸�ս��������Ϣʱ�򣬱�Ϊ��ս���з���
		//������Ϣ���� 
	}
	/// <summary>
	/// ������Ч
	/// </summary>
	/// <param name="skillid"></param>
	public void PlaySkillRangeEffect(int skillid)
	{

		//���ݴ��뼼�ܶ�һ���� ��ȡ��ǰ��Ч������ ��Բ�� ָ��
		PlayEffect();//����
	}
	public void PlayEffect()
	{
		
	}

	void PlayAnimation(int AnimationId)
	{
		//�ж�������ڱ�֤���ܶ�����˳������
		// ������ҷ����弼�ܲ���������
		
		//�������Ŵ���id ȥ�����������в���  ��������
	}

	//�ͷż���
	public void UseSkill(int skillId, int senderId, int targetId, string skillname = "")
	{
		//�ҳ����з�����
		//Ҳû�д�ϼ���
		//ͨ������id�ҵ���Ӧ����
		//ʹ�õ��������� ������ת
		//�����Ŀ�� ����Ŀ��
		//===����ǰ�˳�涯��
		//��ʼ���Ŷ���

		//��ʾ�������� ///Ⱥ����¹���������⴦����

		//���ǵ�һЩ�� ���⴦��
		// ʹ��XP���ܣ����������ָ���� �ص�
		//���������������CD ����������Ч�����CD ������ͬ������ 
		//�������� ���ӹ���CD
		//������������� ��ʾ������

		//npc����������ż��� �����Դ�����Ч��
		//������� ����

	}
	/// <summary>
	/// Ѱ·
	/// </summary>
	public void EnterAutoCombat()
	{
		//���ͷ�����ֵ�ı�
		//������ ����ս��
		//���������
		//�Զ���ҩ

		//������Ӹ���״̬
		//�Զ���ҩ
	}
	//�Զ���ҩ
	private void UpdateSellItem()
    {
		//�жϵȼ� 25
		//�����Ƿ������ҩ
		//����Ʒ����ʰȡ����     
		//���Զ�ǿ����Ʒ   
		//������Ϣ��
	}
	//������Ӹ���״̬
	private void LeaveTeamFollow()
    {
       
    }
}
