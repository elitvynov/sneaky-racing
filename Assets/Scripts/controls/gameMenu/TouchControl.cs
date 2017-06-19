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

		private void FixedUpdate()
		{
			//if (Input.touchCount > 0)
			{
				Debug.Log("Input.touchCount = " + Input.touchCount);

				for (int i = 0; i < Input.touches.Length; i++)
				{
					Vector2 touchPosition = Input.touches[i].position;

					if (RectTransformUtility.RectangleContainsScreenPoint(_left, touchPosition))
					{
						Debug.Log("LEFT");

						_car.Move(-1.0f, 1.0f, 0.0f, 0.0f);
					}

					if (RectTransformUtility.RectangleContainsScreenPoint(_right, touchPosition))
					{
						Debug.Log("RIGHT");

						_car.Move(1.0f, 1.0f, 0.0f, 0.0f);
					}
				}
			}
		}
	}
}