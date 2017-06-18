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

		private float _angle = 5.0f;

		public int index { get; set; }

		public void hide()
		{
			_pointTransform.gameObject.SetActive(false);

			_collider.enabled = false;

			_angle = 4.95f;
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
			if (_angle < 5.0f)
			{
				_angle = Mathf.Clamp(_angle - 5.0f * Time.deltaTime, 0.0f, 5.0f);
			}

			_pointTransform.Rotate(Vector3.forward, _angle);
			_borderTransform.Rotate(Vector3.left, -_angle);
		}
	}
}