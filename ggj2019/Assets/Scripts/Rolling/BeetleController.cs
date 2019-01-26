using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeetleController : MonoBehaviour
{
	[SerializeField]
	private PlayerController m_DungBall;

	[SerializeField]
	private CircleCollider2D m_Collider; 

	private bool m_Follow;

	private void Start()
	{
		LevelDirector levelDirector = LevelDirector.Instance;

		if (levelDirector == null)
			return;

		levelDirector.LevelStartEvent += OnLevelStart;
		levelDirector.LevelStopEvent -= OnLevelStop;
	}

	private void OnDestroy()
	{
		LevelDirector levelDirector = LevelDirector.Instance;

		if (levelDirector == null)
			return;

		levelDirector.LevelStartEvent -= OnLevelStart;
		levelDirector.LevelStopEvent -= OnLevelStop;
	}

	private void Update()
	{
		if (!m_Follow)
			return;

		Vector3 diff = m_DungBall.transform.position - transform.position;
		diff.Normalize();

		float rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 90);

		transform.position = m_DungBall.transform.position + (-m_DungBall.Direction * (m_DungBall.ColliderSize + m_Collider.radius));
	}

	private void OnLevelStart()
	{
		m_Follow = true;
	}

	private void OnLevelStop()
	{
		m_Follow = false;
	}
}
