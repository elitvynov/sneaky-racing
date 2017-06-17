namespace sneakyRacing
{
	using UnityEngine;
	using System.Collections.Generic;

	public class GhostRecorder : MonoBehaviour
	{
		private List<Dictionary<string, ObjectState>> _objectStateList;

		private float _recordFrameTime = 0.0f;
		private bool _isRecording = false;
		
		//private float _lerp;
		/*
		public float frame
		{
			set
			{
				int count = _objectStateList.Count - 1;
				float index = (float)count * value;

				//Console.trace("value=" + value + ", index=" + Mathf.RoundToInt(index) + ", Count=" + _objectStateList.Count);

				restoreState(Mathf.RoundToInt(index));
			}
		}
		*/
		public void record()
		{
			Debug.Log("Time.fixedDeltaTime = " + Time.fixedDeltaTime);

			_objectStateList = new List<Dictionary<string, ObjectState>>();
			_recordFrameTime = 0.0f;
			_isRecording = true;

			saveCurrentState();
			/*
			_fromTimeScale = Time.timeScale * 0.75f;
			_destTimeScale = Time.timeScale * 0.25f;

			_fromFixedDeltaTime = Time.fixedDeltaTime * 0.75f;
			_destFixedDeltaTime = Time.fixedDeltaTime * 0.25f;

			Camera mainCamera = Camera.main;

			if (Random.value < 0.0f)
			{
				int cameraIndex = Random.Range(0, _cameraList.Count);
				
				_cameraCurrent = _cameraList[cameraIndex];
			}
			else
				_cameraCurrent = mainCamera;

			_fromFieldOfView = mainCamera.fieldOfView;
			_fromPosition = mainCamera.transform.localPosition;
			_fromRotation = mainCamera.transform.localRotation;

			_destFieldOfView = _cameraCurrent.fieldOfView;
			_destPosition = _cameraCurrent.transform.localPosition;
			_destRotation = _cameraCurrent.transform.localRotation;

			_cameraCurrent.transform.localPosition = _fromPosition;
			_cameraCurrent.transform.localRotation = _fromRotation;
			_cameraCurrent.enabled = true;
			*/
			//_lerp = 0.0f;
		}

		public void stop()
		{
			//disablePhysics();

			GhostPlayer.setReplay(SettingManager.data.currentTrack, _objectStateList);

			_isRecording = false;
		}
		
		private void saveCurrentState()
		{
			//int layerGui = LayerMask.NameToLayer("UI");

			//Transform[] transforms = transform.GetComponentsInChildren<Transform>();

			//GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();

			Dictionary<string, ObjectState> stateDict = new Dictionary<string, ObjectState>();
			
			foreach (Transform childTransform in transform)
			{
				if (childTransform.gameObject.activeSelf)
				{

					//if (gameObject.CompareTag("MainCamera"))
					//	continue;
					//Debug.Log("_objectStateList = " + _objectStateList.Count);
					ObjectState state = new ObjectState(childTransform, Time.realtimeSinceStartup);

					stateDict[childTransform.name] = state;
				}
			}

			

			_objectStateList.Add(stateDict);
		}
		/*
		private void restoreState(int index)
		{
			Dictionary<string, ObjectState> stateDict = _objectStateList[index];

			foreach (KeyValuePair<string, ObjectState> pair in stateDict)
			{
				ObjectState objectState = pair.Value;
				objectState.restore();
			}
		}
		*/
		private void Start()
		{
			record();
		}

		private void Update()
		{
			

			if (_isRecording)
			{
				//if (_lerp == 1.0f)
				//	return;

				if (_recordFrameTime > 0.25f)
				{
					saveCurrentState();

					_recordFrameTime = 0.0f;
				}
				else
					_recordFrameTime += Time.deltaTime;

				//_lerp = Mathf.Clamp01(_lerp + 0.1f * Time.deltaTime);
				/*
				Time.timeScale = Mathf.Lerp(_fromTimeScale, _destTimeScale, _lerp);
				Time.fixedDeltaTime = Mathf.Lerp(_fromFixedDeltaTime, _destFixedDeltaTime, _lerp);

				//Console.trace("Time.timeScale = " + Time.timeScale + ", Time.fixedDeltaTime = " + Time.fixedDeltaTime);

				_cameraCurrent.fieldOfView = Mathf.Lerp(_fromFieldOfView, _destFieldOfView, _lerp);
				_cameraCurrent.transform.localPosition = Vector3.Slerp(_fromPosition, _destPosition, _lerp);
				_cameraCurrent.transform.localRotation = Quaternion.Slerp(_fromRotation, _destRotation, _lerp);
				*/
			}
		}
	}

	public class ObjectState
	{
		//[System.NonSerialized]
		//private Transform _transform;

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