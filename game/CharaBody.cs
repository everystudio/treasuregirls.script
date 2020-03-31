using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaBody : MonoBehaviour
{
	public GameCharaMain game_chara_main;
	public bool IsEnemy;
	public Enemy enemy;
	void OnCollisionEnter2D(Collision2D _collision)
	{
		Debug.Log(_collision.gameObject.name);
		if (_collision.gameObject.tag == Defines.TAG_ENEMY)
		{
			//Debug.Log("start enemy");
			IsEnemy = true;
		}
		else if (_collision.gameObject.tag == Defines.TAG_DROP_ITEM)
		{
			//Debug.Log("hit drop item");
			DropObject drop_obj = _collision.gameObject.transform.parent.gameObject.GetComponent<DropObject>();
			//Debug.Log(drop_obj);
			if( drop_obj != null)
			{
				DataManager.Instance.dataGetItem.Add(drop_obj.m_master.item_id, 1);
				foreach( DataItemParam getitem in DataManager.Instance.dataGetItem.list)
				{
					MasterItemParam master = DataManager.Instance.masterItem.list.Find(p => p.item_id == getitem.item_id);
					//Debug.Log(string.Format("{0}:{1}", master.name, getitem.num));
				}
				drop_obj.OnGet.Invoke();
			}
		}
		else if( _collision.gameObject.tag == Defines.TAG_GOAL)
		{
			game_chara_main.is_goal = true;
		}
	}

	void OnCollisionExit2D(Collision2D _collision)
	{
		if (_collision.gameObject.tag == Defines.TAG_ENEMY)
		{
			//Debug.Log("exit enemy");
			IsEnemy = false;
		}
	}
	public void EventAttackSlash()
	{
		AttackEffect script = PrefabManager.Instance.MakeScript<AttackEffect>(game_chara_main.m_prefAttack, game_chara_main.m_goAttackRoot);
		script.transform.localPosition = new Vector3(0.0f,0.0f,-1.0f);
		script.transform.localScale = Vector3.one * 2.0f;

		script.Initialize(game_chara_main.m_dataUnitParam,null, "enemy");

	}
	public void EventAttackEnd()
	{
		game_chara_main.OnAttackEnd.Invoke();
	}
}
