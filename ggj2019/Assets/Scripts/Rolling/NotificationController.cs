using UnityEngine;

public class NotificationController : MonoBehaviour
{
	[SerializeField]
	private StringValue m_NotificationText; 

	public void Notify(string message)
	{
		m_NotificationText.Value = message;
	}
}
