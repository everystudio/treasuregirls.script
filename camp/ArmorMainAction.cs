using HutongGames.PlayMaker;
using System;
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
	public class initialize : ArmorMainActionBase
	{
		public FsmString position;
		public override void OnEnter()
		{
			base.OnEnter();

			armorMain.icon_armor_list.Clear();
			armorMain.m_prefArmorIcon.SetActive(false);
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
				DataArmorParam data = DataManager.Instance.dataArmor.list.Find(p => p.position == MasterArmor.ArmorPositionArr[i]);

				armorMain.icon_armor_list[i].Initialize(data, DataManager.Instance.masterArmor.list);

			}
			position.Value = "helmet";
			Finish();
		}
	}
	[ActionCategory("ArmorMainAction")]
	[HutongGames.PlayMaker.Tooltip("ArmorMainAction")]
	public class SelectArmor : ArmorMainActionBase
	{
		public FsmString position;

		public override void OnEnter()
		{
			base.OnEnter();
			armorMain.IconSelect(position.Value);

			armorMain.ShowParamsTotal();
			armorMain.ShowParamsArmor(position.Value);

			Finish();
		}
	}

	[ActionCategory("ArmorMainAction")]
	[HutongGames.PlayMaker.Tooltip("ArmorMainAction")]
	public class Idle : ArmorMainActionBase
	{
		public FsmString position;

		public override void OnEnter()
		{
			base.OnEnter();

			foreach(IconArmor icon in armorMain.icon_armor_list)
			{
				icon.OnClickDataArmor.AddListener(OnClickArmor);
			}

			armorMain.m_btnUpGrade.onClick.AddListener(OnUpgrade);
		}

		private void OnUpgrade()
		{
			Fsm.Event("upgrade");
		}

		private void OnClickArmor(DataArmorParam arg0)
		{
			if( position.Value != arg0.position)
			{
				position.Value = arg0.position;
				Fsm.Event("armor");
			}
		}

		public override void OnExit()
		{
			base.OnExit();
			foreach (IconArmor icon in armorMain.icon_armor_list)
			{
				icon.OnClickDataArmor.RemoveAllListeners();
			}
			armorMain.m_btnUpGrade.onClick.RemoveAllListeners();

		}
	}

	[ActionCategory("ArmorMainAction")]
	[HutongGames.PlayMaker.Tooltip("ArmorMainAction")]
	public class Upgrade : ArmorMainActionBase
	{
		public FsmString position;

		public override void OnEnter()
		{
			base.OnEnter();
			DataArmorParam data_armor = DataManager.Instance.dataArmor.list.Find(p => p.position == position.Value);

			MasterArmorParam master_armor_current = DataManager.Instance.masterArmor.list.Find(p => p.armor_id == data_armor.armor_id);
			MasterArmorParam master_armor_next = DataManager.Instance.masterArmor.list.Find(p => p.armor_id == master_armor_current.next_id);

			data_armor.armor_id = master_armor_next.armor_id;
			data_armor.level = master_armor_next.level;


			foreach( IconArmor icon in armorMain.icon_armor_list)
			{
				if( icon.m_dataArmor.position == position.Value)
				{
					icon.Initialize(data_armor, DataManager.Instance.masterArmor.list);
				}
			}
			Finish();
		}
	}

}
