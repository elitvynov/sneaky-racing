namespace sneakyRacing
{
	using UnityEngine;

	public class MainMenu : Menu
	{
		private MainMenuPanel _mainMenuPanel;

		public MainMenuPanel mainMenuPanel
		{
			get
			{
				return _mainMenuPanel;
			}
		}
		
		private LevelPanel _levelPanel;

		public LevelPanel levelPanel
		{
			get
			{
				return _levelPanel;
			}
		}

		private AboutPanel _aboutPanel;

		public AboutPanel aboutPanel
		{
			get
			{
				return _aboutPanel;
			}
		}

		private void Awake()
		{
			_mainMenuPanel = transform.Find("MenuPanel").GetComponent<MainMenuPanel>();
			_levelPanel = transform.Find("LevelPanel").GetComponent<LevelPanel>();
			_aboutPanel = transform.Find("AboutPanel").GetComponent<AboutPanel>();

			_mainMenuPanel.gameObject.SetActive(true);
			_levelPanel.gameObject.SetActive(false);
			_aboutPanel.gameObject.SetActive(false);
		}
	}
}