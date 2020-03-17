using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharaView : MonoBehaviour
{
	public Image m_imgChara;

	public IconPotion m_iconPotion;
	public List<IconSkill> m_iconSkillList;



	public void Initialize()
	{

		int position = 1;
		foreach (IconSkill icon in m_iconSkillList)
		{
			DataSkillParam data = DataManager.Instance.dataSkill.list.Find(p => p.position == position);
			if (data == null)
			{
				data = new DataSkillParam(0, position);
			}
			MasterSkillParam master = DataManager.Instance.masterSkill.list.Find(p => p.skill_id == data.skill_id);
			icon.Initialize(data, master);
			position += 1;
		}

		DataPotionParam data_potion = DataManager.Instance.dataPotion.list.Find(p => p.is_use == true);
		MasterPotionParam master_potion = DataManager.Instance.masterPotion.list.Find(p => p.potion_id == data_potion.potion_id);

		Debug.Log(data_potion.potion_id);
		Debug.Log(master_potion.potion_id);
		m_iconPotion.Initialize(data_potion, master_potion);


	}

	public int GetSkillId(int _iPosition)
	{
		foreach (IconSkill icon in m_iconSkillList)
		{
			if( icon.m_data.position == _iPosition)
			{
				return icon.m_data.skill_id;
			}
		}
		return 0;
	}
	public int GetSkillPosition(int _iSkillId)
	{
		foreach (IconSkill icon in m_iconSkillList)
		{
			if (icon.m_data.skill_id == _iSkillId)
			{
				return icon.m_data.position;
			}
		}
		return 0;
	}

	public void SkillSelect(int _iPosition)
	{
		foreach (IconSkill icon in m_iconSkillList)
		{
			icon.Select(_iPosition);
		}
	}

}