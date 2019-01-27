using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class EmotionPanel : MonoBehaviour
{
	[SerializeField]
	private Image m_EmoteImage;

	[SerializeField] 
	private Sprite[] m_EmotionSprites;

	public void Emote(int emotion, float duration)
	{
		if (emotion >= m_EmotionSprites.Length)
		{
			Hide();
			return;
		}

		m_EmoteImage.sprite = m_EmotionSprites[emotion];
		m_EmoteImage.enabled = true;

		Invoke("Hide", duration); 
	}

	private void Hide()
	{
		m_EmoteImage.enabled = false; 
	}
}
