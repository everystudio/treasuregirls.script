using HutongGames.PlayMaker;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharaMainAction
{
	[ActionCategory("CharaMainAction")]
	[HutongGames.PlayMaker.Tooltip("CharaMainAction")]
	public abstract class CharaMainActionBase : FsmStateAction
	{
		protected CharaMain charaMain;
		public override void OnEnter()
		{
			base.OnEnter();
			charaMain = Owner.gameObject.GetComponent<CharaMain>();
		}
	}

	[ActionCategory("CharaMainAction")]
	[HutongGames.PlayMaker.Tooltip("CharaMainAction")]
	public class startup_charamain : CharaMainActionBase
	{
		public FsmInt chara_id;
		public override void OnEnter()
		{
			base.OnEnter();

			charaMain.m_prefIconChara.SetActive(false);

			charaMain.icon_chara_list.Clear();
			MonoBehaviourEx.DeleteObjects<IconCharaList>(charaMain.m_goIconRoot);
			foreach(MasterCharaParam chara in DataManager.Instance.masterChara.list)
			{
				IconCharaList script = PrefabManager.Instance.MakeScript<IconCharaList>(charaMain.m_prefIconChara, charaMain.m_goIconRoot);
				script.Initialize(chara, DataManager.Instance.dataChara.list.Find(p => p.chara_id == chara.chara_id));
				charaMain.icon_chara_list.Add(script);
			}

			chara_id.Value = DataManager.Instance.dataChara.list.Find(p => p.status == DataChara.STATUS.USING.ToString()).chara_id;
			Finish();
		}
	}


	[ActionCategory("CharaMainAction")]
	[HutongGames.PlayMaker.Tooltip("CharaMainAction")]
	public class SelectList : CharaMainActionBase
	{
		public FsmInt chara_id;
		public override void OnEnter()
		{
			base.OnEnter();

			foreach (IconCharaList icon in charaMain.icon_chara_list)
			{
				icon.Select(chara_id.Value);
			}

			MasterCharaParam master = DataManager.Instance.masterChara.list.Find(p => p.chara_id == chara_id.Value);

			DataCharaParam data = DataManager.Instance.dataChara.list.Find(p => p.chara_id == chara_id.Value);

			if( data == null)
			{
				charaMain.m_txtBtnSet.text = "<color=red>未購入</color>";
				charaMain.m_txtBtnBuy.text = "購入";
				charaMain.m_txtBtnPrice.text = master.price.ToString();
				charaMain.m_btnSet.interactable = false;
				charaMain.m_btnBuy.interactable = true;
				charaMain.m_goGemLessCover.SetActive(DataManager.Instance.GetGem() < master.price);
			}
			else
			{
				charaMain.m_txtBtnSet.text = "<color=black>セットする</color>";
				charaMain.m_txtBtnBuy.text = "購入済";
				charaMain.m_txtBtnPrice.text = master.price.ToString();
				charaMain.m_btnSet.interactable = true;
				charaMain.m_btnBuy.interactable = false;
				charaMain.m_goGemLessCover.SetActive(false);
			}



			Finish();

		}

	}

	[ActionCategory("CharaMainAction")]
	[HutongGames.PlayMaker.Tooltip("CharaMainAction")]
	public class CharaSelect : CharaMainActionBase
	{
		public FsmInt chara_id;
		public override void OnEnter()
		{
			base.OnEnter();
			charaMain.m_charaView.Initialize();
			foreach ( IconCharaList icon in charaMain.icon_chara_list)
			{
				icon.OnClickIcon.AddListener(OnSelect);
			}

			charaMain.m_btnBuy.onClick.AddListener(() =>
			{
				Fsm.Event("buy");
			});
			charaMain.m_btnSet.onClick.AddListener(() =>
			{
				Fsm.Event("set");
			});
		}

		private void OnSelect(int arg0)
		{
			chara_id.Value = arg0;
			Fsm.Event("select");
		}

		public override void OnExit()
		{
			base.OnExit();
			foreach (IconCharaList icon in charaMain.icon_chara_list)
			{
				icon.OnClickIcon.RemoveAllListeners();
			}
			charaMain.m_btnBuy.onClick.RemoveAllListeners();
			charaMain.m_btnSet.onClick.RemoveAllListeners();
		}
	}


	[ActionCategory("CharaMainAction")]
	[HutongGames.PlayMaker.Tooltip("CharaMainAction")]
	public class Buy : CharaMainActionBase
	{
		public FsmInt chara_id;
		public override void OnEnter()
		{
			base.OnEnter();

			MasterCharaParam master = DataManager.Instance.masterChara.list.Find(p => p.chara_id == chara_id.Value);
			DataCharaParam add_chara = new DataCharaParam();

			if ( DataManager.Instance.UseGem(master.price))
			{
				add_chara.chara_id = chara_id.Value;
				add_chara.status = DataChara.STATUS.IDLE.ToString();
				DataManager.Instance.dataChara.list.Add(add_chara);
			}

			foreach( IconCharaList icon in charaMain.icon_chara_list)
			{
				icon.SetData(add_chara);
			}

			DataManager.Instance.dataChara.Save();
			DataManager.Instance.user_data.Save();
			Finish();
		}

	}

	[ActionCategory("CharaMainAction")]
	[HutongGames.PlayMaker.Tooltip("CharaMainAction")]
	public class Set : CharaMainActionBase
	{
		public FsmInt chara_id;
		public override void OnEnter()
		{
			base.OnEnter();

			DataManager.Instance.dataChara.list.Find(p => p.status == DataChara.STATUS.USING.ToString()).status = DataChara.STATUS.IDLE.ToString();
			DataManager.Instance.dataChara.list.Find(p => p.chara_id == chara_id.Value).status = DataChara.STATUS.USING.ToString();

			Finish();
		}

	}

}
