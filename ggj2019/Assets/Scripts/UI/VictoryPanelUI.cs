using UnityEngine;
using Sjabloon;

public class VictoryPanelUI : MonoBehaviour
{
	[SerializeField]
	private CanvasGroup m_CanvasGroup;

    [SerializeField]
    private AudioSource m_VictoryJingle;

	private void Start()
	{
		LevelDirector levelDirector = LevelDirector.Instance;

		if (levelDirector == null)
			return;

        levelDirector.LevelStartEvent += OnLevelStart;
        levelDirector.LevelSucces += OnLevelSucces;
	}

	private void OnDestroy()
	{
		LevelDirector levelDirector = LevelDirector.Instance;

		if (levelDirector == null)
			return;

        levelDirector.LevelStartEvent -= OnLevelStart;
        levelDirector.LevelSucces -= OnLevelSucces; 
	}

    private void OnLevelStart()
    {
        if (m_CanvasGroup != null)
            m_CanvasGroup.Show(false);
    }

	private void OnLevelSucces()
	{
        if (m_CanvasGroup != null)
		    m_CanvasGroup.Show(true);

        if (m_VictoryJingle != null)
            m_VictoryJingle.Play();
    }
}
