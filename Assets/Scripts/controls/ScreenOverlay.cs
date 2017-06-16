namespace sneakyRacing
{
	using UnityEngine;
	using UnityEngine.UI;

	public class ScreenOverlay : MonoBehaviour
	{
		public System.Action onCompleteEvent;

		private Image _overlayImage;
		
		private float _fadeIntensity = 1.0f;
		private float _fadeDelta = 0.0f;
		private float _fadeDelay = 0.0f;

		private bool _fadeVolume;

		private static ScreenOverlay _instance = null;
		
		public static ScreenOverlay instance 
		{
			get
			{
				if (_instance != null)
					return _instance;
				
				throw new UnityException("ScreenOverlay instance is not initialized.");
			}
		}

		public void Awake()
		{
			if (_instance != null)
				throw new UnityException("ScreenOverlay instance is already initialized.");

			_instance = this;

			_overlayImage = transform.GetComponent<Image>();
			_overlayImage.enabled = true;
			
			gameObject.SetActive(false);
		}

		public void fadeIn(float time = 1.0f, float delay = 0.0f, bool fadeVolume = true)
		{
			_fadeDelta = 1.0f / time;
			_fadeDelay = delay;
			_fadeIntensity = 0.0f;

			_fadeVolume = fadeVolume;

			gameObject.SetActive(true);
		}

		public void fadeOut(float time = 1.0f, float delay = 0.0f, bool fadeVolume = true)
		{
			_fadeDelta = -1.0f / time;
			_fadeDelay = delay;
			_fadeIntensity = 1.0f;

			_fadeVolume = fadeVolume;

			updateImage();

			gameObject.SetActive(true);
		}
		
		private void updateImage()
		{
			Color overlayColor = _overlayImage.color;
			overlayColor.a = _fadeIntensity;
			
			_overlayImage.color = overlayColor;
		}
		
		private void Update()
		{
			if (_fadeDelay > 0.0f)
				_fadeDelay -= Time.unscaledDeltaTime;
			else
			{
				float deltaTime = Mathf.Clamp(Time.unscaledDeltaTime, 0.0f, 0.035f);
				float fadeStep = _fadeDelta * deltaTime;

				_fadeIntensity = Mathf.Clamp01(_fadeIntensity + fadeStep);

				//Console.trace("fadeStep = " + fadeStep + ", deltaTime = " + deltaTime + ", Time.deltaTime = " + Time.deltaTime);
				//Console.trace("fadeIntensity = " + _fadeIntensity);

				updateImage();

				if (_fadeVolume)
					AudioListener.volume = 1.0f - _fadeIntensity;

				if (_fadeIntensity == 0.0f || _fadeIntensity == 1.0f)
				{
					if (onCompleteEvent != null)
						onCompleteEvent();
				}
				
				if (_fadeIntensity == 0.0f)
					gameObject.SetActive(false);
			}
		}
	}
}