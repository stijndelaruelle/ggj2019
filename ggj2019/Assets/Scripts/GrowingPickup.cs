using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GrowingPickup : MonoBehaviour
{
	[SerializeField]
	private float m_GrowAmount;

	[SerializeField]
	private float m_PickupSize;

	private Collider2D m_Collider; 

	private void Start()
	{
		LevelDirector.Instance.LevelStartEvent += OnLevelStart;

		m_Collider = GetComponent<Collider2D>(); 

		//if (m_Collider != null)
		//	m_Collider.isTrigger = false; 
	}

	private void OnDestroy()
	{
		LevelDirector levelDirector = LevelDirector.Instance;

		if (levelDirector != null)
			levelDirector.LevelStartEvent -= OnLevelStart;
	}

	private void OnLevelStart()
	{
		gameObject.SetActive(true);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		IGrowable growable = collision.GetComponent<IGrowable>();

		if (growable != null)
		{
			growable.Grow(m_GrowAmount);
			gameObject.SetActive(false);
		}
	}

	public void OnPlayerGrowth(float newSize)
	{
		if (newSize >= m_PickupSize && m_Collider != null)
			m_Collider.isTrigger = true; 
	}
}
