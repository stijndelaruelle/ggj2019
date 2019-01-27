using UnityEngine;
using Sjabloon;

public class FailurePanelUI : MonoBehaviour
{
	[SerializeField]
	private CanvasGroup m_CanvasGroup;

    [SerializeField]
    private AudioSource m_DefeatJingle;

    private void Start()
	{
		LevelDirector levelDirector = LevelDirector.Instance;

		if (levelDirector == null)
			return;

        levelDirector.LevelStartEvent += OnLevelStart;
        levelDirector.LevelFailed += OnLevelFailed; 
	}

	private void OnDestroy()
	{
		LevelDirector levelDirector = LevelDirector.Instance;

		if (levelDirector == null)
			return;

        levelDirector.LevelStartEvent -= OnLevelStart;
        levelDirector.LevelFailed -= OnLevelFailed; 
	}

    private void OnLevelStart()
    {
        if (m_CanvasGroup != null)
            m_CanvasGroup.Show(false);
    }

    private void OnLevelFailed()
    {
        if (m_CanvasGroup != null)
            m_CanvasGroup.Show(true);

        if (m_DefeatJingle != null)
            m_DefeatJingle.Play();
    }
}

