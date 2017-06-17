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

		private void loadRoom(string index)
		{
			Debug.Log("loadRoom(): " + index);

			if (ScreenOverlay.instance.onCompleteEvent == null)
			{
				ScreenOverlay.instance.onCompleteEvent += onScreenFadePlayComplete;
				ScreenOverlay.instance.fadeIn(0.5f);

				_currentSceneName = index;
			}
		}

		private void onScreenFadePlayComplete()
		{
			ScreenOverlay.instance.onCompleteEvent -= onScreenFadePlayComplete;

			//Scene currentScene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(_currentSceneName);
		}

		private void updateCompetitors(List<Track> userList)
		{
			Debug.LogWarning("updateCompetitors: " + userList.Count);

			for (int i = 0; i < userList.Count; i++)
			{
				GameObject itemRendererObject = Instantiate(_itemRendererPrefab.gameObject) as GameObject;
				itemRendererObject.transform.SetParent(_competitorContentTransform, false);
				itemRendererObject.SetActive(true);

				TrackItemRenderer itemRenderer = itemRendererObject.GetComponent<TrackItemRenderer>();

				Track user = userList[i];

				//SocialUserData socialUser = FBDataManager.instance.getUserByIdentifier(user.id);

				if (user != null)
				{
					Button roomButton = itemRenderer.GetComponent<Button>();

					roomButton.onClick.AddListener(() => {
						loadRoom(user.sceneName);
					});

					itemRenderer.iconName = user.iconName;
					itemRenderer.trackName = user.name;
					//itemRenderer.index = i + 1;
				}
			}

			_inviteItemRenderer.SetAsLastSibling();
		}
		
		private void clearCompetitors()
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

			_competitorPanelTransform = transform.Find("CompetitorPanel");

			_competitorContentTransform = _competitorPanelTransform.Find("Viewport/Content");
			_itemRendererPrefab = _competitorContentTransform.Find("ItemRenderer");
			_itemRendererPrefab.gameObject.SetActive(false);

			_inviteItemRenderer = _competitorContentTransform.Find("InfoItemRenderer");
		}

		private void OnEnable()
		{
			updateCompetitors(SettingManager.tracks);
		}

		private void OnDisable()
		{
			clearCompetitors();
		}
	}
}