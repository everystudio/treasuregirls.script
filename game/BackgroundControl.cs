using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControl : MonoBehaviour {

	public GameObject m_goStageRoot;
	public GameObject m_goCharaControl;
	public GameObject m_goCamera;
	public Vector3 offset;

	[SerializeField] private SpriteRenderer spr_renderer;
	private Material mat;
	private void Start()
	{
		mat = spr_renderer.material;
	}

	void Update()
	{
		//Debug.Log(m_goCharaControl.transform.localPosition - m_goCamera.transform.localPosition + offset);
		Vector3 move = ( m_goCharaControl.transform.localPosition + m_goCamera.transform.localPosition + offset);
		float scroll = Mathf.Repeat(1.0f *( move.x / 30.0f), 1.0f);
		//Debug.Log(scroll);
		mat.SetTextureOffset("_MainTex", new Vector2(scroll, 0.0f));
	}

}
