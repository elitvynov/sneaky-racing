namespace sneakyRacing
{
	using UnityEngine;
	using UnityEngine.SceneManagement;
	using UnityEngine.UI;

	using System.Collections.Generic;

	public class LevelPanel : Panel
	{
		private Transform _competitorPanelTransform;
		private Transform _competitorContentTransform;

		private TrackItemRenderer[] _winnerItemRenderers;

		private Transform _inviteItemRenderer;
		private Transform _itemRendererPrefab;

		private string _currentSceneName;

		public void closeButtonClick()
		{
			if (_inputInvalidator.invalidateEvent() == false)
				return;

			(Level.instance.menu as MainMenu).mainMenuPanel.show();
			hide();
		}

		private void loadTrack(int index, string sceneName)
		{
			Debug.Log("loadRoom(): " + index + " / " + sceneName);

			if (ScreenOverlay.instance.onCompleteEvent == null)
			{
				ScreenOverlay.instance.onCompleteEvent += onScreenFadePlayComplete;
				ScreenOverlay.instance.fadeIn(0.5f);

				SettingManager.data.currentTrack = index;

				_currentSceneName = sceneName;
			}
		}

		private void onScreenFadePlayComplete()
		{
			ScreenOverlay.instance.onCompleteEvent -= onScreenFadePlayComplete;

			//Scene currentScene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(_currentSceneName);
		}

		private void updateTrackList(List<Track> trackList)
		{
			Debug.LogWarning("updateCompetitors: " + trackList.Count);

			for (int i = 0; i < trackList.Count; i++)
			{
				GameObject itemRendererObject = Instantiate(_itemRendererPrefab.gameObject) as GameObject;
				itemRendererObject.transform.SetParent(_competitorContentTransform, false);
				itemRendererObject.SetActive(true);

				TrackItemRenderer itemRenderer = itemRendererObject.GetComponent<TrackItemRenderer>();

				Track track = trackList[i];

				//SocialUserData socialUser = FBDataManager.instance.getUserByIdentifier(user.id);

				if (track != null)
				{
					Button roomButton = itemRenderer.GetComponent<Button>();

					Debug.Log("AddListener(): " + i + " / " + track.sceneName);

					int trackIndex = i;

					roomButton.onClick.AddListener(() =>
					{
						loadTrack(trackIndex, track.sceneName);
					});

					if (i < SettingManager.data.trackList.Count)
					{
						roomButton.interactable = true;
						itemRenderer.stars = SettingManager.data.trackList[i].starCount;
					}
					else
					{
						roomButton.interactable = false;
						itemRenderer.stars = 0;
					}

					itemRenderer.iconName = track.iconName;
					itemRenderer.trackName = track.name;
				}
			}

			_inviteItemRenderer.SetAsLastSibling();
		}
		
		private void clearTrackList()
		{
			TrackItemRenderer[] competitorItemRenderers = _competitorContentTransform.GetComponentsInChildren<TrackItemRenderer>();

			for (int i = 0; i < competitorItemRenderers.Length; i++)
			{
				TrackItemRenderer itemRenderer = competitorItemRenderers[i];

				if (itemRenderer.transform != _itemRendererPrefab)
				{
					Destroy(itemRenderer.gameObject);
				}
			}
		}

		protected override void Awake()
		{
			base.Awake();

			_competitorPanelTransform = transform.Find("ScrollRect");

			_competitorContentTransform = _competitorPanelTransform.Find("Viewport/Content");
			_itemRendererPrefab = _competitorContentTransform.Find("ItemRenderer");
			_itemRendererPrefab.gameObject.SetActive(false);

			_inviteItemRenderer = _competitorContentTransform.Find("InfoItemRenderer");
		}

		private void OnEnable()
		{
			updateTrackList(SettingManager.tracks);
		}

		private void OnDisable()
		{
			clearTrackList();
		}
	}
}