namespace sneakyRacing
{
	using UnityEngine;

	/*
	* Development from 24.01.2016
	* @author Eugene Litvynov <litebox.dh@gmail.com>
	*/
	public class TrackLevel : Level 
	{
		

		public int coins
		{
			set
			{
				SettingManager.data.coins = value;
			}
		}

		private GameMenu _gameMenu;
		
		public GameMenu gameMenu
		{
			get
			{
				if (_gameMenu == null)
					_gameMenu = transform.Find("GameMenu").GetComponent<GameMenu>();
				
				return _gameMenu;
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
				if (gameMenu.pausePanel.visible)
					gameMenu.pausePanel.pause = value;

				//AudioListener.pause = value;
			}
		}

		public void finish()
		{
			gameMenu.pausePanel.visible = false;
		}

		protected override void Awake()
		{
			base.Awake();

			
		}

		protected override void OnApplicationPause(bool pause)
		{
			base.OnApplicationPause(pause);

			if (pause)
			{
				pauseGame = true;
			}
		}
	}
}