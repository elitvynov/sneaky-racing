namespace sneakyRacing
{
	using UnityEngine;

	using UnityStandardAssets.Vehicles.Car;

	public class TouchControl : MonoBehaviour 
	{
		[SerializeField]
		private CarController _car;

		private RectTransform _left;
		private RectTransform _right;

		private void Awake()
		{
			_left = transform.Find("Left") as RectTransform;
			_right = transform.Find("Right") as RectTransform;
		}

		private void Start()
		{
			gameObject.SetActive(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer);
		}

		private float _steering = 0.0f;

		private void Update()
		{
			float horizontalInput = 0.0f;

			for (int i = 0; i < Input.touches.Length; i++)
			{
				Vector2 touchPosition = Input.touches[i].position;

				if (RectTransformUtility.RectangleContainsScreenPoint(_left, touchPosition))
				{
					//Debug.Log("LEFT");

					horizontalInput = -1.0f;
				}

				if (RectTransformUtility.RectangleContainsScreenPoint(_right, touchPosition))
				{
					//Debug.Log("RIGHT");

					horizontalInput = 1.0f;
				}
			}

			_steering = Mathf.Lerp(_steering, horizontalInput, 5.0f * Time.deltaTime);

			//Debug.Log("h = " + horizontalInput + ", _steering = " + _steering);

			_car.Move(_steering, 1.0f, 0.0f, 0.0f);
		}
	}
}