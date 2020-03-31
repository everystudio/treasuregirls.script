using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoButton : MonoBehaviour
{
	public Button m_btn;
	public ArrowButton m_btnLeft;
	public ArrowButton m_btnRight;

	public Animator m_animator;
	private bool m_bIsAuto;

	void Start()
	{
		m_btn.interactable = false;
	}

	public void Initialize( bool _bAuto )
	{
		m_btn.interactable = true;
		m_btn.onClick.RemoveAllListeners();
		m_btn.onClick.AddListener(() =>
		{
			m_bIsAuto = !m_bIsAuto;
			push_button(m_bIsAuto);
		});

		m_bIsAuto = _bAuto;
		push_button(_bAuto);
	}

	private void push_button(bool _bAuto)
	{
		m_animator.SetBool("on", _bAuto);

		m_btnLeft.m_btn.interactable = !_bAuto;
		m_btnRight.m_btn.interactable = !_bAuto;

		m_btnRight.is_press = _bAuto;
	}

}
