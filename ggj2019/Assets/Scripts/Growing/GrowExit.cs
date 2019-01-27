using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrowExit : MonoBehaviour
{
    [SerializeField]
    private GrowLevelBorder m_Border;

    [SerializeField]
    private string m_SceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_Border == null)
            return;

        if (m_Border.IsBroken() == false)
            return;

        LevelDirector.Instance.CompleteLevel(true);
    }
}
