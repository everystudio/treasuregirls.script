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
	}

	public void SkillSelect(int _iPosition)
	{
		foreach (IconSkill icon in m_iconSkillList)
		{
			icon.Select(_iPosition);
		}
	}

}