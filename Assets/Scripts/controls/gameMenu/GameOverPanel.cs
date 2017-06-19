namespace sneakyRacing
{
	using UnityEngine;
	using UnityEngine.SceneManagement;

	public class GameOverPanel : Panel 
	{/*
		private Transform _buttonsTransform;
		private Transform _pauseTransform;
		*/
		public override void show()
		{
			AudioListener.volume = 0.0f;

			gameObject.SetActive(true);

			Star[] stars = transform.Find("Stars").GetComponentsInChildren<Star>();

			TrackData trackData = SettingManager.data.trackList[SettingManager.data.currentTrack];

			for (int i = 0; i < stars.Length; i++)
			{
				Star star = stars[i];
				star.isActive = (i < trackData.starCount);
			}

			// if current track is last opened, and we still have unopened tracks - make next track available to play
			if (SettingManager.data.currentTrack == SettingManager.data.trackList.Count - 1 &&
				SettingManager.data.trackList.Count < SettingManager.tracks.Count)
			{
				SettingManager.data.trackList.Add(new TrackData());
			}
		}
		
		public override void hide()
		{
			gameObject.SetActive(false);
		}
		
		public void nextButtonClick()
		{
			if (_inputInvalidator.invalidateEvent() == false)
				return;

			SettingManager.data.currentTrack++;

			// load next track
			if (SettingManager.data.currentTrack < SettingManager.data.trackList.Count)
			{
				//SettingManager.data.trackList.Add(new TrackData());

				ScreenOverlay.instance.onCompleteEvent += onScreenFadeNextComplete;
				ScreenOverlay.instance.fadeIn(0.5f);
			}
			// player has finished all tracks - return to menu
			else
			{
				SettingManager.data.currentTrack = 0;

				ScreenOverlay.instance.onCompleteEvent += onScreenFadeQuitComplete;
				ScreenOverlay.instance.fadeIn(0.5f);
			}
		}

		private void onScreenFadeNextComplete()
		{
			ScreenOverlay.instance.onCompleteEvent -= onScreenFadeNextComplete;

			Track nextTrack = SettingManager.tracks[SettingManager.data.currentTrack];

			SceneManager.LoadScene(nextTrack.sceneName);
		}

		public void replayButtonClick()
		{
			if (_inputInvalidator.invalidateEvent() == false)
				return;

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
		/*
		protected override void Awake()
		{
			base.Awake();

			//_pauseTransform = transform.Find("PauseButton");
			//_buttonPanelTransform = transform.Find("ButtonPanel");
		}
		*/
	}
}