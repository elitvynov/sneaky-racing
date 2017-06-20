namespace sneakyRacing
{
	using UnityEngine;
	using System.Collections.Generic;

	public class GhostRecorder : MonoBehaviour
	{
		public const float frequency = 0.25f;

		private List<Dictionary<string, ObjectState>> _objectStateList;

		private float _recordFrameTime = 0.0f;
		private bool _isRecording = false;
		
		public List<Dictionary<string, ObjectState>> getReplay()
		{
			return _objectStateList;
		}
		
		public void record()
		{
			_objectStateList = new List<Dictionary<string, ObjectState>>();
			_recordFrameTime = 0.0f;
			_isRecording = true;

			saveCurrentState();
		}

		public void stop()
		{
			//disablePhysics();

			_isRecording = false;
		}
		
		private void saveCurrentState()
		{
			Dictionary<string, ObjectState> stateDict = new Dictionary<string, ObjectState>();
			
			foreach (Transform childTransform in transform)
			{
				if (childTransform.gameObject.activeSelf)
				{
					//Debug.Log("_objectStateList = " + _objectStateList.Count);
					ObjectState state = new ObjectState(childTransform, Time.realtimeSinceStartup);

					stateDict[childTransform.name] = state;
				}
			}

			_objectStateList.Add(stateDict);
		}
		
		private void Start()
		{
			record();
		}

		private void FixedUpdate()
		{
			if (_isRecording)
			{
				if (_recordFrameTime > frequency)
				{
					saveCurrentState();

					_recordFrameTime = 0.0f;
				}
				else
					_recordFrameTime += Time.deltaTime;
			}
		}
	}

	public class ObjectState
	{
		public float time;
		
		public Vector3 position;
		public Quaternion rotation;

		public ObjectState(Transform transform, float time)
		{
			//_transform = transform;

			position = transform.position;
			rotation = transform.rotation;

			this.time = time;
		}

		public void restore(Transform transform)
		{
			transform.position = position;
			transform.rotation = rotation;
		}
	}
}