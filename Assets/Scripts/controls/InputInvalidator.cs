namespace sneakyRacing
{
	using UnityEngine;

	public class InputInvalidator : MonoBehaviour 
	{
		private const float _invalidateDelay = 0.65f;

		private float _eventInvalidateInterval = 0.0f;

		public bool invalidateEvent()
		{
			if (_eventInvalidateInterval > _invalidateDelay)
			{
				_eventInvalidateInterval = 0.0f;

				return true;
			}
			else
				return false;
		}

		private void Update()
		{
			_eventInvalidateInterval += Time.unscaledDeltaTime;
		}
	}
}