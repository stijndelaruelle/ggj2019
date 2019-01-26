using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingPickup : MonoBehaviour
{
	[SerializeField]
	private float m_GrowAmount;

	private void Start()
	{
		LevelDirector.Instance.LevelStartEvent += OnLevelStart;
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
}
