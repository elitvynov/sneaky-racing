namespace sneakyRacing
{
	using UnityEngine;
	using System.Collections.Generic;

	public class GhostPlayer : MonoBehaviour
	{
		private static List<Dictionary<string, ObjectState>> _objectStateList = new List<Dictionary<string, ObjectState>>();

		public static void setReplay(int trackIndex, List<Dictionary<string, ObjectState>> objectStateList)
		{
			_objectStateList = objectStateList;
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
			Debug.LogWarning("_objectStateList.Count = " + _objectStateList.Count);

			if (_objectStateList.Count == 0)
				gameObject.SetActive(false);
		}

		private void Update()
		{
			frame += Time.deltaTime;
		}
	}
}