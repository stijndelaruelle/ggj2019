using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Disco Dungball/Dungball Data")]
public class DungballData : ScriptableObject
{
    [SerializeField]
    private List<PickupData> m_DungballData;

    public void AddPickup(PickupData pickupData)
    {
        if (m_DungballData != null)
            m_DungballData.Add(pickupData);
    }
}
