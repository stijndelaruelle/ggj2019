using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
	[SerializeField]
	private Collider2D m_BoundingArea;

	[SerializeField]
	private GrowingPickup m_PickupPrefab;

	[SerializeField]
	private int m_Amount;

	private List<GrowingPickup> m_Pickups;

	private void Start()
	{
		
	}
}
