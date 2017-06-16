namespace sneakyRacing
{
	using UnityEngine;

	public class Checkpoint : MonoBehaviour
	{
		public System.Action<int> onCheckpointEvent;

		private Transform _pointTransform;
		private Transform _borderTransform;

		private Collider _collider;

		[SerializeField]
		private Color color = Color.blue;

		public int index { get; set; }

		public void hide()
		{
			_pointTransform.gameObject.SetActive(false);

			_collider.enabled = false;
		}

		private void Awake()
		{
			_pointTransform = transform.Find("Body");
			_borderTransform = transform.Find("Border");

			_collider = GetComponent<Collider>();
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

		private void Update()
		{
			_pointTransform.Rotate(Vector3.forward, 5.0f);
			_borderTransform.Rotate(Vector3.left, -5.0f);
		}
	}
}