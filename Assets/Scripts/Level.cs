namespace sneakyRacing
{
	using UnityEngine;
	using UnityEngine.SceneManagement;

	public class Level : MonoBehaviour
	{
		protected static Level _instance = null;

		public static Level instance
		{
			get
			{
				if (_instance != null)
					return _instance;

				throw new UnityException("Level instance is not initialized.");
			}
		}

		public int coins
		{
			set
			{
				SettingManager.data.coins = value;
			}
		}

		public bool pauseGame
		{
			get
			{
				return (Time.timeScale == 0.0f);
			}
			set
			{
				// pause game only before finish
				//if (gameMenu.pausePanel.visible)
				//	gameMenu.pausePanel.pause = value;

				//AudioListener.pause = value;
			}
		}

		private void Awake()
		{
			FPSDisplay.DEBUG = true;
			Stats.DEBUG = true;

			if (_instance != null)
				throw new UnityException("Level instance is already initialized.");

			_instance = this;

			// initialize game
			if (SettingManager.instance.isInitialized == false)
			{
				SettingManager.instance.init();
			}
		}

		private void Start()
		{
			AudioListener.volume = 0.0f;
			Application.targetFrameRate = 60;

			Time.timeScale = 1.0f;  // in previous scene game could be paused

			ScreenOverlay.instance.fadeOut(0.35f, 0.25f);
		}

		private void OnApplicationPause(bool pause)
		{
			if (pause)
			{
				SettingManager.instance.flush();
				//GameSaver.SaveGame ();
			}
		}

		private void OnDestroy()
		{
			SettingManager.instance.flush();
			//GameSaver.SaveGame ();
		}
	}
}