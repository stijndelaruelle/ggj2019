using System.Collections.Generic;
using UnityEngine;

public class GrowLevelGenerator : MonoBehaviour
{
    [SerializeField]
    private DungballData m_RollingDungballData;

    [SerializeField]
    private DungballData m_FallBackDungballData;

    [SerializeField]
    private GrowingPickup m_PickupPrefab;

    [SerializeField]
    private float m_Radius;

    [SerializeField]
    private List<int> m_PickupsPerRing;

    [SerializeField]
    private bool m_AddRandomSpinToRings = true;


    private void Start()
    {
        DungballData dungballData = m_RollingDungballData;

        if (dungballData == null)
            dungballData = m_FallBackDungballData;

        else if (dungballData.PickupCount() <= 0)
            dungballData = m_FallBackDungballData;

        if (dungballData == null)
        {
            Debug.LogWarning("Level cannot be generated, 'RollingDungballData' nor 'FallbackDungballData' found!");
            return;
        }

        Generate(dungballData);
    }

    private void Generate(DungballData dungballData)
    {
        if (m_PickupsPerRing.Count <= 0)
        {
            Debug.LogError("Level can't be generated as there is no spread variable.");
            return;
        }

        if (dungballData == null)
        {
            Debug.LogError("Level can't be generated as there is no dungball data to be found");
            return;
        }

        int numRings = GetNumberOfRings(dungballData);
        if (numRings <= 0)
            return;

        int remainingPickups = dungballData.PickupCount();
        if (remainingPickups <= 0)
            return;

        float radiusOffsetPerRing = (m_Radius / (numRings + 1)); //Div by 0 is impossible as we exited before that!
        int dungBallPickupID = 0;

        for (int ringID = 0; ringID < numRings; ++ringID)
        {
            //How many pickups are there in this ring?
            int maxPickupsInThisRing = 0;
            if (ringID >= m_PickupsPerRing.Count) { maxPickupsInThisRing = m_PickupsPerRing[m_PickupsPerRing.Count - 1]; }
            else                                  { maxPickupsInThisRing = m_PickupsPerRing[ringID]; }

            int numPickupsOnThisRing = 0;
            if (remainingPickups < maxPickupsInThisRing) { numPickupsOnThisRing = remainingPickups; }
            else                                         { numPickupsOnThisRing = maxPickupsInThisRing; }

            remainingPickups -= maxPickupsInThisRing;

            //Spawn each pickup on this ring
            float degreesPerPickupSlot = (360 / numPickupsOnThisRing);
            float randomRotationOfRing = 0;

            if (m_AddRandomSpinToRings)
                randomRotationOfRing = UnityEngine.Random.Range(0, 360);

            for (int pickupOnThisRingID = 0; pickupOnThisRingID < numPickupsOnThisRing; ++pickupOnThisRingID)
            {
                float angle = (degreesPerPickupSlot * pickupOnThisRingID) + randomRotationOfRing;

                Vector3 dir = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle),
                                          Mathf.Cos(Mathf.Deg2Rad * angle), 0.0f);

                dir = dir.normalized;

                //Actually spawn the pickup
                GrowingPickup newPickup = GameObject.Instantiate<GrowingPickup>(m_PickupPrefab, this.transform);
                newPickup.PickupData = dungballData.GetPickupData(dungBallPickupID);

                newPickup.transform.position = dir * (radiusOffsetPerRing * (ringID + 1));
                dungBallPickupID += 1;
            }
        }
    }

    private int GetNumberOfRings(DungballData dungballData)
    {
        //Decide how many rings we'll need
        int numRings = 0;
        int remainingPickups = dungballData.PickupCount();

        if (remainingPickups <= 0)
            return 0;

        while (remainingPickups > 0)
        {
            //How many pickups are there in this ring?
            int maxNumberOfPickups = 0;
            if (numRings >= m_PickupsPerRing.Count) { maxNumberOfPickups = m_PickupsPerRing[m_PickupsPerRing.Count - 1]; }
            else                                    { maxNumberOfPickups = m_PickupsPerRing[numRings]; }

            //Remove that amount from all the pickups we have to devide
            remainingPickups -= m_PickupsPerRing[numRings];

            numRings += 1;
        }

        return numRings;
    }

    private int PickupsOnRing(DungballData dungballData, int ringID)
    {
        int remainingPickups = dungballData.PickupCount();

        if (remainingPickups <= 0)
            return 0;

        for (int i = 0; i < ringID; ++i)
        {
            //How many pickups are there in this ring?
            int maxPickupsInThisRing = 0;
            if (i >= m_PickupsPerRing.Count) { maxPickupsInThisRing = m_PickupsPerRing[m_PickupsPerRing.Count - 1]; }
            else                             { maxPickupsInThisRing = m_PickupsPerRing[i]; }

            remainingPickups -= maxPickupsInThisRing;
        }

        if (remainingPickups < 0)
            return 0;

        return remainingPickups;
    }
}
