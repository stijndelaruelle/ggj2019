using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class EmotionPanel : MonoBehaviour
{
	[SerializeField]
	private Image m_EmoteImage;

	[SerializeField] 
	private Emoticons[] m_EmoticonCollections;

	[SerializeField]
	private bool m_AndroidVersion;

	private void Awake()
	{
#if UNITY_ANDROID
		if (!m_AndroidVersion)
		{
			gameObject.SetActive(false);
		}
		else
			NotificationController.PickupNotification += Emote;
#endif

#if UNITY_STANDALONE || UNITY_EDITOR
		if (m_AndroidVersion)
		{
			gameObject.SetActive(false);
		}
		else NotificationController.PickupNotification += Emote;
#endif

		BallShrinkingSurface.ShrinkingSurfaceEnterEvent += OnShrinkingSurfaceEnter;
		BallShrinkingSurface.ShrinkingSurfaceExitEvent += OnShrinkingSurfaceExit;
	}

	private void OnDestroy()
	{
#if UNITY_ANDROID
		if (m_AndroidVersion)
			NotificationController.PickupNotification -= Emote;
#endif

#if UNITY_STANDALONE || UNITY_EDITOR
		if (!m_AndroidVersion)
			NotificationController.PickupNotification -= Emote;
#endif

		BallShrinkingSurface.ShrinkingSurfaceEnterEvent -= OnShrinkingSurfaceEnter;
		BallShrinkingSurface.ShrinkingSurfaceExitEvent -= OnShrinkingSurfaceExit;
	}


	private void OnShrinkingSurfaceEnter()
	{
		Debug.Log("Shrinking surface entered"); 
		Emote(2);
	}

	private void OnShrinkingSurfaceExit()
	{
		Hide(); 
	}

	private void Emote(int emotion, float duration)
	{
		Emote(emotion); 

		Invoke("Hide", duration); 
	}

	private void Emote(int emotion)
	{
		if (emotion >= m_EmoticonCollections.Length)
		{
			Hide();
			return;
		}

		int rnd = Random.Range(0, m_EmoticonCollections[emotion].EmoticonSprites.Length);
		m_EmoteImage.sprite = m_EmoticonCollections[emotion].EmoticonSprites[rnd];
		m_EmoteImage.enabled = true;
	}

	private void Hide()
	{
		m_EmoteImage.enabled = false; 
	}
}
