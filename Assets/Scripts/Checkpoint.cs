namespace sneakyRacing
{
	using UnityEngine;

	public class Checkpoint : MonoBehaviour
	{
		public System.Action<int> onCheckpointEvent;

		private Transform[] points;

		[SerializeField]
		private Color color = Color.blue;

		public int index { get; set; }

		public void hide()
		{
			gameObject.SetActive(false);
		}

		private void OnTriggerEnter(Collider collider)
		{
			if (collider.CompareTag("Player"))
			{
				Debug.LogWarning("Player get checkpoiny: " + onCheckpointEvent);

				if (onCheckpointEvent != null)
					onCheckpointEvent(index);
			}
		}
	}
}