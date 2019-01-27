using UnityEngine;

public class NotificationController : MonoBehaviour
{
	[SerializeField]
	private StringValue m_NotificationText;

	[SerializeField]
	private EmotionPanel m_EmotionPanel;

	private void Awake()
	{
		GrowingPickup.PickupEvent += OnPickupEvent; 
	}

	private void OnDestroy()
	{
		GrowingPickup.PickupEvent -= OnPickupEvent; 
	}

	private void OnPickupEvent(GrowingPickup pickup)
	{
		if (pickup.PickupData.MoveSpeedAmount > 0)
			m_EmotionPanel.Emote(0, 1.0f);

		if (pickup.PickupData.GrowAmount > 0)
			m_EmotionPanel.Emote(0, 1.0f); 
	}

	public void Notify(string message)
	{
		m_NotificationText.Value = message;
	}
}
