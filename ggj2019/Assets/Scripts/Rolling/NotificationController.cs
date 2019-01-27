using UnityEngine;

public class NotificationController : MonoBehaviour
{
	public delegate void PickupNotificationEvent(int id, float duration); 

	[SerializeField]
	private StringValue m_NotificationText;

	[SerializeField]
	private EmotionPanel m_EmotionPanel;

	public static event PickupNotificationEvent PickupNotification; 

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
		int notificationType = 0; 

		if (pickup.PickupData.MoveSpeedAmount > 0)
			notificationType = 1;

		if (PickupNotification != null)
			PickupNotification(notificationType, 1.0f); 
	}

	public void Notify(string message)
	{
		m_NotificationText.Value = message;
	}
}
