using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : Singleton<GameMain>
{
	public bool IsGoal;

	public GameObject m_goFadePanel;
	public PanelResult m_panelResult;

	public GameObject m_goIconRoot;
	public IconPotion icon_potion;
	public IconSkill[] icon_skill_arr;

	public GameCharaMain player_chara;

}





