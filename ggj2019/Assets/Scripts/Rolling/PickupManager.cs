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
	private GrowingPickup m_PickupPrefab;

	[SerializeField]
	private PickupData[] m_PickupVarieties;

	[SerializeField]
	private DungballData m_DungballData; 

	private List<Transform> m_SpawnLocations;

	private List<GrowingPickup> m_Pickups; 

	private void Start()
	{
		m_Pickups = new List<GrowingPickup>(); 

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
			int rnd = Random.Range(0, m_PickupVarieties.Length); 

			GrowingPickup pickup = Instantiate(m_PickupPrefab, location.position, Quaternion.identity, m_PickupContainer);
			pickup.PickupData = m_PickupVarieties[rnd];
			pickup.SetSprite(m_PickupVarieties[rnd].OutdoorSprite);
			pickup.PickupEvent += OnPickup; 

			m_Pickups.Add(pickup); 
		}
	}

	private void OnPickup(GrowingPickup pickup)
	{
		pickup.PickupEvent -= OnPickup; 
		m_DungballData.AddPickup(pickup.PickupData); 
	}
}
