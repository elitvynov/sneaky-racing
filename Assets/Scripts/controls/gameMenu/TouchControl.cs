namespace sneakyRacing
{
	using UnityEngine;

	using UnityStandardAssets.CrossPlatformInput;
	using UnityStandardAssets.Vehicles.Car;

	public class TouchControl : MonoBehaviour 
	{
		[SerializeField]
		private CarController _car;

		private RectTransform _left;
		private RectTransform _right;

		//private CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis;

		private void Awake()
		{
			_left = transform.Find("Left") as RectTransform;
			_right = transform.Find("Right") as RectTransform;
		}

		private void Start()
		{
			gameObject.SetActive(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer);
		}
		/*
		private void OnEnable()
		{
			m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis("Horizontal");
			CrossPlatformInputManager.RegisterVirtualAxis(m_HorizontalVirtualAxis);
		}

		private void OnDisable()
		{
			if (CrossPlatformInputManager.AxisExists("Horizontal"))
				CrossPlatformInputManager.UnRegisterVirtualAxis("Horizontal");
		}

		private void UpdateVirtualAxes(Vector3 value)
		{
			value = value.normalized;

			m_HorizontalVirtualAxis.Update(value.x);
		}
		*/
		/*
		private void Update()
		{
			Debug.Log("Input.touchCount = " + Input.touchCount);

			for (int i = 0; i < Input.touches.Length; i++)
			{
				Vector2 touchPosition = Input.touches[i].position;

				if (RectTransformUtility.RectangleContainsScreenPoint(_left, touchPosition))
				{
					Debug.Log("LEFT");

					UpdateVirtualAxes(new Vector3(-1, 0, 0));
					//_car.Move(-1.0f, 1.0f, 0.0f, 0.0f);
				}

				if (RectTransformUtility.RectangleContainsScreenPoint(_right, touchPosition))
				{
					Debug.Log("RIGHT");

					UpdateVirtualAxes(new Vector3(1, 0, 0));
					//_car.Move(1.0f, 1.0f, 0.0f, 0.0f);
				}
			}
		}*/

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

					//_car.Move(-1.0f, 1.0f, 0.0f, 0.0f);
				}

				if (RectTransformUtility.RectangleContainsScreenPoint(_right, touchPosition))
				{
					//Debug.Log("RIGHT");

					horizontalInput = 1.0f;

					//_car.Move(1.0f, 1.0f, 0.0f, 0.0f);
				}
			}

			_steering = Mathf.Lerp(_steering, horizontalInput, 5.0f * Time.deltaTime);

			Debug.Log("h = " + horizontalInput + ", _steering = " + _steering);

			_car.Move(_steering, 1.0f, 0.0f, 0.0f);
		}
	}
}