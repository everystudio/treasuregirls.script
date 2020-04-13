using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampMain : Singleton<CampMain>
{
	public string sub_move;
	public void MoveToGetWeapon()
	{
		sub_move = "weapon";
		ViewPanelManager.Instance.Show("shop");
	}

	public void MoveToShop(string _subMove)
	{
		sub_move = _subMove;
		ViewPanelManager.Instance.Show("shop");
	}

}
