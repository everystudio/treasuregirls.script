using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArmorMainAction
{
	[ActionCategory("ArmorMainAction")]
	[HutongGames.PlayMaker.Tooltip("ArmorMainAction")]
	public abstract class ArmorMainActionBase : FsmStateAction
	{
		protected ArmorMain armorMain;
		public override void OnEnter()
		{
			base.OnEnter();
			armorMain = Owner.GetComponent<ArmorMain>();
		}
	}

	[ActionCategory("ArmorMainAction")]
	[HutongGames.PlayMaker.Tooltip("ArmorMainAction")]
	public abstract class initialize : ArmorMainActionBase
	{
		public FsmString position;
		public override void OnEnter()
		{
			base.OnEnter();

			armorMain.icon_armor_list.Clear();
			MonoBehaviourEx.DeleteObjects<IconArmor>(armorMain.m_goRootLeft);
			MonoBehaviourEx.DeleteObjects<IconArmor>(armorMain.m_goRootRight);

			for (int i = 0; i < 4; i++)
			{
				IconArmor script = PrefabManager.Instance.MakeScript<IconArmor>(armorMain.m_prefArmorIcon, armorMain.m_goRootLeft);
				armorMain.icon_armor_list.Add(script);
			}
			for (int i = 0; i < 4; i++)
			{
				IconArmor script = PrefabManager.Instance.MakeScript<IconArmor>(armorMain.m_prefArmorIcon, armorMain.m_goRootRight);
				armorMain.icon_armor_list.Add(script);
			}


			for( int i = 0; i < MasterArmor. ArmorPositionArr.Length ; i++)
			{
				armorMain.icon_armor_list[i].Initialize();

			}


			position.Value = "helmet";
		}

	}
}
