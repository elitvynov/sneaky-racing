namespace sneakyRacing
{
	using UnityEngine;

	using UnityStandardAssets.Cameras;

	public class CameraController : AbstractTargetFollower
	{
		[SerializeField]
		private CheckpointManager _checkpoints;

		[SerializeField]
		private float _distanceMin = 5.0f;

		[SerializeField]
		private float _distanceMax = 50.0f;

		private Vector3 _shift = Vector3.up * 35.0f;

		[SerializeField]
		private float _moveSpeed = 3; // How fast the rig will move to keep up with target's position

		protected override void FollowTarget(float deltaTime)
		{
			// if no target, or no time passed then we quit early, as there is nothing to do
			if (!(deltaTime > 0) || m_Target == null)
			{
				return;
			}

			// initialise some vars, we'll be modifying these in a moment
			Vector3 targetForward = m_Target.forward;
			//Vector3 targetUp = m_Target.up;

			//Vector3[] positions = new Vector3[] { m_Target.position, _checkpoints.getCurrentPoint(), _checkpoints.getNextPoint() };
			//Bounds bounds = GeometryUtility.CalculateBounds(positions, Matrix4x4.identity);

			Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

			bool currentVisible = GeometryUtility.TestPlanesAABB(frustumPlanes, _checkpoints.getCurrentTransform().GetComponent<Collider>().bounds);
			bool nextVisible = GeometryUtility.TestPlanesAABB(frustumPlanes, _checkpoints.getNextTransform().GetComponent<Collider>().bounds);
			
			//Debug.Log("currentVisible = " + currentVisible);
			//Debug.Log("nextVisible = " + nextVisible);
			//Debug.Log("targetForward = " + targetForward);

			if (!currentVisible || !nextVisible)
				_shift.y = Mathf.Clamp(_shift.y + 10.0f * deltaTime, _distanceMin, _distanceMax) + 2.5f;
			
			_shift.y = Mathf.Clamp(_shift.y - 5.0f * deltaTime, _distanceMin, _distanceMax) + 2.5f;

			// camera position moves towards target position:
			//transform.position = Vector3.Lerp(transform.position, bounds.center + _shift, deltaTime * m_MoveSpeed);
			transform.position = Vector3.Lerp(transform.position, (m_Target.position + targetForward * 16.0f) + _shift, deltaTime * _moveSpeed);
		}

		protected override void Start()
		{
			base.Start();

			FollowTarget(1.0f);
		}

		private void OnDrawGizmos()
		{
			if (Application.isPlaying)
			{
				Vector3[] positions = new Vector3[] { m_Target.position, _checkpoints.getCurrentPoint(), _checkpoints.getNextPoint() };

				Gizmos.color = Color.green;

				for (int i = 0; i < positions.Length; i++)
				{
					Gizmos.DrawWireSphere(positions[i], 2.5f);
				}

				Bounds bounds = GeometryUtility.CalculateBounds(positions, Matrix4x4.identity);

				Gizmos.color = Color.white;
				Gizmos.DrawWireCube(bounds.center, bounds.size);

				Gizmos.color = Color.green;
				Gizmos.DrawRay(m_Target.position, m_Target.forward * 16.0f);

				Vector3 cameraPosition = transform.position;
				cameraPosition.y = 0.0f;

				Vector3 targetPosition = m_Target.position;
				targetPosition.y = 0.0f;

				Vector3 forwardPosition = m_Target.forward * 16.0f;
				forwardPosition.y = 0.0f;

				Gizmos.color = Color.red;
				Gizmos.DrawSphere(cameraPosition, 2.5f);
				//Gizmos.DrawSphere(targetPosition, 2.5f);
				Gizmos.DrawSphere(targetPosition + forwardPosition, 2.5f);
			}
		}
	}
}