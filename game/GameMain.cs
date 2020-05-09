using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using TMPro;

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

	public Button m_btnPause;
	public float m_fGameTime;
	public TextMeshProUGUI m_txtLastTime;
	public GameObject m_goPauseCover;

	public TextMeshProUGUI m_txtFloor;

	public SpriteAtlas m_spriteAtlasBackground;

	// 敵用
	public List<GameObject> zako_position;
	public GameObject boss_position;
	public GameObject panel_energy_bar;
	public Enemy m_prefEnemy;
	//public EnergyBar m_prefEnemyHpBar;

	public MasterStageParam master_stage_param;
	public MasterFloorParam master_floor_param;

	public PanelPauseMenu m_panelPauseMenu;

	public GameObject m_prefDamageNum;

	public GameObject m_goGameOver;
}





