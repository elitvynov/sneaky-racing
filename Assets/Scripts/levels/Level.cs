namespace sneakyRacing
{
	using UnityEngine;

	public class Level : MonoBehaviour
	{
		private static Level _instance = null;

		public static Level instance
		{
			get
			{
				if (_instance != null)
					return _instance;

				throw new UnityException("Level instance is not initialized.");
			}
		}

		[SerializeField]
		private Menu _menu;

		public Menu menu
		{
			get
			{
				return _menu;
			}
		}

		protected virtual void Awake()
		{
			if (_instance != null)
				throw new UnityException("Level instance is already initialized.");

			_instance = this;

			// initialize game
			if (SettingManager.instance.isInitialized == false)
			{
				SettingManager.instance.init();
			}
		}

		protected virtual void Start()
		{
			AudioListener.volume = 0.0f;
			Application.targetFrameRate = 60;

			Time.timeScale = 1.0f;  // in previous scene game could be paused

			FPSDisplay.DEBUG = true;
			Stats.DEBUG = true;

			ScreenOverlay.instance.fadeOut(0.5f);
		}

		protected virtual void OnApplicationPause(bool pause)
		{
			AudioListener.pause = pause;

			if (pause)
			{
				SettingManager.instance.flush();
			}
		}

		protected virtual void OnDestroy()
		{
			SettingManager.instance.flush();
		}
	}
}