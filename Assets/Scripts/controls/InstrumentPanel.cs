namespace sneakyRacing
{
	using UnityEngine;
	using UnityEngine.UI;

	public class InstrumentPanel : MonoBehaviour 
	{
		private Transform _pauseTransform;

		public GameObject pauseButton
		{
			get
			{
				return _pauseTransform.gameObject;
			}
		}

		private Text _scoreText;

		public int score
		{
			set
			{
				_scoreText.text = value.ToString();
			}
		}

		private Text _timeText;

		public float time
		{
			set
			{
				_timeText.text = value.ToString();
			}
		}

		private Text _coinsText;

		public int coins
		{
			set
			{
				_coinsText.text = value.ToString();
			}
		}

		private void Awake()
		{
			_pauseTransform = transform.Find("PauseButton");

			_coinsText = transform.Find("Coins/Text").GetComponent<Text>();
			_scoreText = transform.Find("Score/Text").GetComponent<Text>();
			_timeText = transform.Find("Time/Text").GetComponent<Text>();
		}

		private void Start()
		{
			//ammo = 0;
			//distance = 0;
			//energy = 0.0f;
			//coins = SettingManager.data.coins;
		}

		private void Update()
		{
			
		}
	}
}