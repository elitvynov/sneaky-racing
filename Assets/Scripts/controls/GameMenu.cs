namespace sneakyRacing
{
	using UnityEngine;
	using UnityEngine.UI;

	public class GameMenu : Menu
	{
		private NotificationToast _notificationToast;

		public NotificationToast notificationToast
		{
			get
			{
				return _notificationToast;
			}
		}
		
		private PausePanel _pausePanel;

		public PausePanel pausePanel
		{
			get
			{
				return _pausePanel;
			}
		}

		private GameOverPanel _gameOverPanel;

		public GameOverPanel gameOverPanel
		{
			get
			{
				return _gameOverPanel;
			}
		}

		private InstrumentPanel _instrumentPanel;

		public InstrumentPanel instrumentPanel
		{
			get
			{
				return _instrumentPanel;
			}
		}

		private void onGameOver()
		{
			_gameOverPanel.show();
		}

		private void Awake()
		{
			_pausePanel = transform.Find("PausePanel").GetComponent<PausePanel>();
			//_tutorialToast = transform.FindChild("TutorialToast").GetComponent<TutorialToast>();
			_notificationToast = transform.Find("NotificationToast").GetComponent<NotificationToast>();
			_gameOverPanel = transform.Find("GameOverPanel").GetComponent<GameOverPanel>();
			//_resultProfit = transform.FindChild("ResultProfit").GetComponent<ResultProfit>();
			_instrumentPanel = transform.Find("InstrumentPanel").GetComponent<InstrumentPanel>();

			_pausePanel.gameObject.SetActive(true);
			//_tutorialToast.gameObject.SetActive(false);
			_notificationToast.gameObject.SetActive(false);
			_gameOverPanel.gameObject.SetActive(false);
			//_resultProfit.gameObject.SetActive(false);
			//_resultTable.gameObject.SetActive(false);
		}

		private void Start()
		{
			ScreenOverlay.instance.fadeOut(0.5f);
		}
	}
}