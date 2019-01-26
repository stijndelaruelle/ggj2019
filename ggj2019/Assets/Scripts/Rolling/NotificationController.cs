using UnityEngine;

public class NotificationController : MonoBehaviour
{
	[SerializeField]
	private StringValue m_NotificationText; 

	public void Notify(string message)
	{
		Debug.Log("Notified");
		m_NotificationText.Value = message;
	}
}
