using UnityEngine;
using Sjabloon;

public class FailurePanelUI : MonoBehaviour
{
	[SerializeField]
	private CanvasGroup m_CanvasGroup;

	private void Start()
	{
		LevelDirector levelDirector = LevelDirector.Instance;

		if (levelDirector == null)
			return;

		levelDirector.LevelFailed += OnLevelFailed; 
	}

	private void OnDestroy()
	{
		LevelDirector levelDirector = LevelDirector.Instance;

		if (levelDirector == null)
			return;

		levelDirector.LevelFailed -= OnLevelFailed; 
	}

	private void OnLevelFailed()
	{
		m_CanvasGroup.Show(true); 
	}
}
