using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeetleController : MonoBehaviour
{
	[SerializeField]
	private PlayerController m_DungBall;

	private void Update()
	{
		Vector3 diff = m_DungBall.transform.position - transform.position;
		diff.Normalize();

		float rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 90);

		transform.position = m_DungBall.transform.position + (-m_DungBall.Direction * (m_DungBall.ColliderSize * 2)); 
	}
}
