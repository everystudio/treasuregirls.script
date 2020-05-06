using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QuestMainAction
{
	[ActionCategory("QuestMainAction")]
	[HutongGames.PlayMaker.Tooltip("QuestMainAction")]
	public abstract class QuestMainActionBase : FsmStateAction
	{
		protected QuestMain main;
		public override void OnEnter()
		{
			base.OnEnter();
			main = Owner.GetComponent<QuestMain>();
		}
	}

	[ActionCategory("QuestMainAction")]
	[HutongGames.PlayMaker.Tooltip("QuestMainAction")]
	public class initialize : QuestMainActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();

			main.m_btnQuest.onClick.RemoveAllListeners();
			main.m_btnQuest.onClick.AddListener(() =>
			{
				Fsm.Event("quest");
			});

			// charaview
			main.m_charaView.Initialize();

			// weapon
			DataWeaponParam data_equip = DataManager.Instance.dataWeapon.list.Find(p => 0 < p.equip);
			MasterWeaponParam master_equip = DataManager.Instance.masterWeapon.list.Find(p => p.weapon_id == data_equip.weapon_id);
			main.m_weaponInfo.Setup(data_equip, master_equip);

			// treasure
			main.m_prefIconTreasure.SetActive(false);
			main.treasure_list.Clear();
			MonoBehaviourEx.DeleteObjects<IconInventry>(main.m_goTreasureRoot);
			for (int i = 0; i < 3; i++)
			{
				IconInventry icon = PrefabManager.Instance.MakeScript<IconInventry>(main.m_prefIconTreasure, main.m_goTreasureRoot);
				DataTreasureParam equip_data = DataManager.Instance.dataTreasure.list.Find(p => p.equip == i + 1);
				if (equip_data != null)
				{
					MasterTreasureParam master = DataManager.Instance.masterTreasure.list.Find(p => p.treasure_id == equip_data.treasure_id);
					icon.Initialize(equip_data, master);
				}
				else
				{
					equip_data = new DataTreasureParam();
					equip_data.equip = i + 1;
					equip_data.treasure_id = 0;
					icon.Initialize(equip_data, null);
				}
				main.treasure_list.Add(icon);
			}

			// armor
			main.icon_armor_list.Clear();
			main.m_prefIconArmor.SetActive(false);
			MonoBehaviourEx.DeleteObjects<IconArmor>(main.m_goArmorLeft);
			MonoBehaviourEx.DeleteObjects<IconArmor>(main.m_goArmorRight);
			for (int i = 0; i < 4; i++)
			{
				IconArmor script = PrefabManager.Instance.MakeScript<IconArmor>(main.m_prefIconArmor, main.m_goArmorLeft);
				main.icon_armor_list.Add(script);
			}
			for (int i = 0; i < 4; i++)
			{
				IconArmor script = PrefabManager.Instance.MakeScript<IconArmor>(main.m_prefIconArmor, main.m_goArmorRight);
				main.icon_armor_list.Add(script);
			}

			for (int i = 0; i < MasterArmor.ArmorPositionArr.Length; i++)
			{
				DataArmorParam data = DataManager.Instance.dataArmor.list.Find(p => p.position == MasterArmor.ArmorPositionArr[i]);
				main.icon_armor_list[i].Initialize(data, DataManager.Instance.masterArmor.list);
			}
			main.m_panelStage.gameObject.SetActive(false);
			ShowParamsTotal();
			Finish();
		}

		public void ShowParamsTotal()
		{
			int total_hp = 0;
			int total_def = 0;
			int total_mind = 0;
			int total_luck = 0;

			for (int i = 0; i < MasterArmor.ArmorPositionArr.Length; i++)
			{
				DataArmorParam data = DataManager.Instance.dataArmor.list.Find(p => p.position == MasterArmor.ArmorPositionArr[i]);
				MasterArmorParam master = DataManager.Instance.masterArmor.list.Find(p => p.armor_id == data.armor_id);
				total_hp += master.hp;
				total_def += master.def;
				total_mind += master.mind;
				total_luck += master.luck;
			}

			main.m_txtTotalHP.text = total_hp.ToString();
			main.m_txtTotalDef.text = total_def.ToString();
			main.m_txtTotalMind.text = total_mind.ToString();
			main.m_txtTotalLuck.text = total_luck.ToString();

		}

	}

	[ActionCategory("QuestMainAction")]
	[HutongGames.PlayMaker.Tooltip("QuestMainAction")]
	public class idle : QuestMainActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();

			main.m_btnQuest.onClick.AddListener(() =>
			{
				Fsm.Event("quest");
			});
		}

	}

	[ActionCategory("QuestMainAction")]
	[HutongGames.PlayMaker.Tooltip("QuestMainAction")]
	public class quest : QuestMainActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
			main.m_panelStage.Initialize((_floor_id) =>
			{
				DataManager.Instance.game_data.list.Clear();
				DataManager.Instance.game_data.WriteInt("floor_id", _floor_id);
				DataManager.Instance.game_data.Save();
				SceneManager.LoadScene("game");				
			});
			main.m_panelStage.gameObject.SetActive(true);
		}

		public override void OnExit()
		{
			base.OnExit();
			main.m_panelStage.gameObject.SetActive(false);
		}

	}



}
