using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
	[SerializeField]
	private PlayerController m_PlayerController; 

	[SerializeField]
	private Transform m_LocationContainer;

	[SerializeField]
	private Transform m_PickupContainer; 

	[SerializeField]
	private GrowingPickup[] m_PickupPrefabs;

	private List<Transform> m_SpawnLocations;

	private List<GrowingPickup> m_Pickups; 

	private void Start()
	{
		m_Pickups = new List<GrowingPickup>(); 

		m_PlayerController.GrowEvent += OnPlayerGrowth; 

		GetLocations();
		SpawnPickups(); 
	}

	private void GetLocations()
	{
		m_SpawnLocations = new List<Transform>();

		Transform[] locations = m_LocationContainer.GetComponentsInChildren<Transform>();

		foreach (Transform location in locations)
		{
			if (location != m_LocationContainer)
				m_SpawnLocations.Add(location);
		}
	}

	private void SpawnPickups()
	{
		foreach (Transform location in m_SpawnLocations)
		{
			int rnd = Random.Range(0, m_PickupPrefabs.Length - 1); 

			GrowingPickup pickup = Instantiate(m_PickupPrefabs[rnd], location.position, Quaternion.identity, m_PickupContainer);
			pickup.OnPlayerGrowth(m_PlayerController.Size); 

			m_Pickups.Add(pickup); 
		}
	}

	private void OnPlayerGrowth(float newSize)
	{
		foreach (GrowingPickup pickup in m_Pickups)
			pickup.OnPlayerGrowth(newSize); 
	}
}
