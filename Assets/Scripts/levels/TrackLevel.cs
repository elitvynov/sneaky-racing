namespace sneakyRacing
{
	using UnityEngine;

	using System.Collections;

	using UnityStandardAssets.Vehicles.Car;

	public class TrackLevel : Level 
	{
		private float _time = 0.0f;

		private CarController _player;
		
		public CarController player
		{
			get
			{
				return _player;
			}
		}

		private CheckpointManager _checkpointManager;

		public bool pauseGame
		{
			get 
			{ 
				return (Time.timeScale == 0.0f); 
			}
			set 
			{
				// pause game only before finish
				if ((menu as GameMenu).gameOverPanel.visible == false)
					(menu as GameMenu).pausePanel.pause = value;

				//AudioListener.pause = value;
				//AudioListener.volume = 0.0f;

				Debug.LogWarning("AudioListener.volume = " + AudioListener.volume);
			}
		}

		private bool _gameIsOver = false;

		public void gameOver()
		{
			Debug.LogWarning("gameOver()");

			//(menu as GameMenu).pausePanel.visible = false;

			_gameIsOver = true;

			_player.GetComponent<CarUserControl>().enabled = false;

			StartCoroutine(gameOverCoroutine());
		}

		private IEnumerator gameOverCoroutine()
		{
			Debug.LogWarning("IEnumerator gameOver()");

			yield return new WaitForSeconds(1.5f);

			GhostRecorder ghostRecorder = _player.transform.GetComponentInChildren<GhostRecorder>();
			ghostRecorder.stop();

			Debug.LogWarning("ghostRecorder.getReplay(): " + _time);

			SettingManager.instance.setReplay(ghostRecorder.getReplay(), _time);

			if (_time <= 10.0f)
			{
				SettingManager.instance.completeTrack(3, _time);
			}
			else
			{
				if (_time <= 12.0f)
				{
					SettingManager.instance.completeTrack(2, _time);
				}
				else
				{
					SettingManager.instance.completeTrack(1, _time);
				}
			}

			(menu as GameMenu).gameOverPanel.show();
		}

		protected override void Awake()
		{
			base.Awake();

			_player = transform.Find("Car").GetComponent<CarController>();
			_checkpointManager = transform.Find("Checkpoints").GetComponent<CheckpointManager>();
		}

		protected override void OnApplicationPause(bool pause)
		{
			base.OnApplicationPause(pause);

			if (pause)
			{
				pauseGame = true;
			}
		}

		private void Update()
		{
			if (!_gameIsOver)
			{
				_time += Time.deltaTime;

				(menu as GameMenu).instrumentPanel.time = _time;
			}
		}
	}
}