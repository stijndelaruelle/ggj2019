using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class PlayButtonUI : MonoBehaviour
{
    [SerializeField]
    private string m_SceneName;

    [SerializeField]
    private Image m_FadeImage;

    public void StartGame()
    {

        if (m_FadeImage == null)
        {
            OnFadeComplete();
            return;
        }

        m_FadeImage.DOFade(1.0f, 1.0f).OnComplete(OnFadeComplete);
    }

    private void OnFadeComplete()
    {
        SceneManager.LoadScene(m_SceneName);
    }
}
