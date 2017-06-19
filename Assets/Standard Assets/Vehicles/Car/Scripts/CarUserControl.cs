using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use

		private float _supressSteering = 0.0f;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }

        private void FixedUpdate()
        {
            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");

			if (h < -0.5f || h > 0.5f)
				_supressSteering = Mathf.Clamp(_supressSteering + 0.5f * Time.deltaTime, 0.0f, 0.5f);
			else
				_supressSteering = Mathf.Clamp(_supressSteering - 2.0f * Time.deltaTime, 0.0f, 0.5f);

			//Debug.Log("h = " + h + ", _supressSteering = " + _supressSteering);

			//#if !MOBILE_INPUT
			float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, 1.0f - _supressSteering, 0.0f, 0.0f);
/*#else
            m_Car.Move(h, v, v, 0f);
#endif*/
        }
    }
}
