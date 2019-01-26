using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonUI : MonoBehaviour
{
    [SerializeField]
    private string m_GrowSceneName;

    public void StartGame()
    {
        SceneManager.LoadScene(m_GrowSceneName);
    }
}
