using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSaveGame : MonoBehaviour
{
    [SerializeField]
    private DungballData m_DungballData;

    private void Start()
    {
        m_DungballData.ClearPickups();
    }
}
