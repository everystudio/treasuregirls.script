using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;

public class DropObject : MonoBehaviour
{
	public Animator m_animator;
	public SpriteRenderer m_spr;
	public SpriteAtlas m_spriteAtlas;

	public MasterItemParam m_master;

	public UnityEvent OnGet = new UnityEvent();

	public void Initialize(MasterItemParam _master)
	{
		m_master = _master;
		m_spr.sprite = m_spriteAtlas.GetSprite(_master.sprite_name);
	}
}
