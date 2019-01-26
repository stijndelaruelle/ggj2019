using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
	[SerializeField]
	private float m_WarningSize;

	[SerializeField]
	private float m_MaxSize; 

	private void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerController player = collision.GetComponent<PlayerController>();

		if (player == null)
			return;

		if (player.Size <= m_WarningSize)
			Debug.Log("Your ball is too small!");
		else if (player.Size >= m_MaxSize)
			Debug.Log("You ball is too large!");
		else LevelDirector.Instance.StopLevel(); 
	}
}
