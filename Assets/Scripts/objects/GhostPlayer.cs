namespace sneakyRacing
{
	using UnityEngine;
	using System.Collections.Generic;

	public class GhostPlayer : MonoBehaviour
	{
		private static List<Dictionary<string, ObjectState>> _objectStateList = new List<Dictionary<string, ObjectState>>();
		/*
		public static void setReplay(int trackIndex, List<Dictionary<string, ObjectState>> objectStateList)
		{
			_objectStateList = objectStateList;
		}
		*/
		private int p0;
		private int p1;

		private float _time = 0.0f;

		private void setFrame(float time)
		{
			p0 = (int)time;
			time -= p0;

			if (p0 > _objectStateList.Count - 2)
			{
				p0 = _objectStateList.Count - 2;
				time = 1.0f;

				enabled = false;

				return;
			}

			p1 = p0 + 1;

			//Debug.Log("p0 = " + p0 + ", p1 = " + p1 + ", num = " + _objectStateList.Count);

			restoreState(p0, p1, time);
		}

		private void restoreState(int p0, int p1, float time)
		{
			Dictionary<string, ObjectState> stateDict_0 = _objectStateList[p0];
			Dictionary<string, ObjectState> stateDict_1 = _objectStateList[p1];

			//Debug.Log("time = " + time);

			foreach (KeyValuePair<string, ObjectState> pair in stateDict_0)
			{
				Transform childTransform = transform.Find(pair.Key);

				if (childTransform != null)
				{
					ObjectState objectState_0 = pair.Value;
					ObjectState objectState_1 = stateDict_1[pair.Key];

					childTransform.position = Vector3.Lerp(objectState_0.position, objectState_1.position, time);
					//Quaternion rotation = inverse(lerp(rotations[p0], rotations[p1], time));
					childTransform.rotation = Quaternion.Lerp(objectState_0.rotation, objectState_1.rotation, time);

					//objectState.restore(childTransform);
				}
				else
					Debug.LogWarning("Cannot find key " + pair.Key);
			}
		}

		private float _frame = 0.0f;

		public float frame
		{
			get
			{
				return _frame;
			}
			set
			{
				_frame = value;

				//int count = ;
				int index = Mathf.RoundToInt(value);

				//Console.trace("value=" + value + ", index=" + Mathf.RoundToInt(index) + ", Count=" + _objectStateList.Count);

				if (index < _objectStateList.Count - 1)
					restoreState(index);
			}
		}

		private void restoreState(int index)
		{
			Dictionary<string, ObjectState> stateDict = _objectStateList[index];

			foreach (KeyValuePair<string, ObjectState> pair in stateDict)
			{
				Transform childTransform = transform.Find(pair.Key);

				if (childTransform != null)
				{
					ObjectState objectState = pair.Value;
					objectState.restore(childTransform);
				}
				else
					Debug.LogWarning("Cannot find key " + pair.Key);
			}
		}

		private void Start()
		{
			TrackData trackData = SettingManager.data.trackList[SettingManager.data.currentTrack];

			SettingData data = SettingManager.data;

			Debug.LogWarning("_data.currentTrack = " + SettingManager.data.currentTrack + ", trackData.objectStateList = " + trackData.objectStateList);
			//Debug.LogWarning("trackData.objectStateList = " + trackData.objectStateList.Count);

			if (trackData.objectStateList != null && trackData.objectStateList.Count > 0)
			{
				_objectStateList = trackData.objectStateList;
			}
			else
			{
				gameObject.SetActive(false);
			}

			Debug.LogWarning("_objectStateList.Count = " + _objectStateList.Count);
		}

		private void FixedUpdate()
		{
			//frame += Time.deltaTime;

			_time += Time.deltaTime * (1.0f / GhostRecorder.frequency);

			setFrame(_time);
		}
	}
}