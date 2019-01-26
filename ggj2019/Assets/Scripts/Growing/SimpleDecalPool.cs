using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDecalPool : MonoBehaviour
{
    [SerializeField]
    private GameObject m_DecalPrefab;

    [SerializeField]
    private int m_Amount;

    private List<GameObject> m_Decals;
    private int m_CurrentDecalID = 0;

    //Methods
    private void Start()
    {
        LevelDirector.Instance.LevelResetEvent += OnLevelReset;

        //Spawn all the decals
        m_Decals = new List<GameObject>();
        for (int i = 0; i < m_Amount; ++i)
        {
            GameObject newDecal = GameObject.Instantiate<GameObject>(m_DecalPrefab, this.transform);
            newDecal.SetActive(false);

            m_Decals.Add(newDecal);
        }
    }

    private void OnDestroy()
    {
        if (m_Decals == null)
            return;

        for (int i = 0; i < m_Decals.Count; ++i)
        {
            GameObject.Destroy(m_Decals[i]);
        }
    
        m_Decals.Clear();
    }

    public void PlaceDecal(Vector3 position)
    {
        if (m_CurrentDecalID < 0)
        {
            Debug.LogError("Invalid decal ID! Should be above zero!");
            return;
        }

        if (m_CurrentDecalID >= m_Decals.Count)
        {
            Debug.LogWarning("Out of decals! Reusing older decals for now, but maybe you should spawn more!");
            m_CurrentDecalID = 0;
        }

        m_Decals[m_CurrentDecalID].transform.position = position;
        m_Decals[m_CurrentDecalID].SetActive(true);

        m_CurrentDecalID += 1;
    }

    private void OnLevelReset()
    {
        for (int i = 0; i < m_Decals.Count; ++i)
        {
            //No need to reset their position, as they will be disabled anyway.
            m_Decals[i].SetActive(false);
        }

        m_CurrentDecalID = 0;
    }
}
