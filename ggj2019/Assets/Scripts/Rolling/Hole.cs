using UnityEngine;

public class Hole : MonoBehaviour
{
	[SerializeField]
	private NotificationController m_NotificationController;

	[SerializeField]
	private float m_WarningSize; 
	[SerializeField]
	private float m_MaxSize;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerController player = collision.GetComponent<PlayerController>();

		if (player == null)
			return;

		if(player.Size <= m_WarningSize) 
			m_NotificationController.Notify("Mijn bal is te klein, ik heb meer spullen nodig!");

		if (player.Size >= m_MaxSize)
			m_NotificationController.Notify("Mijn bal is te groot, ik pas niet meer door de ingang!"); 
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		m_NotificationController.Notify(""); 
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		PlayerController player = collision.gameObject.GetComponent<PlayerController>();

		if (player == null)
			return;

		if (player.Size <= m_MaxSize)
			LevelDirector.Instance.CompleteLevel(true); 
	}
}