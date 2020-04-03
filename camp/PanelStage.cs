using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelStage : MonoBehaviour
{
	public GameObject m_prefStage;
	public GameObject m_goStageRoot;

	public GameObject m_prefFloor;
	public GameObject m_goFloorRoot;

	public void Initialize(Action<int> _actionFloor)
	{
		m_prefStage.SetActive(false);
		MonoBehaviourEx.DeleteObjects<IconStage>(m_goStageRoot);

		int iStage = 0;
		foreach( DataStageParam data_stage in DataManager.Instance.dataStage.list.FindAll(p => 0 < p.status))
		{
			if (iStage == 0)
			{
				iStage = data_stage.stage_id;
			}
			IconStage script = PrefabManager.Instance.MakeScript<IconStage>(m_prefStage, m_goStageRoot);
			script.Initialize(data_stage, DataManager.Instance.masterStage.list.Find(p => p.stage_id == data_stage.stage_id));
			script.m_btn.onClick.AddListener(() =>
			{
				ShowFloorList(data_stage.stage_id , _actionFloor);
			});
		}
		ShowFloorList(iStage, _actionFloor);
	}

	public void ShowFloorList( int _iStage , Action<int> _action)
	{
		m_prefFloor.SetActive(false);
		MonoBehaviourEx.DeleteObjects<BannerFloor>(m_goFloorRoot);

		foreach ( MasterFloorParam master in DataManager.Instance.masterFloor.list.FindAll(p=>p.stage_id == _iStage))
		{
			BannerFloor script = PrefabManager.Instance.MakeScript<BannerFloor>(m_prefFloor, m_goFloorRoot);
			script.Initialize(DataManager.Instance.dataFloor.list.Find(f => f.floor_id == master.floor_id), master);
			script.m_btn.onClick.AddListener(() =>
			{
				_action.Invoke(script.floor_id);
			});
		}
	}


}
