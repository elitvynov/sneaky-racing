namespace sneakyRacing
{
	using UnityEngine;

	public class ElasticRotation : MonoBehaviour
	{
		[SerializeField]
		private float _elasticity = 0.8f;

		[SerializeField]
		private float _strength = 0.1f;

		[SerializeField, Tooltip("Delay time, before start.")]
		private float _delay = 0.0f;

		private float _delayCounter = 0.0f;

		[SerializeField, Tooltip("Original rotation, to start with.")]
		private float _currentRotation = 0.0f;

		public float currentRotation
		{
			get
			{
				return _currentRotation;
			}
			set
			{
				_currentRotation = value;
			}
		}

		[SerializeField]
		private float _desirableRotation = 0.0f;

		public float desirableRotation
		{
			get
			{
				return _desirableRotation;
			}
			set
			{
				_desirableRotation = value;

				_delayCounter = 0.0f;
			}
		}

		[SerializeField, Tooltip("Could be zero or negative(starting force).")]
		private float _deltaRotation = 0.0f;

		private void Start()
		{
			transform.localRotation = Quaternion.AngleAxis(_currentRotation, Vector3.up);
		}

		private void Update()
		{
			if (_delayCounter < _delay)
			{
				_delayCounter += Time.deltaTime;

				return;
			}

			float velocity = _desirableRotation - _currentRotation;
			_deltaRotation = _deltaRotation * _elasticity + velocity * _strength;

			_currentRotation += _deltaRotation;

			transform.localRotation = Quaternion.AngleAxis(_currentRotation, Vector3.up);
		}
	}
}