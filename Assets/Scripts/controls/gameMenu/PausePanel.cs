namespace sneakyRacing
{
	using UnityEngine;
	using UnityEngine.SceneManagement;

	public class PausePanel : Panel 
	{
		private Transform _buttonPanelTransform;
		private Transform _pauseTransform;

		public void replayButtonClick()
		{
			if (_inputInvalidator.invalidateEvent() == false)
				return;

			(Level.instance as TrackLevel).gameOver();

			ScreenOverlay.instance.onCompleteEvent += onScreenFadeReplayComplete;
			ScreenOverlay.instance.fadeIn(0.5f);
		}

		private void onScreenFadeReplayComplete()
		{
			ScreenOverlay.instance.onCompleteEvent -= onScreenFadeReplayComplete;

			Scene currentScene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(currentScene.buildIndex);
		}

		public void quitButtonClick()
		{
			if (_inputInvalidator.invalidateEvent() == false)
				return;

			ScreenOverlay.instance.onCompleteEvent += onScreenFadeQuitComplete;
			ScreenOverlay.instance.fadeIn(0.5f);
		}

		private void onScreenFadeQuitComplete()
		{
			ScreenOverlay.instance.onCompleteEvent -= onScreenFadeQuitComplete;

			SceneManager.LoadScene("Menu");
		}

		public void resumeButtonClick()
		{
			if (_inputInvalidator.invalidateEvent() == false)
				return;

			pause = false;
		}

		public void pauseButtonClick()
		{
			pause = true;
		}

		public bool pause
		{
			set
			{
				if (value)
				{
					_pauseTransform.gameObject.SetActive(false);
					_buttonPanelTransform.gameObject.SetActive(true);

					AudioListener.volume = 0.0f;
					Time.timeScale = 0.0f;
				}
				else
				{
					_pauseTransform.gameObject.SetActive(true);
					_buttonPanelTransform.gameObject.SetActive(false);

					AudioListener.volume = 1.0f;
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
	}
}