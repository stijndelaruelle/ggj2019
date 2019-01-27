using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sjabloon;

public class StartPanelUI : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup m_CanvasGroup;

    private void Start()
    {
        LevelDirector.Instance.LevelStartEvent += OnLevelStart;
        m_CanvasGroup.Show(true);
    }

    private void OnDestroy()
    {
        LevelDirector levelDirector = LevelDirector.Instance;

        if (levelDirector != null)
            levelDirector.LevelStartEvent -= OnLevelStart;
    }

    private void OnLevelStart()
    {
        //And never open again!
        m_CanvasGroup.Show(false);
    }
}
