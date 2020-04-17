using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class GameMain : Singleton<GameMain>
{
	public bool IsGoal;

	public GameObject m_goFadePanel;
	public PanelResult m_panelResult;

	public GameObject m_goIconRoot;
	public IconPotion icon_potion;
	public IconSkill[] icon_skill_arr;

	public GameCharaMain player_chara;

	public AutoButton m_btnAuto;
	public AutoButtonPotion m_btnAutoPotion;

	public BackgroundControl background;


	public SpriteAtlas m_spriteAtlasBackground;

	// 敵用
	public List<GameObject> zako_position;
	public GameObject boss_position;
	public GameObject panel_energy_bar;
	public Enemy m_prefEnemy;
	public EnergyBar m_prefEnemyHpBar;




}





