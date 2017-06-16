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

			//if (m_FollowVelocity && Application.isPlaying)
			{
				// in follow velocity mode, the camera's rotation is aligned towards the object's velocity direction
				// but only if the object is traveling faster than a given threshold.

				//if (targetRigidbody.velocity.magnitude > m_TargetVelocityLowerLimit)
				{
					// velocity is high enough, so we'll use the target's velocty
					//targetForward = targetRigidbody.velocity.normalized;
					//targetUp = Vector3.up;
				}

				//m_CurrentTurnAmount = Mathf.SmoothDamp(m_CurrentTurnAmount, 1, ref m_TurnSpeedVelocityChange, m_SmoothTurnTime);
			}
			/*
			else
			{
				// we're in 'follow rotation' mode, where the camera rig's rotation follows the object's rotation.

				// This section allows the camera to stop following the target's rotation when the target is spinning too fast.
				// eg when a car has been knocked into a spin. The camera will resume following the rotation
				// of the target when the target's angular velocity slows below the threshold.
				var currentFlatAngle = Mathf.Atan2(targetForward.x, targetForward.z) * Mathf.Rad2Deg;
				if (m_SpinTurnLimit > 0)
				{
					var targetSpinSpeed = Mathf.Abs(Mathf.DeltaAngle(m_LastFlatAngle, currentFlatAngle)) / deltaTime;
					var desiredTurnAmount = Mathf.InverseLerp(m_SpinTurnLimit, m_SpinTurnLimit * 0.75f, targetSpinSpeed);
					var turnReactSpeed = (m_CurrentTurnAmount > desiredTurnAmount ? .1f : 1f);
					if (Application.isPlaying)
					{
						m_CurrentTurnAmount = Mathf.SmoothDamp(m_CurrentTurnAmount, desiredTurnAmount,
															 ref m_TurnSpeedVelocityChange, turnReactSpeed);
					}
					else
					{
						// for editor mode, smoothdamp won't work because it uses deltaTime internally
						m_CurrentTurnAmount = desiredTurnAmount;
					}
				}
				else
				{
					m_CurrentTurnAmount = 1;
				}
				m_LastFlatAngle = currentFlatAngle;
			}
			*/

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
			/*
			// camera's rotation is split into two parts, which can have independend speed settings:
			// rotating towards the target's forward direction (which encompasses its 'yaw' and 'pitch')
			if (!m_FollowTilt)
			{
				targetForward.y = 0;
				if (targetForward.sqrMagnitude < float.Epsilon)
				{
					targetForward = transform.forward;
				}
			}
			var rollRotation = Quaternion.LookRotation(targetForward, m_RollUp);

			// and aligning with the target object's up direction (i.e. its 'roll')
			m_RollUp = m_RollSpeed > 0 ? Vector3.Slerp(m_RollUp, targetUp, m_RollSpeed * deltaTime) : Vector3.up;
			transform.rotation = Quaternion.Lerp(transform.rotation, rollRotation, m_TurnSpeed * m_CurrentTurnAmount * deltaTime);
			*/
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