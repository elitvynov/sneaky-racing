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

		private float _input = 0.0f;

		private void Update()
		{
			for (int i = 0; i < Input.touches.Length; i++)
			{
				Vector2 touchPosition = Input.touches[i].position;

				if (RectTransformUtility.RectangleContainsScreenPoint(_left, touchPosition))
				{
					//Debug.Log("LEFT");

					_input = Mathf.Clamp(_input - 4.0f * Time.deltaTime, -1, 0);

					//_car.Move(-1.0f, 1.0f, 0.0f, 0.0f);
				}

				if (RectTransformUtility.RectangleContainsScreenPoint(_right, touchPosition))
				{
					//Debug.Log("RIGHT");

					_input = Mathf.Clamp(_input + 4.0f * Time.deltaTime, 0, 1);

					//_car.Move(1.0f, 1.0f, 0.0f, 0.0f);
				}
			}

			_input *= (5.0f * Time.deltaTime);

			Debug.Log("_input = " + _input);

			_car.Move(_input, 1.0f, 0.0f, 0.0f);
		}
	}
}