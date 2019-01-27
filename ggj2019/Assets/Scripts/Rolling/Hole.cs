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
			m_NotificationController.Notify("My ball is too small! I need to collect some more shit.");

		if (player.Size >= m_MaxSize)
			m_NotificationController.Notify("My ball is too large! I can't enter the hole."); 
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