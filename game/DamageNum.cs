using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNum : MonoBehaviour
{
	public TMPro.TextMeshPro m_txtNum;
	public Rigidbody2D m_rb;

	/*
	private void Start()
	{
		m_rb.AddForce(100.0f * new Vector2(
			true ? 1.0f : -1.0f * UtilRand.GetRange(1.0f, 0.5f),
			UtilRand.GetRange(2.0f, 1.5f)));
	}
	*/

	public void Initialize(int _iNum , bool _bIsRight)
	{
		m_txtNum.text = _iNum.ToString();
		m_rb.AddForce(UtilRand.GetRange(120.0f, 80.0f) * new Vector2(
			_bIsRight ? 1.0f : -1.0f * UtilRand.GetRange(0.75f, 0.5f),
			UtilRand.GetRange(2.0f, 1.5f)));

		Destroy(gameObject, 2.0f);
	}
}
