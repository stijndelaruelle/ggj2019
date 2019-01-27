using UnityEngine;
using Sjabloon;

public class VictoryPanelUI : MonoBehaviour
{
	[SerializeField]
	private CanvasGroup m_CanvasGroup;

	private void Start()
	{
		LevelDirector levelDirector = LevelDirector.Instance;

		if (levelDirector == null)
			return;

		levelDirector.LevelSucces += OnLevelSucces; 
	}

	private void OnDestroy()
	{
		LevelDirector levelDirector = LevelDirector.Instance;

		if (levelDirector == null)
			return;

		levelDirector.LevelSucces -= OnLevelSucces; 
	}

	private void OnLevelSucces()
	{
		m_CanvasGroup.Show(true); 
	}
}
