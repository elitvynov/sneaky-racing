namespace sneakyRacing
{
	using UnityEngine;
	using UnityEngine.SceneManagement;

	public class PausePanel : Panel 
	{
		private Transform _buttonPanelTransform;
		private Transform _pauseTransform;

		public void quitButtonClick()
		{
			if (_inputInvalidator.invalidateEvent() == false)
				return;

			ScreenOverlay.instance.onCompleteEvent += onScreenFadeQuitComplete;
			ScreenOverlay.instance.fadeIn(0.5f);

			//SoundPlayer.instance.play("ButtonClick");
		}

		private void onScreenFadeQuitComplete()
		{
			ScreenOverlay.instance.onCompleteEvent -= onScreenFadeQuitComplete;

			Scene currentScene = SceneManager.GetActiveScene();
			//SceneManager.LoadScene("Menu");// currentScene.buildIndex);
			SceneManager.LoadScene(currentScene.buildIndex);
		}

		public void resumeButtonClick()
		{
			if (_inputInvalidator.invalidateEvent() == false)
				return;

			pause = false;

			//SoundPlayer.instance.play("ButtonClick");
		}

		public void pauseButtonClick()
		{
			pause = true;

			//SoundPlayer.instance.play("ButtonClick");
		}

		public bool pause
		{
			set
			{

				//if (_quitConfirmation.isOpened || _retireConfirmation.isOpened)
				//	return;

				//_trafficLights.pause();

				//Console.warning("pause = " + value);

				if (value)
				{
					_pauseTransform.gameObject.SetActive(false);
					_buttonPanelTransform.gameObject.SetActive(true);

					Time.timeScale = 0.0f;
				}
				else
				{
					_buttonPanelTransform.gameObject.SetActive(false);

					//_trafficLights.onCompleteEvent += onPauseComplete;
					//_trafficLights.activate();

					_pauseTransform.gameObject.SetActive(true);
					Time.timeScale = 1.0f;
				}
			}
		}

		protected override void Awake()
		{
			base.Awake();

			_pauseTransform = transform.Find("PauseButton");
			_buttonPanelTransform = transform.Find("ButtonPanel");
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Level.instance.pauseGame = true;
			}
		}
	}
}