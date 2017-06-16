namespace sneakyRacing
{
	using UnityEngine;
	using UnityEngine.SceneManagement;

	public class GameOverPanel : Panel 
	{
		private Transform _buttonsTransform;
		private Transform _pauseTransform;

		public void show()
		{
			Time.timeScale = 0.0f;

			gameObject.SetActive(true);
		}

		public void hide()
		{
			gameObject.SetActive(false);
		}

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
	}
}