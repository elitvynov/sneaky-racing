namespace sneakyRacing
{
	using UnityEngine;

	public class CheckpointManager : MonoBehaviour
	{
		private Checkpoint[] points;

		public int getRestCount()
		{
			return points.Length - _currentPoint;
		}

		private int _currentPoint;

		[SerializeField]
		private Color color = Color.blue;

		public Transform getCurrentTransform()
		{
			return points[_currentPoint].transform;//.position;
		}

		public Transform getNextTransform()
		{
			//Debug.Log("points.Length = " + points.Length + ", _currentPoint = " + _currentPoint);

			if (_currentPoint + 1 < points.Length)
				return points[_currentPoint + 1].transform;//.position;
			else
				return points[_currentPoint].transform;//.position;
		}

		public Vector3 getCurrentPoint()
		{
			return points[_currentPoint].transform.position;
		}

		public Vector3 getNextPoint()
		{
			//Debug.Log("points.Length = " + points.Length + ", _currentPoint = " + _currentPoint);

			if (_currentPoint + 1 < points.Length)
				return points[_currentPoint + 1].transform.position;
			else
				return points[_currentPoint].transform.position;
		}

		private void onCheckpointEvent(int index)
		{
			Debug.LogWarning("index = " + index + ", _currentPoint: " + _currentPoint);

			if (index == _currentPoint)
			{
				points[index].onCheckpointEvent -= onCheckpointEvent;
				points[index].hide();

				if (_currentPoint + 1 < points.Length)
				{
					_currentPoint++;

					(Level.instance.menu as GameMenu).instrumentPanel.checkpoints = getRestCount();

					Debug.LogWarning("Next checkpoint: " + _currentPoint);
				}
				else
				{
					(Level.instance as TrackLevel).gameOver();
				}
			}
		}

		public void correctNames()
		{
			points = transform.GetComponentsInChildren<Checkpoint>();

			for (int i = 0; i < points.Length; i++)
			{
				points[i].gameObject.name = "Point_" + i;
			}
		}

		private void Awake()
		{
			points = transform.GetComponentsInChildren<Checkpoint>();

			for (int i = 0; i < points.Length; i++)
			{
				points[i].onCheckpointEvent += onCheckpointEvent;
				points[i].index = i;
			}

			_currentPoint = 0;
		}

		private void Start()
		{
			(Level.instance.menu as GameMenu).instrumentPanel.checkpoints = getRestCount();
		}

		private void OnDrawGizmos()
		{
			if (!Application.isPlaying)
			{
				points = transform.GetComponentsInChildren<Checkpoint>();

				_currentPoint = 0;
			}

			Gizmos.color = color;
			GizmosLite.color = color;

			//Debug.Log("points.Length = " + points.Length + ", _currentPoint = " + _currentPoint);

			for (int i = 0; i < points.Length; i++)
			{
				if (i == 0)
					Gizmos.DrawSphere(points[i].transform.position, 2);
				else
					Gizmos.DrawSphere(points[i].transform.position, 1);

				if (i < points.Length - 1)
					GizmosLite.drawLineArrowXY(points[i].transform.position, points[i + 1].transform.position, 3.5f, 30.0f);
			}
		}
	}
}