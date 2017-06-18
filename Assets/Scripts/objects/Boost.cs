namespace sneakyRacing
{
	using UnityEngine;

	public class Boost : MonoBehaviour
	{
		private Transform _bodyTransform;

		private Collider _collider;

		public void hide()
		{
			_bodyTransform.gameObject.SetActive(false);

			_collider.enabled = false;
		}

		private void Awake()
		{
			_bodyTransform = transform.Find("Body");

			_collider = GetComponent<Collider>();
		}

		private void OnTriggerEnter(Collider collider)
		{
			if (collider.CompareTag("Player"))
			{
				//Debug.LogWarning("Player get checkpoiny: " + onCheckpointEvent);

				//if (onCheckpointEvent != null)
				//	onCheckpointEvent();
			}
		}

		private void Update()
		{
			_bodyTransform.Rotate(Vector3.forward, 5.0f);
		}
	}
}