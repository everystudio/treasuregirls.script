using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdMobInitialize : Singleton<AdMobInitialize>
{
	public bool IsAdMobInitialized { get { return m_bIsAdMobInitialized; } }
	private bool m_bIsAdMobInitialized = false;
	public override void Initialize()
	{
		base.Initialize();
		SetDontDestroy(true);
	}

	public void AdMobInitialized(bool _bFlag)
	{
		m_bIsAdMobInitialized = _bFlag;
	}
}
