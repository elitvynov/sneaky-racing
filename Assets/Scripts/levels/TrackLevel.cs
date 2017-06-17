namespace sneakyRacing
{
	using UnityEngine;

	using UnityStandardAssets.Vehicles.Car;

	public class TrackLevel : Level 
	{
		

		public int coins
		{
			set
			{
				SettingManager.data.coins = value;
			}
		}

		private CarController _player;
		
		public CarController player
		{
			get
			{
				return _player;
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
				if ((menu as GameMenu).pausePanel.visible)
					(menu as GameMenu).pausePanel.pause = value;

				//AudioListener.pause = value;
			}
		}

		public void gameOver()
		{
			Debug.LogWarning("gameOver()");

			//(menu as GameMenu).pausePanel.visible = false;

			GhostRecorder ghostRecorder = _player.transform.GetComponentInChildren<GhostRecorder>();
			ghostRecorder.stop();

			SettingManager.instance.completeTrack(0);
		}

		protected override void Awake()
		{
			base.Awake();

			_player = transform.Find("Car").GetComponent<CarController>();
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