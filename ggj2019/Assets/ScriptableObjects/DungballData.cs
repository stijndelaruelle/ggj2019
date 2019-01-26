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

    public PickupData GetPickupData(int pickupID)
    {
        if (m_DungballData == null)
            return null;

        if (pickupID < 0 || pickupID >= m_DungballData.Count)
            return null;

        return m_DungballData[pickupID];
    }

    public int PickupCount()
    {
        if (m_DungballData != null)
            return m_DungballData.Count;

        return 0;
    }
}
