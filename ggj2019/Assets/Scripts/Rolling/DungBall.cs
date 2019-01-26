using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungBall : MonoBehaviour
{
	private float m_Radius = 0.5f;
	private float m_DefaultIncrement = 0.2f; 

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log(collision);
		PickUp pickUp = collision.gameObject.GetComponent<PickUp>();

		if (pickUp == null)
			return;

		pickUp.GetPickUp();
		m_Radius += m_DefaultIncrement; 
	}

	public float Radius
	{
		get { return m_Radius; }
	}
}
